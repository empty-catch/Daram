using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class YieldInstructionCache 
{
    private static Dictionary<float, WaitForSeconds> waitingSecond = new Dictionary<float, WaitForSeconds>();
    private static object waitFrame = null;

    public static object WaitFrame => waitFrame;

    public static WaitForSeconds WaitingSecond(float waitingTime){
        if(!waitingSecond.ContainsKey(waitingTime)){
            waitingSecond.Add(waitingTime, new WaitForSeconds(waitingTime));
        }

        return waitingSecond[waitingTime];
    }

}
