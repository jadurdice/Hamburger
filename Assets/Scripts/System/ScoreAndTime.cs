using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreAndTime : MonoBehaviour
{
    //スコアとタイムの表示コントロールスクリプト
    //全部受けるだけだから外部参照はいらない

   　//増加するスコア
    public int scoreNum = 0;
    //タイム表示
    public int timeNum = 0;

    //タイム経過を偵察する用
    int checkTime;

    //次のランキングに渡すスコアを記録
    public static int scoreToNext;

    //スコアとタイムを表示するテキスト
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;

    void Start()
    {
        //シーン遷移時に消さないよう
        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {

        //スコア増加や減少する時の表現
        //増やす時は、増加するスコアを減少して、次に渡すスコアを増加
        //（いわば次に渡すスコアこそ真のスコア）
        if(scoreNum == 0)
        {
            scoreText.color = Color.white;
        }
        if(scoreNum > 0)
        {
            if (scoreNum > 50)
            {
                //デガすぎるならシーン遷移する時に個数が残る
                //だから加速する
                scoreNum -= 9;
                scoreToNext += 9;
            }
            else
            {
                scoreNum -= 1;
                scoreToNext += 1;
            }

            //緑にする
            scoreText.color = new Color(0.4f,1.0f,0.0f,1.0f);

        }
        if (scoreNum < 0)
        {
            //赤にする
            scoreNum += 1;
            scoreToNext -= 1;
            scoreText.color = new Color(1.0f,0.4f,0.0f,1.0f);
        }



        //タイム表示を、10秒以上なら緑、以下なら赤+カウントダウン音を流す
        
        if(timeNum >= 10)
        {
            timeText.color = Color.green;
            timeText.SetText(timeNum.ToString());
        }
        else
        {
            //10の桁を0にする方法がわからないから0を足す
            timeText.color = Color.red;
            timeText.SetText("0"+timeNum.ToString());

        }

        if(timeNum <= 10 && checkTime != timeNum)
        {
            FindObjectOfType<AudioManeger>().Play("S_clock");
            checkTime = timeNum;
        }
        

        //スコアを設定
        scoreText.SetText(scoreToNext.ToString());
        
    }

    public int GetScore()
    {
        //スコアをランキングに渡す
        return scoreToNext;
    }
}
