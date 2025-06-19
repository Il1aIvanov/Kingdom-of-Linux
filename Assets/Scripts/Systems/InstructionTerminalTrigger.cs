using UnityEngine;
using KingdomOfLinux.Controllers;
using KingdomOfLinux.Interfaces;

public class InstructionTerminalTrigger : MonoBehaviour, IInteractable
{
    [TextArea] public string instructionText;
    private bool _hasTriggered;

    public void Interact()
    {
        if (_hasTriggered) return;

        Debug.Log("[InstructionTerminal] Triggered TerminalPanel");

        TerminalController.Instance.OpenTerminalInst(
            "cat /Town/ClockTower/Instruction",
            () =>
            {
                InstructionController.Instance.Show(instructionText);
                _hasTriggered = false; 
            }
        );

        FindObjectOfType<InteractionDetector>()?.HideIcon();
    }

    public bool CanInteract() =>
        !_hasTriggered && !InstructionController.Instance.IsOpen() && !PauseController.IsGamePaused;
}