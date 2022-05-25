using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ScoresWindow : PauseScript
{
    // Start is called before the first frame update
    private SortedDictionary<string,int> users = new SortedDictionary<string, int>();
    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    public Text user_1;
    public Text user_2;
    public Text user_3;
    async void Start()
    {
        Started();
        
    
        if(PlayerPrefs.HasKey("User 1")){
            player1.SetActive(true);
            string user1 = PlayerPrefs.GetString("User 1");
            int p1 = PlayerPrefs.GetInt(user1);
            user_1.text =  user1 + " - " + p1;
        }
        
        if(PlayerPrefs.HasKey("User 2")){
            player2.SetActive(true);
            string user2 = PlayerPrefs.GetString("User 2");
            int p2 = PlayerPrefs.GetInt(user2);
            user_2.text =  user2 + " - " + p2;
        }

        if(PlayerPrefs.HasKey("User 3")){
            player3.SetActive(true);
            string user3 = PlayerPrefs.GetString("User 3");
            int p3 = PlayerPrefs.GetInt(user3);
            user_3.text =  user3 + " - " + p3;
        }
    }
}
