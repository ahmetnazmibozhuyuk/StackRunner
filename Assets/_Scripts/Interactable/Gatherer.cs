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

        private float _upperCounter = 2.5f;

        private float _redColor = 1f;
        private float _grayColor = 1f;
        private float _blueColor = 0.2f;

        private readonly float _colorChange = 0.05f;
        private void Start()
        {

            AddToGatherer(gameObject);
        }

        public void AddToGatherer(GameObject objectToAdd)
        {
            if (_collectedObject.Contains(objectToAdd)) return;

            objectToAdd.GetComponent<Renderer>().material.color = new Color(_redColor, _grayColor, _blueColor);
            AddObjectColor();

            _collectedObject.Add(objectToAdd);
        }
        private void AddObjectColor()
        {
            if (_redColor > 0)
            {
                _redColor -= _colorChange;
            }
            else if (_grayColor > 0)
            {
                _grayColor -= _colorChange;
            }
            else
            {
                _blueColor -= _colorChange;
            }
        }
        private void SubtractObjectColor()
        {
            if (_blueColor < 0.2f)
            {
                _blueColor += _colorChange;
            }
            else if (_grayColor < 1)
            {
                _grayColor += _colorChange;
            }
            else
            {
                _redColor += _colorChange;
            }
        }
        public void RemoveLastMember()
        {
            _collectedObject.RemoveAt(_collectedObject.Count - 1);
            SubtractObjectColor();
        }
        public void DestroyLastMember()
        {
            Destroy(_collectedObject[_collectedObject.Count - 1]);
            _collectedObject.RemoveAt(_collectedObject.Count - 1);
            SubtractObjectColor();
        }
        private void FixedUpdate()
        {
            if (GameManager.instance.CurrentState == GameState.GameStarted)
            {
                SetGatheredPosition();
            }
        }
        public void WinningScreen()
        {
            _counter = _collectedObject.Count;
            GameManager.instance.Player.GetComponent<Rigidbody>().isKinematic = true;
            //MoveBlock();
            MoveBlocksFinal();

        }
        //private void MoveBlock()
        //{
        //    _counter--;
        //    if (_counter <= 0)
        //    {
        //        _collectedObject[_counter].transform.DORotateQuaternion(Quaternion.identity, 1f);
        //        _collectedObject[_counter].transform.DOMove(GameManager.instance.GoalPoint.transform.position + Vector3.up * _upperCounter, 1f);
        //        GameManager.instance.ChangeState(GameState.GameWon);
        //        return;
        //    }
        //    _collectedObject[_counter].transform.DORotateQuaternion(Quaternion.identity, 0.2f);
        //    _collectedObject[_counter].transform.DOMove(GameManager.instance.GoalPoint.transform.position + Vector3.up * _upperCounter, 0.2f).OnComplete(MoveBlock);
        //    _upperCounter += 2.7f;
        //}
        private void MoveBlocksFinal()
        {
            // Tüm loopu iki adımda yerleştir her biri önce mesafeyi gelsin sonra ileri gitsin. ilk taş ikinci hareketi özellikle yavaş yapsın.
            for (int i = _collectedObject.Count - 1; i >= 0; i--)
            {
                if (i == 0)
                {
                    _collectedObject[0].transform.DORotateQuaternion(Quaternion.identity, 0.2f);
                    _collectedObject[0].transform.DOMove(GameManager.instance.GoalPoint.transform.position + Vector3.up * _upperCounter, 0.2f + _upperCounter * 0.1f).
                        SetEase(Ease.OutCubic).
                        OnComplete(GameWon);

                    return;
                }
                _collectedObject[i].transform.DORotateQuaternion(Quaternion.identity, 0.2f);
                _collectedObject[i].transform.DOMove(GameManager.instance.GoalPoint.transform.position+Vector3.forward*0.8f + Vector3.up * _upperCounter, 0.2f + _upperCounter * 0.1f).SetEase(Ease.OutCubic);
                _upperCounter += 2.357f;
            }
        }
        private void GameWon()
        {
            _collectedObject[0].transform.DOMove(_collectedObject[0].transform.position + Vector3.forward * 0.8f, 0.2f).OnComplete(
                        () => GameManager.instance.ChangeState(GameState.GameWon));
        }
        private void SetGatheredPosition()
        {
            for (int i = 1; i < _collectedObject.Count; i++)
            {
                if (_collectedObject[i] == null)
                {
                    _collectedObject.RemoveAt(i);
                    _grayColor -= 0.05f;
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
