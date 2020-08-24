using UnityEngine;

namespace MyUnityLibrary.Utilities
{
    /// <summary>
    /// Time.time의 영향을 받지 않고 realTime을 기준으로 작동하는 WaitForSeconds
    /// </summary>
    public sealed class WaitForRealSeconds : CustomYieldInstruction
    {
        private readonly float _endTime;

        public override bool keepWaiting => _endTime > Time.realtimeSinceStartup;

        public WaitForRealSeconds(float seconds)
        {
            _endTime = Time.realtimeSinceStartup + seconds;
        }
    }
}