using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableTrashBin: InteractableBase
{
    public delegate void PickUpTrashBin();
    public static event PickUpTrashBin OnPickUpTrashBin;

    public override void Interact()
    {
        base.Interact();
        OnPickUpTrashBin?.Invoke();
    }
}
