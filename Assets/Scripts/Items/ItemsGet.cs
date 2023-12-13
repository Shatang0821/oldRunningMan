using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ItemsGet : MonoBehaviour
{
    [SerializeField] AudioData collectSFX;
    [SerializeField] GameObject collectVFX;

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            this.gameObject.SetActive(false);
            AudioManager.Instance.PlaySFX(collectSFX);
            PoolManager.Release(collectVFX, transform.position);
        }
    }

}
