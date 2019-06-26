using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorControl : MonoBehaviour
{
    //カーソルコントローラースクリプト
    //カーソルの画像とクリックする点を変わる

    public Texture2D def;
    public Vector2 hotspot;

    void Start()
    {
        //カーソルを画面内に留まるようにする
        Cursor.lockState = CursorLockMode.Confined;

        //消さないようにする
        DontDestroyOnLoad(gameObject);

        //クリックの点を変わる
        //実際的には、デフォルトカーソルの先っぽは画像の中心点にあるので、ちゃんと左上にあるように調整
        hotspot = def.texelSize * 0.5f;
        hotspot.y *= -1;
        
        Cursor.SetCursor(def,hotspot,CursorMode.ForceSoftware);

    }
    
}
