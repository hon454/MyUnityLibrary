using System.Collections;
using MyUnityLibrary.Patterns;
using MyUnityLibrary.Utilities;
using UnityEngine;

public sealed class GameSpeedManager : MonoSingleton<GameSpeedManager>
{
    public float DefaultTimeScale { get; set; } = 1f;
    public bool CanChangeGameSpeed { get; set; } = true;
    public bool CanInterrupt { get; set; } = true;

    private Coroutine _coroutine;

    public void ResetGameSpeed()
    {
        Time.timeScale = DefaultTimeScale;
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void StopChangingGameSpeed()
    {
        if (_coroutine == null)
        {
            return;
        }
        
        StopCoroutine(_coroutine);
        _coroutine = null;
        ResetGameSpeed();
    }

    public void ChangeGameSpeed(float timeScale)
    {
        Time.timeScale = timeScale;
    }

    public void ChangeGameSpeedForSeconds(float durationSeconds, float timeScale)
    {
        if (!CanChangeGameSpeed)
        {
            return;
        }

        if (_coroutine != null && !CanInterrupt)
        {
            if (!CanInterrupt)
            {
                return;
            }
            
            StopCoroutine(_coroutine);
        }

        _coroutine = StartCoroutine(ChangeGameSpeedForSecondsRoutine(durationSeconds, timeScale));
    }
    
    public void ChangeGameSpeedForAnimationCurve(float durationSeconds, AnimationCurve animationCurve)
    {
        if (!CanChangeGameSpeed)
        {
            return;
        }

        if (_coroutine != null && !CanInterrupt)
        {
            if (!CanInterrupt)
            {
                return;
            }
            
            StopCoroutine(_coroutine);
        }

        _coroutine = StartCoroutine(ChangeGameSpeedForAnimationCurveRoutine(durationSeconds, animationCurve));
    }

    private IEnumerator ChangeGameSpeedForSecondsRoutine(float durationSeconds, float timeScale)
    {
        Time.timeScale = timeScale;
        yield return new WaitForRealSeconds(durationSeconds);
        
        ResetGameSpeed();
        _coroutine = null;
    }
    
    private IEnumerator ChangeGameSpeedForAnimationCurveRoutine(float durationSeconds, AnimationCurve animationCurve)
    {
        float beginRealtime = Time.realtimeSinceStartup;
        while (Time.realtimeSinceStartup - beginRealtime < durationSeconds)
        {
            float time = (Time.realtimeSinceStartup - beginRealtime) / durationSeconds;
            Time.timeScale = Mathf.Clamp(animationCurve.Evaluate(time), 0f, 1f);
            yield return null;
        }

        ResetGameSpeed();
        _coroutine = null;
    }
}
