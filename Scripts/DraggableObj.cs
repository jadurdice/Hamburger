using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class DraggableObj : MonoBehaviour ,IDragHandler,IBeginDragHandler,IEndDragHandler
{
    public GameObject Hamburger;


    public GameObject bottom;
    public GameObject center;
    public GameObject top;

    public float length;

    public GameObject actBurger;
    public GameObject bottomDragging;
    public GameObject centerDragging;
    public GameObject topDragging;

    public float moveSpeed;

    private void Awake()
    {

    }

    private void Update()
    {

        
    }



    public void OnBeginDrag(PointerEventData eventData)
    {

        actBurger = Instantiate(Hamburger, this.transform);

        topDragging = Instantiate(top, actBurger.transform);
        centerDragging  = Instantiate(center, actBurger.transform);
        bottomDragging = Instantiate(bottom, actBurger.transform);

        Hamburger.GetComponent<HamburgerControl>().thisBottom = bottomDragging;
        Hamburger.GetComponent<HamburgerControl>().thisCenter = centerDragging;
        Hamburger.GetComponent<HamburgerControl>().thisTop    = topDragging;

        topDragging.GetComponent<BunsControl>().center = centerDragging;

        bottomDragging.transform.position = eventData.position;
        bottomDragging.GetComponent<BunsControl>().center = centerDragging;
    }

    public void OnDrag(PointerEventData eventData)
    {
        float targetAng = Mathf.Atan2(topDragging.transform.position.y - bottomDragging.transform.position.y, topDragging.transform.position.x - bottomDragging.transform.position.x);

        centerDragging.transform.position = Vector2.Lerp(bottomDragging.transform.position, topDragging.transform.position, 0.5f);

        topDragging.transform.position = Vector3.Lerp(bottomDragging.transform.position,eventData.position,length);
        topDragging.transform.eulerAngles = (new Vector3(0.0f, 0.0f, Mathf.Rad2Deg * targetAng - 90.0f));

        bottomDragging.transform.eulerAngles = (new Vector3(0.0f, 0.0f, Mathf.Rad2Deg * targetAng - 90.0f));

    }
    public void OnEndDrag(PointerEventData eventData)
    {
        topDragging.GetComponent<Rigidbody2D>().simulated = true;
        topDragging.GetComponent<Rigidbody2D>().freezeRotation = true;
        topDragging.GetComponent<BunsControl>().isEndDrag = true;
        topDragging.GetComponent<BunsControl>().moveSpeed = moveSpeed;

        bottomDragging.GetComponent<BunsControl>().isEndDrag = true;
        bottomDragging.GetComponent<Rigidbody2D>().simulated = true;
        bottomDragging.GetComponent<Rigidbody2D>().freezeRotation = true;
        bottomDragging.GetComponent<BunsControl>().moveSpeed = moveSpeed;
    }
}


