using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class OrderController : MonoBehaviour
{
    //オーダーコントロール
    //システム面の一番偉いスクリプト

    //スコアスクリプト参照
    public ScoreAndTime score;

    //ビッグバーガーIII参照
    public Transition changer;

    //タイムを計測する変数
    float TimeCnt;

    const int ORDER_NUM = 3;//注文票数

    const int FOOD_NUM = 3;//注文票食材数

    public int LimitTime;//注文票制限時間

    //仮判定用ハンバーガー
    int[] Humber = new int[FOOD_NUM] { 1, 2, 3 };

    //ハンバーガー注文票
    public List<DishCotrol> orderList;

    public int FoodMin = 1;//材料先頭ＩＤ
    public int FoodMax = 5;//材料末尾ＩＤ

    //各注文票時間計測 時間経過でisOrderをfalse
    int[] time = new int[ORDER_NUM] { 1, 2, 3 };

    //今のタイム
    public int playTime;

    //開始前と終了後
    public int extraTime;

    //注文票の状態 falseになったら注文生成
    bool[] isOrder = new bool[ORDER_NUM] { false, false, false };

    //開始した？
    public bool isStart;

    float fade = 0.4f;

    public int IngrediantJudge(List<int> checkBurger)
    {
        //食材を判定する関数

        //仕方は
        //1.引数で入れたバーガーの食材番号をチェック
        //2.食材がない、もしくは食材番号の中に0より小さい（ゴミ）があると失敗
        //3.参つのオーダーを順番で判定する

        if (checkBurger.Count == 0)
        {
            return -1;
        }

        foreach (int i in checkBurger)
        {
            if(i < 0)
            {
                return -1;
            }
        }

        for (int i = 0; i < 3; i++)
        {
            //判定用リスト生成
            int j;
            var checkOrder = new List<int> { };
            //注文を代入
            checkOrder = new List<int>(orderList[i].orderNow);

            if (checkOrder.Count == 0)
            {
                continue;
            }
            if (checkOrder.Count > checkBurger.Count)
            {
                //先に個数チェック、注文数が多い場合絶対失敗→スキップ
                checkOrder.Clear();
                continue;
            }

            for (int k = 0; k < checkBurger.Count; k++)
            {
                for (j = 0; j < checkOrder.Count; j++)
                {
                    //バーガーの素材確認→注文確認
                    //同じだったら、判定用リストのその食材の数字を0にする
                    //もともと0だったらスキップ
                    if (checkOrder[j] == 0)
                    {
                        continue;
                    }
                    if (checkBurger[k] == checkOrder[j])
                    {
                        checkOrder[j] = 0;
                    }
                }
            }

            for (j = 0; j < checkOrder.Count; j++)
            {
                //全部0なのかチェック
                if (checkOrder[j] != 0)
                {
                    break;
                }
            }

            if (j != checkOrder.Count)
            {
                //判定失敗
                checkOrder.Clear();
            }
            else
            {
                //判定成功、返す
                switch (orderList[i].orderNow.Count)
                {
                    case 1:
                        playTime += 3; break;
                    case 2:
                        playTime += 6; break;
                    case 3:
                        playTime += 15; break;
                }
                
                AddScore(orderList[i].orderNow.Count,checkBurger.Count);

                orderList[i].orderNow.Clear();

                return (i);
            }
        }

        return -1;
    }

    public void OrderGenerate(int slot)
    {

        //注文生成開始
        var orderGene = new List<int> { };

        //注文総数を確定
        //確率は、50%一個 40%二個 10%三個
        int percent = Random.Range(0, 100);
        int orderMax = 0;
        if(percent < 49)
        {
            orderMax = 1;
        }
        if(percent >=49 && percent < 90)
        {
            orderMax = 2;
        }
        if(percent >= 90)
        {
            orderMax = 3;
        }

        //注文詳細生成
        int i = 0;
        do
        {
            //食材番号をランダム生成
            int FoodCompare = Random.Range(FoodMin, FoodMax + 1);

            //なんもないなら直接入れる
            if (orderGene.Count == 0)
            {
                orderGene.Add(FoodCompare);
                i += 1;
            }
            else
            {
                //重複させないようにチェックする
                int checkCnt = 0;
                for (int j = 0; j < orderGene.Count; j++)
                {
                    if (FoodCompare == orderGene[j])
                    {
                        checkCnt += 1;
                    }
                }


                if (checkCnt == 0)
                {
                    orderGene.Add(FoodCompare);
                    i += 1;
                }

            }
        } while (i < orderMax);

        orderGene.Sort();
        orderList[slot].orderNow = new List<int>(orderGene);
        orderList[slot].isIn = true;
    }

    public void AddScore(int orderCount,int burgerCount)
    {
        //スコアスクリプトに増加するスコアを渡す
        score.scoreNum += (10 * orderCount * burgerCount);
    }

    void SetTimer(int orderSlot, int second)
    {
        //注文スロットのタイムを設定
        time[orderSlot] = second;
        orderList[orderSlot].time = second;
    }

    void TimeCount()
    {

        //タイム計測
        //要領はフレーム計測と同じ
        float realTime = 0;
        realTime = Time.realtimeSinceStartup;
        realTime -= TimeCnt;

       

        if (realTime > 1.0f)
        {
            //一秒経過
            TimeCnt = Time.realtimeSinceStartup;

            if (!isStart)
            {

                extraTime += 1;
                Debug.Log(extraTime);
                if (extraTime >5)
                {
                    isStart = true;
                }

            }
            else
            {

                
                playTime -= 1;
                if (playTime <= 0)
                {
                    //ゲーム終了
                    playTime = 0;

                    extraTime += 1;



                    if(extraTime > 9)
                    {
                        changer.GoToRank();
                    }
                }
                score.timeNum = playTime;

                for (int i = 0; i < 3; i++)
                {
                    time[i] -= 1;

                    if (orderList[i].time <= 0)
                    {
                        time[i] -= 10;
                    }
                    else
                    {
                        orderList[i].time = time[i];
                    }

                    if (time[i] <= 0)
                    {

                        if (isOrder[i])
                        {
                            //オーダー時間切れ
                            isOrder[i] = false;
                            orderList[i].orderNow.Clear();
                            orderList[i].isOut = true;

                            SetTimer(i, LimitTime / 2);
                            orderList[i].time = 0;
                        }
                        else
                        {
                            //注文作成
                            isOrder[i] = true;
                            OrderGenerate(i);
                            SetTimer(i, LimitTime);
                        }

                    }

                }


            }
            
        }
        
    }

    private void Start()
    {
        //ドラッグする時に、時間計測を一緒に遅くさせないように、realtimeSinceStartupにする
        TimeCnt = Time.realtimeSinceStartup;
    }

    void Update()
    {
        //時間計測
        TimeCount();
        score.timeNum = playTime;
        if(extraTime >= 8)
        {
            //終了時音楽のフェードアウト用
            FindObjectOfType<AudioManeger>().fadeOutFlag[1] = true;
            fade *= 0.95f;
        }
    }

    void debugOrder(List<int> order)
    {
        //ListはUnityのデフォルトデバッグログなら内容は反応しないので、内容を反応するようにする。
        string debugstr = "Order :";
        foreach (int i in order)
        {
            debugstr = debugstr + i + ",";
        }
        Debug.Log(debugstr);
    }
}
