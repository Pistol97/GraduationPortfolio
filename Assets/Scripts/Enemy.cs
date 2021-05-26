﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    Rigidbody rigid;
    NavMeshAgent nav;

    [SerializeField]
    private Transform target;
    private Animator animator;
    public bool isChase = false;

    private AudioSource audioSource;
    [SerializeField] private AudioClip[] idle;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        nav = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        ChaseDistance();
        ChangeIdle();
        //if(isChase)
        //{
        //    nav.SetDestination(target.position);
        //}
    }

    private void FixedUpdate()
    {
        FreezeVelocity();
    }

    void ChaseDistance()
    {
        if(Vector3.Distance(target.position,gameObject.transform.position)<=10)
        {
            animator.SetBool("IsWalk", true);
            nav.SetDestination(target.position);
            isChase = true;
            if (Vector3.Distance(target.position, gameObject.transform.position) <= 3)
            {
                animator.SetBool("IsAttack", true);
            }
            else
            {
                animator.SetBool("IsAttack", false);
            }
        }
        else
        {
            animator.SetBool("IsWalk", false);
            isChase = false;
        }
    }

    void FreezeVelocity()
    {
        if(isChase)
        {
            rigid.velocity = Vector3.zero;
            rigid.angularVelocity = Vector3.zero;
        }
    }

    private void ChangeIdle()
    {
        if(!audioSource.isPlaying)
        {
            audioSource.clip = idle[Random.Range(0, idle.Length - 1)];
            audioSource.Play();
        }

        else
        {
            return;
        }
    }
}
