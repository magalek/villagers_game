using Interfaces;
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
        
        public void UpdateInfo(IItem item)
        {
            itemImage.sprite = item.Sprite;
            amountLabel.text = item.Amount.ToString();
        }
    }
}