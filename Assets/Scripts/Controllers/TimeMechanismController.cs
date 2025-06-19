using UnityEngine;
using TMPro;
using System;
using KingdomOfLinux.Characters;

namespace KingdomOfLinux.Controllers
{
    public class TimeMechanismController : MonoBehaviour
    {
        public static TimeMechanismController Instance;

        [SerializeField] private GameObject winnerPanel;
        [SerializeField] private TMP_Text winnerText;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);

            if (winnerPanel != null)
                winnerPanel.SetActive(false);
        }

        public void ShowWinnerPanel()
        {
            string now = DateTime.Now.ToString("HH:mm:ss dd MMMM yyyy");
            winnerText.text = $"Механизм успешно настроен!\n\nТекущее время в KingdomOfLinux:\n{now}";
            winnerPanel.SetActive(true);

            PauseController.SetPause(true);
            var player = FindObjectOfType<PlayerMovement>();
            if (player != null) player.enabled = false;
        }

        public void HideWinnerPanel()
        {
            winnerPanel.SetActive(false);
            PauseController.SetPause(false);
            var player = FindObjectOfType<PlayerMovement>();
            if (player != null) player.enabled = true;
        }
    }
}