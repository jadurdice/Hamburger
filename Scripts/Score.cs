using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{

    public Text scoreText;//スコア

    public int scoreNum = 0;//スコア変数

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "$" + scoreNum.ToString();
    }
}
