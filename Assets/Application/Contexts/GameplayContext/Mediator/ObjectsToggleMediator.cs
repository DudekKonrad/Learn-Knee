using UnityEngine;

namespace Application.GameplayContext.Mediator
{
    public class ObjectsToggleMediator : MonoBehaviour
    {
        [SerializeField] private GameObject[] _objects;

        public void OnToggleChoose()
        {
            foreach (var o in _objects)
            {
                o.SetActive(!o.activeSelf);
            }
        }
    }
}
