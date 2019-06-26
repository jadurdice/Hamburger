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
    public Transform showZone;
    public List<GameObject> ingreSlot;
    
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
            FindObjectOfType<AudioManeger>().Play("S_OrderSolve");
            thisAnimator.SetBool("isOut", true);
            thisAnimator.SetBool("isIn", false);
            isOut = false;
            showIngre(false);
            time = 0;


        }

        if (isIn)
        {
            FindObjectOfType<AudioManeger>().Play("S_OrderIn");
            thisAnimator.SetBool("isOut", false);
            thisAnimator.SetBool("isIn", true);
            showIngre(true);
            isIn = false;
        }

    }

    void showIngre(bool isNewOrder)
    {
        if (isNewOrder)
        {
            for (int i = 0; i < orderNow.Count; i++)
            {
                GameObject newIng;
                newIng = Instantiate(ingreSlot[(orderNow[i] - 1)],showZone);
                newIng.GetComponent<Rigidbody2D>().simulated = false;

            }

        }
        else
        {
            foreach (Transform n in showZone)
            {
                GameObject.Destroy(n.gameObject);
            }
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
