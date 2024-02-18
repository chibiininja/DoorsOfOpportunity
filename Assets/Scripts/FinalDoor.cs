using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class FinalDoor : MonoBehaviour
{
    public bool IsOpen = false;
    public GameObject Highlight;
    public GameObject ConnectedHighlight;
    [Header("Rotation Configs")]
    [SerializeField]
    private bool IsRotatingDoor = true;
    [SerializeField]
    private float RotationSpeed = 1f;
    [SerializeField]
    private float RotationAmount = 90f;
    [SerializeField]
    private float ForwardDirection = 0;
    [Header("Sliding Configs")]
    [SerializeField]
    private GameObject upperHalf;
    [SerializeField]
    private GameObject lowerHalf;
    [SerializeField]
    private Vector3 SlideDirection = Vector3.up;
    [SerializeField]
    private float SlidingSpeed = 1f;

    private Vector3 StartRotation;
    private Vector3 Forward;
    private Vector3 Backward;
    private Vector3 UpperStartPosition;
    private Vector3 LowerStartPosition;

    private Coroutine AnimationCoroutine;

    private void Awake()
    {
        StartRotation = transform.rotation.eulerAngles;
        // "Forward" is pointing into the door frame
        Forward = transform.right;
        Backward = new Vector3(-1f * Forward.x, -1f * Forward.y, -1f * Forward.z);
        UpperStartPosition = upperHalf.transform.position;
        LowerStartPosition = lowerHalf.transform.position;
    }

    private void Start()
    {
        if (IsOpen)
        {
            if (AnimationCoroutine != null)
            {
                StopCoroutine(AnimationCoroutine);
            }

            if (IsRotatingDoor)
            {
                float dot = Vector3.Dot(Forward, (new Vector3(0f, 0f, 0f) - transform.position).normalized);
                AnimationCoroutine = StartCoroutine(DoRotationOpen(dot));
            }
            else
            {
                AnimationCoroutine = StartCoroutine(DoSlidingOpen());
            }
        }
    }

    public void Open(Vector3 UserPosition)
    {
        if (!IsOpen)
        {
            if (AnimationCoroutine != null)
            {
                StopCoroutine(AnimationCoroutine);
            }

            if (IsRotatingDoor)
            {
                float dot = Vector3.Dot(Forward, (UserPosition - transform.position).normalized);
                Debug.Log($"Dot: {dot.ToString("N3")}");
                AnimationCoroutine = StartCoroutine(DoRotationOpen(dot));
            }
            else
            {
                AnimationCoroutine = StartCoroutine(DoSlidingOpen());
            }
        }
    }

    public void Open(bool OpenForward)
    {
        if (!IsOpen)
        {
            if (AnimationCoroutine != null)
            {
                StopCoroutine(AnimationCoroutine);
            }

            if (IsRotatingDoor)
            {
                float dot = Vector3.Dot(Forward, !OpenForward ? Forward : Backward);
                AnimationCoroutine = StartCoroutine(DoRotationOpen(dot));
            }
            else
            {
                AnimationCoroutine = StartCoroutine(DoSlidingOpen());
            }
        }
    }

    private IEnumerator DoRotationOpen(float ForwardAmount)
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation;

        endRotation = Quaternion.Euler(new Vector3(StartRotation.x, StartRotation.y + (ForwardAmount >= ForwardDirection ? RotationAmount : -RotationAmount), StartRotation.z));

        IsOpen = true;

        float time = 0;
        while (time < 1)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, time);
            yield return null;
            time += Time.deltaTime * RotationSpeed;
        }
    }

    private IEnumerator DoSlidingOpen()
    {
        Vector3 endUpperPosition = UpperStartPosition + 6.680711f * SlideDirection;
        Vector3 startUpperPosition = upperHalf.transform.position;
        Vector3 endLowerPosition = LowerStartPosition + 7.796122f * -SlideDirection;
        Vector3 startLowerPosition = lowerHalf.transform.position;

        float time = 0;
        IsOpen = true;
        while (time < 1)
        {
            upperHalf.transform.position = Vector3.Lerp(startUpperPosition, endUpperPosition, time);
            lowerHalf.transform.position = Vector3.Lerp(startLowerPosition, endLowerPosition, time);
            yield return null;
            time += Time.deltaTime * SlidingSpeed;
        }
    }

    public void Close()
    {
        if (IsOpen)
        {
            if (AnimationCoroutine != null)
            {
                StopCoroutine(AnimationCoroutine);
            }

            if (IsRotatingDoor)
            {
                AnimationCoroutine = StartCoroutine(DoRotationClose());
            }
            else
            {
                AnimationCoroutine = StartCoroutine(DoSlidingClose());
            }
        }
    }

    private IEnumerator DoRotationClose()
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(StartRotation);

        IsOpen = false;

        float time = 0;
        while (time < 1)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, time);
            yield return null;
            time += Time.deltaTime * RotationSpeed;
        }
    }

    private IEnumerator DoSlidingClose()
    {
        Vector3 endUpperPosition = UpperStartPosition;
        Vector3 startUpperPosition = upperHalf.transform.position;
        Vector3 endLowerPosition = LowerStartPosition;
        Vector3 startLowerPosition = lowerHalf.transform.position;

        float time = 0;
        IsOpen = false;
        while (time < 1)
        {
            upperHalf.transform.position = Vector3.Lerp(startUpperPosition, endUpperPosition, time);
            lowerHalf.transform.position = Vector3.Lerp(startLowerPosition, endLowerPosition, time);
            yield return null;
            time += Time.deltaTime * SlidingSpeed;
        }
    }
}
