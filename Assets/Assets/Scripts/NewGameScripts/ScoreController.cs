using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public Text scoreValue;

    public int Score { get; private set; } = 0;

    public void IncrementScore()
    {
        Score++;
        scoreValue.text = $"{ Score }";
    }
}
