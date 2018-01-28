using System;
using UnityEngine;

public enum PackageColor
{
    Red, Green, Blue,
    Yellow, Pink, Orange
}

public static class PackageColorExtensions
{
    public static Color ToColor(this PackageColor color)
    {
        switch (color)
        {
            case PackageColor.Blue:
                return Color.blue;
            case PackageColor.Red:
                return Color.red;
            case PackageColor.Green:
                return Color.green;
            case PackageColor.Yellow:
                return Color.yellow;
            case PackageColor.Pink:
                return new Color(1f, 20/255f, 147/255f);
            case PackageColor.Orange:
                return new Color(1f, 140/255f, 0);
        }

        throw new ArgumentException("Invalid Color");
    }
}