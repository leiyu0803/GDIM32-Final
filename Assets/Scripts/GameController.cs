using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class GameController : MonoBehaviour
{   
    // Dialogue start event.
    // 传递与之对话的NPC对象给DialogueController
    public delegate void DialogueStartHandler(GameObject NPC);
    public static event DialogueStartHandler onDialogueStart;
    // (完成时删除) 对话触发逻辑尚未添加
    [Header("UI & Timer Settings")]
    [Space]
    [SerializeField] private float _MaxTime = 300f;
    [SerializeField] TMP_Text _timerText;
    
    [SerializeField] private TMP_Text _itemText;
    [SerializeField] private TMP_Text _scoreText;

    [Space]
    [Header("Other Assignments")]
    [Space]
    [SerializeField] GameObject _NPCLookAtPoint;
    [SerializeField] GameObject _NPCPrefab;
    [SerializeField] Transform _NPCSpawnTransform;

    private IcecreamFlavor _orderFlavour = IcecreamFlavor.None;
    private ContainerType _orderContainer = ContainerType.None;

    private float _currentTime;
    public static GameController Instance { get; private set; }

    public delegate void DisplayWarning(string Warningtext);
    public static event DisplayWarning OnDisplayWarning;

    public delegate void OrderCompleted();
    public static event OrderCompleted OnOrderCompleted;

    private int _score;

    public bool _isNPCInteracted = false;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        InteractableNPC.OnNPC += NPCInteract;
    }

    public void RegisterNPC(NPCMovement npc)
    {
        if (npc != null)
        {
            npc.onArrived += OnNPCArrived;
        }
    }

    private void OnNPCArrived(NPCMovement npc)
    {
        Debug.Log("NPC Arrived at destination");
        if (_NPCLookAtPoint != null)
        {
            npc.FaceTarget(_NPCLookAtPoint.transform);
        }

            
    }

    private void Start()
    {
        _currentTime = _MaxTime;
        Instantiate(_NPCPrefab, _NPCSpawnTransform.position, _NPCSpawnTransform.rotation);
    }

    private void Update()
    {
        _currentTime -= Time.deltaTime;
        if (_currentTime<=0)
        {
            PlayerPrefs.SetInt("Score", _score);
            PlayerPrefs.Save();
            SceneManager.LoadScene("GameOver");
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

    private void NPCInteract()
    {
        Debug.Log("NPC Interacted");
        //onDialogueStart?.Invoke(Locator.Player.gameObject);
        if(_isNPCInteracted == false)
        {
            Test_NewOrder();
            _isNPCInteracted = true;
        }
        else
        {
            SubmitOrder();
        }

    }
    private void SubmitOrder()
    {
        if (Locator.Player.GetCurrentContainer() == _orderContainer && Locator.Player.GetCurrentFlavor() == _orderFlavour)
        {
            OnOrderCompleted?.Invoke();
            _score++;
            _scoreText.text = _score.ToString();
            _isNPCInteracted = false;
            _orderFlavour = IcecreamFlavor.None;
            _orderContainer = ContainerType.None;
            Instantiate(_NPCPrefab, _NPCSpawnTransform.position, _NPCSpawnTransform.rotation);
        }
        else
        {
            OnDisplayWarning?.Invoke("Wrong Order! Try Again!");
        }
    }
}
