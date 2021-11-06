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
    public class CreateAssetWindow : EditorWindow
    {
        Type[] _CreatableAssetTypes;
        string[] _CreatableAssetTypeNames;
        int _CurrentSelectedTypeIndex = 0; 

        SerializedAssetData _NewSerializedAssetData;

        void OnEnable() 
        {
            _CreatableAssetTypes = GetAllCreatableAsset();

            _CreatableAssetTypeNames = _CreatableAssetTypes.Select(t => CompileInheritedClassNames(t)).ToArray();

            _CurrentSelectedTypeIndex = 0;
            
            _NewSerializedAssetData = CreateNewAssetData(_CreatableAssetTypes[_CurrentSelectedTypeIndex]);
        }

        void OnGUI() 
        {
            int tempSelectedTypeIndex = _CurrentSelectedTypeIndex;

            tempSelectedTypeIndex = EditorGUILayout.Popup(_CurrentSelectedTypeIndex, _CreatableAssetTypeNames);

            if(tempSelectedTypeIndex != _CurrentSelectedTypeIndex)
            {
                _CurrentSelectedTypeIndex = tempSelectedTypeIndex;

                _NewSerializedAssetData = CopyOverData(_NewSerializedAssetData, CreateNewAssetData(_CreatableAssetTypes[_CurrentSelectedTypeIndex]));
            }

            DrawSerializedAssetData(_NewSerializedAssetData);

            if(GUILayout.Button("Save"))
            {
                AssetDatabase.CreateAsset(_NewSerializedAssetData.assetObject, $"Assets/Game/{_NewSerializedAssetData.name}.asset");

                Close();
            }

            if(GUILayout.Button("Test"))
            {
                foreach(Type t in typeof(TestPassive).GetBaseTypes(typeof(Characters.Action)))
                {
                    Debug.Log(t);
                }
            }
        }

        Type[] GetAllCreatableAsset()
        {
            List<Type> typeList = new List<Type>();
            
            typeList.AddRange(Assembly.GetAssembly(typeof(Characters.Action)).GetTypes().Where(t => !t.IsAbstract && typeof(IIcon).IsAssignableFrom(t)));
            typeList.AddRange(Assembly.GetAssembly(typeof(CharacterData)).GetTypes().Where(t => !t.IsAbstract && typeof(IIcon).IsAssignableFrom(t)));

            return typeList.ToArray();
        }

        string CompileInheritedClassNames(Type type)
        {
            string compiledClassNames = "";

            foreach(string typeName in type.Assembly.GetTypes().Where(t => type.IsSubclassOf(t)).Select(t => t.Name))
                compiledClassNames += typeName + "/";

            compiledClassNames += type.Name;
            
            return compiledClassNames;
        }

        SerializedAssetData CreateNewAssetData(Type type)
        {
            return new SerializedAssetData(ScriptableObject.CreateInstance(type));
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
    }
}