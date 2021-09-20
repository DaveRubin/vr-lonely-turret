using System;
using UnityEngine;

public class PlayerInput :MonoBehaviour{

    public Action<Vector2> onMoved;

    void Update() {
        Vector2 result = Vector2.zero;
        //up
        if (Input.GetKey(KeyCode.S)) {
            result.y -=1;
        }
        //down
        if (Input.GetKey(KeyCode.W)) {
            result.y +=1;
        }
        //left
        if (Input.GetKey(KeyCode.A)) {
            result.x -=1;
        }
        //right
        if (Input.GetKey(KeyCode.D)) {
            result.x +=1;
        }
        if (onMoved != null && result != Vector2.zero)
            onMoved(result);

    }
}
