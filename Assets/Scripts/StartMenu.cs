using UnityEngine;

public class StartMenu : MonoBehaviour
{
	public void Shop() { 
		UnityEngine.SceneManagement.SceneManager.LoadScene("Shop");
	}

	public void TestScense() {
		UnityEngine.SceneManagement.SceneManager.LoadScene("TestScense");
	}
}
