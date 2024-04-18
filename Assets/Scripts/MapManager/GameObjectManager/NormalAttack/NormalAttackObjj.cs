using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NormalAttackObjj : MonoBehaviour, ISkillObj {
    protected float weight = 13f;
    protected float dmgPerUnit;

    protected Vector3 start;
    protected Vector3 end;
    protected float startTime;
    protected float journeyLength;
    private TrailRenderer trailRenderer;
    private void Awake() {
        trailRenderer = GetComponent<TrailRenderer>();
    }

    private void OnEnable() {
        dmgPerUnit = CharacterStats.Instance.PlayerAtk * GameConstants.dmgScale[(int)AttackType.NormalAttack];
        StartCoroutine(UpdatePosition());
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
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        float speed = 10f;
        float newPositionX = transform.position.x + speed * Time.deltaTime;
        transform.position = new Vector3(newPositionX, transform.position.y, transform.position.z);
        
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
