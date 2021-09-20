using System;
using UnityEngine;

public class Enemy :MonoBehaviour {
    float life = 100;
    Vector3 direction = Vector3.zero;
    float speed = 5;
    public Action onKill;

    void Awake() {

    }

    void Update() {
        if (direction != Vector3.zero) {
            transform.localPosition += direction*Time.deltaTime;
        }
    }

    public void StartMovingToCenter() {
        direction = (Vector3.zero - transform.localPosition)/speed;
    }

    public void Hit() {
        life -= 25;
        if (life == 0) {
            if (onKill != null) onKill();
            Kill();

        }
    }

    public void Kill() {
        GameObject explosion = GameObject.Instantiate(GameObject.Find("MainGame").GetComponent<Prefabs>().enemyExplosionPrefab);
        explosion.transform.position = transform.position;
        GameObject.Destroy(gameObject);
    }



}
