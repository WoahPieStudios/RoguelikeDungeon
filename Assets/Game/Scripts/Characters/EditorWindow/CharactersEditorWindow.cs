using System;
using System.Linq;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using UnityEditor;
using UnityEditor.Callbacks;

using Game.Characters;

namespace Game.CharactersEditor
{
    public class CharactersEditorWindow : EditorWindow
    {
        AssetItem[] _AssetItems = null;
        AssetItem[] _DisplayAssetItems = null;
 
        AssetItem _SelectedAssetItem = null;

        Vector2 _ExplorerScrollPosition;
        Vector2 _ContentScrollPosition; 

        float _ItemScale = 0;

        float _ExplorerAreaWidth;
        float _ContentAreaWidth;

        string _SearchQuery;

        bool _IsMovingSeperator;

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

                window.RefreshDatabase();

                AssetItem assetItem = window._AssetItems.First(assetItem => assetItem.assetObject == assetObject);

                if(assetItem != null)
                {
                    window._SelectedAssetItem = assetItem;

                    return true;
                }

                else
                    return false;
            }

            else
                return false;
        }

        void OnEnable() 
        {
            _SelectedAssetItem = null;
            
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
        } 

        IEnumerable<AssetItem> GetAssets()
        {
            UnityEngine.Object assetObject;

            foreach(string path in AssetDatabase.GetAllAssetPaths())
            {
                assetObject = AssetDatabase.LoadAssetAtPath(path, typeof(UnityEngine.Object));

                if(assetObject is IIcon)
                    yield return new AssetItem(assetObject, path);
            }
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

            DrawSeperator();

            DrawAssetContent();

            EditorGUILayout.EndHorizontal();
        }

        void DrawSeperator()
        {
            float width = position.width;

            Rect rect = EditorGUILayout.BeginVertical(GUILayout.Width(10));

            GUILayout.FlexibleSpace();

            Handles.color = Color.grey;
            Handles.DrawLine(rect.center + Vector2.up * (rect.height / 2), rect.center + Vector2.down * (rect.height / 2));

            GUILayout.EndVertical();
            EditorGUIUtility.AddCursorRect(GUILayoutUtility.GetLastRect(), MouseCursor.ResizeHorizontal);

            if(rect.Contains(EditorInput.mousePosition) && EditorInput.MouseDown(0))
                _IsMovingSeperator = true;

            else if(EditorInput.MouseUp(0))
                _IsMovingSeperator = false;

            if(_IsMovingSeperator && EditorInput.MouseDrag(0))
            {
                float percentage = Mathf.Clamp(Mathf.InverseLerp(0, width, EditorInput.mousePosition.x), 0.2f, 0.8f);

                width -= 10;
                
                _ExplorerAreaWidth = width * percentage;
                _ContentAreaWidth = width * (1 - percentage);

                Repaint();
            }
        }

        void DrawExplorerArea()
        {
            float itemSize = Mathf.Lerp(40, 100, _ItemScale);

            EditorGUILayout.BeginVertical(GUILayout.Width(_ExplorerAreaWidth));

            DrawSearchBar();

            _ExplorerScrollPosition = EditorGUILayout.BeginScrollView(_ExplorerScrollPosition);
    
            DrawAssetItems(itemSize);

            GUILayout.FlexibleSpace();

            EditorGUILayout.EndScrollView();

            EditorGUILayout.BeginHorizontal(GUILayout.Height(20));

            GUILayout.FlexibleSpace();

            _ItemScale = GUILayout.HorizontalSlider(_ItemScale, 0, 1, GUILayout.Width(100)); 

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.EndVertical();
        }

        void DrawAssetContent()
        {
            EditorGUILayout.BeginVertical("box", GUILayout.Width(_ContentAreaWidth));

            if(_SelectedAssetItem != null && _SelectedAssetItem.assetObject)
            {
                EditorGUI.BeginChangeCheck();

                SerializedProperty property = _SelectedAssetItem.serializedAssetData.serializedObject.GetIterator();

                property.NextVisible(true);

                _ContentScrollPosition = EditorGUILayout.BeginScrollView(_ContentScrollPosition);// BeginVertical();
                
                EditorGUILayout.LabelField(_SelectedAssetItem.name);

                while(property.NextVisible(false))
                    EditorGUILayout.PropertyField(property);

                EditorGUILayout.EndScrollView();

                if(EditorGUI.EndChangeCheck())
                {
                    _SelectedAssetItem.serializedAssetData.UpdateData();

                    DrawExplorerArea();

                    Repaint();
                }
            }

            EditorGUILayout.EndVertical();
        }

        void DrawSearchBar()
        {
            EditorGUILayout.BeginHorizontal();

            EditorGUI.BeginChangeCheck();

            _SearchQuery = EditorGUILayout.TextField(_SearchQuery, GUILayout.Width(Mathf.Clamp(500, 0, _ExplorerAreaWidth)));

            if(EditorGUI.EndChangeCheck())
                RefreshForQuery();

            GUILayout.FlexibleSpace();

            EditorGUILayout.EndHorizontal();
        }
        
        void DrawAssetItems(float itemSize)
        {
            if(_DisplayAssetItems != null && _DisplayAssetItems.Length > 0)
            {
                // TEMP SOLUTION FOR REMOVING UNEXPECTED DELETION OF ASSET
                // _AssetItems = _AssetItems.Where(data => data.assetObject).ToArray();

                if(itemSize > 40)
                {
                    for(int i = 0; i < _DisplayAssetItems.Length; i++)
                    {
                        int items = Mathf.Clamp(Mathf.FloorToInt(_ExplorerAreaWidth / (itemSize + 19)), 0, _DisplayAssetItems.Length);

                        if(items + i > _DisplayAssetItems.Length)
                            items -= (items + i) - _DisplayAssetItems.Length;

                        EditorGUILayout.BeginHorizontal();

                        for(int n = 0; n < items; n++)
                        {
                            _DisplayAssetItems[n + i].Draw(itemSize);

                            CheckAssetItemEvent(_DisplayAssetItems[n + i]);
                        }

                        GUILayout.FlexibleSpace();

                        EditorGUILayout.EndHorizontal();

                        i += items - 1;
                    }
                }

                else
                {
                    foreach(AssetItem assetItem in _DisplayAssetItems)
                    {
                        assetItem.Draw(itemSize);

                        CheckAssetItemEvent(assetItem);
                    }
                }
            }
        }

        void CheckAssetItemEvent(AssetItem assetItem)
        {
            Event currentEvent = Event.current; 
            
            if(assetItem.Contains(currentEvent.mousePosition))
            {
                if(currentEvent.type == EventType.MouseDown && currentEvent.button == 0)
                {
                    SelectAssetItem(assetItem);

                    Repaint();
                }

                else if(currentEvent.type == EventType.ContextClick)
                    DrawContextMenu(assetItem);
            }
        }

        void RefreshDatabase()
        {
            _AssetItems = GetAssets().ToArray();

            RefreshForQuery();
        }

        void RefreshForQuery()
        {
            if(_SearchQuery != string.Empty)
                _DisplayAssetItems = _AssetItems.Where(assetItem => assetItem.name.ToLower().Contains(_SearchQuery.ToLower())).ToArray();

            else
                _DisplayAssetItems = _AssetItems;
        }

        void DrawContextMenu(AssetItem assetItem)
        {
            GenericMenu menu = new GenericMenu();

            menu.AddItem(new GUIContent("Delete"), false, DeleteAssetItem, assetItem);
            menu.AddItem(new GUIContent("Rename"), false, RenameAssetItem, assetItem);
            menu.ShowAsContext();
        }

        void DeleteAssetItem(object assetItemObject)
        {
            AssetItem assetItem = assetItemObject as AssetItem;

            _AssetItems = _AssetItems.Where(a => a != assetItem).ToArray();

            AssetDatabase.DeleteAsset(assetItem.path);
        }

        void RenameAssetItem(object assetItemObject)
        {
            AssetItem assetItem = assetItemObject as AssetItem;
        }

        void SelectAssetItem(AssetItem assetItem)
        {
            if(_SelectedAssetItem != null)
                _SelectedAssetItem.isSelected = false;

            _SelectedAssetItem = assetItem;

            _SelectedAssetItem.isSelected = true;
        }
    }
}