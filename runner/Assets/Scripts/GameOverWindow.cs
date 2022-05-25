using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;


public class GameOverWindow : PauseScript
{
    public Text score;
    public Text playerName;
    private Dictionary<string,int> users = new Dictionary<string, int>();
    // Start is called before the first frame update
    async void Start()
    {   
        Started();
        string user = PlayerPrefs.GetString("Current user");
        playerName.text = user;
        score.text = player.points.ToString();


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
        
        users.Add(user,player.points);
        Debug.Log(users.Count);

        PlayerPrefs.DeleteKey("User 1");
        PlayerPrefs.DeleteKey("User 2");
        PlayerPrefs.DeleteKey("User 3");

        int i=1;
        int size=users.Count;
        var myList = users.ToList();

        myList.Sort((pair1, pair2) => pair2.Value.CompareTo(pair1.Value));
        foreach (var value in myList)
        {
            if(i == size + 1){
                break;
            }

            
            PlayerPrefs.SetString("User " + i,value.Key);
            PlayerPrefs.SetInt(value.Key,value.Value);
            i++;
        }

        Debug.Log(users);
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
