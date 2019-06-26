using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RankingControl : MonoBehaviour
{
    //ランキングコントロールスクリプト
    //ランキングを作る

    //表示する最大数
    const int maxShow = 5;

    //テキスト列
    //TMProを日本語が表示できる方法ができないから普通のテキストにする
    public Text nameText;
    public TextMeshProUGUI highNameText; //ハイスコアを表示するText
    public TextMeshProUGUI highScoreText; //ハイスコアを表示するText

    //PlayerPrebsを取り出すとか保存するとかで使う変数たち
    private int[] highScore = new int[maxShow]; //ハイスコア用変数
    private string[] highName = new string[maxShow]; //ハイスコア用変数
    private string nameKey = "name"; //ハイスコアの保存先キー
    private string scoreKey = "score"; //ハイスコアの保存先キー

    //inputFieldを参照して名前をもらう
    public GameObject inputField;

    //スコアが5名以上かな？
    //実はbool型関数でできるようかな？
    public bool needInput;

    //今回のスコア
    int thisGameScore;

    //入力した名前
    string inputName;

    void LoadRanking()
    {
        //ランキングを呼び出す
        //そして呼び出す内容をTMProに渡す

        string nameStr = "";
        string scoreStr = "";
        for (int i = 0; i < maxShow; i++)
        {
            nameStr = nameStr + (i+1).ToString() + " . " + PlayerPrefs.GetString(nameKey + (i+1).ToString()) +"\n";
            scoreStr = scoreStr + PlayerPrefs.GetInt(scoreKey + (i+1).ToString()) + "\n";
        }
        //保存しておいたハイスコアをキーで呼び出し取得
        //保存されていなければ0になる

        //ハイスコアを渡す
        nameText.text = nameStr;
        highScoreText.text = scoreStr;
    }
    
    public void InputEnd()
    {
        //入力が完了
        //名前を渡す、渡したら入力欄を消す
        needInput = false;
        ScoreSorting(maxShow - 1);
        Destroy(inputField);
    }

    public void InputChange()
    {
        //入力した名前を読み取り
        inputName = inputField.GetComponent<InputField>().text;
    }

    void ScoreSorting(int turn)
    {
  　    //スコアをソートさせる関数
        //上のランクより高い場合、一個一個のキーを呼び出す必要がある
        //比較は、4から0まで比較するようにする

        //今保存しているデータを呼び出す
        int checkScore = PlayerPrefs.GetInt(scoreKey + turn.ToString());
        string checkStr = PlayerPrefs.GetString(nameKey + turn.ToString());

        //もし今回のスコアがより多い場合
        if (thisGameScore > checkScore)
        {

            if (turn == 0)
            {
                //もう1位
                PlayerPrefs.SetInt(scoreKey + (turn + 1).ToString(), thisGameScore);
                PlayerPrefs.SetString(nameKey + (turn + 1).ToString(), inputName);
                return;
            }
            else
            {
                //壱位じゃない場合次にソートする
                //再帰型関数にする
                ScoreSorting(turn - 1);

            }

            //とりあえずこの順列のスコアを↓にセーブする
            PlayerPrefs.SetInt(scoreKey + (turn + 1).ToString(), checkScore);
            PlayerPrefs.SetString(nameKey + (turn + 1).ToString(), checkStr);
        }
        else
        {
            if (turn + 1 >= maxShow)
            {
                //6位の場合もうチェックしない
                return;
            } 
            
            //比較したら低いのでこのままでいい
            PlayerPrefs.SetInt(scoreKey + (turn + 1).ToString(), thisGameScore);
            PlayerPrefs.SetString(nameKey + (turn + 1).ToString(), inputName);
        }
    }

    void ScoreDefault()
    {
        //スコアのデフォルト化
        //先生方の頭文字を入れる

        for (int i = 0; i <= maxShow; i++)
        {
            PlayerPrefs.SetInt(scoreKey + (i + 1).ToString(), (300-50*i));

            string insert = "";

            switch (i)
            {
                case 0:
                    insert = "A.T"; break;
                case 1:
                    insert = "S.Y"; break;
                case 2:
                    insert = "K.K"; break;
                case 3:
                    insert = "H.T"; break;
                case 4:
                    insert = "N.Y"; break;
            }

            PlayerPrefs.SetString(nameKey + (i + 1).ToString(), insert);
        }

    }

    

    private void Start()
    {
        //スタート時

        //壱位のスコアが0の場合、全然データがないのでデフォルト化
        if(PlayerPrefs.GetInt(scoreKey + "1") == 0)
        {
            ScoreDefault();
        }

        //今回のスコアを獲得
        thisGameScore = ScoreAndTime.scoreToNext;

        //5位より高い場合、入力が必要
        if (thisGameScore > PlayerPrefs.GetInt(scoreKey + "5"))
        {
            needInput = true;
        }
        else
        {
            Destroy(inputField);
        }

    }

    private void Update()
    {
        if (!needInput)
        {
            //入力終了
            LoadRanking();
            //テキストをフェードイン
            highScoreText.color = Color.Lerp(highScoreText.color, Color.white, Time.deltaTime);
            nameText.color = Color.Lerp(nameText.color, Color.white, Time.deltaTime);
        }


    }
}
