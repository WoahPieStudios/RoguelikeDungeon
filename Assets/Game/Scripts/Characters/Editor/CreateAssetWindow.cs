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
    public class CreateAssetWindow : EditorWindow
    {
        Type[] _CreatableAssetTypes;
        string[] _CreatableAssetTypeNames;
        int _CurrentSelectedTypeIndex = 0; 

        SerializedAssetData _NewSerializedAssetData;

        bool _IsNameEmpty = false;

        [MenuItem("Window/Characters Asset Creation Window")]
        static void OpenWindow()
        {
            GetWindow<CreateAssetWindow>("Characters Asset Creation Window");
        }

        void OnEnable() 
        {
            _CreatableAssetTypes = CharactersUtilities.allCreatableAssetTypes;

            _CreatableAssetTypeNames = _CreatableAssetTypes.Select(t => t.Name).ToArray();

            _CurrentSelectedTypeIndex = 0;
            
            _NewSerializedAssetData = CreateNewAssetData(_CreatableAssetTypes[_CurrentSelectedTypeIndex]);

            _IsNameEmpty = false;
        }

        void OnGUI() 
        {
            int tempSelectedTypeIndex = EditorGUILayout.Popup("Asset Type", _CurrentSelectedTypeIndex, _CreatableAssetTypeNames);

            EditorGUILayout.Space();

            if(tempSelectedTypeIndex != _CurrentSelectedTypeIndex)
            {
                _CurrentSelectedTypeIndex = tempSelectedTypeIndex;

                _NewSerializedAssetData = CopyOverData(_NewSerializedAssetData, CreateNewAssetData(_CreatableAssetTypes[_CurrentSelectedTypeIndex]));
            }

            DrawSerializedAssetData(_NewSerializedAssetData);

            if(GUILayout.Button("Save"))
            {
                if(_NewSerializedAssetData.name != "")
                {
                    SaveFile(_NewSerializedAssetData);
    
                    Close();
                }

                else
                    _IsNameEmpty = true;
            }

            if(_IsNameEmpty)
            {
                EditorGUILayout.LabelField("Name must not be empty!");
            }
        }

        SerializedAssetData CreateNewAssetData(Type type)
        {
            return new SerializedAssetData(CreateInstance(type));
        }

        SerializedAssetData CopyOverData(SerializedAssetData fromData, SerializedAssetData toData)
        {
            string fromDataName = fromData.name;
            
            SerializedProperty propertyIterator = fromData.serializedObject.GetIterator();
            SerializedProperty property;

            propertyIterator.NextVisible(true);
            
            while(propertyIterator.NextVisible(true))
            {
                property = toData.serializedObject.FindProperty(propertyIterator.name);

                if(property != null && property.propertyType == propertyIterator.propertyType)
                    toData.serializedObject.CopyFromSerializedProperty(propertyIterator);
            }

            toData.assetObject.name = fromDataName;

            return toData;
        }

        void DrawSerializedAssetData(SerializedAssetData serializedAssetData)
        {
            SerializedProperty property = serializedAssetData.serializedObject.GetIterator();

            EditorGUI.BeginChangeCheck();

            property.NextVisible(true);

            serializedAssetData.assetObject.name = EditorGUILayout.TextField("Name", serializedAssetData.name);

            while(property.NextVisible(true))
                EditorGUILayout.PropertyField(property);

            if(EditorGUI.EndChangeCheck())
            {
                serializedAssetData.UpdateData();
            }
        }

        void SaveFile(SerializedAssetData serializedAssetData)
        {
            Type type = serializedAssetData.assetObject.GetType();
            string path = "Assets/Game/Data/Characters/" + type.Name;

            if(serializedAssetData.name != "")
            {
                if(!AssetDatabase.IsValidFolder(path))
                    AssetUtilities.CreateFolder(path);
                
                AssetDatabase.CreateAsset(serializedAssetData.assetObject, $"{path}/{serializedAssetData.name}.asset");
            }
        }
    }
}