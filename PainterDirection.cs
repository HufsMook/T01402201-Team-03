using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PainterDirection : MonoBehaviour
{
    public float speed = 5f; // 플레이어 이동 속도
    public SpriteRenderer spriteRenderer; // 스프라이트 렌더러 컴포넌트
    private Animator animator;
    private bool isKeyDown = false;
    public float jumpForce = 5f; // 점프 힘
    private bool isJumping = false;
    public float jumpForceMultiplier = 1f; // 키를 누른 시간에 곱해지는 점프 힘 계수
    private float jumpStartTime; // 점프 시작 시간을 저장하는 변수
    private Rigidbody2D rb;
    public float groundDistance = 1f; // 땅과의 거리 체크를 위한 변수
    private bool isAttacking = false;
    private bool isGettingLife = false;
    private float getLifeStartTime;
    private float gettingLifeTime = 0.8f;
    [SerializeField]
    private GameObject attackEffectPrefab;
    private GameObject attackEffectInstance;
    private float destroyDelay = 0.1f;


    public int HP = 5;
    public int toLife = 5;
    private bool ishit = false;
    public Color originalColor;
    public float blinkingDuration = 0.3f; // 깜빡이는 시간
    public float blinkingInterval = 0.05f; // 깜빡이는 간격

    private bool isBlinking = false; // 깜빡이는 중인지 여부
    private Renderer renderer; // 렌더러 컴포넌트
    private CapsuleCollider2D playerCollider;
    public AudioClip attackBGM, getLifeBGM;

    private void run()
    {
        if (isKeyDown && !animator.GetCurrentAnimatorStateInfo(0).IsName("Painter_attack") && Input.GetKeyDown(KeyCode.LeftAlt))
        {
            animator.Play("Painter_jump");
            return; // jump 애니메이션 실행 후 함수 종료
        }

        float horizontalInput = 0f;
        if (Input.GetKey(KeyCode.LeftArrow)) horizontalInput = -1f;
        else if (Input.GetKey(KeyCode.RightArrow)) horizontalInput = 1f;

        // 플레이어 이동
        Vector3 movement = new Vector3(horizontalInput, 0f, 0f) * speed * Time.deltaTime;
        transform.position += movement;

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            // 방향키를 누르는 순간 처리
            isKeyDown = true;
            animator.Play("Painter_run_1");
        }
        else if (isKeyDown && ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))))
        {
            animator.Play("Painter_run_2");
        }
        else if (isKeyDown && ((Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))))
        {
            // 방향키를 떼는 순간 처리
            isKeyDown = false;
            animator.Play("Painter_run_3");
        }

        // 스프라이트 반전
        if (horizontalInput < 0) spriteRenderer.flipX = true;
        else if (horizontalInput > 0) spriteRenderer.flipX = false;
    }

    public void jump()
    {
        // 점프 키 입력을 감지
        if (isAttacking)
        {
            Debug.Log("hi");
            return;
        }
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            if (isGrounded())
            {
                Debug.Log("ddd");
                animator.Play("Painter_jump");
                isJumping = true;
                jumpStartTime = Time.time;
                isKeyDown = false; // run 애니메이션 실행 중단
            }
        }

        // 점프 중인 동안 키를 누르고 있는지 체크하여 점프 힘을 조정
        if (isJumping && Input.GetKey(KeyCode.LeftAlt))
        {
            animator.Play("Painter_jump");
            float jumpTime = Time.time - jumpStartTime;
            if (jumpTime > 0.3) isJumping = false;
            else
            {
                float currentJumpForce = jumpForce + (jumpTime * jumpForceMultiplier);
                rb.velocity = new Vector2(rb.velocity.x, currentJumpForce);
            }
        }

        if (Input.GetKeyUp(KeyCode.LeftAlt))
        {
            if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)))
            {
                animator.Play("Painter_run_2");
                isKeyDown = true;
            }
        }

        if (isGrounded() && isJumping)
        {
            animator.Play("Painter_stand");
        }
    }

    private bool isGrounded()
    {
        if (SceneManager.GetActiveScene().name == "Exhibition" && -3 < transform.position.y && transform.position.y < -2.9) return true;
        else if (-2.65 < transform.position.y && transform.position.y < -2.62)
        {
            return true;
        }
        return false;
    }



    public void attack()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            animator.Play("Painter_attack");
            AudioSource.PlayClipAtPoint(attackBGM, transform.position);
            isKeyDown = false;
            isAttacking = true;

            Quaternion newRotation;
            if (spriteRenderer.flipX)
            {
                attackEffectInstance = Instantiate(attackEffectPrefab, new Vector3(transform.position.x - 1.5f, transform.position.y, transform.position.z), Quaternion.identity);
                newRotation = Quaternion.Euler(50f, 0f, 240f);
            }
            else
            {
                attackEffectInstance = Instantiate(attackEffectPrefab, new Vector3(transform.position.x + 1.5f, transform.position.y, transform.position.z), Quaternion.identity);
                newRotation = Quaternion.Euler(50f, 0f, 60f);
            }
            attackEffectInstance.transform.rotation = newRotation;
            Destroy(attackEffectInstance, destroyDelay);
        }
        else if (isAttacking && !animator.GetCurrentAnimatorStateInfo(0).IsName("Painter_attack"))
        {
            if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)))
            {
                animator.Play("Painter_run_2");
                isKeyDown = true;
            }
            isAttacking = false;
        }
    }

    public void getLife()
    {
        if (toLife == 5 && Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("life");

            isGettingLife = true;
            getLifeStartTime = Time.time;
            animator.Play("Painter_getlife");
            AudioSource.PlayClipAtPoint(getLifeBGM, transform.position);
        }

        if (isGettingLife && Input.GetKey(KeyCode.Z))
        {
            float getLifeTime = Time.time - getLifeStartTime;
            if (gettingLifeTime < getLifeTime) // Z 키를 2초간 누르고 있으면
            {
                isGettingLife = false; // 애니메이션 재생 후 isGettingLife 값을 false로 설정
                if (HP < 5) HP++;
                Debug.Log("getLife end");
                toLife = 0;
            }
        }

        if (Input.GetKeyUp(KeyCode.Z) && animator.GetCurrentAnimatorStateInfo(0).IsName("Painter_getlife"))
        {
            Debug.Log("Z key released");
            isGettingLife = false;
            toLife = 0;
            animator.Play("Painter_stand");
        }
    }

    private void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        originalColor = spriteRenderer.color;
        renderer = GetComponent<Renderer>();
        playerCollider = GetComponent<CapsuleCollider2D>();
    }

    private void Update()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Painter_attack"))
        {
            jump();
            attack();

            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Painter_stand") || animator.GetCurrentAnimatorStateInfo(0).IsName("Painter_run_3") || isGettingLife)
            {
                getLife(); // getLife() 함수 호출
            }
        }

        run();

        if (HP < 0)
        {
            SceneManager.LoadScene("Dead End");
        }
    }

    //painter Damege
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("bossAttack") && !ishit)
        {
            isHitted();
            if (0 < HP) HP--;
            spriteRenderer.color = Color.red;
            StartCoroutine(ResetSpriteColor(0.1f));

            ishit = true;
            StartCoroutine(DisableHitForDuration(1f));
        }
    }

    private bool invin = false;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("bossAttack") && !ishit)
        {
            isHitted();
            if (0 < HP) HP--;
            spriteRenderer.color = Color.red;
            if (other.gameObject.name == "WIL_atteck_effect2_1")
            {
                Destroy(other.gameObject);
                Debug.Log("hidddddddddddddd");
            }
            invin = true;

            // 0.1초 후에 보스 스프라이트의 색을 원래 색으로 돌려줌
            StartCoroutine(ResetSpriteColor(0.1f));

            ishit = true;
            StartCoroutine(DisableHitForDuration(1f));
        }
    }
    IEnumerator ResetSpriteColor(float delay)
    {
        yield return new WaitForSeconds(delay);

        // 보스 스프라이트의 색을 원래 색으로 돌려줌
        spriteRenderer.color = originalColor;
    }

    IEnumerator DisableHitForDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        ishit = false;
    }


    void isHitted()
    {
        if (isBlinking)
            return;
        isBlinking = true;

        StartCoroutine(BlinkingCoroutine());
    }

    private IEnumerator BlinkingCoroutine()
    {
        float elapsedTime = 0f;
        bool isVisible = true;

        while (elapsedTime < blinkingDuration)
        {
            // 투명도를 변경하여 깜빡이는 효과 생성
            if (isVisible)
                renderer.material.color = new Color(1f, 1f, 1f, 76f / 255f);
            else
                renderer.material.color = new Color(1f, 1f, 1f, 1f);

            isVisible = !isVisible;

            yield return new WaitForSeconds(blinkingInterval);

            elapsedTime += blinkingInterval;
        }

        // 깜빡이기가 끝났을 때 투명도를 원래 값으로 설정
        renderer.material.color = new Color(1f, 1f, 1f, 1f);

        isBlinking = false;
    }
}
