using UnityEngine;

public class EnemyStatesManager
{
    public EnemyState currentState; 

    public void ChangeState(EnemyState newState)
    {
        if (currentState != null)
        {
            currentState.ExitState(); 
        }

        currentState = newState; 
        currentState.EnterState();
    }

    public void UpdateState()
    {
        if (currentState != null)
        {
            currentState.UpdateState(); 
        }
    }
}
