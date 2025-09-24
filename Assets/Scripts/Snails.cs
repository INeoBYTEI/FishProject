using UnityEngine;

public class Snails : MonoBehaviour
{
    public GameObject playerHand;
    public bool isEquipped = false;
    private Rigidbody rb;
    private BoxCollider boxCollider;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
    }
    public void Equip()
    {
        isEquipped = true;
        boxCollider.enabled = false;
        transform.SetParent(playerHand.transform);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        rb.isKinematic = true;
    }

    public void Unequip()
    {
        isEquipped = false;
        boxCollider.enabled = true;
        transform.SetParent(null, true);
        rb.isKinematic = false;
    }
}
