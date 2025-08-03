using UnityEngine;

namespace Pendulum.Screens.GameScreen
{
    public class BallFactory
    {
        private Transform _ballParent;
        private Ball _ballPrefab;
        private BallSettings[] _settings;
        
        public BallFactory(BallSettings[] settings, Ball prefab, Transform ballParent)
        {
            _ballPrefab = prefab;
            _settings = settings;
            _ballParent = ballParent;
        }
        
        public Ball GetBall()
        {
            //TODO: add object pool
            var index = Random.Range(0, _settings.Length);
            
            var ball = Object.Instantiate(_ballPrefab);
            ball.Init(index, _ballParent, _settings[index].Color);

            return ball;
        }
    }
}