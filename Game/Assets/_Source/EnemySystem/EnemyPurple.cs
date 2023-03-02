namespace EnemySystem
{
    public class EnemyPurple : Enemy
    {
        protected override void Awake()
        {
            base.Awake();
            
            NavMeshAgent.SetDestination(RouteList[Random.Next(0, RouteList.Count)].position);

            StartCoroutine(UpdateRoute());
        }
    }
}
