using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class ScoresButton : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler
{
    public GameObject parent;
    public RectTransform Button;
    public Button btn;
    public GameObject  scores;
    void Start()
    {
        Button.GetComponent<Animator>().Play("hoverOff");
    }

    // Update is called once per frame
    void Update()
    {
        if(parent.active == true){
            //btn.enabled = false;
        }
    }

    public void OnPointerEnter(PointerEventData data){
        Button.GetComponent<Animator>().Play("hoverOn");
    }

    public void OnPointerExit(PointerEventData data){
        Button.GetComponent<Animator>().Play("hoverOff");
    }
    public void showScores(){
        Time.timeScale = 0;
        scores.gameObject.SetActive(true);
    }
}
