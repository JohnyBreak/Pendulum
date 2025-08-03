using System.Collections;
using Pendulum.ScoreSystem;
using UnityEngine;
using Pendulum.Screens;
using Pendulum.Screens.StartScreen;
using Pendulum.Screens.GameScreen;
using Pendulum.Screens.ScoreScreen;
using Unity.VisualScripting;

namespace Pendulum.Bootstrap
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private StartScreenView _startScreenView;
        [SerializeField] private GameScreenView _gameScreenView;
        [SerializeField] private ScoreScreenView _scoreScreenView;
        [SerializeField] private PendulumSwinger _pendulumSwinger;
        [SerializeField] private Ball _ballPrefab;
        [SerializeField] private BallsSettings ballsSettings;
        [SerializeField] private BallChecker[] _checkers;
        [SerializeField] private BallFallDetector _fallDetector;
        
        private ScreenManager _screenManager;
        
        private void Awake()
        {
            var score = new Score();
            
            _startScreenView.Init(
                () => _screenManager.EnableScreen(ScreenNames.Game));
            
            _scoreScreenView.Init(
                () => _screenManager.EnableScreen(ScreenNames.Game),
                () => _screenManager.EnableScreen(ScreenNames.Start));

            
            _pendulumSwinger.Init(
                new BallFactory(
                    ballsSettings.Settings, 
                    _ballPrefab, 
                    _pendulumSwinger.BallParent));

            BallMatchSystem matchSystem = new BallMatchSystem(
                _checkers);

            _fallDetector.Init(_pendulumSwinger.OnFallCallback);
            
            var gameScreen = new GameScreen(_gameScreenView, score,
                _pendulumSwinger,
                matchSystem,
                () => _screenManager.EnableScreen(ScreenNames.Score));
            
            _gameScreenView.Init(gameScreen.OnClick);
            
            _screenManager = new ScreenManager(new IScreen[]
                {
                    new StartScreen(_startScreenView),
                    gameScreen,
                    new ScoreScreen(_scoreScreenView, score)
                });
        }

        private IEnumerator Start()
        {
            yield return null;
            
            _screenManager.EnableScreen(ScreenNames.Start);
        }
    }
}
