using JetBrains.Annotations;
using UnityEngine;

namespace KingdomOfLinux.Characters
{
    [CreateAssetMenu(fileName = "NewNpcDialogue", menuName = "Npc Dialogue")]
    public class NpcDialogue : ScriptableObject
    {
        [CanBeNull] public string npcName;
        public Sprite npcPortrait;
        public string[] dialogueLines;
        public bool[] autoProgressLines;
        public bool[] endDialogueLines;
        public float autoProgressDeelay = 1.5f;
        public float typingSpeed = 0.05f;
        public AudioClip typingSound;
        public float voicePitch = 1f;
        public DialogueChoice[]  dialogueChoices;
        
    }

    [System.Serializable]
    public class DialogueChoice
    {
        public int dialogueIndex;
        public string[] choises;
        public int[] nextDialogueIndexes;
    }
}
