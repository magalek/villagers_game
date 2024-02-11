using System;
using UnityEngine;

namespace Entities
{
    public class ActionProgress : IActionProgress
    {
        public event Action Ended;

        public float PercentageNormalized => Mathf.Clamp01(percentage);
        public bool IsCompleted { get; private set; }

        private float percentage;

        public void Update(float value)
        {
            if (IsCompleted) return;
            
            percentage = value;
            if (percentage >= 1)
            {
                Complete();
            }
        }

        public void Complete()
        {
            percentage = 1;
            Ended?.Invoke();
            IsCompleted = true;
        }
    }
}