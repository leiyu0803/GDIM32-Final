using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{   
    // Dialogue start event.
    // 传递与之对话的NPC对象给DialogueController
    public delegate void DialogueStartHandler(GameObject NPC);
    public static event DialogueStartHandler onDialogueStart;
    // (完成时删除) 对话触发逻辑尚未添加
    
    [SerializeField] private float _MaxTime = 300f;
    [SerializeField] TMP_Text _timerText;

    private float _currentTime;
    public static GameController Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }
    private void Start()
    {
        _currentTime = _MaxTime;
    }

    private void Update()
    {
        _currentTime -= Time.deltaTime;
        if (_currentTime >= _MaxTime)
        {
            Debug.Log("Game Over");
        }
        UpdateTimerUI();
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
}
