using System;
using System.Collections.Generic;
using UnityEngine;
using PDollarGestureRecognizer;

public class GestureDrawer : MonoBehaviour
{
    [SerializeField]
    private new LineRenderer renderer;
    [SerializeField]
    private TextAsset[] gestureXmls;
    [SerializeField]
    private IntEvent gestureDrawed;

    private List<Gesture> gestures = new List<Gesture>();
    private List<Point> points = new List<Point>();
    private Vector3 position;
    private int strokeID = -1;
    private int positionCount;

    private void Awake()
    {
        foreach (var xml in gestureXmls)
        {
            gestures.Add(GestureIO.ReadGestureFromXML(xml.text));
        }
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButton(0))
        {
            position = Input.mousePosition;
        }
#elif UNITY_ANDROID || UNITY_IOS
        if (Input.touchCount > 0)
        {
            position = Input.GetTouch(0).position;
        }
#endif

        if (Input.GetMouseButtonDown(0))
        {
            strokeID++;
            renderer.gameObject.SetActive(true);
            positionCount = 0;
        }
        if (Input.GetMouseButton(0))
        {
            points.Add(new Point(position.x, -position.y, strokeID));
            positionCount++;
            renderer.positionCount = positionCount;
            var worldPoint = Camera.main.ScreenToWorldPoint(new Vector3(position.x, position.y, 10));
            renderer.SetPosition(positionCount - 1, worldPoint);
        }
        if (Input.GetMouseButtonUp(0))
        {
            var candidate = new Gesture(points.ToArray());
            try
            {
                var result = PointCloudRecognizer.Classify(candidate, gestures.ToArray());
#if UNITY_EDITOR
                Debug.Log($"{result.GestureClass}, {result.Score}");
#endif

                if (result.Score >= 0.75F)
                {
                    gestureDrawed?.Invoke(GetGestureIndex(result.GestureClass));
                }
            }
            catch (IndexOutOfRangeException) { }

            strokeID = -1;
            points.Clear();
            renderer.positionCount = 0;
            renderer.gameObject.SetActive(false);
        }
    }

    private int GetGestureIndex(string value)
    {
        switch (value)
        {
            case "Vertical":
                return 0;
            case "Up":
                return 1;
            case "Horizontal":
                return 2;
            case "Down":
                return 3;
            case "DownArrow":
                return 4;
            case "Zigzag":
                return 5;
            case "UpArrow":
                return 6;
            case "Circle":
                return 7;
        }

        throw new ArgumentException();
    }
}
