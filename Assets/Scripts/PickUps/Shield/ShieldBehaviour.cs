﻿using Shield;
using TMPro;
using UnityEngine;

public class ShieldBehaviour : MonoBehaviour
{
    public int ShieldLayersNumber { get;  set; }

    public SpriteRenderer shield_lvl_01;
    public SpriteRenderer shield_lvl_02;
    public SpriteRenderer shield_lvl_03;

    public TextMeshProUGUI layersText;
    public TextMeshProUGUI countdownText;

    private SimpleTimer countdownTimer;


    private void Start()
    {
        ResetCountdownTimer();
    }

    private void ResetCountdownTimer()
    {
        countdownTimer = new SimpleTimer(10, () => { IncrementShield(-1); });
    }

    private void Update()
    {
        if (ShieldLayersNumber > 0)
        {
            countdownTimer.UpdateTimer();
            UpdateCountdownText();
        }
        else
        {
            countdownText.text = "";
        }

    }

    private void UpdateCountdownText()
    {
        countdownText.text = $"{ (int)countdownTimer.remainsTime_Sec }";
    }

    public void IncrementShield(int levels)
    {
        ShieldLayersNumber += levels;
        UpdateShieldLayersText();
        UpdateShieldImages();

        if (ShieldLayersNumber == 0)
        {
            ResetCountdownTimer();
        }
    }

    private void UpdateShieldLayersText()
    {
        string romanNumber = "";

        if (ShieldLayersNumber > 0)
        {
            romanNumber = RomanNumerals.ToRoman((uint)ShieldLayersNumber);
        }

        layersText.text = romanNumber;
    }

    private void UpdateShieldImages()
    {
        shield_lvl_03.gameObject.SetActive(ShieldLayersNumber >= 3);
        shield_lvl_02.gameObject.SetActive(ShieldLayersNumber >= 2);
        shield_lvl_01.gameObject.SetActive(ShieldLayersNumber >= 1);
    }
}
