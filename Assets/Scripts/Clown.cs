using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clown : MonoBehaviour // add an audiosource to the clown!
{
    public Animator Animator;
    // Start is called before the first frame update
    void Start()
    {
        Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Animator.SetBool("IsChasing", true);
        }
    }
}
