using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour
{
    private TrackLoop track;
    public Player player;
    
    private void Awake() {
        track = GameObject.FindWithTag("GameController").GetComponent<TrackLoop>();
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    public void Started(){
        track.isPaused = true;
    }

    public void Ended(){
        track.isPaused = false;
    }

    private void OnEnable() {
        
    }


    // Start is called before the first frame update
    void Start()
    {
       Started();        
    }

    private void OnDisable() {
       track.isPaused = false;  
    }
}
