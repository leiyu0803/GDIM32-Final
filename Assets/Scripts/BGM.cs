using GLTFast.Schema;
using Unity.VisualScripting;
using UnityEngine;

public class BGM : MonoBehaviour
{
    public static BGM Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
