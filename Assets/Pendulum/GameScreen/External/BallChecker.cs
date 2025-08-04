using System;
using System.Collections;
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
        private Coroutine _coroutine;

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

        public void Check()
        {
            var hasBelow = _hasBallBelowPredicate.Invoke();
            if (!hasBelow)
            {
                return;
            }
                
            var colliderResult =
                Physics2D.OverlapBox(transform.position, new Vector2(0.03f, 0.03f), 0, _ballLayer);
            if (colliderResult)
            {
                if (colliderResult.TryGetComponent(out Ball ball))
                {
                    _ball = ball;
                    _setIdCallback?.Invoke(_row, _column, ball.ID);
                }
            }
            else
            {
                _ball = null;
                _setIdCallback?.Invoke(_row, _column, -1);
            }
        }

        // private void OnEnable()
        // {
        //     StopRoutine();
        //     _coroutine = StartCoroutine(CheckRoutine());
        // }
        //
        // private void StopRoutine()
        // {
        //     if (_coroutine != null)
        //     {
        //         StopCoroutine(_coroutine);
        //         _coroutine = null;
        //     }
        // }

        // private void OnDisable()
        // {
        //     StopRoutine();
        // }

        private IEnumerator CheckRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(0.15f);

                Debug.LogError("CheckRoutine()");
                var hasBelow = _hasBallBelowPredicate.Invoke();
                if (!hasBelow)
                {
                    continue;
                }
                
                var colliderResult =
                    Physics2D.OverlapBox(transform.position, new Vector2(0.03f, 0.03f), 0, _ballLayer);
                if (colliderResult)
                {
                    if (colliderResult.TryGetComponent(out Ball ball))
                    {
                        _ball = ball;
                        _setIdCallback?.Invoke(_row, _column, ball.ID);
                    }
                }
                else
                {
                    _ball = null;
                    _setIdCallback?.Invoke(_row, _column, -1);
                }
            }
        }

        // private void OnTriggerEnter2D(Collider2D other)
        // {
        //     var hasBelow = _hasBallBelowPredicate?.Invoke();
        //     if (hasBelow == false)
        //     {
        //         return;
        //     }
        //     
        //     if (((1<<other.gameObject.layer) & _ballLayer) == 0)
        //     {
        //         return;
        //     }
        //
        //     if (other.TryGetComponent(out Ball ball))
        //     {
        //         _ball = ball;
        //         //Debug.LogError($"enter {_row} {_column}");
        //         _setIdCallback?.Invoke(_row, _column, ball.ID);
        //     }
        // }
        //
        // private void OnTriggerExit2D(Collider2D other)
        // {
        //     if (((1<<other.gameObject.layer) & _ballLayer) == 0)
        //     {
        //         return;
        //     }
        //     
        //     if (other.TryGetComponent(out Ball ball))
        //     {
        //         _ball = null;
        //         //Debug.LogError($"exit {_row} {_column}");
        //         _setIdCallback?.Invoke(_row, _column, -1);
        //     }
        // }
    }
}