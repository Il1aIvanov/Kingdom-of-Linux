using System;
using System.Collections;
using KingdomOfLinux.Interfaces;
using KingdomOfLinux.Controllers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KingdomOfLinux.Characters
{
    public class Npc : MonoBehaviour, IInteractable
    {
        public NpcDialogue dialogueData;
        private static readonly int XVelocity = Animator.StringToHash("xVelocity");
        private DialogueController _dialogueUI;
        private int _dialogueIndex;
        private bool _isTyping, _isDialogueActive;
        private PlayerMovement _playerMovement;
        private Animator _npcAnimator;
        private Rigidbody2D _npcRb;
        
        public void Interact()
        {
            if (dialogueData == null || (PauseController.IsGamePaused && !_isDialogueActive))
                return;
            if (_isDialogueActive)
            {
                NextLine();
            }
            else
            {
                StartDialogue(); 
            }
            
        }

        public bool CanInteract()
        {
            return !_isDialogueActive;
        }
        
        
        
        public void EndDialogue()
        {
            StopAllCoroutines();
            _isDialogueActive = false;
            _dialogueUI.SetDialogueText("");
           _dialogueUI.ShowDialogue(false);
            PauseController.SetPause(false);
            if (_playerMovement != null)
                _playerMovement.enabled = true;
        }
        
        private void StartDialogue()
        {
            HideInteractionIcon();
            _isDialogueActive = true;
            _dialogueIndex = 0;
            if (_playerMovement != null)
                _playerMovement.enabled = false;
            if (_npcRb != null)
                _npcRb.linearVelocity = Vector2.zero;
            _npcAnimator?.SetFloat(XVelocity, 0f); // проигрывается idle
            _dialogueUI.ShowDialogue(true);
            _dialogueUI.SetNpcInfo(dialogueData.npcName, dialogueData.npcPortrait);
            PauseController.SetPause(true);
            DisplayCurrentLine();
        }
        
        private void NextLine()
        {
            if (_isTyping)
            {
                StopAllCoroutines();
                _dialogueUI.SetDialogueText(dialogueData.dialogueLines[_dialogueIndex]);
                _isTyping = false;
                return;
            }
            _dialogueUI.ClearChoices();
            if (dialogueData.endDialogueLines.Length > _dialogueIndex
                && dialogueData.endDialogueLines[_dialogueIndex])
            {
                EndDialogue();
                return;
            }

            foreach (var dialogueChoice in dialogueData.dialogueChoices)
            {
                if (dialogueChoice.dialogueIndex == _dialogueIndex)
                {
                    DisplayChoices(dialogueChoice);
                    return;
                }
            }
            if (++_dialogueIndex < dialogueData.dialogueLines.Length)
            {
               DisplayCurrentLine();
               return;
            }
            EndDialogue();
        }

        private void Start()
        {
            _dialogueUI = DialogueController.Instance;
        }

        [Obsolete("Obsolete")]
        private void Awake()
        {
            _playerMovement = FindObjectOfType<PlayerMovement>();
            _npcAnimator = GetComponent<Animator>();
            _npcRb = GetComponent<Rigidbody2D>();
        }
        
        private void HideInteractionIcon()
        {
            var detector = FindObjectOfType<InteractionDetector>();
            if (detector != null)
            {
                detector.HideIcon(); 
            }
        }
        
        private IEnumerator TypeLine()
        {
            _isTyping = true;
            _dialogueUI.SetDialogueText("");
            foreach (var letter in dialogueData.dialogueLines[_dialogueIndex])
            {
                _dialogueUI.SetDialogueText(_dialogueUI.dialogueText.text += letter);
                yield return new WaitForSeconds(dialogueData.typingSpeed);
            }
            _isTyping = false;
            if (dialogueData.autoProgressLines.Length <= _dialogueIndex
                || !dialogueData.autoProgressLines[_dialogueIndex]) yield break;
            yield return new WaitForSeconds(dialogueData.autoProgressDeelay);
            NextLine();
        }

        private void DisplayChoices(DialogueChoice choice)
        {
            for (var i = 0; i < choice.choises.Length; i++)
            {
                var nextIndex = choice.nextDialogueIndexes[i];
                _dialogueUI.CreateChoiceButton(choice.choises[i], () => ChooseOption(nextIndex) );
            }
        }

        private void ChooseOption(int nextIndex)
        {
            _dialogueIndex = nextIndex;
            _dialogueUI.ClearChoices();
            DisplayCurrentLine();
        }

        private void DisplayCurrentLine()
        {
            StopAllCoroutines();
            StartCoroutine(TypeLine());
        }
    }
}
