using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using UnityEditor;

namespace Game.CharactersEditor
{
    public class AssetItem
    {
        Rect _Rect;

        SerializedAssetData _SerializedAssetData;

        string _Path;

        public SerializedAssetData serializedAssetData => _SerializedAssetData;
        public UnityEngine.Object assetObject => _SerializedAssetData.assetObject;
        public string name => _SerializedAssetData.name;
        public Texture icon => _SerializedAssetData.icon;
        public string path => _Path;

        public bool isSelected { get; set; } = false;
        
        public AssetItem(UnityEngine.Object assetObject)
        {
            _SerializedAssetData = new SerializedAssetData(assetObject);
        }

        public AssetItem(UnityEngine.Object assetObject, string path)
        {
            _SerializedAssetData = new SerializedAssetData(assetObject);

            _Path = path;
        }

        GUIContent LimitLabel(string text)
        {
            GUIContent content = new GUIContent(text);

            Vector2 size = GUI.skin.label.CalcSize(content);

            return content;
        }

        public void Draw(float size)
        {
            GUIStyle box = new GUIStyle("box");
            GUIStyle textStyle = new GUIStyle();

            if(isSelected)
                box.normal.background = Texture2D.grayTexture;

            textStyle.normal.textColor = Color.white;
            textStyle.clipping = TextClipping.Clip;

            if(size > 40f)
            {
                textStyle.alignment = TextAnchor.MiddleCenter;

                EditorGUILayout.BeginVertical(box, GUILayout.Width(size), GUILayout.Height(size));

                GUILayout.Box(_SerializedAssetData.icon, GUILayout.Width(size), GUILayout.Height(size));
                GUILayout.Label(LimitLabel(_SerializedAssetData.name), textStyle, GUILayout.Width(size));

                EditorGUILayout.EndVertical();
            }

            else
            {
                textStyle.alignment = TextAnchor.MiddleLeft;

                EditorGUILayout.BeginHorizontal(box);

                GUILayout.Box(_SerializedAssetData.icon, GUILayout.Width(20), GUILayout.Height(20));
                GUILayout.Label(LimitLabel(_SerializedAssetData.name));

                EditorGUILayout.EndHorizontal();
            }

            _Rect = GUILayoutUtility.GetLastRect();
        }

        public bool Contains(Vector2 point)
        {
            return _Rect.Contains(point);
        }
    }
}