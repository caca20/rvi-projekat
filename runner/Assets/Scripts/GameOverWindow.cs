using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverWindow : PauseScript
{
    public Text score;
    public Text playerName;
    // Start is called before the first frame update
    void Start()
    {   
        Started();
        playerName.text = PlayerPrefs.GetString("Current user");
        score.text = player.points.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void onDisable(){
        Ended();
        PlayerPrefs.DeleteKey("Current user");
    }
}
