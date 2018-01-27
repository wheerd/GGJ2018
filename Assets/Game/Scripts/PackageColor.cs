using System;
using UnityEngine;

public enum PackageColor
{
    Red, Green, Blue
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
        }

        throw new ArgumentException("Invalid Color");
    }
}