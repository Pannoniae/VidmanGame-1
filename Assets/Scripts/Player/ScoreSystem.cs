using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{
    public GameObject ScoreText;
    public static int Score;

    void Start()
    {
        // ha savefile léteik és vagy continue volt megnyomva: loadscore else:
        Score = 0;
    }

    void Update()
    {
        ScoreText.GetComponent<Text>().text = "SCORE: " + Score;
    }
}