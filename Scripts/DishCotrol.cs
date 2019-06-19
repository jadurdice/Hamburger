using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DishCotrol : MonoBehaviour
{
    //皿のコントロール
    //注文が入ったらアニメーションをプレイ
    //注文の描画など

    //注文管理スクリプト参照
    public List<int> orderNow;

    Animator thisAnimator;

    public Slider pop;
    public bool isIn;
    public bool isOut;

    public TextMeshProUGUI text;
    public int time;

    void Start()
    {
        thisAnimator = this.gameObject.GetComponent<Animator>();   
    }

    // Update is called once per frame
    void Update()
    {

        pop.value = time;

        if (isOut)
        {
            
            thisAnimator.SetBool("isOut", true);
            thisAnimator.SetBool("isIn", false);
            isOut = false;

            time = 0;


        }

        if (isIn)
        {
            thisAnimator.SetBool("isOut", false);
            thisAnimator.SetBool("isIn", true);

            isIn = false;
        }

    }

    public void textOrder(List<int> order)
    {
        string showStr   = "";


        foreach (int i in order)
        {
            showStr = showStr + i + ",";
        }
        text.SetText(showStr);
    }
}
