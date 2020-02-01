using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowUp : MonoBehaviour
{
    GameObject[] children;
    GameObject parent;
    float spaceTimer = 2f;
    float nextSpace = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        children = GameObject.FindGameObjectsWithTag("Child");
        parent = GameObject.FindWithTag("Parent");
    }

    // Update is called once per frame
    void Update()
    {
      if(Input.GetKey(KeyCode.Space) && Time.time > nextSpace)
      {
        nextSpace = Time.time + spaceTimer;
        for(int i = 0; i < children.Length; i++)
        {
          Vector3 pos1 = children[i].transform.position;
          Vector3 pos2 = parent.transform.position;
          Vector3 direction = (Vector3.Normalize(pos1 - pos2)*500);
          children[i].GetComponent<Rigidbody>().AddForce(direction, ForceMode.Acceleration);
        }
      }
    }
}
