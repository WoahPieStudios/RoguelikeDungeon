using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using UnityEditor;

namespace Game.CharactersEditor
{
    public class Explorer
    {
        float _Padding = 19;
        float _ItemScale;

        Vector2 _ScrollPosition;
        
        string _SearchQuery;
        string[] _CategoryNames;
        int _Category;

        public AssetItem[] _DisplayedAssetItems;

        public AssetItem[] assetItems { get; set; }
        public AssetItem[] displayedAssetItems => _DisplayedAssetItems;

        public event Action onAfterDraw;

        void DrawAssetItems(float itemSize, float explorerWidth)
        {
            if(displayedAssetItems == null || displayedAssetItems.Length <= 0)
                return;

            if(itemSize > 40)
                DrawAssetItemTileLayout(itemSize, explorerWidth);

            else
                DrawAssetItemListLayout();
        }

        void DrawAssetItemTileLayout(float itemSize, float explorerWidth)
        {
            int itemsPerRow = Mathf.FloorToInt(explorerWidth / (itemSize + _Padding));
            float addItemSize = (explorerWidth - (itemsPerRow * (itemSize + _Padding))) / itemsPerRow; // To Fill the extra spaces
            
            for(int i = 0; i < displayedAssetItems.Length; i++)
            {
                int items = Mathf.Clamp(itemsPerRow, 0, displayedAssetItems.Length);

                if(items + i > displayedAssetItems.Length)
                    items -= (items + i) - displayedAssetItems.Length;

                EditorGUILayout.BeginHorizontal();

                for(int n = 0; n < items; n++)
                    displayedAssetItems[n + i].DrawTile(addItemSize + itemSize);

                GUILayout.FlexibleSpace();

                EditorGUILayout.EndHorizontal();

                i += items - 1;
            }
        }

        void DrawAssetItemListLayout()
        {
            foreach(AssetItem assetItem in displayedAssetItems)
                assetItem.DrawList();
        }

        void DrawSearchBar()
        {
            EditorGUI.BeginChangeCheck();

            _SearchQuery = EditorGUILayout.TextField(_SearchQuery);

            if(EditorGUI.EndChangeCheck())
                Refresh();
        }

        void DrawCategoryField()
        {
            EditorGUI.BeginChangeCheck();

            _Category = EditorGUILayout.MaskField(_Category, _CategoryNames);
            // EditorGUILayout.MaskField(0, _CategoryNames);
            if(EditorGUI.EndChangeCheck())
                Refresh();
        }

        public void Draw(float explorerWidth)
        {
            float itemSize = Mathf.Lerp(40, 100, _ItemScale);

            EditorGUILayout.BeginVertical(GUILayout.Width(explorerWidth));

            EditorGUILayout.BeginHorizontal();

            DrawSearchBar();

            GUILayout.FlexibleSpace();

            DrawCategoryField();

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal(GUILayout.Height(20));

            GUILayout.FlexibleSpace();

            _ItemScale = GUILayout.HorizontalSlider(_ItemScale, 0, 1, GUILayout.Width(100));

            EditorGUILayout.EndHorizontal();

            _ScrollPosition = EditorGUILayout.BeginScrollView(_ScrollPosition);

            DrawAssetItems(itemSize, explorerWidth);

            onAfterDraw?.Invoke();

            GUILayout.FlexibleSpace();

            EditorGUILayout.EndScrollView();

            EditorGUILayout.EndVertical();
        }

        public void Refresh()
        {
            _CategoryNames = CharactersUtilities.categoryNames;

            // Search Query
            _DisplayedAssetItems = !string.IsNullOrEmpty(_SearchQuery) ? assetItems.Where(assetItem => assetItem.name.ToLower().Contains(_SearchQuery.ToLower())).ToArray() : assetItems;

            // Category Query
            IEnumerable<AssetItem> tempDisplayAssets = displayedAssetItems;
            IEnumerable<int> categoryIndexesSelected = CharactersUtilities.GetIndexesFromByte(_Category, _CategoryNames.Length);

            if (tempDisplayAssets.IsNotNullAndEmpty() && categoryIndexesSelected.IsNotNullAndEmpty())
            {
                foreach(int i in categoryIndexesSelected)
                    tempDisplayAssets = tempDisplayAssets.Where(a => !CharactersUtilities.ContainsCategoryAttribute(a.assetObject.GetType(), _CategoryNames[i]));
            
                _DisplayedAssetItems = displayedAssetItems.Except(tempDisplayAssets).ToArray();
            }

            else
                _DisplayedAssetItems = null;
        }
    }
}