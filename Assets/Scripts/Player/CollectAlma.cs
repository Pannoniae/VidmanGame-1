using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectAlma : MonoBehaviour
{
    public AudioSource CollectSound;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Alma" && gameObject.name == "Player" && gameObject.tag == "Player")
        {
            other.gameObject.SetActive(false);
            CollectSound.Play();
            ScoreSystem.Score += 10;
            Physics2D.IgnoreCollision(other, GetComponent<Collider2D>());
        }
    }

}