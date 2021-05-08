using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    private Vector3 dist;
    private Vector3 start;
    private float timeFromStartMoving;
    private float timeToMove;
    private bool isMoving;

    public float Speed = 10f;
    public Camera MainCamera;
    private void Start() {
        dist = transform.position;
        start = transform.position;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit)) {
                var obj = hit.transform.gameObject;
                if (obj.CompareTag("Map") || obj.CompareTag("Player")) {
                    dist.x = hit.point.x;
                    dist.x = Mathf.Clamp(dist.x, -14.748f, 15.51f);

                    start.x = transform.position.x;

                    timeToMove = Vector3.Distance(start, dist) / Speed;
                    timeFromStartMoving = Time.time;
                    isMoving = true;
                }
            }
        }
        
    }

    private void FixedUpdate() {
        if (isMoving) {
            float t = (Time.time - timeFromStartMoving) / timeToMove;

            transform.position = Vector3.Lerp(start, dist, t);

            if (t >= 1f) {
                isMoving = false;
            }
        }
    }
}
