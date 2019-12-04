using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPanel : MonoBehaviour {

    Animator anim;

    void Start() {
        anim = GetComponent<Animator>();
    }

    public void OpenPanel() {
        EnablePanel();
        anim = GetComponent<Animator>();
        anim.SetTrigger("Open");
    }


    public void ClosePanel() {
        anim.SetTrigger("Close");
    }

    public void EnablePanel() {
        gameObject.SetActive(true);
    }

    public void DisablePanel() {
        gameObject.SetActive(false);
    }

}
