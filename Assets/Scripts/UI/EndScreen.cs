using UnityEngine;
using UnityEngine.UIElements;

namespace WizzardsHat.UI
{
    public class EndScreen : MonoBehaviour
    {
        [SerializeField] private UIDocument _uiDocument;

        private Button _restartButton;
        private Button _exitButton;
        
        public Label ScoreLabel { get; private set; }
        public UIDocument EndScreenDocument => _uiDocument;
        
        private void Awake()
        {
            _restartButton = _uiDocument.rootVisualElement.Q<Button>("Restart");
            _exitButton = _uiDocument.rootVisualElement.Q<Button>("Exit");
            ScoreLabel = _uiDocument.rootVisualElement.Q<Label>("Score");

            _restartButton.clicked += OnStartButtonPressed;
            _exitButton.clicked += OnExitButtonPressed;
            
            _uiDocument.rootVisualElement.style.visibility = Visibility.Hidden;
        }

        private void OnStartButtonPressed()
        {
            _uiDocument.rootVisualElement.style.visibility = Visibility.Hidden;
        }

        private void OnExitButtonPressed()
        {
            Application.Quit();
        }

        private void OnDestroy()
        {
            _restartButton.clicked -= OnStartButtonPressed;
            _exitButton.clicked -= OnExitButtonPressed;
        }
    }
}