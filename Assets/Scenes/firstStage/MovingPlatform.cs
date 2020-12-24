using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public GameObject Player;

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject == Player)
        {
            Player.transform.parent = transform;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject == Player)
        {
            Player.transform.parent = null;
        }
    }


}
