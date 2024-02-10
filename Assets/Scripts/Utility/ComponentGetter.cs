using UnityEngine;

namespace Utility
{
    public class ComponentGetter<TComponent>
    {
        private TComponent component;

        public ComponentGetter(Component _parent)
        {
            component = _parent.GetComponent<TComponent>();
            if (component == null) Debug.LogError($"No component {typeof(TComponent)} on {_parent.name}");
        }

        public TComponent Get() => component;
    }
}