using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableNPC : InteractableBase
{
    // event now carries the GameObject of the NPC that was interacted with
    public delegate void NPC(GameObject npc);
    public static event NPC OnNPC;

    private void Start()
    {
        GameController.OnOrderCompleted += OrderCompleate;
    }

    public override void Interact()
    {
        OnNPC?.Invoke(gameObject);
    }

    private void OrderCompleate()
    {
        Locator.Player._interactItems.Remove(gameObject);
        Destroy(this);
    }
    private void OnDestroy()
    {
        GameController.OnOrderCompleted -= OrderCompleate;
    }
}
