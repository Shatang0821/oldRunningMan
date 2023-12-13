using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCheck : ItemsGet
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        GameManager.GameState = GameState.Scoring;
    }
}
