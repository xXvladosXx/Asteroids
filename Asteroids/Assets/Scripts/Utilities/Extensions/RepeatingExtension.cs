using System;
using System.Collections;
using UnityEngine;

namespace Utilities.Extensions
{
    public static class RepeatingExtension
    {
        public static void CallWithRepeat(this MonoBehaviour monoBehaviour, Action action, float delay)
        {
            monoBehaviour.StartCoroutine(CallWithRepeatRoutine(action, delay));
        }

        private static IEnumerator CallWithRepeatRoutine(Action method, float delay)
        {
            while (true)
            {
                method();
                yield return new WaitForSeconds(delay);
            }
        }
    }
}