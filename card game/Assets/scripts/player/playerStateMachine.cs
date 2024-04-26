using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerStateMachine 
{
    public playerState currentState {  get; private set; }

    public void Initialized(playerState startState)
    {
        currentState = startState;
        currentState.Enter();
    }

    public void ChangeState(playerState newState)
    {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }
}
