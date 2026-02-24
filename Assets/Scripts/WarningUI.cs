using UnityEngine;

public class WarningUI : MonoBehaviour
{
	[SerializeField] float _displayDuration = 2f;

    private float _Duration;
    private void Awake()
    {
        PlayerController.OnDisplayWarning += StartDisplay;
        gameObject.SetActive(false);
    }
    private void StartDisplay(string Warningtext)
    {
        _Duration = _displayDuration;
        gameObject.SetActive(true);
        GetComponentInChildren<TMPro.TMP_Text>().text = Warningtext;
    }
    private void Update()
    { 
        if (_Duration > 0)
        {
            _Duration -= Time.deltaTime;
            if (_Duration <= 0)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
