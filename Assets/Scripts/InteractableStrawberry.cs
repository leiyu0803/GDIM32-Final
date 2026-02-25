using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableStrawberry : InteractableBase
{
    public delegate void PickUpStrawberry();
    public static event PickUpStrawberry OnPickUpStrawberry;

    public override void Interact()
    {
        OnPickUpStrawberry?.Invoke();
    }
}
