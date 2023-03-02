namespace EnemySystem
{
    public class EnemyRed : Enemy
    {
        protected override void Awake()
        {
            base.Awake();
            
            NavMeshAgent.SetDestination(RouteList[Random.Next(0, RouteList.Count)].position);

            StartCoroutine(UpdateRoute());
        }
    }
}
