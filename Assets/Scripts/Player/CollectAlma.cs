using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectAlma : MonoBehaviour
{

    public AudioSource CollectSound;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Alma" && gameObject.name == "player" && gameObject.tag == "Player")
        {
            Destroy(other.gameObject);
            CollectSound.Play();
            ScoreSystem.Score += 10;
            Physics2D.IgnoreCollision(other, GetComponent<Collider2D>());
        }
    }

}