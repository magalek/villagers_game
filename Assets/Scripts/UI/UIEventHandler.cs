using Interfaces;
using Map.Tiles;
using Nodes;

namespace UI
{
    public abstract class UIEventHandler
    {
        public abstract void OnClick(MapTile tile);

        public abstract void OnStartHover(MapTile tile);

        public abstract void OnStopHover();
    }

    public class ContainerUIEventHandler : UIEventHandler
    {
        private readonly IItemContainer container;
        
        public ContainerUIEventHandler(IItemContainer _container)
        {
            container = _container;
        }
        
        public override void OnClick(MapTile tile)
        {
            throw new System.NotImplementedException();
        }

        public override void OnStartHover(MapTile tile)
        {
            UIManager.Current.ShowPanelInfo(tile.GridPosition, container);
        }

        public override void OnStopHover()
        {
            UIManager.Current.HidePanelInfo();
        }
    }
}