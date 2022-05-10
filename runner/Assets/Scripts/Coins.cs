using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : RoadMovement
{
    [SerializeField] public int value;

    public int GetValueOfCoins(){
        return value;
    }
}
