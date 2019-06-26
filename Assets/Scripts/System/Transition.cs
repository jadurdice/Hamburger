using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    //シーン遷移用マシーン「ビッグバーガーIII」スクリプト
    //全部のシーン遷移はビッグバーガーIIIにお任せあれ

    //このスクリプトにつけたオブジェクトのアニメーター
    public Animator transitionAnim;

    private void Start()
    {
        //開始する時に、フェードアウトフラグを消して、音量を設置し直して、プレイする
        //命名規則は"B_"+"シーン名"

        FindObjectOfType<AudioManeger>().fadeOutFlag[SceneManager.GetActiveScene().buildIndex] = false;
        FindObjectOfType<AudioManeger>().SetVolume("B_" + SceneManager.GetActiveScene().name, 0.3f);
        FindObjectOfType<AudioManeger>().Play("B_" + SceneManager.GetActiveScene().name);
    }

    //GoToシーリズ
    //指定のシーンへ行く
    public void GoToPlay()
    {
        
        StartCoroutine(LoadScene("Play"));
        
    }
    public void GoToRank()
    {
        
        StartCoroutine(LoadScene("Rank"));
    }
    public void GoToTitle()
    {
        StartCoroutine(LoadScene("Title"));
        ScoreAndTime.scoreToNext = 0;
    }
    public void GoToEnd()
    {
        transitionAnim.SetTrigger("end");
        new WaitForSeconds(1.5f);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;//unityで使う場合
#else
                 Application.Quit(); //exeの場合 
#endif

    }

    //UIのクリック音
    //たぶん他の処でやった方がいい
    public void ClickOnAudio()
    {
        FindObjectOfType<AudioManeger>().Play("S_systemOn");
    }

    public void ClickDownAudio()
    {
        FindObjectOfType<AudioManeger>().Play("S_systemClick");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    //パクったIEnumerator
    //実際的にはなんのことがあったのかわからない
    IEnumerator LoadScene(string sceneName)
    {
        FindObjectOfType<AudioManeger>().fadeOutFlag[SceneManager.GetActiveScene().buildIndex] = true;
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(sceneName);
    }
}
