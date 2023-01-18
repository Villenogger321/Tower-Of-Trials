using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchLighter : MonoBehaviour
{
    [SerializeField] float lightDistance;
    Transform torchParent;
    List<Torch> unlitTorches = new List<Torch>();
    void Start()
    {
        torchParent = GameObject.Find("Torches").transform;
        unlitTorches.AddRange(torchParent.GetComponentsInChildren<Torch>());
    }

    void FixedUpdate()
    {
        Collider2D[] closeColliders = Physics2D.OverlapCircleAll(transform.position, lightDistance);
        foreach (Collider2D col in closeColliders)
        {
            if (col.GetComponent<Torch>() is Torch torch)
            {
                torch.LightTorch();
                unlitTorches.Remove(torch);
            }
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, lightDistance);
    }
}
