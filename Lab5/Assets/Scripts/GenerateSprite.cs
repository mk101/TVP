using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateSprite : MonoBehaviour {

    public Sprite Target;
    public int Count;

    void Start() {
        
    }

    void Update() {
        
    }

    public void Generate() {
        if (Count <= 0) {
            Debug.LogError("Count <= 0");
            return;
        }

        float x, y;
        x = transform.position.x;
        y = transform.position.y;

        for (int i = 0; i < Count; i++) {
            GameObject cur = new GameObject();
            cur.transform.position = new Vector3(x, y);
            cur.AddComponent<SpriteRenderer>();
            cur.GetComponent<SpriteRenderer>().sprite = Target;
            cur.transform.SetParent(transform);
            x += Target.rect.width/100;
        }
    }
}
