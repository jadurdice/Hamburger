using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    public Timer timer;//対象のタイマー
    public GameObject result;//対象のresultのUI
    public Text ResultTimeText;//resultのタイムの文字の表示場所
    public Text ResultScoreText;//resultのスコアの文字の表示場所
    public Score score;//対象のスコア

    public bool IsGamePaused;//一時的休止フラグ

    // Start is called before the first frame update
    void Start()
    {
        result.SetActive(false);
        IsGamePaused = false;
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer.TotalTime <= 0)//時間終了したら
        {
            PauseGame();//ゲームを一時的休止
            ResultTimeText.text = timer.TimeConutUp.ToString();//総計のゲーム時間表示
            ResultScoreText.text = score.scoreNum.ToString();//スコア表示
        }
    }
    void PauseGame()
    {
        IsGamePaused = true;
        result.SetActive(true);
        Time.timeScale = 0; //一時的休止
    }
}
