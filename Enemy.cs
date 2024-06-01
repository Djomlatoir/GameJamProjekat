using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{

  //  private HealthPlayer playerHealth;

    public NavMeshAgent agent;


    public int damage;
   
    public Transform player;
    public float fireforce = 32f;
    public float fireforceUP = 5f;
   


    public GameObject[] AllObjects;
    public GameObject NearestOBJ;

    float distance;
    float nearestDistance = 10000;

    public LayerMask whatIsGround, whatIsPlayer;

    public float health;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;



    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;


    private void Awake()
    {

      //  playerHealth = gameObject.GetComponent<HealthPlayer>();
        if (GameObject.Find("FirstPerson 1") != null)
            player = GameObject.Find("FirstPerson 1").transform;
      
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (playerInAttackRange && playerInSightRange) AttackPlayer();
        for (int i = 0; i < AllObjects.Length; i++)
        {
            distance = Vector3.Distance(this.transform.position, AllObjects[i].transform.position);

            if (distance < nearestDistance)
            {
                NearestOBJ = AllObjects[i];
                nearestDistance = distance;


            }

            if (player == null)
            {
                NearestOBJ = AllObjects[i];
                player = NearestOBJ.transform;
            }

            player = NearestOBJ.transform;


        }
        AllObjects = GameObject.FindGameObjectsWithTag("Player");

        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange)
        {

            Patroling();

        }
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        

    }

    private void Patroling()
    {

        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
        // Debug.Log("Player transform ="+ player.position);
    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(player);


        if (!alreadyAttacked)
        {
            ///Attack code here
            if (attackRange >= 3)
            {
                Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
                rb.AddForce(transform.forward * fireforce, ForceMode.Impulse);
                rb.AddForce(transform.up * fireforceUP, ForceMode.Impulse);
            }

            ///End of attack code

            //   GetComponent<HealthPlayer>().TakeDamage(damage);
            alreadyAttacked = true;

            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
    }
    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
        Gizmos.color = Color.gray;
        Gizmos.DrawWireSphere(transform.position, walkPointRange);
    }


}
