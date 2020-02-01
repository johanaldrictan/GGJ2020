using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReformPlayer : MonoBehaviour
{
    GameObject player;
    GameObject parent;
    int i;
    bool selection = true;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale == 0.1f)
        {
            GameObject[] childSpheres = GameObject.FindGameObjectsWithTag("Child");
            List<GameObject> spheres = new List<GameObject>(childSpheres);
            spheres.Add(GameObject.FindWithTag("Parent"));
            i = (spheres.Count-1);
            while(selection)
            {
              if(Input.GetKey(KeyCode.RightArrow))
              {
                i += 1;
                if(i >= spheres.Count)
                {
                  i = 0;
                }
              }
              else if(Input.GetKey(KeyCode.LeftArrow))
              {
                i -= 1;
                if(i < 0)
                {
                  i = spheres.Count-1;
                }
              }
              else if(Input.GetKey(KeyCode.Space))
              {
                Debug.Log("Space press to choose");
              }
              GameObject.FindWithTag("Parent").tag = "Child";
              spheres[i].tag = "Parent";
            }
        }
    }
}
