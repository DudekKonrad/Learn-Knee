using UnityEngine;

namespace Application.Utils
{
    public static class TransformExtensionMethods
    {
        public static void RemoveAllChildren(this Transform transform)
        {
            foreach (Transform child in transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
    }
}