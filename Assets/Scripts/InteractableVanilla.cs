using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableVanilla : InteractableBase
{
    public delegate void PickUpVanilla();
    public static event PickUpVanilla OnPickUpVanilla;

    public override void Interact()
    {
        OnPickUpVanilla?.Invoke();
    }
}
