using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastZoneScript : MonoBehaviour
{
    private Vector3 respawnPoint;
    void Awake(){
        respawnPoint = GetComponent<Transform>().position;
        respawnPoint.y += .5f;
    }
    void OnTriggerExit(Collider other){
        other.GetComponent<AttributeHandler>().die(respawnPoint);
    }
}
