using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;

public class ScoresWindow : MonoBehaviour
{
    // Start is called before the first frame update
    private SortedDictionary<string,int> users = new SortedDictionary<string, int>();
    public RectTransform content;
    async void Start()
    {
        for(int i=0; i<5;i++){
            string key = "User " + (i+1); 
            if(PlayerPrefs.HasKey(key)){
                string user = PlayerPrefs.GetString(key);
                int score = PlayerPrefs.GetInt(user);

                users.Add(user,score);
            } else{
                break;
            }
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
