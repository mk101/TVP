using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Ball : MonoBehaviour {
    public float Speed;
    private new Rigidbody rigidbody;
    [HideInInspector]
    public Vector3 dir;
    public float k = 2f;

    public event System.Action OnPlayerScore;
    public event System.Action OnEnemyScore;

    void Start() {
        rigidbody = GetComponent<Rigidbody>();
        transform.position = new Vector3(-0.2f, 3.035f, 0.07f);
        dir = new Vector3(Random.Range(-1f,1f), 0, -1f);
        
    }

    private void FixedUpdate() {
        rigidbody.velocity = dir * Time.fixedDeltaTime * Speed;
    }

    private void OnCollisionEnter(Collision collision) {
        var obj = collision.gameObject;
        if (obj.CompareTag("Player") || obj.CompareTag("Enemy")) {
            Vector3 pos = transform.position;
            Vector3 cpos = obj.transform.position;
            float dirToCenter = Mathf.Abs(pos.x - cpos.x);
            float sign = cpos.x <= pos.x ? 1 : -1;

            dir.x = sign * dirToCenter / k;
            dir.z *= -1f;
            
        } else if (obj.CompareTag("Wall")) {
            dir.x *= -1f;
        } else if (obj.CompareTag("PlayerWall")) {
            OnEnemyScore?.Invoke();
            Start();
        } else if (obj.CompareTag("EnemyWall")) {
            OnPlayerScore?.Invoke();
            Start();
        }
    }
}
