using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBurger : MonoBehaviour
{
    //ハンバーガーを移動するスクリプト
    //ハンバーガーコントロールと分別して管理

    //目標皿
    Transform target;
    //原寸
    Vector2 originalSize;
    //トリガー
    bool isActivated;
    //移動速度
    float moveSpeed;
    //縮小係数
    float shrink;
    //移動カウンター
    float moveCnt;

    public void Active(Transform termination)
    {
        //移動開始関数

        isActivated = true;
        target = termination;
        moveSpeed = 5f;
        shrink = 0.5f;
  　}

    void MoveToDish()
    {
        //目標地点に向かって移動、縮小

        Transform thisObj = this.gameObject.transform;

        thisObj.position = Vector2.Lerp(thisObj.position, target.position, Time.deltaTime*moveSpeed);
        thisObj.localScale = Vector2.Lerp(thisObj.localScale, originalSize * shrink, Time.deltaTime*moveSpeed);

        moveCnt += Time.deltaTime;

        if(moveCnt > 1.0f)
        {
            //一秒経過
            //目標皿を動かす
            if(!target.parent.GetComponent<DishCotrol>())
            {
                target.parent.GetComponent<Animation>().Play();
            }
            else
            {
                target.parent.GetComponent<DishCotrol>().isOut = true;
            }
        }

    }

    private void Start()
    {
        originalSize = this.gameObject.transform.localScale;
    }

    void Update()
    {
        if (isActivated)
        {
            MoveToDish();
        }
    }
}
