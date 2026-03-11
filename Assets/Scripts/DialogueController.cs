using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueController : MonoBehaviour
{   
    public delegate void DialogueEndHandler();
    public static event DialogueEndHandler onDialogueEnd;
    public delegate void OrderPlacedHandler(IcecreamFlavor flavor, ContainerType container);
    public static event OrderPlacedHandler onOrderPlaced;

    [SerializeField] DialogueUIController dialogueUIController;

    public bool isInDialogue = false;

    private Dialogue currentDialogue;
    private DialogueSet currentDialogueSet;
    private IcecreamFlavor flavorToAdd = 0;
    private ContainerType containerToAdd = 0;

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
        currentDialogueSet = NPC.GetComponent<NPCDialogue>()._dialogueSet;
        flavorToAdd = 0;
        containerToAdd = 0;
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

    public void handleDialogueOptionSelected(GameObject selectedOption)
    {
        bool found = false;
        foreach (Dialogue dialogue in currentDialogueSet.dialogues)
        {
            if (selectedOption.GetComponent<DialogueOption>().nextDialogueName == dialogue.name)
            {
                found = true;
                if (selectedOption.GetComponent<DialogueOption>().changeFlavor)
                {
                    flavorToAdd = selectedOption.GetComponent<DialogueOption>().flavorToAdd;
                }
                if (selectedOption.GetComponent<DialogueOption>().changeContainer)
                {
                    containerToAdd = selectedOption.GetComponent<DialogueOption>().containerToAdd;
                }
                if (selectedOption.GetComponent<DialogueOption>().createEvent && flavorToAdd != 0 && containerToAdd != 0)
                {
                    onOrderPlaced?.Invoke(flavorToAdd, containerToAdd);
                }else{
                    Debug.LogError("DialogueOption is set to create an event but flavorToAdd or containerToAdd is not set properly.");
                }
                if (dialogue.name == EndKey)
                {
                    // reached the designated end dialogue
                    dialogueUIController.destroyDialogueUI();
                    onDialogueEnd?.Invoke();
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
            Debug.LogError("DialogueSet does not contain a dialogue with the name '" + selectedOption.GetComponent<DialogueOption>().nextDialogueName + "'");
        }
    }

    private void onDisable()
    {
        GameController.onDialogueStart -= handleDialogueStart;
        DialogueUIController.onDialogueOptionSelected -= handleDialogueOptionSelected;
    }
}
