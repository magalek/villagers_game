using UnityEngine;

namespace Managers
{
    public class MonoManager : MonoBehaviour, IMonoManager
    {
        private void Awake()
        {
            ManagerLoader.AddMonoManager(this);
            OnAwake();
        }
        
        protected virtual void OnAwake(){}
    }
}