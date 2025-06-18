using UnityEngine;
using TMPro;
using UnityEngine.Events;
using KingdomOfLinux.Characters;

namespace KingdomOfLinux.Controllers
{
    public class TerminalController : MonoBehaviour
    {
        public static TerminalController Instance;

        [SerializeField] private GameObject terminalPanel;
        [SerializeField] private TMP_InputField commandInput;
        private PlayerMovement _playerMovement;

        private string _expectedCommand;

        private UnityEvent _unityOnSuccess;
        private System.Action _actionOnSuccess;

        private void Awake()
        {
            _playerMovement = FindObjectOfType<PlayerMovement>();
            if (Instance == null) Instance = this;
            else Destroy(gameObject);

            terminalPanel.SetActive(false);
        }

        public void OpenTerminal(string expectedCmd, UnityEvent onSuccessUnity)
        {
            _expectedCommand = expectedCmd;
            _unityOnSuccess = onSuccessUnity;
            PauseController.SetPause(true);
            if (_playerMovement != null)
                _playerMovement.enabled = false;
            terminalPanel.SetActive(true);
            commandInput.text = "";
            commandInput.ActivateInputField();
        }

        public void OpenTerminal(string expectedCmd, System.Action onSuccessAction)
        {
            _expectedCommand = expectedCmd;
            _actionOnSuccess = onSuccessAction;
            PauseController.SetPause(true);
            if (_playerMovement != null)
                _playerMovement.enabled = false;
            terminalPanel.SetActive(true);
            commandInput.text = "";
            commandInput.ActivateInputField();
        }

        public void OnSubmitCommand()
        {
            var typed = commandInput.text.Trim();
            Debug.Log($"[TerminalController] User typed: {typed}");

            if (typed == _expectedCommand)
            {
                Debug.Log("[TerminalController] Command matched!");

                _unityOnSuccess?.Invoke();
                _actionOnSuccess?.Invoke();

                terminalPanel.SetActive(false);
                commandInput.text = "";
            }
            else
            {
                terminalPanel.SetActive(false);
                Debug.LogWarning("[TerminalController] Incorrect command!");
            }
            PauseController.SetPause(false);
            if (_playerMovement != null)
                _playerMovement.enabled = true;
        }
    }
}