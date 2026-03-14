using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class DialogueUIController : MonoBehaviour
{
    public delegate void DialogueOptionSelectedHandler(DialogueOption selectedOption);
    public static event DialogueOptionSelectedHandler onDialogueOptionSelected;

    [Header("UI References")]
    [SerializeField] private TMP_Text npcDialogueText;
    [SerializeField] private Transform optionsContainer;
    [SerializeField] private GameObject optionButtonPrefab;

    [Header("Layout Settings")]
    [SerializeField] private float optionSpacing = 10f;

    private List<GameObject> currentOptionButtons = new List<GameObject>();

    public void updateDialogueUI(Dialogue dialogue)
    {
        if (dialogue == null)
        {
            Debug.LogError("DialogueUIController: Dialogue is null");
            return;
        }

        ShowCursor();
        DisplayNPCDialogue(dialogue.NPCLine);
        CreateOptionButtons(dialogue.dialogueOptions);
    }

    private void DisplayNPCDialogue(string[] dialogueLines)
    {
        if (npcDialogueText == null)
        {
            Debug.LogError("DialogueUIController: NPC Dialogue Text is not assigned");
            return;
        }

        string formattedText = ProcessDialogueText(dialogueLines);
        npcDialogueText.text = formattedText;
        npcDialogueText.gameObject.SetActive(true);
    }

    private void CreateOptionButtons(List<DialogueOption> options)
    {
        ClearCurrentOptions();

        if (optionsContainer == null || optionButtonPrefab == null)
        {
            Debug.LogError("DialogueUIController: OptionsContainer or OptionButtonPrefab is not assigned");
            return;
        }

        Debug.Log("DialogueUIController: Creating " + options.Count + " option buttons");

        foreach (DialogueOption option in options)
        {
            GameObject buttonObj = Instantiate(optionButtonPrefab, optionsContainer);
            buttonObj.name = "DialogueOption_" + option.nextDialogueName;

            TMP_Text buttonText = buttonObj.GetComponentInChildren<TMP_Text>();
            if (buttonText != null)
            {
                string formattedOptionText = ProcessDialogueText(option.optionText);
                buttonText.text = formattedOptionText;
                Debug.Log("DialogueUIController: Button text set to: " + formattedOptionText);
            }

            Button button = buttonObj.GetComponent<Button>();
            button.onClick.AddListener(delegate { OnOptionButtonClicked(option);});
            Debug.Log("DialogueUIController: OnClick listener added to button for option: " + option.nextDialogueName);
            currentOptionButtons.Add(buttonObj);
        }
    }
    public void OnOptionButtonClicked(DialogueOption option)
    {
        Debug.Log("DialogueUIController: OnOptionButtonClicked method called for: " + option.nextDialogueName);

        if (onDialogueOptionSelected != null)
        {
            Debug.Log("DialogueUIController: Invoking event with " + onDialogueOptionSelected.GetInvocationList().Length + " listeners");
            onDialogueOptionSelected.Invoke(option);
        }
        else
        {
            Debug.LogWarning("DialogueUIController: No listeners subscribed to onDialogueOptionSelected event!");
        }
    }

    private void ClearCurrentOptions()
    {
        foreach (GameObject buttonObj in currentOptionButtons)
        {
            if (buttonObj != null)
            {
                Destroy(buttonObj);
            }
        }
        currentOptionButtons.Clear();
    }

    public void hideDialogueUI()
    {
        if (npcDialogueText != null)
        {
            npcDialogueText.gameObject.SetActive(false);
        }

        foreach (GameObject buttonObj in currentOptionButtons)
        {
            if (buttonObj != null)
            {
                buttonObj.SetActive(false);
            }
        }
    }

    public void showDialogueUI()
    {
        if (npcDialogueText != null)
        {
            npcDialogueText.gameObject.SetActive(true);
        }

        foreach (GameObject buttonObj in currentOptionButtons)
        {
            if (buttonObj != null)
            {
                buttonObj.SetActive(true);
            }
        }
    }

    public void destroyDialogueUI()
    {
        if (npcDialogueText != null)
        {
            npcDialogueText.text = "";
            npcDialogueText.gameObject.SetActive(false);
        }

        ClearCurrentOptions();
        HideCursor();
    }

    private string ProcessDialogueText(string[] textLines)
    {
        if (textLines == null || textLines.Length == 0)
        {
            return "";
        }

        string result = "";

        for (int i = 0; i < textLines.Length; i++)
        {
            string line = textLines[i];
            string processedLine = ReplaceVariablePlaceholders(line);
            result += processedLine;

            if (i < textLines.Length - 1)
            {
                result += "\n";
            }
        }

        return result;
    }

    private string ReplaceVariablePlaceholders(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return text;
        }

        string result = text;
        int startIndex = result.IndexOf("{{");

        while (startIndex != -1)
        {
            int endIndex = result.IndexOf("}}", startIndex);

            if (endIndex == -1)
            {
                Debug.LogWarning("DialogueUIController: Unclosed variable placeholder in text: " + text);
                break;
            }

            string variablePath = result.Substring(startIndex + 2, endIndex - startIndex - 2);
            string variableValue = GetVariableValue(variablePath);

            result = result.Substring(0, startIndex) + variableValue + result.Substring(endIndex + 2);
            startIndex = result.IndexOf("{{", startIndex + variableValue.Length);
        }

        return result;
    }

    private string GetVariableValue(string variablePath)
    {
        string[] pathParts = variablePath.Split('.');

        if (pathParts.Length != 2)
        {
            Debug.LogError("DialogueUIController: Invalid variable path format: " + variablePath + " (expected ClassName.FieldName)");
            return "{{" + variablePath + "}}";
        }

        string className = pathParts[0];
        string fieldName = pathParts[1];

        System.Type type = System.Type.GetType(className);
        if (type == null)
        {
            Debug.LogError("DialogueUIController: Class '" + className + "' not found");
            return "{{" + variablePath + "}}";
        }

        var field = type.GetField(fieldName);
        if (field == null)
        {
            Debug.LogError("DialogueUIController: Field '" + fieldName + "' not found in class '" + className + "'");
            return "{{" + variablePath + "}}";
        }

        if (!field.IsStatic)
        {
            Debug.LogError("DialogueUIController: Field '" + fieldName + "' in class '" + className + "' must be static");
            return "{{" + variablePath + "}}";
        }

        try
        {
            object value = field.GetValue(null);
            return value != null ? value.ToString() : "";
        }
        catch (System.Exception ex)
        {
            Debug.LogError("DialogueUIController: Error getting value of '" + variablePath + "': " + ex.Message);
            return "{{" + variablePath + "}}";
        }
    }

    private void ShowCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void HideCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}