using System;
using System.Collections;
using System.Collections.Generic;
using Entities;
using Interfaces;
using Managers;
using UnityEngine;
using Utility;

namespace Buildings
{
    public class GatheringBuilding : MonoBehaviour, IInteractable<Villager>
    {
        private HashSet<Villager> currentVillagers = new HashSet<Villager>();


        private void Start()
        {
            ManagerLoader.Get<EconomyManager>().AddValue(10);
        }

        private void Update()
        {
            // UpdateStatus();
        }

        private void UpdateStatus()
        {
            foreach (var villager in currentVillagers)
            {
                ManagerLoader.Get<EconomyManager>().AddValue(villager.gatheringSpeed);
            }
        }

        public bool CanInteract(Villager entity)
        {
            return true;
        }

        public void Interact(Villager villager)
        {
            if (!currentVillagers.Add(villager)) return;
            
            
        }
    }
}