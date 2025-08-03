namespace Pendulum.Screens.StartScreen
{
    public class StartScreen : IScreen
    {
        private readonly StartScreenView _view;

        public StartScreen(StartScreenView view)
        {
            _view = view;
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
                _view.StartAnimation();
                return;
            }
            
            _view.StopAnimation();
        }
        
        public bool IsActive()
        {
            return _view.gameObject.activeInHierarchy;
        }
        
        public string GetName()
        {
            return ScreenNames.Start;
        }
    }
}
