using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunsControl : MonoBehaviour
{
    //バンスコントロール
    //運動中のバンスを管理する関数


    //総管理するハンバーガーコントロールにつなぐ
    public HamburgerControl masterBurger;


    //ドラッグは終わりました？
    public bool isEndDrag;

    //飛びこむ中心点
    public GameObject center;

    //移動速度
    public float moveSpeed;

    //二つのバンスをコントロールする角度
    public float angle;

    private void Awake()
    {
        
    }

    private void FixedUpdate()
    {
        this.transform.eulerAngles = (new Vector3(0.0f, 0.0f, Mathf.Rad2Deg * angle - 90.0f));
        if (!isEndDrag)
        {
            //終わってないなら角度を調整する
            this.transform.eulerAngles = (new Vector3(0.0f, 0.0f, Mathf.Rad2Deg * angle - 90.0f));
        }

        if (isEndDrag&& !masterBurger.isComplete)
        {
            //終わったら中心点へ移動
            this.transform.position = Vector2.Lerp(this.transform.position,center.transform.position,Time.deltaTime * moveSpeed);
        }

        if (masterBurger.isComplete)
        {
            //もし接続して終わったら用済み、物理演算をさせない
            this.GetComponent<Rigidbody2D>().simulated = false;
            angle = masterBurger.angle;
            this.transform.eulerAngles = (new Vector3(0.0f, 0.0f, Mathf.Rad2Deg * angle - 90.0f));

        }

    }


    //衝突関数
    private void OnCollisionEnter2D(Collision2D collider)
    {
        //二つのパートを分配

        if(collider.transform.tag == "Bottom"|| collider.transform.tag == "Top")
        {
            //下が上につける、総管理の完成フラグをtrueにする
            masterBurger.EnterJudge();

            //場所を調整
            masterBurger.LineUp();
            masterBurger.angle = angle;
            masterBurger.completeCnt = Time.time;

        }

        if (collider.transform.tag == "Ingredient" && collider.transform.parent.parent != masterBurger.transform && collider.gameObject.GetComponent<IngrediantControl>().isBelongBuns == false)
        {
            //食材と接続、
            //やることは：
            //1.総管理に番号を渡す
            //2.食材の親子関係を設置する（描画するため）
            //3.食材の角度を調整
            //4.このバンスにつける設定を食材コントロールに渡す

            //総管理に番号を渡す
            masterBurger.PlusIng(collider.gameObject.GetComponent<IngrediantControl>().ingNo);

            //食材の親子関係を設置する（描画するため）
            collider.transform.SetParent(center.transform);

            collider.gameObject.GetComponent<PolygonCollider2D>().isTrigger = true;

            //食材の角度を調整
            collider.transform.eulerAngles = (new Vector3(0.0f, 0.0f, Mathf.Rad2Deg * angle - 90.0f));
            collider.gameObject.GetComponent<Rigidbody2D>().freezeRotation = true;

            //このバンスにつける設定を食材コントロールに渡す
            collider.gameObject.GetComponent<IngrediantControl>().attach = this.gameObject;
            collider.gameObject.GetComponent<IngrediantControl>().isBelongBuns = true;

        }



    }
}
