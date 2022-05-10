using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int points;
    [SerializeField] public Rigidbody rgdbody;
    [SerializeField] public GameObject player;

    public void AddPoints(int value){
        points += value;
    }

  //red potion
    public void Die(){
        Destroy(gameObject);
        Debug.Log("Player RIP");
        //popup to stop the game
    }

    void Awake()
    {
        points = 0;
    }

    private void OnCollisionEnter(Collision other) {
        GameObject item = other.gameObject;
        if(item.tag == "Coin"){
            Coins coins = item.GetComponent<Coins>();
            AddPoints(coins.GetValueOfCoins());
            Destroy(other.gameObject);
        }

        if(item.tag == "RedPotion"){
            Die();
            Debug.Log("red potion");
            Destroy(other.gameObject);
        }

        if(item.tag == "BluePotion"){
            Debug.Log("blue potion");
            Destroy(other.gameObject);
        }

        //TODO:kad se sudari sa preprekama, zaustavlja se igra?

    }

    private void Update() {
        
    }
}