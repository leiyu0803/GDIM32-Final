using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableChocolate : InteractableBase
{
    public delegate void PickUpChocolate();
    public static event PickUpChocolate OnPickUpChocolate;

    public override void Interact()
    {
        OnPickUpChocolate?.Invoke();
    }
}
