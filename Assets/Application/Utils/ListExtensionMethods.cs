using System;
using System.Collections.Generic;

namespace Application.Utils
{
    public static class ListExtensionMethods
    {
        public static void Shuffle<T>(IList<T> list)
        {
            var n = list.Count;
            while (n > 1)
            {
                n--;
                var k = UnityEngine.Random.Range(0, n + 1);
                (list[k], list[n]) = (list[n], list[k]);
            }
        }
        
        public static T GetRandomElement<T>(this IList<T> list) => list[UnityEngine.Random.Range(0, list.Count)];
    }
}