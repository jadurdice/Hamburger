using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HamburgerControl : MonoBehaviour
{

    //ハンバーガー全体コントロール
    //今接続している食材を管理

    public OrderController masterOrder;

    //食材のList
    public List<int> ingrediantIncome;

    //もう両面挟んだ？
    public bool isComplete;

    //準備
    public GameObject thisBottom;
    public GameObject thisCenter;
    public GameObject thisTop;

    //目標の皿までたどりつく
    public List<GameObject> dish;

    //合体した後の角度
    public float angle;

    //回転の速度
    public float rotSpeed;

    //完成カウンター
    public float completeCnt;

    RectTransform gotoDish;

    public void EnterJudge()
    {
        gotoDish = dish[(masterOrder.IngrediantJudge(ingrediantIncome) + 1)].GetComponent<RectTransform>();
        Debug.Log(gotoDish);
    }

    public void LineUp()
    {
        //整列用
        //オブジェクトは下の方が表に出るから、完成した時は一度解体して整列をする

        //解体
        thisBottom.transform.SetParent(this.transform.parent);
        thisCenter.transform.SetParent(this.transform.parent);
        thisTop.transform.SetParent(this.transform.parent);

        //解体後で座標、角度調整
        this.gameObject.transform.eulerAngles = (new Vector3(0.0f, 0.0f, Mathf.Rad2Deg * angle - 90));
        thisCenter.transform.eulerAngles = (new Vector3(0.0f, 0.0f, Mathf.Rad2Deg * angle - 90));
        this.gameObject.transform.position = thisCenter.transform.position;

        //整列
        thisBottom.transform.SetParent(this.transform);
        thisCenter.transform.SetParent(this.transform);
        thisTop.transform.SetParent(this.transform);
    }

    void adjustAngle()
    {
        //完成したら一度90度にする、そのための角度計算
        angle = Mathf.Lerp(angle,Mathf.Deg2Rad * 90,Time.deltaTime*rotSpeed);

        thisCenter.transform.eulerAngles = (new Vector3(0.0f, 0.0f, Mathf.Rad2Deg * angle-90));
        this.gameObject.transform.eulerAngles = (new Vector3(0.0f, 0.0f, Mathf.Rad2Deg * angle - 90));

        //完成カウンターしたら
        if (Time.time >= completeCnt + 1.0f)
        {
            GetComponent<MoveBurger>().Active(gotoDish);
        }
        else
        {
            this.gameObject.transform.position = Vector2.Lerp(this.gameObject.transform.position, this.gameObject.transform.parent.position, Time.deltaTime * rotSpeed);
        }
    }

    void OnBecameInvisible()
    {
        //見えなくなったら
        Destroy(gameObject);
    }

    private void Update()
    {
        //完成したら
        if (isComplete)
        {
            adjustAngle();
        }

        if(this.gameObject.transform.position.x < -50　|| this.gameObject.transform.position.x > 1000)
        {
            Destroy(gameObject);
        }

    }

    //食材と接続したとき、食材番号をlistに入れる
    public void PlusIng(int ingNo)
    {
        //番号を入れたらソートする
        ingrediantIncome.Add(ingNo);
        ingrediantIncome.Sort();
    }

    //Listデバッグ用
    void debugIngre()
    {
        string debugstr = "Ingrdiant :";


        foreach (int i in ingrediantIncome)
        {
            debugstr = debugstr + i + ",";
        }
        Debug.Log(debugstr);
    }
}
