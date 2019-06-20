using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartCountdown : MonoBehaviour
{
    // Start is called before the first frame update

    public OrderController cnt;

    

    // Update is called once per frame
    void Update()
    {
        if(cnt.extraTime < 5)
        {
            this.gameObject.GetComponent<TextMeshProUGUI>().fontSize += Time.deltaTime * 60.0f;
        }


        switch (cnt.extraTime)
        {
            case 1: this.gameObject.GetComponent<TextMeshProUGUI>().text = "Ready?"; break;
            case 2:this.gameObject.GetComponent<TextMeshProUGUI>().text = "3";       break;
            case 3:this.gameObject.GetComponent<TextMeshProUGUI>().text = "2";         break;
            case 4:this.gameObject.GetComponent<TextMeshProUGUI>().text = "1";         break;
            case 5: this.gameObject.GetComponent<TextMeshProUGUI>().text = "GO!"; break;
            case 6: this.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(-3000, 0, 0); break;
            case 7:
                this.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(Mathf.Lerp(this.gameObject.GetComponent<RectTransform>().anchoredPosition.x, 0,Time.deltaTime * 10), 0, 0);
                this.gameObject.GetComponent<TextMeshProUGUI>().text = "TIME UP!"; break;
        }
    }
}
