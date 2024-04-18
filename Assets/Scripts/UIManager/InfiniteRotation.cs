using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class InfiniteRotation : MonoBehaviour {
    public float rotationSpeed = 600f;
    private void OnEnable() {
        StartCoroutine(Rotate());
    }
    IEnumerator Rotate() {
        while (true) 
        {
            transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);

            yield return null; 
        }
    }
}
