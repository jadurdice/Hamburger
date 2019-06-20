using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RankingControl : MonoBehaviour
{

    const int maxShow = 5;

    public TextMeshProUGUI highNameText; //ハイスコアを表示するText
    public TextMeshProUGUI highScoreText; //ハイスコアを表示するText
    private int[] highScore = new int[maxShow]; //ハイスコア用変数
    private string[] highName = new string[maxShow]; //ハイスコア用変数
    private string nameKey = "name"; //ハイスコアの保存先キー
    private string scoreKey = "score"; //ハイスコアの保存先キー

    int thisGameScore;

    void LoadRanking()
    {
        string nameStr = "";
        string scoreStr = "";
        for (int i = 0; i <= maxShow; i++)
        {
            nameStr = nameStr + (i+1).ToString() + " . " + PlayerPrefs.GetString(nameKey + (i+1).ToString()) +"\n";
            scoreStr = scoreStr + PlayerPrefs.GetInt(scoreKey + (i+1).ToString()) + "\n";
        }
        //保存しておいたハイスコアをキーで呼び出し取得し保存されていなければ0になる
        highNameText.text = nameStr;
        highScoreText.text = scoreStr;

        //ハイスコアを表示
    }


    void ScoreSorting(int turn)
    {
        int checkScore = PlayerPrefs.GetInt(scoreKey + turn.ToString());
        if (thisGameScore > checkScore)
        {
            string checkStr = PlayerPrefs.GetString(nameKey + turn.ToString());
            
            PlayerPrefs.SetInt(scoreKey + (turn + 1).ToString(), checkScore);
            PlayerPrefs.SetString(nameKey + (turn + 1).ToString(), checkStr);

            if(turn == 0)
            {
                PlayerPrefs.SetInt(scoreKey + (turn + 1).ToString(), thisGameScore);
                PlayerPrefs.SetString(nameKey + (turn + 1).ToString(), checkStr);
                return;
            }
            else
            {
                ScoreSorting(turn - 1);
            }
            return;
        }
        else
        {
            if (turn + 1 >= maxShow)
            {
                return;
            }

            PlayerPrefs.SetInt(scoreKey + (turn + 1).ToString(), thisGameScore);

            return;
        }
    }

    void test()
    {
        for (int i = 0; i <= maxShow; i++)
        {
            PlayerPrefs.SetInt(scoreKey + (i + 1).ToString(), (i-4)* (i-4));
            PlayerPrefs.SetString(nameKey + (i + 1).ToString(), (i+1).ToString());
        }
    }

    private void Start()
    {
        thisGameScore = ScoreAndTime.scoreToNext;

        ScoreSorting(maxShow - 1);
     //   test();


        LoadRanking();
    }
}
