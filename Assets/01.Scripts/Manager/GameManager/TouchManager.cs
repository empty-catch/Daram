using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour
{
    private Vector2 touchDownPositionNotScreen;
    
    private Vector2 touchDownPosition;
    private Vector2 touchUpPosition;

    private Vector2 swipeDirection;

    private float minSwipeDistance;

    private bool isTouchDown;
    private bool isTouchUp;
    private bool isSwipe;

    public Vector2 TouchDownPosition => touchDownPosition;
    public Vector2 TouchUpPosition => touchUpPosition;
    public Vector2 SwipeDirection => swipeDirection;

    public bool IsTouchDown => isTouchDown;
    public bool IsTouchUp => isTouchUp;
    public bool IsSwipe => isSwipe;

    private void Awake(){
        minSwipeDistance = Screen.width / 5;
    }

    private void Update(){
        ProcessingTouch();
    }

    private void ProcessingTouch(){
        if(Input.touchCount > 0){
            Touch tempTouch = Input.touches[0];

            if(tempTouch.phase.Equals(TouchPhase.Began)){
                touchDownPositionNotScreen = tempTouch.position;
                touchDownPosition = Camera.main.ScreenToWorldPoint(touchDownPositionNotScreen);

                isTouchDown = true;
            }
            else if(tempTouch.phase.Equals(TouchPhase.Moved)){
                Vector2 currentPosition = tempTouch.position;

                if((currentPosition - touchDownPositionNotScreen).magnitude > minSwipeDistance){
                    swipeDirection = (currentPosition - touchDownPositionNotScreen).normalized;
                }
            }
            else if(tempTouch.phase.Equals(TouchPhase.Ended)){
                isTouchDown = false;
                isSwipe = false;
                isTouchUp = true;
            }
        }
    }
}
