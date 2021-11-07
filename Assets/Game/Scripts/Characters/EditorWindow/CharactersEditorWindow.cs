using System;
using System.Linq;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.Sprites;

using Game.Characters;

namespace Game.CharactersEditor
{
    public class CharactersEditorWindow : EditorWindow
    {
        SerializedAssetData[] _SerializedAssetDatas = null;

        SerializedAssetData _SelectedData = null;

        Vector2 _ExplorerScrollPosition;
        Vector2 _ContentScrollPosition; 


        [MenuItem("Window/Characters Window")]
        static CharactersEditorWindow OpenWindow()
        {
            CharactersEditorWindow window = CharactersEditorWindow.GetWindow<CharactersEditorWindow>("Characters Window");

            window.RefreshDatabase();

            return window;
        }

        [OnOpenAsset]
        static bool OpenWindow(int instanceID, int line)
        {
            UnityEngine.Object assetObject = EditorUtility.InstanceIDToObject(instanceID);

            if(assetObject is IIcon)
            {
                CharactersEditorWindow window = OpenWindow();

                window._SelectedData = new SerializedAssetData(assetObject);

                return true;
            }

            else
                return false;
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
            _ExplorerScrollPosition = EditorGUILayout.BeginScrollView(_ExplorerScrollPosition);

            if(_SerializedAssetDatas != null && _SerializedAssetDatas.Length > 0)
            {
                // TEMP SOLUTION FOR REMOVING UNEXPECTED DELETION OF ASSET
                _SerializedAssetDatas = _SerializedAssetDatas.Where(data => data.assetObject).ToArray();


                foreach(SerializedAssetData data in _SerializedAssetDatas)
                {
                    EditorGUILayout.BeginVertical();

                    if(GUILayout.Button(data.icon ? data.icon : null, GUILayout.Width(100), GUILayout.Height(100)))
                    {
                        _SelectedData = data;
                    }

                    EditorGUILayout.LabelField(data.name);

                    EditorGUILayout.EndVertical();
                }
            }
            
            EditorGUILayout.EndScrollView();
        }

        void DrawAssetContent()
        {
            if(_SelectedData != null && _SelectedData.assetObject)
            {
                EditorGUI.BeginChangeCheck();

                SerializedProperty property = _SelectedData.serializedObject.GetIterator();

                property.NextVisible(true);
                
                //AssetDatabase.RenameAsset()

                _ContentScrollPosition = EditorGUILayout.BeginScrollView(_ContentScrollPosition);// BeginVertical();
                
                EditorGUILayout.LabelField(_SelectedData.name);

                while(property.NextVisible(true))
                {
                    EditorGUILayout.PropertyField(property);
                }

                EditorGUILayout.EndScrollView(); //EditorGUILayout.EndVertical();

                if(EditorGUI.EndChangeCheck())
                {
                    _SelectedData.UpdateData();

                    DrawExplorerArea();
                }
            }
        }

        void RefreshDatabase()
        {
            _SerializedAssetDatas = AssetDatabase.GetAllAssetPaths().
                Select(path => AssetDatabase.LoadAssetAtPath(path, typeof(UnityEngine.Object))).
                Where(assetObject => assetObject is IIcon).
                Select(assetObject => new SerializedAssetData(assetObject)).
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

                Debug.Log("Blah");
            }
        }

        void DoSomething(object o)
        {
            Debug.Log(o);
        }
    }
}