using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.EditorTools;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("---- HEALTH ----")]
    [SerializeField] protected float maxHealth; //Å‘åHP
    [SerializeField] protected float health;    //Œ»İHP

    [SerializeField] GameObject deathVFX;


    protected virtual void OnEnable()
    {
        health = maxHealth;
    }

    /// <summary>
    /// ƒ_ƒ[ƒWˆ—
    /// </summary>
    /// <param name="damage"></param>
    public virtual void TakenDamage(float damage)
    {
        health -= damage;

        if (health <= 0f)
        {
            Die();
        }
    }

    /// <summary>
    /// €–Sˆ—
    /// </summary>
    public virtual void Die()
    {
        //UIã‚ÅHP‚ğ‚O‚³‚¹‚é‚½‚ß
        health = 0f;
        PoolManager.Release(deathVFX, transform.position);
        gameObject.SetActive(false);
    }
}
