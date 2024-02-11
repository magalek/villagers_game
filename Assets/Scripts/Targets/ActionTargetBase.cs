using UnityEngine;

namespace Targets
{
    public class ActionTargetBase : MonoBehaviour
    {
        private const int ACTION_TARGET_LAYER = 6;
        
        protected virtual void Awake()
        {
            gameObject.layer = ACTION_TARGET_LAYER;
        }
    }
}