using System;
using UnityEngine;

public enum PackageColor
{
    Red, Green, Blue,
    Yellow
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
        }

        throw new ArgumentException("Invalid Color");
    }
}