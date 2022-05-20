using UnityEngine;
using StackRunner.Managers;

namespace StackRunner.Interactable
{
    public class GatheredObject : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Collectable"))
            {
                GameManager.instance.Gatherer.AddToGatherer(other.gameObject);
            }
            if (other.gameObject.CompareTag("Obstacle"))
            {
                PoolManager.instance.SpawnParticle(transform.position);
                GameManager.instance.Gatherer.DestroyLastMember();
            }
            if (other.gameObject.CompareTag("Goal"))
            {
                GameManager.instance.ChangeState(GameState.GameCheckingResults);
            }
        }
    }
}
