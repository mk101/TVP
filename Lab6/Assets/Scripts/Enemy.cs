using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public Ball GameBall;
    public float Speed = 2f;

    private Vector3 dist;
    private Vector3 start;
    private float timeFromStartMoving;
    private float timeToMove;
    private bool isMoving;
    private float debugSpeed;
    void Start(){
        dist = transform.position;
        start = transform.position;
        debugSpeed = Speed;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.F)) {
            if (Speed != 1) {
                Speed = 1;
            } else {
                Speed = debugSpeed;
            }
        }
        if (Physics.Raycast(GameBall.transform.position, GameBall.dir, out RaycastHit hit, 500f)) {
            var obj = hit.transform.gameObject;
            if (obj.CompareTag("EnemyWall") && !isMoving) {
                Vector3 wallHit = hit.point;
                Vector3 pos = wallHit - 3 * GameBall.dir.normalized;

                dist.x = pos.x;
                dist.x = Mathf.Clamp(dist.x, -14.748f, 15.51f);

                start.x = transform.position.x;

                timeToMove = Vector3.Distance(start, dist) / Speed;
                timeFromStartMoving = Time.time;
                isMoving = true;
            } else if (obj.CompareTag("Wall") && !isMoving) {
                Vector3 nDir = new Vector3(-GameBall.dir.x, GameBall.dir.y, GameBall.dir.z);
                if (Physics.Raycast(hit.point, nDir, out RaycastHit nHit, 250f)) {
                    var nObj = nHit.transform.gameObject;
                    if (nObj.CompareTag("EnemyWall")) {
                        Vector3 wallHit = nHit.point;
                        Vector3 pos = wallHit - 3 * nDir.normalized;

                        dist.x = pos.x;
                        dist.x = Mathf.Clamp(dist.x, -14.748f, 15.51f);

                        start.x = transform.position.x;

                        timeToMove = Vector3.Distance(start, dist) / Speed;
                        timeFromStartMoving = Time.time;
                        isMoving = true;
                    }
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
