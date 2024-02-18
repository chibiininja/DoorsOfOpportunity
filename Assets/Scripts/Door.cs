using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Door : MonoBehaviour
{
    public bool IsOpen = false;
    public GameObject Highlight;
    public GameObject ConnectedHighlight;
    public GameObject DebugText;
    [Header("Rotation Configs")]
    [SerializeField]
    private bool IsRotatingDoor = true;
    [SerializeField]
    private float RotationSpeed = 1f;
    [SerializeField]
    private float RotationAmount = 90f;
    [SerializeField]
    private float ForwardDirection = 0;

    private Vector3 StartRotation;
    private Vector3 Forward;
    private Vector3 Backward;
    private Vector3 StartPosition;

    private Coroutine AnimationCoroutine;

    private void Awake()
    {
        StartRotation = transform.rotation.eulerAngles;
        // "Forward" is pointing into the door frame
        Forward = transform.right;
        Backward = new Vector3(-1f * Forward.x, -1f * Forward.y, -1f * Forward.z);
        StartPosition = transform.position;

        #if !(UNITY_EDITOR)
        DebugText.SetActive(false);
        #endif
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
        }
    }

    private void OnValidate()
    {
        DebugText.GetComponent<TextMeshPro>().text = transform.name + " is " + (IsOpen ? "Open" : "Closed");
    }

    private void Update()
    {
        DebugText.GetComponent<TextMeshPro>().text = transform.name + " is " + (IsOpen ? "Open" : "Closed");
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
}
