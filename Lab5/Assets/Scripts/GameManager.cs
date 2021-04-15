using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public List<AudioClip> DieClips;
    public List<AudioClip> WingClips;
    public List<AudioClip> HitClips;
    public List<AudioClip> PointClips;
    public List<AudioClip> SwooshClips;

    public GameObject PipeGenerator;

    public Image InfoImage;
    public Image GameOverImage;
    public TextMeshProUGUI ScoreText;

    public int Points;
    private Player player;
    private GameObject playerGM;

    public bool IsPlaying;
    public event System.Action<bool> IsPlayingChange;

    void Start() {
        playerGM = GameObject.FindGameObjectWithTag("Player");
        player = playerGM.GetComponent<Player>();
        player.Died += Player_Died;
        player.Winged += Player_Winged;
        Points = 0;
        IsPlaying = false;
    }

    private void Player_Winged() {
        playerGM.GetComponent<AudioSource>().PlayOneShot(WingClips[Random.Range(0, WingClips.Count)]);
    }

    private void Player_Died() {
        playerGM.GetComponent<AudioSource>().PlayOneShot(HitClips[Random.Range(0, HitClips.Count)]);
        playerGM.GetComponent<AudioSource>().PlayOneShot(DieClips[Random.Range(0, DieClips.Count)]);
        IsPlaying = false;
        IsPlayingChange?.Invoke(false);
        GameOverImage.gameObject.SetActive(true);
        //GameObject.Destroy(GameObject.FindGameObjectWithTag("Generator"));
        PipeGenerator.SetActive(false);
    }

    public void AddPoint() {
        Points++;
        playerGM.GetComponent<AudioSource>().PlayOneShot(PointClips[Random.Range(0, PointClips.Count)]);
        SetPointsUI();
    }

    private void SetPointsUI() {
        string p = Points.ToString();
        System.Text.StringBuilder builder = new System.Text.StringBuilder();
        for (int i = 0; i < p.Length; i++) {
            builder.Append($"<sprite={int.Parse(p[i].ToString())}>");
        }
        ScoreText.text = builder.ToString();
    }

    private void Update() {
        if (!IsPlaying && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))) {
            IsPlaying = true;
            Points = 0;

            var Pipes = GameObject.FindGameObjectsWithTag("Pipe");
            for (int i = 0; i < Pipes.Length; i++) {
                GameObject.Destroy(Pipes[i]);
            }

            player.Reset();

            player.SetIsActive(true);
            PipeGenerator.SetActive(true);
            InfoImage.gameObject.SetActive(false);
            GameOverImage.gameObject.SetActive(false);
            IsPlayingChange?.Invoke(true);
            ScoreText.text = "<sprite=0>";
            playerGM.GetComponent<AudioSource>().PlayOneShot(SwooshClips[Random.Range(0, SwooshClips.Count)]);
            //GameObject.Instantiate(PipeGenerator);
            
        }
    }
}
