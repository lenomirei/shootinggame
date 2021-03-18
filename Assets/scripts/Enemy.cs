using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed_ = 1;
    public int life_ = 1;
    protected float rotate_speed_ = 30;
    protected Transform transform_;
    internal Renderer renderer_;
    public int point_ = 10;
    public Transform background_;

    // Start is called before the first frame update
    void Start()
    {
        background_ = GameObject.Find("star").transform;
        transform_ = this.transform;
        renderer_ = this.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMove();
        if (NeedDelete())
        {
            Destroy(this.gameObject);
        }
    }

    protected virtual void UpdateMove() {
        float rx = Mathf.Sin(Time.time) * Time.deltaTime;
        this.transform_.Translate(new Vector3(rx, 0, -this.speed_ * Time.deltaTime));
    }

    protected virtual bool NeedDelete() {
        Transform camera_main = Camera.main.transform;
        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform_.position);
        Vector3 dir = (transform_.position - camera_main.position).normalized;
        float dot = Vector3.Dot(camera_main.forward, dir);
        if (dot > 0 && viewPos.x > 0 && viewPos.x < 1 && viewPos.y > 0 && viewPos.y < 1)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.CompareTo("PlayerRocket") == 0)
        {
            Rocket rocket = other.GetComponent<Rocket>();
            if (rocket != null)
            {
                life_ -= (int)rocket.power_;
                if (life_ <= 0)
                {
                    GameManager.instance_.AddScore(point_);
                    Destroy(this.gameObject);
                }
            }
        }
        else if (other.tag.CompareTo("Player") == 0) {
            life_ = 0;
            Destroy(this.gameObject);
        }
    }

}
