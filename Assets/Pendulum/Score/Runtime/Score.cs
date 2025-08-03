namespace Pendulum.ScoreSystem
{
    public class Score
    {
        private int _currentScore;

        public int Current => _currentScore;
        
        public void Add(int addValue)
        {
            if (addValue < 1)
            {
                return;
            }

            _currentScore += addValue;
        }

        public void Reset()
        {
            _currentScore = 0;
        }
    }
}
