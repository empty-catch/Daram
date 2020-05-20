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
    private Image abilityGaugeImage;

    [SerializeField]
    private Button abilityButton;

    [SerializeField]
    private Sprite idleAbilityButtonImage;

    [SerializeField]
    private Sprite[] abilityButtonImages;

    private int score;

    [SerializeField]
    private float defaultHp;
    private float hp;

    private Action abilitySkil;
    private int abilityIndex;

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
    }

    public void ManaChanged(int mana, int maxMana)
    {
        abilityGaugeImage.fillAmount = (float)mana / maxMana;
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
