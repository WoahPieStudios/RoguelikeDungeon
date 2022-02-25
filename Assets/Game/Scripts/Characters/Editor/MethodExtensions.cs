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
    public static class MethodExtensions
    {
        public static bool IsNotNullAndEmpty<T>(this IEnumerable<T> source)
        {
            return source != null && source.Any();
        }

        public static bool IsSameType<T>(this IEnumerable<T> source)
        {
            Type type = source.First().GetType();

            return !(source.Where(o => o.GetType() != type).Count() > 0);
        }

        public static string Concat(this IEnumerable<string> source, string seperator)
        {
            string result = "";

            foreach(string s in source)
                result += s + seperator;
                
            return result;
        }

        public static string Concat(this IEnumerable<string> source)
        {
            string result = "";

            foreach(string s in source)
                result += s;
                
            return result;
        }
        
        public static IEnumerable<Type> GetBaseTypes(this Type type)
        {
            Type currentType = type.BaseType;
            
            while(currentType != null)
            {
                yield return currentType;

                currentType = currentType.BaseType;
            }
        }
        
        public static IEnumerable<Type> GetBaseTypes(this Type type, params Type[] rootTypes)
        {
            Type currentType = type.BaseType;
            
            while(currentType != null)
            {
                yield return currentType;

                if(rootTypes.Contains(currentType))
                    break;

                currentType = currentType.BaseType;
            }
        }
    }
}