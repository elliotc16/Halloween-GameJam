using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    // Start is called before the first frame update    
    void Start()
    {
        var minutes = Boss.runTimer / 60;
        minutes = Mathf.RoundToInt(minutes);
        var seconds = Boss.runTimer - minutes * 60;
        seconds = Mathf.RoundToInt(seconds);
        
        GetComponent<Text>().text = "You Took: " + minutes.ToString() + ":" + seconds.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
