using System;
using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    public class Pool<TObject>
    {
        private readonly List<TObject> objects = new List<TObject>();
        private readonly int increment;
        private readonly Func<TObject> builderMethod;
        private readonly Func<TObject, bool> canGetMethod;
        private readonly Action<TObject> disableMethod;

        public Pool(Func<TObject> _builderMethod, Func<TObject, bool> _canGetMethod, int initialSize, int _increment, Action<TObject> _disableMethod = null)
        {
            increment = _increment;
            builderMethod = _builderMethod;
            canGetMethod = _canGetMethod;
            disableMethod = _disableMethod;
            
            for (int i = 0; i < initialSize; i++)
            {
                objects.Add(builderMethod());
            }
        }

        public TObject Get()
        {
            var count = objects.Count;
            for (int i = 0; i < count; i++)
            {
                if (canGetMethod(objects[i]))
                {
                    return objects[i];
                }
            }
            for (int i = 0; i < increment; i++)
            {
                objects.Add(builderMethod());
            }
            return objects[count];
        }
        
        public void Return() {
            foreach (var obj in objects)
            {
                disableMethod?.Invoke(obj);
            }
        }
    }
}