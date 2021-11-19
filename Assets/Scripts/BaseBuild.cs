using UnityEngine;

public abstract class BaseBuild : MonoBehaviour
{
    public abstract float Size { get; }

    public bool CanCreate { get; set; } = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ground")
        {
            CanCreate = true;
        }
        else
        {
            CanCreate = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        CanCreate = true;
    }
}
