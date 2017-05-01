using System.Timers;

public class GameTimer
{
    Timer turningTimer;
    public bool timerPassed { get; set; }

    public void TimerElapsed(object source, ElapsedEventArgs e)
    {
        turningTimer.Stop();
        timerPassed = true;
    }

    public void ResetAndStartTimer(float intervalSeconds)
    {
        ResetTimerNew(intervalSeconds);
        StartTimer();
    }

    public void StartTimer()
    {
        turningTimer.Start();
    }

    public void ResetTimerNew(float intervalSeconds)
    {
        turningTimer = new Timer();
        turningTimer.Interval = intervalSeconds * 1000f;
        turningTimer.Elapsed += new ElapsedEventHandler(TimerElapsed);
        timerPassed = false;
    }
}
