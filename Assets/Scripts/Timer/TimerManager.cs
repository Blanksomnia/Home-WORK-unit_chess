using System.Collections.Generic;

public class TimerManager
{
    List<Timer> timers = new List<Timer>();
    List<Timer> trash = new List<Timer>();

    public void Updata(float deltaTime)
    {
        if(timers.Count > 0)
        {
            for(int i = 0; i < timers.Count; i++)
            {
                timers[i].Update(deltaTime);
                if(!timers[i].activated)
                    trash.Add(timers[i]);
            }
        }

        if(trash.Count > 0)
        {
            for (int i = 0;i < trash.Count; i++)
                timers.Remove(trash[i]);

            trash.Clear();
        }
    }

    public void Add(Timer timer)
    {
        timers.Add(timer);
    }

    public void Clear()
    {
        timers.Clear();
        trash.Clear();
    }
}
