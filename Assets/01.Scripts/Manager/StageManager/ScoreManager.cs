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

    [SerializeField]
    private Image idleAbilityButtonImage;

    [SerializeField]
    private Image[] abilityButtonImages;

    private int score;

    private float maxAbilityGage = 100;
    private float abilityGage;

    [SerializeField]
    private float defaultHp;
    private float hp;

    private Action abilitySkil;

    private void Awake()
    {
        hp = defaultHp;

        ScoreUp(0);
        GetDamage(0);
    }

    public void SettingAbilitySkil(Action abilitySkil)
    {
        this.abilitySkil = abilitySkil;
    }

    public void ScoreUp(int score)
    {
        this.score += score;
        scoreText.text = this.score.ToString();


        abilityGage += score;

        if (abilityGage >= maxAbilityGage)
        {
            abilityGage = maxAbilityGage;
            abilityButton.interactable = true;
            ChangeButtonImage();
        }

        abilityGageImage.fillAmount = (abilityGage / maxAbilityGage);
    }

    public void AbilitySkil()
    {
        abilitySkil();
        AbilityGageInitialization();
        abilityButton.image = idleAbilityButtonImage;
    }

    private void AbilityGageInitialization()
    {
        abilityGage = 0;
        abilityGageImage.fillAmount = 0;
        abilityButton.interactable = false;
    }

    private void ChangeButtonImage()
    {
        int index = UnityEngine.Random.Range(0, abilityButtonImages.Length);
        abilityButton.image = abilityButtonImages[index];
    }

    public void GetDamage(float damage)
    {
        hp -= damage;

        if (hp < 0)
        {
            hp = 0;
        }

        for (int i = 0; i < hpImages.Length; i++)
        {
            hpImages[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < hp / (defaultHp / hpImages.Length); i++)
        {
            hpImages[i].gameObject.SetActive(true);
        }

    }

}
