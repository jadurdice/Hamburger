using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HamburgerControl : MonoBehaviour
{

    //ハンバーガー全体コントロール
    //今接続している食材を管理

    //食材のList
    public List<int> ingrediantIncome;

    //もう両面挟んだ？
    public bool isComplete;

    //準備
    public GameObject thisBottom;
    public GameObject thisCenter;
    public GameObject thisTop;

    public float angle;

    public float rotSpeed;


    public void LineUp()
    {
        thisBottom.transform.SetParent(this.transform.parent);
        thisCenter.transform.SetParent(this.transform.parent);
        thisTop.transform.SetParent(this.transform.parent);

        this.gameObject.transform.eulerAngles = (new Vector3(0.0f, 0.0f, Mathf.Rad2Deg * angle - 90));
        thisCenter.transform.eulerAngles = (new Vector3(0.0f, 0.0f, Mathf.Rad2Deg * angle - 90));

        this.gameObject.transform.position = thisCenter.transform.position;



        thisBottom.transform.SetParent(this.transform);
        thisCenter.transform.SetParent(this.transform);
        thisTop.transform.SetParent(this.transform);
    }

    void adjustAngle()
    {
        angle = Mathf.Lerp(angle,Mathf.Deg2Rad * 90,Time.deltaTime);

      thisCenter.transform.eulerAngles = (new Vector3(0.0f, 0.0f, Mathf.Rad2Deg * angle-90));
        this.gameObject.transform.eulerAngles = (new Vector3(0.0f, 0.0f, Mathf.Rad2Deg * angle - 90));
        //thisBottom.transform.eulerAngles = Vector3.Lerp(this.transform.eulerAngles, new Vector3(0, 0, 0), Time.deltaTime);
        //thisCenter.transform.eulerAngles = Vector3.Lerp(this.transform.eulerAngles, new Vector3(0, 0, 90), Time.deltaTime);
        //thisTop.transform.eulerAngles = Vector3.Lerp(this.transform.eulerAngles, new Vector3(0, 0, 0), Time.deltaTime);
        //this.transform.eulerAngles = Vector3.Lerp(this.transform.eulerAngles, new Vector3(0, 0, 90), Time.deltaTime);
    }

    private void Update()
    {
        if (isComplete)
        {
            float t = Time.time;
            Debug.Log((int)t);
            adjustAngle();

        }



    }

    //食材と接続したとき、食材番号をlistに入れる
    public void PlusIng(int ingNo)
    {
        //番号を入れたらソートする
        ingrediantIncome.Add(ingNo);
        ingrediantIncome.Sort();

        //デバッグ用
        debugIngre();
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
