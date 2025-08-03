using System;
using Pendulum.ScoreSystem;

namespace Pendulum.Screens.GameScreen
{
    public class GameScreen : IScreen
    {
        private readonly GameScreenView _view;
        private readonly Score _score;
        private readonly PendulumSwinger _pendulumSwinger;
        private readonly BallMatchSystem _ballMatchSystem;
        private readonly Action _gameOverCallback;
        private readonly BallSettings[] _settings;
        
        public GameScreen(
            GameScreenView view,
            Score score, 
            PendulumSwinger pendulumSwinger,
            BallMatchSystem ballMatchSystem,
            BallSettings[] settings,
            Action gameOverCallback)
        {
            _view = view;
            _score = score;
            _settings = settings;
            _pendulumSwinger = pendulumSwinger;
            _ballMatchSystem = ballMatchSystem;
            _gameOverCallback = gameOverCallback;

            _ballMatchSystem.Init(OnMatch, OnGameOver);
            _pendulumSwinger.Toggle(false);
        }

        public bool IsActive()
        {
            return _view.gameObject.activeInHierarchy;
        }

        public void SetActive(bool isActive)
        {
            if (!_view)
            {
                return;
            }
            
            _view.gameObject.SetActive(isActive);
            _view.ToggleTouchDetector(isActive);
            
            _pendulumSwinger.Toggle(isActive);
        }

        public void OnClick()
        {
            _pendulumSwinger.DropBall();
        }

        private void OnMatch(int ballId)
        {
            _score.Add(_settings[ballId].MatchScore);
        }

        private void OnGameOver()
        {
            _view.ToggleTouchDetector(false);
            _gameOverCallback?.Invoke();
        }

        public string GetName()
        {
            return ScreenNames.Game;
        }
    }
}