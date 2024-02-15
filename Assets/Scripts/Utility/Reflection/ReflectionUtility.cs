using System;
using System.Collections.Generic;
using UnityEngine;

namespace Utility.Reflection
{
    public static class ReflectionUtility
    {
        public static List<Type> GetImplementingTypes<TType>()
        {
            List<Type> types = new List<Type>();

            foreach (var type in typeof(TType).Assembly.GetTypes())
            {
                if (type != typeof(TType) && 
                    typeof(TType).IsAssignableFrom(type)) types.Add(type);
            }

            return types;
        }
        
        public static List<Type> GetImplementingTypes(Type passedType)
        {
            List<Type> types = new List<Type>();

            foreach (var type in passedType.Assembly.GetTypes())
            {
                if (type.BaseType == null || !type.BaseType.IsGenericType) continue;
                
                if (type.BaseType.GetGenericTypeDefinition() == passedType) types.Add(type);
            }

            return types;
        }
    }
}