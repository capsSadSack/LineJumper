using Shield;
using TMPro;
using UnityEngine;

public class ShieldBehaviour : MonoBehaviour
{
    public int LayersNumber { get;  set; }

    public AudioSource shieldBrokeSound;

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
        if (LayersNumber > 0)
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
        if (levels != 0)
        {
            LayersNumber += levels;
            UpdateShieldLayersText();
            UpdateShieldImages();

            if (LayersNumber == 0)
            {
                ResetCountdownTimer();
            }

            if (levels < 0)
            {
                shieldBrokeSound.Play();
            }
        }
    }

    private void UpdateShieldLayersText()
    {
        string romanNumber = "";

        if (LayersNumber > 0)
        {
            romanNumber = RomanNumerals.ToRoman((uint)LayersNumber);
        }

        layersText.text = romanNumber;
    }

    private void UpdateShieldImages()
    {
        shield_lvl_03.gameObject.SetActive(LayersNumber >= 3);
        shield_lvl_02.gameObject.SetActive(LayersNumber >= 2);
        shield_lvl_01.gameObject.SetActive(LayersNumber >= 1);
    }
}
