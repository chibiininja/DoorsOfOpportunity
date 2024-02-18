using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    [Serializable]
    public class DoorTuple
    {
        public Door door;
        public bool expectedValue;
        public bool OpenForward = true;
    }
    [SerializeField]
    private bool IsLinked;
    public List<DoorTuple> Inputs;
    public List<DoorTuple> Outputs;
    private bool Correct = false;


    public List<DoorTuple> GetOutputs()
    {
        return Outputs;
    }
    void UpdateDoor(Door door)
    {
        Inputs.ForEach(delegate (DoorTuple t)
        {
            if (t.door == door)
                StartCoroutine(CheckValues());
        });
    }

    IEnumerator CheckValues()
    {
        yield return new WaitForSeconds(0.1f);
        Correct = true;
        Inputs.ForEach(delegate (DoorTuple t)
        {
            if (t.door.IsOpen != t.expectedValue)
                Correct = false;
        });

        if (IsLinked)
        {
            if (Correct)
                SetOutputs(true);
            else
                SetOutputs(false);
        }
        else
        {
            if (Correct)
            {
                SetOutputs(true);
            }
        }
    }

    void SetOutputs(bool value)
    {
        Outputs.ForEach(delegate (DoorTuple t)
        {
            if (value)
            {
                if (t.expectedValue)
                    t.door.Open(t.OpenForward);
                else
                    t.door.Close();
            }
            else
            {
                if (t.expectedValue)
                    t.door.Close();
                else
                    t.door.Open(t.OpenForward);
            }
        });
    }
}
