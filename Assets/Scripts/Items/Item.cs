using Interfaces;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace Items
{
    [CreateAssetMenu(menuName = "Items/New Item", fileName = "Item")]
    public class Item : ScriptableObject, IItem
    {
        [ItemId] public string id;

        [SerializeField] private Sprite sprite;
        
        public string Id => id;
        public Sprite Sprite => sprite;
        
        public int Amount { get; set; }

        public bool ChangeAmount(int value)
        {
            Amount += value;
            return true; // change so checked if not bigger than stack size 
        }

        public IItem Copy()
        {
            return Instantiate(this);
        }
    }
}