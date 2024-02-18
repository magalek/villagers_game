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
}