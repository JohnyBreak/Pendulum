using System;
using UnityEngine;

namespace Pendulum.Screens.GameScreen
{
    public class GameScreenView : MonoBehaviour
    {
        [SerializeField] private TouchDetector _touchDetector;
        private Action _clickCallback;
        
        public void Init(Action clickCallback)
        {
            _clickCallback = clickCallback;
            _touchDetector.ClickEvent += OnClick;
        }

        public void ToggleTouchDetector(bool isActive)
        {
            _touchDetector.gameObject.SetActive(isActive);
        }

        private void OnClick()
        {
            _clickCallback?.Invoke();
        }

        private void OnDestroy()
        {
            _touchDetector.ClickEvent -= OnClick;
        }
    }
}
