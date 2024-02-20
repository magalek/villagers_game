using System;
using Data.Buildings;

namespace UI
{
    public class UIContext
    {
        public event Action Changed;

        public readonly ConsumableData<BuildingData> BuildingData = new ConsumableData<BuildingData>();

        public void SetBuildingData(BuildingData data)
        {
            BuildingData.Set(data);
            Changed?.Invoke();
        }
    }

    public class ConsumableData<T> where T : class
    {
        public event Action Consumed;
        
        public T Value { get; private set; }

        public void Set(T val)
        {
            Value = val;
        }

        public void Consume()
        {
            Value = null;
            Consumed?.Invoke();
        }
    }
}