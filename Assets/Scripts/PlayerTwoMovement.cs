using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTwoMovement : MonoBehaviour
{
    [SerializeField] GameObject player;
     Rigidbody2D rb;
     float playerSpeed = 30f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
             rb.MovePosition(transform.position + new Vector3(0, 1, 0) * playerSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
             rb.MovePosition(transform.position + new Vector3(0, 1, 0) * -playerSpeed * Time.deltaTime);
        }
   }
}
