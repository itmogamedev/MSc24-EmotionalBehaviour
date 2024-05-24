using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class EventManager : MonoBehaviour
{
    public float DayDuration = 90;
    private float Timer = 0;



    private void Start()
    {
        Timer = DayDuration;
    }

    // Time 
    public static Action onDayChanged;
    private void Update()
    {
        Timer -= Time.deltaTime;
        if (Timer<0)
        {
            NewDay();
            Debug.Log("NewDay");
            Timer = DayDuration;

        }



      
    }


    private void NewDay()
    {
        onDayChanged?.Invoke();
    }
  
  

}
