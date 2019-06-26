using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartCountdown : MonoBehaviour
{
    //開始時のカウントダウンのスクリプト
    //真面目に考えてないから適当なスクリプト

    //秒数を捕捉できるように、オーダーコントロールを参照する
    public OrderController cnt;

    void Update()
    {
        //オーダーコントロールの中のextraTimeを参照して動く
        if(cnt.extraTime < 5)
        {
            //デカくなる
            this.gameObject.GetComponent<TextMeshProUGUI>().fontSize += Time.deltaTime * 60.0f;
        }

        //テキストの内容を変わる
        switch (cnt.extraTime)
        {
            case 1: this.gameObject.GetComponent<TextMeshProUGUI>().text = "Ready?"; FindObjectOfType<AudioManeger>().Play("S_CountDown"); break;
            case 2:this.gameObject.GetComponent<TextMeshProUGUI>().text = "3";
                break;
            case 3:this.gameObject.GetComponent<TextMeshProUGUI>().text = "2";         break;
            case 4:this.gameObject.GetComponent<TextMeshProUGUI>().text = "1";         break;
            case 5: this.gameObject.GetComponent<TextMeshProUGUI>().text = "GO!"; break;
            case 6: this.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(-3000, 0, 0); break;

                //終わる時スライドイン
            case 7:
                FindObjectOfType<AudioManeger>().Play("S_Finish");break;
            case 8:
                this.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(Mathf.Lerp(this.gameObject.GetComponent<RectTransform>().anchoredPosition.x, 0,Time.deltaTime * 10), 0, 0);
                this.gameObject.GetComponent<TextMeshProUGUI>().text = "TIME UP!"; break;
        }
    }
}
