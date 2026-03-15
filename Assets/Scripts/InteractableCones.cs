using UnityEngine;

public class InteractableCones : InteractableBase
{
    public delegate void PickUpCones();
    public static event PickUpCones OnPickUpCones;

    public override void Interact()
	{
        base.Interact();
        OnPickUpCones?.Invoke();
    }
}
