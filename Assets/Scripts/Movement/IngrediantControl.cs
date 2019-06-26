    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngrediantControl : MonoBehaviour
{
    //食材コントロール（というよりクラス）
    //汎用できるため関数少なめ

    //食材番号
    public int ingNo;

    //バンスについたか？
    public bool isBelongBuns;

    //つけたバンスのGameObject
    public GameObject attach;

    float timeCnt;

    private void Start()
    {
        timeCnt = Time.time;
    }

    private void Update()
    {
        if (isBelongBuns)
        {
            //バンスとついていく
            this.transform.position = this.transform.parent.position;

        }
        else
        {
            if(Time.time > timeCnt + 10.0f)
            {
                Destroy(gameObject);
            }
        }
    }

}
