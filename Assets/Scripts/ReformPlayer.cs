using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReformPlayer : MonoBehaviour
{
    GameObject player;
    GameObject parent;
    List<GameObject> spheres;
    GameObject[] childSpheres;
    int i;
    bool selection = true;
    float toggleTimer = .000005f;
    float nextToggle = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale == 0.00001f)
        {
            if(selection)
            {
                childSpheres = GameObject.FindGameObjectsWithTag("Child");
                spheres = new List<GameObject>(childSpheres);
                spheres.Add(GameObject.FindWithTag("Parent"));
                i = (spheres.Count-1);
                selection = false;
            }

            if(Input.GetKey(KeyCode.RightArrow) && Time.time > nextToggle)
            {
                nextToggle = Time.time + toggleTimer;
                i += 1;
                if(i >= spheres.Count)
                {
                i = 0;
                }
            }
            else if(Input.GetKey(KeyCode.LeftArrow) && Time.time > nextToggle)
            {
              nextToggle = Time.time + toggleTimer;
              i -= 1;
              if(i < 0)
              {
                i = spheres.Count-1;
              }
            }
            else if(Input.GetKey(KeyCode.Space) && Time.time > nextToggle)
            {
                nextToggle = Time.time + toggleTimer;
                Time.timeScale = 1f;
                childSpheres = GameObject.FindGameObjectsWithTag("Child");
                parent = GameObject.FindWithTag("Parent");
                parent.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
                for(int i = 0; i < childSpheres.Length; i++)
                {
                    //children[i].GetComponent<MeshRenderer>().enabled = true;
                    childSpheres[i].GetComponent<SphereCollider>().enabled = false;
                    childSpheres[i].GetComponent<Rigidbody>().useGravity = false;
                    //children[i].transform.parent = null;
                    childSpheres[i].GetComponent<StopSphereAtParent>().enabled = true;
                    Vector3 pos1 = childSpheres[i].transform.position;
                    Vector3 pos2 = new Vector3(parent.transform.position.x, parent.transform.position.y+1, parent.transform.position.z);
                    Vector3 direction = (Vector3.Normalize(pos2 - pos1)*30);
                    childSpheres[i].GetComponent<Rigidbody>().velocity = (direction+player.GetComponent<Rigidbody>().velocity);
                }
              selection = true;
            }
            GameObject.FindWithTag("Parent").GetComponent<Light>().enabled = false;
            GameObject.FindWithTag("Parent").tag = "Child";
            spheres[i].tag = "Parent";
            spheres[i].GetComponent<Light>().enabled = true;
        }
    }
}
