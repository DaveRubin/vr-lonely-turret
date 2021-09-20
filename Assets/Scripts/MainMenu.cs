using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu :MonoBehaviour {
    void Awake() {
        GetComponentInChildren<Button>().onClick.AddListener(StartGame);
    }

    public void StartGame() {
        SceneManager.LoadScene("Game");
    }
}
