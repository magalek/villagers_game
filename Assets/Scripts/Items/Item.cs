using System;
using Interfaces;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace Items
{
    [CreateAssetMenu(menuName = "Data/Items/New Item", fileName = "Item")]
    public class Item : ScriptableObject
    {
        [SerializeField, ItemId] private string id;

        [SerializeField] private Sprite sprite;
        
        public string Id => id;
        public Sprite Sprite => sprite;

        public bool Equals(Item item) => item.id == id;
    }
}