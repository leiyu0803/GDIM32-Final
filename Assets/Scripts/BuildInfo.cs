using UnityEngine;

[CreateAssetMenu(fileName = "BuildInfo", menuName = "Game/Build Info")]
public class BuildInfo : ScriptableObject
{
    public string versionPurpose;
    public string buildID;
}
