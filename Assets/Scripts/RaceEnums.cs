namespace SpaceShootingRace.Core
{
    public enum RaceState
    {
        Waiting,
        Racing,
        Finished,
        GameOver
    }

    public struct FinishResult
    {
        public string LevelId;
        public int Rank;
        public int Score;
        public float TimeSeconds;
        public bool Finished;
        public int Stars;
        public bool FirstPlace;
        public bool NoLivesLost;
    }
}