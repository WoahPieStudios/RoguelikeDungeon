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
        public static Type[] creatableAssetRootTypes => GetRootCreatableAssetTypes(); //_CreatableAssetRootTypes;
        public static Type[] allCreatableAssetTypes => GetCreatableAssetTypes(); //_AllCreatableAssetTypes;

        static Type[] GetCreatableAssetTypes()
        {
            return Assembly.GetAssembly(typeof(Characters.Action)).GetTypes().Where(t => t.GetCustomAttribute<CreatableAssetAttribute>() != null && !t.IsAbstract).ToArray();
        }

        static Type[] GetRootCreatableAssetTypes()
        {
            return Assembly.GetAssembly(typeof(Characters.Action)).GetTypes().Where(t => t.GetCustomAttribute<RootCreatableAssetAttribute>() != null && !t.IsAbstract).ToArray();
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