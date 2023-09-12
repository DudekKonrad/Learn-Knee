using UnityEngine;

namespace Application.Utils
{
    public interface ISelectionResponse
    {
        public bool IsSelected { get; set; }
        public bool IsViewed { get; set; }
        public void OnSelect();
        public void OnDeselect();
        public void OnChosen();
        public void Disappear();
        public void Appear();
        public GameObject GameObject { get; }
        public void SetDefaultColor();
    }
}