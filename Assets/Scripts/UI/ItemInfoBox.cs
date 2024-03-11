using Interfaces;
using Items;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    public class ItemInfoBox : MonoBehaviour
    {
        [SerializeField] private Image itemImage;
        [SerializeField] private TextMeshProUGUI amountLabel;
        
        public void UpdateInfo(ContainerEntry entry)
        {
            //Debug.Log($"{entry.item} - {entry.amount}");
            itemImage.sprite = entry.item.Sprite;
            amountLabel.text = entry.amount.ToString();
        }
    }
}