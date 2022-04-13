using UnityEngine;
using UnityEngine.InputSystem;
using WizzardsHat.UI;

namespace WizzardsHat.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody2D = null!;
        [SerializeField] private float _jumpForce = 8f;

        private Menu _menu;
        private bool _isEndOfGame;

        public int Score { get; private set; }

        private void Awake()
        {
            LockConstrains();
            _menu = FindObjectOfType<Menu>();
            _menu.StartButtonPressed += UnlockConstrains;
        }

        public void OnSpaceHit(InputAction.CallbackContext callback)
        {
            if (callback.started && !_isEndOfGame)
            {
                _rigidbody2D.AddForce(CalculateJumpForce(), ForceMode2D.Impulse);
            }
        }

        private Vector2 CalculateJumpForce() => new Vector2(0, _jumpForce - _rigidbody2D.velocity.y);

        private void UnlockConstrains() => _rigidbody2D.constraints = RigidbodyConstraints2D.None;
        private void LockConstrains() => _rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;

        private void OnDestroy()
        {
            _menu.StartButtonPressed -= UnlockConstrains;
        }
        
        private void OnCollisionEnter2D()
        {
            _isEndOfGame = true;
            Destroy(_rigidbody2D);
        }

        private void OnTriggerExit2D(Collider2D col)
        {
            if (col.isTrigger && col.CompareTag("Pipe"))
            {
                Score++;
            }
        }
    }
}
