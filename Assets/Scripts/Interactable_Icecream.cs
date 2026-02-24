using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_Icecream : InteractableBase
{
    public delegate void PickUpIcecream();
    public static event PickUpIcecream OnPickUpIcecream;

    public override void Interact()
    {
        OnPickUpIcecream?.Invoke();
    }
}
