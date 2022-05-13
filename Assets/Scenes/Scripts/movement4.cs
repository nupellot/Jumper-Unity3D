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
    public float AdditionalPressure = 0;

    //что бы эта переменная работала добавьте тэг "Ground" на вашу поверхность земли
    private bool _isGrounded;
    private Rigidbody _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }


    void FixedUpdate()
    {
        discreteMovement();
    }

    private void discreteMovement() {
        if (this.transform.position.y - 0.1 <= this.GetComponent<Renderer>().bounds.size.y / 2) {
            Debug.Log("Is Grounded");
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
        } else
        if (this.transform.position.y - 0.1 >= this.GetComponent<Renderer>().bounds.size.y / 2) {
            Debug.Log("Not Grounded");
            _rb.AddForce(Vector3.down * AdditionalPressure);
        }
    }


    private void JumpLogic() {
        if (Input.GetAxis("Jump") > 0) {
            if (_isGrounded) {
                _rb.AddForce(Vector3.up * JumpForce);
            }
        }
    }

    // void OnCollisionEnter(Collision collision) {
    //     if (collision.gameObject.tag == "Ground") {
    //         _isGrounded = true;
    //     }
    //
    //     // IsGroundedUpate(collision, true);
    // }
    //
    // void OnCollisionExit(Collision collision) {
    //   if (collision.gameObject.tag == "Ground") {
    //       _isGrounded = false;
    //   }
    //     // IsGroundedUpate(collision, false);
    // }
    //
    // private void IsGroundedUpate(Collision collision, bool value) {
    //     if (collision.gameObject.tag == ("Ground")) {
    //         _isGrounded = value;
    //     }
    // }
}
