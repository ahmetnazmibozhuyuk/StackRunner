using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using StackRunner.Managers;

namespace StackRunner.Interactable
{
    public class Gatherer : MonoBehaviour
    {
        [SerializeField] private float objectPositionDelay;
        [SerializeField] private float distanceBetweenObjects = 0.2f;

        private List<GameObject> _collectedObject = new();

        private int _counter;

        private float _upperCounter = 1;

        private void Start()
        {

            AddToGatherer(gameObject);
        }

        public void AddToGatherer(GameObject objectToAdd)
        {
            if (_collectedObject.Contains(objectToAdd)) return;
            _collectedObject.Add(objectToAdd);
        }
        public void RemoveLastMember()
        {
            _collectedObject.RemoveAt(_collectedObject.Count - 1);
        }
        private void FixedUpdate()
        {
            if(GameManager.instance.CurrentState == GameState.GameStarted)
            {
                SetGatheredPosition();
            }
        }
        public void WinningScreen()
        {
            _counter = _collectedObject.Count;
            GameManager.instance.Player.GetComponent<Rigidbody>().isKinematic = true;
            MoveBlock();

        }
        private void MoveBlock()
        {
            _counter--;
            if(_counter <= 0)
            {
                _collectedObject[_counter].transform.DORotateQuaternion(Quaternion.identity, 1f);
                _collectedObject[_counter].transform.DOMove(GameManager.instance.GoalPoint.transform.position+Vector3.up * _upperCounter, 1f);
                GameManager.instance.ChangeState(GameState.GameWon);
                return;
            }
            _collectedObject[_counter].transform.DORotateQuaternion(Quaternion.identity, 0.2f);
            _collectedObject[_counter].transform.DOMove(GameManager.instance.GoalPoint.transform.position + Vector3.up * _upperCounter, 0.2f).OnComplete(MoveBlock);
            _upperCounter += 2.3f;
        }

        private void SetGatheredPosition()
        {
            for (int i = 1; i < _collectedObject.Count; i++)
            {
                if (_collectedObject[i] == null)
                {
                    _collectedObject.RemoveAt(i);
                    i--;
                    continue;
                }

                // todo x y ve z kısımlarını ayır, y değeri lerp ile değil doğrudan takip etsin.
                _collectedObject[i].transform.SetPositionAndRotation(
SetPosition(_collectedObject[i].transform.position, _collectedObject[i - 1].transform.position),
Quaternion.Slerp(_collectedObject[i].transform.rotation, _collectedObject[i - 1].transform.rotation, objectPositionDelay));
            }

        }
        private Vector3 SetPosition(Vector3 currentPosition, Vector3 positionToMove)
        {
            return Vector3.Slerp(currentPosition + Vector3.forward * distanceBetweenObjects, positionToMove, objectPositionDelay);
        }


    }

}
