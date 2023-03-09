namespace Gameplay
{
    public interface ISelectionResponse
    {
        public bool IsSelected { get; set; }
        public bool IsViewed { get; set; }
        public void OnSelect();
        public void OnDeselect();
        public void OnClick();
    }
}