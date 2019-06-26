using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed_ = 1;
    public int life_ = 10;
    protected float rotate_speed_ = 30;
    protected Transform transform_;
    internal Renderer renderer_;
    internal bool active_;
    public int point_ = 10;

    // Start is called before the first frame update
    void Start()
    {
        transform_ = this.transform;
        renderer_ = this.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMove();
        if (renderer_.isVisible)
        {
            //Debug.Log("Object is visible");
        }
        else Debug.Log("Object is no longer visible");
    }

    protected virtual void UpdateMove() {
        float rx = Mathf.Sin(Time.time) * Time.deltaTime;
        this.transform_.Translate(new Vector3(rx, 0, -this.speed_ * Time.deltaTime));
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

    public void OnBecameInvisible()
    {

    }

    void OnBecameVisible()
    {
        active_ = true;
    }
}
