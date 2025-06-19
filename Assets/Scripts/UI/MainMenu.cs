using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public void OnPlayPressed()
    {
        // Загрузка игровой сцены
        SceneManager.LoadScene(1);
    }

    public void OnExitPressed()
    {
        Application.Quit();
    }
}
