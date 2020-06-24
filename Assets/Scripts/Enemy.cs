﻿
using UnityEngine;

/// <summary>
/// Used to set a bot as an enemy player so it can be damaged
/// </summary>

[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Animator))]

public class Enemy : MonoBehaviour
{
    public float health = 100f;
    public Animator animator;
    public CapsuleCollider hitbox;    

    public void TakeDamage(float amount)
    {
        health -= amount;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        hitbox.enabled = false;
        animator.enabled = false;        
    }

}
