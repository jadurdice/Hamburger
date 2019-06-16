using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngrediantControl : MonoBehaviour
{
    //食材コントロール
    //汎用できるため関数少なめ

    //食材番号
    public int ingNo;

    //バンスについたか？
    public bool isBelongBuns;

    //つけたバンスのGameObject
    public GameObject attach;

    private void Update()
    {
        if (isBelongBuns)
        {
            //バンスとついていく
            this.transform.position = this.transform.parent.position;
            

        }
    }

}
