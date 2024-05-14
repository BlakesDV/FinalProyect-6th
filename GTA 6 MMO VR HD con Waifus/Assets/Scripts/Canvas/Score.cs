using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{
    public TMP_Text PlayerScoreTxt;
    public TMP_Text PlayerScoreBgTxt;
    private int score = 0;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Watermelon"))
        {
            score++;
            UpdateScoreText();
        }
    }

    void UpdateScoreText()
    {
        PlayerScoreTxt.text = "Score: " + score.ToString();
        PlayerScoreBgTxt.text = "Score: " + score.ToString();
    }
}
