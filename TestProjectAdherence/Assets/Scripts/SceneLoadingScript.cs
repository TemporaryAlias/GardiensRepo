using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadingScript : MonoBehaviour
{
    private void Awake()
    {
        if(PlayerPrefs.GetString("First Load")!= "FALSE")
        {
           //SceneManager.LoadScene("First Load Scene");
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
