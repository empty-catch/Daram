// using System;
// using System.Linq;
// using System.Collections.Generic;
// using UnityEngine;

// public class Gesture : MonoBehaviour
// {
//     [SerializeField]
//     private IntsEvent gestureDrawed;

//     private bool isDragging;
//     private Queue<int> numbers = new Queue<int>();

//     public void TouchBegan(int index)
//     {
//         isDragging = true;
//         TouchEntered(index);
//     }

//     public void TouchEnded()
//     {
//         if (isDragging)
//         {
//             isDragging = false;
//             var array = numbers.Distinct().ToArray();
//             gestureDrawed?.Invoke(array);
//             numbers.Clear();

// #if UNITY_EDITOR
//             var builder = new System.Text.StringBuilder();
//             foreach (var i in array)
//             {
//                 builder.Append(i);
//             }
//             Debug.Log(builder.ToString());
// #endif
//         }
//     }

//     public void TouchEntered(int index)
//     {
//         if (isDragging)
//         {
//             numbers.Enqueue(index);
//         }
//     }
// }
