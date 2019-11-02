using UnityEngine;

public class CollectAlma : MonoBehaviour {
    public AudioSource CollectSound;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Alma") && gameObject.name == "Player" && gameObject.CompareTag("Player")) {
            other.gameObject.SetActive(false);
            CollectSound.Play();
            ScoreSystem.score += 10;
            Physics2D.IgnoreCollision(other, GetComponent<Collider2D>());
        }
    }
}