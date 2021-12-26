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
        static Type[] _CreatableAssetRootTypes = new Type[] { typeof(CharacterData), typeof(Characters.Action) };
        static Type[] _AllCreatableAssetTypes = new Type[] { typeof(HeroData), typeof(EnemyData), typeof(PassiveEffect), typeof(ActiveEffect), typeof(Attack), typeof(Skill), typeof(Ultimate) };
        
        public static Type[] creatableAssetRootTypes => _CreatableAssetRootTypes;
        public static Type[] allCreatableAssetTypes => _AllCreatableAssetTypes;
        
        public static IEnumerable<Type> GetAllCreatableAssetTypes()
        {
            foreach(Type t in Assembly.GetAssembly(typeof(Characters.Action)).GetTypes().Where(t => !t.IsAbstract && (t.IsSubclassOf(typeof(Characters.Action)) || t.IsSubclassOf(typeof(CharacterData)))))
                yield return t;
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