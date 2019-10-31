using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DetectFinishedLevel : MonoBehaviour
{
    public GameObject toDisableCanvas;
    public GameObject toEnableCanvas;
    public Text PointsText;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player" && gameObject.name == "DetectFinishedLevel")
        {
            PointsText.text = "Score: " + ScoreSystem.Score;
            toDisableCanvas.SetActive(false);
            toEnableCanvas.SetActive(true);
        }
    }

}
