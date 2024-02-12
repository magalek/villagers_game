using System;
using UnityEngine;
using UnityEngine.UI;
using Vector2 = System.Numerics.Vector2;

namespace Actions
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] private GameObject barParent;
        [SerializeField] private Image imageBar;

        private Transform parent;

        private RectTransform rectTransform;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        public void Initialize(Transform _parent)
        {
            parent = _parent;
        }
        
        public void UpdateBar(float amount)
        {
            if (!barParent.activeSelf) barParent.SetActive(true);

            rectTransform.position = Camera.main.WorldToScreenPoint(parent.position) + (Vector3.up * 20);
            imageBar.fillAmount = Mathf.Clamp01(amount);
        }

        public void HideBar()
        {
            barParent.SetActive(false);
        }
    }
}