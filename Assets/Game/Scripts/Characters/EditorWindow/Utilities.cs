using System;
using System.Linq;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Game.CharactersEditor
{
    public static class Utilities 
    {
        public static IEnumerable<Type> GetBaseTypes(this Type type)
        {
            if(type.BaseType == null) 
                return null;

            return Enumerable.Repeat(type.BaseType, 1).Concat(type.BaseType.GetBaseTypes());
        }
        
        public static IEnumerable<Type> GetBaseTypes(this Type type, params Type[] rootTypes)
        {
            int i = 0;
            
            while(type.BaseType != null && !rootTypes.Contains(type.BaseType))
            {
                if(i > 50)
                    break;

                yield return type.BaseType;
            }
        }
    }
}