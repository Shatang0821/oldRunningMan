using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    [SerializeField] GameObject player;

    private void OnEnable()
    {
        GameManager.onRespown += RespawnPlayer;
    }

    private void OnDisable()
    {
        GameManager.onRespown -= RespawnPlayer;
    }

    public void RespawnPlayer()
    {
        if (player != null)
        {
            player.transform.position = RespawnPointManager.Instance.GetRespawnPoint(RespawnPointManager.Instance.currentRespawnPoint).position;
            player.SetActive(true);
        }
        GameManager.GameState = GameState.Playing;
    }
}
