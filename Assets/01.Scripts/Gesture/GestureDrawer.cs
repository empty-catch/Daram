using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PDollarGestureRecognizer;
using DG.Tweening;

public class GestureDrawer : MonoBehaviour
{
    [SerializeField]
    private float sealDuration;
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
    private Tween sealTween;

    private int sealedGesture = -1;
    private int debuff;
    private bool isDebuffed;
    private IEnumerator debuffCoroutine;

    public void ToggleDebuff(int index)
    {
        if (debuff == 0)
        {
            debuffCoroutine = Debuff();
            StartCoroutine(debuffCoroutine);
        }
        else if ((debuff ^ (1 << index)) == 0)
        {
            StopCoroutine(debuffCoroutine);
        }
        debuff ^= 1 << index;
    }

    public void SetSealedGesture(int gesture)
    {
        sealedGesture = gesture;
        sealTween?.Kill();
        sealTween = DOVirtual.DelayedCall(sealDuration, () => sealedGesture = -1);
    }

    public void ActivateAbility(Image image)
    {
        isAbilityActivated = !isAbilityActivated;
        image.color = isAbilityActivated ? Color.blue : Color.red;
    }

    private IEnumerator Debuff()
    {
        isDebuffed = false;
        while (true)
        {
            isDebuffed = !isDebuffed;
            yield return YieldInstructionCache.WaitingSecond(4F);
        }
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
                    result = PointCloudRecognizer.Classify(candidate, abilityGestures);
                    abilityDrawed?.Invoke(GetGestureIndex(result.GestureClass));
                }
                else
                {
                    result = PointCloudRecognizer.Classify(candidate, normalGestures);
                    var gestureIndex = GetGestureIndex(result.GestureClass);
                    if (gestureIndex != sealedGesture)
                    {
                        normalDrawed?.Invoke(gestureIndex);
                    }
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
                return isDebuffed ? 1 : 0;
            case "Up":
                return isDebuffed ? 0 : 1;
            case "Horizontal":
                return isDebuffed ? 3 : 2;
            case "Down":
                return isDebuffed ? 2 : 3;
            case "DownArrow":
                return isDebuffed ? 5 : 4;
            case "UpArrow":
                return isDebuffed ? 4 : 5;
            case "Zigzag":
                return 6;
            case "Wind":
                return 7;
            case "Flame":
                return 8;
            case "Ice":
                return 9;
            case "Circle":
                return 10;
        }
        throw new ArgumentException();
    }
}
