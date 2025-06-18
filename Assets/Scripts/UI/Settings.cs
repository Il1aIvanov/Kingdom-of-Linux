using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{
// Отключение или включение звука
    public void ToggleSoundOn()
    { 
        AudioListener.volume = 1;
    }
    public void ToggleSoundOff()
    { 
        AudioListener.volume = 0;
    }
}
