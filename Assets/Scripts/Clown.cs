using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Clown : MonoBehaviour // add an audiosource to the clown!
{
    public Animator Animator;
    private NavMeshAgent _agent;
    private bool _enabled;
    private Transform _player;
    private PlayerBehaviour _playerBehaviour;

    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask whatIsPlayer;

    [Header("Settings")]
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _chaseSpeed, _patrolSpeed;
    [SerializeField] private float _patrolRange;
    [SerializeField] private int _itemDropRate;

    //States
    [SerializeField] private float sightRange, attackRange;
    [SerializeField] private float _ragDollTime = 10;
    [Header("Debug")]
    [SerializeField] private bool playerInSightRange;
    private bool playerNotWayOutOfSightRange;
    [SerializeField] private bool playerInAttackRange;
    //Patroling
    [SerializeField] private Vector3 walkPoint;
    [SerializeField] private bool walkPointSet;

    [Header("Sounds")]
    private AudioSource _audioSource;
    [SerializeField] private AudioClip[] _deathSounds;
    [SerializeField] private AudioClip[] _onSightSounds;

    public bool isDead;
    private Animator _animator;
    void Start()
    {
        Animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        //_skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        _player = GameObject.Find("Player").transform;
        _playerBehaviour = _player.GetComponent<PlayerBehaviour>();
        _agent = GetComponent<NavMeshAgent>();
        _enabled = false;
        _agent.enabled = false;
    }
    public void Chase()
    {
        Animator.SetBool("IsChasing", true);
        //transform.position -= new Vector3(0, 10, 0);
        _agent.enabled = true;
        _enabled = true;
    }
    void Update()
    {


        if (_agent.enabled)
        {
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
            playerNotWayOutOfSightRange = Physics.CheckSphere(transform.position, sightRange * 2, whatIsPlayer);

            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
            if (!playerInSightRange && !playerInAttackRange && playerNotWayOutOfSightRange) Patroling();
            if (playerInSightRange && !playerInAttackRange) ChasePlayer();
            if (playerInAttackRange) AttackPlayer();
        }
    }


    public void OnDeath()
    {


    }


    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
        {
            _agent.speed = _patrolSpeed;
            _agent.SetDestination(walkPoint);
        }
        Vector2 distanceToWalkPoint = new(transform.position.x - walkPoint.x, transform.position.z - walkPoint.z);

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1.5f)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = UnityEngine.Random.Range(-_patrolRange, _patrolRange);
        float randomX = UnityEngine.Random.Range(-_patrolRange, _patrolRange);

        NavMeshHit hit;
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        for (int i = 0; i < 10; i++)
        {
            if (!NavMesh.SamplePosition(walkPoint, out hit, 2f, NavMesh.AllAreas))
            {
                randomX = UnityEngine.Random.Range(-_patrolRange, _patrolRange);
                randomZ = UnityEngine.Random.Range(-_patrolRange, _patrolRange);
                walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
                continue;
            }
            NavMeshPath navpath = new NavMeshPath();
            if (NavMesh.CalculatePath(transform.position, hit.position, NavMesh.AllAreas, navpath)) // if theres a path
            {
                if (navpath.status == NavMeshPathStatus.PathPartial || navpath.status == NavMeshPathStatus.PathInvalid) //if its fucked
                    continue; // redo
            }

            walkPoint = hit.position;
            break;
        }  // find a position on the navmesh

        if (Physics.Raycast(walkPoint + new Vector3(0, 0.1f, 0), -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        _agent.speed = _chaseSpeed;
        _agent.SetDestination(_player.position);
        _agent.isStopped = false;
    }

    private void AttackPlayer()
    {
        //_animator.SetBool("Atack", true);
        DamagePlayer();
        _agent.velocity = Vector3.zero;
    }

    public void DamagePlayer()
    {
        if (playerInAttackRange)
        {
            _playerBehaviour.Kill();
        }
    }
}

