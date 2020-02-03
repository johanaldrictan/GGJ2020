using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedoneBlowUp : MonoBehaviour
{
    public delegate void SelectSlime(Transform t);
    public static event SelectSlime OnSelect;
    public GameObject[] children;
    public GameObject parent;
    public GameObject player;
    float spaceTimer = 2f;
    float nextSpace = 0.0f;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      if(Input.GetKey(KeyCode.Space) && Time.time > nextSpace && player.activeInHierarchy == true)
      {
            SFXManager.instance.PlayRandomWithMod("Pop");
        parent.transform.parent = null;
        player.SetActive(false);
        parent.GetComponent<MeshRenderer>().enabled = true;
        parent.GetComponent<SphereCollider>().enabled = true;
        parent.GetComponent<AnchorSphere>().enabled = false;
        parent.GetComponent<Rigidbody>().useGravity = true;
        nextSpace = Time.time + spaceTimer;
        for(int i = 0; i < children.Length; i++)
        {
          children[i].GetComponent<MeshRenderer>().enabled = true;
          children[i].GetComponent<SphereCollider>().enabled = true;
          children[i].GetComponent<Rigidbody>().useGravity = true;
          children[i].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
          children[i].transform.parent = null;
          Vector3 pos1 = children[i].transform.position;
          Vector3 pos2 = parent.transform.position;
          Vector3 direction = (Vector3.Normalize(pos1 - pos2)*10);
          children[i].GetComponent<Rigidbody>().velocity = (direction+player.GetComponent<Rigidbody>().velocity);
        }
        OnSelect(parent.transform);
        StartCoroutine(afterThree());
      }
    }
    IEnumerator afterThree()
    {
      yield return new WaitForSecondsRealtime(3);
      Time.timeScale = 0.00001f;

    }
}
