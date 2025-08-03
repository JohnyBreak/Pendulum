using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Pendulum.Screens.GameScreen
{
    public class TouchDetector : MonoBehaviour, IPointerClickHandler
    {
        public event Action ClickEvent;
        
        public void OnPointerClick(PointerEventData eventData)
        {
            ClickEvent?.Invoke();
        }
    }
}
