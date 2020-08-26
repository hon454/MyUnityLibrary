using System;
using System.Diagnostics;
using UnityEngine;

namespace MyUnityLibrary.Debugging
{
    /// <summary>
    /// <example>
    /// <code>
    /// using (new DisposableStopWatch(name, numberOfTests)
    /// {
    ///     // do something...
    /// }
    /// </code>
    /// </example>
    /// </summary>
    public class DisposableStopWatch : IDisposable
    {
        private readonly string _name;
        private readonly uint _numberOfTests;
        private readonly Stopwatch _stopwatch;

        public DisposableStopWatch(string name, uint numberOfTests = 1)
        {
            _name = name;
            _numberOfTests = Math.Min(1, numberOfTests);
            _stopwatch = Stopwatch.StartNew();
        }

        public void Dispose()
        {
            _stopwatch.Stop();
            float ms = _stopwatch.ElapsedMilliseconds;
            UnityEngine.Debug.Log(
                $"{_name} finished: {ms:0.00}ms total, {ms / _numberOfTests:0.000000}ms per test for {_numberOfTests} tests");
        }
    }
}
