using UnityEngine;

public class Follow :MonoBehaviour{
    public Transform target;
    public float drag = 4;

    void Update() {
        Vector3 delta = transform.position - target.position;
        transform.position -= delta/drag;
    }
}
