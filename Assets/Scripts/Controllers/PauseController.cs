using UnityEngine;

namespace KingdomOfLinux.Controllers
{
    public class PauseController : MonoBehaviour
    {
        public static bool IsGamePaused { get; private set; } = false;

        public static void SetPause(bool pause)
        {
            IsGamePaused = pause;
        }
    }
}