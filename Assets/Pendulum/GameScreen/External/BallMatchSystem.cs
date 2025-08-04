using System;
using System.Collections;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Pendulum.Screens.GameScreen
{
    public class BallMatchSystem
    {
        private const int Empty = -1;
        private const int Rows = 3;
        private const int Columns = 3;

        private Coroutine _coroutine;
        private MonoBehaviour _coroutineRunner;
        private BallChecker[] _checkers;
        private Action _gameOverCallback;
        private Action<int> _onMatch;

        private int[,] _ballsIds = new int[,]
        {
            { Empty, Empty, Empty },
            { Empty, Empty, Empty },
            { Empty, Empty, Empty }
        };
        
        public BallMatchSystem(BallChecker[] checkers)
        {
            _checkers = checkers;
            if (IsCheckersArrayValid() == false)
            {
                Debug.LogError("Checkers array is not valid");
                return;
            }

            InitCheckers();
        }

        public void Init(
            Action<int> onMatch, 
            Action gameOverCallback, 
            MonoBehaviour coroutineRunner)
        {
            if (onMatch == null)
            {
                Debug.LogError("onMatch is null");
            }
            
            if (gameOverCallback == null)
            {
                Debug.LogError("gameOverCallback is null");
            }

            _onMatch = onMatch;
            _gameOverCallback = gameOverCallback;
            _coroutineRunner = coroutineRunner;
        }

        public void Toggle(bool isActive)
        {
            if (isActive)
            {
                _coroutineRunner.StopAllCoroutines();
                _coroutine = _coroutineRunner.StartCoroutine(CheckRoutine());
            }
            else
            {
                _coroutineRunner.StopAllCoroutines();
                _coroutine = null;
            }
        }

        private void InitCheckers()
        {
            _checkers[0].Init(0, 0, SetIndex, () => _checkers[3].HasBall);
            _checkers[1].Init(0, 1, SetIndex, () => _checkers[4].HasBall);
            _checkers[2].Init(0, 2, SetIndex, () => _checkers[5].HasBall);
            _checkers[3].Init(1, 0, SetIndex, () => _checkers[6].HasBall);
            _checkers[4].Init(1, 1, SetIndex, () => _checkers[7].HasBall);
            _checkers[5].Init(1, 2, SetIndex, () => _checkers[8].HasBall);
            _checkers[6].Init(2, 0, SetIndex, () => true);
            _checkers[7].Init(2, 1, SetIndex, () => true);
            _checkers[8].Init(2, 2, SetIndex, () => true);
        }

        private bool IsCheckersArrayValid()
        {
            int total = 0;
            foreach (var ballChecker in _checkers)
            {
                if (!ballChecker)
                {
                    continue;
                }

                total++;
            }

            return total == Columns * Rows;
        }

        private void SetIndex(int row, int column, int id)
        {
            _ballsIds[row, column] = id;
            
            // if (id == Empty)
            // {
            //     return;
            // }
            
            //LogArray();
            //CheckMatch(row, column);
        }

        private IEnumerator CheckRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(0.2f);
                CheckAll();
            }
        }

        private void CheckAll()
        {
            foreach (var checker in _checkers)
            {
                checker.Check();
            }
            
            if (_ballsIds[2, 0] != Empty
                && _ballsIds[2, 1] == _ballsIds[2, 0]
                && _ballsIds[2, 2] == _ballsIds[2, 0])
            {
                _onMatch?.Invoke(_ballsIds[2, 0]);
                RemoveBallAt(2, 0);
                RemoveBallAt(2, 1);
                RemoveBallAt(2, 2);
            }
            
            if (_ballsIds[1, 0] != Empty
                && _ballsIds[1, 1] == _ballsIds[1, 0]
                && _ballsIds[1, 2] == _ballsIds[1, 0])
            {
                _onMatch?.Invoke(_ballsIds[1, 0]);
                RemoveBallAt(1, 0);
                RemoveBallAt(1, 1);
                RemoveBallAt(1, 2);
            }
            
            if (_ballsIds[0, 0] != Empty
                && _ballsIds[0, 1] == _ballsIds[0, 0]
                && _ballsIds[0, 2] == _ballsIds[0, 0])
            {
                _onMatch?.Invoke(_ballsIds[0, 0]);
                RemoveBallAt(0, 0);
                RemoveBallAt(0, 1);
                RemoveBallAt(0, 2);
            }
            
            if (_ballsIds[0, 0] != Empty
                && _ballsIds[1, 0] == _ballsIds[0, 0]
                && _ballsIds[2, 0] == _ballsIds[0, 0])
            {
                _onMatch?.Invoke(_ballsIds[0, 0]);
                RemoveBallAt(0, 0);
                RemoveBallAt(1, 0);
                RemoveBallAt(2, 0);
            }
            
            if (_ballsIds[0, 1] != Empty
                && _ballsIds[1, 1] == _ballsIds[0, 1]
                && _ballsIds[2, 1] == _ballsIds[0, 1])
            {
                _onMatch?.Invoke(_ballsIds[0, 1]);
                RemoveBallAt( 0, 1);
                RemoveBallAt( 1, 1);
                RemoveBallAt( 2, 1);
            }
            
            if (_ballsIds[0, 2] != Empty
                && _ballsIds[1, 2] == _ballsIds[0, 2]
                && _ballsIds[2, 2] == _ballsIds[0, 2])
            {
                _onMatch?.Invoke(_ballsIds[0, 2]);
                RemoveBallAt( 0, 2);
                RemoveBallAt( 1, 2);
                RemoveBallAt( 2, 2);
            }
            
            if (_ballsIds[0, 0] != Empty
                && _ballsIds[1, 1] == _ballsIds[0, 0]
                && _ballsIds[2, 2] == _ballsIds[0, 0])
            {
                // diagonal
                _onMatch?.Invoke(_ballsIds[0, 0]);
                RemoveBallAt(0, 0);
                RemoveBallAt(1, 1);
                RemoveBallAt(2, 2);
            }

            if (_ballsIds[0, 2] != Empty
                && _ballsIds[1, 1] == _ballsIds[0, 2]
                && _ballsIds[2, 0] == _ballsIds[0, 2])
            {
                // diagonal
                _onMatch?.Invoke(_ballsIds[0, 2]);
                RemoveBallAt(0, 2);
                RemoveBallAt(1, 1);
                RemoveBallAt(2, 0);
            }
            
            bool hasEmptySpace = false;
            foreach (var id in _ballsIds)
            {
                if (id == Empty)
                {
                    hasEmptySpace = true;
                    break;
                }
            }

            if (hasEmptySpace)
            {
                return;
            }
            
            foreach (var ballChecker in _checkers)
            {
                Object.Destroy(ballChecker.Ball.gameObject);
            }
            
            _gameOverCallback?.Invoke();
        }

        private void CheckMatch(int row, int column)
        {
            var ballID = _ballsIds[row, column];

            if (ballID == Empty)
            {
                return;
            }

            // if match => remove balls, add score
            
            if (_ballsIds[row, 0] == ballID
                && _ballsIds[row, 1] == ballID
                && _ballsIds[row, 2] == ballID)
            {
                // horizontal
                
                _onMatch?.Invoke(ballID);
                RemoveBallAt(row, 0);
                RemoveBallAt(row, 1);
                RemoveBallAt(row, 2);
            }
            
            if (_ballsIds[0, column] == ballID
                && _ballsIds[1, column] == ballID
                && _ballsIds[2, column] == ballID)
            {
                _onMatch?.Invoke(ballID);
                RemoveBallAt(0, column);
                RemoveBallAt(1, column);
                RemoveBallAt(2, column);
                // vertical
            }

            if (_ballsIds[0, 0] == ballID
                && _ballsIds[1, 1] == ballID
                && _ballsIds[2, 2] == ballID)
            {
                // diagonal
                _onMatch?.Invoke(ballID);
                RemoveBallAt(0, 0);
                RemoveBallAt(1, 1);
                RemoveBallAt(2, 2);
            }

            if (_ballsIds[0, 2] == ballID
                && _ballsIds[1, 1] == ballID
                && _ballsIds[2, 0] == ballID)
            {
                // diagonal
                _onMatch?.Invoke(ballID);
                RemoveBallAt(0, 2);
                RemoveBallAt(1, 1);
                RemoveBallAt(2, 0);
            }
            
            bool hasEmptySpace = false;
            foreach (var id in _ballsIds)
            {
                if (id == Empty)
                {
                    hasEmptySpace = true;
                    break;
                }
            }

            if (hasEmptySpace)
            {
                return;
            }
            
            foreach (var ballChecker in _checkers)
            {
                Object.Destroy(ballChecker.Ball.gameObject);
            }
            
            _gameOverCallback?.Invoke();
        }

        private void RemoveBallAt(int row, int column)
        {
            var index = (row * Columns) + column;
            if (_checkers[index].HasBall)// TODO: return to pool
            {
                Object.Destroy(_checkers[index].Ball.gameObject);    
            }
        }

        private void LogArray()
        {
            Debug.LogError($"|{_ballsIds[0,0]}| |{_ballsIds[0,1]}| |{_ballsIds[0,2]}|\n" +
                           $"|{_ballsIds[1,0]}| |{_ballsIds[1,1]}| |{_ballsIds[1,2]}|\n" +
                           $"|{_ballsIds[2,0]}| |{_ballsIds[2,1]}| |{_ballsIds[2,2]}|");
        }
    }
}