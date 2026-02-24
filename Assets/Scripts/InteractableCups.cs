using UnityEngine;

public class InteractableCups : InteractableBase
{
    public delegate void PickUpCups();
    public static event PickUpCups OnPickUpCups;

    public override void Interact()
	{
        OnPickUpCups?.Invoke();
    }
}
