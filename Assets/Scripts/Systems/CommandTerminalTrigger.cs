using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using KingdomOfLinux.Interfaces;
using KingdomOfLinux.Controllers;

public class CommandTerminalTrigger : MonoBehaviour, IInteractable
{
    [TextArea] public string expectedCommand = "cd ClockTower";
    public string sceneToLoad = "ClockTowerScene"; // имя сцены как в Build Settings
    public UnityEvent onCommandSuccess;

    public void Interact()
    {
        if (PauseController.IsGamePaused) return;

        Debug.Log("[CommandTerminal] Opening terminal...");
        TerminalController.Instance.OpenTerminal(expectedCommand, () =>
        {
            Debug.Log("[CommandTerminal] Command matched!");

            // Загружаем сцену
            if (!string.IsNullOrEmpty(sceneToLoad))
                SceneManager.LoadScene(sceneToLoad);

            onCommandSuccess?.Invoke();
        });

        FindObjectOfType<InteractionDetector>()?.HideIcon();
    }

    public bool CanInteract()
    {
        return !PauseController.IsGamePaused;
    }
}