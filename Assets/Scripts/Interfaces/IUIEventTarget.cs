using Map.Tiles;
using UI;

namespace Interfaces
{
    public interface IUIEventTarget
    {
        UIEventHandler UIEventHandler { get; }
    }
}