using System.Collections.Generic;

namespace Utility
{
    public static class ListExtensions
    {
        public static T RandomItem<T>(this List<T> list) => list[UnityEngine.Random.Range(0, list.Count)];
    }
}