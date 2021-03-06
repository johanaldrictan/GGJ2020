﻿using System.Collections;
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

        if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)/*|| Input.GetAxis("Horizontal") > 0*/)
        {
          if(Input.GetKey(KeyCode.LeftShift))
          {
            this.gameObject.transform.rotation = Quaternion.Euler(this.gameObject.transform.rotation.x, 270, this.gameObject.transform.rotation.z);
                rb.velocity = new Vector3(0, rb.velocity.y, -1*5f);
                anim.SetFloat("MoveSpeed", 3);
          }
          else
          {
            this.gameObject.transform.rotation = Quaternion.Euler(this.gameObject.transform.rotation.x, 270, this.gameObject.transform.rotation.z);
                rb.velocity = new Vector3(0, rb.velocity.y, -1*2.5f);
                anim.SetFloat("MoveSpeed", 1);
            }
        }
        else if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)/* || Input.GetAxis("Horizontal") < 0*/)
        {
          if(Input.GetKey(KeyCode.LeftShift))
          {
            this.gameObject.transform.rotation = Quaternion.Euler(this.gameObject.transform.rotation.x, 90, this.gameObject.transform.rotation.z);
                rb.velocity = new Vector3(0, rb.velocity.y, 1*5f);
                anim.SetFloat("MoveSpeed", 3);
            }
          else
          {
            this.gameObject.transform.rotation = Quaternion.Euler(this.gameObject.transform.rotation.x, 90, this.gameObject.transform.rotation.z);
                rb.velocity = new Vector3(0, rb.velocity.y, 1*2.5f);
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
