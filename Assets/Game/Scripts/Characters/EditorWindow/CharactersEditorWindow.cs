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
        private AssetItem[] _AssetItems;
        private AssetItem[] _DisplayAssetItems;

        private Vector2 _ExplorerScrollPosition;
        private Vector2 _ContentScrollPosition;

        private float _ItemScale;

        private float _ExplorerAreaWidth;
        private float _ContentAreaWidth;

        private string _SearchQuery;
        private int _Category;

        private readonly float _Padding = 19;

        private bool _IsMovingSeperator;

        private bool _IsEnabled;

        [MenuItem("Window/Characters Window")]
        private static CharactersEditorWindow OpenWindow()
        {
            CharactersEditorWindow window = CharactersEditorWindow.GetWindow<CharactersEditorWindow>("Characters Window");

            window.RefreshDatabase();

            return window;
        }

        [OnOpenAsset]
        private static bool OpenWindow(int instanceID, int line)
        {
            UnityEngine.Object assetObject = EditorUtility.InstanceIDToObject(instanceID);

            if (assetObject is IIcon)
            {
                CharactersEditorWindow window = OpenWindow();

                window.RefreshDatabase();

                AssetItem assetItem = window._AssetItems.First(assetItem => assetItem.assetObject == assetObject);

                if (assetItem != null)
                {
                    Select.AddSelection(assetItem);

                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private void OnEnable()
        {
            RefreshDatabase();

            _IsEnabled = true;

            Select.onAddSelection += OnAddSelection;
            Select.onRemoveSelection += OnRemoveSelection;
        }

        private void OnDisable()
        {
            _ExplorerScrollPosition = Vector2.zero;
            _ContentScrollPosition = Vector2.zero;

            Select.onAddSelection -= OnAddSelection;
            Select.onRemoveSelection -= OnRemoveSelection;
        }

        private void OnGUI()
        {
            EditorGUILayout.BeginVertical();

            DrawMenuBar();

            DrawContent();

            EditorGUILayout.EndVertical();

            if(_IsEnabled)
                _IsEnabled = false;
        }

        private IEnumerable<AssetItem> GetAllAssetItemsFromDatabase()
        {
            UnityEngine.Object assetObject;

            foreach(string path in AssetDatabase.GetAllAssetPaths())
            {
                assetObject = AssetDatabase.LoadAssetAtPath(path, typeof(UnityEngine.Object));

                if(assetObject is IIcon)
                    yield return new AssetItem(assetObject, path);
            }
        }

        private void OnAddSelection(object[] selections)
        {
            foreach(AssetItem assetItem in selections.Where(o => o is AssetItem).Select(o => o as AssetItem))
                assetItem.isSelected = true;
        }

        private void OnRemoveSelection(object[] selections)
        {
            foreach(AssetItem assetItem in selections.Where(o => o is AssetItem).Select(o => o as AssetItem))
                assetItem.isSelected = false;
        }

        private void DrawMenuBar()
        {
            EditorGUILayout.BeginHorizontal();

            if(GUILayout.Button("Create New Asset"))
            {
                GetWindow<CreateAssetWindow>("Create Asset");
            }

            if(GUILayout.Button("Refresh"))
            {
                RefreshDatabase();
            }

            EditorGUILayout.EndHorizontal();
        }
        private void DrawContent()
        {
            EditorGUILayout.BeginHorizontal();

            DrawExplorerArea();

            DrawSeperator();

            DrawAssetContent();

            EditorGUILayout.EndHorizontal();
        }

        private void DrawSeperator()
        {
            float width = position.width;
            float percentage;

            Rect rect = EditorGUILayout.BeginVertical(GUILayout.Width(10));

            GUILayout.FlexibleSpace();

            Handles.color = Color.grey;
            Handles.DrawLine(rect.center + (Vector2.up * (rect.height / 2)), rect.center + (Vector2.down * (rect.height / 2)));

            GUILayout.EndVertical();
            EditorGUIUtility.AddCursorRect(GUILayoutUtility.GetLastRect(), MouseCursor.ResizeHorizontal);

            // Event 
            if(rect.Contains(EditorInput.mousePosition) && EditorInput.GetMouseButtonDown(0))
                _IsMovingSeperator = true;

            else if(EditorInput.GetMouseButtonUp(0))
                _IsMovingSeperator = false;

            if(_IsMovingSeperator && EditorInput.GetMouseButtonDrag(0))
            {
                percentage = Mathf.Clamp(Mathf.InverseLerp(0, width, EditorInput.mousePosition.x), 0.2f, 0.8f);

                width -= _Padding;

                _ExplorerAreaWidth = width * percentage;
                _ContentAreaWidth = width * (1 - percentage);

                Repaint();
            }

            if(_IsEnabled)
            {
                percentage = 0.8f;

                width -= _Padding;

                _ExplorerAreaWidth = width * percentage;
                _ContentAreaWidth = width * (1 - percentage);

                Repaint();
            }
    }

        private void DrawExplorerArea()
        {
            float itemSize = Mathf.Lerp(40, 100, _ItemScale);

            EditorGUILayout.BeginVertical(GUILayout.Width(_ExplorerAreaWidth));

            EditorGUILayout.BeginHorizontal();

            DrawSearchBar();

            GUILayout.FlexibleSpace();

            DrawCategoryField();

            EditorGUILayout.EndHorizontal();

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

        private void DrawAssetContent()
        {
            // AssetItem assetItem = Select.selection.FirstOrDefault(o => o is AssetItem) as AssetItem;

            // EditorGUILayout.BeginVertical("box", GUILayout.Width(_ContentAreaWidth));

            // if(assetItem != null && assetItem.assetObject)
            // {
            //     EditorGUI.BeginChangeCheck();

            //     SerializedProperty property = assetItem.serializedAssetData.serializedObject.GetIterator();

            //     property.NextVisible(true);

            //     _ContentScrollPosition = EditorGUILayout.BeginScrollView(_ContentScrollPosition);// BeginVertical();

            //     EditorGUILayout.LabelField(assetItem.name);

            //     while(property.NextVisible(false))
            //         EditorGUILayout.PropertyField(property);

            //     EditorGUILayout.EndScrollView();

            //     if(EditorGUI.EndChangeCheck())
            //     {
            //         assetItem.serializedAssetData.UpdateData();

            //         DrawExplorerArea();

            //         Repaint();
            //     }
            // }

            // EditorGUILayout.EndVertical();

            IEnumerable<AssetItem> assetItemsSelected = Select.selection.Where(o => o is AssetItem).Select(o => o as AssetItem);

            if(assetItemsSelected.Any())
            {
                AssetItem assetItem = assetItemsSelected.First();

                UnityEngine.Object[] assetObjects = Select.selection.Where(o => o is AssetItem).Select(o => (o as AssetItem).assetObject).ToArray();

                SerializedObject serializedObject = new SerializedObject(assetObjects);

                EditorGUILayout.BeginVertical("box", GUILayout.Width(_ContentAreaWidth));

                if(assetObjects.Length > 0 && assetObjects.IsSameType())
                {
                    EditorGUI.BeginChangeCheck();

                    SerializedProperty property = serializedObject.GetIterator();

                    property.NextVisible(true);

                    _ContentScrollPosition = EditorGUILayout.BeginScrollView(_ContentScrollPosition);// BeginVertical();

                    //EditorGUILayout.LabelField(assetItem.name);

                    while(property.NextVisible(false))
                        EditorGUILayout.PropertyField(property);

                    EditorGUILayout.EndScrollView();

                    if(EditorGUI.EndChangeCheck())
                    {
                        //assetItem.serializedAssetData.UpdateData();

                        DrawExplorerArea();

                        Repaint();
                    }
                }

                EditorGUILayout.EndVertical();
            }
        }

        private void DrawSearchBar()
        {
            EditorGUI.BeginChangeCheck();

            _SearchQuery = EditorGUILayout.TextField(_SearchQuery);

            if(EditorGUI.EndChangeCheck())
                RefreshForQuery();
        }

        private void DrawCategoryField()
        {
            EditorGUI.BeginChangeCheck();

            _Category = EditorGUILayout.MaskField(_Category, CharactersUtilities.allCreatableAssetTypes.Select(t => t.Name).ToArray());

            if(EditorGUI.EndChangeCheck())
                RefreshForQuery();
        }

        private void DrawAssetItems(float itemSize)
        {
            if(_DisplayAssetItems?.Length > 0)
            {
                if(itemSize > 40)
                {
                    for(int i = 0; i < _DisplayAssetItems.Length; i++)
                    {
                        int items = Mathf.Clamp(Mathf.FloorToInt(_ExplorerAreaWidth / (itemSize + _Padding)), 0, _DisplayAssetItems.Length);

                        if(items + i > _DisplayAssetItems.Length)
                            items -= (items + i) - _DisplayAssetItems.Length;

                        EditorGUILayout.BeginHorizontal();

                        for(int n = 0; n < items; n++)
                            _DisplayAssetItems[n + i].Draw(itemSize);

                        GUILayout.FlexibleSpace();

                        EditorGUILayout.EndHorizontal();

                        i += items - 1;
                    }
                }

                else
                {
                    foreach(AssetItem assetItem in _DisplayAssetItems)
                        assetItem.Draw(itemSize);
                }

                CheckAssetItemsEvent();
            }
        }

        private void CheckAssetItemsEvent()
        {
            AssetItem mousePositionInsideAssetItem = Array.Find(_DisplayAssetItems, assetItem => assetItem.Contains(EditorInput.mousePosition));

            if (EditorInput.GetMouseButtonDown(0))
            {
                if (!EditorInput.isShiftPressed)
                    Select.RemoveSelection(Select.selection.Where(o => o is AssetItem).ToArray());

                if (mousePositionInsideAssetItem != null)
                    Select.AddSelection(mousePositionInsideAssetItem);

                Repaint();
            }
            else if (EditorInput.GetMouseButtonDown(1))
            {
                DrawContextMenu(mousePositionInsideAssetItem);
            }
        }

        private void RefreshDatabase()
        {
            Select.RemoveAllSelection();

            _AssetItems = GetAllAssetItemsFromDatabase().ToArray();

            RefreshForQuery();
        }

        private void RefreshForQuery()
        {
            // Search Query
            _DisplayAssetItems = !string.IsNullOrEmpty(_SearchQuery) ? _AssetItems.Where(assetItem => assetItem.name.ToLower().Contains(_SearchQuery.ToLower())).ToArray() : _AssetItems;

            // Category Query
            IEnumerable<int> categoryIndexesSelected = CharactersUtilities.GetIndexesFromByte(_Category, CharactersUtilities.allCreatableAssetTypes.Length);

            if (categoryIndexesSelected.Any())
            {
                for (int i = 0; i < CharactersUtilities.allCreatableAssetTypes.Length; i++)
                {
                    if (!categoryIndexesSelected.Any(index => index == i))
                    {
                        _DisplayAssetItems = _DisplayAssetItems.Where(assetItem =>
                        {
                            Type type = assetItem.assetObject.GetType();
                            return !(type.IsSubclassOf(CharactersUtilities.allCreatableAssetTypes[i]) || type == CharactersUtilities.allCreatableAssetTypes[i]);
                        }).ToArray();
                    }
                }
            }

            else
            {
                _DisplayAssetItems = null;
            }
        }

        private void DrawContextMenu(AssetItem assetItem)
        {
            GenericMenu menu = new GenericMenu();

            menu.AddItem(new GUIContent("Delete"), false, DeleteAssetItem, assetItem);
            menu.AddItem(new GUIContent("Rename"), false, RenameAssetItem, assetItem);
            menu.ShowAsContext();
        }

        private void DeleteAssetItem(object assetItemObject)
        {
            AssetItem assetItem = assetItemObject as AssetItem;

            Select.RemoveSelection(assetItemObject);

            _AssetItems = _AssetItems.Where(a => a != assetItem).ToArray();
            _DisplayAssetItems = _DisplayAssetItems.Where(a => a != assetItem).ToArray();

            AssetDatabase.DeleteAsset(assetItem.path);
        }

        private void RenameAssetItem(object assetItemObject)
        {
            AssetItem assetItem = assetItemObject as AssetItem;
        }
    }
    // public class CharactersEditorWindow : EditorWindow
    // {
    //     AssetItem[] _AssetItems = null;
    //     AssetItem[] _DisplayAssetItems = null;
 
    //     AssetItem _SelectedAssetItem = null;

    //     Vector2 _ExplorerScrollPosition;
    //     Vector2 _ContentScrollPosition; 

    //     float _ItemScale = 0;

    //     float _ExplorerAreaWidth;
    //     float _ContentAreaWidth;

    //     string _SearchQuery;
    //     int _Category;

    //     float _Padding = 19;

    //     bool _IsMovingSeperator;

    //     bool _IsEnabled = false;

    //     [MenuItem("Window/Characters Window")]
    //     static CharactersEditorWindow OpenWindow()
    //     {
    //         CharactersEditorWindow window = CharactersEditorWindow.GetWindow<CharactersEditorWindow>("Characters Window");

    //         window.RefreshDatabase();

    //         return window;
    //     }

    //     [OnOpenAsset]
    //     static bool OpenWindow(int instanceID, int line)
    //     {
    //         UnityEngine.Object assetObject = EditorUtility.InstanceIDToObject(instanceID);

    //         if(assetObject is IIcon)
    //         {
    //             CharactersEditorWindow window = OpenWindow();

    //             window.RefreshDatabase();

    //             AssetItem assetItem = window._AssetItems.First(assetItem => assetItem.assetObject == assetObject);

    //             if(assetItem != null)
    //             {
    //                 window._SelectedAssetItem = assetItem;

    //                 return true;
    //             }

    //             else
    //                 return false;
    //         }

    //         else
    //             return false;
    //     }

    //     void OnEnable() 
    //     {
    //         _SelectedAssetItem = null;
            
    //         RefreshDatabase();

    //         _IsEnabled = true;
    //     }

    //     void OnDisable() 
    //     {
    //         _ExplorerScrollPosition = Vector2.zero;
    //         _ContentScrollPosition = Vector2.zero;
    //     }

    //     void OnGUI() 
    //     {
    //         EditorGUILayout.BeginVertical();

    //         DrawMenuBar();

    //         DrawContent();

    //         EditorGUILayout.EndVertical();

    //         if(_IsEnabled)
    //             _IsEnabled = false;
    //     } 

    //     IEnumerable<AssetItem> GetAllAssetItemsFromDatabase()
    //     {
    //         UnityEngine.Object assetObject;

    //         foreach(string path in AssetDatabase.GetAllAssetPaths())
    //         {
    //             assetObject = AssetDatabase.LoadAssetAtPath(path, typeof(UnityEngine.Object));

    //             if(assetObject is IIcon)
    //                 yield return new AssetItem(assetObject, path);
    //         }
    //     }
        
    //     void DrawMenuBar()
    //     {
    //         EditorGUILayout.BeginHorizontal();

    //         if(GUILayout.Button("Create New Asset"))
    //         {
    //             CreateAssetWindow.GetWindow<CreateAssetWindow>("Create Asset");
    //         }

    //         if(GUILayout.Button("Refresh"))
    //         {
    //             RefreshDatabase();
    //         }

    //         EditorGUILayout.EndHorizontal();
    //     }
    //     void DrawContent()
    //     {
    //         EditorGUILayout.BeginHorizontal();

    //         DrawExplorerArea();

    //         DrawSeperator();

    //         DrawAssetContent();

    //         EditorGUILayout.EndHorizontal();
    //     }

    //     void DrawSeperator()
    //     {
    //         float width = position.width;

    //         Rect rect = EditorGUILayout.BeginVertical(GUILayout.Width(10));

    //         GUILayout.FlexibleSpace();

    //         Handles.color = Color.grey;
    //         Handles.DrawLine(rect.center + Vector2.up * (rect.height / 2), rect.center + Vector2.down * (rect.height / 2));

    //         GUILayout.EndVertical();
    //         EditorGUIUtility.AddCursorRect(GUILayoutUtility.GetLastRect(), MouseCursor.ResizeHorizontal);

    //         // Event 
    //         if(rect.Contains(EditorInput.mousePosition) && EditorInput.MouseDown(0))
    //             _IsMovingSeperator = true;

    //         else if(EditorInput.MouseUp(0))
    //             _IsMovingSeperator = false;

    //         if(_IsMovingSeperator && EditorInput.MouseDrag(0))
    //         {
    //             float percentage = Mathf.Clamp(Mathf.InverseLerp(0, width, EditorInput.mousePosition.x), 0.2f, 0.8f);

    //             width -= _Padding;
                
    //             _ExplorerAreaWidth = width * percentage;
    //             _ContentAreaWidth = width * (1 - percentage);

    //             Repaint();
    //         }

    //         if(_IsEnabled)
    //         {
    //             float percentage = 0.8f;

    //             width -= _Padding;
                
    //             _ExplorerAreaWidth = width * percentage;
    //             _ContentAreaWidth = width * (1 - percentage);

    //             Repaint();
    //         }
    // }

    //     void DrawExplorerArea()
    //     {
    //         float itemSize = Mathf.Lerp(40, 100, _ItemScale);

    //         EditorGUILayout.BeginVertical(GUILayout.Width(_ExplorerAreaWidth));

    //         EditorGUILayout.BeginHorizontal();
            
    //         DrawSearchBar();

    //         GUILayout.FlexibleSpace();
            
    //         DrawCategoryField();

    //         EditorGUILayout.EndHorizontal();

    //         _ExplorerScrollPosition = EditorGUILayout.BeginScrollView(_ExplorerScrollPosition);
    
    //         DrawAssetItems(itemSize);

    //         GUILayout.FlexibleSpace();

    //         EditorGUILayout.EndScrollView();

    //         EditorGUILayout.BeginHorizontal(GUILayout.Height(20));

    //         GUILayout.FlexibleSpace();

    //         _ItemScale = GUILayout.HorizontalSlider(_ItemScale, 0, 1, GUILayout.Width(100)); 

    //         EditorGUILayout.EndHorizontal();

    //         EditorGUILayout.EndVertical();
    //     }

    //     void DrawAssetContent()
    //     {
    //         EditorGUILayout.BeginVertical("box", GUILayout.Width(_ContentAreaWidth));

    //         if(_SelectedAssetItem != null && _SelectedAssetItem.assetObject)
    //         {
    //             EditorGUI.BeginChangeCheck();

    //             SerializedProperty property = _SelectedAssetItem.serializedAssetData.serializedObject.GetIterator();

    //             property.NextVisible(true);

    //             _ContentScrollPosition = EditorGUILayout.BeginScrollView(_ContentScrollPosition);// BeginVertical();
                
    //             EditorGUILayout.LabelField(_SelectedAssetItem.name);

    //             while(property.NextVisible(false))
    //                 EditorGUILayout.PropertyField(property);

    //             EditorGUILayout.EndScrollView();

    //             if(EditorGUI.EndChangeCheck())
    //             {
    //                 _SelectedAssetItem.serializedAssetData.UpdateData();

    //                 DrawExplorerArea();

    //                 Repaint();
    //             }
    //         }

    //         EditorGUILayout.EndVertical();
    //     }

    //     void DrawSearchBar()
    //     {
    //         EditorGUI.BeginChangeCheck();

    //         _SearchQuery = EditorGUILayout.TextField(_SearchQuery);

    //         if(EditorGUI.EndChangeCheck())
    //             RefreshForQuery();
    //     }

    //     void DrawCategoryField()
    //     {
    //         EditorGUI.BeginChangeCheck();

    //         _Category = EditorGUILayout.MaskField(_Category, CharactersUtilities.allCreatableAssetTypes.Select(t => t.Name).ToArray());

    //         if(EditorGUI.EndChangeCheck())
    //             RefreshForQuery();
    //     }
        
    //     void DrawAssetItems(float itemSize)
    //     {
    //         if(_DisplayAssetItems != null && _DisplayAssetItems.Length > 0)
    //         {
    //             if(itemSize > 40)
    //             {
    //                 for(int i = 0; i < _DisplayAssetItems.Length; i++)
    //                 {
    //                     int items = Mathf.Clamp(Mathf.FloorToInt(_ExplorerAreaWidth / (itemSize + _Padding)), 0, _DisplayAssetItems.Length);

    //                     if(items + i > _DisplayAssetItems.Length)
    //                         items -= (items + i) - _DisplayAssetItems.Length;

    //                     EditorGUILayout.BeginHorizontal();

    //                     for(int n = 0; n < items; n++)
    //                     {
    //                         _DisplayAssetItems[n + i].Draw(itemSize);

    //                         CheckAssetItemEvent(_DisplayAssetItems[n + i]);
    //                     }

    //                     GUILayout.FlexibleSpace();

    //                     EditorGUILayout.EndHorizontal();

    //                     i += items - 1;
    //                 }
    //             }

    //             else
    //             {
    //                 foreach(AssetItem assetItem in _DisplayAssetItems)
    //                 {
    //                     assetItem.Draw(itemSize);

    //                     CheckAssetItemEvent(assetItem);
    //                 }
    //             }
    //         }
    //     }

    //     void CheckAssetItemEvent(AssetItem assetItem)
    //     {
    //         Event currentEvent = Event.current; 
            
    //         if(assetItem.Contains(currentEvent.mousePosition))
    //         {
    //             if(currentEvent.type == EventType.MouseDown && currentEvent.button == 0)
    //             {
    //                 SelectAssetItem(assetItem);

    //                 Repaint();
    //             }

    //             else if(currentEvent.type == EventType.ContextClick)
    //                 DrawContextMenu(assetItem);
    //         }
    //     }

    //     void RefreshDatabase()
    //     {
    //         _AssetItems = GetAllAssetItemsFromDatabase().ToArray();

    //         // foreach(UnityEngine.Object assetObject in AssetUtilities.GetAllAssetsInPath("Assets/Game/Data/Characters/"))
    //         // foreach(AssetItem assetItem in _AssetItems)
    //         // {
    //         //     Debug.Log(assetItem.assetObject);
    //         // }

    //         _SelectedAssetItem = null;

    //         RefreshForQuery();
    //     }

    //     void RefreshForQuery()
    //     {
    //         // Search Query
    //         _DisplayAssetItems = !string.IsNullOrEmpty(_SearchQuery) ? _AssetItems.Where(assetItem => assetItem.name.ToLower().Contains(_SearchQuery.ToLower())).ToArray() : _AssetItems;

    //         // Category Query
    //         IEnumerable<int> categoryIndexesSelected = CharactersUtilities.GetIndexesFromByte(_Category, CharactersUtilities.allCreatableAssetTypes.Length);

    //         if(categoryIndexesSelected.Any())
    //         {
    //             for(int i = 0; i < CharactersUtilities.allCreatableAssetTypes.Length; i++)
    //             {
    //                 if(!categoryIndexesSelected.Where(index => index == i).Any())
    //                 {
    //                     _DisplayAssetItems = _DisplayAssetItems.Where(assetItem => 
    //                     {
    //                         Type type = assetItem.assetObject.GetType();
    //                         return !(type.IsSubclassOf(CharactersUtilities.allCreatableAssetTypes[i]) || type == CharactersUtilities.allCreatableAssetTypes[i]);
    //                     }).ToArray();
    //                 }
    //             }
    //         }

    //         else
    //             _DisplayAssetItems = null;
    //     }

    //     void DrawContextMenu(AssetItem assetItem)
    //     {
    //         GenericMenu menu = new GenericMenu();

    //         menu.AddItem(new GUIContent("Delete"), false, DeleteAssetItem, assetItem);
    //         menu.AddItem(new GUIContent("Rename"), false, RenameAssetItem, assetItem);
    //         menu.ShowAsContext();
    //     }

    //     void DeleteAssetItem(object assetItemObject)
    //     {
    //         AssetItem assetItem = assetItemObject as AssetItem;

    //         if(assetItem == _SelectedAssetItem)
    //             _SelectedAssetItem = null;

    //         _AssetItems = _AssetItems.Where(a => a != assetItem).ToArray();
    //         _DisplayAssetItems = _DisplayAssetItems.Where(a => a != assetItem).ToArray();

    //         AssetDatabase.DeleteAsset(assetItem.path);
    //     }

    //     void RenameAssetItem(object assetItemObject)
    //     {
    //         AssetItem assetItem = assetItemObject as AssetItem;
    //     }

    //     void SelectAssetItem(AssetItem assetItem)
    //     {
    //         if(_SelectedAssetItem != null)
    //             _SelectedAssetItem.isSelected = false;

    //         _SelectedAssetItem = assetItem;

    //         _SelectedAssetItem.isSelected = true;
    //     }
    // }
}