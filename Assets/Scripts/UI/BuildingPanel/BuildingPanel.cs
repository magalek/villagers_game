using System;
using System.Collections.Generic;
using Data.Buildings;
using Managers;
using UnityEditor;
using UnityEngine;

namespace UI.BuildingPanel
{
    public class BuildingPanel : MonoBehaviour
    {
        [SerializeField] private BuildingInfoBox infoBoxPrefab;
        [SerializeField] private Transform boxesRoot;

        [SerializeField] private BuildingContextPanel contextPanel;
        
        [SerializeField] private List<BuildingData> buildingDatas;
        
        private List<BuildingInfoBox> infoBoxes = new List<BuildingInfoBox>();

        private BuildingInfoBox selectedBox;
        
        private void Awake()
        {
            Populate();
        }

        private void Start()
        {
            InputManager.Current.RightMouseClicked += OnRightMouseClicked;
            UIContextManager.Current.Context.BuildingData.Consumed += OnBuildingDataConsumed;
        }

        private void OnBuildingDataConsumed()
        {
            Deselect();
        }

        private void OnRightMouseClicked()
        {
            UIContextManager.Current.Context.SetBuildingData(null);
            Deselect();
        }

        public void Select(BuildingInfoBox box)
        {
            if (selectedBox != null) selectedBox.Deselect();
            selectedBox = box;
            UIContextManager.Current.Context.SetBuildingData(selectedBox.Data);
            contextPanel.ShowInfo(selectedBox.Data);
        }

        public void Deselect()
        {
            if (selectedBox == null) return;
            UIContextManager.Current.Context.SetBuildingData(null);
            selectedBox.Deselect();
            selectedBox = null;
            contextPanel.ClearInfo();
        }

        public void OnInfoBoxPointerEnter(BuildingInfoBox box)
        {
            contextPanel.ShowInfo(box.Data);
        }
        
        public void OnInfoBoxPointerExit()
        {
            if (selectedBox == null) contextPanel.ClearInfo();
        }
        
        private void Populate()
        {
            //TODO: populate with buildings from scriptable objects

            foreach (var data in buildingDatas)
            {
                var box = Instantiate(infoBoxPrefab, boxesRoot);
                box.Initialize(this, data);
                infoBoxes.Add(box);
            }
        }


#if UNITY_EDITOR
        private void OnValidate()
        {
            var guids = AssetDatabase.FindAssets("t:BuildingData");

            buildingDatas = new List<BuildingData>();
            foreach (var guid in guids)
            {
                buildingDatas.Add(AssetDatabase.LoadAssetAtPath<BuildingData>(AssetDatabase.GUIDToAssetPath(guid)));
            }
        }
#endif
    }
}