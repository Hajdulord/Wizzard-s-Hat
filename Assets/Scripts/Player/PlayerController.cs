using UnityEngine;
using UnityEngine.InputSystem;

namespace WizzardsHat.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody2D = null!;
        [SerializeField] private float _jumpForce = 8f;

        private void Awake()
        {
            _rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
        }

        public void OnSpaceHit(InputAction.CallbackContext callback)
        {
            if (callback.started)
            {
                _rigidbody2D.AddForce(CalculateJumpForce(), ForceMode2D.Impulse);
            }
        }

        private Vector2 CalculateJumpForce() => new Vector2(0, _jumpForce - _rigidbody2D.velocity.y);

        public void UnlockConstrains() => _rigidbody2D.constraints = RigidbodyConstraints2D.None;
        public void LockConstrains() => _rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
    }
}
