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
    public AudioClip JumpSound;
    private bool _isGrounded;
    private Rigidbody _rb;
    // private AudioSource dynamic;
    // private int CurrentZPosition = 0;
    // public AudioSource music;
    // public AudioClip jump;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        PlayerPrefs.SetInt("CurrentZPosition", 0);
        PlayerPrefs.SetFloat("GapBetweenZeroAndPlayer", 1.05f);
        // dynamic = GetComponent<AudioSource>();
    }
    // private void Awake()
    // 	{
    //                  // Добавляем компонент AudioSource к объекту
    //         music = gameObject.AddComponent<AudioSource>();
    //                  // Воспроизвести звуковые эффекты в начале
    //         music.playOnAwake = false;
    //                  // Загружаем файл звукового эффекта, я назвал скачущий аудиофайл jump
    //         jump = Resources.Load<AudioClip>("JumpSound.wav");
    //     }

    void FixedUpdate()
    {
        DiscreteMovement();
        UpdateRecord();
    }

    private void DiscreteMovement() {

        // Проверяем, находится ли игрок ниже определенного уровня (условно )
        if (transform.position.y - GetComponent<Renderer>().bounds.size.y / 2 <= PlayerPrefs.GetFloat("GapBetweenZeroAndPlayer")) {
            // Debug.Log("Is Grounded");
            _rb.velocity = new Vector3(0, 0, 0);  // Сбрасываем скорость, чтобы остановить игрока.
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) {  // Проиходит прыжок в одном из направлений.
                  _rb.AddForce(Vector3.up * JumpForce);  // Прыжок.
                  GetComponent<AudioSource>().PlayOneShot(JumpSound);
                  // music.clip = jump;
                  // music.Play();
                  if (Input.GetAxis("Horizontal") > 0) {
                      _rb.AddForce(new Vector3(1, 0, 0) * ForwardForce);
                      transform.rotation = Quaternion.Euler(0, 90, 0);
                  }
                  if (Input.GetAxis("Horizontal") < 0) {
                      _rb.AddForce(new Vector3(-1, 0, 0) * ForwardForce);
                      transform.rotation = Quaternion.Euler(0, 270, 0);
                  }
                  if (Input.GetAxis("Horizontal") == 0) {
                      if (Input.GetAxis("Vertical") > 0) {  // Это движение вперёд. (Вдоль оси Z)
                          if (PlayerPrefs.HasKey("CurrentZPosition")) {
                              PlayerPrefs.SetInt("CurrentZPosition", PlayerPrefs.GetInt("CurrentZPosition") + 1);
                          }
                          _rb.AddForce(new Vector3(0, 0, 1) * ForwardForce);
                          transform.rotation = Quaternion.Euler(0, 0, 0);
                          // CurrentZPosition += 1;
                      }
                      if (Input.GetAxis("Vertical") < 0) {
                          if (PlayerPrefs.HasKey("CurrentZPosition")) {
                              PlayerPrefs.SetInt("CurrentZPosition", PlayerPrefs.GetInt("CurrentZPosition") - 1);
                          }
                          _rb.AddForce(new Vector3(0, 0, -1) * ForwardForce);
                          transform.rotation = Quaternion.Euler(0, 180, 0);
                          // CurrentZPosition -= 1;
                      }
                  }
            }
        } else
        if (transform.position.y - GetComponent<Renderer>().bounds.size.y / 2 > PlayerPrefs.GetFloat("GapBetweenZeroAndPlayer")) {
            // Debug.Log("Not Grounded");
            _rb.AddForce(Vector3.down * AdditionalPressure);
            if (_rb.velocity.y > 0) {
                Input.ResetInputAxes();
            }
        }
    }



    private void UpdateRecord() {
        if (PlayerPrefs.GetInt("CurrentZPosition") > PlayerPrefs.GetInt("Record")) {
            PlayerPrefs.SetInt("Record", PlayerPrefs.GetInt("CurrentZPosition"));
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
