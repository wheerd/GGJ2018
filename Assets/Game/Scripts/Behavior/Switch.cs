using System;
using System.Linq;
using Assets.Game.Scripts;
using ModestTree;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class Switch : MonoBehaviourWithCursor
{
    private const float dampening = 0.3f;
    private const float threshold = 1.0f;

    public SwitchBottom Bottom;

    private SwitchExit currentExit;

    private float currentLevel;

    public SwitchExit DefaultExit;

    public Hotkey Hotkey;

    public InputMode InputMode;

    public float OutputSpeed = 5.0f;

    public Slider slider;

    public SwitchType SwitchType;

    [Inject] private PlayMusicClipSignal playMusicClipSignal;

    [SerializeField] private AudioClip changeSwitchSound;

    private bool _isActive = true;

    private void Start()
    {
        slider.gameObject.SetActive(false);

        if (Hotkey == Hotkey.Auto) Hotkey = GetFirstUnusedHotkey();

        var hotkeyName = Hotkey.ToString();
        GetComponentInChildren<Text>().text = hotkeyName.Substring(hotkeyName.Length - 1);

        Bottom.Speed = OutputSpeed;

        // disable hotkey UI on mobile
#if UNITY_ANDROID || UNITY_IOS
        GetComponentInChildren<Text>().transform.parent.gameObject.SetActive(false);
#endif

        currentExit = DefaultExit;
        UpdateRotation();
    }

    private void OnTriggerStay(Collider other)
    {
        var gameObject = other.gameObject;
        if (gameObject.CompareTag(Tags.Package))
        {
            _isActive = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var gameObject = other.gameObject;
        if (gameObject.CompareTag(Tags.Package))
        {
            _isActive = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var gameObject = other.gameObject;
        if (gameObject.CompareTag(Tags.Package))
        {
            _isActive = false;
        }
    }

    private Hotkey GetFirstUnusedHotkey()
    {
        var hotkeys = (Hotkey[]) Enum.GetValues(typeof(Hotkey));
        var usedHotkeys = FindObjectsOfType<Switch>().Select(s => s.Hotkey).ToHashSet();

        return hotkeys.First(h => !usedHotkeys.Contains(h));
    }

    private void FixedUpdate()
    {
        if (!_isActive)
        {
            return;
        }

        if (Hotkey.WasPressedThisFrame())
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

                    currentLevel -= Time.fixedDeltaTime;
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

        if (Hotkey.WasReleasedThisFrame() && InputMode == InputMode.Hold) SetSwitchExit(DefaultExit);

        if (currentLevel >= 0)
        {
            if (!Hotkey.IsPressed() || InputMode != InputMode.OneSecond)
                currentLevel -= Time.fixedDeltaTime;
            slider.normalizedValue = Math.Max(currentLevel / threshold, 0);
            slider.gameObject.SetActive(true);
        }
        else
        {
            slider.gameObject.SetActive(false);
        }

        if (!Hotkey.IsPressed() && InputMode == InputMode.OneSecond && currentExit != DefaultExit && currentLevel <= 0)
            SetSwitchExit(DefaultExit);
    }

    private void OnMouseDown()
    {
        if (!_isActive)
        {
            return;
        }

        SetSwitchExit(currentExit.NextValid(SwitchType));
    }

    private void SetSwitchExit(SwitchExit switchExit)
    {
        currentExit = switchExit;

        playMusicClipSignal.Fire(changeSwitchSound);
        UpdateRotation();
    }

    private void OnValidate()
    {
        if (DefaultExit == SwitchExit.Ahead && SwitchType == SwitchType.TwoWay) DefaultExit = SwitchExit.Left;
        currentExit = DefaultExit;
        UpdateRotation();
    }

    private void UpdateRotation()
    {
        float angle;

        switch (currentExit)
        {
            case SwitchExit.Left:
                angle = -90;
                break;
            case SwitchExit.Ahead:
                angle = 0;
                break;
            case SwitchExit.Right:
                angle = 90;
                break;
            default:
                throw new Exception();
        }

        Bottom.transform.rotation = Quaternion.Euler(0, angle, 0);
    }
}

public enum InputMode
{
    Single,
    Hold,
    OneSecond,
    ButtonMash
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