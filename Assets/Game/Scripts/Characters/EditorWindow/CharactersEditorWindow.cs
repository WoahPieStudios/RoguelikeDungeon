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
        Type[] _ActionTypes = new Type[] { typeof(PassiveEffect), typeof(ActiveEffect), typeof(Attack), typeof(Skill), typeof(Ultimate) };
        Type[] _CharacterDataTypes = new Type[] { typeof(HeroData), typeof(EnemyData) };

        Dictionary<Type, CharacterData[]> _CharacterDataDictionary = new Dictionary<Type, CharacterData[]>();
        Dictionary<Type, Characters.Action[]> _ActionsDictionary = new Dictionary<Type, Characters.Action[]>();

        [MenuItem("Window/Characters Window")]
        static CharactersEditorWindow OpenWindow()
        {
            CharactersEditorWindow window = CharactersEditorWindow.GetWindow<CharactersEditorWindow>("Characters Window");

            window.RefreshDatabase();

            return window;
        }

        [OnOpenAsset]
        static bool OpenWindow(int instanceID, int line)
        {
            CharactersEditorWindow window = OpenWindow();

            ScriptableObject asset = EditorUtility.InstanceIDToObject(instanceID) as ScriptableObject;

            Type type = asset.GetType();

            if(type.IsSubclassOf(typeof(Characters.Action)))
            {
                window.SelectAction(asset as Characters.Action);
            }

            else if(type.IsSubclassOf(typeof(CharacterData)))
            {
                window.SelectCharacterData(asset as CharacterData);
            }

            return true;
        }

        void OnGUI() 
        {
            EditorGUILayout.BeginVertical();

            DrawMenuBar();

            DrawContent();

            EditorGUILayout.EndVertical();
            //AssetDatabase.folder    
        } 
        
        void DrawMenuBar()
        {
            EditorGUILayout.BeginHorizontal();

            if(GUILayout.Button("Refresh"))
            {
                RefreshDatabase();
            }

            EditorGUILayout.EndHorizontal();
        }

        void DrawContent()
        {
            EditorGUILayout.BeginHorizontal();

            DrawExplorerArea();

            DrawAssetContent();

            EditorGUILayout.EndHorizontal();
        }

        void DrawExplorerArea()
        {
            if(GUILayout.Button("Explorer"))
            {
                RefreshDatabase();
            }
        }

        void DrawAssetContent()
        {

            if(GUILayout.Button("Asset Content"))
            {
                RefreshDatabase();
            }
        }

        void RefreshDatabase()
        {
            string[] assetPaths = AssetDatabase.GetAllAssetPaths();

            foreach(Type t in _ActionTypes)
            {
                Debug.Log(t.Name);
            }

            foreach(Type t in _CharacterDataTypes)
            {
                Debug.Log(t.Name);
            }
        }

        void SelectCharacterData(CharacterData data)
        {
            Debug.Log(data.name);
        }

        void SelectAction(Characters.Action action)
        {
            Debug.Log(action.name);
        }
    }
}