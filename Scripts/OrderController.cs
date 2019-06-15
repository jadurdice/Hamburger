using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class OrderController : MonoBehaviour
{
    //プレイヤーの完成バーガースクリプト参照
    //public Humburger humburger;


    const int ONE_MATCH   = 10;
    const int TWO_MATCH   = 20;
    const int THREE_MATCH = 30;

    //注文票のスクリプト参照
    public Order1 order1;
    public Order2 order2;
    public Order3 order3;

    //スコアスクリプト参照
    public Score score;

    float TimeCnt;

    int Second;

    const int ORDER_NUM = 3;//注文票数

    const int FOOD_NUM  = 3;//注文票食材数

    public int LimitTime;//注文票制限時間


    //仮判定用ハンバーガー
    int[] Humber = new int[FOOD_NUM] { 1, 2, 3 };

    //ハンバーガー注文票
    public List<int> HumberList1;
    public List<int> HumberList2;
    public List<int> HumberList3;

    public int FoodMin = 1;//材料先頭ＩＤ
    public int FoodMax = 3;//材料末尾ＩＤ

    //各注文票時間計測 時間経過でisOrderをfalse
    int[] time = new int[ORDER_NUM] { 0, 0, 0 };

   

    //注文票の状態 falseになったら注文生成
    bool[] isOrder = new bool[ORDER_NUM] { false, false, false };

    public void IngrediantJudge(List<int> Humburger) 
    {
        //判定の内容
        var HumList = new List<int>();
        HumList.AddRange(Humber);
        HumList.Sort();

        var HumList1 = new List<int>();//判定用リスト生成
        HumList1.AddRange(HumberList1);
        HumList1.Sort();
        var HumList2 = new List<int>();//判定用リスト生成
        HumList2.AddRange(HumberList2);
        HumList2.Sort();
        var HumList3 = new List<int>();//判定用リスト生成
        HumList3.AddRange(HumberList3);
        HumList3.Sort();

        //注文票１と判定
        for (int i = 0; i < HumList.Count; i++) {
            if (HumList[i] == HumList1[i]) {
                HumList[i] = 0;
            }
        }
        for (int i = 0; i < HumList.Count; i++) {
            if (HumList[i] != 0) {
                break;
            }
            if(i == HumList.Count - 1 && HumList[i] == 0) {

                HumberList1.Clear();
                isOrder[0] = false;
                //スコア加算
                if (HumList.Count == 1) {
                    score.scoreNum += ONE_MATCH;
                }
                if (HumList.Count == 2) {
                    score.scoreNum += TWO_MATCH;
                }
                if (HumList.Count == 3) {
                    score.scoreNum += THREE_MATCH;
                }
                return;
            }
        }

        //注文票２と判定
        for (int i = 0; i < HumList.Count; i++) {
            if (HumList[i] == HumList2[i]) {
                HumList[i] = 0;
            }
        }
        for (int i = 0; i < HumList.Count; i++) {
            if (HumList[i] != 0) {
                break;
            }
            if (i == HumList.Count - 1 && HumList[i] == 0) {

                HumberList2.Clear();
                isOrder[1] = false;
                if (HumList.Count == 1) {
                    score.scoreNum += ONE_MATCH;
                }
                if (HumList.Count == 2) {
                    score.scoreNum += TWO_MATCH;
                }
                if (HumList.Count == 3) {
                    score.scoreNum += THREE_MATCH;
                }
                return;
            }
        }

        //注文票３と判定
        for (int i = 0; i < HumList.Count; i++) {
            if (HumList[i] == HumList3[i]) {
                HumList[i] = 0;
            }
        }
        for (int i = 0; i < HumList.Count; i++) {
            if (HumList[i] != 0) {
                break;
            }
            if (i == HumList.Count - 1 && HumList[i] == 0) {

                HumberList3.Clear();
                isOrder[2] = false;
                if (HumList.Count == 1) {
                    score.scoreNum += ONE_MATCH;
                }
                if (HumList.Count == 2) {
                    score.scoreNum += TWO_MATCH;
                }
                if (HumList.Count == 3) {
                    score.scoreNum += THREE_MATCH;
                }
                return;
            }
        }

        return;

        //ハンバーガーのlistと注文判定
    } 

    public List<int> OrderGenerate() {

        //注文生成開始
        var orderGene = new List<int> { };

        //注文総数を確定
        int orderMax = Random.Range(1,3);

        //注文詳細生成
        for(int i = 0; i < orderMax; i++) {

            do {
                int FoodCompare = Random.Range(FoodMin,FoodMax);
                int j;
                for (j = 0; j <= orderGene.Count; j++) {
                    if (FoodCompare == orderGene[i]) {
                        break;
                    }
                }
                if (FoodCompare == orderGene.Count) {

                    //全部被ってなければループ脱出
                    orderGene.Add(FoodCompare);
                    break;
                }
            } while (true);
        }
        return orderGene;
    }

    void Start()
    {
        //judge.GetComponent<OrderController>().IngrediantJudge(HumberList1);
        //HumberList1 = new List<int>();
    }

    // Update is called once per frame
    void Update()
    {
        //時間計測
        TimeCnt += Time.time;
        if (TimeCnt >= 1.0f) {
            Second += 1;
            TimeCnt = 0;
        }

        //注文票生成
        if (isOrder[0] == false) {
            HumberList1.AddRange(OrderGenerate());
            isOrder[0] = true;
        }
        if (isOrder[1] == false) {
            HumberList2.AddRange(OrderGenerate());
            isOrder[1] = true;
        }
        if (isOrder[2] == false) {
            HumberList3.AddRange(OrderGenerate());
            isOrder[2] = true;
        }

        //注文票経過時間カウント
        for (int i = 0; i < ORDER_NUM; i++) {
            if (isOrder[i] != true) {
                continue;
            }
            time[i] += Second;
            if(time[i] >= LimitTime) {

                //注文票時間切れ
                isOrder[i] = false;
                time[i]    = 0;//計測時間初期化
            }
        }
    }
}
