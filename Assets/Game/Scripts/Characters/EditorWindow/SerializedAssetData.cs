using System;
using System.Linq;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using UnityEditor;

using Game.Characters;

namespace Game.CharactersEditor
{
    class SerializedAssetData
    {
        Texture2D _Icon;
        readonly SerializedObject _SerializedObject;

        public Texture2D icon => _Icon;
        public string name => assetObject.name;
        public UnityEngine.Object assetObject => serializedObject.targetObject;
        public SerializedObject serializedObject => _SerializedObject;

        public SerializedAssetData(UnityEngine.Object assetObject)
        {
            _Icon = GetIcon(assetObject);

            _SerializedObject = new SerializedObject(assetObject);
        }

        Texture2D GetIcon(UnityEngine.Object assetObject)
        {
            Sprite sprite = (assetObject as IIcon).icon;

            return sprite ? AssetPreview.GetAssetPreview(sprite) : null;
        }


        public void UpdateData()
        {
            string name = this.name;

            _Icon = GetIcon(assetObject);

            _SerializedObject.ApplyModifiedProperties();

            assetObject.name = name;
        }
    }
}