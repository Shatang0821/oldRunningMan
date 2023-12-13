using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundDetector : MonoBehaviour
{
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float detectionRadius = 0.1f;
    Collider2D[] collider2Ds = new Collider2D[1];
    
    public bool isOn => Physics2D.OverlapCircleNonAlloc(transform.position, detectionRadius, collider2Ds, groundLayer) != 0;
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

    }
}
