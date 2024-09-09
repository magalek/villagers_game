using System.Collections.Generic;
using Buildings;
using Items;
using UnityEngine;
using UnityEngine.Serialization;

namespace Data.Buildings
{
    [CreateAssetMenu(fileName = "Building", menuName = "Data/Buildings/New Building")]
    public class BuildingData : ScriptableObject
    {
        public Sprite icon;
        [FormerlySerializedAs("buildingPrefab")] public BuildingNode buildingNodePrefab;
        [TextArea] public string description;
        public List<ContainerEntry> requiredItems;
    }
}