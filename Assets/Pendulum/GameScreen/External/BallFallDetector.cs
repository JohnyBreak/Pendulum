using System;
using UnityEngine;

namespace Pendulum.Screens.GameScreen
{
    public class BallFallDetector : MonoBehaviour
    {
        [SerializeField] private LayerMask _ballLayer;
        private Action _onFallCallback;
        
        public void Init(Action onFallCallback)
        {
            if (onFallCallback == null)
            {
                Debug.LogError("onFallCallback is null");
            }
            
            _onFallCallback = onFallCallback;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (((1<<other.gameObject.layer) & _ballLayer) == 0)
            {
                return;
            }

            if (other.TryGetComponent(out Ball ball))
            {
                _onFallCallback?.Invoke();
            }
        }
    }
}