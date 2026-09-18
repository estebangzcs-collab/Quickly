using System.Collections.Generic;

namespace SpaceShootingRace.Core
{
    public class LevelDef
    {
        public string Id;
        public string Name;
        public int ScoreStar2Threshold;
        public string ScenePath;
    }

    public class PlanetDef
    {
        public string Id;
        public string Name;
        public List<LevelDef> Levels = new();
    }

    public static class LevelCatalog
    {
        public static readonly List<PlanetDef> Planets = new()
        {
            new PlanetDef
            {
                Id = "mars",
                Name = "Marte",
                Levels =
                {
                    new LevelDef { Id = "mars-1", Name = "Cráteres Rojos", ScoreStar2Threshold = 600, ScenePath = "res://scenes/levels/mars_1.tscn" },
                    new LevelDef { Id = "mars-2", Name = "Cañón del Terror", ScoreStar2Threshold = 900, ScenePath = "res://scenes/levels/mars_2.tscn" },
                    new LevelDef { Id = "mars-3", Name = "Torre de Polvo", ScoreStar2Threshold = 1200, ScenePath = "res://scenes/levels/mars_3.tscn" }
                }
            },
            new PlanetDef
            {
                Id = "jupiter",
                Name = "Júpiter",
                Levels =
                {
                    new LevelDef { Id = "jupiter-1", Name = "Anillos de Tormenta", ScoreStar2Threshold = 900, ScenePath = "res://scenes/levels/jupiter_1.tscn" },
                    new LevelDef { Id = "jupiter-2", Name = "Cinturón de Asteroides", ScoreStar2Threshold = 1200, ScenePath = "res://scenes/levels/jupiter_2.tscn" },
                    new LevelDef { Id = "jupiter-3", Name = "La Gran Mancha", ScoreStar2Threshold = 1600, ScenePath = "res://scenes/levels/jupiter_3.tscn" }
                }
            }
        };

        public static LevelDef Find(string id)
        {
            foreach (var planet in Planets)
            {
                foreach (var level in planet.Levels)
                {
                    if (level.Id == id) return level;
                }
            }
            return null;
        }
    }
}