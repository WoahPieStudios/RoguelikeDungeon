using System;
using System.Linq;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using UnityEditor;
using UnityEditor.Sprites;
using UnityEditor.Callbacks;

using Game.Characters;

namespace Game.CharactersEditor
{
    public class CharactersEditorWindow : EditorWindow
    {
        AssetItem[] _AssetItems = null;

        SerializedAssetData _SelectedData = null;

        Vector2 _ExplorerScrollPosition;
        Vector2 _ContentScrollPosition; 

        float _ItemScale = 0;

        [MenuItem("Window/Characters Window")]
        static CharactersEditorWindow OpenWindow()
        {
            CharactersEditorWindow window = CharactersEditorWindow.GetWindow<CharactersEditorWindow>("Characters Window");

            window.RefreshDatabase();

            return window;
        }

        // [OnOpenAsset]
        // static bool OpenWindow(int instanceID, int line)
        // {
        //     UnityEngine.Object assetObject = EditorUtility.InstanceIDToObject(instanceID);

        //     if(assetObject is IIcon)
        //     {
        //         CharactersEditorWindow window = OpenWindow();

        //         window._SelectedData = new SerializedAssetData(assetObject);

        //         return true;
        //     }

        //     else
        //         return false;
        // }

        class AssetItem
        {
            Rect _Rect;
            readonly SerializedAssetData _SerializedAssetData;

            public SerializedAssetData serializedAssetData => _SerializedAssetData;
            
            public AssetItem(UnityEngine.Object assetObject)
            {
                _SerializedAssetData = new SerializedAssetData(assetObject);
            }


            GUIContent LimitLabel(string text)
            {
                GUIContent content = new GUIContent(text);

                // Vector2 size = GUI.skin.label.CalcSize(content);

                return content;
            }

            public bool Draw(float size)
            {
                GUIStyle textStyle = new GUIStyle();

                bool isSelected;

                textStyle.normal.textColor = Color.white;
                textStyle.clipping = TextClipping.Clip;

                if(size > 40f)
                {
                    textStyle.alignment = TextAnchor.MiddleCenter;

                    EditorGUILayout.BeginVertical();

                    isSelected = GUILayout.Button(_SerializedAssetData.icon, GUILayout.Width(size), GUILayout.Height(size));

                    GUILayout.Label(LimitLabel(_SerializedAssetData.name), textStyle, GUILayout.Width(size));
                    
                    EditorGUILayout.EndVertical();
                    
                }

                else
                {
                    textStyle.alignment = TextAnchor.MiddleLeft;

                    isSelected = GUILayout.Button(LimitLabel(_SerializedAssetData.name));
                }

                return isSelected;
            }

            public bool Contains(Vector2 point)
            {
                return _Rect.Contains(point);
            }
        }

        void OnEnable() 
        {
            _SelectedData = null;
            
            RefreshDatabase();
        }

        void OnDisable() 
        {
            _ExplorerScrollPosition = Vector2.zero;
            _ContentScrollPosition = Vector2.zero;
        }

        void OnGUI() 
        {
            EditorGUILayout.BeginVertical();

            DrawMenuBar();

            DrawContent();

            EditorGUILayout.EndVertical();

            CheckAndDrawContextMenu();
        } 
        
        void DrawMenuBar()
        {
            EditorGUILayout.BeginHorizontal();

            if(GUILayout.Button("Create New Asset"))
            {
                CreateAssetWindow.GetWindow<CreateAssetWindow>("Create Asset");
            }

            if(GUILayout.Button("Refresh"))
            {
                RefreshDatabase();
            }

            EditorGUILayout.EndHorizontal();
        }

        void DrawContent()
        {
            EditorGUILayout.BeginHorizontal();

            DrawExplorerArea();

            DrawAssetContent();

            EditorGUILayout.EndHorizontal();
        }

        void DrawExplorerArea()
        {
            float itemSize = Mathf.Lerp(40, 100, _ItemScale);

            EditorGUILayout.BeginVertical();

            _ExplorerScrollPosition = EditorGUILayout.BeginScrollView(_ExplorerScrollPosition);

            if(_AssetItems != null && _AssetItems.Length > 0)
            {
                // TEMP SOLUTION FOR REMOVING UNEXPECTED DELETION OF ASSET
                _AssetItems = _AssetItems.Where(data => data.serializedAssetData.assetObject).ToArray();
                
                foreach(AssetItem assetItem in _AssetItems)
                {
                    if(assetItem.Draw(itemSize))
                    {
                        _SelectedData = assetItem.serializedAssetData;
                    }
                }
            }
            
            EditorGUILayout.EndScrollView();
            
            _ItemScale = GUILayout.HorizontalSlider(_ItemScale, 0, 1); 

            EditorGUILayout.EndVertical();
        }

        void DrawAssetContent()
        {
            EditorGUILayout.BeginVertical("box");

            if(_SelectedData != null && _SelectedData.assetObject)
            {
                EditorGUI.BeginChangeCheck();

                SerializedProperty property = _SelectedData.serializedObject.GetIterator();

                property.NextVisible(true);

                _ContentScrollPosition = EditorGUILayout.BeginScrollView(_ContentScrollPosition);// BeginVertical();
                
                EditorGUILayout.LabelField(_SelectedData.name);

                while(property.NextVisible(true))
                {
                    EditorGUILayout.PropertyField(property);
                }

                EditorGUILayout.EndScrollView();

                if(EditorGUI.EndChangeCheck())
                {
                    _SelectedData.UpdateData();

                    DrawExplorerArea();
                }
            }

            EditorGUILayout.EndVertical();
        }

        void RefreshDatabase()
        {
            _AssetItems = AssetDatabase.GetAllAssetPaths().
                Select(path => AssetDatabase.LoadAssetAtPath(path, typeof(UnityEngine.Object))).
                Where(assetObject => assetObject is IIcon).
                Select(assetObject => new AssetItem(assetObject)).
                ToArray();
        }

        void CheckAndDrawContextMenu()
        {
            Rect controlRect = EditorGUILayout.GetControlRect();

            Event currentEvent = Event.current; 

            if(currentEvent.type == EventType.ContextClick)
            {
                GenericMenu menu = new GenericMenu();

                menu.AddDisabledItem(new GUIContent("I clicked on a thing"));
                menu.AddItem(new GUIContent("Do a thing"), false, DoSomething, "Blah");
                menu.ShowAsContext();
            }
        }

        void DoSomething(object o)
        {
            Debug.Log(o);
        }
    }
}