using UnityEngine;
using KingdomOfLinux.Interfaces;
using KingdomOfLinux.Controllers;

namespace KingdomOfLinux.Interactables
{
    public class Instruction : MonoBehaviour, IInteractable
    {
        [TextArea]
        public string text;

        private bool _isShowing;

        public void Interact()
        {
            if (_isShowing || string.IsNullOrEmpty(text)) return;

            _isShowing = true;
            InstructionController.Instance.Show(text);
            HideInteractionIcon();
        }

        public bool CanInteract()
        {
            return !_isShowing && !InstructionController.Instance.IsOpen();
        }

        private void HideInteractionIcon()
        {
            var detector = FindObjectOfType<InteractionDetector>();
            if (detector != null)
                detector.HideIcon();
        }

        private void Update()
        {
            // Если хочешь ESC для закрытия
            if (_isShowing && Input.GetKeyDown(KeyCode.Escape))
            {
                InstructionController.Instance.Hide();
                _isShowing = false;
            }
        }
    }
}