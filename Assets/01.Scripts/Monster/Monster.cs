using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private Vector2 moveDirection;


    public void Execute(){
        gameObject.SetActive(true);
        moveDirection = (Vector2.zero - (Vector2)gameObject.transform.position).normalized;
        StartCoroutine(ExecuteCoroutine());
    }

    private IEnumerator ExecuteCoroutine(){
        while(true){
            gameObject.transform.Translate(moveDirection * speed);
            yield return YieldInstructionCache.WaitFrame;

            if(gameObject.transform.position.x * moveDirection.x > -1){
                ResetObject();
            }
        }       
    }
    
    public void ResetObject(){
        gameObject.SetActive(false);
    }
}
