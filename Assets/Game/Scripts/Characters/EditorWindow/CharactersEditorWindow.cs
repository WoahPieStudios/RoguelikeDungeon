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
        AssetItem[] _AssetItems;
        AssetItem[] _DisplayAssetItems;

        SerializedObject _DisplaySerializedObject;
        bool _LockContent;

        Vector2 _ExplorerScrollPosition;
        Vector2 _ContentScrollPosition;

        float _ItemScale;

        float _SeperatorPercent;
        bool _IsMovingSeperator;

        float _ExplorerAreaWidth;
        float _ContentAreaWidth;

        string _SearchQuery;
        string[] _CategoryNames;
        int _Category;

        float _Padding = 19;

        [MenuItem("Window/Characters Window")]
        static CharactersEditorWindow OpenWindow()
        {
            CharactersEditorWindow window = GetWindow<CharactersEditorWindow>("Characters Window");

            window.RefreshDatabase();

            return window;
        }

        [OnOpenAsset]
        static bool OpenWindow(int instanceID, int line)
        {
            UnityEngine.Object assetObject = EditorUtility.InstanceIDToObject(instanceID);

            if (!(assetObject is IIcon))
                return false;

            CharactersEditorWindow window = OpenWindow();

            window.RefreshDatabase();

            AssetItem assetItem = window._AssetItems.First(assetItem => assetItem.assetObject == assetObject);

            if (assetItem != null)
            {
                Select.AddSelection(assetItem);

                return true;
            }

            else
                return false;
        }

        void OnEnable()
        {
            RefreshDatabase();

            _DisplaySerializedObject = null;

            _LockContent = false;

            _SeperatorPercent = 0.8f;

            Select.onAddSelection += OnAddSelection;
            Select.onRemoveSelection += OnRemoveSelection;
        }

        void OnDisable()
        {
            _ExplorerScrollPosition = Vector2.zero;
            _ContentScrollPosition = Vector2.zero;

            Select.onAddSelection -= OnAddSelection;
            Select.onRemoveSelection -= OnRemoveSelection;
        }

        void OnGUI()
        {
            EditorGUILayout.BeginVertical();

            DrawMenuBar();

            DrawContent();

            EditorGUILayout.EndVertical();
        }

        void OnAddSelection(object[] selections)
        {
            IEnumerable<AssetItem> assetItems = selections.Where(o => o is AssetItem).Select(o => o as AssetItem);

            foreach(AssetItem assetItem in assetItems)
                assetItem.isSelected = true;

            foreach(AssetItem assetItem in assetItems.Where(o => o.isRenaming))
                assetItem.RenameCancel();
        }

        void OnRemoveSelection(object[] selections)
        {
            IEnumerable<AssetItem> assetItems = selections.Where(o => o is AssetItem).Select(o => o as AssetItem);
            
            foreach(AssetItem assetItem in assetItems)
                assetItem.isSelected = false;

            foreach(AssetItem assetItem in assetItems.Where(o => o.isRenaming))
                assetItem.RenameCancel();
        }

        public IEnumerable<AssetItem> GetAllAssetItemsFromDatabase()
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
                GetWindow<CreateAssetWindow>("Create Asset");
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
            Handles.DrawLine(rect.center + (Vector2.up * (rect.height / 2)), rect.center + (Vector2.down * (rect.height / 2)));

            GUILayout.EndVertical();
            EditorGUIUtility.AddCursorRect(GUILayoutUtility.GetLastRect(), MouseCursor.ResizeHorizontal);

            if(rect.Contains(EditorInput.mousePosition) && EditorInput.GetMouseButtonDown(0))
                _IsMovingSeperator = true;

            else if(EditorInput.GetMouseButtonUp(0))
                _IsMovingSeperator = false;

            if(_IsMovingSeperator && EditorInput.GetMouseButtonDrag(0))
                _SeperatorPercent = Mathf.Clamp(Mathf.InverseLerp(0, width, EditorInput.mousePosition.x), 0.2f, 0.8f);

            width -= _Padding;

            _ExplorerAreaWidth = width * _SeperatorPercent;
            _ContentAreaWidth = width * (1 - _SeperatorPercent);

            Repaint();
        }

        #region Explorer Area
        void DrawExplorerArea()
        {
            float itemSize = Mathf.Lerp(40, 100, _ItemScale);

            EditorGUILayout.BeginVertical(GUILayout.Width(_ExplorerAreaWidth));

            EditorGUILayout.BeginHorizontal();

            DrawSearchBar();

            GUILayout.FlexibleSpace();

            DrawCategoryField();

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal(GUILayout.Height(20));

            GUILayout.FlexibleSpace();

            _ItemScale = GUILayout.HorizontalSlider(_ItemScale, 0, 1, GUILayout.Width(100));

            EditorGUILayout.EndHorizontal();

            _ExplorerScrollPosition = EditorGUILayout.BeginScrollView(_ExplorerScrollPosition);

            DrawAssetItems(itemSize);

            CheckAssetItemsEvent();

            GUILayout.FlexibleSpace();

            EditorGUILayout.EndScrollView();

            EditorGUILayout.EndVertical();
        }

        void DrawSearchBar()
        {
            EditorGUI.BeginChangeCheck();

            _SearchQuery = EditorGUILayout.TextField(_SearchQuery);

            if(EditorGUI.EndChangeCheck())
                RefreshForQuery();
        }

        void DrawCategoryField()
        {
            EditorGUI.BeginChangeCheck();

            _Category = EditorGUILayout.MaskField(_Category, _CategoryNames);
            // EditorGUILayout.MaskField(0, _CategoryNames);
            if(EditorGUI.EndChangeCheck())
                RefreshForQuery();
        }

        void DrawAssetItems(float itemSize)
        {
            if(_DisplayAssetItems == null || _DisplayAssetItems.Length <= 0)
                return;

            if(itemSize > 40)
                DrawAssetItemTileLayout(itemSize);

            else
                DrawAssetItemListLayout();
        }

        void DrawAssetItemTileLayout(float itemSize)
        {
            int itemsPerRow = Mathf.FloorToInt(_ExplorerAreaWidth / (itemSize + _Padding));
            float addItemSize = (_ExplorerAreaWidth - (itemsPerRow * (itemSize + _Padding))) / itemsPerRow; // To Fill the extra spaces
            
            for(int i = 0; i < _DisplayAssetItems.Length; i++)
            {
                int items = Mathf.Clamp(itemsPerRow, 0, _DisplayAssetItems.Length);

                if(items + i > _DisplayAssetItems.Length)
                    items -= (items + i) - _DisplayAssetItems.Length;

                EditorGUILayout.BeginHorizontal();

                for(int n = 0; n < items; n++)
                    _DisplayAssetItems[n + i].DrawInTileForm(addItemSize + itemSize);

                GUILayout.FlexibleSpace();

                EditorGUILayout.EndHorizontal();

                i += items - 1;
            }
        }

        void DrawAssetItemListLayout()
        {
            foreach(AssetItem assetItem in _DisplayAssetItems)
                assetItem.DrawInListForm();
        }
        #endregion
        
        void DrawAssetContent()
        {
            UnityEngine.Object[] assetObjects = Select.selection.Where(o => o is AssetItem).Select(o => o as AssetItem).Select(o => o.assetObject).ToArray();

            EditorGUILayout.BeginVertical("box", GUILayout.Width(_ContentAreaWidth));

            if(GUILayout.Button(_LockContent ? "Unlock" : "Lock"))
                _LockContent = assetObjects.Length > 0 && !_LockContent;

            if(!_LockContent)
                _DisplaySerializedObject = assetObjects.Length > 0 && assetObjects.IsSameType() ? new SerializedObject(assetObjects) : null;

            // if(assetObjects.Length > 0 && assetObjects.IsSameType())
            //     _DisplaySerializedObject = new SerializedObject(assetObjects);

            
            if(_DisplaySerializedObject == null)
            {
                GUILayout.FlexibleSpace();
                
                EditorGUILayout.EndVertical();

                return;
            }

            EditorGUI.BeginChangeCheck();

            SerializedProperty property = _DisplaySerializedObject.GetIterator();

            property.NextVisible(true);

            _ContentScrollPosition = EditorGUILayout.BeginScrollView(_ContentScrollPosition);

            EditorGUILayout.LabelField(_DisplaySerializedObject.isEditingMultipleObjects ? "-" : _DisplaySerializedObject.targetObject.name, EditorStyles.boldLabel);

            while (property.NextVisible(false))
                EditorGUILayout.PropertyField(property);

            EditorGUILayout.EndScrollView();

            if(EditorGUI.EndChangeCheck())
            {
                _DisplaySerializedObject.ApplyModifiedProperties();

                DrawExplorerArea();

                Repaint();
            }

            EditorGUILayout.EndVertical();
        }

        void CheckAssetItemsEvent()
        {
            if(_DisplayAssetItems == null || _DisplayAssetItems.Length <= 0)
                return;

            AssetItem mousePositionInsideAssetItem = Array.Find(_DisplayAssetItems, assetItem => assetItem.Contains(EditorInput.mousePosition));

            if(EditorInput.GetMouseButtonDrag(0))
                HandleDragAndDropEvent();

            if (EditorInput.GetMouseButtonUp(0))
                HandleLeftClick();
            
            else if (EditorInput.GetMouseButtonDown(1))
                HandleRightClick();

            HandleRenameAssetItem();

            Repaint();
        }

        void HandleLeftClick()
        {
            AssetItem[] assetItems = Select.selection.Where(o => o is AssetItem).Cast<AssetItem>().ToArray();
            AssetItem mousePositionInsideAssetItem = Array.Find(_DisplayAssetItems, assetItem => assetItem.Contains(EditorInput.mousePosition));

            if (!EditorInput.isShiftPressed)
                Select.RemoveSelection(assetItems);

            if (mousePositionInsideAssetItem != null)
            {
                if (Select.selection.Contains(mousePositionInsideAssetItem))
                    Select.RemoveSelection(mousePositionInsideAssetItem);

                else
                    Select.AddSelection(mousePositionInsideAssetItem);
            }

            GUI.FocusControl(null);
        }

        void HandleRightClick()
        {
            AssetItem[] assetItems = Select.selection.Where(o => o is AssetItem).Cast<AssetItem>().ToArray();

            if(assetItems.Length <= 0)
                return;

            GenericMenu menu = new GenericMenu(); 

            if(assetItems?.Length > 0)
                menu.AddItem(new GUIContent("Delete"), false, DeleteAssetItem, assetItems);
            else
                menu.AddDisabledItem(new GUIContent("Delete"));

            if(assetItems?.Length == 1)
                menu.AddItem(new GUIContent("Rename"), false, RenameAssetItem, assetItems.First());
            else
                menu.AddDisabledItem(new GUIContent("Rename"));

            menu.AddItem(new GUIContent("Duplicate"), false, DuplicateAssetItem, assetItems);
                
            menu.ShowAsContext();
        }

        void HandleRenameAssetItem()
        {
            AssetItem renameAssetItem = Array.Find(_DisplayAssetItems, assetItem => assetItem.isRenaming);

            if((EditorInput.GetKeyDown(KeyCode.Return) || EditorInput.GetKeyDown(KeyCode.KeypadEnter)) && renameAssetItem != null)
            {
                renameAssetItem.RenameAccept();

                RefreshDatabase();
            }
        }

        void HandleDragAndDropEvent()
        {
            AssetItem[] assetItems = Select.selection.Where(o => o is AssetItem).Cast<AssetItem>().ToArray();
            AssetItem mousePositionInsideAssetItem = Array.Find(_DisplayAssetItems, assetItem => assetItem.Contains(EditorInput.mousePosition));

            if(mousePositionInsideAssetItem == null || EditorInput.deltaMousePostion.magnitude < 2 || assetItems.Length <= 0 || !mousePositionInsideAssetItem.isSelected)
                return;

            DragAndDrop.PrepareStartDrag();
            DragAndDrop.objectReferences = assetItems.Select(a => a.assetObject).ToArray();
            DragAndDrop.StartDrag(null);
        }

        void RefreshDatabase()
        {
            _AssetItems = GetAllAssetItemsFromDatabase().OrderBy(assetItem => assetItem.name).ToArray();

            _CategoryNames = CharactersUtilities.categoryNames;

            RefreshForQuery();
        }

        void RefreshForQuery()
        {
            // Select.RemoveSelection(Select.selection.Where(o => o is AssetItem));
            Select.RemoveAllSelection();
            // Search Query
            _DisplayAssetItems = !string.IsNullOrEmpty(_SearchQuery) ? _AssetItems.Where(assetItem => assetItem.name.ToLower().Contains(_SearchQuery.ToLower())).ToArray() : _AssetItems;

            // Category Query
            IEnumerable<AssetItem> tempDisplayAssets = _DisplayAssetItems;
            IEnumerable<int> categoryIndexesSelected = CharactersUtilities.GetIndexesFromByte(_Category, _CategoryNames.Length);

            if (categoryIndexesSelected.Any())
            {
                foreach(int i in categoryIndexesSelected)
                    tempDisplayAssets = tempDisplayAssets.Where(a => !CharactersUtilities.ContainsCategoryAttribute(a.assetObject.GetType(), _CategoryNames[i]));
            
                _DisplayAssetItems = _DisplayAssetItems.Except(tempDisplayAssets).ToArray();
            }

            else
                _DisplayAssetItems = null;
        }

        #region Context Menu
        void DeleteAssetItem(object assetItemObjects)
        {
            if(!(assetItemObjects is AssetItem[]))
                return;

            AssetItem[] assetItems = assetItemObjects as AssetItem[];

            Select.RemoveSelection(assetItems);

            if(_LockContent)
                RemoveAssetsFromSerializedContent(assetItems);

            _AssetItems = _AssetItems.Except(assetItems).ToArray();
            _DisplayAssetItems = _DisplayAssetItems.Except(assetItems).ToArray();

            foreach(AssetItem assetItem in assetItems)
                AssetDatabase.DeleteAsset(assetItem.path);
        }

        void RemoveAssetsFromSerializedContent(AssetItem[] assetItems)
        {
            IEnumerable<UnityEngine.Object> assetItemObjects = assetItems.Select(assetItem => assetItem.assetObject);
            
            UnityEngine.Object[] serializedObjects = _DisplaySerializedObject.targetObjects.Where(o => !assetItemObjects.Contains(o)).ToArray();

            if(serializedObjects.Length <= 0)
                _LockContent = false;

            _DisplaySerializedObject = new SerializedObject(serializedObjects);
        }

        void RenameAssetItem(object assetItemObject)
        {
            AssetItem assetItem = assetItemObject as AssetItem;

            assetItem.RenameStart();
        }

        void DuplicateAssetItem(object assetItemObjects)
        {
            if(!(assetItemObjects is AssetItem[]))
                return;
                
            AssetItem[] assetItems = assetItemObjects as AssetItem[];

            Select.RemoveSelection(assetItems);

            foreach(AssetItem assetItem in assetItems)
                assetItem.Duplicate();

            RefreshDatabase();
        }
        #endregion
    }
}