using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NormalAttackObjj : MonoBehaviour, ISkillObj {
    protected float weight = 20f;
    protected float dmgPerUnit;

    protected Vector3 start;
    protected Vector3 end;
    protected float startTime;
    protected float journeyLength;
    private TrailRenderer trailRenderer;
    protected float[] randomNum = new float[5];
    private void Awake() {
        trailRenderer = GetComponent<TrailRenderer>();
    }

    private void OnEnable() {
        dmgPerUnit = CharacterStats.Instance.PlayerAtk * GameConstants.dmgScale[(int)AttackType.NormalAttack];
        StartCoroutine(UpdatePosition());
        randomNum[0] = Random.Range(0, 3);
        randomNum[1] = Random.Range(0, 2);
        randomNum[2] = Random.Range(0, 2);
        randomNum[4] = Random.Range(0, 4);
    }
    public virtual void Move() {

    }
    IEnumerator UpdatePosition() {
        yield return new WaitForSeconds(0f);
        startTime = Time.time;
        start = transform.position;
        end = new Vector3(transform.position.x + weight, transform.position.y, transform.position.z);
        trailRenderer.time = 0.2f;

    }
    protected void GoParabol(float height, float speed, float minRotation, float maxRotation) {
        journeyLength = Vector3.Distance(start, end) / speed;
        float timeElapsed = Time.time - startTime;
        if (timeElapsed >= journeyLength) {
            GoStraight();
            return;
        }
        float parabolicHeight = height * Mathf.Sin((timeElapsed / journeyLength) * Mathf.PI);
        Vector3 newPosition = Vector3.Lerp(start, end, timeElapsed / journeyLength);
        newPosition.y += parabolicHeight;
        transform.position = newPosition;

        float rotationZ = Mathf.Lerp(minRotation, maxRotation, timeElapsed / journeyLength);
        transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);
    }


    protected void GoStraight() {
        float speed = 10f;
        float angleRad = transform.eulerAngles.z * Mathf.Deg2Rad; // Chuyển đổi góc quay thành radian
        Vector3 direction = new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad), 0f); // Tính toán hướng dựa trên góc quay

        // Di chuyển theo hướng tính được
        transform.position += direction * speed * Time.deltaTime;
    }



    public void DeSpawn() {
        ObjectPooling.Instance.DeSpawn(this.gameObject);
    }
    private void OnDisable() {
        trailRenderer.time = 0;
        journeyLength = 0;
        startTime = 0;
    }
}
