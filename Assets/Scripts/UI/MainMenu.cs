using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public void OnPlayPressed()
    {
        // Загрузка игровой сцены
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void CloseGame()
    {
        // Закрытие сцены меню
        Application.Quit();
    }
}
