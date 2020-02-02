using UnityEngine;

//Goes on the empty parent of both Whole and Pieces object
public class BreakableObstacle : MonoBehaviour
{
    [SerializeField] private GameObject whole; //This object is active when environment is "repaired"
    [SerializeField] private GameObject[] pieces; //This object is active when environment is "broken"
    private Vector3[] piecesPos;
    private Quaternion[] piecesRot;
    private bool repaired = true;

    private void Awake()
    {
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
        whole.SetActive(repair);
        foreach (GameObject go in pieces)
        {
            go.SetActive(!repair);
        }
    }
}
