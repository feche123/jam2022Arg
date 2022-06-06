using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody2D rbPlayer;
    [SerializeField] GameObject playerGameObj;
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    int dobleJump;
   
    void Update()
    {
        Move();
        Jump();
    }

    void Move()
    {
        rbPlayer.velocity = new Vector2(Input.GetAxis("Horizontal")*Time.deltaTime*speed*100, rbPlayer.velocity.y);
    }
    void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && dobleJump < 2)
        {
            rbPlayer.velocity = new Vector2(rbPlayer.velocity.x,0);
            rbPlayer.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            dobleJump++;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("ground"))
        dobleJump = 0;
    }
}
