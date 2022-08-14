using System;
using System.Collections;
using UnityEngine;

namespace Utilities.Extensions
{
    public static class DelayExtension
    {
        public static void CallWithDelay(this MonoBehaviour monoBehaviour, Action action, float delay)
        {
            monoBehaviour.StartCoroutine(CallWithDelayRoutine(action, delay));
        }

        private static IEnumerator CallWithDelayRoutine(Action method, float delay)
        {
            yield return new WaitForSeconds(delay);
            method();
        }
    }
}