using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace KingdomOfLinux.Controllers
{
    public class DialogueController : MonoBehaviour
    {
        public static DialogueController Instance { get; private set; }
        public GameObject dialoguePanel;
        public TMP_Text dialogueText, nameText;
        public Image portraitImage;
        public Transform choiceContainer;
        public GameObject choiceButtonPrefab;

        public void ShowDialogue(bool isShow)
        {
            dialoguePanel.SetActive(isShow);
        }

        public void SetNpcInfo(string npcName, Sprite npcPortrait)
        {
            nameText.text = npcName;
            portraitImage.sprite = npcPortrait;
            
        }

        public void SetDialogueText(string text)
        {
            dialogueText.text = text;
        }

        public void ClearChoices()
        {
            foreach (Transform child in choiceContainer)
            {
                Destroy(child.gameObject);
            }
        }

        public void CreateChoiceButton(string choiceText, UnityEngine.Events.UnityAction onClick)
        {
            var choiceButton = Instantiate(choiceButtonPrefab, choiceContainer);
            choiceButton.GetComponentInChildren<TMP_Text>().text = choiceText;
            choiceButton.GetComponent<Button>().onClick.AddListener(onClick);
        }

        private void Awake()
        {
            if (Instance == null) Instance = this;
            Destroy(gameObject);
        }
    }
}