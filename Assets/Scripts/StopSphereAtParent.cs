using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopSphereAtParent : MonoBehaviour
{
    GameObject parent;
    // Start is called before the first frame update
    void Start()
    {
        parent = GameObject.FindWithTag("Parent");
    }

    // Update is called once per frame
    void Update()
    {
        if(parent.transform.position.x-.5 <= this.gameObject.transform.position.x || this.gameObject.transform.position.x < parent.transform.position.x+.5)
        {
            this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
        }
    }
}
