using UnityEngine;

public class FollowBG : MonoBehaviour {
    public GameObject player;

    void LateUpdate() {
        gameObject.transform.position = player.transform.position;
    }
}