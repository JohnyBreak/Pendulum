using System.Collections.Generic;

namespace Pendulum.Screens
{
    public class ScreenManager
    {
        private Dictionary<string, IScreen> _screens;

        public ScreenManager(IScreen[] screens)
        {
            _screens = new(screens.Length);
            
            for (int i = 0; i < screens.Length; i++)
            {
                _screens[screens[i].GetName()] = screens[i];
            }
        }

        public void EnableScreen(string screenName)
        {
            foreach (var screen in _screens.Values)
            {
                if (screen.IsActive() == false)
                {
                    continue;
                }

                screen.SetActive(false);
            }

            if (_screens.TryGetValue(screenName, out var screenToActive))
            {
                screenToActive.SetActive(true);
            }
        }
    }
}
