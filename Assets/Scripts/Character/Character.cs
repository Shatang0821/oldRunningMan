using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.EditorTools;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("---- HEALTH ----")]
    [SerializeField] protected float maxHealth; //�ő�HP
    [SerializeField] protected float health;    //����HP

    [SerializeField] GameObject deathVFX;


    protected virtual void OnEnable()
    {
        health = maxHealth;
    }

    /// <summary>
    /// �_���[�W����
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
    /// ���S����
    /// </summary>
    public virtual void Die()
    {
        //UI���HP���O�����邽��
        health = 0f;
        PoolManager.Release(deathVFX, transform.position);
        gameObject.SetActive(false);
    }
}
