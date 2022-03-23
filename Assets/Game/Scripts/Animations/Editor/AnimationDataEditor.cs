using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using UnityEditor;

using UnityEditorInternal;

using Game.Animations;

namespace Game.AnimationsEditor
{
    [CustomEditor(typeof(AnimationData))]
    public class AnimationDataEditor : Editor
    {
        private AnimationData _AnimationData;
        private ReorderableList _EventTimeStampList;

        private void OnEnable() 
        {
            SerializedProperty eventTimeStampsProperty = serializedObject.FindProperty("_EventTimeStamps");

            _AnimationData = target as AnimationData;

            _EventTimeStampList = new ReorderableList(eventTimeStampsProperty.serializedObject, eventTimeStampsProperty, true, true, true, true);
            
            _EventTimeStampList.drawElementCallback = OnDrawElement;
            _EventTimeStampList.onAddCallback = OnAdd;
            _EventTimeStampList.onRemoveCallback = OnRemove;
            _EventTimeStampList.drawHeaderCallback = OnHeader;
            _EventTimeStampList.elementHeight = 60;
        }

        public override void OnInspectorGUI()
        {
            SerializedProperty animationClipProperty = serializedObject.FindProperty("_AnimationClip");
            SerializedProperty layerProperty = serializedObject.FindProperty("_Layer");

            EditorGUILayout.PropertyField(animationClipProperty);
            EditorGUILayout.PropertyField(layerProperty);

            if(animationClipProperty.objectReferenceValue)
                _EventTimeStampList.DoLayoutList();

            serializedObject.ApplyModifiedProperties();
        }

        private IEnumerable<SerializedProperty> GetElementsInArray(SerializedProperty property)
        {
            for(int i = 0; i < property.arraySize; i++)
                yield return property.GetArrayElementAtIndex(i);
        }

        private void OnHeader(Rect rect)
        {
            EditorGUI.LabelField(rect, "Event Time Stamps");
        }

        private void OnDrawElement(Rect rect, int index, bool isActive, bool isFocused)
        {
            SerializedProperty timeStampProperty = _EventTimeStampList.serializedProperty.GetArrayElementAtIndex(index);//  properties[index];
            SerializedProperty timeValueProperty = timeStampProperty.FindPropertyRelative("timeValue");
            SerializedProperty timeTypeProperty = timeStampProperty.FindPropertyRelative("timeType");
            SerializedProperty eventIndexProperty = timeStampProperty.FindPropertyRelative("eventIndex");

            rect.y = rect.yMin;
            rect.height = EditorGUIUtility.singleLineHeight;

            timeTypeProperty.enumValueIndex = EditorGUI.Popup(rect, "Event Time Type", timeTypeProperty.enumValueIndex, System.Enum.GetNames(typeof(EventTimeType)));

            rect.y += EditorGUIUtility.singleLineHeight;

            switch((EventTimeType)timeTypeProperty.enumValueIndex)
            {
                case EventTimeType.Percentage:
                    timeValueProperty.floatValue = EditorGUI.Slider(rect, "Time Value", timeValueProperty.floatValue, 0, 1);
                    break;

                case EventTimeType.Frame:
                    int maxFrame = Mathf.RoundToInt(_AnimationData.animationClip.length * _AnimationData.animationClip.frameRate);
                    int currentFrame = Mathf.RoundToInt(timeValueProperty.floatValue * maxFrame);

                    timeValueProperty.floatValue = (float)EditorGUI.IntSlider(rect, "Time Value", currentFrame, 0, maxFrame) / (float)maxFrame;
                    break;

                case EventTimeType.Time:
                    float length = _AnimationData.animationClip.length;
                    
                    timeValueProperty.floatValue = EditorGUI.Slider(rect, "Time Value", timeValueProperty.floatValue * length, 0, length) / length;
                    break;
            }

            rect.y += EditorGUIUtility.singleLineHeight;

            eventIndexProperty.intValue = EditorGUI.IntField(rect, "Event Index", eventIndexProperty.intValue);
        }

        private void OnAdd(ReorderableList list)
        {
            int index = list.index == -1 ? list.count : list.index;

            list.serializedProperty.InsertArrayElementAtIndex(index);
        }

        private void OnRemove(ReorderableList list)
        {
            int index = list.index == -1 ? list.count : list.index;

            list.serializedProperty.DeleteArrayElementAtIndex(index);
        }
    }
}