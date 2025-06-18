using UnityEngine;
using TMPro;
using KingdomOfLinux.Characters;

namespace KingdomOfLinux.Controllers
{
    public class InstructionController : MonoBehaviour
    {
        public static InstructionController Instance;

        public GameObject panel;
        public TMP_Text instructionText;
        private PlayerMovement _playerMovement;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            if (panel != null)
            {
                _playerMovement = FindObjectOfType<PlayerMovement>();
                panel.SetActive(false);
            }
        }
        
        public void Show(string text)
        {
            instructionText.text = text;
            panel.SetActive(true);
            if (_playerMovement != null)
                _playerMovement.enabled = false;
            PauseController.SetPause(true);
        }
        
        public void Hide()
        {
            panel.SetActive(false);
            PauseController.SetPause(false);
            if (_playerMovement != null)
                _playerMovement.enabled = true;
        }
        
        public bool IsOpen() => panel.activeSelf;
    }
}