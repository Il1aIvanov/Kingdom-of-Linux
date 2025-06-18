using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{
    public Button muteButton;
    public bool isMuted;

    void ToggleSound()
    { // Отключение или включение звука
        AudioListener.volume = isMuted ? 0 : 1;
    }
}
