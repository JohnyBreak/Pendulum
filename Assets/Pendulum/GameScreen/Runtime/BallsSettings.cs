using System;
using UnityEngine;

namespace Pendulum.Screens.GameScreen
{
    [CreateAssetMenu(fileName = "BallsColors", menuName = "Configs/BallsColors")]
    public class BallsSettings : ScriptableObject
    {
        public BallSettings[] Settings;
    }

    [Serializable]
    public class BallSettings
    {
        [SerializeField] private int _matchScore;
        [SerializeField] private Color _color;
        public int MatchScore => _matchScore;
        public Color Color => _color;
    }
}