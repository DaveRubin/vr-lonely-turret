using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TurretControl : MonoBehaviour {


    public float ROTATION_SPEED = 0.4f;
    private const float COOL_DOWN_DURATION = 0.2f;
    private Transform pointer;
    bool readyToFire = true;
    private List<Transform> barrels;
    private int currentBarrel = 0;
    private Prefabs prefabs;

    void Awake() {
        pointer = GameObject.Find("Pointer").transform;
        InvokeRepeating("Lookat", 0, 0.1f);
        barrels = new List<Transform>();
        barrels.Add(transform.Find("Turret/G1"));
        barrels.Add(transform.Find("Turret/G2"));
        barrels.Add(transform.Find("Turret/G3"));
        MainGame.clicked += OnClicked;
        prefabs = GameObject.Find("MainGame").GetComponent<Prefabs>();
    }

    void Lookat() {
        Vector3 relativePos = pointer.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        transform.DORotate(rotation.eulerAngles, ROTATION_SPEED);
    }

    public void OnClicked() {
        if (readyToFire) {
            GameObject bullet = GameObject.Instantiate(prefabs.bulletPrefab);
            readyToFire = false;
            Transform barrel = barrels[currentBarrel];
            currentBarrel = (currentBarrel + 1) % barrels.Count;
            Sequence sequence = DOTween.Sequence();
            float currentX = barrel.localPosition.x;
            sequence.Append(barrel.DOLocalMoveX(currentX-1,0.1f));
            sequence.Append(barrel.DOLocalMoveX(currentX,0.4f));
            bullet.transform.rotation = transform.rotation;
            bullet.transform.position = barrel.position;
            DOVirtual.DelayedCall(COOL_DOWN_DURATION, () => {
                readyToFire = true;
            });
        }
    }
}
