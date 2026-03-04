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
    [SerializeField] GameObject NPCLineText;
    [SerializeField] GameObject OptionsContainerPrefab;

    private Dialogue currentDialogue;
    private bool isInDialogue = false;
    private List<GameObject> currentOptions = new List<GameObject>();

    public void updateDialogueUI(Dialogue dialogue)
    {
        currentDialogue = dialogue;
        // 将NPC的台词数组转换为字符串，并显示在UI上
        string npcLine = trimText(dialogue.NPCLine);
        NPCLineText.GetComponent<TMP_Text>().text = npcLine;

        // 销毁之前的选项按钮
        foreach (GameObject option in currentOptions)
        {
            Destroy(option);
        }
        currentOptions.Clear();
        // 为每个选项创建一个按钮，并设置按钮文本为选项文本
        foreach (DialogueOption option in dialogue.dialogueOptions)
        {
            GameObject optionButton = Instantiate(
                OptionsContainerPrefab, 
                OptionsContainerPrefab.transform.parent);
            string optionText = trimText(option.optionText);
            optionButton.GetComponentInChildren<TMP_Text>().text = optionText;
            currentOptions.Add(optionButton);
        }
    }

    // 临时隐藏/显示对话UI
    // 你可能不会用这个功能，但是我写了，所以就放在这里了
    // !! 不会释放资源
    public void hideDialogueUI()
    {
        NPCLineText.SetActive(false);
        foreach (GameObject option in currentOptions)
        {
            option.SetActive(false);
        }

    }
    public void showDialogueUI()
    {
        NPCLineText.SetActive(true);
        foreach (GameObject option in currentOptions)
        {
            option.SetActive(true);
        }
    }

    // 销毁对话UI
    public void destroyDialogueUI()
    {   
        NPCLineText.GetComponent<TMP_Text>().text = "";
        NPCLineText.SetActive(false);
        foreach (GameObject option in currentOptions)
        {
            Destroy(option);
        }
        currentOptions.Clear();
    }

    void Update()
    {   
        isInDialogue = dialogueController.isInDialogue;
        if (isInDialogue)
        {
            GameObject clickedOption = iconClickDetection();
            if (clickedOption != null)
            {   
                // 获取被点击的选项在当前选项列表中的索引
                int optionIndex = currentOptions.IndexOf(clickedOption);
                string nextDialogueName = currentDialogue.dialogueOptions[optionIndex].nextDialogueName[0];
                onDialogueOptionSelected?.Invoke(nextDialogueName);
            }
        }
    }

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

    private string trimText (string[] texts){
        string text = "";
        for (int i = 0; i < texts.Length; i++)
        {
            string textPart = texts[i];
            // 寻找文本中的"{{}}"标识，并替换为对应的变量值
            int startIndex = textPart.IndexOf("{{");
            while (startIndex != -1){
                int endIndex = textPart.IndexOf("}}", startIndex);
                if (endIndex != -1)
                {
                    // 访问任意类的变量（比如{{Locator.data}}），获取变量值并替换文本中的"{{}}"标识
                    string variablePath = textPart.Substring(startIndex + 2, endIndex - startIndex - 2);
                    string variableValue = "";
                    string[] pathParts = variablePath.Split('.');
                    if (pathParts.Length == 2){
                        string className = pathParts[0];
                        string variableName = pathParts[1];
                        System.Type type = System.Type.GetType(className);
                        if (type != null){
                            var field = type.GetField(variableName);
                            if (field != null){
                                variableValue = field.GetValue(null).ToString();
                            }
                            else{
                                Debug.LogError("Variable '" + variableName + "' not found in class '" + className + "'");
                            }
                        }
                        else{
                            Debug.LogError("Class '" + className + "' not found");
                        }
                    }
                    else{
                        Debug.LogError("Invalid variable path: " + variablePath);
                    }   
                    text += textPart.Substring(0, startIndex) + variableValue;
                    textPart = textPart.Substring(endIndex + 2);
                }
                else{
                    break;
                }
            }
            text += textPart;
            text += "\n";
        }
        text = text.TrimEnd('\n');
        return text;
    }
}