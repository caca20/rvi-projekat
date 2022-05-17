using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePotion : MonoBehaviour
{

    public int timeActive;

    private void Awake() {
        timeActive = 3;
    }

    private void OnCollisionEnter(Collision other) {
        Debug.Log("pokupio plavi");
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Rigidbody rgbody = player.GetComponent<Rigidbody>();
        rgbody.isKinematic = true;
        StartCoroutine(EndOfPotion(timeActive));
    }

    public IEnumerator EndOfPotion(int actTime)
    {
        bool ind = true;
        while(ind){
            Debug.Log("--------");
            yield return new WaitForSecondsRealtime(3);
            Debug.Log("kraj plavog");
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Rigidbody rgbody = player.GetComponent<Rigidbody>();
            rgbody.isKinematic = false;    
            ind=false;
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
