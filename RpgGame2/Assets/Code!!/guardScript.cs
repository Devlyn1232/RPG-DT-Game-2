using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class guardScript : MonoBehaviour
{
    //This has no parameters and has a void return type
    public static event System.Action OnGuardHasSpottedPlayer;

    public Transform pathHolder;

    [SerializeField] private float walkSpeed = 4f;
    [SerializeField] private float runSpeed = 6f;
    [SerializeField] private float waitTime = 0.3f;
    [SerializeField] private float turnSpeed = 90;
    [SerializeField] private float timeToSeePlayer = 0.5f;

    [SerializeField] private NavMeshAgent agent;


    [SerializeField] private Light spotlight;
    [SerializeField] private float viewDistance;
    [SerializeField] private LayerMask viewMask;

  

    private float viewAngle;
    private float playerVisibleTimer;

    Color originalSpotlightColor;

    [SerializeField] private Transform Player;
    [SerializeField] private Transform Guard;


    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        viewAngle = spotlight.spotAngle;
        originalSpotlightColor = spotlight.color;

        Vector3[] waypoints = new Vector3[pathHolder.childCount];
        for (int i = 0; i < waypoints.Length; i ++)
        {
            waypoints[i] = pathHolder.GetChild(i).position;
            waypoints[i] = new Vector3(waypoints[i].x, transform.position.y, waypoints[i].z);
        }
        agent = GetComponent<NavMeshAgent>();
        agent.isStopped = false;
        agent.speed = walkSpeed;

        StartCoroutine(FollowPath (waypoints));


    }

    private void Update()
    {
        if (canSeePlayer())
        {
            playerVisibleTimer += Time.deltaTime;
        }
        else
        {
            playerVisibleTimer -= Time.deltaTime;
        }
        playerVisibleTimer = Mathf.Clamp(playerVisibleTimer, 0, timeToSeePlayer);
        spotlight.color = Color.Lerp(originalSpotlightColor, Color.red, playerVisibleTimer / timeToSeePlayer);

    }

    bool canSeePlayer()
    {
        if (Vector3.Distance(transform.position,Player.position) < viewDistance)
            //if player is in the view distance
        {
            //find if the player is inbetween the guards forward position and the player position and determine smallest angle between the 2
            Vector3 dirToPlayer = (Player.position - transform.position).normalized;
            float angleBetweenGuardAndPlayer = Vector3.Angle(transform.forward, dirToPlayer);
            if (angleBetweenGuardAndPlayer < viewAngle / 2f)
            {
                //see if line of sight to the player is blocked
                //use linecast to use 2 points to cast the ray between
                //if it has not hit anything, it can finally see the player
                if(!Physics.Linecast(transform.position, Player.position, viewMask))
                {
                    return true;
                }
            }
        }
        //if one check fails, its false
        return false;
    }

    IEnumerator FollowPath(Vector3[] waypoints)
    {
        int targetWaypointIndex = 0;
        Vector3 targetWaypoint = waypoints[targetWaypointIndex];
        transform.LookAt(targetWaypoint);
        {
            while (true)
            {

                if (!canSeePlayer())
                {
                    // Check if we've reached the destination
                    if (!agent.pathPending)
                    {
                        if (agent.remainingDistance <= agent.stoppingDistance)
                        {
                            if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                            {
                                //When targetWaypointIndex is equal to waypoints.length, the value will reset to 0
                                targetWaypointIndex = (targetWaypointIndex + 1) % waypoints.Length;

                                targetWaypoint = waypoints[targetWaypointIndex];

                                yield return new WaitForSeconds(waitTime);

                                //to wait while guard is rotating, use yield return
                                StartCoroutine(TurnToFace(targetWaypoint));
                            }
                        }
                    }
                }
                //transform.position = Vector3.MoveTowards(transform.position, targetWaypoint, walkSpeed * Time.deltaTime);
                agent.SetDestination(targetWaypoint);
                agent.speed = walkSpeed;

                if (canSeePlayer())
                {
                    agent.SetDestination(Player.position);
                    transform.LookAt(Player);
                    agent.speed = runSpeed;
                }
                yield return null;

            }
        }
    }

    IEnumerator TurnToFace(Vector3 lookTarget) // calculate angle guard needs on y axis to be facing the look target
        //if we have look direction, we can use mathf to find angle
    {
        
        Vector3 dirToLookTarget = (lookTarget - transform.position).normalized;
        //* as it is in radians
        float targetAngle = 90 - Mathf.Atan2(dirToLookTarget.z,dirToLookTarget.x) * Mathf.Rad2Deg;

        //to make guard rotate to angle over time
        //we need to stop the loop below when we are looking at the target thats why we use delta angle
        //delta angle tells us how many degrees apart 2 angles are so when the angle is 0 it will stop rotation
        while (Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.y, targetAngle)) > 0.05f)
            //Mathf.abs is used as it wont accept negative angles and wont turn when the value is -
            //0.05 is used as the euler angles have a chance to never reach exactly the target angle so we use a small value
        {
            float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetAngle, turnSpeed * Time.deltaTime);
            transform.eulerAngles = Vector3.up * angle;
            yield return null;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("1st step");
             OnGuardHasSpottedPlayer();
            if(OnGuardHasSpottedPlayer != null)
            {
                Debug.Log("2nd step");
            }

        }
    }



    //  }
    // }

    private void OnDrawGizmos()
    {
        Vector3 startPosition = pathHolder.GetChild(0).position;
        Vector3 previousPosition = startPosition;

        foreach (Transform waypoint in pathHolder)
        {
            Gizmos.DrawSphere(waypoint.position, 0.3f);
            Gizmos.DrawLine(previousPosition, waypoint.position);
            previousPosition = waypoint.position;
        }
        Gizmos.DrawLine(previousPosition, startPosition);

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * viewDistance);
    }

}
