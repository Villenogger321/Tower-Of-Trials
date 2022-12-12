using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerOrderChecker : MonoBehaviour
{
    #region Variables
    [SerializeField] bool changeOnMove;
    [SerializeField] int layerOrder;

    SpriteRenderer spriteRenderer;
    #endregion

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        layerOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;
        spriteRenderer.sortingOrder = layerOrder;
    }
    void FixedUpdate()
    {
        if (changeOnMove)
        {
            layerOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;
            spriteRenderer.sortingOrder = layerOrder;   
        }
    }
}
