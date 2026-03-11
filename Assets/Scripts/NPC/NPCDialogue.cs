using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    public DialogueSet _dialogueSet;
    private void Awake()
    {
        int randomIndex = Random.Range(0, GameController.Instance._dialogueSets.Count);
        _dialogueSet = GameController.Instance._dialogueSets[randomIndex];
    }
}
