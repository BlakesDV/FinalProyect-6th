using UnityEngine;

/// <summary>
/// Represents an aggressive agent with perception and decision-making capabilities.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class AgresivoTerritorial : BasicAgent
{

    [SerializeField] Animator animator;
    [SerializeField] AgressiveAgentStates agentState;
    [SerializeField]
    Transform eyesPercept;
    Rigidbody rb;
    [SerializeField] Collider[] perceibed;
    string currentAnimationStateName;
    [SerializeField] bool insideCollider = false;
    [SerializeField] Vector3 cubeSize;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        agentState = AgressiveAgentStates.None;
        currentAnimationStateName = "";
    }

    void Update()
    {
        decisionManager();
    }

    private void FixedUpdate()
    {
        perceibed = Physics.OverlapBox(eyesPercept.position, cubeSize * .5f);
    }
    private void OnCollisionEnter(Collision collision)
    {
        perceptionManager(collision);
    }

    void perceptionManager(Collision collision)
    {
        //if usuario en box
        target = null;
        if (perceibed != null)
        {
            foreach (Collider tmp in perceibed)
            {
                if (tmp.CompareTag("Player"))
                {
                    target = tmp.transform;
                    insideCollider = true;
                }
            }
        }
    }


    void decisionManager()
    {
        AgressiveAgentStates newState;
        if (target == null)
        {
            insideCollider = false;
            newState = AgressiveAgentStates.Wander;
        }
        else if (insideCollider)
        {
            newState = AgressiveAgentStates.Pursuit;
            if (Vector3.Distance(transform.position, target.position) < stopThreshold)
            {
                newState = AgressiveAgentStates.Attack;
            }
        }
        else
        {
            newState = AgressiveAgentStates.Escape;
        }
        changeAgentState(newState);
        actionManager();
        movementManager();
    }

    /// <param name="t_newState">The new state of the agent.</param>
    void changeAgentState(AgressiveAgentStates t_newState)
    {
        if (agentState == t_newState)
        {
            return;
        }
        agentState = t_newState;
        if (agentState != AgressiveAgentStates.Wander)
        {
            wanderNextPosition = null;
        }
    }

    void actionManager()
    {
        switch (agentState)
        {
            case AgressiveAgentStates.None:
                break;
            case AgressiveAgentStates.Attack:
                // biting();
                break;
            case AgressiveAgentStates.Escape:
                // screaming();
                break;
        }
    }

    void movementManager()
    {
        switch (agentState)
        {
            case AgressiveAgentStates.None:
                rb.velocity = Vector3.zero;
                break;
            case AgressiveAgentStates.Pursuit:
                pursuiting();
                break;
            case AgressiveAgentStates.Attack:
                attacking();
                break;
            case AgressiveAgentStates.Wander:
                wandering();
                break;
        }
    }

    private void wandering()
    {
        if (!currentAnimationStateName.Equals("SniffGround"))
        {
            Debug.Log(currentAnimationStateName);
            animator.Play("SniffGround", 0);
            currentAnimationStateName = "SniffGround";
        }
        if ((wanderNextPosition == null) ||
            (Vector3.Distance(transform.position, wanderNextPosition.Value) < 0.5f))
        {
            wanderNextPosition = SteeringBehaviours.wanderNextPos(this);
        }
        rb.velocity = SteeringBehaviours.seek(this, wanderNextPosition.Value);
    }

    private void pursuiting()
    {
        if (!currentAnimationStateName.Equals("Run") && !currentAnimationStateName.Equals("Walk"))
        {
            animator.Play("Run", 0);
            currentAnimationStateName = "Run";
        }
        maxVel *= 2;
        rb.velocity = SteeringBehaviours.seek(this, target.position);
        rb.velocity = SteeringBehaviours.arrival(this, target.position, slowingRadius, stopThreshold);
        if (Vector3.Distance(transform.position, target.position) <= slowingRadius)
        {
            if (!currentAnimationStateName.Equals("Walk"))
            {
                animator.Play("Walk", 0);
                currentAnimationStateName = "Walk";
            }
        }
        maxVel /= 2;
    }

    private void attacking()
    {
        if (!currentAnimationStateName.Equals("Bite"))
        {
            animator.Play("Bite", 0);
            currentAnimationStateName = "Bite";
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(eyesPercept.position, cubeSize);
    }

    private enum AgressiveAgentStates
    {
        None,
        Pursuit,
        Attack,
        Escape,
        Wander
    }
}