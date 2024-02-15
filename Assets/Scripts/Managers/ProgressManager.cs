using System;
using System.Collections.Generic;
using Actions;
using UnityEngine;

namespace Managers
{
    public class ProgressManager : MonoManager<ProgressManager>
    {
        [SerializeField] private ProgressBar progressBarPrefab;
        [SerializeField] private Transform canvasParent;

        private Dictionary<ActionQueue, ProgressBar> actionQueues = new Dictionary<ActionQueue, ProgressBar>();

        private void Update()
        {
            foreach (var pair in actionQueues)
            {
                if (pair.Key.currentAction != null)
                {
                    if (!pair.Key.currentAction.ShowProgress || pair.Key.currentAction.Progress.IsCompleted) pair.Value.HideBar();
                    else pair.Value.UpdateBar(pair.Key.currentAction.Progress.PercentageNormalized);
                }
            }
        }

        public void RegisterProgressBar(ActionQueue actionQueue, Transform parent)
        {
            var progressBar = Instantiate(progressBarPrefab, canvasParent);
            progressBar.Initialize(parent);
            actionQueues[actionQueue] = progressBar;
        }
    }
}