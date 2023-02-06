using UnityEngine;
public class RotateObject : MonoBehaviour
{
    [SerializeField] private GameObject _gameObject;
    [SerializeField] private float _rotationSpeed;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            var xRotation = Input.GetAxis("Mouse X") * _rotationSpeed;
            var yRotation = Input.GetAxis("Mouse Y") * _rotationSpeed;
        
            _gameObject.transform.Rotate(Vector3.down, xRotation, Space.World);
            _gameObject.transform.Rotate(Vector3.right, yRotation, Space.World);
        }
    } 
}
