using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{

    public StartWindow parent;
    public GameObject window;

    public GameObject alert;
    private SortedDictionary<string,int> users = new SortedDictionary<string, int>();

    void Start()
    {
        Time.timeScale = 0;

        if(PlayerPrefs.HasKey("User 1")){
            string user1 = PlayerPrefs.GetString("User 1");
            int p1 = PlayerPrefs.GetInt(user1);
            users.Add(user1,p1);
        }
        
        if(PlayerPrefs.HasKey("User 2")){
            string user2 = PlayerPrefs.GetString("User 2");
            int p2 = PlayerPrefs.GetInt(user2);
            users.Add(user2,p2);
        }

        if(PlayerPrefs.HasKey("User 3")){
            string user3 = PlayerPrefs.GetString("User 3");
            int p3 = PlayerPrefs.GetInt(user3);
            users.Add(user3,p3);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(parent.input.isFocused){
            alert.SetActive(false);
        }
    }

    public void startGame(){

        if(users.ContainsKey(parent.input.text)){
            alert.SetActive(true);
            parent.input.text = "";
            Time.timeScale = 0;
        } else {
            PlayerPrefs.SetString("Current user", parent.input.text);
            window.SetActive(false);
            Time.timeScale = 1;
        }
    }

}
