using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{

    // when play button is pressed, the scene number will be incremented
    public void PressPlay()
    {
        SceneManager.LoadScene(1);
    }
}
