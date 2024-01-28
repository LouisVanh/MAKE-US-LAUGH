using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Clown : MonoBehaviour // add an audiosource to the clown!
{
    public Animator Animator;
    private AudioSource _audioSource;
    private NavMeshAgent _agent;
    void Start()
    {
        Animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Animator.SetBool("IsChasing", true);
        }
    }
}
