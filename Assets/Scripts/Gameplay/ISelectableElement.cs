namespace Gameplay
{
    public enum ElementType
    {
        Cube = 0,
        Sphere = 1
    }
    
    public interface ISelectableElement : ISelectionResponse
    {
        public ElementType ElementType
        {
            get;
            set;
        }
    }
}