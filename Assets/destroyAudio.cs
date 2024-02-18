using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyAudio : MonoBehaviour
{
    private float startTime;
    [SerializeField] float delay;
    void Start()
    {
        startTime += Time.time;
    }
    void Update()
    {
        if (Time.time - startTime >= delay)
        {
            Destroy(this.gameObject);
        }
    }
}
