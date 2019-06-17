using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
     
    }

    //開始ボタンを押すと、ゲームスタート
    public void Click_start()
    {
        SceneManager.LoadScene("SampleScene");
    }

    //退出ボタンを押すと、退出
    public void Click_end()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;//unityで使う場合
        #else
              Application.Quit();  //exeの場合
        #endif
    }

    public void Click_return()
    {
        SceneManager.LoadScene("Title");
    }
    public void Click_explain()
    {
        SceneManager.LoadScene("Explain");
    }
    public void Click_rank()
    {
        SceneManager.LoadScene("Rank");
    }

    // Update is called once per frame
    void Update()
    {
        //ESCでゲームを退出することも可能
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;//unityで使う場合
#else
                 Application.Quit(); //exeの場合 
#endif
        }

    }
}
