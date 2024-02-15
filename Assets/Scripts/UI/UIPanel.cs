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

        private Pool<ItemInfoBox> boxPool;

        private RectTransform rectTransform;

        private void Awake()
        {
            boxPool =
                new Pool<ItemInfoBox>(CreateNewBox, box => !box.gameObject.activeSelf,4, 4, box => box.gameObject.SetActive(false));
            rectTransform = GetComponent<RectTransform>();
        }

        public void ChangeContainer(Vector2 _position, IItemContainer itemContainer)
        {
            container = itemContainer;
            position = _position;
            for (int i = 0; i < container.Items.Count; i++)
            {
                boxPool.Get().UpdateInfo(container.Items[i]);
            }
        }

        public void Hide()
        {
            for (int i = 0; i < container.Items.Count; i++)
            {
                boxPool.Return();
            }
        }

        private void LateUpdate()
        {
            if (!gameObject.activeSelf) return;

            // int itemsAmount = container.Items.Count;
            // int maxIndex = Mathf.Max(itemsAmount, boxQueue.Count);
            //
            //
            //
            // for (int i = 0; i < maxIndex; i++)
            // {
            //     if (i >= boxQueue.Count) boxQueue.Enqueue(Instantiate(itemInfoBoxPrefab, groupParent));
            //     var box = boxQueue.Dequeue();
            //     if (i >= itemsAmount) box.gameObject.SetActive(false);
            //     else box.UpdateInfo(container.Items[i]);
            //     boxQueue.Enqueue(box);
            // }
            MoveAndResize(container.Items.Count);
        }

        private void MoveAndResize(int activeBoxes)
        {
            rectTransform.position = CameraManager.Camera.WorldToScreenPoint(position) + (Vector3.up * 60);
            
            var height = 30 + (Mathf.Clamp(activeBoxes - 1, 0, int.MaxValue) * 25);
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, height);
        }

        private ItemInfoBox CreateNewBox() => Instantiate(itemInfoBoxPrefab, groupParent);
    }
}