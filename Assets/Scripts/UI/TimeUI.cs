using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class TimeUI : MonoBehaviour
    {
        [SerializeField] private Button stopButton;
        [SerializeField] private Button normalTimeButton;
        [SerializeField] private Button fastTimeButton;

        private TimeButton lastClicked = TimeButton.Normal;
        private TimeButton currentClicked = TimeButton.Normal;
        
        private void Awake()
        {
            stopButton.onClick.AddListener(OnStopButtonClicked);
            normalTimeButton.onClick.AddListener(OnNormalTimeButtonClicked);
            fastTimeButton.onClick.AddListener(OnFastTimeButtonClicked);
        }

        private void OnStopButtonClicked() => ChangeTimeScale(TimeButton.Stop);

        private void OnNormalTimeButtonClicked() => ChangeTimeScale(TimeButton.Normal);

        private void OnFastTimeButtonClicked() => ChangeTimeScale(TimeButton.Fast);

        private void ChangeTimeScale(TimeButton newScale)
        {
            lastClicked = currentClicked;
            Time.timeScale = newScale switch
            {
                TimeButton.Stop => 0,
                TimeButton.Normal => 1,
                TimeButton.Fast => 2,
                _ => Time.timeScale
            };
            currentClicked = newScale;
        }
        
        private enum TimeButton
        {
            Stop,
            Normal,
            Fast
        }
    }
}