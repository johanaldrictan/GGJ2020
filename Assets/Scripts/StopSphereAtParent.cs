using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopSphereAtParent : MonoBehaviour
{
    public GameObject stopper;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(stopper.transform.position.z-.5f < this.gameObject.transform.position.z && this.gameObject.transform.position.z < stopper.transform.position.z + .5f)
        {
            this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
        }
    }
}
