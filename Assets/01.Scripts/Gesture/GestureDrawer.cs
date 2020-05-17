using System;
using System.Collections.Generic;
using UnityEngine;
using PDollarGestureRecognizer;

public class GestureDrawer : MonoBehaviour
{
    [SerializeField]
    private new LineRenderer renderer;
    [SerializeField]
    private TextAsset[] normalXmls;
    [SerializeField]
    private TextAsset[] abilityXmls;
    [SerializeField]
    private IntEvent normalDrawed;
    [SerializeField]
    private IntEvent abilityDrawed;

    private Gesture[] normalGestures;
    private Gesture[] abilityGestures;
    private List<Point> points = new List<Point>();
    private Vector3 position;
    private int strokeID = -1;
    private int positionCount;
    private bool isAbilityActivated;

    public void ActivateAbility()
    {
        isAbilityActivated = true;
    }

    private void Awake()
    {
        normalGestures = new Gesture[normalXmls.Length];
        abilityGestures = new Gesture[abilityXmls.Length];

        for (int i = 0; i < normalXmls.Length; i++)
        {
            normalGestures[i] = GestureIO.ReadGestureFromXML(normalXmls[i].text);
        }
        for (int i = 0; i < abilityXmls.Length; i++)
        {
            abilityGestures[i] = GestureIO.ReadGestureFromXML(abilityXmls[i].text);
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
                Result result;
                if (isAbilityActivated)
                {
                    isAbilityActivated = false;
                    result = PointCloudRecognizer.Classify(candidate, abilityGestures);
                    abilityDrawed?.Invoke(GetGestureIndex(result.GestureClass));
                }
                else
                {
                    result = PointCloudRecognizer.Classify(candidate, normalGestures);
                    normalDrawed?.Invoke(GetGestureIndex(result.GestureClass));
                }
#if UNITY_EDITOR
                Debug.Log($"{result.GestureClass} {result.Score}");
#endif
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
            case "UpArrow":
                return 5;
            case "Zigzag":
                return 6;
            case "Wind":
                return 7;
            case "Flame":
                return 8;
            case "Circle":
                return 9;
            case "Ice":
                return 10;
        }
        throw new ArgumentException();
    }
}
