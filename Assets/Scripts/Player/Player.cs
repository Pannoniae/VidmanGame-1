using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
    public int Score;
    public string level;
    public PauseMenu pauseMenu;

    public void SavePlayer() {
        Score = ScoreSystem.Score;
        level = SceneManager.GetActiveScene().name;
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer() {
        pauseMenu = GameObject.Find("pause Canvas").GetComponent<PauseMenu>();
        var data = SaveSystem.LoadPlayer();

        var crScene = SceneManager.GetActiveScene().name;
        if (crScene != data.level) {
            SceneManager.LoadScene(data.level);
        }

        ScoreSystem.Score = data.Score;

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position;

        pauseMenu.Resume();
    }
}