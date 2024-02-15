using UnityEngine;

namespace Targets
{
    public class BaseSceneMonoBehaviour : MonoBehaviour
    {
        protected virtual void Awake()
        {
            var position = transform.position;
            position.z = position.y;
            transform.position = position;
        }
    }
}