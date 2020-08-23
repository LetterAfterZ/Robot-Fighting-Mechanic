using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private Rigidbody2D _rb2D = null;
    [SerializeField] private SpriteRenderer _spriteRenderer = null;
    [SerializeField] private Animator _animator = null;

    private Vector2 _movement;

    private bool _canMove = true;


    void Update() {
        if (_canMove)
            MovePlayer();

    }

    private void MovePlayer() {
        //get player input
        _movement = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));

        //set movement
        _animator.SetFloat("Horizontal",_movement.x);
        _animator.SetFloat("Vertical",_movement.y);
        _animator.SetFloat("Speed",_movement.sqrMagnitude);

        //manual sprite flipping
        SetSpriteFacing();

        if(Input.GetKey("space")) {
            //play attack animation
            _animator.SetBool("IsAttacking",true);
        } else {
            _animator.SetBool("IsAttacking",false);
        }
    }

    void FixedUpdate()
    {                
        //move towards target
        if (_canMove)
            _rb2D.MovePosition(_rb2D.position + _movement * _moveSpeed * Time.deltaTime);        
    }

    private void SetSpriteFacing() {
        if(_movement.x >= 0.01f) {
            _spriteRenderer.flipX = false;
        } else if(_movement.x <= -0.01f) {
            _spriteRenderer.flipX = true;
        }
    }

    public void SetPlayerMovement(bool setMovement){
        if (setMovement){
            _canMove = true;
        }else{            
            _canMove = false;
            _rb2D.MovePosition(_rb2D.position);
            _animator.SetFloat("Speed",0);
            _animator.SetBool("IsAttacking",true);
            
        }
    }
}
