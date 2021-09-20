using DG.Tweening;
using UnityEngine;

public class CameraFollow : MonoBehaviour{
    private const float ROTATION_SPEED = 6;
    public Transform target;
    private Vector3 targetRotation;

    void Awake() {
        MainGame.clicked += OnClicked;
    }

    public void OnClicked() {
        Vector3 relativePos = target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        targetRotation = rotation.eulerAngles;
        transform.DORotate(targetRotation,ROTATION_SPEED);
    }
}
