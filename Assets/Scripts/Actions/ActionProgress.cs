using Entities;
using UnityEngine;

namespace Actions
{
    public class ActionProgress : IActionProgress
    {
        public event System.Action Ended;

        public float PercentageNormalized => Mathf.Clamp01(percentage);
        public bool IsCompleted { get; private set; }
        public bool IsMax => PercentageNormalized == 1;

        private float percentage;

        public void Update(float value, bool completeOnClamp = true)
        {
            if (IsCompleted) return;
            
            percentage = value;
            if (percentage >= 1 && completeOnClamp)
            {
                Complete();
            }
        }

        public void Reset()
        {
            percentage = 0;
        }

        public void Clamp()
        {
            percentage = 1;
        }
        
        public void Complete()
        {
            percentage = 1;
            Ended?.Invoke();
            IsCompleted = true;
        }
    }
}