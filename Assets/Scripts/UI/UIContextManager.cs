using System;
using System.Collections.Generic;
using Data.Buildings;
using Interfaces;
using Managers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class UIContextManager : MonoManager<UIContextManager>
    {
        [SerializeField] private UIPanel uiPanelPrefab;
        [SerializeField] private Image previewImage;
        [SerializeField] private RectTransform previewTransform;
        
        [SerializeField, Range(0, 100)] private int boxHeight;

        private UIPanel panel;

        private UIPanel lockedPanel;

        public UIContext Context { get; } = new UIContext();

        protected override void OnAwake()
        {
            panel = Instantiate(uiPanelPrefab, transform);
            panel.gameObject.SetActive(false);
            Context.Changed += OnContextChanged;
        }

        private void Update()
        {
            if (previewImage.gameObject.activeSelf && TileSelector.CurrentTile)
            {
                previewTransform.position =
                    CameraManager.Camera.WorldToScreenPoint(TileSelector.CurrentTile.transform.position + (Vector3.up * 0.5f));
            }
        }

        private void OnContextChanged()
        {
            if (Context.BuildingData.Value)
            {
                previewImage.gameObject.SetActive(true);
                previewImage.sprite = Context.BuildingData.Value.icon;
                previewTransform.sizeDelta = new Vector2(
                    Context.BuildingData.Value.icon.texture.width, 
                    Context.BuildingData.Value.icon.texture.height);
            }
            else
            {
                previewImage.gameObject.SetActive(false);
                previewImage.sprite = null;
            }
        }

        private void Start()
        {
            InputManager.Current.LeftMouseClicked += OnLeftMouseClicked;
        }

        private void OnLeftMouseClicked()
        {
            //if (EventSystem.current.IsPointerOverGameObject()) return;
            if (TileSelector.CurrentTile == null) return;
            TileSelector.CurrentTile.Click(Context);
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
            if (lockedPanel) lockedPanel.gameObject.SetActive(false);
        }
    }
}