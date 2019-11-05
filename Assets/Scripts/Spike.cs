using System;
using UnityEngine;

// This WILL kill you.
public class Spike : MonoBehaviour
{
    // Start is called before the first frame update

    private void OnCollisionEnter2D(Collision2D other) {
        print("truee");
        if (other.gameObject.CompareTag("Player")) { // this is a quick hack, please fix if you see this
            other.gameObject.GetComponent<Player>().Kill();
        }
    }
}
