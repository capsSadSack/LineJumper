using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Text scoreValue;

    private int score = 0;

    public void IncrementScore()
    {
        score++;
        scoreValue.text = $"{ score }";
    }
}
