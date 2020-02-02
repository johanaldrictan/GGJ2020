using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReformPlayer : MonoBehaviour
{
    public delegate void SelectSlime(Transform t);
    public static event SelectSlime OnSelect;
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
                parent.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
                for(int i = 0; i < childSpheres.Length; i++)
                {
                    childSpheres[i].GetComponent<SphereCollider>().enabled = false;
                    childSpheres[i].GetComponent<Rigidbody>().useGravity = false;
                    childSpheres[i].GetComponent<StopSphereAtParent>().enabled = true;
                    Vector3 pos1 = childSpheres[i].transform.position;
                    Vector3 pos2 = new Vector3(parent.transform.position.x, parent.transform.position.y+1, parent.transform.position.z);
                    Vector3 direction = (Vector3.Normalize(pos2 - pos1)*30);
                    childSpheres[i].GetComponent<Rigidbody>().velocity = (direction+player.GetComponent<Rigidbody>().velocity);
                    if(i == childSpheres.Length-1)
                    {
                      parent.transform.position = new Vector3(parent.transform.position.x, parent.transform.position.y+.5f, parent.transform.position.z);
                      parent.transform.rotation = Quaternion.Euler(0, 0, 0);
                      StartCoroutine(reformSlime());
                    }
                }
              selection = true;
            }
            GameObject.FindWithTag("Parent").GetComponent<Light>().enabled = false;
            OnSelect(GameObject.FindWithTag("Parent").transform); //Event to change camera
            GameObject.FindWithTag("Parent").tag = "Child";
            spheres[i].tag = "Parent";
            spheres[i].GetComponent<Light>().enabled = true;
        }
    }
    IEnumerator reformSlime()
    {
      yield return new WaitForSecondsRealtime(2);
      for(int i = 0; i < spheres.Count; i++)
      {
        spheres[i].GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
        spheres[i].GetComponent<MeshRenderer>().enabled = false;
        spheres[i].GetComponent<SphereCollider>().enabled = false;
        spheres[i].GetComponent<Rigidbody>().useGravity = false;
        spheres[i].GetComponent<StopSphereAtParent>().enabled = false;
        spheres[i].GetComponent<AnchorSphere>().enabled = false;
        if(spheres[i] != parent)
        {
          spheres[i].transform.parent = parent.transform;
        }
      }
      int k = 0;
      while(k < childSpheres.Length)
      {
        for(int x = -1; x < 2; x++)
        {
          for(int y = -1; y < 2; y++)
          {
            if(x != 0 || y != 0)
            {
              childSpheres[k].transform.localPosition = new Vector3(x,y,0);
              childSpheres[k].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
              k++;
            }
          }
        }
      }
      parent.GetComponent<AnchorSphere>().enabled = true;
      parent.transform.parent = player.transform;
      parent.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
      player.SetActive(true);
      player.transform.position = parent.transform.position;

    }
}
