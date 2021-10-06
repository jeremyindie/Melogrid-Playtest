using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public bool isActive;
    public float moveSpeed = 5f;
    public Transform movePoint;
    public LayerMask whatStopsMovement;

    public List<string> moveList;
    // Start is called before the first frame update
    void Start()
    {
        movePoint.parent = null;
        isActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
        if (!isActive)
        {
            return;
        }
        Move();
    }
    private void Move()
    {
        
        if (Vector3.Distance(transform.position, movePoint.position) <= 0.05f)
        {
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
            {
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), .2f, whatStopsMovement))
                {
                    movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
                    if (Input.GetAxisRaw("Horizontal") > 0)
                    {
                        moveList.Add("R");
                        FindObjectOfType<AudioManager>().Play("RightSound");
                    }
                    else
                    {
                        moveList.Add("L");
                        FindObjectOfType<AudioManager>().Play("LeftSound");
                    }
                }
            }
            else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
            {
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), .2f, whatStopsMovement))
                {
                    movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
                    if (Input.GetAxisRaw("Vertical") > 0)
                    {
                        moveList.Add("U");
                        FindObjectOfType<AudioManager>().Play("UpSound");
                    }
                    else
                    {
                        moveList.Add("D");
                        FindObjectOfType<AudioManager>().Play("DownSound");
                    }
                }
            }
        }
    }
}
