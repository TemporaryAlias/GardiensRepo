using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPanel : MonoBehaviour {

    Animator anim;
    public AudioSource output;
    [SerializeField] AudioClip open;
    [SerializeField] AudioClip close;

    void Start() {
        anim = GetComponent<Animator>();
    }

    public void OpenPanel() {
        EnablePanel();
        anim = GetComponent<Animator>();
        output.clip = open;
        output.Play();
        anim.SetTrigger("Open");
    }


    public void ClosePanel() {
        output.clip = close;
        output.Play();
        anim.SetTrigger("Close");
    }

    public void EnablePanel() {
        gameObject.SetActive(true);
    }

    public void DisablePanel() {
        gameObject.SetActive(false);
    }

}
