using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKey(KeyCode.RightArrow))
        {
          this.gameObject.transform.rotation = Quaternion.Euler(this.gameObject.transform.rotation.x, 180, this.gameObject.transform.rotation.z);
          this.gameObject.GetComponent<Rigidbody>().velocity = (Vector3.right*10);
        }
        else if(Input.GetKey(KeyCode.LeftArrow))
        {
          this.gameObject.transform.rotation = Quaternion.Euler(this.gameObject.transform.rotation.x, 0, this.gameObject.transform.rotation.z);
          this.gameObject.GetComponent<Rigidbody>().velocity = (Vector3.left*10);
        }
        else
        {
          this.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
        }
    }
}
