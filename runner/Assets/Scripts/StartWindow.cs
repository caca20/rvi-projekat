using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartWindow : MonoBehaviour
{
    public InputField input;
    public int usersCount;
    private string[] users;
    
    public RectTransform StartButton;

    void Start()
    {
       
    }

    void Update()
    {
        
    }

    /*public void saveUser(){
        Debug.Log(input.text);
        PlayerPrefs.SetString("Current user", input.text);
        this.gameObject.SetActive(false);
        Time.timeScale = 1;
    }*/
}