using EnemySystem.State;

namespace Interface
{
    public interface IDamage
    {
        public AEnemyState GetState();
        public void GetDamage();
    }
}