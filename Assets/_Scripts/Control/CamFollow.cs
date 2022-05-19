using UnityEngine;
using StackRunner.Managers;

namespace StackRunner.Control
{
    public class CamFollow : MonoBehaviour
    {
        private void Update()
        {
            if(GameManager.instance.CurrentState == GameState.GameStarted)
            {
                FollowPlayer();
            }
            if(GameManager.instance.CurrentState == GameState.GameCheckingResults)
            {
                MoveUpwards();
            }
        }
        private void FollowPlayer()
        {
            if (GameManager.instance.Player == null) return;
            transform.position = new Vector3(
                transform.position.x,
                transform.position.y,
                GameManager.instance.Player.transform.position.z
                );
        }
        private void MoveUpwards()
        {
            transform.position += Vector3.up * 0.05f;
        }
    }
}