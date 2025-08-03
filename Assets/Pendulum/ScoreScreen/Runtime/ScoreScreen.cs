using Pendulum.ScoreSystem;

namespace Pendulum.Screens.ScoreScreen
{
    public class ScoreScreen : IScreen
    {
        private readonly ScoreScreenView _view;
        private readonly Score _score;

        public ScoreScreen(ScoreScreenView view, Score score)
        {
            _view = view;
            _score = score;
        }

        public void SetActive(bool isActive)
        {
            if (!_view)
            {
                return;
            }
            
            _view.gameObject.SetActive(isActive);
            
            if (isActive)
            {
                _view.SetScore(_score.Current);
                return;
            }
            
            _score.Reset();
        }
        
        public bool IsActive()
        {
            return _view.gameObject.activeInHierarchy;
        }
        
        public string GetName()
        {
            return ScreenNames.Score;
        }
    }
}