using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Utility.Reflection;

namespace Managers
{
    public static class ManagerLoader
    {
        private static readonly Dictionary<Type, IMonoManager> ManagersDictionary = new Dictionary<Type, IMonoManager>();
        //
        // [RuntimeInitializeOnLoadMethod]
        // private static void CreateManagers()
        // {
        //     foreach (var type in ReflectionUtility.GetImplementingTypes(typeof(Manager<>)))
        //     {
        //         Debug.Log(type);
        //
        //         var genericType = typeof(Manager<>).MakeGenericType(type.BaseType.GenericTypeArguments[0]);
        //         
        //         ManagersDictionary[type] = Activator.CreateInstance(genericType);
        //     }
        // }

        public static void AddMonoManager(IMonoManager manager)
        {
            ManagersDictionary[manager.GetType()] = manager;
        }
        //
        // public static TManager Get<TManager>() where TManager : class, IManager
        // {
        //     if (ManagersDictionary.TryGetValue(typeof(TManager), out object manager))
        //     {
        //         return (TManager)manager;
        //     }
        //     return null;
        // }

        public static object Get(Type managerType)
        {
            if (ManagersDictionary.TryGetValue(managerType, out IMonoManager manager))
            {
                return manager;
            }
            return null;
        }
    }
}