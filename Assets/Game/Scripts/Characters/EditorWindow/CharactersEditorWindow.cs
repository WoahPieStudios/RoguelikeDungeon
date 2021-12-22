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

        Vector2 _ExplorerScrollPosition;

        float _SeperatorPercent;
        bool _IsMovingSeperator;

        float _ExplorerAreaWidth;
        float _ContentAreaWidth;

        float _Padding = 19;

        Inspector _Inspector = new Inspector();

        Explorer _Explorer = new Explorer();

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
            

            _Inspector.onGUIChange += OnInspectorGUIChange;

            _Explorer.onAfterDraw += CheckAssetItemsEvent;

            _SeperatorPercent = 0.8f;

            Select.onAddSelection += OnAddSelection;
            Select.onRemoveSelection += OnRemoveSelection;

            RefreshDatabase();
        }

        void OnDisable()
        {
            _ExplorerScrollPosition = Vector2.zero;

            _Inspector.onGUIChange -= OnInspectorGUIChange;

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

        void OnInspectorGUIChange()
        {
            _Explorer.Draw(_ExplorerAreaWidth);

            Repaint();
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

            _Explorer.Draw(_ExplorerAreaWidth);

            DrawSeperator();

            _Inspector.Draw(_ContentAreaWidth);

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

        void CheckAssetItemsEvent()
        {
            if(_Explorer.displayedAssetItems == null || _Explorer.displayedAssetItems.Length <= 0)
                return;

            AssetItem mousePositionInsideAssetItem = Array.Find(_Explorer.displayedAssetItems, assetItem => assetItem.Contains(EditorInput.mousePosition));

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
            AssetItem mousePositionInsideAssetItem = Array.Find(_Explorer.displayedAssetItems, assetItem => assetItem.Contains(EditorInput.mousePosition));

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
            AssetItem renameAssetItem = Array.Find(_Explorer.displayedAssetItems, assetItem => assetItem.isRenaming);

            if((EditorInput.GetKeyDown(KeyCode.Return) || EditorInput.GetKeyDown(KeyCode.KeypadEnter)) && renameAssetItem != null)
            {
                renameAssetItem.RenameAccept();

                RefreshDatabase();
            }
        }

        void HandleDragAndDropEvent()
        {
            AssetItem[] assetItems = Select.selection.Where(o => o is AssetItem).Cast<AssetItem>().ToArray();
            AssetItem mousePositionInsideAssetItem = Array.Find(_Explorer.displayedAssetItems, assetItem => assetItem.Contains(EditorInput.mousePosition));

            if(mousePositionInsideAssetItem == null || EditorInput.deltaMousePostion.magnitude < 2 || assetItems.Length <= 0 || !mousePositionInsideAssetItem.isSelected)
                return;

            DragAndDrop.PrepareStartDrag();
            DragAndDrop.objectReferences = assetItems.Select(a => a.assetObject).ToArray();
            DragAndDrop.StartDrag(null);
        }

        void RefreshDatabase()
        {
            _AssetItems = GetAllAssetItemsFromDatabase().OrderBy(assetItem => assetItem.name).ToArray();

            _Explorer.assetItems = GetAllAssetItemsFromDatabase().OrderBy(assetItem => assetItem.name).ToArray();

            // Select.RemoveSelection(Select.selection.Where(o => o is AssetItem));
            Select.RemoveAllSelection();

            _Explorer.Refresh();
        }

        #region Context Menu
        void DeleteAssetItem(object assetItemObjects)
        {
            if(!(assetItemObjects is AssetItem[]))
                return;

            AssetItem[] assetItems = assetItemObjects as AssetItem[];

            Select.RemoveSelection(assetItems);

            if(_Inspector.lockContent)
                RemoveAssetsFromSerializedContent(assetItems);

            foreach(AssetItem assetItem in assetItems)
                AssetDatabase.DeleteAsset(assetItem.path);

            RefreshDatabase();
        }

        void RemoveAssetsFromSerializedContent(AssetItem[] assetItems)
        {
            IEnumerable<UnityEngine.Object> assetItemObjects = assetItems.Select(assetItem => assetItem.assetObject);
            
            UnityEngine.Object[] serializedObjects = _Inspector.serializedObjectInspected.targetObjects.Where(o => !assetItemObjects.Contains(o)).ToArray();

            if(serializedObjects.Length <= 0)
                _Inspector.lockContent = false;

            _Inspector.serializedObjectInspected = new SerializedObject(serializedObjects);
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