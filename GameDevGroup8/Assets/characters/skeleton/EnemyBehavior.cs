using System.Collections;
using System.Collections.Generic;
using System.Drawing;
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

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        //Status
        checkIfDead();

        //AI
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

            if (playerInSightRange && !playerInAttackRange)
            {
                ChasePlayer();
                animator.SetBool("isAttacking", false);
                //animator.SetBool("isMoving", true);
            }
        }

        if (playerInSightRange && playerInAttackRange)
        {
            Attacking();
            //animator.SetBool("isAttacking", true);
            //animator.SetBool("isMoving", false);
        }


    }


    //AI Status
    public void checkIfDead()
    {
        if (hp.getHP() <= 0)
        {
            animator.SetBool("isDead", true);
            dead = true;
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
        //animator.SetBool("isAttacking", true);
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


}
