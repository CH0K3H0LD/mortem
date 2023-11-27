using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    public Transform[] waypoints;
    private int currentWaypointIndex = 0;
    public float patrolSpeed = 3f;
    public float chaseSpeed = 6f;
    public float detectionRange = 2f;
    public float fieldOfViewAngle = 90f;
    public float chaseDuration = 5f;
    private float chaseTimer = 0f;
    private NavMeshAgent navMeshAgent;
    private Transform player;

    private bool isChasing = false;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        SetNextWaypoint();
    }

    void Update()
    {
        if (CanSeePlayer())
        {
            if (!isChasing)
            {
                StartChasing();
            }
            else
            {
                // If already chasing, continuously update destination to player's position
                navMeshAgent.SetDestination(player.position);

                // Check if enemy caught up to player
                if (Vector3.Distance(transform.position, player.position) < 1f)
                {
                    // Reset the scene
                    ResetScene();
                }
            }
        }
        else if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.5f)
        {
            if (isChasing)
            {
                StopChasing();
            }
            else
            {
                SetNextWaypoint();
            }
        }

        if (isChasing)
        {
            if (chaseTimer > 0)
            {
                chaseTimer -= Time.deltaTime;
            }
            else
            {
                StopChasing();
            }
        }
    }

    void SetNextWaypoint()
    {
        if (waypoints.Length == 0)
            return;

        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        Vector3 target = waypoints[currentWaypointIndex].position;

        navMeshAgent.SetDestination(target);
        navMeshAgent.speed = patrolSpeed;
    }

    bool CanSeePlayer()
    {
        if (Vector3.Distance(transform.position, player.position) < detectionRange)
        {
            Vector3 directionToPlayer = (player.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToPlayer) < fieldOfViewAngle * 0.5f)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, directionToPlayer, out hit, detectionRange))
                {
                    if (hit.collider.CompareTag("Player"))
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    void StartChasing()
    {
        isChasing = true;
        chaseTimer = chaseDuration;
        navMeshAgent.SetDestination(player.position);
        navMeshAgent.speed = chaseSpeed;
    }

    void StopChasing()
    {
        isChasing = false;
        navMeshAgent.ResetPath();
        navMeshAgent.speed = patrolSpeed;
        chaseTimer = 0f;
    }

    void ResetScene()
    {
        // Reload the current scene
        Debug.Log("Resetting the scene...");
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}   