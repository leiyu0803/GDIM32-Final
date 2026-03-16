using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum IcecreamStage
{
    Empty,
    Container,
    Finished
}

public enum IcecreamFlavor
{
    Grape,
    Strawberry,
    Blood,
    None
}

public enum ContainerType
{
    Cone,
    Cup,
    None
}

public enum NPCType
{
    DesruptiveCostumer,
    IndecisiveCostumer,
    RegularCostumer,
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
    [Header("Ice Cream Assets")]
    [SerializeField] Material _grapeMat;
    [SerializeField] Material _bloodMat;
    [SerializeField] Material _strawberryMat;
    [SerializeField] GameObject _cupIcecream;
    [SerializeField] GameObject _coneIcecream;
    [SerializeField] GameObject _cupIcecreamIcecream;
    [SerializeField] GameObject _coneIcecreamIcecream;

    [SerializeField] AudioSource _cupSound;
    [SerializeField] AudioSource _coneSound;
    [SerializeField] AudioSource _icecreamSound;
    [SerializeField] AudioSource _trashSound;


    public IcecreamStage _currentStage = IcecreamStage.Empty;
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

        if (_currentStage == IcecreamStage.Empty)
        {
            Debug.Log("Picked up cones");
            _currentContainer = ContainerType.Cone;
            _currentStage = IcecreamStage.Container;
            _coneIcecream.SetActive(true);
            _coneSound.Play();
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
        if (_currentStage == IcecreamStage.Empty)
        {
            _currentContainer = ContainerType.Cup;
            _currentStage = IcecreamStage.Container;
            _cupIcecream.SetActive(true);
            _cupSound.Play();
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
            _currentFlavor = IcecreamFlavor.Grape;
            _currentStage = IcecreamStage.Finished;
            _icecreamSound.Play();
            if (_currentContainer == ContainerType.Cup)
            {
                _cupIcecreamIcecream.SetActive(true);
                _cupIcecreamIcecream.GetComponent<Renderer>().material = _grapeMat;
            }
            else if (_currentContainer == ContainerType.Cone)
            {
                _coneIcecreamIcecream.SetActive(true);
                _coneIcecreamIcecream.GetComponent<Renderer>().material = _grapeMat;
            }
        }
        else if (_currentStage == IcecreamStage.Empty)
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
            _icecreamSound.Play();
            if (_currentContainer == ContainerType.Cup)
            {
                _cupIcecreamIcecream.SetActive(true);
                _cupIcecreamIcecream.GetComponent<Renderer>().material = _strawberryMat;
            }
            else if (_currentContainer == ContainerType.Cone)
            {
                _coneIcecreamIcecream.SetActive(true);
                _coneIcecreamIcecream.GetComponent<Renderer>().material = _strawberryMat;
            }
        }
        else if (_currentStage == IcecreamStage.Empty)
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
            _currentFlavor = IcecreamFlavor.Blood;
            _currentStage = IcecreamStage.Finished;
            _icecreamSound.Play();
            if (_currentContainer == ContainerType.Cup)
            {
                _cupIcecreamIcecream.SetActive(true);
                _cupIcecreamIcecream.GetComponent<Renderer>().material = _bloodMat;
            }
            else if (_currentContainer == ContainerType.Cone)
            {
                _coneIcecreamIcecream.SetActive(true);
                _coneIcecreamIcecream.GetComponent<Renderer>().material = _bloodMat;
            }
        }
        else if (_currentStage == IcecreamStage.Empty)
        {
            OnDisplayWarning?.Invoke("You need to pick up a container first!");
        }
        else if (_currentStage == IcecreamStage.Finished)
        {
            OnDisplayWarning?.Invoke("You already have an ice cream!");
        }

    }
    public void Clear()
    {
        _currentStage = IcecreamStage.Empty;
        _currentFlavor = IcecreamFlavor.None;
        _currentContainer = ContainerType.None;
        _cupIcecream.SetActive(false);
        _coneIcecream.SetActive(false);
        _cupIcecreamIcecream.SetActive(false);
        _coneIcecreamIcecream.SetActive(false);
    }
    private void Trashcan()
    {
        if(_currentStage == IcecreamStage.Empty)
        {
            OnDisplayWarning?.Invoke("You don't have anything to throw away!");
            return;
        }
        _trashSound.Play();
        Clear();
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