using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.EditorTools;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("---- HEALTH ----")]
    [SerializeField] protected float maxHealth; //最大HP
    [SerializeField] protected float health;    //現在HP

    [SerializeField] GameObject deathVFX;


    protected virtual void OnEnable()
    {
        health = maxHealth;
    }

    /// <summary>
    /// ダメージ処理
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
    /// 死亡処理
    /// </summary>
    public virtual void Die()
    {
        //UI上でHPを０させるため
        health = 0f;
        PoolManager.Release(deathVFX, transform.position);
        gameObject.SetActive(false);
    }
}
