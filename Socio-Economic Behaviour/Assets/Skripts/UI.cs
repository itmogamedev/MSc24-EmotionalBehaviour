using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class UГ : MonoBehaviour
{
    public TextMeshProUGUI Text;

    private int DayCount = 0;
    //public int NPCAmount = 3;
    public int Citizens = 0;
    private int NewCharCount = 0;
    private float AvgMoney = 0;
    private float AvgEmotion = 0;
    private int Deaths = 0;
    // Start is called before the first frame update
    void Start()
    {
        Text = FindObjectOfType<TextMeshProUGUI>();

    }
    private void OnEnable()
    {
        EventManager.onDayChanged += NewDay;
        NPC.onCharChange += CharChange;
        NPC.onMoney += MoneyCount;
        NPC.onEmotions += EmotionCount;
        NPC.onBehaviourChange += BehaviourChange;
    }
    private void OnDisable()
    {
        EventManager.onDayChanged -= NewDay;
        NPC.onCharChange += CharChange;


    }



    private void NewDay()
    {

        DayCount++;

    }
    private void CharChange()
    {
        Citizens ++;
        //Citizens = NPCAmount;


    }
    private void MoneyCount(float value)
    {
        AvgMoney += value;
        AvgMoney = (AvgMoney/ Citizens);
       // Debug.Log("Avg Mon" + AvgMoney);

    }

    private void EmotionCount(float value)
    {

        Debug.Log(value);
        AvgEmotion += value;
        AvgEmotion = AvgEmotion / Citizens;
        if (!(AvgEmotion > -100 || AvgEmotion < 100))
                AvgEmotion = 20*Random.Range(-3, 3);

        Debug.Log("Avg Emotions" + AvgEmotion);

    }
    private void BehaviourChange()
    {
       
        Debug.Log("Avg Emotions" + AvgEmotion);

    }



    // Update is called once per frame
    void Update()
    {
        Text.text = $"День - {DayCount}\r\nСредняя зарплата = {AvgMoney*100 }\r\nКоличество смертей = {Deaths}\r\nКоличество населения = {Citizens}\r\nЭмоциональный фон = {AvgEmotion}" ;



    
    }
}
