using System;
using Data.Buildings;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(BuildingData))]
    public class BuildingDataDrawer : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
        }
    }
}