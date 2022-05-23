using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int points;
    [SerializeField] public Rigidbody rgdbody;
    [SerializeField] public GameObject player;
    [SerializeField] public Camera cam;
    [SerializeField] public GameObject gameOverPopup;

    public GameObject bluePotionPrefab;
    private BluePotion bluePotion;
    
    private bool boosterOn=false;

    public GameObject timerBar;
    private TimerUi boosterTimer;

    public void AddPoints(int value){
        points += value;
    }

    public void Die(){
        Destroy(gameObject);
        Debug.Log("Player RIP");
        gameOverPopup.SetActive(true);
        Time.timeScale = 0;
    }

    void Awake()
    {
        points = 0;
        boosterOn = false;
        bluePotion = bluePotionPrefab.GetComponent<BluePotion>();
        boosterTimer = timerBar.GetComponent<TimerUi>();
    }

    private void Update() {
        Vector3 viewPos = cam.WorldToViewportPoint(transform.position);
        if(gameObject && (transform.position.z<=-13 || transform.position.y<=-1) ){
        // !(viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1 && viewPos.z > 0)
            Die();
        }
    }

    private void OnCollisionEnter(Collision other) {
        GameObject item = other.gameObject;
        if(item.tag == "Coin"){
            Coins coins = item.GetComponent<Coins>();
            AddPoints(coins.GetValueOfCoins());
            Destroy(other.gameObject);
        }

        if(item.tag == "RedPotion"){
            Debug.Log("red potion");
            if(boosterOn){
                rgdbody.isKinematic=true;
            }else{
                Die();
                Destroy(other.gameObject);
            }
        }

        if(item.tag == "BluePotion"){
            Destroy(other.gameObject);
            boosterOn = true;
            boosterTimer.useBluePotion();
            rgdbody.constraints &= ~RigidbodyConstraints.FreezePositionZ;
            StartCoroutine(EndOfPotion(bluePotion.getTimeActive()));
    
        }

        if(item.tag == "Obstacle"){
            if(boosterOn){
                rgdbody.isKinematic=true;
            }
            else{
                rgdbody.constraints &= ~RigidbodyConstraints.FreezePositionZ;
            }
        }
    }

    void OnCollisionExit(Collision collisionInfo)
    {
        GameObject item = collisionInfo.gameObject;
        if(item.tag == "Obstacle"){
            rgdbody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
        }

        if(boosterOn){
            if(item.tag == "Coin"){
                rgdbody.isKinematic = true;
            }
            if(item.tag == "Obstacle"){
                rgdbody.isKinematic = false;
            }

        }
    }

    public IEnumerator EndOfPotion(int actTime)
    {
        yield return new WaitForSecondsRealtime(actTime);
        boosterOn = !boosterTimer.timeOver(); 
        if(!boosterOn){
            rgdbody.constraints &= ~RigidbodyConstraints.FreezePositionZ;
            rgdbody.isKinematic = false; 
        }
    }
}