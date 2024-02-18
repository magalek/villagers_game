using System;
using System.Collections.Generic;
using System.Linq;
using Interfaces;
using Managers;
using UnityEngine;
using Utility;

namespace UI
{
    public class UIPanel : MonoBehaviour
    {
        [SerializeField] private ItemInfoBox itemInfoBoxPrefab;

        [SerializeField] private Transform groupParent;
        
        public event Action<UIPanel, int> Resized;
        
        private IItemContainer container;
        private Vector2 position;

        private List<ItemInfoBox> boxes = new List<ItemInfoBox>();

        private RectTransform rectTransform;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        public void ChangeContainer(Vector2 _position, IItemContainer itemContainer)
        {
            if (container != null)
            {
                container.Updated -= ScaleBoxes;
            }
            container = itemContainer;
            position = _position;
            container.Updated += ScaleBoxes;
            ScaleBoxes();
        }

        public void CopyTo(UIPanel panel)
        {
            panel.ChangeContainer(position, container);
        }

        public void Hide()
        {
            foreach (var box in boxes)
            {
                box.gameObject.SetActive(false);
            }
        }

        private void LateUpdate()
        {
            if (!gameObject.activeSelf || container == null) return;
            MoveAndResize(container.Items.Count);
        }

        private void MoveAndResize(int activeBoxes)
        {
            rectTransform.position = CameraManager.Camera.WorldToScreenPoint(position) + (Vector3.up * 60);
            
            var height = 30 + (Mathf.Clamp(activeBoxes - 1, 0, int.MaxValue) * 25);
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, height);
        }

        private void ScaleBoxes()
        {
            int itemsAmount = container.Items.Count;
            int maxIndex = Mathf.Max(itemsAmount, boxes.Count);
            
            
            for (int i = 0; i < maxIndex; i++)
            {
                if (i >= boxes.Count) boxes.Add(CreateNewBox());
                var box = boxes[i];
                if (i >= itemsAmount) box.gameObject.SetActive(false);
                else
                {
                    box.gameObject.SetActive(true);
                    box.UpdateInfo(container.Items[i]);
                }
            }
        }

        private ItemInfoBox CreateNewBox() => Instantiate(itemInfoBoxPrefab, groupParent);
    }
}