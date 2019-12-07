using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusRewardWindow : MonoBehaviour
{
    Animator anim;

    void Start() {
        anim = GetComponent<Animator>();

        anim.SetTrigger("Reward");
    }

    public void DestroyObject() {
        Destroy(gameObject);
    }

}
