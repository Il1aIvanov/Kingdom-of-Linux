using UnityEngine;
using KingdomOfLinux.Interfaces;
using KingdomOfLinux.Controllers;

public class TimeMechanismTrigger : MonoBehaviour, IInteractable
{
    [TextArea] public string expectedCommand = "sudo timedatectl time-zone KingdomOfLinux";

    public void Interact()
    {
        if (PauseController.IsGamePaused) return;

        TerminalController.Instance.OpenTerminalTM(expectedCommand, () =>
        {
            Debug.Log("[TimeMechanismTrigger] Command successful!");
            TimeMechanismController.Instance.ShowWinnerPanel();
        });

        FindObjectOfType<InteractionDetector>()?.HideIcon();
    }

    public bool CanInteract() => !PauseController.IsGamePaused;
}