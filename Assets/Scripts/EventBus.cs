using System;
using SpaceShootingRace.Obstacles;
using SpaceShootingRace.Ships;
using SpaceShootingRace.Wildcards;

namespace SpaceShootingRace.Core
{
    public static class EventBus
    {
        public static event Action RaceStarted;
        public static event Action<RaceState> RaceStateChanged;
        public static event Action<ShipBase> ShipDestroyed;
        public static event Action<ShipBase, int, Node> ShipDamaged;
        public static event Action<ShipBase, int, int> LivesChanged;
        public static event Action<WildcardType> WildcardStored;
        public static event Action<WildcardType> WildcardActivated;
        public static event Action<WildcardType> WildcardEnded;
        public static event Action<int> ScoreChanged;
        public static event Action<ShipBase, int, int> RaceProgressUpdated;
        public static event Action<Obstacle> ObstacleDestroyed;
        public static event Action<ShipBase> RivalAffected;
        public static event Action<ShipBase> LapCompleted;
        public static event Action<FinishResult> PlayerFinished;
        public static event Action PlayerDied;
        public static event Action<string> LevelSelected;
    }
}