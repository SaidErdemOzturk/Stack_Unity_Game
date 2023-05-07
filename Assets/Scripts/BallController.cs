using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float jumpSpeed;
    public GameObject speedEffect;
    public ScoreManager scoreManager;
    private GameObject tempEffect;
    private Rigidbody rigidbody;
    private bool mouseX;
    private bool control = true;
    private GameManager gameManager;
    private float currentTime;
    private bool speedAbility;
    private SoundManager soundManager;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        gameManager = FindObjectOfType<GameManager>();
        soundManager = FindObjectOfType<SoundManager>();
        PartController.OnBallHitSafe += DestroyObstacle;
        PartController.OnBallHitUnsafe += UnsafeCollision;
        PartController.OnBallHitFinish += FinishCollision;
        PartController.OnBallHitSpeed += DestroyObstacle;

    }
    private void OnDestroy()
    {
        PartController.OnBallHitSafe -= DestroyObstacle;
        PartController.OnBallHitUnsafe -= UnsafeCollision;
        PartController.OnBallHitFinish -= FinishCollision;
        PartController.OnBallHitSpeed -= DestroyObstacle;

    }

    private void FixedUpdate()
    {
        if (mouseX)
        {
            rigidbody.velocity = -Vector3.up * Time.deltaTime * 200;
            currentTime += Time.deltaTime * 0.8F;
        }
        else
        {
            currentTime -= Time.deltaTime * 0.5F;
        }
    }
    void Update()
    {
        scoreManager.circleSlider.fillAmount = currentTime;
        if (Input.GetMouseButtonDown(0))
        {
            mouseX = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            mouseX = false;
        }
        if (currentTime >= 1)
        {
            currentTime = 1;
            speedAbility = true;
            speedEffect.SetActive(true);
        }
        if(currentTime<=0)
        {
            currentTime = 0;
            speedAbility = false;
            speedEffect.SetActive(false);
        }
        if (speedAbility)
        {
            currentTime -= Time.deltaTime * 1F;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!mouseX)
        {
            if (control)
            {
                rigidbody.velocity = Vector3.up * Time.deltaTime*200;
                StartCoroutine(jumpCoolDown(0.001F));
            }
        }
        else
        {
            IPart part = collision.gameObject.GetComponent<IPart>();

            if (part != null)
            {
                if (control)
                {
                    if (speedAbility)
                    {
                        part.OnBallSpeedHitWithClick();
                    }
                    else
                    {
                        part.OnBallHitWithClick();
                    }
                }
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (!mouseX)
        {
            if (control)
            {
                rigidbody.velocity = Vector3.up * Time.deltaTime*200;

                StartCoroutine(jumpCoolDown(0.001F));
            }
        }
        else
        {
            IPart part = collision.gameObject.GetComponent<IPart>();
            if (part != null)
            {
                if (control)
                {
                    if (speedAbility)
                    {
                        part.OnBallSpeedHitWithClick();
                    }
                    else
                    {
                        part.OnBallHitWithClick();

                    }
                }
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            StartCoroutine(ScaleObstacle(other.gameObject));
        }
    }

    public void FinishCollision(bool control)
    {
        if (control)
        {
            gameManager.WinLevel();
        }
    }
    public void UnsafeCollision(bool control)
    {
        if (control)
        {
            gameManager.LoseLevel();
        }
    }
    public void DestroyObstacle(ObstacleController obstacleController)
    {
        gameManager.NextObstacle(currentTime);
        obstacleController.DestroyParts();
        StartCoroutine(jumpCoolDown(0.001F));
    }


    IEnumerator jumpCoolDown(float time)
    {
        soundManager.Play(AudioType.Jump);
        control = false;
        yield return new WaitForSecondsRealtime(time);
        control = true;
    }

    IEnumerator ScaleObstacle(GameObject obstacle)
    {
        obstacle.GetComponent<BoxCollider>().enabled = false;
        obstacle.transform.localScale = new Vector3(1.2F, 1.2F, 1.2F);
        yield return new WaitForSecondsRealtime(0.2F);
        obstacle.transform.localScale = new Vector3(1F, 1F, 1F);

    }
}
