using System;
using UnityEngine;

[Serializable]
public enum Hotkey
{
    Auto, Hotkey1, Hotkey2, Hotkey3, Hotkey4, Hotkey5, Hotkey6, Hotkey7, Hotkey8, Hotkey9
}

public static class HotkeyExtensions
{
    public static bool WasPressedThisFrame(this Hotkey hotkey)
    {
        return Input.GetButtonDown(hotkey.ToString());
    }

    public static bool IsPressed(this Hotkey hotkey)
    {
        return Input.GetButton(hotkey.ToString());
    }

    public static bool WasReleasedThisFrame(this Hotkey hotkey)
    {
        return Input.GetButtonUp(hotkey.ToString());
    }
}