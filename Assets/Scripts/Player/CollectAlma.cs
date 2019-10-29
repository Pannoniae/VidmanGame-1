using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectAlma : MonoBehaviour
{

    public AudioSource CollectSound;
    void OnTriggerEnter2D(Collider2D collision)
    {
        CollectSound.Play();
        ScoreSystem.Score += 10;
        Destroy(gameObject);
    }

}