using UnityEngine;

namespace Managers
{
    public class MonoManager<TManager> : MonoBehaviour, IMonoManager where TManager : IMonoManager
    {
        public static TManager Current => (TManager)ManagerLoader.Get(typeof(TManager));
        
        private void Awake()
        {
            ManagerLoader.AddMonoManager(this);
            OnAwake();
        }
        
        protected virtual void OnAwake(){}
    }
}