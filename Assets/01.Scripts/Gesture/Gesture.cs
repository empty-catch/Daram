using System;
using System.Collections.Generic;
using UnityEngine;

public class Gesture : MonoBehaviour
{
    public event Action<int[]> GestureDrawed;

    private bool isDragging;
    private Queue<int> numbers;

    public void TouchBegan()
    {
        isDragging = true;
    }

    public void TouchEnded()
    {
        if (isDragging)
        {
            isDragging = false;
            GestureDrawed?.Invoke(numbers.ToArray());
            numbers.Clear();
        }
    }

    public void TouchEntered(int index)
    {
        if (isDragging)
        {
            numbers.Enqueue(index);
        }
    }
}
