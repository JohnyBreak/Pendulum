using System;
using UnityEngine;
using UnityEngine.UI;

namespace Pendulum.Screens.StartScreen
{
    public class StartScreenView : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private StartScreenAnimationBehaviour _animationBehaviour;
        
        private Action _buttonCallback;
        
        public void Init(Action buttonCallback)
        {
            _buttonCallback = buttonCallback;
        }

        public void StartAnimation()
        {
            _animationBehaviour?.StartAnimation();
        }

        public void StopAnimation()
        {
            _animationBehaviour?.StopAnimation();
        }

        private void Awake()
        {
            _button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            _buttonCallback?.Invoke();
        }


        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnClick);
        }
    }
}
