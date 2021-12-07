using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using UnityEditor;

namespace Game.CharactersEditor
{
    public class AssetItem : IRename, IDuplicate
    {
        Rect _Rect;
        SerializedAssetData _SerializedAssetData;
        string _Path;

        bool _IsRenaming = false;
        string _NewName;

        public SerializedAssetData serializedAssetData => _SerializedAssetData;
        public Object assetObject => _SerializedAssetData.assetObject;
        public string name => _SerializedAssetData.name;
        public Texture icon => _SerializedAssetData.icon;
        public string path => _Path;

        public bool isSelected { get; set; } = false;
        public bool isRenaming => _IsRenaming;

        public AssetItem(Object assetObject, string path)
        {
            _SerializedAssetData = new SerializedAssetData(assetObject);

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
                GUILayout.Label(LimitLabel(_SerializedAssetData.name), textStyle, options);
        }

        public void DrawInTileForm(float size)
        {
            GUIStyle box = GetBox();
            GUIStyle textStyle = GetTextStyle();

            textStyle.alignment = TextAnchor.MiddleCenter;

            EditorGUILayout.BeginVertical(box, GUILayout.Width(size), GUILayout.Height(size));

            GUILayout.Box(icon, GUILayout.Width(size), GUILayout.Height(size));

            DrawName(textStyle, GUILayout.Width(size));

            EditorGUILayout.EndVertical();

            _Rect = GUILayoutUtility.GetLastRect();
        }

        public void DrawInListForm()
        {
            GUIStyle box = GetBox();
            GUIStyle textStyle = GetTextStyle();

            textStyle.alignment = TextAnchor.MiddleLeft;

            EditorGUILayout.BeginHorizontal(box);

            GUILayout.Box(_SerializedAssetData.icon, GUILayout.Width(20), GUILayout.Height(20));

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
    }
}