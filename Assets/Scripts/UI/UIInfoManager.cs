using System;
using Interfaces;
using Managers;
using UnityEngine;

namespace UI
{
    public class UIInfoManager : MonoManager
    {
        [SerializeField] private ItemInfoBox itemInfoBoxPrefab;
        [SerializeField, Range(0, 100)] private int boxHeight;

        [SerializeField] private Transform boxParent;
        
        private RectTransform rectTransform;

        protected override void OnAwake()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        public void ShowContainerInfo(IItemContainer container)
        {
            foreach (var item in container.Items)
            {
                
            }
        }
    }

    public class ItemInfoBox : MonoBehaviour
    {
    }
}