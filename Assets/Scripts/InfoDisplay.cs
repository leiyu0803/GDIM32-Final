using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DebugInfoDisplay : MonoBehaviour
{
    public TMP_Text displayText;
    public BuildInfo buildInfo;

    private string runCode;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        runCode = RunCodeGenerator.GenerateRunCode();

        SceneManager.sceneLoaded += OnSceneLoaded;

        RefreshDisplay();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        RefreshDisplay();
    }

    private void RefreshDisplay()
    {
        string groupName = "NEW BEE";
        string versionPurpose = buildInfo.versionPurpose;
        string buildID = buildInfo.buildID;
        string sceneName = SceneManager.GetActiveScene().name;

        displayText.text =
            $"{groupName} | {versionPurpose} {buildID} | {sceneName} | {runCode}";
    }
}
