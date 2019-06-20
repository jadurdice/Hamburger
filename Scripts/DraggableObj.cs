using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class DraggableObj : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    //ドラッグゾーンコントロール
    //マウスにドラッグできる領域をコントロールする

    OrderController masterOrder;

    //Prefab生成用のGameObject
    //各パーツ分別
    public GameObject Hamburger;
    public GameObject bottom;
    public GameObject center;
    public GameObject top;

    public Image backgroundMask;

    //ドラッグできる長さ制限
    public float length;

    //統括管理するのため指名できるための各パーツ
    GameObject actBurger;
    GameObject bottomDragging;
    GameObject centerDragging;
    GameObject topDragging;

    public List<GameObject> dish;

    //バンスの移動速度
    public float moveSpeed;

    float tempCnt;
    float slowFactor = 0.1f;

    bool isStart;

    void Start()
    {
        masterOrder = GetComponent<OrderController>();
    }

    void Update()
    {
        isStart = masterOrder.isStart;
    }

    //ドラッグ開始
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isStart)
        {


            //ドラッグ開始
            //やること：
            //1.ハンバーガーを生成（上中下統括）
            //2.ボトムの座標を指定
            //3.各パーツのスクリプトにGameObjectを渡す

            //ハンバーガーを生成（上中下統括）
            actBurger = Instantiate(Hamburger, this.transform);
            bottomDragging = Instantiate(bottom, actBurger.transform);
            centerDragging = Instantiate(center, actBurger.transform);
            topDragging = Instantiate(top, actBurger.transform);

            actBurger.GetComponent<HamburgerControl>().dish = dish;
            actBurger.GetComponent<HamburgerControl>().masterOrder = this.gameObject.GetComponent<OrderController>();

            actBurger.GetComponent<HamburgerControl>().thisBottom = bottomDragging;
            actBurger.GetComponent<HamburgerControl>().thisCenter = centerDragging;
            actBurger.GetComponent<HamburgerControl>().thisTop = topDragging;

            //ボトムの座標を指定
            bottomDragging.transform.position = eventData.position;

            //各パーツのスクリプトにGameObjectを渡す
            topDragging.GetComponent<BunsControl>().center = centerDragging;
            topDragging.GetComponent<BunsControl>().masterBurger = actBurger.GetComponent<HamburgerControl>();
            bottomDragging.GetComponent<BunsControl>().center = centerDragging;
            bottomDragging.GetComponent<BunsControl>().masterBurger = actBurger.GetComponent<HamburgerControl>();

            tempCnt = 0.0f;

        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (isStart)
        {
            //ドラッグ中
            //やること：
            //1.ボトムからトップまでの角度を計算してバンスコントロールに渡す
            //2.トップとセンターの座標を指定

            //ボトムからトップまでの角度を計算してバンスコントロールに渡す
            float targetAng = Mathf.Atan2(topDragging.transform.position.y - bottomDragging.transform.position.y, topDragging.transform.position.x - bottomDragging.transform.position.x);
            topDragging.GetComponent<BunsControl>().angle = targetAng;
            bottomDragging.GetComponent<BunsControl>().angle = targetAng;

            //トップとセンターの座標を指定
            centerDragging.transform.position = Vector2.Lerp(bottomDragging.transform.position, topDragging.transform.position, 0.5f);
            topDragging.transform.position = Vector3.Lerp(bottomDragging.transform.position, eventData.position, length);

            //スローにする
            Time.timeScale = Mathf.Lerp(1.0f, 0.2f, tempCnt);
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
            tempCnt += slowFactor;

            backgroundMask.color = new Color(0.0f, 0.0f, 0.0f, Mathf.Lerp(0.0f, 0.3f, tempCnt / 2));

            Debug.Log(Time.timeScale);
        }
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (isStart)
        {
            //ドラッグ終了
            //やること：
            //トップとボトムの→物理演算を許可、回転を固定、移動速度を渡す、ドラッグ終了フラグを渡す

            topDragging.GetComponent<Rigidbody2D>().simulated = true;
            topDragging.GetComponent<Rigidbody2D>().freezeRotation = true;
            topDragging.GetComponent<BunsControl>().isEndDrag = true;
            topDragging.GetComponent<BunsControl>().moveSpeed = moveSpeed;

            bottomDragging.GetComponent<Rigidbody2D>().simulated = true;
            bottomDragging.GetComponent<Rigidbody2D>().freezeRotation = true;
            bottomDragging.GetComponent<BunsControl>().isEndDrag = true;
            bottomDragging.GetComponent<BunsControl>().moveSpeed = moveSpeed;

            Time.timeScale = 1.0f;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;

            backgroundMask.color = Color.clear;

        }
    }
}


