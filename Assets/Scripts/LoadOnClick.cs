using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadOnClick : MonoBehaviour {
    public void LoadScene(string level) {
        SceneManager.LoadScene(level);
    }
}