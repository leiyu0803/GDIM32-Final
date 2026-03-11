using UnityEngine;
using System.Collections.Generic;

// 对话选项类，包含选项文本和下一段对话的名称
[System.Serializable]
public class DialogueOption {
    public string[] optionText;
    public string nextDialogueName;
    public bool createEvent; // 是否创建事件
    public string eventName; // 事件名称
    public IcecreamFlavor flavorToAdd; // 选择该选项后添加的冰淇淋口味
    public ContainerType containerToAdd; // 选择该选项后添加的容器类型
}
// 对话类，包含NPC的台词、选项列表和对话名称
[System.Serializable]
public class Dialogue {
    public string[] NPCLine;
    public List<DialogueOption> dialogueOptions;
    public string name; // 需与DialogueOption中的nextDialogueName对应
}
// 对话集类，包含多个对话
[CreateAssetMenu(fileName = "DialogueSet", menuName = "ScriptableObjects/Dialogue Set", order = 1)]
public class DialogueSet : ScriptableObject {
    public NPCType customerType; // 对话对应的顾客类型
    public List<Dialogue> dialogues = new List<Dialogue>(){
        // 默认开始和结束对话，请勿删除这两段对话，否则会导致对话系统无法正常工作
        new Dialogue(){
            name = "startOfDialogue",
            NPCLine = new string[0],
            dialogueOptions = new List<DialogueOption>()
        },
        // 结束对话，无需选项，表示对话结束
        new Dialogue(){
            name = "endOfDialogue",
            NPCLine = new string[0],
            dialogueOptions = new List<DialogueOption>()
        }
    };
}