using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameController : MonoBehaviour
{
    [SerializeField] private float _MaxTime = 300f;
    [SerializeField] TMP_Text _timerText;

    [SerializeField] private TMP_Text _itemText;
    [SerializeField] private TMP_Text _scoreText;

    private IcecreamFlavor _orderFlavour;
    private ContainerType _orderContainer;

    private float _currentTime;
    public static GameController Instance { get; private set; }

    public delegate void DisplayWarning(string Warningtext);
    public static event DisplayWarning OnDisplayWarning;

    public delegate void OrderCompleted();
    public static event OrderCompleted OnOrderCompleted;

    private int _score;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        InteractableSubmit.OnSubmit += SubmitOrder;
    }
    private void Start()
    {
        _currentTime = _MaxTime;
        Test_NewOrder();
    }

    private void Update()
    {
        _currentTime -= Time.deltaTime;
        if (_currentTime >= _MaxTime)
        {
            Debug.Log("Game Over");
        }
        UpdateTimerUI();
        UpadteItemUI();
    }

    private void Test_NewOrder()
    {
        int randomContainer = Random.Range(0, 2);
        int randomFlavor = Random.Range(0, 3);
        _orderContainer = (ContainerType)randomContainer;
        _orderFlavour = (IcecreamFlavor)randomFlavor;
    }
    private void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(_currentTime / 60);
        int seconds = Mathf.FloorToInt(_currentTime % 60);
        _timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (minutes < 1) 
        {
            _timerText.color = Color.red;
        }
    }

    private void UpadteItemUI()
    {
        _itemText.text = $"Order:\n {_orderContainer} \n {_orderFlavour} \n Current: \n {Locator.Player.GetCurrentContainer()} \n {Locator.Player.GetCurrentFlavor()}";
    }

    private void SubmitOrder()
    {
        Debug.Log("Submit Order");
        if (Locator.Player.GetCurrentContainer() == _orderContainer && Locator.Player.GetCurrentFlavor() == _orderFlavour)
        {
            OnOrderCompleted?.Invoke();
            _score++;
            _scoreText.text = _score.ToString();
            Test_NewOrder();
        }
        else
        {
            OnDisplayWarning?.Invoke("Wrong Order! Try Again!");
        }
    }
}
