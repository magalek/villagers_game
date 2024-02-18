﻿using System;
using Interfaces;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace Items
{
    [CreateAssetMenu(menuName = "Items/New Item", fileName = "Item")]
    public class Item : ScriptableObject, IItem
    {
        [SerializeField, ItemId] private string id;

        [SerializeField] private Sprite sprite;
        
        public string Id => id;
        public Sprite Sprite => sprite;

        public IItem Copy()
        {
            return Instantiate(this);
        }
    }
}