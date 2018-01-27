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

    private readonly HashSet<GameObject> ignored = new HashSet<GameObject>();

    private readonly Queue<GameObject> packageQueue = new Queue<GameObject>();

    private SwitchExit currentExit;

    private float currentLevel;

    public SwitchExit DefaultExit;

    public string Hotkey = "Hotkey1";

    public InputMode InputMode;

    public float OutputSpeed = 5.0f;

    public Slider slider;
    public SwitchType SwitchType;

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

        if (packageQueue.Any())
        {
            var package = packageQueue.Dequeue();
            MovePackageToExit(package, currentExit);
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
        float yAngle;

        switch (currentExit)
        {
            case SwitchExit.Ahead:
                yAngle = 180;
                break;
            case SwitchExit.Left:
                yAngle = 90;
                break;
            case SwitchExit.Right:
                yAngle = -90;
                break;
            default:
                throw new Exception();
        }

        top.transform.rotation = Quaternion.Euler(0, yAngle, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Tags.Package))
        {
            if (ignored.Contains(other.gameObject)) return;
            ignored.Add(other.gameObject);

            var position = transform.position;
            var rigidBody = other.gameObject.GetComponent<Rigidbody>();

            rigidBody.velocity = Vector3.zero;
            rigidBody.MovePosition(position);
            rigidBody.isKinematic = true;

            packageQueue.Enqueue(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(Tags.Package)) ignored.Remove(other.gameObject);
    }

    private void MovePackageToExit(GameObject gameObject, SwitchExit exit)
    {
        var rigidBody = gameObject.GetComponent<Rigidbody>();
        rigidBody.isKinematic = false;

        switch (exit)
        {
            case SwitchExit.Ahead:
                rigidBody.velocity = transform.forward * OutputSpeed;
                break;
            case SwitchExit.Left:
                rigidBody.velocity = transform.right * -1 * OutputSpeed;
                break;
            case SwitchExit.Right:
                rigidBody.velocity = transform.right * OutputSpeed;
                break;
            default:
                throw new ExitGUIException();
        }
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