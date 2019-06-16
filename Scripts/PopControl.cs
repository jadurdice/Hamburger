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

    void PopIng()
    {
        int tempNo = Random.Range(0, Ingrediant.Count);

        spawnIng = Instantiate(Ingrediant[tempNo],dragZone.transform);
        spawnIng.transform.position = this.transform.position;
    }


    void Update()
    {
        if (isReadyPop)
        {
            PopIng();
            isReadyPop = false;
        }
        
    }
}
