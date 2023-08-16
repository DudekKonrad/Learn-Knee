
using UnityEngine;

namespace Application.Utils
{
    public class ReadExcel : MonoBehaviour
    {
        [SerializeField] private TextAsset _textAsset;

        private void ReadTranslations() {
            var dataLines = _textAsset.text.Split('\n');
 
            foreach (var line in dataLines)
            {
                Debug.Log($"Line: {line}");
                var data = line.Split(';');
                foreach (var x in data)
                {
                    Debug.Log($"{x}");
                }
            }
        }
    }
}