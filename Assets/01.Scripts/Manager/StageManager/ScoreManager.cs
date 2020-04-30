using System;
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

    [SerializeField]
    private Image abilityGageImage; 

    [SerializeField]
    private Button abilityButton;

    private int score;

    private float maxAbilityGage = 100;
    private float abilityGage;

    [SerializeField]
    private float defaultHp;
    private float hp;

    private Action abilitySkil;

    private void Awake(){
        hp = defaultHp;
        
        ScoreUp(0);
        GetDamage(0);
    }

    public void SettingAbilitySkil(Action abilitySkil){
        this.abilitySkil = abilitySkil;
    }

    public void ScoreUp(int score){
        this.score += score;
        scoreText.text = this.score.ToString();
        
        
        abilityGage += score;

        if(abilityGage >= maxAbilityGage){
            abilityGage = maxAbilityGage;
            abilityButton.interactable = true;
        }
        
        abilityGageImage.fillAmount = (abilityGage / maxAbilityGage);
    }

    public void AbilitySkil(){
        abilitySkil();
        AbilityGageInitialization();
    }  

    private void AbilityGageInitialization(){
        abilityGage = 0;
        abilityGageImage.fillAmount = 0;
        abilityButton.interactable = false;
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
