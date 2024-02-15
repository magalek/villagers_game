using Interfaces;
using Map.Tiles;
using UI;

namespace Targets
{
    public abstract class ActionNodeBase : BaseSceneMonoBehaviour, IUIEventTarget
    {
        private const int NODE_TARGET_LAYER = 6;
        
        protected override void Awake()
        {
            base.Awake();
            gameObject.layer = NODE_TARGET_LAYER;
        }
        
        public abstract UIEventHandler UIEventHandler { get; protected set; }
    }
}