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
    Color selectColor;
    public int yBoundNeg;
    public int yBoundPos;
    public int zBoundNeg;
    public int zBoundPos;
    // Start is called before the first frame update
    void Start()
    {
      selectColor = parent.GetComponent<Light>().color;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale < 0.0001f && Time.timeScale > .000001f)
        {
            if(selection)
            {
                i = 0;
                currentSphere = spheres[Mathf.Abs(i%spheres.Length)];
                OnSelect(currentSphere.transform);
                currentSphere.GetComponent<Light>().enabled = true;
                selection = false;
            }


            if((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && Time.time > nextToggle)
            {
                nextToggle = Time.time + toggleTimer;
                spheres[Mathf.Abs(i%spheres.Length)].GetComponent<Light>().enabled = false;
                i += 1;
                if(yBoundNeg > spheres[Mathf.Abs(i%spheres.Length)].transform.position.y || yBoundPos < spheres[Mathf.Abs(i%spheres.Length)].transform.position.y || zBoundNeg > spheres[Mathf.Abs(i%spheres.Length)].transform.position.z || zBoundPos < spheres[Mathf.Abs(i%spheres.Length)].transform.position.z)
                {
                  i +=1;
                }
                currentSphere = spheres[Mathf.Abs(i%spheres.Length)];
                OnSelect(currentSphere.transform);
                currentSphere.GetComponent<Light>().enabled = true;

            }
            else if((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && Time.time > nextToggle)
            {
                nextToggle = Time.time + toggleTimer;
                spheres[Mathf.Abs(i%spheres.Length)].GetComponent<Light>().enabled = false;
                i -= 1;
                if(yBoundNeg > spheres[Mathf.Abs(i%spheres.Length)].transform.position.y || yBoundPos < spheres[Mathf.Abs(i%spheres.Length)].transform.position.y || zBoundNeg > spheres[Mathf.Abs(i%spheres.Length)].transform.position.z || zBoundPos < spheres[Mathf.Abs(i%spheres.Length)].transform.position.z)
                {
                  i -=1;
                }
                currentSphere = spheres[Mathf.Abs(i % spheres.Length)];

                OnSelect(currentSphere.transform);
                currentSphere.GetComponent<Light>().enabled = true;

            }
            else if(Input.GetKey(KeyCode.Space) && Time.time > nextToggle)
            {
                nextToggle = Time.time + toggleTimer;
                Time.timeScale = 1f;
                currentSphere.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
                for(int j = 0; j < spheres.Length; j++)
                {
                  if(currentSphere != spheres[j])
                  {
                    currentSphere.GetComponent<Light>().enabled = false;
                    spheres[j].GetComponent<SphereCollider>().enabled = false;
                    spheres[j].GetComponent<Rigidbody>().useGravity = false;
                    spheres[j].GetComponent<StopSphereAtParent>().enabled = true;
                    spheres[j].GetComponent<StopSphereAtParent>().stopper = currentSphere;
                    Vector3 pos1 = spheres[j].transform.position;
                    Vector3 pos2 = new Vector3(currentSphere.transform.position.x, currentSphere.transform.position.y+.5f, currentSphere.transform.position.z);
                    Vector3 direction = (Vector3.Normalize(pos2 - pos1)*30);
                    spheres[j].GetComponent<Rigidbody>().velocity = (direction);
                  }
                  if(j == spheres.Length-1)
                  {
                    parent.GetComponent<Light>().enabled = true;
                    parent.GetComponent<Light>().color = Color.yellow;
                    parent.GetComponent<Light>().range = 10f;
                    parent.GetComponent<Light>().intensity = 20f;
                    parent.transform.position = currentSphere.transform.position;
                    parent.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
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
        for(int z = -1; z < 2; z++)
        {
          for(int y = -1; y < 2; y++)
          {
            if(z != 0 || y != 0)
            {
              childSpheres[k].transform.localPosition = new Vector3(0,y,z);
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
      parent.GetComponent<Light>().enabled = false;
      parent.GetComponent<Light>().color = selectColor;
      parent.GetComponent<Light>().intensity = 1f;
      parent.GetComponent<Light>().range = .5f;
    }
}
