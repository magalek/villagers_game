using Data.Buildings;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.BuildingPanel
{
    public class BuildingInfoBox : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Image iconRenderer;
        [SerializeField] private Image border;

        public BuildingData Data { get; private set; }
        
        private Button button;

        private BuildingPanel panel;
        
        private void Awake()
        {
            button = GetComponentInChildren<Button>();
            button.onClick.AddListener(Select);
        }

        private void Select()
        {
            panel.Select(this);
            border.gameObject.SetActive(true);
        }

        public void Deselect()
        {
            border.gameObject.SetActive(false);
        }

        public void Initialize(BuildingPanel parentPanel, BuildingData data)
        {
            panel = parentPanel;
            iconRenderer.sprite = data.icon;
            Data = data;
        }

        public void OnPointerEnter(PointerEventData eventData) => panel.OnInfoBoxPointerEnter(this);
        public void OnPointerExit(PointerEventData eventData) => panel.OnInfoBoxPointerExit();
    }
}