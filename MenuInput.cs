using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInput : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextScene >= SceneManager.sceneCountInBuildSettings)
            nextScene = 0;
        if (UnityEngine.Input.GetButtonDown("Submit"))
            SceneManager.LoadScene(nextScene);
    }
}
