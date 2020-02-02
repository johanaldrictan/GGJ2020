using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocktoXY : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      this.gameObject.transform.position = new Vector3(0,this.gameObject.transform.position.y, this.gameObject.transform.position.z);
    }
}
