using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject John;
    
    void Update()
    {
        Vector3 newPosition = John.transform.position;
        newPosition.z = -10f; // Mantener la cámara a una distancia fija en el eje Z
        transform.position = newPosition;
    }

}
