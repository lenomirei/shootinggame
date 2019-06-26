using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperEnemy : Enemy
{
    public Transform enemy_rocket_;
    protected float fire_timer_ = 2;
    protected Transform player_;

    void Awake()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("Player");
        if (obj != null) {
            player_ = obj.transform;
        }
    }

    protected override void UpdateMove() {
        fire_timer_ -= Time.deltaTime;
        if (fire_timer_ <= 0) {
            fire_timer_ = 2;
            if (player_ != null) {
                Vector3 relative_pos = transform_.position - player_.position;
                Instantiate(enemy_rocket_, transform_.position, Quaternion.LookRotation(relative_pos));

            }
        }

        transform_.Translate(new Vector3(0, 0, -speed_ * Time.deltaTime));
    }

}
