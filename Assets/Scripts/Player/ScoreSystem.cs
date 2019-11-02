using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{
    public Text ScoreText;
    public static int Score = 0;

    void Update()
    {
        ScoreText.text = "SCORE: " + Score;
    }
}