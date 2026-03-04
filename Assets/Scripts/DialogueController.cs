using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueController : MonoBehaviour
{   
    [SerializeField] DialogueUIController dialogueUIController;

    public bool isInDialogue = false;

    private Dialogue currentDialogue;
    private DialogueSet currentDialogueSet;

    // magic-string constants make refactoring easier and avoid typos
    private const string StartKey = "startOfDialogue";
    private const string EndKey = "endOfDialogue";

    void Start()
    {
        GameController.onDialogueStart += handleDialogueStart;
        DialogueUIController.onDialogueOptionSelected += handleDialogueOptionSelected;
    }

    private void handleDialogueStart(GameObject NPC)
    {   
        currentDialogueSet = NPC.GetComponent<DialogueSet>();
        isInDialogue = false;
        bool found = false;

        foreach (Dialogue dialogue in currentDialogueSet.dialogues)
        {
            if (dialogue.name == StartKey)
            {
                found = true;
                isInDialogue = true;
                currentDialogue = dialogue;
                dialogueUIController.updateDialogueUI(currentDialogue);
                break;
            }
        }
        if (!found)
        {
            Debug.LogError("DialogueSet does not contain a dialogue with the name '" + StartKey + "'");
        }
    }

    public void handleDialogueOptionSelected(string nextDialogueName)
    {
        bool found = false;
        foreach (Dialogue dialogue in currentDialogueSet.dialogues)
        {
            if (dialogue.name == nextDialogueName)
            {
                found = true;
                if (dialogue.name == EndKey)
                {
                    // reached the designated end dialogue
                    dialogueUIController.destroyDialogueUI();
                    isInDialogue = false;
                }
                else
                {
                    currentDialogue = dialogue;
                    dialogueUIController.updateDialogueUI(currentDialogue);
                }
                break;
            }
        }
        if (!found)
        {
            isInDialogue = false;
            Debug.LogError("DialogueSet does not contain a dialogue with the name '" + nextDialogueName + "'");
        }
    }

    private void onDisable()
    {
        GameController.onDialogueStart -= handleDialogueStart;
        DialogueUIController.onDialogueOptionSelected -= handleDialogueOptionSelected;
    }
}
