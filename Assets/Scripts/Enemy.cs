using UnityEngine;
using UnityEngine.UI;


public class Enemy : MonoBehaviour
{
    public float speed = 10f;

    public float startHealth = 100;
    private float health;

    public int value = 50;

    public GameObject deathEffect;

    public Image healthbar;

    private Transform target;
    private int waypointsIndex = 0;
   
    void Start()
    {
        target = Waypoints.points[0];
        health = startHealth;
    }

    public void TakeDamage(int amount)
    {
        health -= amount;

        healthbar.fillAmount = health / startHealth;


        if(health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {

        PlayerStats.money += value;

        GameObject deathParticles = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(deathParticles, 2f);
        Destroy(gameObject);

        WaveSpawner.EnemiesAlive--;
    }

    private void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if(Vector3.Distance(transform.position, target.position) <= 0.3)
        {
            GetNextWaypoint();
        }
    }

    private void GetNextWaypoint()
    {
        if(waypointsIndex >= Waypoints.points.Length - 1)
        {
            Destroy(gameObject);
            EndPath();
            return;
        }

        waypointsIndex++;
        target = Waypoints.points[waypointsIndex];
    }

    private void EndPath()
    {
        PlayerStats.lives--;
        WaveSpawner.EnemiesAlive--;
        Destroy(gameObject);
    }
}
