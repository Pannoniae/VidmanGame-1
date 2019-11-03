using UnityEngine;

public class BGMusic : MonoBehaviour {
    public static BGMusic Instance { get; set; }

    void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }
        else {
            Instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }
}