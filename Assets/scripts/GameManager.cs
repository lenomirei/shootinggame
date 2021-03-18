using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance_;

    public Transform canvas_main_;
    public Transform canvas_game_over_;
    public Text text_score_;
    public Text text_best_score_;
    public Text text_life_;

    protected int score_;
    protected int best_score_;
    protected Player player_;

    public AudioClip music_clip_;
    protected AudioSource audio_;
    // Start is called before the first frame update
    void Start()
    {
        instance_ = this;
        audio_ = this.gameObject.AddComponent<AudioSource>();
        audio_.clip = music_clip_;
        audio_.loop = true;
        audio_.Play();

        player_ = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        text_score_ = canvas_main_.Find("TextScore").GetComponent<Text>();
        text_best_score_= canvas_main_.Find("TextBest").GetComponent<Text>();
        text_life_ = canvas_main_.Find("TextLife").GetComponent<Text>();
        text_score_.text = string.Format("分数, {0}", score_);
        text_best_score_.text = string.Format("最高分, {0}", best_score_);
        text_life_.text = string.Format("生命, {0}", player_.life_);

        var restart_button = canvas_game_over_.transform.Find("Button").GetComponent<Button>();
        restart_button.onClick.AddListener(delegate () {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        });

        canvas_game_over_.gameObject.SetActive(false);


    }

    public void AddScore(int point) {
        score_ += point;
        text_score_.text = string.Format("分数, {0}", score_);
        if (best_score_ < score_) {
            best_score_ = score_;
            text_best_score_.text = string.Format("最高分, {0}", best_score_);
        }

    }

    public void ChangeLife(int life) {
        text_life_.text = string.Format("生命, {0}", life);
        if (life <= 0) {
            canvas_game_over_.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
