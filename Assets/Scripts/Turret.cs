using System;
using UnityEngine;

public class Turret :MonoBehaviour{

    public float health = 100;
    public Action onDeath ;
    public Action onHit;

    void Awake() {

    }

    public void Reset() {
        health = 100;
    }

    void OnTriggerEnter(Collider collider) {
        Debug.LogErrorFormat("Got {0}",collider.gameObject.name);
        if (collider.gameObject.name == "Enemy") {
            collider.transform.parent.GetComponent<Enemy>().Kill();
            health -= 25;
            if (health == 0 && onDeath != null) onDeath();
            else if (health > 0 && onHit != null) onHit();
        }
    }
}
