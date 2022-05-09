using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int points;
    [SerializeField] public Rigidbody rgdbody;
    [SerializeField] public GameObject player;

    public void AddPoints(int value){
        points += value;
    }

  //red potion
    public void Die(){
        Destroy(gameObject);
        //popup
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
        }

        if(item.tag == "RedPotion"){
            Die();
        }

        if(item.tag == "BluePotion"){

        }

        Destroy(other.gameObject);
    }

    private void Update() {
        
    }
}