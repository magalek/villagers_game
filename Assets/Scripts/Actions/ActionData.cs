using System.Collections.Generic;
using Entities;
using Items;

namespace Actions
{
    public class ActionData
    {
        public readonly IEntity Entity;

        public Item Item
        {
            get => item;
            set
            {
                if (value == null) Entity.ItemHolder.ClearItem();
                else Entity.ItemHolder.SetItem(value);
                item = value;
            }
        }

        private Item item;

        public ActionData(IEntity entity)
        {
            Entity = entity;
        }

        public override string ToString()
        {
            return $"DATA: {Entity} - {Item}";
        }
    }
}