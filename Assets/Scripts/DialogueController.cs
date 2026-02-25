using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueController : MonoBehaviour
{   
    [SerializeField] DialogueUIController dialogueUIController;

    public bool isInDialogue = false;

    private Dialogue currentDialogue;
    private DialogueSet currentDialogueSet;

    void Start()
    {
        GameController.onDialogueStart += handleDialogueStart;
        DialogueUIController.onDialogueOptionSelected += handleDialogueOptionSelected;
    }

    private void handleDialogueStart(GameObject NPC)
    {   
        currentDialogueSet = NPC.GetComponent<DialogueSet>();
        foreach (Dialogue dialogue in currentDialogueSet.dialogues)
        {
            if (dialogue.name == "startOfDialogue")
            {
                isInDialogue = true;
                currentDialogue = dialogue;
                dialogueUIController.updateDialogueUI(currentDialogue);
                break;
            }
            isInDialogue = false;
            Debug.LogError("DialogueSet does not contain a dialogue with the name 'startOfDialogue'");
        }
    }

    public void handleDialogueOptionSelected(string nextDialogueName)
    {
        foreach (Dialogue dialogue in currentDialogueSet.dialogues)
        {
            if (dialogue.name == nextDialogueName)
            {
                currentDialogue = dialogue;
                dialogueUIController.updateDialogueUI(currentDialogue);
                break;
            }
            if (dialogue.name == "endOfDialogue")
            {
                dialogueUIController.hideDialogueUI();
                isInDialogue = false;
                break;
            }
            isInDialogue = false;
            Debug.LogError("DialogueSet does not contain a dialogue with the name '" + nextDialogueName + "'");
        }
    }

    private void onDisable()
    {
        GameController.onDialogueStart -= handleDialogueStart;
    }
}
