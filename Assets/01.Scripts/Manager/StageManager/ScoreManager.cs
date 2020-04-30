using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private Text scoreText;
    
    [SerializeField]
    private Image[] hpImages;


    private int score;

    [SerializeField]
    private float defaultHp;
    private float hp;

    private void Awake(){
        hp = defaultHp;
        
        ScoreUp(0);
        GetDamage(0);
    }

    public void ScoreUp(int score){
        this.score += score;
        scoreText.text = this.score.ToString();
    }

    public void GetDamage(float damage){
        hp -= damage;
        
        if(hp < 0){
            hp = 0;
        }

        for(int i = 0; i < hpImages.Length; i++){
            hpImages[i].gameObject.SetActive(false);
        }

        for(int i = 0; i < hp / (defaultHp / hpImages.Length); i++){
            hpImages[i].gameObject.SetActive(true);
        }

    }

}
