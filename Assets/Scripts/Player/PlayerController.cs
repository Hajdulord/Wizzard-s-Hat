using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using WizzardsHat.UI;

namespace WizzardsHat.Player
{
    public class PlayerController : MonoBehaviour
    {   
        [Header("Assigned Fields")]
        [SerializeField] private Rigidbody2D _rigidbody2D = null!;
        [SerializeField] private UIDocument _scoreUI = null!;
        [SerializeField] private EndScreen _endScreen = null!;
        [Header("Adjustable properties")]
        [SerializeField] private float _jumpForce = 8f;

        private Menu _menu;
        private bool _isEndOfGame;
        private Label _scoreLabel;

        private int _score;

        private void Awake()
        {
            LockConstrains();
            _scoreLabel = _scoreUI.rootVisualElement.Q<Label>("Score");
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

            _scoreUI.rootVisualElement.style.visibility = Visibility.Hidden;
            _endScreen.EndScreenDocument.rootVisualElement.style.visibility = Visibility.Visible;

            _endScreen.ScoreLabel.text = _scoreLabel.text;

            Time.timeScale = 0f;
        }

        private void OnTriggerExit2D(Collider2D col)
        {
            if (col.isTrigger && col.CompareTag("Pipe"))
            {
                _score++;
                _scoreLabel.text = _score.ToString();
            }
        }
    }
}
