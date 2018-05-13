using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {
//        public int startTime; // we will count down from this value

    public string Name;
    public float Time = 0;
    public float TimeStep = 0.01f;
    public bool Interrupted = false;
    private IEnumerator _coroutine;

    public void StartTimer(float duration) {
        Time = duration;
        Interrupted = false;
        _coroutine = CountDown();
        StartCoroutine(_coroutine);
    }

    public float StopTimer() {
        StopCoroutine(_coroutine);
        Interrupted = true;
        return Time;
    }

    private IEnumerator CountDown() {
        while (Time > 0) {
            Time -= TimeStep;
            yield return new WaitForSeconds(TimeStep);
        }

        Interrupted = false;
    }
}