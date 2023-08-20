namespace Application.ProjectContext.Models
{
    public class LeaderboardRecord
    {
        public int Position;
        public string Id;
        public int Score;

        public LeaderboardRecord(int position, string id, int score)
        {
            Position = position;
            Id = id;
            Score = score;
        }
    }
}