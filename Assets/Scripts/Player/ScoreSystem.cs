using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour {
    public static int score = 0;
    public Text ScoreText;

    private void Update() {
        ScoreText.text = "SCORE: " + score;
    }
}