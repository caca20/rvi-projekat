using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseWindow : PauseScript
{
    public Text name;
    public Text currCoins;

    // Start is called before the first frame update
    void Start()
    {
        Started();
        Debug.Log(name.text);
        name.text = PlayerPrefs.GetString("Current user");
        
        currCoins.text = "Total coins: " + player.points.ToString();
        
        Debug.Log(name.text);
    }

    //Current user
    // Update is called once per frame
    void Update()
    {
        
    }
}
