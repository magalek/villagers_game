using Interfaces;
using Map.Tiles;

namespace UI
{
    public class ContainerUIEventHandler : UIEventHandler
    {
        private readonly IItemContainer container;
        
        public ContainerUIEventHandler(IItemContainer _container)
        {
            container = _container;
        }
        
        public override void OnClick(MapTile tile)
        {
            UIManager.Current.LockPanel();
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