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
        public static void CreateFolder(string path)
        {
            string currentPath = "";
            string tempPath = "";

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