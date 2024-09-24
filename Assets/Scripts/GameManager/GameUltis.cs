using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameUltis {

    //public static void DelayCall(float time, Action Callback) {
    //    StartCoroutine(IEDelayCall(time, Callback));
    //}
    public static IEnumerator IEDelayCall(float time, Action Callback) {
        yield return new WaitForSeconds(time);
        Callback?.Invoke();
    }

    public static float RandomFloatNumber(float a, float b) {
        float firstNum = a * 2;
        float secondNum = b * 2;
        return UnityEngine.Random.Range(firstNum, secondNum + 1) / 2;
    }
    public static int RandomIntNumber(int a, int b) {
        return UnityEngine.Random.Range(a, b + 1);
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

    public static string ObjectName(GameObject obj) {
        return obj.name.Replace("(Clone)", "");
    }

    public static void Hide(GameObject obj) {
        obj.SetActive(false);
    }

    public static void SetParent(GameObject obj, Transform parent) {
        obj.transform.SetParent(parent);
    }

    public static void Show(GameObject obj) {
        obj.SetActive(true);
    }

    public static bool ExitScreen(Vector3 currentPosition) {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(currentPosition);
        return (screenPosition.x < 0 || screenPosition.x > Screen.width || screenPosition.y < 0|| screenPosition.y > Screen.height * 2);
    }


    public static bool ExitLeftScreen(Vector3 currentPosition) {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(currentPosition);
        return (screenPosition.x < 0 );
    }
    public static bool ExitRightScreen(Vector3 currentPosition) {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(currentPosition);
        return (screenPosition.x > Screen.width);
    }
}
