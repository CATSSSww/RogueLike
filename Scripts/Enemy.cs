using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] private int health;
    [SerializeField] private float normalSpeed;
    [SerializeField] private int damage;
    [SerializeField] private float startStopTime;
    [SerializeField] private float startTimeBtwAttack;
    private Player player;
    private Animator anim;
    
    private float speed;
    private float timeBtwAttack;
    private float stopTime;


    private void Start()
    {
        anim = GetComponent<Animator>();
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        if(stopTime < 0)
        {
            speed = normalSpeed;
        }
        else
        {
            speed = 0;
            stopTime -= Time.deltaTime;
        }
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        if(player.transform.position.x > transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        stopTime = startStopTime;
        health -= damage;
    }
    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if(timeBtwAttack <= 0)
            {
                anim.SetTrigger("enemyAttack");
            }
            else
            {
                timeBtwAttack -= Time.deltaTime;
            }
        }
    }
    public void OnEnemyAttack()
    {
        player.ChangeHealth(-damage);
        timeBtwAttack = startTimeBtwAttack;
    }
}
