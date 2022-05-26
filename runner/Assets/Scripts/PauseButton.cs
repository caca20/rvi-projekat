using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;



public class PauseButton : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler
{
    public RectTransform Button;
    public GameObject pause;    
    public Button btn;

    void Start()
    {
        Button.GetComponent<Animator>().Play("hoverOff");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerEnter(PointerEventData data){
        Button.GetComponent<Animator>().Play("hoverOn");
    }

    public void OnPointerExit(PointerEventData data){
        Button.GetComponent<Animator>().Play("hoverOff");
    }
    public void showPause(){
        
        Time.timeScale = 0;
        pause.gameObject.SetActive(true);
    }
}
