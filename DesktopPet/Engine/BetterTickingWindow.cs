using System.Diagnostics;
using System.Windows.Media;
using System.Windows.Threading;

namespace DesktopPet.Engine;

public abstract class BetterTickingWindow : TimedWindow
{
    private const float TARGET_MS = 10f; // 100 FPS
    private const int MAX_STEPS = 5; // Catch-up max

    // Fallback-Timer
    private readonly DispatcherTimer _backupTimer;
    private readonly Stopwatch _sw = new();

    private float _accumulator;
    private long _last;

    private bool _running;

    protected BetterTickingWindow()
    {
        Unloaded += (_, _) => StopTicking();

        // Backup
        _backupTimer = new DispatcherTimer
        {
            Interval = TimeSpan.FromMilliseconds(TARGET_MS)
        };
        _backupTimer.Tick += (_, _) => ProcessTicks();

        // dispatcher events as Clock helper
        Dispatcher.Hooks.OperationPosted += (_, _) =>
        {
            if (_running) ProcessTicks();
        };

        Dispatcher.Hooks.OperationCompleted += (_, _) =>
        {
            if (_running) ProcessTicks();
        };

        LayoutUpdated += (_, _) =>
        {
            if (_running) ProcessTicks();
        };
    }

    public override bool IsTicking => _running;

    public override void StartTicking()
    {
        if (_running)
            return;

        _running = true;

        _accumulator = 0;
        _sw.Restart();
        _last = _sw.ElapsedTicks;

        // init timers
        CompositionTarget.Rendering += OnRender; // primary Clock

        _backupTimer.Start(); // backup Clock

        OnTickStart();
    }

    public override void StopTicking()
    {
        if (!_running)
            return;

        _running = false;

        CompositionTarget.Rendering -= OnRender;
        _backupTimer.Stop();

        OnTickStop();
    }

    private void OnRender(object? sender, EventArgs e)
    {
        ProcessTicks();
    }

    private void ProcessTicks()
    {
        var now = _sw.ElapsedTicks;
        var deltaMs = (now - _last) * 1000f / Stopwatch.Frequency;
        _last = now;

        _accumulator += deltaMs;

        var steps = 0;

        while (_accumulator >= TARGET_MS && steps < MAX_STEPS)
        {
            _accumulator -= TARGET_MS;
            Tick();
            steps++;
        }

        // max repeat for MAX_STEPS
        if (steps == MAX_STEPS)
            _accumulator = 0;
    }
}