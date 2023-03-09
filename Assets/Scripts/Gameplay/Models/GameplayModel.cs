using Utils;

namespace Gameplay.Models
{
    public class GameplayModel : Singleton<GameplayModel>
    {
        public enum GameStates
        {
            MainMenu = 0,
            Init = 1,
            Gameplay = 2,
        }
    }
}