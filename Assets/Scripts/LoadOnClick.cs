using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadOnClick : MonoBehaviour {
    public const int NUMBER_OF_LEVELS = 2;
    public void LoadScene(string level) {
        var levelNo = int.Parse(level.Substring(5));
        if (levelNo > NUMBER_OF_LEVELS) {
            Debug.Log($"Tried to load a level which doesn't exist yet (Level{level})");
        }
        SceneManager.LoadScene(level);
    }
}