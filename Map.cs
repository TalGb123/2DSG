using System;
using System.Collections.Generic;
using System.IO;

namespace _2DSG
{
    internal class Map
    {
        public string Name { get; set; }
        public string BackgroundImage { get; set; }
        public List<PlatformData> Platforms { get; set; } = new List<PlatformData>();
        public List<PlayerStart> PlayerStarts { get; set; } = new List<PlayerStart>();

        public class PlatformData
        {
            public double x, y, w, h;
            public PlatformData(double x, double y, double w, double h)
            {
                this.x = x; this.y = y; this.w = w; this.h = h;
            }
        }

        public class PlayerStart
        {
            public double X;
            public double Y;
        }

        public static List<Map> LoadAll(string path)
        {
            var maps = new List<Map>();
            Map current = null;
            foreach (var line in File.ReadLines(path))
            {
                var trimmed = line.Trim();
                if (string.IsNullOrEmpty(trimmed) || trimmed.StartsWith("#"))
                    continue;

                var parts = trimmed.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length == 2)
                {
                    if (current != null && current.Platforms.Count > 0)
                        maps.Add(current);
                    current = new Map { Name = parts[0], BackgroundImage = parts[1], Platforms = new List<PlatformData>(), PlayerStarts = new List<PlayerStart>() };
                }
                else if ((parts.Length == 5 && parts[0] == "!") && current != null)
                {
                    double x = double.Parse(parts[1]);
                    double y = double.Parse(parts[2]);
                    double w = double.Parse(parts[3]);
                    double h = double.Parse(parts[4]);
                    current.Platforms.Add(new PlatformData(x, y, w, h));
                }
                else if ((parts.Length == 3 && parts[0] == "?") && current != null)
                {
                    double x = double.Parse(parts[1]);
                    double y = double.Parse(parts[2]);
                    current.PlayerStarts.Add(new PlayerStart { X = x, Y = y });
                }
            }
            if (current != null && current.Platforms.Count > 0)
                maps.Add(current);
            return maps;
        }

        public List<Platform> CreatePlatforms(int formWidth, int formHeight)
        {
            var plats = new List<Platform>();
            foreach (var p in Platforms)
            {
                int x = (int)(p.x * formWidth);
                int y = (int)(p.y * formHeight);
                int w = (int)(p.w * formWidth);
                int h = (int)(p.h * formHeight);
                plats.Add(new Platform(x, y, w, h));
            }
            return plats;
        }
    }
}
