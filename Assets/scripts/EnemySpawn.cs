using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public Transform enemy_;
    protected Transform transform_;
    // Start is called before the first frame update
    void Start()
    {
        transform_ = this.transform;
        StartCoroutine(SpawnEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected IEnumerator SpawnEnemy() {
        yield return new WaitForSeconds(Random.Range(5, 15));
        Instantiate(enemy_, transform_.position, transform_.rotation);

        StartCoroutine(SpawnEnemy());
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position, "item.png", true);
    }
}
