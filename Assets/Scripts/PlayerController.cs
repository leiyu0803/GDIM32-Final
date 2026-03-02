using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum IcecreamStage
{
    None,
    Container,
    Finished
}

public enum IcecreamFlavor
{
    Chocolate,
    Strawberry,
    Vanilla,
    None
}

public enum ContainerType
{
    Cone,
    Cup,
    None
}

public class PlayerController : MonoBehaviour
{
    public delegate void DisplayWarning(string Warningtext);
    public static event DisplayWarning OnDisplayWarning;

    public List<GameObject> _interactItems;

    [SerializeField] GameObject _pickupUI;
    [SerializeField] TMP_Text _pickupText;

    [SerializeField] GameObject _WarningUI;
    [SerializeField] TMP_Text _warningText;

    public IcecreamStage _currentStage = IcecreamStage.None;
    public IcecreamFlavor _currentFlavor;
    public ContainerType _currentContainer;
    public static PlayerController Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;

        InteractableCones.OnPickUpCones += PickUpCones;
        InteractableCups.OnPickUpCups += PickUpCup;
        InteractableChocolate.OnPickUpChocolate += PickUpChocolate;
        InteractableStrawberry.OnPickUpStrawberry += PickUpStrawberry;
        InteractableVanilla.OnPickUpVanilla += PickUpVanilla;
        InteractableTrashBin.OnPickUpTrashBin += Trashcan;
        GameController.OnOrderCompleted += Trashcan;
    }


    private void Update()
    {
        UpdatePickUpSystem();
    }


    private void UpdatePickUpSystem()
    {
        if (_interactItems.Count > 0)
        {
            float closestDistance = float.MaxValue;
            GameObject closestItem = null;
            foreach (GameObject item in _interactItems)
            {
                float distance = Vector3.Distance(transform.position, item.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestItem = item;
                }
            }
            _pickupUI.SetActive(true);
            if(closestItem.tag == "TrashBin") 
            {
                _pickupText.text = "Press F to throw away your ice cream";
            }
            else if(closestItem.tag == "NPC" && GameController.Instance._isNPCInteracted == true)
            {
                _pickupText.text = "Press F to submit order";
            }
            else if(closestItem.tag == "NPC" &&  GameController.Instance._isNPCInteracted == false)
            {
                _pickupText.text = "Press F to take order";
            }
            else
                _pickupText.text = "Press F to pick up " + closestItem.name;
            if (Input.GetKeyDown(KeyCode.F))
            {
                closestItem.GetComponent<InteractableBase>().Interact();
            }
        }
        else
        {
            _pickupUI.SetActive(false);
        }
    }

    private void PickUpCones()
    {

        if (_currentStage == IcecreamStage.None)
        {
            Debug.Log("Picked up cones");
            _currentContainer = ContainerType.Cone;
            _currentStage = IcecreamStage.Container;
        }
        else if (_currentStage == IcecreamStage.Container)
        {
            OnDisplayWarning?.Invoke("You already have a container!");
        }
        else if (_currentStage == IcecreamStage.Finished)
        {
            OnDisplayWarning?.Invoke("You already have an ice cream!");
        }
    }

    private void PickUpCup()
    {
        if (_currentStage == IcecreamStage.None)
        {
            _currentContainer = ContainerType.Cup;
            _currentStage = IcecreamStage.Container;
        }
        else if (_currentStage == IcecreamStage.Container)
        {
            OnDisplayWarning?.Invoke("You already have a container!");
        }
        else if (_currentStage == IcecreamStage.Finished)
        {
            OnDisplayWarning?.Invoke("You already have an ice cream!");
        }
    }

    private void PickUpChocolate()
    {
        if (_currentStage == IcecreamStage.Container)
        {
            _currentFlavor = IcecreamFlavor.Chocolate;
            _currentStage = IcecreamStage.Finished;
        }
        else if (_currentStage == IcecreamStage.None)
        {
            OnDisplayWarning?.Invoke("You need to pick up a container first!");
        }
        else if (_currentStage == IcecreamStage.Finished)
        {
            OnDisplayWarning?.Invoke("You already have an ice cream!");
        }
    }
    private void PickUpStrawberry()
    {
        if (_currentStage == IcecreamStage.Container)
        {
            _currentFlavor = IcecreamFlavor.Strawberry;
            _currentStage = IcecreamStage.Finished;
        }
        else if (_currentStage == IcecreamStage.None)
        {
            OnDisplayWarning?.Invoke("You need to pick up a container first!");
        }
        else if (_currentStage == IcecreamStage.Finished)
        {
            OnDisplayWarning?.Invoke("You already have an ice cream!");
        }
    }
    private void PickUpVanilla()
    {
        if (_currentStage == IcecreamStage.Container)
        {
            _currentFlavor = IcecreamFlavor.Vanilla;
            _currentStage = IcecreamStage.Finished;
        }
        else if (_currentStage == IcecreamStage.None)
        {
            OnDisplayWarning?.Invoke("You need to pick up a container first!");
        }
        else if (_currentStage == IcecreamStage.Finished)
        {
            OnDisplayWarning?.Invoke("You already have an ice cream!");
        }
    }

    private void Trashcan()
    {
        if(_currentStage == IcecreamStage.None)
        {
            OnDisplayWarning?.Invoke("You don't have anything to throw away!");
            return;
        }
        _currentStage = IcecreamStage.None;

    }

    public ContainerType GetCurrentContainer()
    {
        if (_currentStage == IcecreamStage.Container || _currentStage == IcecreamStage.Finished)
        {
            return _currentContainer;
        }
        else
        {
            return ContainerType.None;
        }
    }

    public IcecreamFlavor GetCurrentFlavor()
    {
        if (_currentStage == IcecreamStage.Finished)
        {
            return _currentFlavor;
        }
        else
        {
            return IcecreamFlavor.None;
        }
    }
}