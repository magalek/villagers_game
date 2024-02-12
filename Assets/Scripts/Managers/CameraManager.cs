using System;
using UI;
using UnityEngine.InputSystem;

namespace Managers
{
    public class CameraManager : MonoManager
    {
        private UIInfoManager uiInfoManager;

        protected override void OnAwake()
        {
            uiInfoManager = ManagerLoader.Get<UIInfoManager>();
        }

        private void LateUpdate()
        {
            
        }
    }
}