using UnityEngine;

namespace Pendulum.Screens.GameScreen
{
    public class Ball : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private float _forceMultiplier;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private int _id;

        public int ID => _id;
        
        public void Init(int id, Transform parent, Color color)
        {
            _id = id;
            transform.parent = parent;
            _rigidbody.isKinematic = true;
            _rigidbody.velocity = Vector2.zero;
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.Euler(Vector3.zero);
            _spriteRenderer.color = color;
        }
        
        public void Unparent(float angularForce, bool needToTurn)
        {
            transform.parent = null;
            _rigidbody.isKinematic = false;
            
            transform.rotation = Quaternion.Euler(0,needToTurn? 180 : 0 ,0);
            
            _rigidbody.velocity = angularForce * _forceMultiplier * transform.right;
        }
    }
}