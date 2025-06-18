using System;
using UnityEngine;

namespace KingdomOfLinux.Controllers
{
    public class MenuController : MonoBehaviour
    {
        public GameObject menuCanvas;

        private void Start()
        {
            menuCanvas.SetActive(false);
        }

        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.Escape)) return;
            if (!menuCanvas.activeSelf && PauseController.IsGamePaused) return;
            menuCanvas.SetActive(!menuCanvas.activeSelf);
            PauseController.SetPause(menuCanvas.activeSelf);
        }
    }
}