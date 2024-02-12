using System;
using System.Collections.Generic;
using Interfaces;
using Managers;
using UnityEngine;

namespace UI
{
    public class UIPanelInfoManager : MonoManager
    {
        [SerializeField] private UIPanel uiPanelPrefab;
        
        [SerializeField, Range(0, 100)] private int boxHeight;

        private RectTransform rectTransform;

        private Dictionary<IUIPanelBuilder, UIPanel> uiPanels = new Dictionary<IUIPanelBuilder, UIPanel>();

        private UIPanel panel;
        
        protected override void OnAwake()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        public void ShowPanelInfo(Vector2 position, IItemContainer container)
        {
            if (panel == null) panel = Instantiate(uiPanelPrefab, transform);
            panel.ChangeContainer(position, container);

            // if (uiPanels.TryGetValue(container, out _))
            // {
            //     var panel = Instantiate(uiPanelPrefab, transform);
            //     panel.ChangeContainer(container);
            // }
        }
    }
}