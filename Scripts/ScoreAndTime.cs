using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreAndTime : MonoBehaviour
{

    public int scoreNum = 0;//スコア変数
    public int timeNum = 0;

    public static int scoreToNext;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {

        if(scoreNum == 0)
        {
            scoreText.color = Color.white;
        }
        if(scoreNum > 0)
        {
            scoreNum -= 1;
            scoreToNext += 1;
            scoreText.color = new Color(0.4f,1.0f,0.0f,1.0f);

        }

        scoreText.SetText(scoreToNext.ToString());
        timeText.SetText(timeNum.ToString());
    }

    public int GetScore()
    {
        return scoreToNext;
    }
}
