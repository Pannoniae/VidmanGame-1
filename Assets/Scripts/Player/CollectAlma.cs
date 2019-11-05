using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectAlma : MonoBehaviour {
    public AudioSource CollectSound;

    void OnTriggerEnter2D(Collider2D other) {
        var alma = other.gameObject;
        var player = gameObject;
        if (alma.CompareTag("Alma") && player.CompareTag("Player") && player.name == "Player") {
            alma.SetActive(false);
            CollectSound.Play();
            ScoreSystem.Score += 10;
            Physics2D.IgnoreCollision(other, GetComponent<Collider2D>());
        }
    }
}