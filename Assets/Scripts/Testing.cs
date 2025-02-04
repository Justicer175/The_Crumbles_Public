using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    public GameObject followPlayer;
    public Vector2 followOffset;
    public float speed = 3f;
    private Vector2 treshoald;
    private Rigidbody2D rb;


    private Transform player;
    public float smoothing;


    // Start is called before the first frame update
    void Start()
    {
        treshoald = calculate();

        rb = followPlayer.GetComponent<Rigidbody2D>();
        player = followPlayer.GetComponent<Transform>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector2 follow = followPlayer.transform.position;
        float xDiff = Vector2.Distance(Vector2.right * transform.position.x, Vector2.right * follow.x);
        float yDifference = Vector2.Distance(Vector2.up * transform.position.y, Vector2.up * follow.y);

        Vector3 newPosition = transform.position;

        if (Mathf.Abs(xDiff) >= treshoald.x)
        {
            newPosition.x = follow.x;
        }
        if (Mathf.Abs(yDifference) >= treshoald.y)
        {
            newPosition.y = follow.y;
        }
        float moveSpeed = rb.velocity.magnitude > speed ? rb.velocity.magnitude : speed;


       // if ((player.position.x > -13.8 && player.position.x < 12.7) || (transform.position.x > -13.8 && transform.position.x < 12.7))
       // {
            // transform.position = Vector3.MoveTowards(transform.position, newPosition, moveSpeed * Time.deltaTime);
            //transform.position = new Vector3(player.position.x, 1.58f, -10);
            transform.position = Vector3.Lerp(transform.position, newPosition, smoothing * Time.deltaTime);
       // }



    }

    private Vector3 calculate()
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
        Vector2 border = calculate();
        Gizmos.DrawWireCube(transform.position, new Vector3(border.x * 2, border.y * 2, 1));
    }

}
