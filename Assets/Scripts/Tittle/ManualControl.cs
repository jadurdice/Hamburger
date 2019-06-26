using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManualControl : MonoBehaviour
{   //マニュアルの操作スクリプト
    //showZone画像にこのスクリプトのSpriteが付けたやつを、固定のshowZoneの画像を変わるだけ。

    //全部のマニュアルの画像
    public List<Sprite> manual;

    //書き変わる領域を指定するshowZone
    public Image showZone;

    //今のページ数
    int nowPages;

    //ページ最大数
    int maxPages;

    //このスクリプトが付ける物の場所
    Vector2 targetPos;

    public void ManualIn()
    {   
        //画面の中に入る
        //同時に今のページ数をリセットする

        nowPages = 0;
        targetPos = Vector2.zero;
    }
    public void ManualOut()
    {
        //画面の中から出る
        targetPos = new Vector2(0.0f, 1000.0f);
    }


    //ページ変更
    //今のページ数を変わるだけ
    //循環できるように、最大数を超えた場合0になる

    public void NextPage()
    {

        nowPages += 1;

        if(nowPages > maxPages)
        {
            nowPages = 0;
        }
    }
    public void PreviousPage()
    {
        nowPages -= 1;
        if(nowPages < 0)
        {
            nowPages = maxPages;
        }
    }


    private void Start()
    {
        //Posを設定、このオブジェクトの基の場所を設定
        //これは、Updateで毎フレームこれの場所を更新するので、事前に設置
        targetPos = this.GetComponent<RectTransform>().anchoredPosition;

        //最大ページ数を設定
        //0から始まるので最大数の-1
        maxPages = manual.Count - 1;
    }

    private void Update()
    {
        //毎フレーム、目標の場所へ移動
        this.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(this.GetComponent<RectTransform>().anchoredPosition, targetPos, Time.deltaTime*5);

        //常にshowZoneのスプライトを、今のページ数を変わる。
        showZone.sprite = manual[nowPages];
    }

}
