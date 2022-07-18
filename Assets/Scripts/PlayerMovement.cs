using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    bool movingRight = true;
    public float movementSpeed = 2.5f;
    private float maxSpeed = 10;

    private GameManager gameManager;
    private Animator animator;
    private AudioSource audioSource;
    public AudioClip collecetibleAudio;
    public GameObject effect;

    [SerializeField] private int triggersToIgnore;
    [SerializeField] private int currentIgnoredTriggers = 0;


    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        if (!gameManager.gameStarted)
        {
            return;
        }
        else
        {
            animator.SetTrigger("game_started");
        }


        transform.position +=  transform.forward * movementSpeed * Time.deltaTime;

        

        if (!GroundCheck())
        {
            animator.SetTrigger("not_on_ground");
        }
        else
        {
            animator.SetTrigger("on_ground");
        }

        if (transform.position.y < -7 )
        {
            gameManager.RestartGame();
        }
    }

    private void Update()
    {
        if (!gameManager.gameStarted)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            TurnDirection();
        }

        if (movementSpeed < maxSpeed)
        {
            movementSpeed += Time.deltaTime * 0.1f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("TriggerPlane"))
        {
            if (currentIgnoredTriggers < triggersToIgnore)
            {
                currentIgnoredTriggers++;
                return;
            }
            else
            {
                gameManager.MoveBlock();
            }
        }

        else if (other.CompareTag("Collectible"))
        {
            Instantiate(effect, other.gameObject.transform.position,effect.transform.rotation);
            Destroy(other.gameObject);
            audioSource.PlayOneShot(collecetibleAudio);
            gameManager.IncreaseScore();
        }
    }

    private void TurnDirection()
    {
        movingRight = !movingRight;
        var rotationVector = transform.rotation.eulerAngles;
        rotationVector.y *= -1;
        transform.rotation = Quaternion.Euler(rotationVector);
    }

    private bool GroundCheck()
    {
        if (transform.position.y < 0.8f)
        {
            return false;
        }
        return true;
    }

}
