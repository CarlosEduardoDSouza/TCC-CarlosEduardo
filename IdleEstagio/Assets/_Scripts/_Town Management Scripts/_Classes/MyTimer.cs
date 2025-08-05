using System;
using System.IO;

public class MyTimer
{
    public float duration;
    float baseDuration;
    public event Action OnTimerEnd;
    public bool isLooping;
    private bool timerEnded;
    private float timeEnlapsed;

    public void InitializeTimer(float _duration, bool _isLooping)
    {
        duration = _duration;
        baseDuration = _duration;
        isLooping = _isLooping;
        timeEnlapsed = 0;
    }

    public void TickTimer(float tickSpeed)
    {
        if(timerEnded) return;

        duration -= tickSpeed;
        timeEnlapsed += tickSpeed;

        if(duration <= 0)
            TimerEnded();
    }

    public void TimerEnded()
    {
        OnTimerEnd?.Invoke();

        if(isLooping)
        {
            duration = baseDuration;
            timeEnlapsed = 0;
            timerEnded = false;
        }
        else
            timerEnded = true;
    }

    public float GetFillAmout()
    {
        return timeEnlapsed / baseDuration;
    }

    public float GetRemainingTimeInSeconds()
    {
        return baseDuration - timeEnlapsed;
    }
}
