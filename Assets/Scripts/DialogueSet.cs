using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class DialogueOption {
    public string optionTexts;
    public string nextDialogueName;
}
[System.Serializable]
public class Dialogue {
    public string NPCLine;
    public List<DialogueOption> dialogueOptions;
    public string name;
}
[CreateAssetMenu(fileName = "DialogueSet", menuName = "ScriptableObjects/Dialogue Set", order = 1)]
public class DialogueSet : ScriptableObject {
    public List<Dialogue> dialogues = new List<Dialogue>(){
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