using System;

public delegate void TimerDelegate();
public delegate void TimerDelegateSeconds(int second);
public class Timer
{
    private float _time;
    public TimerDelegateSeconds changeEverySecond;
    public TimerDelegate end;
    private bool _activated = false;
    int seconds;

    public bool activated => _activated;
    public Timer(float time, TimerDelegateSeconds changeEverySecond, TimerDelegate end)
    {
        seconds = Convert.ToInt32(time);
        this.end = end;
        this.changeEverySecond = changeEverySecond;
        _time = time;
        _activated = true;
    }

    public void Update(float deltaTime)
    {
        if (activated)
        {
            if (_time > 0)
            {
                _time -= 1 * deltaTime;

                if(changeEverySecond != null)
                {
                    int current = Convert.ToInt32(_time);
                    if (seconds != current)
                    {
                        seconds = current;
                        changeEverySecond(seconds);
                    }
                }
            }
            else
            {
                _activated = false;
                end!();
            }
        }

    }
}
