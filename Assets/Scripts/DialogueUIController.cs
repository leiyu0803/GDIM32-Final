using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueUIController : MonoBehaviour
{   
    public delegate void DialogueOptionSelectedHandler(string nextDialogueName);
    public static event DialogueOptionSelectedHandler onDialogueOptionSelected;

    [SerializeField] DialogueController dialogueController;
    [SerializeField] TMP_Text NPCLineText;
    [SerializeField] GameObject OptionsContainerPrefab;

    private bool isInDialogue = false;

    public void updateDialogueUI(Dialogue dialogue)
    {
        NPCLineText.text = dialogue.NPCLine;
        foreach (Transform child in OptionsContainerPrefab.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (DialogueOption option in dialogue.dialogueOptions)
        {
            GameObject optionButton = Instantiate(
                OptionsContainerPrefab, 
                OptionsContainerPrefab.transform.parent);

            optionButton.GetComponentInChildren<TMP_Text>().text = option.optionTexts;

            optionButton.GetComponent<Button>().onClick.AddListener(()
                => dialogueController.handleDialogueOptionSelected(option.nextDialogueName));
        }
    }

    public void hideDialogueUI()
    {
        NPCLineText.text = "";
        foreach (Transform child in OptionsContainerPrefab.transform)
        {
            Destroy(child.gameObject);
        }
    }

    void Update()
    {   
        isInDialogue = dialogueController.isInDialogue;
        if (isInDialogue)
        {
            GameObject clickedOption = iconClickDetection();
            if (clickedOption != null)
            {
                onDialogueOptionSelected?.Invoke(clickedOption.GetComponent<DialogueOption>().nextDialogueName);
            }
        }
    }
    //detects if mouse clicks on dialogue option UI element. If so, returns the dialogue option that was clicked on. Otherwise, returns null.
    //raycasting not used because dialogue options are not 3D objects in the world, but rather UI elements that are always facing the camera.
    private GameObject iconClickDetection(){
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Input.mousePosition;
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(mousePos), Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject.tag == "DialogueOption")
            {
                return hit.collider.gameObject;
            }
        }
        return null;
    }
}
