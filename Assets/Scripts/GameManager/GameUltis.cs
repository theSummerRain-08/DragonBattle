using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameUltis {

    public static void DelayCall(this MonoBehaviour mono, float time, Action Callback) {
        mono.StartCoroutine(IEDelayCall(time, Callback));
    }
    public static IEnumerator IEDelayCall(float time, Action Callback) {
        yield return new WaitForSeconds(time);
        Callback?.Invoke();
    }

    public static int RandomNumber(int a, int b) {
        return UnityEngine.Random.Range(a, b);
    }

    public static T TryGetMonoComponent<T>(this MonoBehaviour mono, ref T tryValue) {
        if (tryValue == null)
            tryValue = mono.gameObject.GetComponent<T>();
        return tryValue;
    }


    public static void CreateContainer(this MonoBehaviour mono, string name, ref Transform trans) {
        GameObject obj = new GameObject(name);
        obj.transform.parent = mono.transform;
        trans = obj.transform;
    }

    public static string ObjectName(this MonoBehaviour mono) {
        return mono.gameObject.name.Replace("(Clone)", "");
    }
}
