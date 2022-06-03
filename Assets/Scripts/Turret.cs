using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Turret : MonoBehaviour
{
    private Transform target;
    public float range = 15f;

    public float fireRate = 1f;
    private float fireCoutdown = 0f;

    public GameObject bulletPrefab;
    public Transform firePoint;

    public string enemyTag = "Enemy";
    bool updatingTarget = true;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(UpdateTarget());
        //InvokeRepeating("Updatetarget", 0f, 0.5f);
    }


    IEnumerator UpdateTarget()
    {
        Debug.Log("toto");

        while (updatingTarget)
        {
            Debug.Log("toto2");

            GameObject[] ennemies = GameObject.FindGameObjectsWithTag(enemyTag);
            float shortestDistance = Mathf.Infinity;
            GameObject nearestEnemy = null;

            foreach (GameObject enemy in ennemies)
            {
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                if (distanceToEnemy < shortestDistance)
                {
                    shortestDistance = distanceToEnemy;
                    nearestEnemy = enemy;
                }
            }
            if (nearestEnemy != null && shortestDistance <= range)
            {
                target = nearestEnemy.transform;
                GetComponent<Animator>().SetTrigger("StartAnimation");
            }
            else
            {
                GetComponent<Animator>().SetTrigger("StopAnimation");
                target = null;
            }
            yield return new WaitForSeconds(1);

        }

        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            return;
        }

        Vector3 dir = target.position - transform.position;

        if (fireCoutdown <= 0f)
        {
            Shoot();
            fireCoutdown = 1 / fireRate;
        }


        fireCoutdown -= Time.deltaTime;
    }

    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.Seek(target);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }


}
