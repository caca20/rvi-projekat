using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    
    public RectTransform Button;
    // Start is called before the first frame update
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
    

    
}
