using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    GameObject parent;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        parent = GameObject.FindWithTag("Parent");
        this.gameObject.transform.position = new Vector3(parent.transform.position.x, parent.transform.position.y, this.gameObject.transform.position.z);
    }
}
