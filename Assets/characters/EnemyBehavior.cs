using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class EnemyBehavior : MonoBehaviour
{
    public EnemyHP hp;
    public Animator animator;
    private bool dead = false;

    public EnemyAttackHitbox attackHitbox;

    public NavMeshAgent agent;
    public Transform player;
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
    //private bool canAttack = true;

    //public float attackCooldown;

    public float turnSpeed;

    public float aimTime;

    public float attackDamage;

    public bool chasePlayer;

    public bool firesInsteadOfAttack;
    public GameObject projectile;
    public GameObject firingPoint;
    [SerializeField] private float projectileSpeed;
    float accuracy = 1f;
    private Vector3 playerOffset = new Vector3(0, 0.5f, 0);

    public GameObject currency1;
    public int currencyDropChance;
    private bool hasDroppedItem = false;

    private EnemyWaves waves;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        waves = GameObject.FindGameObjectWithTag("EnemyWaveSpawner").GetComponent<EnemyWaves>();
    }

    // Update is called once per frame
    void Update()
    {
        //Status
        checkIfDead();

        //AI
        if(!dead)
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
                    animator.SetBool("isAttacking", false);
                }

                if (chasePlayer)
                {
                    if (playerInSightRange && !playerInAttackRange)
                    {
                        ChasePlayer();
                        animator.SetBool("isAttacking", false);
                        //animator.SetBool("isMoving", true);
                    }
                }
            }

            if(!firesInsteadOfAttack)
            {
                if (playerInSightRange && playerInAttackRange)
                {
                    Attacking();
                    //animator.SetBool("isAttacking", true);
                    //animator.SetBool("isMoving", false);
                }
            }

            if(firesInsteadOfAttack)
            {
                if(playerInSightRange && playerInAttackRange)
                {
                    Firing();
                }
            }
        }
    }


    //AI Status
    public void checkIfDead()
    {
        if (hp.getHP() <= 0)
        {
            if(dead == false)
            {
                waves.removeEnemyCount();
            }

            animator.SetBool("isDead", true);
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

        if(walkPointSet)
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

        if(canTurnStationary)
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
        StartCoroutine(animationTrigger());
        StartCoroutine(turningStationary());
    }

    IEnumerator cooldown()
    {
        yield return new WaitForSeconds(timeBetweenAttacks);
        alreadyAttacked = false;
        canMove = true;
    }

    IEnumerator animationTrigger()
    {
        animator.SetBool("isAttacking", true);
        yield return new WaitForSeconds(0.1f);
        animator.SetBool("isAttacking", false);
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


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = UnityEngine.Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = UnityEngine.Color.yellow;
        Gizmos.DrawWireSphere (transform.position, sightRange);
    }

    private void Firing()
    {
        if (!alreadyAttacked)
        {
            initiateFiring();
        }

        if (canTurnStationary)
        {
            agent.SetDestination(transform.position);
            Vector3 relativePos = player.transform.position - transform.position;
            Quaternion toRotation = Quaternion.LookRotation(relativePos);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, turnSpeed * Time.deltaTime);
        }
    }

    private void SpawnProjectile()
    {
        Vector3 playerTargetMain = player.transform.position + playerOffset;

        GameObject obj = Instantiate(projectile, firingPoint.transform.position, firingPoint.transform.rotation);
        obj.GetComponent<Rigidbody>().velocity = BallisticVelocity(firingPoint.transform.position, playerTargetMain, Vector3.zero);
    }

    private void initiateFiring() //This is triggered through an animation event that is tied to whatever frame of the animation should fire!
    {
        canMove = false;
        alreadyAttacked = true;
        canTurnStationary = true;

        StartCoroutine(cooldown());
        StartCoroutine(animationTrigger());
        StartCoroutine(turningStationary());
    }

    //This is used for arcing projectiles. They will land on players!
    public Vector3 BallisticVelocity(Vector3 source, Vector3 target, Vector3 targetVelocity)
    {
        Vector3 horiz = new Vector3(target.x - source.x, 0, target.z - source.z);
        float t = horiz.magnitude / projectileSpeed;
        for (int a = 0; a < accuracy; a++)
        {
            horiz = new Vector3(target.x + targetVelocity.x * t - source.x, 0, target.z + targetVelocity.z * t - source.z);
            t = horiz.magnitude / projectileSpeed;
        }
        // after t seconds, the cannonball will reach the horizontal location of the target -
        // so all we have to do is make sure its 'y' coordinate zeros out right there
        float gravityY = (.5f * Physics.gravity * t * t).y;
        // now we've calculated how much the projectile will fall during that time
        // so let's add a 'y' component to the velocity that will take care of the rest
        float yComponent = (target.y - source.y - gravityY) / t + targetVelocity.y;

        horiz = horiz.normalized * projectileSpeed;
        return new Vector3(horiz.x, yComponent, horiz.z);
    }

}
