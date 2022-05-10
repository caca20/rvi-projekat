using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadMovement : MonoBehaviour
{
    private Vector3 destination = new Vector3(0f,0.81f,-35f);
    private Vector3 moveVector = new Vector3(0f,0f,1f);
    public float moveSpeed = 3;

    //TODO: kad se pauzira da se zaustavi kretanje ?

    void Start()
    {
    }

    void Update()
    {
        MoveTowardsDestination();
    }

    public void MoveTowardsDestination(){
        Vector3 goToPosition = moveVector*moveSpeed*Time.deltaTime;
        transform.Translate(goToPosition);
    
        if(gameObject && transform.position.z <= destination.z){
            Destroy(gameObject);
        }
    }

    public void SpeedUp(int speed){
        moveSpeed = speed;
    }

}
