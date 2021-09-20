using UnityEngine;

public class Player :MonoBehaviour {

    Vector3 lastPos = Vector3.zero;
    Vector3 targetPos = Vector3.zero;
    public float speed = 0.4f;
    Vector3 currentSpeed = Vector3.zero;
    float maxSpeed = 4;

    void Awake() {
        lastPos = transform.position;
        targetPos = transform.localPosition;
        GetComponent<PlayerInput>().onMoved += OnPlayerInput;
    }

    public void OnPlayerInput(Vector2 directions) {
        targetPos += new Vector3(directions.x,0,directions.y)*speed;

    }

    void Update() {
        transform.localPosition = Vector3.Lerp(transform.localPosition,targetPos,Time.deltaTime);
        currentSpeed = transform.localPosition - lastPos;
        lastPos = transform.localPosition;
    }
}
