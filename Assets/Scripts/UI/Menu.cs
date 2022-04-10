using UnityEngine;
using UnityEngine.UIElements;
using WizzardsHat.Player;

namespace WizzardsHat.UI
{
    public class Menu : MonoBehaviour
    {
        [SerializeField] private UIDocument _uiDocument;
        [SerializeField] private PlayerController _player;
        
        private Button _startButton;
        private Button _exitButton;

        private void Awake()
        {
            _startButton = _uiDocument.rootVisualElement.Q<Button>("Start");
            _exitButton = _uiDocument.rootVisualElement.Q<Button>("Exit");

            _startButton.clicked += OnStartButtonPressed;
            _exitButton.clicked += OnExitButtonPressed;
        }

        private void OnStartButtonPressed()
        {
            _uiDocument.rootVisualElement.style.visibility = Visibility.Hidden;
            _player.UnlockConstrains();
        }

        private void OnExitButtonPressed()
        {
            Application.Quit();
        }
    }
}
