using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    private Animator anim;
    private Rigidbody rb;

    private void Awake()
    {
        anim = this.gameObject.GetComponent<Animator>();
        rb = this.gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKey(KeyCode.RightArrow) /*|| Input.GetAxis("Horizontal") > 0*/)
        {
          if(Input.GetKey(KeyCode.LeftShift))
          {
            this.gameObject.transform.rotation = Quaternion.Euler(this.gameObject.transform.rotation.x, 180, this.gameObject.transform.rotation.z);
                rb.velocity = (Vector3.right*20);
                anim.SetFloat("MoveSpeed", 3);
          }
          else
          {
            this.gameObject.transform.rotation = Quaternion.Euler(this.gameObject.transform.rotation.x, 180, this.gameObject.transform.rotation.z);
                rb.velocity = (Vector3.right*10);
                anim.SetFloat("MoveSpeed", 1);
            }
        }
        else if(Input.GetKey(KeyCode.LeftArrow)/* || Input.GetAxis("Horizontal") < 0*/)
        {
          if(Input.GetKey(KeyCode.LeftShift))
          {
            this.gameObject.transform.rotation = Quaternion.Euler(this.gameObject.transform.rotation.x, 0, this.gameObject.transform.rotation.z);
                rb.velocity = (Vector3.left*20);
                anim.SetFloat("MoveSpeed", 3);
            }
          else
          {
            this.gameObject.transform.rotation = Quaternion.Euler(this.gameObject.transform.rotation.x, 0, this.gameObject.transform.rotation.z);
                rb.velocity = (Vector3.left*10);
                anim.SetFloat("MoveSpeed", 1);
            }
        }
        else
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
            anim.SetFloat("MoveSpeed", 1);
        }
    }
}
