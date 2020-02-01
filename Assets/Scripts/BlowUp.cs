using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowUp : MonoBehaviour
{
    GameObject[] children;
    GameObject parent;
    GameObject player;
    float spaceTimer = 2f;
    float nextSpace = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        children = GameObject.FindGameObjectsWithTag("Child");
        parent = GameObject.FindWithTag("Parent");
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
      if(Input.GetKey(KeyCode.Space) && Time.time > nextSpace)
      {
        parent.transform.parent = null;
        player.SetActive(false);
        parent.GetComponent<MeshRenderer>().enabled = true;
        parent.GetComponent<SphereCollider>().enabled = true;
        parent.GetComponent<AnchorSphere>().enabled = false;
        nextSpace = Time.time + spaceTimer;
        for(int i = 0; i < children.Length; i++)
        {
          children[i].GetComponent<MeshRenderer>().enabled = true;
          children[i].GetComponent<SphereCollider>().enabled = true;
          Vector3 pos1 = children[i].transform.position;
          Vector3 pos2 = parent.transform.position;
          Vector3 direction = (Vector3.Normalize(pos1 - pos2)*10);
          children[i].GetComponent<Rigidbody>().velocity = (direction+player.GetComponent<Rigidbody>().velocity);
        }
      }
    }
}
