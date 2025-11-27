using System.Diagnostics;
using System.Runtime.InteropServices;

namespace DesktopPet;

//help: https://stackoverflow.com/questions/79252139/c-sharp-unity-high-precision-timer-using-timerqueuetimer
//help: https://learn.microsoft.com/de-de/windows/win32/api/threadpoollegacyapiset/nf-threadpoollegacyapiset-createtimerqueuetimer
//help: https://learn.microsoft.com/de-de/windows/win32/api/threadpoollegacyapiset/nf-threadpoollegacyapiset-deletetimerqueuetimer
//help: https://learn.microsoft.com/de-de/dotnet/api/system.runtime.interopservices.gchandle?view=net-8.0
public class HighPrecisionTimer
{
    private GCHandle _callbackHandle;

    private long _lastTicks;

    private Stopwatch _stopwatch;
    private IntPtr _timerHandle = IntPtr.Zero;

    private uint _timeMillis;
    
    public uint Interval
    {
        get => _timeMillis;
        set
        {
            value = Math.Max(10, Math.Min(1000, value));
            _timeMillis = value;
        }
    }

    public bool IsTicking => _timerHandle != IntPtr.Zero;

    public event HighPrecisionTimerTick Tick;

    public delegate void HighPrecisionTimerTick(float deltaMillis);

    [DllImport("kernel32.dll")]
    private static extern bool CreateTimerQueueTimer(
        out IntPtr phNewTimer,
        IntPtr TimerQueue,
        TimerCallback Callback,
        IntPtr Parameter,
        uint DueTime,
        uint Period,
        uint Flags);

    [DllImport("kernel32.dll")]
    private static extern bool DeleteTimerQueueTimer(
        IntPtr TimerQueue,
        IntPtr Timer,
        IntPtr CompletionEvent);
    
    public void StartTicking()
    {
        if (_timerHandle != IntPtr.Zero)
            return;

        // self handled Garbage Collection, so it cannot get terminated by accident
        var cb = new TimerCallback(TimerTick);
        _callbackHandle = GCHandle.Alloc(cb);

        _stopwatch = new Stopwatch();
        _stopwatch.Start();

        var ok = CreateTimerQueueTimer(
            out _timerHandle,
            IntPtr.Zero,
            cb,
            IntPtr.Zero,
            0,
            _timeMillis,
            0);

        if (!ok)
        {
            throw new Exception("Failed to create high precision Win32 timer");
        }
    }
    
    public void StopTicking()
    {
        if (_timerHandle == IntPtr.Zero)
            return;

        DeleteTimerQueueTimer(IntPtr.Zero, _timerHandle, IntPtr.Zero);
        _timerHandle = IntPtr.Zero;

        if (_callbackHandle.IsAllocated) // Garbage collect
            _callbackHandle.Free();
    }

    private void TimerTick(IntPtr _, bool __)
    {
        var now = _stopwatch.ElapsedTicks;

        var deltaMillis = (now - _lastTicks) * 1000f / Stopwatch.Frequency;
        _lastTicks = now;

        Tick?.Invoke(deltaMillis);
    }
    
    private delegate void TimerCallback(IntPtr param, bool timerOrWaitFired);

}