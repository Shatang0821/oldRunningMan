using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    [SerializeField] PlayerState[] states;//ステータス配列

    Animator animator;

    Player player;

    PlayerInput input;

    void Awake()
    {
        animator = GetComponent<Animator>();

        player = GetComponent<Player>();

        input = GetComponent<PlayerInput>();

        stateTable = new Dictionary<System.Type, IState>(states.Length);

        foreach(PlayerState state in states)
        {
            state.Initialize(animator,player,input,this);
            stateTable.Add(state.GetType(), state);
        }
    }

    void Start()
    {
        if(animator == null)
        {
            Debug.Log("animator NONE");
        }
        SwitchOn(stateTable[typeof(PlayerState_Idle)]);
    }
}
