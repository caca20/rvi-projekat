using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerUi : MonoBehaviour
{
    public GameObject timerObj;
    private float remainTime;
    private float activeTime = 5f;
    public Image timerBar;

    public void useBluePotion(){
        remainTime = activeTime;
    }

    public bool timeOver(){
        return remainTime<=0;
    }

    // Start is called before the first frame update
    void Awake()
    {
    }
    void Start()
    {
     //   gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(remainTime>0){
            remainTime -= Time.deltaTime;
            timerBar.fillAmount = remainTime / activeTime;
        }
        
    }
}
