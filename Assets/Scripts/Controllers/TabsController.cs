using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace KingdomOfLinux.Controllers
{
    public class TabsController : MonoBehaviour
    { 
        [SerializeField] private Image[] tabImages;
        [SerializeField] private GameObject[] pages;

        public void AciveTab(int tabNumber)
        {
            for (var i = 0; i < tabImages.Length; i++)
            {
                pages[i].SetActive(false);
                tabImages[i].color = Color.grey;
            }
            pages[tabNumber].SetActive(true);
            tabImages[tabNumber].color = Color.white;
        }
        
        private void Start()
        {
            AciveTab(0);
        }
        
        
    }
}