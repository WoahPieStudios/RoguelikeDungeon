using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using UnityEditor;

namespace Game.CharactersEditor
{
    public class AssetItem : SerializedAssetData, IRename, IDuplicate, IDrawList, IDrawTile
    {
        Rect _Rect;
        string _Path;

        bool _IsRenaming = false;
        string _NewName;

        string[] _Categories;

        public string path => _Path;

        public bool isSelected { get; set; } = false;
        public bool isRenaming => _IsRenaming;

        public AssetItem(Object assetObject, string path) : base(assetObject)
        {
            _Path = path;
        }

        GUIContent LimitLabel(string text)
        {
            GUIContent content = new GUIContent(text);

            // Vector2 size = GUI.skin.label.CalcSize(content);

            return content;
        }

        GUIStyle GetBox()
        {
            GUIStyle box = new GUIStyle("box");

            if(isSelected)
                box.normal.background = Texture2D.grayTexture;

            return box;
        }

        GUIStyle GetTextStyle()
        {
            GUIStyle textStyle = new GUIStyle();

            textStyle.normal.textColor = Color.white;
            textStyle.clipping = TextClipping.Clip;

            return textStyle;
        }

        void DrawName(GUIStyle textStyle, params GUILayoutOption[] options)
        {
            if(isRenaming)
                _NewName = EditorGUILayout.TextField(_NewName);

            else
                GUILayout.Label(LimitLabel(name), textStyle, options);
        }

        public void DrawTile(float tileSize)
        {
            GUIStyle box = GetBox();
            GUIStyle textStyle = GetTextStyle();

            textStyle.alignment = TextAnchor.MiddleCenter;

            EditorGUILayout.BeginVertical(box, GUILayout.Width(tileSize), GUILayout.Height(tileSize));

            GUILayout.Box(icon, GUILayout.Width(tileSize), GUILayout.Height(tileSize));

            DrawName(textStyle, GUILayout.Width(tileSize));

            EditorGUILayout.EndVertical();

            _Rect = GUILayoutUtility.GetLastRect();
        }

        public void DrawList()
        {
            GUIStyle box = GetBox();
            GUIStyle textStyle = GetTextStyle();

            textStyle.alignment = TextAnchor.MiddleLeft;

            EditorGUILayout.BeginHorizontal(box);

            GUILayout.Box(icon, GUILayout.Width(20), GUILayout.Height(20));

            DrawName(textStyle);

            EditorGUILayout.EndHorizontal();

            _Rect = GUILayoutUtility.GetLastRect();
        }

        public bool Contains(Vector2 point)
        {
            return _Rect.Contains(point);
        }

        public void RenameStart()
        {
            _IsRenaming = true;
            
            _NewName = name;
        }

        public void RenameCancel()
        {
            _IsRenaming = false;
        }

        public void RenameAccept()
        {
            _IsRenaming = false;

            AssetDatabase.RenameAsset(path, _NewName);
        }

        public void Duplicate()
        {
            Object newAssetObject = Object.Instantiate(assetObject);

            string fileName = $"/{name}.asset";
            string newFileName = $"/{name} - copy.asset";

            AssetDatabase.CreateAsset(newAssetObject, path.Remove(path.Length - fileName.Length - 1, fileName.Length) + newFileName);
        }

        public void UpdatePath(string newPath)
        {
            if(AssetDatabase.IsValidFolder(path))
                _Path = newPath;
        }
    }
}