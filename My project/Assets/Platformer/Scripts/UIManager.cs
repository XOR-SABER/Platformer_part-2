using System;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI PointsTxt;
    public TextMeshProUGUI WorldTxt;
    public TextMeshProUGUI TimeTxt; 
    public TextMeshProUGUI CoinText; 

    private int points;
    private int coins;
    
    public float level_time = 300; 
    void Start()
    {
        points = 0;
    }

    public void AddPoints(int amount) {
        points = Math.Min(amount + points, 999999);
        updatePointText();
    }

    public void AddCoin(int amount) {
        coins = amount + coins % 100;
        updateCoinText();
    }

    // Update is called once per frame
    void Update()
    {
        if(level_time > 0) {
            level_time -= Time.deltaTime;

            // Display the timer in seconds only
            int seconds = Mathf.FloorToInt(level_time);
            string timerString = seconds.ToString();
            TimeTxt.text = timerString;
        }
    }

    void updatePointText() {
        string updateStr = points.ToString();
        if(updateStr.Length < 6) {
            for(int i = updateStr.Length; i < 6; i++) {
                updateStr = "0" + updateStr;
            }
        }
        PointsTxt.text = updateStr;
    }

    void updateCoinText() {
        string updateStr = coins.ToString();
        CoinText.text = "x" + updateStr;
    }

}
