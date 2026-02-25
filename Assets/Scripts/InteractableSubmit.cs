using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableSubmit : InteractableBase
{
    public delegate void Submit();
    public static event Submit OnSubmit;

    public override void Interact()
    {
        OnSubmit?.Invoke();
    }
}
