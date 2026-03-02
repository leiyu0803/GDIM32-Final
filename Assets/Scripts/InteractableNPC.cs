using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableNPC : InteractableBase
{
    public delegate void NPC();
    public static event NPC OnNPC;

    private void Start()
    {
        GameController.OnOrderCompleted += OrderCompleate;
    }

    public override void Interact()
    {
        OnNPC?.Invoke();
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
