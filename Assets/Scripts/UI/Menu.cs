using System;
using UnityEngine;
using UnityEngine.UIElements;
using WizzardsHat.Player;

namespace WizzardsHat.UI
{
    public class Menu : MonoBehaviour
    {
        [SerializeField] private UIDocument _uiDocument;

        private Button _startButton;
        private Button _exitButton;

        public event Action StartButtonPressed;

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
            StartButtonPressed!.Invoke();
        }

        private void OnExitButtonPressed()
        {
            Application.Quit();
        }
    }
}
