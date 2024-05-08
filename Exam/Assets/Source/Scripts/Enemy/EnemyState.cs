using UnityEngine;

public abstract class EnemyState
{
    public EnemyAI AI; 

    public virtual void EnterState() { }

    public virtual void UpdateState() { }

    public virtual void ExitState() { }
}
