using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Pendulum.Screens.StartScreen
{
    public class StartScreenAnimationBehaviour : MonoBehaviour
    {
        [SerializeField] private Transform _castle;
        [SerializeField] private Transform _startT;
        [SerializeField] private Transform _endT;
        [SerializeField] private Transform _star;
        [SerializeField] private Transform _text;
        
        private Vector3 _startPosition;
        private Vector3 _endPosition;
        private Sequence _sequence;

        private IEnumerator Start()
        {
            yield return null;
            
            _castle.localScale = Vector3.one;
            _endPosition = _endT.position;
            _startPosition = _startT.position;
            _castle.localScale = Vector3.zero;
        }

        public void StartAnimation()
        {
            _castle.localScale = Vector3.zero;
            _star.localScale = Vector3.zero;
            _star.position = _startT.position;
            _text.localScale = Vector3.zero;
            
            if (_sequence != null)
            {
                StopAnimation();
            }

            _sequence = DOTween.Sequence();
            _sequence.AppendCallback(() => _castle.localScale = Vector3.zero);
            _sequence.AppendCallback(() => _star.localScale = Vector3.zero);
            _sequence.AppendCallback(() => _text.localScale = Vector3.zero);
            _sequence.Append(_castle.DOScale(Vector3.one, 1).SetEase(Ease.Linear));
            _sequence.AppendCallback(() => _star.position = _startPosition);
            _sequence.Append(_star.DOJump(_endPosition, 500, 1, 2).SetEase(Ease.Linear));
            _sequence.Join(_star.DOScale(Vector3.one, 0.3f).SetEase(Ease.Linear));
            _sequence.Append(_text.DOScale(Vector3.one, 2).SetEase(Ease.Linear));
            _sequence.Join(_star.DOScale(Vector3.zero, 0.3f).SetEase(Ease.Linear));
            _sequence.AppendInterval(1);
            _sequence.Append(_castle.DOScale(Vector3.zero, 1).SetEase(Ease.Linear));
            _sequence.SetLoops(-1);
            
            _sequence.Play();
        }

        public void StopAnimation()
        {
            if (_sequence != null)
            {
                _sequence.Kill();
                _sequence = null;
            }
        }
        
#if UNITY_EDITOR
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                StartAnimation();
            }
        }
#endif
    }
}