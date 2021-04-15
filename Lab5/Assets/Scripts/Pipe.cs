using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour {

    private GameManager gm;
    [Range(1, 100)]
    public float Speed = 2f;
    void Start() {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    
    void Update() {
        if (gm.IsPlaying) {
            transform.Translate(new Vector3(-Speed * Time.deltaTime, 0, 0));
            if (transform.position.x <= -6) {
                GameObject.Destroy(gameObject);
            }
        }
    }
}
