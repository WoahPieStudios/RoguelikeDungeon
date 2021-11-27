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
    public static class AssetUtilities 
    {
<<<<<<< HEAD:Assets/Game/Scripts/Characters/EditorWindow/AssetUtilities.cs
=======
        static readonly Type[] _CreatableAssetRootTypes = new Type[] { typeof(CharacterData), typeof(Characters.Action) };
        
        public static Type[] creatableAssetRootTypes => _CreatableAssetRootTypes;

>>>>>>> ee4d850124339e48857a7472963abbf7e8acd4ca:Assets/Game/Scripts/Characters/EditorWindow/Utilities.cs
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

        public static IEnumerable<UnityEngine.Object> GetAllAssetsInPath(string path)
        {
            if(AssetDatabase.IsValidFolder(path))
            {
                foreach(UnityEngine.Object assetObject in AssetDatabase.LoadAllAssetsAtPath(path))
                    yield return assetObject;
                
                foreach(string assetPath in AssetDatabase.GetSubFolders(path))
                {
                    foreach(UnityEngine.Object assetObject in GetAllAssetsInPath(assetPath))
                        yield return assetObject;
                }
            }
        }
    }
}