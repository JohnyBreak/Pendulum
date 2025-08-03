using System;
using UnityEngine;

namespace Pendulum.Screens.GameScreen
{
    public class BallChecker : MonoBehaviour
    {
        [SerializeField] private LayerMask _ballLayer;
        
        private int _row;
        private int _column;
        private Action<int, int, int> _setIdCallback;
        private Func<bool> _hasBallBelowPredicate;
        private Ball _ball;
        public bool HasBall => _ball;
        public Ball Ball => _ball;

        public void Init(
            int row, 
            int column, 
            Action<int, int, int> setIdCallback, 
            Func<bool> hasBallBelowPredicate)
        {
            _row = row;
            _column = column;

            if (setIdCallback == null)
            {
                Debug.LogError("setIdCallback in BallChecker is null");
                return;
            }

            _setIdCallback = setIdCallback;
            
            if (hasBallBelowPredicate == null)
            {
                Debug.LogError("hasBallBelowPredicate in BallChecker is null");
                return;
            }
            
            _hasBallBelowPredicate = hasBallBelowPredicate;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var hasBelow = _hasBallBelowPredicate?.Invoke();
            if (hasBelow == false)
            {
                return;
            }
            
            if (((1<<other.gameObject.layer) & _ballLayer) == 0)
            {
                return;
            }

            if (other.TryGetComponent(out Ball ball))
            {
                _ball = ball;
                _setIdCallback?.Invoke(_row, _column, ball.ID);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (((1<<other.gameObject.layer) & _ballLayer) == 0)
            {
                return;
            }
            
            if (other.TryGetComponent(out Ball ball))
            {
                _ball = null;
                _setIdCallback?.Invoke(_row, _column, -1);
            }
        }
    }
}