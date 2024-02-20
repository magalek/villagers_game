using Buildings;
using UnityEngine;
using UnityEngine.Serialization;

namespace Data.Buildings
{
    [CreateAssetMenu(fileName = "Building", menuName = "Data/Buildings/New Building")]
    public class BuildingData : ScriptableObject
    {
        public Sprite icon;
        public Building buildingPrefab;
        [TextArea] public string description;
    }
}