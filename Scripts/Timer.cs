using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class Timer : MonoBehaviour
{
    public GameObject text;//Timerの表示場所
    public int TotalTime;//制限時間カウンター
    int maxtime;//分、秒を分けるため、制限時間の最大値を保存
    float timecount;//秒ごとのタイムカウンター
    int minute;//分
    int second;//秒
    public int TimeConutUp;//ゲーム終了までかかる時間

    void Start()
    {
        maxtime = TotalTime;
        timecount = 0;
        TimeConutUp = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timecount += Time.deltaTime;//フレームタイムの累加

        if (TotalTime >= 0)
        {
            minute = TotalTime / 60;
            second = TotalTime % 60;
            //残る時間により、文字のカラーを変更する
            if (TotalTime > 0.5 * maxtime)//半分以上の時、白色
            {
                text.GetComponent<Text>().color = Color.white;
            }
            if (TotalTime <= 0.5 * maxtime && TotalTime > 0.1 * maxtime)//半分以下、十分の一以上、黄色
            {
                text.GetComponent<Text>().color = Color.yellow;
            }
            if (TotalTime <= 0.1 * maxtime)//十分の一以下、赤色
            {
                text.GetComponent<Text>().color = Color.red;
            }
            //分、秒を分けて表示する
            text.GetComponent<Text>().text = string.Format("{0:d2}:{1:d2}", minute, second);//分、秒の形表示
        }
        if (timecount > 1)//累計時間1秒以上
        {
            timecount -= 1;//秒カウンターのリセット
            TotalTime -= 1;//残る時間の計算
            TimeConutUp += 1;//ゲーム時間の計算
        }
    }
}
