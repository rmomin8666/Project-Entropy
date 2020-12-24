using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//handles and stores health/stocks/weight any other character specific values
public class AttributeHandler : MonoBehaviour
{
    // Start is called before the first frame update
    private float health = 0f;
    [SerializeField] private int stocks = 3;
    private Rigidbody body;

    //character specific values!
    //need to decide if we should allow wonky things like slowing a character down or changing jump force;
    //public float weight {get;} = 1f Do we need this in here? depends

    void Start(){
        body = GetComponent<Rigidbody>();
    }

    public void die(Vector3 respawnPoint){
        health = 0f;
        stocks--;
        //reset other cool stuff here ie. something like banjo wonderwing
        if(stocks <= 0){
            transform.position = respawnPoint; //remove this and end game!
        }else{
            transform.position = respawnPoint;
            body.velocity = Vector3.zero;
        }
    }
    
    //other char responsible for handling dmg, kb etc. to allow for interesting effects ie. flinching
    //I don't think adding in the code for the above will slow enough for a flinch. Hardcoded flinches yes. Should create values specific to object (character) after object creation. -bobzo
    //Also if this is used to produce the object will it become emcombered putting each characters unique features in the same file. -bobzo
}
