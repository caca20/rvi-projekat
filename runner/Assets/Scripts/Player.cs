using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int points;
    [SerializeField] public Rigidbody rgdbody;
    [SerializeField] public GameObject player;
    [SerializeField] public Camera cam;

    private bool boosterOn=false;

    private int endTime=3;

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

    private void Update() {
        Vector3 viewPos = cam.WorldToViewportPoint(transform.position);
        if(gameObject && !(viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1 && viewPos.z > 0)){
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
            Die();
            Debug.Log("red potion");
            Destroy(other.gameObject);
        }

        if(item.tag == "BluePotion"){
            Debug.Log("blue potion");
            Destroy(other.gameObject);
            Debug.Log("pokupio plavi");
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Rigidbody rgbody = player.GetComponent<Rigidbody>();
            rgbody.isKinematic = true;
            Vector3 pos = transform.position;
            pos.y = 0.81f;
            transform.position = pos;
            StartCoroutine(EndOfPotion(3));
    
        }

        //TODO:kad se sudari sa preprekama, zaustavlja se igra?
        if(item.tag == "Obstacle"){

            rgdbody.constraints &= ~RigidbodyConstraints.FreezePositionZ;
            //Stop the game after 3 sec
        }
    }

    void OnCollisionExit(Collision collisionInfo)
    {
        GameObject item = collisionInfo.gameObject;
        if(item.tag == "Obstacle"){
            rgdbody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
        }
    }

    public IEnumerator RipPlayer(int time){
        yield return new WaitForSecondsRealtime(time);
        Die();
    }

    public IEnumerator EndOfPotion(int actTime)
    {
        bool ind = true;
        //while(ind){
            Debug.Log("--------");
            yield return new WaitForSecondsRealtime(3);
            Debug.Log("kraj plavog");
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Rigidbody rgbody = player.GetComponent<Rigidbody>();
            rgbody.isKinematic = false;    
            ind=false;
        ///}
    }
}