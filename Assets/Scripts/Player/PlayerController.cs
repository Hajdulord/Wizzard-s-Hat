using System;
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
        private Label _scoreLabel;
        private bool _isEndOfGame;
        private int _score;

        public event Action GameEndAction;

        private void Awake()
        {
            LockConstrains();
            _scoreLabel = _scoreUI.rootVisualElement.Q<Label>("Score");
            _menu = FindObjectOfType<Menu>();
            _menu.StartButtonPressed += UnlockConstrains;
            _endScreen.RestartButton.clicked += OnRestart;
        }

        public void OnSpaceHit(InputAction.CallbackContext callback)
        {
            if (callback.started && !_isEndOfGame)
            {
                _rigidbody2D.AddForce(CalculateJumpForce(), ForceMode2D.Impulse);
            }
        }

        private Vector2 CalculateJumpForce() => new(0, _jumpForce - _rigidbody2D.velocity.y);

        private void UnlockConstrains() => _rigidbody2D.constraints = RigidbodyConstraints2D.None;
        private void LockConstrains() => _rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;

        private void OnRestart()
        {
            transform.position = Vector3.zero;
            _rigidbody2D = gameObject.AddComponent<Rigidbody2D>();
            _rigidbody2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
            _rigidbody2D.sleepMode = RigidbodySleepMode2D.NeverSleep;
            _score = 0;
            _scoreLabel.text = _score.ToString();
            _isEndOfGame = false;
            _scoreUI.rootVisualElement.style.visibility = Visibility.Visible;
        }
        private void OnDestroy()
        {
            _menu.StartButtonPressed -= UnlockConstrains;
            _endScreen.RestartButton.clicked -= OnRestart;
        }
        
        private void OnCollisionEnter2D()
        {
            _isEndOfGame = true;
            GameEndAction!.Invoke();
            Destroy(_rigidbody2D);

            _scoreUI.rootVisualElement.style.visibility = Visibility.Hidden;
            _endScreen.EndScreenDocument.rootVisualElement.style.visibility = Visibility.Visible;

            _endScreen.ScoreLabel.text = _scoreLabel.text;
        }

        private void OnTriggerExit2D(Collider2D col)
        {
            if (col.isTrigger && col.CompareTag("Pipe") && !_isEndOfGame)
            {
                _score++;
                _scoreLabel.text = _score.ToString();
            }
        }

    }
}
