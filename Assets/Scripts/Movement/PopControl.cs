using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopControl : MonoBehaviour
{
    //0上1下2右3左
    public int popPos;

    //発射口コントロール
    //食材を射出のスポーン地点

    //発射する準備ができました？
    public bool isReadyPop;

    //全部の食材を入れる
    public List<GameObject> Ingrediant;
    public List<GameObject> Garbage;

    //ドラッグゾーン
    public GameObject dragZone;

    GameObject spawnIng;

    //時間計測用
    float timeCnt;
    float nowTime;

    //落ちるスピード
    float fallenSpeed = 40.0f;

    //移動速度
    float moveSpeed = 450.0f;

    //食材が出る確率
    int popPercent = 25;

    //毎ポップ点の噴出時間をずらすように使う
    float secPace;

    //噴出時の角度調整と回す調整
    float axisRange;
    int rotRange;


    //食材とゴミの生成
    void PopGarbage()
    {
        int tempNo = Random.Range(0, Garbage.Count);

        spawnIng = Instantiate(Garbage[tempNo], dragZone.transform);
        spawnIng.transform.position = this.transform.position;
        spawnIng.GetComponent<Rigidbody2D>().gravityScale = fallenSpeed;
        FindObjectOfType<AudioManeger>().Play("S_IngPop");
    }

    void PopIng()
    {
        int tempNo = Random.Range(0, Ingrediant.Count);

        spawnIng = Instantiate(Ingrediant[tempNo], dragZone.transform);
        spawnIng.transform.position = this.transform.position;
        spawnIng.GetComponent<Rigidbody2D>().gravityScale = fallenSpeed;
        FindObjectOfType<AudioManeger>().Play("S_IngPop");

    }

    //噴出口により、移動方向が変わるようにする
    void DownSide() {

        spawnIng.GetComponent<Rigidbody2D>().velocity = new Vector3(axisRange,2.5f,0.0f) * moveSpeed;
    }
    void RightSide() {
        axisRange -= 0.5f;
        spawnIng.GetComponent<Rigidbody2D>().velocity = new Vector3(1.0f,axisRange,0.0f) * -moveSpeed;
    }
    void LeftSide() {
        axisRange += 0.5f;
        spawnIng.GetComponent<Rigidbody2D>().velocity = new Vector3(1.0f, axisRange, 0.0f) * moveSpeed;
    }

  

    void Update()
    {
        //一定時間ごと噴出させるかの判定を行う
        //そしたら確率により噴出する

        int ran = 0;

        if (FindObjectOfType<OrderController>().isStart)
        {
            nowTime = Time.time;
            if (nowTime + secPace > timeCnt + 1.5f)
            {
                ran = Random.Range(0, 100);
                if (ran < popPercent)
                {
                    isReadyPop = true;
                }
                timeCnt = Time.time;
                secPace = Random.Range(0.0f, 0.5f);
            }
            if (isReadyPop)
            {
                axisRange = Random.Range(-0.25f, 0.25f);
                rotRange = Random.Range(0, 360);
                if (ran < popPercent*0.05f)
                {
                    PopGarbage();
                }
                else
                {
                    PopIng();
                }
                switch (popPos)
                {
                    case 0:
                        break;
                    case 1:
                        DownSide();
                        break;
                    case 2:
                        RightSide();
                        break;
                    case 3:
                        LeftSide();
                        break;
                }
                spawnIng.GetComponent<Rigidbody2D>().angularVelocity = rotRange;
                isReadyPop = false;
            }
        }
    }
}
