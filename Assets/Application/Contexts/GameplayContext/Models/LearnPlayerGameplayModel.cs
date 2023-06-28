using JetBrains.Annotations;

namespace Application.GameplayContext.Models
{
    [UsedImplicitly]
    public class LearnPlayerGameplayModel
    {
        private readonly PlayerInputModel _playerInputModel = new PlayerInputModel();
        public PlayerInputModel PlayerInputModel => _playerInputModel;
    }
}