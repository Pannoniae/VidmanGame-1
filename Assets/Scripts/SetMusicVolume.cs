using UnityEngine;
using UnityEngine.UI;

public class SetMusicVolume : MonoBehaviour {
    public string audioObjectName;
    GameObject audioObject;
    AudioSource audioSource;
    Slider slider;

    void Start() {
        audioObject = GameObject.Find(audioObjectName);
        audioSource = audioObject.GetComponent<AudioSource>();
        slider = GetComponent<Slider>();
        slider.value = audioSource.volume;
    }

    public void UpdateAudio() {
        audioSource.volume = slider.value; //if its 0.0-1.0;
    }
}