using UnityEngine;

namespace Application.Utils
{
    public class Rotate : MonoBehaviour
    {
        [SerializeField] private float _speed = 90f;

        private void Update()
        {
            transform.Rotate(0, 0, _speed * Time.deltaTime);
        }
    }
}