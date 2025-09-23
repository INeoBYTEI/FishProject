using UnityEngine;

public class CanHandler : MonoBehaviour
{
    public GameObject sprinklesPrefab;
    public Transform sprinkleSpawnPoint;
    public GameObject playerHand;
    public bool isEquipped = false;
    private Animator animator;
    private Rigidbody rb;
    private BoxCollider boxCollider;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
    }
    private void Update()
    {
        if (isEquipped)
        {
            boxCollider.enabled = false;
            transform.SetParent(playerHand.transform);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }
        else
        {
            boxCollider.enabled = true;
            transform.SetParent(null);
        }
    }

    public void EquipCan()
    {
        isEquipped = true;
        // rb.isKinematic = true;
        // transform.SetParent(playerHand.transform);
        // transform.localPosition = Vector3.zero;
        // transform.localRotation = Quaternion.identity;
    }
    public void Sprinkle()
    {
        Instantiate(sprinklesPrefab, sprinkleSpawnPoint.position, Quaternion.identity);
        animator.SetTrigger("Sprinkle");
    }
}
