using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    [SerializeField]
    private Transform Camera;
    [SerializeField]
    private float MaxUseDistance = 5f;
    [SerializeField]
    private LayerMask UseLayers;

    public void OnUse()
    {
        Debug.Log("Use!");
        if (Physics.Raycast(Camera.position, Camera.forward, out RaycastHit hit, MaxUseDistance, UseLayers))
        {
            if (hit.collider.transform.parent.TryGetComponent<Door>(out Door door))
            {
                if (door.IsOpen)
                {
                    door.Close();
                }
                else
                {
                    door.Open(transform.position);
                }
            }
        }
    }

    private void Update()
    {
        if (Physics.Raycast(Camera.position, Camera.forward, out RaycastHit hit, MaxUseDistance, UseLayers) && hit.collider.transform.parent.TryGetComponent<Door>(out Door door))
        {
            //Outline Door
        }
    }
}
