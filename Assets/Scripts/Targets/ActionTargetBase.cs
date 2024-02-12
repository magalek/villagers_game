using Interfaces;
using Map.Tiles;

namespace Targets
{
    public abstract class ActionTargetBase : BaseSceneMonoBehaviour, IClickTarget
    {
        private const int ACTION_TARGET_LAYER = 6;
        
        protected override void Awake()
        {
            base.Awake();
            gameObject.layer = ACTION_TARGET_LAYER;
        }

        public abstract void OnClick(MapTile tile);
    }
}