using System;
using System.Collections;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public TMPro.TextMeshProUGUI timerGameObject;
    public TimeSpan timer;
    public bool timerGoing = true;
    
    private float elapsedTime = 0f;
    void Start()
    {
        timer = TimeSpan.Zero;
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;
        timer = TimeSpan.FromSeconds(elapsedTime);
        timerGameObject.text = $"Timer: {timer.Minutes:00}:{timer.Seconds:00}";
    }
    
}

