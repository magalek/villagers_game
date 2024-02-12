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

        private void Awake()
        {
            stopButton.onClick.AddListener(OnStopButtonClicked);
            normalTimeButton.onClick.AddListener(OnNormalTimeButtonClicked);
            fastTimeButton.onClick.AddListener(OnFastTimeButtonClicked);
        }

        private void OnStopButtonClicked()
        {
            Time.timeScale = 0;
        }
        
        private void OnNormalTimeButtonClicked()
        {
            Time.timeScale = 1;
        }
        
        private void OnFastTimeButtonClicked()
        {
            Time.timeScale = 2;
        }
    }
}