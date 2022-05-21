using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{

    public StartWindow parent;
    public GameObject window;
    void Start()
    {
        Time.timeScale = 0; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startGame(){
        PlayerPrefs.SetString("Current user", parent.input.text);
        window.SetActive(false);
        Time.timeScale = 1;
    }

}
