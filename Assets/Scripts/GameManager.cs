using System.Collections.Generic;
using Godot;
using SpaceShootingRace.Auth;
using SpaceShootingRace.Race;
using SpaceShootingRace.Save;
using SpaceShootingRace.Ships;
using SpaceShootingRace.Systems;

namespace SpaceShootingRace.Core
{
    public partial class GameManager : Node
    {
        public static GameManager Instance { get; private set; }

        public RaceState State { get; private set; } = RaceState.Waiting;
        public PlayerShip Player { get; private set; }
        public ScoringSystem Scoring { get; private set; }
        public RaceManager Race { get; private set; }
        public LevelDef CurrentLevel { get; private set; }
        public SaveData Save { get; private set; }
        public FinishResult LastResult { get; private set; }

        public bool IsGuestSession => AuthService.IsGuest;

        public override void _EnterTree()
        {
            Instance = this;
        }

        public override void _Ready()
        {
            Save = SaveSystem.Load() ?? new SaveData();
            Subscribe();
        }

        private void Subscribe()
        {
            EventBus.PlayerFinished += OnPlayerFinished;
            EventBus.PlayerDied += OnPlayerDied;
            EventBus.ShipDestroyed += OnShipDestroyed;
        }

        public void LoadLevel(string levelId)
        {
            var def = LevelCatalog.Find(levelId);
            if (def == null) return;
            CurrentLevel = def;
            State = RaceState.Waiting;
            EventBus.LevelSelected?.Invoke(levelId);
            if (!string.IsNullOrEmpty(def.ScenePath))
            {
                GetTree().ChangeSceneToFile(def.ScenePath);
            }
        }

        public void BeginRace(RaceManager race, PlayerShip player, ScoringSystem scoring)
        {
            Race = race;
            Player = player;
            Scoring = scoring;
            State = RaceState.Racing;
            EventBus.RaceStateChanged?.Invoke(State);
            EventBus.RaceStarted?.Invoke();
            if (Race != null) Race.StartRace();
        }

        private void OnShipDestroyed(ShipBase ship)
        {
            if (ship != Player) return;
            EventBus.PlayerDied?.Invoke();
        }

        private void OnPlayerFinished(FinishResult result)
        {
            result.LevelId = CurrentLevel?.Id ?? string.Empty;
            result.Score = Scoring?.Total ?? 0;
            result.FirstPlace = result.Rank == 1;
            result.NoLivesLost = Player != null && Player.Lives >= Player.MaxLives;

            int stars = 0;
            if (result.Finished) stars++;
            if (CurrentLevel != null && result.Score >= CurrentLevel.ScoreStar2Threshold) stars++;
            if (result.FirstPlace || result.NoLivesLost) stars++;
            result.Stars = Mathf.Clamp(stars, 0, 3);
            LastResult = result;

            ApplyPersistence(result);

            State = RaceState.Finished;
            EventBus.RaceStateChanged?.Invoke(State);
        }

        private void OnPlayerDied()
        {
            State = RaceState.GameOver;
            EventBus.RaceStateChanged?.Invoke(State);
        }

        private void ApplyPersistence(FinishResult result)
        {
            if (IsGuestSession || string.IsNullOrEmpty(result.LevelId)) return;

            if (!Save.Progress.TryGetValue(result.LevelId, out var progress))
            {
                progress = new LevelProgress();
                Save.Progress[result.LevelId] = progress;
            }

            if (result.Stars > progress.Stars) progress.Stars = result.Stars;
            if (result.Score > progress.BestScore) progress.BestScore = result.Score;
            if (progress.BestTime <= 0f || result.TimeSeconds > 0f && result.TimeSeconds < progress.BestTime)
            {
                progress.BestTime = result.TimeSeconds;
            }

            if (result.Stars >= 3 && TrophySystem.GrantTrophy(Save, result.LevelId))
            {
                if (!string.IsNullOrEmpty(AuthService.CurrentNickname))
                {
                    RankingSystem.AddPoints(Save, AuthService.CurrentNickname, TrophySystem.TrophyPointsValue);
                }
            }

            SaveSystem.Save(Save);
        }

        public Dictionary<string, LevelProgress> AllProgress() => Save?.Progress ?? new Dictionary<string, LevelProgress>();
    }
}

namespace SpaceShootingRace.Core
{
    public partial class GameManager : Node
    {
        public static GameManager Instance { get; private set; }

        public RaceState State { get; private set; } = RaceState.Waiting;
        public PlayerShip Player { get; private set; }
        public ScoringSystem Scoring { get; private set; }
        public RaceManager Race { get; private set; }
        public LevelDef CurrentLevel { get; private set; }
        public SaveData Save { get; private set; }
        public FinishResult LastResult { get; private set; }

        public bool IsGuestSession => AuthService.IsGuest;

        public override void _EnterTree()
        {
            Instance = this;
        }

        public override void _Ready()
        {
            Save = SaveSystem.Load() ?? new SaveData();
            Subscribe();
        }

        private void Subscribe()
        {
            EventBus.PlayerFinished += OnPlayerFinished;
            EventBus.PlayerDied += OnPlayerDied;
            EventBus.ShipDestroyed += OnShipDestroyed;
        }

        public void LoadLevel(string levelId)
        {
            var def = LevelCatalog.Find(levelId);
            if (def == null) return;
            CurrentLevel = def;
            State = RaceState.Waiting;
            EventBus.LevelSelected?.Invoke(levelId);
            if (!string.IsNullOrEmpty(def.ScenePath))
            {
                GetTree().ChangeSceneToFile(def.ScenePath);
            }
        }

        public void BeginRace(RaceManager race, PlayerShip player, ScoringSystem scoring)
        {
            Race = race;
            Player = player;
            Scoring = scoring;
            State = RaceState.Racing;
            EventBus.RaceStateChanged?.Invoke(State);
            EventBus.RaceStarted?.Invoke();
            if (Race != null) Race.StartRace();
        }

        private void OnShipDestroyed(ShipBase ship)
        {
            if (ship != Player) return;
            EventBus.PlayerDied?.Invoke();
        }

        private void OnPlayerFinished(FinishResult result)
        {
            result.LevelId = CurrentLevel?.Id ?? string.Empty;
            result.Score = Scoring?.Total ?? 0;
            result.FirstPlace = result.Rank == 1;
            result.NoLivesLost = Player != null && Player.Lives >= Player.MaxLives;

            int stars = 0;
            if (result.Finished) stars++;
            if (CurrentLevel != null && result.Score >= CurrentLevel.ScoreStar2Threshold) stars++;
            if (result.FirstPlace || result.NoLivesLost) stars++;
            result.Stars = Mathf.Clamp(stars, 0, 3);
            LastResult = result;

            ApplyPersistence(result);

            State = RaceState.Finished;
            EventBus.RaceStateChanged?.Invoke(State);
        }

        private void OnPlayerDied()
        {
            State = RaceState.GameOver;
            EventBus.RaceStateChanged?.Invoke(State);
        }

        private void ApplyPersistence(FinishResult result)
        {
            if (IsGuestSession || string.IsNullOrEmpty(result.LevelId)) return;

            if (!Save.Progress.TryGetValue(result.LevelId, out var progress))
            {
                progress = new LevelProgress();
                Save.Progress[result.LevelId] = progress;
            }

            if (result.Stars > progress.Stars) progress.Stars = result.Stars;
            if (result.Score > progress.BestScore) progress.BestScore = result.Score;
            if (progress.BestTime <= 0f || result.TimeSeconds > 0f && result.TimeSeconds < progress.BestTime)
            {
                progress.BestTime = result.TimeSeconds;
            }

            if (result.Stars >= 3 && TrophySystem.GrantTrophy(Save, result.LevelId))
            {
                if (!string.IsNullOrEmpty(AuthService.CurrentNickname))
                {
                    RankingSystem.AddPoints(Save, AuthService.CurrentNickname, TrophySystem.TrophyPointsValue);
                }
            }

            SaveSystem.Save(Save);
        }

        public Dictionary<string, LevelProgress> AllProgress() => Save?.Progress ?? new Dictionary<string, LevelProgress>();
    }
}