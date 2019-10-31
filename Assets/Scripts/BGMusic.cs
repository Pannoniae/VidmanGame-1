using UnityEngine;

public class BGMusic : MonoBehaviour {
    private static BGMusic instance;

    public static BGMusic Instance => instance;

    void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
            return;
        }
        else {
            instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }
}