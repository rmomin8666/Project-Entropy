using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawnTest : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject myPrefab;

    public void SRED()
    {
    // This script will simply instantiate the Prefab when the game starts.

        // Instantiate at position (0, 0, 0) and zero rotation.
        Instantiate(myPrefab, new Vector3(0, 0, 0), Quaternion.identity);
    }
}
