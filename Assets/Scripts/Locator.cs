using Unity.VisualScripting;
using UnityEngine;

public class Locator : MonoBehaviour
{
    public static Locator Instance { get; private set; }
    public static PlayerController Player { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        GameObject gameObject = GameObject.FindWithTag("Player");
        Player = gameObject.GetComponent<PlayerController>();
    }
}
