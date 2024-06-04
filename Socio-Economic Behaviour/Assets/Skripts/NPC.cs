using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Device;
using UnityEngine.Events;
using UnityEngine.SocialPlatforms;
using Unity.VisualScripting;


public class NPC : MonoBehaviour
{
    // Set up 
    public float Extraversion = 0.5f;
    public float Agreeableness = 0.5f;
    public float Openess = 0.5f;
    public float Conscientiousness = 0.5f;
    public float Neuroticism = 0.5f;
    public float MaxHP = 100;
    public float Savings = 0;
    public float EmotionalCoefficient = 0;
    public float JobPrestige = 0;
    public float Characters = 0;
    public float Spending = 0;
    public float Income = 0;
    // Emotions
    public float Calm = 1; 
    public float Happy= 1; 
    public float Surprise = 1; 
    public float Disgust = 1; 
    public float Interest = 1; 
    public int  ColleguesAmount=1;



    // calculated
    float currentHP;
    float damage;
    float heal;
    
    int NewColleguesAmount;
    // Needs
    float JobNeed;
    float SafetyNeed;
    float CommunicationNeed;
    float FinancialNeed;
    float SavingNeed;
    //Personal Needs
    float currentHP_P;
    float ColleguesAmount_P;
    float FinancialNeed_P;
    float JobNeed_P;
    float SavingNeed_P;
    //
    float EmotionalIntencity;
    float NeedSutisfied;
    // State
    private bool Positive = false;
    private bool Negative = false;
    private bool kill = false;
    private bool steal = false;
    private bool donate = false;
    private bool help = false;
   

    // Start is called before the first frame update
    void Start()
    {
        onMoney?.Invoke(Income);
        EmotionCalc();
        CharCount();
        onEmotions?.Invoke(EmotionalIntencity);
    }

    public static Action onCharChange;

    public static Action onBehaviourChange;



    public static Action<float> onMoney;
    public static Action<float> onEmotions;


    
    private void OnEnable()
    {
        EventManager.onDayChanged += NewDay ;
        


    }
    private void OnDisable()
    {
        EventManager.onDayChanged -= NewDay;

    }

    // Update is called once per frame
    void Update()
    {
        
        //Safety need
        //SafetyNeed = currentHP + damage + heal + (HPdelta/ Characters);
        if (currentHP <0 ) { }
        //Communication need
        CommunicationNeed = NewColleguesAmount + ColleguesAmount;
        //Financial 
        

  





        //Personal needs
       if (Negative = true)
        {

            int flip = UnityEngine.Random.Range(0, 1);
            if (flip == 0 )
            {
                kill = true;
            }
            else
            {
                steal = true;
            }
        }
       else if (Positive = true)
        {
            int flip = UnityEngine.Random.Range(0, 1);
            if (flip == 0)
            {
                donate = true;
            }
            else
            {
                help = true;
            }
        }



       
    

        if (Input.GetKeyDown(KeyCode.I))
        {
            //Debug.Log("Calm =" + Calm);
            //Debug.Log("Happy =" + Happy);
            //Debug.Log("Surprise =" + Surprise);
            //Debug.Log("Disgust =" + Disgust);
            //Debug.Log("Interest =" + Interest);
            Debug.Log("EmotionalIntencity =" + EmotionalIntencity);
            //Debug.Log("NeedSutisfied =" + NeedSutisfied);

        }


       

    }

    private void NewDay()
    {

        EmotionCalc();
        onEmotions?.Invoke(EmotionalIntencity);
        Debug.Log(FinancialNeed);
    }

    private void CharCount()
    {

        onCharChange?.Invoke();

    }    



    private void EmotionCalc()
    {
        SafetyNeed = 100;
        FinancialNeed = Income + Savings - Spending;
        Savings = FinancialNeed;
        SavingNeed = (Savings / Income) * 100;
        JobNeed = 100 * JobPrestige;
        CommunicationNeed = NewColleguesAmount + ColleguesAmount;
        SafetyNeed = (SafetyNeed * (Extraversion + Agreeableness + (1 - Openess) + Conscientiousness + Neuroticism)) / 5;
        CommunicationNeed = (ColleguesAmount * (Extraversion + Agreeableness + Openess + Conscientiousness + (1 - Neuroticism))) / 5;
        FinancialNeed = (FinancialNeed * (0 + 0 + (1 - Openess) + Conscientiousness + Neuroticism)) / 5;
        JobNeed = (JobNeed * (Extraversion + 0 + Openess + Conscientiousness + Neuroticism)) / 5;
        SavingNeed = (SavingNeed * (0 + 0 + 0 + Conscientiousness + Neuroticism)) / 5;





        //Current Emotions
        Calm = SafetyNeed * EmotionalCoefficient;
        Happy = CommunicationNeed * EmotionalCoefficient;
        Surprise = FinancialNeed * EmotionalCoefficient;
        Disgust = JobNeed * EmotionalCoefficient;
        Interest = SavingNeed * EmotionalCoefficient;

        //Emotional Intencity
        EmotionalIntencity = ((Math.Abs(Calm + Happy + Surprise * Disgust * Interest)) / 100);

        //Needs Satisfied
        NeedSutisfied = JobNeed + SafetyNeed + CommunicationNeed + FinancialNeed + SavingNeed;
        if (NeedSutisfied >= 0)
            NeedSutisfied = 1;
        else
            NeedSutisfied = -1;


        EmotionalIntencity = EmotionalIntencity * NeedSutisfied;


        if (EmotionalIntencity >100)
            EmotionalIntencity = 100;
        else if (EmotionalIntencity <-100)
            EmotionalIntencity = -100;



        if (EmotionalIntencity >30)
        {
            int Chance = UnityEngine.Random.Range(0, 100);
            if (Chance > 30)
            {
                Positive = true;
            }
            else { Positive = false; }
            
        }
        if (EmotionalIntencity <-30)
        {
            int Chance = UnityEngine.Random.Range(30, 100);
            if (Chance > 30)
            {
                Negative = true;
            }
            else {  Negative = false; }
        }
        else
        {
            Positive=false;
            Negative=false;
        }


        Debug.Log("NPCNewDay");

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (steal = true)
        {
            Steal();
        }
        else if (kill  = true)
        {
            Kill();
        }
        else if (donate = true)
        {

            Donate();
        }
        else if (help = true)
        {

            Help();
        }

    }


    private void Steal()
    {
        Income -= 40;
        EmotionEvent();
        steal = false;
        onBehaviourChange?.Invoke();
    }

    private void Donate()
    {
        Income += 40;
        EmotionEvent();
        donate = false;
        onBehaviourChange?.Invoke();
    }

    private void Kill()
    {
        SafetyNeed -= -50;
        EmotionEvent();
        Destroy(gameObject);
        kill = false;
        onBehaviourChange?.Invoke();
    }

    private void Help()
    {

        SafetyNeed += 30;
        EmotionEvent();
        help = false;
        onBehaviourChange?.Invoke();
    }




    private void EmotionEvent()
    {
        FinancialNeed = Income + Savings - Spending;
        Savings = FinancialNeed;
        SavingNeed = (Savings / Income) * 100;
        JobNeed = 100 * JobPrestige;
        CommunicationNeed = NewColleguesAmount + ColleguesAmount;
        SafetyNeed = (SafetyNeed * (Extraversion + Agreeableness + (1 - Openess) + Conscientiousness + Neuroticism)) / 5;
        CommunicationNeed = (ColleguesAmount * (Extraversion + Agreeableness + Openess + Conscientiousness + (1 - Neuroticism))) / 5;
        FinancialNeed = (FinancialNeed * (0 + 0 + (1 - Openess) + Conscientiousness + Neuroticism)) / 5;
        JobNeed = (JobNeed * (Extraversion + 0 + Openess + Conscientiousness + Neuroticism)) / 5;
        SavingNeed = (SavingNeed * (0 + 0 + 0 + Conscientiousness + Neuroticism)) / 5;





        //Current Emotions
        Calm = SafetyNeed * EmotionalCoefficient;
        Happy = CommunicationNeed * EmotionalCoefficient;
        Surprise = FinancialNeed * EmotionalCoefficient;
        Disgust = JobNeed * EmotionalCoefficient;
        Interest = SavingNeed * EmotionalCoefficient;

        //Emotional Intencity
        EmotionalIntencity = ((Math.Abs(Calm + Happy + Surprise * Disgust * Interest)) / 100);

        //Needs Satisfied
        NeedSutisfied = JobNeed + SafetyNeed + CommunicationNeed + FinancialNeed + SavingNeed;
        if (NeedSutisfied >= 0)
            NeedSutisfied = 1;
        else
            NeedSutisfied = -1;
        EmotionalIntencity = EmotionalIntencity * NeedSutisfied;
        if (EmotionalIntencity > 100)
            EmotionalIntencity = 100;
        else if (EmotionalIntencity < -100)
            EmotionalIntencity = -100;



        if (EmotionalIntencity > 30)
        {
            int Chance = UnityEngine.Random.Range(0, 100);
            if (Chance > 30)
            {
                Positive = true;
                Negative = false;

            }
            else { Positive = false; }

        }
        if (EmotionalIntencity < -30)
        {
            int Chance = UnityEngine.Random.Range(30, 100);
            if (Chance > 30)
            {
                Negative = true;
                Positive = false;

            }
            else { Negative = false; }
        }
        else
        {
            Positive = false;
            Negative = false;
        }
    }

}
