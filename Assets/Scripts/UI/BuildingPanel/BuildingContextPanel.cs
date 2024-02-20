using System;
using Data.Buildings;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.BuildingPanel
{
    public class BuildingContextPanel : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI descriptionLabel;

        private void Awake()
        {
            ClearInfo();
        }

        public void ShowInfo(BuildingData data)
        {
            icon.gameObject.SetActive(true);
            icon.sprite = data.icon;
            descriptionLabel.text = data.description;
        }

        public void ClearInfo()
        {
            icon.gameObject.SetActive(false);
            icon.sprite = null;
            descriptionLabel.text = string.Empty;
        }
    }
}