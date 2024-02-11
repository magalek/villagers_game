using Interfaces;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Items
{
    public class ItemObject : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer itemRenderer;
        
        public IItem item;

        public void Initialize(IItem _item)
        {
            item = _item;
            itemRenderer.sprite = item.Sprite;
        }
    }
}