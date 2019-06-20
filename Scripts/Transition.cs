using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    public Animator transitionAnim;

    static int prevScene;

    // Update is called once per frame
    void Update()
    {
        
    }

    void SavePrevScene()
    {
        prevScene = SceneManager.GetActiveScene().buildIndex;
    }

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

    IEnumerator LoadScene(string sceneName)
    {
        SavePrevScene();
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(sceneName);
    }
}
