namespace SpaceShootingRace.Core
{
    public static class Layers
    {
        public const uint Ship = 1u << 0;
        public const uint Obstacle = 1u << 1;
        public const uint World = 1u << 2;
        public const uint Pickup = 1u << 3;
        public const uint OilZone = 1u << 4;
        public const uint RaceTrigger = 1u << 5;
        public const uint Projectile = 1u << 6;
    }
}