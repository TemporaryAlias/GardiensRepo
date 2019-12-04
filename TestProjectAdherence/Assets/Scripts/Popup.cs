using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour {

    Animator anim;

    /*
    void Start() {
        anim = GetComponentInChildren<Animator>();
    }
    */

    public void OpenPopup() {
        anim = GetComponentInChildren<Animator>();

        if (anim != null) {
            anim.SetTrigger("Open");
        }
    }

    public void ClosePopup() {
        StartCoroutine(Close());
    }

    IEnumerator Close() {
        if (anim != null) {
            anim.SetTrigger("Close");
        }

        yield return new WaitForSeconds(0.1f);

        gameObject.SetActive(false);
    }

}
