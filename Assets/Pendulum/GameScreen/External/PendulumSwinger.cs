using System;
using UnityEngine;

namespace Pendulum.Screens.GameScreen
{
    public class PendulumSwinger : MonoBehaviour
    {
        [SerializeField] private Transform _root;
        [SerializeField] private Transform _ballParent;
        [SerializeField] private float _swingAngle = 30f;
        [SerializeField] private float _swingSpeed = 1f;

        private BallFactory _factory;
        private Ball _ball;
        private int _sign;
        private float _currentAngle;
        private float _prevAngle;
        private float _time;
        private float _angularForce;
        private bool _needToTurn;

        public Transform BallParent => _ballParent;

        public void Init(BallFactory factory)
        {
            _factory = factory;
            SpawnBall();
        }

        public void OnFallCallback()
        {
            SpawnBall();
        }

        private void SpawnBall()
        {
            _ball = _factory.GetBall();
        }

        public void DropBall()
        {
            if (!_ball)
            {
                return;
            }

            _ball.Unparent(_angularForce, _needToTurn);
            _ball = null;
        }

        public void Toggle(bool isActive)
        {
            ResetValues();
            gameObject.SetActive(isActive);
        }

        private void ResetValues()
        {
            _currentAngle = 0f;
            _time = 0f;
            _angularForce = 0f;
        }

        private void Update()
        {
            _time += Time.deltaTime * _swingSpeed;
            _currentAngle = _swingAngle * Mathf.Sin(_time);
            _root.rotation = Quaternion.Euler(0, 0, _currentAngle);

            _sign = Math.Sign(_currentAngle);
            
            _angularForce = Mathf.Clamp01(
                Mathf.InverseLerp(0.5f * _sign, 0, _root.rotation.z));
            
            _needToTurn = _prevAngle > _currentAngle;
            _prevAngle = _currentAngle;
        }
    }
}
