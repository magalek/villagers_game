using System;
using System.Collections.Generic;
using Interfaces;
using Managers;
using UnityEngine;

namespace UI
{
    public class UIManager : MonoManager<UIManager>
    {
        [SerializeField] private UIPanel uiPanelPrefab;
        
        [SerializeField, Range(0, 100)] private int boxHeight;

        private RectTransform rectTransform;

        private Dictionary<IUIPanelBuilder, UIPanel> uiPanels = new Dictionary<IUIPanelBuilder, UIPanel>();

        private UIPanel panel;

        private UIPanel lockedPanel;

        protected override void OnAwake()
        {
            rectTransform = GetComponent<RectTransform>();
            panel = Instantiate(uiPanelPrefab, transform);
            panel.gameObject.SetActive(false);
        }

        public void ShowPanelInfo(Vector2 position, IItemContainer container)
        {
            if (!panel.gameObject.activeSelf) panel.gameObject.SetActive(true);
            panel.ChangeContainer(position, container);
        }

        public void LockPanel()
        {
            if (lockedPanel == null) lockedPanel = Instantiate(uiPanelPrefab, transform);
            lockedPanel.gameObject.SetActive(true);
            panel.CopyTo(lockedPanel);
        }

        public void HidePanelInfo()
        {
            panel.Hide();
            panel.gameObject.SetActive(false);
        }

        public void OnEmptyClick()
        {
            lockedPanel.gameObject.SetActive(false);
        }
    }
}