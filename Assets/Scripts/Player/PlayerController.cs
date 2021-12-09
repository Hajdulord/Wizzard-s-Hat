using UnityEngine;
using UnityEngine.InputSystem;

namespace WizzardsHat.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody2D = null!;
        [SerializeField] private float _jumpForce = 8f;

        public void OnSpaceHit(InputAction.CallbackContext callback)
        {
            if (callback.started)
            {
                _rigidbody2D.AddForce(CalculateJumpForce(), ForceMode2D.Impulse);
            }
        }

        private Vector2 CalculateJumpForce() => new Vector2(0, _jumpForce - _rigidbody2D.velocity.y);
    }
}
