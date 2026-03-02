using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] private TMP_Text _finalScoreText;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        int score = PlayerPrefs.GetInt("Score");
        _finalScoreText.text = "Score: " + score.ToString();
    }
}
