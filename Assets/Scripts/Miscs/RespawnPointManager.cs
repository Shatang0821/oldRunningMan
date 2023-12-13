using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPointManager : Singleton<RespawnPointManager>
{
    // ���񉻗�\�C�p����Inspector��?�u
    [SerializeField] private List<RespawnPointData> respawnPointDataList;

    public Dictionary<string, Transform> respawnPoints;

    public string currentRespawnPoint;

    protected override void Awake()
    {
        base.Awake();
        InitializeRespawnPoints();
    }

    private void InitializeRespawnPoints()
    {
        respawnPoints = new Dictionary<string, Transform>();

        foreach (var data in respawnPointDataList)
        {
            if (data.pointTransform != null)
            {
                respawnPoints[data.point] = data.pointTransform;
            }
        }
    }

    public Transform GetRespawnPoint(string key)
    {
        if (respawnPoints.ContainsKey(key))
        {
            return respawnPoints[key];
        }
        else
        {
            Debug.LogError("Respawn point not found: " + key);
            return null;
        }
    }

    public void UpdateRespawnPoint(string key)
    {
        if (respawnPoints.ContainsKey(key))
        {
            currentRespawnPoint = key;
        }
    }
}

[System.Serializable]
public class RespawnPointData
{
    public string point;
    public Transform pointTransform;
}
