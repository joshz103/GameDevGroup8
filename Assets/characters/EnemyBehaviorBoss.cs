using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;

public class EnemyBehaviorBoss : MonoBehaviour
{
    public EnemyHP hp;
    public Animator animator;
    private bool dead = false;

    private playerstats stats;

    public EnemyAttackHitboxBoss attackHitbox;

    private NavMeshAgent agent;
    private Transform player;
    private PlayerController playerController;
    public LayerMask groundMask;
    public LayerMask playerMask;

    //Patrolling
    private Vector3 walkPoint;
    private bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    private bool alreadyAttacked;

    //States
    public float sightRange;
    public float attackRange;
    private bool playerInSightRange;
    private bool playerInAttackRange;

    private bool canMove = true;
    private bool canTurnStationary = true;

    public float turnSpeed;

    public float attackDamage;

    public bool chasePlayer;

    public float aimTime;

    public GameObject currency1;
    public int currencyDropChance;
    private bool hasDroppedItem = false;

    private EnemyWaves waves;

    //Boss Related Stuff
    private int bossAction;
    public int numberOfBossActions;
    public GameObject spawnedEnemy;
    public Transform spawnedEnemySpawnpoint;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        stats = GameObject.FindGameObjectWithTag("Player").GetComponent<playerstats>();
        agent = gameObject.GetComponent<NavMeshAgent>();
        waves = GameObject.FindGameObjectWithTag("EnemyWaveSpawner").GetComponent<EnemyWaves>();
        //hp = gameObject.GetComponent<EnemyHP>();
        //animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Status
        checkIfDead();

        //AI
        if (!dead)
        {
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerMask);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerMask);

            //Movement Animation
            if (agent.remainingDistance > 0)
            {
                animator.SetBool("isMoving", true);
            }
            else
            {
                animator.SetBool("isMoving", false);
            }

            if (canMove)
            {
                //State triggers
                if (!playerInSightRange && !playerInAttackRange)
                {
                    Patrolling();
                }

                if (chasePlayer)
                {
                    if (playerInSightRange && !playerInAttackRange)
                    {
                        ChasePlayer();
                    }
                }
            }

            if (playerInSightRange && playerInAttackRange)
            {
                Attacking();
            }

        }


    }

    //AI Status
    public void checkIfDead()
    {
        if (hp.getHP() <= 0)
        {
            if (dead == false)
            {
                if (waves) //If we spawn an enemy for testing or something, it wont get stuck here and interrupt death animations and stuff
                {
                    waves.removeEnemyCount();
                }
            }

            animator.SetBool("isDead", true);

            gameObject.layer = 9;

            dead = true;
            if (Random.Range(0, 100) < currencyDropChance && hasDroppedItem == false)
            {
                Debug.Log("Dropped item");
                Instantiate(currency1, transform.position, transform.rotation);
            }
            hasDroppedItem = true;
            Destroy(gameObject, 2f);
        }
    }

    public bool isDead()
    {
        return dead;
    }

    //AI Behavior

    private void Patrolling()
    {
        if (!walkPointSet)
        {
            SearchWalkPoint();
        }

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }

    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, groundMask))
        {
            walkPointSet = true;
        }
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void Attacking()
    {
        if (!alreadyAttacked)
        {
            initiateAttack();
        }

        if (canTurnStationary)
        {
            agent.SetDestination(transform.position);
            Vector3 relativePos = player.transform.position - transform.position;
            Quaternion toRotation = Quaternion.LookRotation(relativePos);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, turnSpeed * Time.deltaTime);
        }
    }

    private void initiateAttack()
    {
        canMove = false;
        alreadyAttacked = true;
        canTurnStationary = true;

        StartCoroutine(cooldown());
        StartCoroutine(turningStationary());
        pickBossAction();
    }

    IEnumerator cooldown()
    {
        yield return new WaitForSeconds(timeBetweenAttacks);
        alreadyAttacked = false;
        canMove = true;
    }

    IEnumerator turningStationary()
    {
        yield return new WaitForSeconds(aimTime);
        canTurnStationary = false;
    }

    public void triggerHitbox() //This is triggered through an animation event that is tied to whatever frame of the animation should deal damage!
    {
        attackHitbox.triggerHitbox();
    }

    public float getAttackDamage()
    {
        return attackDamage;
    }

    //Boss stuff

    public void pickBossAction()
    {
        bossAction = UnityEngine.Random.Range(1, numberOfBossActions + 1);
        animator.SetInteger("BossAction", bossAction);
    }

    public void resetBossAction()
    {
        animator.SetInteger("BossAction", 0);
    }

    public void summonEnemy()
    {
        if (waves) //If we spawn an enemy for testing or something, it wont get stuck here and interrupt
        {
            waves.addEnemyCount();
        }
        Instantiate(spawnedEnemy, spawnedEnemySpawnpoint.transform.position, spawnedEnemySpawnpoint.transform.rotation);
    }    

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = UnityEngine.Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = UnityEngine.Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

}
