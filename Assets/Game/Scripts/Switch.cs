using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Game.Scripts;
using UnityEngine;
using UnityEngine.UI;

public enum InputMode
{
    Single,
    Hold,
    OneSecond,
    ButtonMash
}

public class Switch : MonoBehaviourWithCursor
{
    private const float dampening = 0.3f;
    private const float threshold = 1.0f;

    private SwitchExit currentExit;

    private float currentLevel;

    public SwitchExit DefaultExit;

    public string Hotkey = "Hotkey1";

    public InputMode InputMode;

    public float OutputSpeed = 5.0f;

    public Slider slider;
    public SwitchType SwitchType;

    public SwitchBottom Bottom;

    private void Start()
    {
        slider.gameObject.SetActive(false);
        GetComponentInChildren<Text>().text = Hotkey.Substring(Hotkey.Length - 1);
        currentExit = DefaultExit;
        UpdateRotation();
    }

    private void Update()
    {
        if (Input.GetButtonDown(Hotkey))
            switch (InputMode)
            {
                case InputMode.ButtonMash:
                    slider.gameObject.SetActive(true);
                    currentLevel += dampening;
                    slider.normalizedValue = Math.Max(currentLevel / threshold, 0);
                    if (currentLevel >= threshold)
                    {
                        SetSwitchExit(currentExit.NextValid(SwitchType));
                        currentLevel -= threshold;
                    }

                    currentLevel -= Time.deltaTime;
                    break;
                case InputMode.OneSecond:
                    slider.gameObject.SetActive(true);
                    currentLevel = threshold;
                    slider.normalizedValue = 1f;
                    SetSwitchExit(DefaultExit.NextValid(SwitchType));
                    break;
                default:
                    SetSwitchExit(currentExit.NextValid(SwitchType));
                    break;
            }

        if (Input.GetButtonUp(Hotkey) && InputMode == InputMode.Hold) SetSwitchExit(DefaultExit);

        if (currentLevel >= 0)
        {
            if (!Input.GetButton(Hotkey) || InputMode != InputMode.OneSecond)
                currentLevel -= Time.deltaTime;
            slider.normalizedValue = Math.Max(currentLevel / threshold, 0);
            slider.gameObject.SetActive(true);
        }
        else
        {
            slider.gameObject.SetActive(false);
        }

        if (!Input.GetButton(Hotkey) && InputMode == InputMode.OneSecond && currentExit != DefaultExit && currentLevel <= 0)
        {
            SetSwitchExit(DefaultExit);
        }
    }

    private void OnMouseDown()
    {
        SetSwitchExit(currentExit.NextValid(SwitchType));
    }

    private void SetSwitchExit(SwitchExit switchExit)
    {
        currentExit = switchExit;
        UpdateRotation();
    }

    void OnValidate()
    {
        currentExit = DefaultExit;
        UpdateRotation();
    }

    private void UpdateRotation()
    {
        var top = transform.GetChild(1);

        switch (currentExit)
        {
            case SwitchExit.Ahead:
                Bottom.OutputAngle = 0;
                break;
            case SwitchExit.Left:
                Bottom.OutputAngle = -90;
                break;
            case SwitchExit.Right:
                Bottom.OutputAngle = 90;
                break;
            default:
                throw new Exception();
        }

        top.transform.rotation = Quaternion.Euler(0, 180 + Bottom.OutputAngle, 0);
    }
}

public enum SwitchType
{
    TwoWay,
    ThreeWay
}

public enum SwitchExit
{
    Ahead,
    Left,
    Right
}

public static class EnumExtension
{
    public static T Next<T>(this T value)
    {
        var values = (T[]) Enum.GetValues(typeof(T));
        var index = Array.IndexOf(values, value);
        var nextIndex = (index + 1) % values.Length;
        return values[nextIndex];
    }

    public static SwitchExit NextValid(this SwitchExit value, SwitchType type)
    {
        var next = value.Next();
        if (type == SwitchType.TwoWay && next == SwitchExit.Ahead) next = next.Next();
        return next;
    }
}