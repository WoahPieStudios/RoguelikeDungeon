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
    public static class CharactersUtilities
    {
        public static Type[] allCreatableAssetTypes => GetCreatableAssetTypes();
        public static string[] categoryNames => GetCategoryNames().ToArray();

        static Type[] GetCreatableAssetTypes()
        {
            return Assembly.GetAssembly(typeof(Characters.Action)).GetTypes().Where(t => t.GetCustomAttribute<CreatableAssetAttribute>() != null && !t.IsAbstract && t.IsSubclassOf(typeof(ScriptableObject))).ToArray();
        }
        static string[] GetCategoryNames()
        {
            List<string> categoryNames = new List<string>();

            foreach(CreatableAssetAttribute c in Assembly.GetAssembly(typeof(Characters.Action)).GetTypes().
                Select(t => t.GetCustomAttribute<CreatableAssetAttribute>()))
            {
                if(c?.categories == null)
                    continue;

                categoryNames.AddRange(c?.categories.Where(s => !categoryNames.Contains(s)));
            }

            return categoryNames.ToArray();
        }

        public static bool ContainsCategoryAttribute(Type t, string categoryName)
        {
            CreatableAssetAttribute creatableAssetAttribute = t.GetCustomAttribute<CreatableAssetAttribute>();

            return creatableAssetAttribute != null && creatableAssetAttribute.categories.Where(s => s == categoryName).Count() > 0;
        }

        public static string[] GetCategories(Type t)
        {
            return t.GetCustomAttribute<CreatableAssetAttribute>().categories;
        }

        public static IEnumerable<int> GetIndexesFromByte(int source, int length)
        {
            if(source < 0)
            {
                for(int i = 0; i < length; i++)
                    yield return i;
            }

            else
            {
                byte intByte = Convert.ToByte(source);

                string bytes = System.Convert.ToString(intByte, 2);

                int offset = length - bytes.Length; 

                // for(int i = offset; i < length; i++)
                // {
                //     if(bytes[i - offset] != '0')
                //         yield return i;
                // }

                for(int i = bytes.Length - 1; i >= 0; i--)
                {
                    if(bytes[i] != '0')
                        yield return length - (offset + i) - 1;
                }
            }
        }
    }
}