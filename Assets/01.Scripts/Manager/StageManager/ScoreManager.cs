using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    private Sprite idleAbilityButtonImage;

    [SerializeField]
    private Sprite[] abilityButtonImages;

    private int score;

    private float maxAbilityGage = 100;
    private float abilityGage;

    [SerializeField]
    private float defaultHp;
    private float hp;

    private Action abilitySkil;

    [SerializeField]
    private Sprite defaultHpSprite;

    [SerializeField]
    private Sprite damagedHpSprite;

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
        abilityButton.image.sprite = idleAbilityButtonImage;
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
        abilityButton.image.sprite = abilityButtonImages[index];
    }

    public void GetDamage(float damage)
    {
        hp -= damage;

        if (hp <= 0)
        {
            hp = 0;
            SceneManager.LoadScene(0);
        }

        for (int i = 0; i < hpImages.Length; i++)
        {
            hpImages[i].sprite = damagedHpSprite;
        }

        for (int i = 0; i < hp / (defaultHp / hpImages.Length); i++)
        {
            hpImages[i].sprite = defaultHpSprite;
        }

    }

}
