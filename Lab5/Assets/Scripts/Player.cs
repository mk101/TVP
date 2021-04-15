using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour {
    [Range(100, 250)]
    public float Force = 180f;
    
    private bool isActive;

    public event Action Died;
    public event Action Winged;

    private new Rigidbody2D rigidbody;
    private float angle = 20;

    void Start() {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    
    void Update() {
        if (isActive) {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) {
                Winged?.Invoke();
                rigidbody.AddRelativeForce(new Vector2(0, Force));
                angle = 20;
            }
        }
        angle = Mathf.Lerp(angle, -angle - 80, Time.deltaTime);
        rigidbody.SetRotation(angle);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (isActive) {
            Died?.Invoke();
            GetComponent<Animator>().SetBool("IsDead", true);
            isActive = false;
        }
    }

    public void SetIsActive(bool value) {
        isActive = value;
        rigidbody.simulated = true;
        GetComponent<Animator>().SetBool("IsDead", !value);
    }

    public void Reset() {
        transform.position = new Vector3(0, -0.72f, -2);
        rigidbody.SetRotation(0);
    }
}
