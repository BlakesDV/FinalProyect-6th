using UnityEngine;

/// <summary>
/// Represents an aggressive agent with perception and decision-making capabilities.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class AgressiveAgent : BasicAgent {

    [SerializeField] float eyesPerceptRadious, earsPerceptRadious;
    [SerializeField] Transform eyesPercept, earsPercept;
    [SerializeField] Animator animator;
    [SerializeField] AgressiveAgentStates agentState;
    Rigidbody rb;
    Collider[] perceibed, perceibed2;
    string currentAnimationStateName;


    void Start () {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        agentState = AgressiveAgentStates.None;
        currentAnimationStateName = "";
    }

    void Update () {
        perceptionManager();
        decisionManager();
    }

    private void FixedUpdate () {
        perceibed = Physics.OverlapSphere(eyesPercept.position, eyesPerceptRadious);
        perceibed2 = Physics.OverlapSphere(earsPercept.position, earsPerceptRadious);
    }

    /// <summary>
    /// Manages perception by detecting nearby enemies.
    /// </summary>
    void perceptionManager () {
        target = null;
        if (perceibed != null) {
            foreach (Collider tmp in perceibed) {
                if (tmp.CompareTag("Enemy")) {
                    target = tmp.transform;
                }
            }
        }
        if (perceibed2 != null) {
            foreach (Collider tmp in perceibed2) {
                if (tmp.CompareTag("Enemy")) {
                    target = tmp.transform;
                }
            }
        }
    }

    /// <summary>
    /// Manages decision-making based on the agent's perception.
    /// </summary>
    void decisionManager () {
        AgressiveAgentStates newState;
        if (target == null) {
            newState = AgressiveAgentStates.Wander;
        } else if (target.GetComponent<Rigidbody>().mass < rb.mass) {
            newState = AgressiveAgentStates.Pursuit;
            if (Vector3.Distance(transform.position, target.position) < stopThreshold) {
                newState = AgressiveAgentStates.Attack;
            }
        } else {
            newState = AgressiveAgentStates.Escape;
        }
        changeAgentState(newState);
        actionManager();
        movementManager();
    }

    /// <summary>
    /// Changes the state of the agent only if its a new state
    /// </summary>
    /// <param name="t_newState">The new state of the agent.</param>
    void changeAgentState (AgressiveAgentStates t_newState) {
        if (agentState == t_newState) {
            return;
        }
        agentState = t_newState;
        if (agentState != AgressiveAgentStates.Wander) {
            wanderNextPosition = null;
        }
    }

    /// <summary>
    /// Manages actions based on the current state of the agent.
    /// </summary>
    void actionManager () {
        switch (agentState) {
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

    /// <summary>
    /// Manages movement based on the current state of the agent.
    /// </summary>
    void movementManager () {
        switch (agentState) {
            case AgressiveAgentStates.None:
                rb.velocity = Vector3.zero;
                break;
            case AgressiveAgentStates.Pursuit:
                pursuiting();
                break;
            case AgressiveAgentStates.Attack:
                attacking();
                break;
            case AgressiveAgentStates.Escape:
                escaping();
                break;
            case AgressiveAgentStates.Wander:
                wandering();
                break;
        }
    }

    /// <summary>
    /// Moves the agent randomly within the environment.
    /// </summary>
    private void wandering () {
        if (!currentAnimationStateName.Equals("walksent")) {
            Debug.Log(currentAnimationStateName);
            animator.Play("walksent", 0);
            currentAnimationStateName = "walksent";
        }
        if (( wanderNextPosition == null ) ||
            ( Vector3.Distance(transform.position, wanderNextPosition.Value) < 0.5f )) {
            wanderNextPosition = SteeringBehaviours.wanderNextPos(this);
        }
        rb.velocity = SteeringBehaviours.seek(this, wanderNextPosition.Value);
    }

    /// <summary>
    /// Handles pursuing the target.
    /// </summary>
    private void pursuiting () {
        if (!currentAnimationStateName.Equals("run") && !currentAnimationStateName.Equals("walk")) {
            animator.Play("run", 0);
            currentAnimationStateName = "run";
        }
        maxVel *= 2;
        rb.velocity = SteeringBehaviours.seek(this, target.position);
        rb.velocity = SteeringBehaviours.arrival(this, target.position, slowingRadius, stopThreshold);
        if (Vector3.Distance(transform.position, target.position) <= slowingRadius) {
            if (!currentAnimationStateName.Equals("walk")) {
                animator.Play("walk", 0);
                currentAnimationStateName = "walk";
            }
        }
        maxVel /= 2;
    }

    /// <summary>
    /// Handles attacking the target.
    /// </summary>
    private void attacking () {
        if (!currentAnimationStateName.Equals("attack")) {
            animator.Play("attack", 0);
            currentAnimationStateName = "attack";
        }
    }

    /// <summary>
    /// Handles escaping from the target.
    /// </summary>
    private void escaping () {
        if (!currentAnimationStateName.Equals("run")) {
            animator.Play("run", 0);
            currentAnimationStateName = "run";
        }
        rb.velocity = SteeringBehaviours.flee(this, target.position);
    }

    /// <summary>
    /// Displays perception spheres in the scene view.
    /// </summary>
    private void OnDrawGizmos () {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(eyesPercept.position, eyesPerceptRadious);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(earsPercept.position, earsPerceptRadious);
    }

    /// <summary>
    /// Enumeration of possible agent states.
    /// </summary>
    private enum AgressiveAgentStates {
        None,
        Pursuit,
        Attack,
        Escape,
        Wander
    }
}