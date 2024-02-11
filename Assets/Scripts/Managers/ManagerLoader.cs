﻿using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Utility.Reflection;

namespace Managers
{
    public static class ManagerLoader
    {
        private static readonly Dictionary<Type, object> ManagersDictionary = new Dictionary<Type, object>();

        [RuntimeInitializeOnLoadMethod]
        private static void CreateManagers()
        {
            foreach (var type in ReflectionUtility.GetImplementingTypes<IManager>())
            {
                Debug.Log(type);
                ManagersDictionary[type] = (IManager)Activator.CreateInstance(type);
            }
        }

        public static void AddMonoManager(IMonoManager manager)
        {
            ManagersDictionary[manager.GetType()] = manager;
        }

        public static TManager Get<TManager>() where TManager : IManagerBase
        {
            return (TManager)ManagersDictionary[typeof(TManager)];
        }
    }
}