using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    public delegate void DialogueEndHandler(bool IsOrder, IcecreamFlavor flavor, ContainerType container); 
    public static event DialogueEndHandler onDialogueEnd;

    [SerializeField] DialogueUIController dialogueUIController;

    public bool isInDialogue = false;

    private Dialogue currentDialogue;
    private DialogueSet currentDialogueSet;
    public static IcecreamFlavor flavorToAdd = 0;
    public static ContainerType containerToAdd = 0;

    [SerializeField] AudioSource GetOut;

    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] CameraMovement cameraMovement;

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
        playerMovement.enabled = false;
        cameraMovement.enabled = false;
        currentDialogueSet = NPC.GetComponent<NPCDialogue>()._dialogueSet;
        flavorToAdd = 0;
        containerToAdd = 0;
        isInDialogue = false;

        if (currentDialogueSet.customerType == NPCType.RegularCostumer)
        {
            int randomContainer = Random.Range(0, 2);
            int randomFlavor = Random.Range(0, 3);
            containerToAdd = (ContainerType)randomContainer;
            flavorToAdd = (IcecreamFlavor)randomFlavor;
        }

        foreach (Dialogue dialogue in currentDialogueSet.dialogues)
        {
            if (dialogue.name == StartKey)
            {
                isInDialogue = true;
                currentDialogue = dialogue;
                dialogueUIController.updateDialogueUI(currentDialogue);
                break;
            }
        }
    }

    public void handleDialogueOptionSelected(DialogueOption selectedOption)
    {
        bool found = false;
        foreach (Dialogue dialogue in currentDialogueSet.dialogues)
        {
            if (selectedOption.nextDialogueName == dialogue.name)
            {
                found = true;

                if (selectedOption.changeFlavor)
                {
                    flavorToAdd = selectedOption.flavorToAdd;
                }
                if (selectedOption.changeContainer)
                {
                    containerToAdd = selectedOption.containerToAdd;
                }
                if (dialogue.name == EndKey)
                {
                    // reached the designated end dialogue
                    Debug.Log("DialogueController: End of dialogue reached");
                    dialogueUIController.destroyDialogueUI();
                    bool isorder = currentDialogueSet.customerType != NPCType.DesruptiveCostumer;
                    if (!isorder)GetOut.Play();
                    onDialogueEnd?.Invoke(isorder, flavorToAdd, containerToAdd);
                    isInDialogue = false;
                    playerMovement.enabled = true;
                    cameraMovement.enabled = true;
                }
                else
                {
                    Debug.Log("DialogueController: Updating to next dialogue");
                    currentDialogue = dialogue;
                    dialogueUIController.updateDialogueUI(currentDialogue);
                }
                break;
            }
        }
        if (!found)
        {
            isInDialogue = false;
            Debug.LogError("DialogueSet does not contain a dialogue with the name '" + selectedOption.nextDialogueName + "'");
        }
    }
}
