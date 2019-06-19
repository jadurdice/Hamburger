using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopControl : MonoBehaviour
{
    //発射口コントロール
    //食材を射出のスポーン地点

    //発射する準備ができました？
    public bool isReadyPop;

    //全部の食材を入れる
    public List<GameObject> Ingrediant;

    //ドラッグゾーン
    public GameObject dragZone;

    GameObject spawnIng;

    float timeCnt;
    float nowTime;

    float fallenSpeed = 20.0f;

    void PopIng()
    {
        int tempNo = Random.Range(0, Ingrediant.Count);

        spawnIng = Instantiate(Ingrediant[tempNo], dragZone.transform);
        spawnIng.transform.position = this.transform.position;
        spawnIng.GetComponent<Rigidbody2D>().gravityScale = fallenSpeed;

    }


    void Update()
    {



        nowTime = Time.time;

        if (nowTime > timeCnt + 1.0f)
        {
            int ran = Random.Range(0, 100);

            if (ran < 50)
            {
                isReadyPop = true;
            }

            timeCnt = Time.time;
        }



        if (isReadyPop)
        {
            PopIng();
            isReadyPop = false;
        }

    }
}
