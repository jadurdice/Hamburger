using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunsControl : MonoBehaviour
{
    public bool isEndDrag;

    public GameObject center;

    public float moveSpeed;

    private void Awake()
    {
        
    }

    private void Update()
    {
        if (isEndDrag)
        {
            this.transform.position = Vector2.Lerp(this.transform.position,center.transform.position,Time.deltaTime * moveSpeed);
        }
    }

    private void OnCollisionEnter2D(Collision2D collider)
    {
        if(collider.transform.tag == "Ingredient" && collider.transform.parent != this.transform)
        {
            collider.transform.SetParent(this.transform);
            Debug.Log("aaa");
        }
       

    }
}
