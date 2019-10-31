using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionEvent : MonoBehaviour
{
    public Text ActionText;
    public GameObject destination;
    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "UsableDoor")
        {
            ActionText.text = "Press [E] to use";
            ActionText.enabled = true;
        }
    }
    void OnTriggerStay2D(Collider2D other) {
        if (Input.GetButtonDown("Use"))
        {
            destination = other.gameObject.transform.Find("Destination").gameObject;
            gameObject.transform.position = destination.transform.position;
        }
    }
    void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "UsableDoor")
        {
            ActionText.enabled = false;
        }
    }
}
