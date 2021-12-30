using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using UnityEditor;

namespace Game.CharactersEditor
{
    public class Inspector
    {
        Vector2 _ScrollPosition;

        public bool lockContent { get; set; } = false;
        public SerializedObject serializedObjectInspected { get; set; } = null;
        public event Action onGUIChange;

        public void Draw(float inspectorWidth)
        {
            UnityEngine.Object[] assetObjects = Select.selection.Where(o => o is AssetItem).Select(o => o as AssetItem).Select(o => o.assetObject).ToArray();

            EditorGUILayout.BeginVertical("box", GUILayout.Width(inspectorWidth));

            if(GUILayout.Button(lockContent ? "Unlock" : "Lock"))
                lockContent = assetObjects.Length > 0 && !lockContent;

            if(!lockContent)
                serializedObjectInspected = assetObjects.Length > 0 && assetObjects.IsSameType() ? new SerializedObject(assetObjects) : null;

            
            if(serializedObjectInspected == null)
            {
                GUILayout.FlexibleSpace();
                
                EditorGUILayout.EndVertical();

                return;
            }

            EditorGUI.BeginChangeCheck();

            SerializedProperty property = serializedObjectInspected.GetIterator();

            property.NextVisible(true);

            _ScrollPosition = EditorGUILayout.BeginScrollView(_ScrollPosition);

            EditorGUILayout.LabelField(serializedObjectInspected.isEditingMultipleObjects ? "-" : serializedObjectInspected.targetObject.name, EditorStyles.boldLabel);

            while (property.NextVisible(false))
                EditorGUILayout.PropertyField(property);

            EditorGUILayout.EndScrollView();

            if(EditorGUI.EndChangeCheck())
            {
                serializedObjectInspected.ApplyModifiedProperties();

                onGUIChange?.Invoke();

                // DrawExplorerArea();

                // Repaint();
            }

            EditorGUILayout.EndVertical();
        }
    }
}