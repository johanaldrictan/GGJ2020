using UnityEngine;

//Goes on the empty parent of both Whole and Pieces object
public class BreakableObstacle : MonoBehaviour
{
    [SerializeField] private GameObject whole; //This object is active when environment is "repaired"
    [SerializeField] private GameObject[] pieces; //This object is active when environment is "broken"
    private bool repaired = true;

    private void Update()
    {
        if (repaired && Input.GetButtonDown("Break"))
        {
            ToggleRepair(false);
        }
        else if (!repaired && Input.GetButtonDown("Repair"))
        {
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
