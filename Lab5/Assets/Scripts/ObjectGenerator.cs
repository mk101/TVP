using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGenerator : MonoBehaviour {
    private GameManager gm;
    public GameObject Target;
    public Vector2 MinRandom;
    public Vector2 MaxRandom;
    public float Delay;

    void Start() {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        if (Delay <= 0 || gm == null) {
            throw new System.InvalidOperationException();
        }

        gm.IsPlayingChange += Gm_IsPlayingChange;

    }

    private void Gm_IsPlayingChange(bool obj) {
        if (obj) {
            StartCoroutine(Create());
        }
    }

    IEnumerator Create() {
        while (gm.IsPlaying) {

            var obj = GameObject.Instantiate(Target);
            obj.transform.position = transform.position + new Vector3(
                MinRandom.x == MaxRandom.x ? MinRandom.x : Random.Range(MinRandom.x, MaxRandom.x),
                MinRandom.y == MaxRandom.y ? MinRandom.y : Random.Range(MinRandom.y, MaxRandom.y),
                0
            );
            yield return new WaitForSeconds(Delay);
        }
    }
}
