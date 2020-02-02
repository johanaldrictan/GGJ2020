using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedoneReformPlayer : MonoBehaviour
{
    public delegate void SelectSlime(Transform t);
    public static event SelectSlime OnSelect;
    public GameObject player;
    public GameObject parent;
    public GameObject[] spheres;
    public GameObject[] childSpheres;
    GameObject currentSphere;
    int i;
    int t;
    bool selection = true;
    float toggleTimer = .000005f;
    float nextToggle = 0.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale < 0.0001f && Time.timeScale > .000001f)
        {
            if(selection)
            {
                i = 0;
                selection = false;
            }
            currentSphere = spheres[Mathf.Abs(i%spheres.Length)];
            OnSelect(currentSphere.transform);
            currentSphere.GetComponent<Light>().enabled = true;

            if(Input.GetKey(KeyCode.RightArrow) && Time.time > nextToggle)
            {
                nextToggle = Time.time + toggleTimer;
                spheres[Mathf.Abs(i%spheres.Length)].GetComponent<Light>().enabled = false;
                i += 1;
            }
            else if(Input.GetKey(KeyCode.LeftArrow) && Time.time > nextToggle)
            {
              nextToggle = Time.time + toggleTimer;
              spheres[Mathf.Abs(i%spheres.Length)].GetComponent<Light>().enabled = false;
              i -= 1;
            }
            else if(Input.GetKey(KeyCode.Space) && Time.time > nextToggle)
            {
                nextToggle = Time.time + toggleTimer;
                Time.timeScale = 1f;
                currentSphere.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
                for(int j = 0; j < spheres.Length; j++)
                {
                  currentSphere.GetComponent<Light>().enabled = false;
                  if(currentSphere != spheres[j])
                  {
                    spheres[j].GetComponent<SphereCollider>().enabled = false;
                    spheres[j].GetComponent<Rigidbody>().useGravity = false;
                    spheres[j].GetComponent<StopSphereAtParent>().enabled = true;
                    spheres[j].GetComponent<StopSphereAtParent>().stopper = currentSphere;
                    Vector3 pos1 = spheres[j].transform.position;
                    Vector3 pos2 = new Vector3(currentSphere.transform.position.x, currentSphere.transform.position.y+1, currentSphere.transform.position.z);
                    Vector3 direction = (Vector3.Normalize(pos2 - pos1)*30);
                    spheres[j].GetComponent<Rigidbody>().velocity = (direction);
                  }
                  if(j == spheres.Length-1)
                  {
                    parent.transform.position = new Vector3(parent.transform.position.x, parent.transform.position.y, parent.transform.position.z);
                    parent.transform.rotation = Quaternion.Euler(0, 0, 0);
                    StartCoroutine(reformSlime());
                  }

                }
              selection = true;
            }

        }
    }
    IEnumerator reformSlime()
    {
      yield return new WaitForSecondsRealtime(2);
      for(int p = 0; p < spheres.Length; p++)
      {
        spheres[p].GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
        spheres[p].GetComponent<MeshRenderer>().enabled = false;
        spheres[p].GetComponent<SphereCollider>().enabled = false;
        spheres[p].GetComponent<Rigidbody>().useGravity = false;
        spheres[p].GetComponent<StopSphereAtParent>().enabled = false;
        spheres[p].GetComponent<AnchorSphere>().enabled = false;
        if(spheres[p] != parent)
        {
          spheres[p].transform.parent = parent.transform;
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
      OnSelect(player.transform);

    }
}
