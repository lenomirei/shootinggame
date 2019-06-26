using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed_ = 1;
    private Transform transform_;
    public Transform rocket_;
    public int life_ = 3;
    public AudioClip shoot_clip_;
    protected AudioSource audio_;
    public Transform explosionFX_;

    // 子弹发射计时器
    float shoot_timer_ = 0;

    // Start is called before the first frame update
    void Start()
    {
        audio_ = this.GetComponent<AudioSource>();
        transform_ = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0)) {
            shoot_timer_ -= Time.deltaTime;
            if (shoot_timer_ <= 0)
            {
                shoot_timer_ = 0.1f;
                Instantiate(rocket_, this.transform_.position, this.transform_.rotation);
                audio_.PlayOneShot(shoot_clip_);
            }
        }
        // 水平移动距离
        float moveh = 0;
        // 纵向移动距离
        float movev = 0;
        if (Input.GetKey(KeyCode.UpArrow)) {
            movev -= speed_ * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow)) {
            movev += speed_ * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftArrow)) {
            moveh += speed_ * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            moveh -= speed_ * Time.deltaTime;
        }
        this.transform_.Translate(new Vector3(moveh, 0, movev));
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.CompareTo("PlayerRocket") != 0) {
            life_ -= 1;
            GameManager.instance_.ChangeLife(life_);
            if (life_ <= 0) {
                Instantiate(explosionFX_, transform_.position, transform_.rotation);
                Destroy(this.gameObject);
            }
        }
    }
}
