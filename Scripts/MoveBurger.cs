using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBurger : MonoBehaviour
{
    //ハンバーガーを移動するスクリプト
    //ハンバーガーコントロールと分別して管理


    //目標皿
    RectTransform target;
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

    public void Active(RectTransform termination)
    {
        isActivated = true;
        target = termination;
        moveSpeed = 5f;
        shrink = 0.5f;
       

    }

    void MoveToDish()
    {
        RectTransform thisObj;
        thisObj = this.gameObject.GetComponent<RectTransform>();

        thisObj.localPosition = Vector2.Lerp(thisObj.localPosition, new Vector2(target.localPosition.x, target.localPosition.y - thisObj.localScale.y), Time.deltaTime*moveSpeed);
        thisObj.localScale = Vector2.Lerp(thisObj.localScale, originalSize * shrink, Time.deltaTime*moveSpeed);

        moveCnt += Time.deltaTime;

        if(moveCnt > 1.0f)
        {
            if(!target.gameObject.GetComponent<DishCotrol>())
            {
                target.gameObject.GetComponent<Animation>().Play();
            }
            else
            {
                target.gameObject.GetComponent<DishCotrol>().isOut = true;
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
