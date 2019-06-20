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



    void LoadRanking()
    {
        string nameStr = "";
        string scoreStr = "";
        for (int i = 0; i < maxShow; i++)
        {
            nameStr = nameStr + (i+1).ToString() + " . " + PlayerPrefs.GetString(nameKey + i.ToString()) +"\n";
            scoreStr = scoreStr + PlayerPrefs.GetInt(scoreKey + i.ToString()) + "\n";
        }
        //保存しておいたハイスコアをキーで呼び出し取得し保存されていなければ0になる
        highNameText.text = nameStr;
        highScoreText.text = scoreStr;

        //ハイスコアを表示
    }

    void test()
    {
        for (int i = 0; i < maxShow; i++)
        {
            PlayerPrefs.SetString(nameKey + i.ToString(), i.ToString());
            PlayerPrefs.SetInt(scoreKey + i.ToString(), (i*i));
        }
    }

    private void Start()
    {
        test();
        LoadRanking();
    }
}
