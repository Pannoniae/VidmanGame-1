using UnityEngine;
using UnityEngine.UI;

public class DetectFinishedLevel : MonoBehaviour {
    public GameObject toDisableCanvas;
    public GameObject toEnableCanvas;
    public Text PointsText;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player") && gameObject.name == "DetectFinishedLevel") {
            PointsText.text = "Score: " + ScoreSystem.score;
            toDisableCanvas.SetActive(false);
            toEnableCanvas.SetActive(true);
        }
    }
}