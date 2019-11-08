using System;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
    public int Score;
    public string level;
    public PauseMenu pauseMenu;

    public Vector2 spawn;
    public GM gm;

    public void Awake() {
        spawn = transform.position;
    }

    public void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            // ReSharper disable once Unity.PreferNonAllocApi
            var objects = gm.elements;
            foreach (var o in objects.OfType<Button>()) {
                print("a");
                o.flip();
            }
        }
    }

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

    public void Kill() {
        // TODO add "player respawned" screen
        transform.position = spawn;
    }
}