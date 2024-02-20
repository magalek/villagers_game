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

        public static void RegisterMonoManager(IMonoManager manager)
        {
            if (ManagersDictionary.ContainsKey(manager.GetType()))
            {
                Debug.LogWarning($"Another instance of {manager.GetType()} tried registering - destroying.");
                manager.Destroy();
                return;
            }
            ManagersDictionary[manager.GetType()] = manager;
        }
        
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