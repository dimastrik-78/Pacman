namespace EnemySystem.State
{
    public abstract class AEnemyState
    {
        protected EnemyStateMachine Owner;
        protected AEnemyState(EnemyStateMachine owner)
        {
            Owner = owner;
        }

        public virtual void Enter() {}
    }
}