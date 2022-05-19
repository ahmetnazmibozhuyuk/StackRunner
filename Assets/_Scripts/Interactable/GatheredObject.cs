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
                Destroy(gameObject);
            }
            if (other.gameObject.CompareTag("Goal"))
            {
                GameManager.instance.ChangeState(GameState.GameCheckingResults);
            }
            //if (other.gameObject.CompareTag("Elevator"))
            //{
            //    Destroy(GetComponent<Collider>());
            //    GameManager.instance.Gatherer.RemoveLastMember();
            //    StartCoroutine(Co_MovePosition(0.2f));

            //}
        }
        //private IEnumerator Co_MovePosition(float duration)
        //{
            
        //    while (true)
        //    {
        //        duration -= 0.1f;
        //        if (duration <= 0) break;
        //        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.identity, 0.1f);

        //        GameManager.instance.Ground.transform.position += 100 * Time.deltaTime * Vector3.up;
        //        yield return null;
        //    }
        //}
    }
}
