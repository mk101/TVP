using System;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public Text StartText;
    public Text WinText;
    public TextMeshProUGUI PlayerText;
    public TextMeshProUGUI EnemyText;
    [Range(1, 50)]
    public uint WinScore = 25;
    public AudioClip PointClip;

    private bool isActive;
    private bool isWin;
    private uint playerScore;
    private uint enemyScore;


    void Start() {
        isActive = false;
        isWin = false;
        playerScore = 0;
        enemyScore = 0;
        StartText.gameObject.SetActive(true);
        WinText.gameObject.SetActive(false);
        PlayerText.text = "<sprite=0>";
        EnemyText.text = "<sprite=0>";
        Time.timeScale = 0;
        var ball = GameObject.Find("Ball").GetComponent<Ball>();
        ball.OnPlayerScore += Ball_OnPlayerScore;
        ball.OnEnemyScore += Ball_OnEnemyScore;
    }

    private void Ball_OnEnemyScore() {
        GetComponent<AudioSource>().PlayOneShot(PointClip);
        enemyScore++;
        string str = enemyScore.ToString();
        StringBuilder builder = new StringBuilder();
        for (int i = 0; i < str.Length; i++) {
            builder.Append($"<sprite={str[i]}>");
        }
        EnemyText.text = builder.ToString();

        CheckWin();
    }

    private void CheckWin() {
        if (playerScore == WinScore) {
            isWin = true;
            WinText.text = "Победа";
            WinText.gameObject.SetActive(true);
            Time.timeScale = 0;
        } else if (enemyScore == WinScore) {
            isWin = true;
            WinText.text = "Поражение";
            WinText.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }

    private void Ball_OnPlayerScore() {
        GetComponent<AudioSource>().PlayOneShot(PointClip);
        playerScore++;
        string str = playerScore.ToString();
        StringBuilder builder = new StringBuilder();
        for (int i = 0; i < str.Length; i++) {
            builder.Append($"<sprite={str[i]}>");
        }
        PlayerText.text = builder.ToString();

        CheckWin();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (!isActive && !isWin) {
                isActive = true;
                StartText.gameObject.SetActive(false);
                Time.timeScale = 1;
            } else if (isWin) {
                var ball = GameObject.Find("Ball").GetComponent<Ball>();
                ball.OnPlayerScore -= Ball_OnPlayerScore;
                ball.OnEnemyScore -= Ball_OnEnemyScore;
                Start();
            }
        }
    }
}
