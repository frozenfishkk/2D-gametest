using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyStateMachine
{
    public enemyState currentState { get; private set; }
    
    public void Initialize(enemyState state)
    {
        currentState = state;
        currentState.Enter();
    }

    public void ChangeState (enemyState newState)
    {   
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }

}
