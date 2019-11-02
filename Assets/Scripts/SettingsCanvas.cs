using UnityEngine;

public class SettingsCanvas : MonoBehaviour {
    public GameObject settingsMenuUI;
    public GameObject previousMenuUI;

    public void buttonBack() {
        settingsMenuUI.SetActive(false);
        previousMenuUI.SetActive(true);
    }

    public void setSettingsActive() {
        settingsMenuUI.SetActive(true);
        previousMenuUI.SetActive(false);
    }
}