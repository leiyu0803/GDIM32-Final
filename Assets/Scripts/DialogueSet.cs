using UnityEngine;
using System.Collections.Generic;

// 对话选项类，包含选项文本和下一段对话的名称
[System.Serializable]
public class DialogueOption {
    public string optionTexts;
    public string nextDialogueName;
}
// 对话类，包含NPC的台词、选项列表和对话名称
[System.Serializable]
public class Dialogue {
    public string NPCLine;
    public List<DialogueOption> dialogueOptions;
    public string name; // 需与DialogueOption中的nextDialogueName对应
}
// 对话集类，包含多个对话
[CreateAssetMenu(fileName = "DialogueSet", menuName = "ScriptableObjects/Dialogue Set", order = 1)]
public class DialogueSet : ScriptableObject {
    public List<Dialogue> dialogues = new List<Dialogue>(){
        // 默认开始h和结束对话，请勿删除这两段对话，否则会导致对话系统无法正常工作
        new Dialogue(){
            name = "startOfDialogue",
            NPCLine = "",
            dialogueOptions = new List<DialogueOption>()
        },
        new Dialogue(){
            name = "endOfDialogue",
            NPCLine = "",
            dialogueOptions = new List<DialogueOption>()
        }
    };
}