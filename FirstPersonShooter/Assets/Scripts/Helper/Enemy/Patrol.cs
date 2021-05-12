using UnityEngine;
using UnityEngine.AI;

namespace ExampleTemplate
{
    public static class Patrol
    {
        public static Vector3 GenericPoint(Vector3 position)
        {
            Vector3 result;

            var dis = Random.Range(5, 50);
            var randomPoint = Random.insideUnitSphere * dis;

            NavMesh.SamplePosition(position + randomPoint, out var hit, dis, NavMesh.AllAreas);
            result = hit.position;

            return result;
        }
    }
}