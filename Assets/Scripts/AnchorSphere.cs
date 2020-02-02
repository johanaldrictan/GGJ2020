using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorSphere : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.position = new Vector3(this.gameObject.transform.parent.position.x, this.gameObject.transform.parent.position.y - .2f, this.gameObject.transform.parent.position.z);
    }
}
