using UnityEngine;

namespace Managers
{
    
    public class EconomyManager : IManager
    {
        public void AddValue(float value)
        {
            Debug.Log($"Added {value}");
        }
    }
}