using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AbiltiySceneManager : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField]
    private Text remainingPointText;

    [SerializeField]
    private Text[] skilPointTexts;

    private Dictionary<int, int> skilPoint = new Dictionary<int, int>();

    private int defaultRemainingPoint = 25;
    private int remainingPoint;

    private int RemainingPoint {
        set{
            remainingPoint = defaultRemainingPoint - value;
            remainingPointText.text = "남은 포인트 : " + remainingPoint.ToString();
        }
    }

    private void Awake(){
        PointLoad();
    }

    private void PointLoad(){
        string[] skilNames = {
            "ElectricPoint",
            "WindPoint",
            "FirePoint",
            "IcePoint",
            "EarthPoint",
        };
        
        void loadValue(int index, string name){
            if(!PlayerPrefs.HasKey(name)){
                PlayerPrefs.SetInt(name, 0);
            }

            int value = PlayerPrefs.GetInt(name);
            skilPoint[index] = value;
        }

        for(int i = 0; i < 5; i++){
            loadValue(i, skilNames[i]);
            SetSkilPointText(i, skilPoint[i]);
        }

        CalculateRemainingSkilPoint();
    }

    private void PointSave(){
        PlayerPrefs.SetInt("ElectricPoint", skilPoint[0]);
        PlayerPrefs.SetInt("WindPoint", skilPoint[1]);
        PlayerPrefs.SetInt("FirePoint", skilPoint[2]);
        PlayerPrefs.SetInt("IcePoint", skilPoint[3]);
        PlayerPrefs.SetInt("EarthPoint", skilPoint[4]);
    }

    private void SetSkilPointText(int index, int value){
        skilPointTexts[index].text = $"{value}/5";
    }

    private void CalculateRemainingSkilPoint(){
        int value = 0;

        for(int i = 0; i < 5; i++){
            value += skilPoint[i];
        }

        RemainingPoint = value;
    }

    public void AddSkilPoint(int index){
        skilPoint[index] = skilPoint[index] + 1 <= 5  
        ? skilPoint[index] + 1
        : skilPoint[index];

        SetSkilPointText(index, skilPoint[index]);
        PointSave();
        CalculateRemainingSkilPoint();
    }

    public void MinusSkilPoint(int index){
        skilPoint[index] = skilPoint[index] - 1 >= 0
        ? skilPoint[index] - 1
        : skilPoint[index];

        SetSkilPointText(index, skilPoint[index]);
        PointSave();
        CalculateRemainingSkilPoint();
    }
}
