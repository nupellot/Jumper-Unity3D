using UnityEngine;

//эта строчка гарантирует что наш скрипт не завалится ести на плеере будет отсутствовать компонент Rigidbody
[RequireComponent(typeof(Rigidbody))]


public class movement4 : MonoBehaviour
{
    // т.к. логика движения изменилась мы выставили меньшее и более стандартное значение
    // public float speed = 5f;
    public float JumpForce = 30f;
    public float animationTime = 1;
    public float ForwardForce = 5;
    public float AdditionalPressure;

    //что бы эта переменная работала добавьте тэг "Ground" на вашу поверхность земли
    private bool _isGrounded;
    private Rigidbody _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }


    void FixedUpdate()
    {
        //обратите внимание что все действия с физикой
        //желательно делать в FixedUpdate, а не в Update
        // JumpLogic();

        // в даном случае допустимо использовать это здесь, но можно и в Update.
        // но раз уж вызываем здесь, то
        // двигать будем используя множитель fixedDeltaTimе
        // MovementLogic();
        discreteMovement();
    }

    private void discreteMovement() {
      if (_isGrounded) {
        // Vector3 PreviousVelocity = _rb.velocity;
        _rb.velocity = new Vector3(0, 0, 0);  // Сбрасываем скорость, чтобы остановить игрока.

        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) {
              _rb.AddForce(Vector3.up * JumpForce);  // Прыжок.
              if (Input.GetAxis("Horizontal") > 0) {
                _rb.AddForce(new Vector3(1, 0, 0) * ForwardForce);
                transform.rotation = Quaternion.Euler(0, 90, 0);
              }
              if (Input.GetAxis("Horizontal") < 0) {
                _rb.AddForce(new Vector3(-1, 0, 0) * ForwardForce);
                transform.rotation = Quaternion.Euler(0, 270, 0);
              }
              // _rb.AddForce(Vector3.up * JumpForce);
              // _rb.AddForce(new Vector3(1, 0, 0) * ForwardForce);

              if (Input.GetAxis("Horizontal") == 0) {
                if (Input.GetAxis("Vertical") > 0) {
                  _rb.AddForce(new Vector3(0, 0, 1) * ForwardForce);
                  transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                if (Input.GetAxis("Vertical") < 0) {
                  _rb.AddForce(new Vector3(0, 0, -1) * ForwardForce);
                  transform.rotation = Quaternion.Euler(0, 180, 0);
                }
              }
        }
        // float h = Input.GetAxis("Horizontal");  // Движение вдоль горизонтальной оси.
        // float v = Input.GetAxis("Vertical");  // Движение вдоль вертикальной оси.
        // Input.ResetInputAxes();
        //
        // // if (h * v == 0) {  // Вызывается движение только в одну сторону.
        // if (h != 0 || v != 0) {
        //   _rb.AddForce(Vector3.up * JumpForce);  // Прыжок.
        // }
        // if (h > 0) {
        //   _rb.AddForce(new Vector3(1, 0, 0) * ForwardForce);
        //   transform.rotation = Quaternion.Euler(0, 90, 0);
        // } else if (h < 0) {
        //   _rb.AddForce(new Vector3(-1, 0, 0) * ForwardForce);
        //   transform.rotation = Quaternion.Euler(0, 270, 0);
        // } else if (v > 0) {
        //   _rb.AddForce(new Vector3(0, 0, 1) * ForwardForce);
        //   transform.rotation = Quaternion.Euler(0, 0, 0);
        // } else if (v < 0) {
        //   _rb.AddForce(new Vector3(0, 0, -1) * ForwardForce);
        //   transform.rotation = Quaternion.Euler(0, 180, 0);
        // }

      } else if (!_isGrounded) {
          // if (_rb.velocity.y < 0) {
            _rb.AddForce(Vector3.down * AdditionalPressure);
          // }
      }
        // }
        //
        // if (h * v != 0) {
        //
        // }



    }

    // private void MovementLogic()
    // {
    //     float moveHorizontal = Input.GetAxis("Horizontal");
    //     float moveVertical = Input.GetAxis("Vertical");
    //     Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
    //
    //     // что бы скорость была стабильной в любом случае
    //     // и учитывая что мы вызываем из FixedUpdate мы умножаем на fixedDeltaTimе
    //     transform.Translate(movement * speed * Time.fixedDeltaTime);
    // }

    private void JumpLogic()
    {
        if (Input.GetAxis("Jump") > 0)
        {
            if (_isGrounded)
            {
                // Обратите внимание что я делаю на основе Vector3.up а не на основе transform.up
                // если наш персонаж это шар -- его up может быть в том числе и вниз и влево и вправо.
                // Но нам нужен скачек только вверх! Потому и Vector3.up
                _rb.AddForce(Vector3.up * JumpForce);
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        IsGroundedUpate(collision, true);
    }

    void OnCollisionExit(Collision collision)
    {
        IsGroundedUpate(collision, false);
    }

    private void IsGroundedUpate(Collision collision, bool value)
    {
        if (collision.gameObject.tag == ("Ground"))
        {
            _isGrounded = value;
        }
    }
}
