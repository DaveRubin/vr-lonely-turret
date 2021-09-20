using UnityEngine;

public class Bullet :MonoBehaviour{
    void Update() {
        transform.localPosition += transform.forward;
    }

    void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.name == "Floor" ) {
            Kill();

        }
        else if (collider.gameObject.name == "Enemy" ){
            Kill();
            collider.transform.parent.GetComponent<Enemy>().Hit();
        }
    }

    public void Kill() {
        GameObject bullet = GameObject.Instantiate(GameObject.Find("MainGame").GetComponent<Prefabs>().bulletExplosionPrefab);
        bullet.transform.position = transform.position;
        GameObject.Destroy(gameObject);
    }
}
