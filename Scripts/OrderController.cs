using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class OrderController : MonoBehaviour
{
    //プレイヤーの完成バーガースクリプト参照
    //public Humburger humburger;

    

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

    //完成物と注文票の比較判定フラグ
    bool[] isMatch = new bool[ORDER_NUM] { false, false, false };

    void Start()
    {
        
        //HumberList1 = new List<int>();
    }

    // Update is called once per frame
    void Update()
    {
        //時間計測
        TimeCnt += Time.time;
        if (TimeCnt >= 1.0f) {
            Second += 1;
        }
       
        //注文票生成
        for(int i = 0; i < ORDER_NUM; i++) {
            if(isOrder[i] != false) {
                continue;
            }
        }
        if (isOrder[0] == false) {
            for (int i = 0; i < FOOD_NUM; i++) {
                HumberList1.Add(Random.Range(FoodMin, FoodMax + 1));//材料末尾ＩＤ＋１
            }
            isOrder[0] = true;
        }
        if (isOrder[1] == false) {
            for (int i = 0; i < FOOD_NUM; i++) {
                HumberList1.Add(Random.Range(FoodMin, FoodMax + 1));//材料末尾ＩＤ＋１
            }
            isOrder[1] = true;
        }
        if (isOrder[2] == false) {
            for (int i = 0; i < FOOD_NUM; i++) {
                HumberList1.Add(Random.Range(FoodMin, FoodMax + 1));//材料末尾ＩＤ＋１
            }
            isOrder[2] = true;
        }
        //注文票判定

        //受け取ったバーガーをソート
        var HumList = new List<int>();
        HumList.AddRange(Humber);
        HumList.Sort();//{ 1, 2, 3 }の順番になる

        //注文票もソート
        var HumList1 = new List<int>();//判定用リスト生成
        var HumList2 = new List<int>();
        var HumList3 = new List<int>();
        HumList.AddRange(HumberList1);//現在のリストを渡す
        HumList.AddRange(HumberList2);
        HumList.AddRange(HumberList3);
        HumList1.Sort();
        HumList2.Sort();
        HumList3.Sort();

        for (int i = 0; i < ORDER_NUM; i++) {
            for (int j = 0; j < FOOD_NUM; j++) {
                if(HumList[j] == HumList1[j]) {
                    isMatch[j] = true;
                }
                if (HumList[j] == HumList2[j]) {
                    isMatch[j] = true;
                }
                if (HumList[j] == HumList3[j]) {
                    isMatch[j] = true;
                }
                if (HumList[j] != HumList1[j]) {
                    isMatch[j] = false;
                }
                if (HumList[j] != HumList2[j]) {
                    isMatch[j] = false;
                }
                if (HumList[j] != HumList3[j]) {
                    isMatch[j] = false;
                }



            }
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
