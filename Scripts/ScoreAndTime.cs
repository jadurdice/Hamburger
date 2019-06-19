using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreAndTime : MonoBehaviour
{

    public int scoreNum = 0;//スコア変数
    public int timeNum = 0;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;

    void Start()
    {
        
    }

    void Update()
    {
        scoreText.SetText(scoreNum.ToString());
        timeText.SetText(timeNum.ToString());
    }
}
