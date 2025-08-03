using UnityEngine;

namespace Pendulum.Screens.GameScreen
{
    public class BallDestroyer : MonoBehaviour
    {
        [SerializeField] private LayerMask _ballLayer;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (((1<<other.gameObject.layer) & _ballLayer) == 0)
            {
                return;
            }
            
            if (other.TryGetComponent(out Ball ball))
            {
                Destroy(ball.gameObject);
            }
        }
    }
}