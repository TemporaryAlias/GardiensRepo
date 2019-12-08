using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialBehaviour : MonoBehaviour {

    [SerializeField] List<Popup> tutorialPopups = new List<Popup>();
    [SerializeField] List<Button> interactables = new List<Button>();

    int currentPopup;

    private void Start() {

        //debug to test the tutorial, comment out to stop having to do the tutorial every time
        //PlayerPrefs.SetString("Seen Tutorial", "FALSE");

        if (PlayerPrefs.GetString("Seen Tutorial") == "TRUE") {
            gameObject.SetActive(false);
            return;
        }

        StartTutorial();
    }

    void StartTutorial() {
        foreach (Button button in interactables) {
            button.interactable = false;
        }

        currentPopup = 0;

        tutorialPopups[currentPopup].gameObject.SetActive(true);
        tutorialPopups[currentPopup].OpenPopup();
    }

    public void StartNextPopup() {
        StartCoroutine(NextPopup());
    }

    IEnumerator NextPopup() {
        tutorialPopups[currentPopup].ClosePopup();
        currentPopup++;

        yield return new WaitForSeconds(0.2f);

        if (currentPopup <= tutorialPopups.Count - 1) {
            tutorialPopups[currentPopup].gameObject.SetActive(true);
            tutorialPopups[currentPopup].OpenPopup();
        } else {
            EndTutorial();
        }
    }

    void EndTutorial() {
        PlayerPrefs.SetString("Seen Tutorial", "TRUE");

        foreach (Button button in interactables) {
            button.interactable = true;
        }

        gameObject.SetActive(false);
    }

    public void GiveItem(string itemToGive) {
        if (PlayerPrefs.GetInt(itemToGive) < 1) {
            PlayerPrefs.SetInt(itemToGive, 1);
        } else {
            PlayerPrefs.SetInt(itemToGive, PlayerPrefs.GetInt(itemToGive) + 1);
        }
    }

}
