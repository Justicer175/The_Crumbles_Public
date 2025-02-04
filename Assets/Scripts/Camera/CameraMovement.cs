using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public GameObject followObject;

    public GameObject camera;

    public Vector2 followOffset;
    public float speed = 3f;
    private Vector2 threshold;
    private Rigidbody2D rb;


    private Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(-21.27f, 3.51f, -10);
        threshold = calculateThreshold();
        rb = followObject.GetComponent<Rigidbody2D>();
        //Debug.Log(gameObject.transform.position);
        //Debug.Log(camera.transform.forward);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        Vector2 follow = followObject.transform.position;
        float xDifference = Vector2.Distance(Vector2.right * transform.position.x, Vector2.right * follow.x);
        float yDifference = Vector2.Distance(Vector2.up * transform.position.y, Vector2.up * follow.y);

        Vector3 newPosition = transform.position;
        if (Mathf.Abs(xDifference) >= threshold.x)
        {
            newPosition.x = follow.x;
        }

        if (Mathf.Abs(yDifference) >= threshold.y)
        {
            newPosition.y = follow.y;
        }
        float moveSpeed = rb.velocity.magnitude > speed ? rb.velocity.magnitude : speed;


        if(followObject.transform.position.x >= -16f && followObject.transform.position.x < 94.45f)
        {

 
            transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, speed);
            
        }
        else if (followObject.transform.position.x >= 94.45f)
        {
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(90f, 3.51f, -10), ref velocity, speed);
        }
        else if (transform.position.x > -16f )
        {

            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(-21.27f, 3.51f, -10), ref velocity, speed);
            //transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, speed);
        }
        
        else
        {
            //transform.position = Vector3.SmoothDamp(transform.position, new Vector3(-25f, 2.7f, -10f), ref velocity, speed);
            //transform.position = new Vector3(-21.27f, 3.51f, -10);
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(-21.27f, 3.51f, -10), ref velocity, speed);
            
        }
        //Debug.Log(gameObject.transform.position);
        //Debug.Log(followObject.transform.position);


    }
    private Vector3 calculateThreshold()
    {
        Rect aspect = Camera.main.pixelRect;
        Vector2 t = new Vector2(Camera.main.orthographicSize * aspect.width / aspect.height, Camera.main.orthographicSize);
        t.x -= followOffset.x;
        t.y -= followOffset.y;
        return t;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Vector2 border = calculateThreshold();
        Gizmos.DrawWireCube(transform.position, new Vector3(border.x * 2, border.y * 2, 1));
    }
}
