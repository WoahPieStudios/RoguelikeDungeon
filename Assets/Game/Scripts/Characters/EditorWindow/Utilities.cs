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
    public static class Utilities 
    {
        static readonly Type[] _CreatableAssetRootTypes = new Type[] { typeof(CharacterData), typeof(Characters.Action) };
        
        public static Type[] creatableAssetRootTypes => _CreatableAssetRootTypes;

        public static void CreateFolder(string path)
        {
            string currentPath = "";
            string tempPath;

            foreach(string s in path.Split('/'))
            {
                tempPath = currentPath;

                currentPath += (tempPath.Length > 0 ? "/" : "") + s;

                if(tempPath != string.Empty && !AssetDatabase.IsValidFolder(currentPath))
                    AssetDatabase.CreateFolder(tempPath, s);

                tempPath = currentPath;
            }

            AssetDatabase.Refresh();
        }

        public static IEnumerable<Type> GetAllCreatableAssetTypes()
        {
            foreach(Type t in Assembly.GetAssembly(typeof(Characters.Action)).GetTypes().Where(t => !t.IsAbstract && (t.IsSubclassOf(typeof(Characters.Action)) || t.IsSubclassOf(typeof(CharacterData)))))
                yield return t;

            // foreach(Type t in Assembly.GetAssembly(typeof(CharacterData)).GetTypes().Where(t => !t.IsAbstract && t.IsSubclassOf(typeof(CharacterData))))
            //     yield return t;
        }
    }
}