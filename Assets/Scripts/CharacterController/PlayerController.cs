using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterController
{
    [SerializeField] private LayerMask layerMask;

    void Update() {
        if (Input.GetMouseButton(0)) {
            Move();
        }
    }

    protected override void Attack() {
        throw new System.NotImplementedException();
    }
    protected override void Move() {
        Vector2 rayOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition); 
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.zero, Mathf.Infinity, layerMask); 

        if (hit.collider != null) 
        {
            float lerfSpeed = 6f;
            Vector2 currentPosition = transform.position;
            Vector2 newPosition = new Vector3(hit.point.x, hit.point.y);
            transform.position = Vector2.Lerp(currentPosition, newPosition, lerfSpeed*Time.deltaTime);
        }
    }
    protected override void TakeDamaged() {
        throw new System.NotImplementedException(); 
    }
}
