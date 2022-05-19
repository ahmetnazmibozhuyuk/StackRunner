using UnityEngine;

namespace StackRunner
{
    public class Hole : MonoBehaviour
    {
        private void Start()
        {
            transform.rotation = Quaternion.Euler(0, 0, transform.position.x * -50);
        }
    }
}
