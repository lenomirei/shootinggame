using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public float speed_ = 10;
    public float live_time_ = 1;
    public float power_ = 1.0f;
    protected Transform transform_;
    // Start is called before the first frame update
    void Start()
    {
        this.transform_ = this.transform;
        Destroy(this.gameObject, this.live_time_);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform_.Translate(new Vector3(0, 0, -this.speed_ * Time.deltaTime));
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.CompareTo("Enemy") == 0) {
            Destroy(this.gameObject);
        }
    }
}
