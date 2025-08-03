using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Pendulum.Screens.ScoreScreen
{
    public class ScoreScreenView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private Button _retryButton;
        [SerializeField] private Button _goToStartButton;

        private Action _retryCallback;
        private Action _startCallback;
        
        public void Init(Action retryCallback, Action startCallback)
        {
            _retryCallback = retryCallback;
            _startCallback = startCallback;
        }

        public void SetScore(int currentScore)
        {
            if (_scoreText == null)
            {
                return;
            }

            _scoreText.text = $"Score: {currentScore}";
        }
        
        private void Awake()
        {
            _retryButton.onClick.AddListener(OnRetryClick);
            _goToStartButton.onClick.AddListener(OnStartClick);
        }

        private void OnRetryClick()
        {
            _retryCallback?.Invoke();
        }

        private void OnStartClick()
        {
            _startCallback?.Invoke();
        }
        
        private void OnDestroy()
        {
            _retryButton.onClick.RemoveListener(OnRetryClick);
            _goToStartButton.onClick.RemoveListener(OnStartClick);
        }
    }
}
