using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawLine : MonoBehaviour
{
    //線を描く
    //線というより細長い画像
    //回転角度を計算して、中心点をハンズの中に入れるだけ
    //基本はハンバーガーの伸ばし

    public Image arrow; 
    public RectTransform pntStart; 
    public RectTransform pntEnd; 

    void Update()
    {
        arrow.transform.position = Vector3.Lerp(pntStart.transform.position,pntEnd.transform.position,0.5f);
        arrow.transform.localRotation = Quaternion.AngleAxis(-GetAngle(), Vector3.forward);

        var distance = Vector2.Distance(pntEnd.anchoredPosition, pntStart.anchoredPosition);
        arrow.rectTransform.sizeDelta = new Vector2(50, Mathf.Max(1, distance - 30));
    }


    public float GetAngle()
    {
        var dir = pntEnd.position - pntStart.position;
        var dirV2 = new Vector2(dir.x, dir.y);
        var angle = Vector2.SignedAngle(dirV2, Vector2.down);
        return angle;
    }
}


