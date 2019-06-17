using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class OrderController : MonoBehaviour
{
    //プレイヤーの完成バーガースクリプト参照
    //public Humburger humburger;


    const int ONE_MATCH = 10;
    const int TWO_MATCH = 20;
    const int THREE_MATCH = 30;

    //スコアスクリプト参照
    public Score score;

    float TimeCnt;

    const int ORDER_NUM = 3;//注文票数

    const int FOOD_NUM = 3;//注文票食材数

    public int LimitTime;//注文票制限時間

    //仮判定用ハンバーガー
    int[] Humber = new int[FOOD_NUM] { 1, 2, 3 };

    //ハンバーガー注文票
    public List<DishCotrol> orderList;

    public int FoodMin = 1;//材料先頭ＩＤ
    public int FoodMax = 3;//材料末尾ＩＤ

    //各注文票時間計測 時間経過でisOrderをfalse
    int[] time = new int[ORDER_NUM] { 1, 2, 3 };

    //注文票の状態 falseになったら注文生成
    bool[] isOrder = new bool[ORDER_NUM] { false, false, false };

    public int IngrediantJudge(List<int> checkBurger)
    {
        for (int i = 0; i < 3; i++)
        {
            //判定用リスト生成
            int j;
            var checkOrder = new List<int> { };
            //注文を代入、ソート
            checkOrder = new List<int>(orderList[i].orderNow);
 
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
                    //同じだったら0にする
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
        int orderMax = Random.Range(0, 4) ;

        //注文詳細生成
        int i = 0;
        do
        {

            int FoodCompare = Random.Range(FoodMin, FoodMax + 1);
            if (orderGene.Count == 0)
            {
                orderGene.Add(FoodCompare);
                i += 1;
            }
            else
            {

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
        debugOrder(orderGene);



        orderList[slot].orderNow = new List<int>(orderGene);
        
        orderList[slot].isIn = true;
    }

    public void AddScore(int HumberLen)
    {
        switch (HumberLen)
        {
            case 1: score.scoreNum += ONE_MATCH; break;
            case 2: score.scoreNum += TWO_MATCH; break;
            case 3: score.scoreNum += THREE_MATCH; break;
        }

    }

    void SetTimer(int orderSlot, int second)
    {
        time[orderSlot] += second;
    }

    void TimeCount()
    {
        TimeCnt += Time.deltaTime;

        if (TimeCnt >= 1.0f)
        {
            //一秒経過

            for (int i = 0; i < 3; i++)
            {
                time[i] -= 1;

                if (time[i] <= 0)
                {

                    if (isOrder[i])
                    {
                        //オーダー時間切れ
                        isOrder[i] = false;
                        orderList[i].orderNow.Clear();
                        orderList[i].isOut = true;

                        SetTimer(i, LimitTime / 2);
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

            TimeCnt = 0;
        }
    }

    private void Start()
    {

    }

    void Update()
    {
        //時間計測
        TimeCount();


    }

    void debugOrder(List<int> order)
    {
        string debugstr = "Order :";


        foreach (int i in order)
        {
            debugstr = debugstr + i + ",";
        }
        Debug.Log(debugstr);
    }
}
