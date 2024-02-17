using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleTrigger : MonoBehaviour
{
    [Serializable]
    public class DoorTuple
    {
        public Door door;
        public bool OpenForward = true;
    }
    public List<DoorTuple> Inputs;
    public List<DoorTuple> Outputs;

    void UpdateDoor(Door door)
    {
        Inputs.ForEach(delegate (DoorTuple t)
        {
            if (t.door == door)
                SetOutputs();
        });
    }

    void SetOutputs()
    {
        Outputs.ForEach(delegate (DoorTuple t)
        {
            if (t.door.IsOpen)
                t.door.Close();
            else
                t.door.Open(t.OpenForward);
        });
    }
}
