using UnityEngine;

//Goes on the empty parent of both Whole and Pieces object
public class BreakableObstacle : MonoBehaviour
{
    [SerializeField] private GameObject[] whole; //This object is active when environment is "repaired". These should be stationary.
    [SerializeField] private GameObject[] pieces; //This object is active when environment is "broken". These may have rigidbodies.
    private Vector3[] piecesPos;
    private Quaternion[] piecesRot;
    private bool repaired = true;

    private static BreakableObstacle bo;

    private void Awake()
    {
        //Singleton
        if (bo == null)
        {
            bo = this;
        }
        else if (bo != this)
        {
            Debug.LogError("There is already a BreakableObject.", bo.gameObject);
        }

        piecesPos = new Vector3[pieces.Length];
        for (int i = 0; i < pieces.Length; i++)
        {
            piecesPos[i] = pieces[i].transform.position;
        }

        piecesRot = new Quaternion[pieces.Length];
        for (int i = 0; i < pieces.Length; i++)
        {
            piecesRot[i] = pieces[i].transform.rotation;
        }
    }

    private void OnDestroy()
    {
        if(bo == this)
        {
            bo = null;
        }
    }

    private void Update()
    {
        if (repaired && Input.GetButtonDown("Break"))
        {
            ToggleRepair(false);
        }
        else if (!repaired && Input.GetButtonDown("Repair"))
        {
            for (int i = 0; i < pieces.Length; i++)
            {
                Rigidbody rb = pieces[i].GetComponent<Rigidbody>();
                if (rb != null)
                {
                    pieces[i].transform.position = piecesPos[i];
                    pieces[i].transform.rotation = piecesRot[i];
                    rb.velocity = Vector3.zero;
                }
            }
            ToggleRepair(true);
        }
    }

    private void ToggleRepair(bool repair)
    {
        repaired = repair;
        foreach (GameObject w_go in whole)
        {
            w_go.SetActive(repair);
        }
        foreach (GameObject p_go in pieces)
        {
            p_go.SetActive(!repair);
        }
    }
}
