using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MainGame : MonoBehaviour {

    Transform pointer;
    public static Action clicked;
    const float DISTANCE = 20;
    Prefabs prefabs;
    Turret turret;
    int kills = 0;
    Text textKills;
    Text textHealth;

    CanvasGroup tutorialOverlay;
    CanvasGroup endGameOverlay;

    public string BEST_SCORE_KEY = "BEST_SCORE";

    void Awake() {
        pointer = transform.Find("Pointer");
        prefabs = GetComponent<Prefabs>();
        turret = GameObject.Find("Player").GetComponent<Turret>();
        textKills = transform.Find("Canvas/Kills").GetComponent<Text>();
        textHealth = transform.Find("Canvas/Health").GetComponent<Text>();
        tutorialOverlay = transform.Find("Canvas/TutorialOverlay").GetComponent<CanvasGroup>();
        endGameOverlay = transform.Find("Canvas/EndGameOverlay").GetComponent<CanvasGroup>();
        turret.onDeath += OnDeath;
        turret.onHit += onHit;
        tutorialOverlay.GetComponentInChildren<Button>().onClick.AddListener(OnTutorialClick);
        endGameOverlay.GetComponentInChildren<Button>().onClick.AddListener(OnEndGameClick);
        endGameOverlay.gameObject.SetActive(false);
    }

    void Update() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // Casts the ray and get the first game object hit
        foreach (RaycastHit hit in Physics.RaycastAll(ray)) {
            if (hit.transform.name == "Floor") {
                pointer.position = hit.point;
            }
        }
        if (Input.GetMouseButton(0)) {
            if (clicked != null) clicked();
        }
    }

    private void CreateEnemy() {
        float angle = UnityEngine.Random.Range(0, 360) * Mathf.Deg2Rad;
        Vector3 pos = new Vector3(Mathf.Sin(angle) * DISTANCE, 0, Mathf.Cos(angle) * DISTANCE);
        Enemy enemy = GameObject.Instantiate(prefabs.enemyPrefab).GetComponent<Enemy>();
        Debug.Log("Create enemy at " + pos);
        enemy.transform.localPosition = pos;
        enemy.onKill += onEnemyKilled;
        enemy.StartMovingToCenter();

        Invoke("CreateEnemy", UnityEngine.Random.Range(4, 7));
    }

    public void OnDeath() {
        CancelInvoke("CreateEnemy");
        UpdateGUI();
        if (PlayerPrefs.GetInt(BEST_SCORE_KEY,int.MinValue) < kills) {
            PlayerPrefs.SetInt(BEST_SCORE_KEY,kills);
        }
        endGameOverlay.gameObject.SetActive(true);
        endGameOverlay.transform.Find("Text").GetComponent<Text>().text = string.Format(@"GAME OVER
YOU KILLED {0} CUBES

BEST SCORE {1}",kills,PlayerPrefs.GetInt(BEST_SCORE_KEY));

        DOVirtual.Float(0,1,1,val=> {
            endGameOverlay.alpha = val;
        });
        Debug.Log("Death!");
    }

    public void StartGame() {
        kills = 0;
        turret.Reset();
        Invoke("CreateEnemy", UnityEngine.Random.Range(1, 2));
        UpdateGUI();
    }

    public void UpdateGUI() {
        textKills.text = string.Format("KILLS {0}",kills);
        textHealth.text = string.Format("HEALTH {0}%",turret.health);
    }

    public void onHit() {
        UpdateGUI();
    }

    public void onEnemyKilled() {
        kills++;
        UpdateGUI();
    }

    public void ShowTutorial(Action doneCallback) {

    }

    public void OnTutorialClick() {
        DOVirtual.Float(1,0,1,val=> {
            tutorialOverlay.alpha = val;
        }).OnComplete(()=>{
            tutorialOverlay.gameObject.SetActive(false);
            StartGame();
        });
    }

    public void OnEndGameClick() {
        DOVirtual.Float(1,0,1,val=> {
            endGameOverlay.alpha = val;
        }).OnComplete(()=>{
            endGameOverlay.gameObject.SetActive(false);
            StartGame();
        });
    }
}
