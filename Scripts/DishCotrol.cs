using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishCotrol : MonoBehaviour
{
    //皿のコントロール
    //注文が入ったらアニメーションをプレイ
    //注文の描画など

    //注文管理スクリプト参照
    public List<int> orderNow;

    Animator thisAnimator;

    public bool isIn;
    public bool isOut;

    void Start()
    {
        thisAnimator = this.gameObject.GetComponent<Animator>();   
    }

    // Update is called once per frame
    void Update()
    {

        if (isOut)
        {
            thisAnimator.SetBool("isOut", true);
            thisAnimator.SetBool("isIn", false);
            isOut = false;
        }

        if (isIn)
        {
            thisAnimator.SetBool("isOut", false);
            thisAnimator.SetBool("isIn", true);
            isIn = false;
        }

    }
}
