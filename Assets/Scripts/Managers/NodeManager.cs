using Buildings;
using Data.Buildings;
using Nodes;
using UnityEngine;

namespace Managers
{
    public class NodeManager : MonoManager<NodeManager>
    {
        [SerializeField] private BuildingNode buildingNodePrefab;

        public ActionNodeBase CreateNewBuilding(BuildingData data)
        {
            var building = Instantiate(data.buildingNodePrefab);
            building.Initialize(data);
            return building;
        }
    }
}