using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HelpButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    
    public RectTransform Button;
    public GameObject help;
    public Button btn;
    public GameObject parent;

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

    public void showHelp(){
        Time.timeScale = 0;
        help.gameObject.SetActive(true);
    }
}
