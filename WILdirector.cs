using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WILdirector : MonoBehaviour
{
    public int WIL_HP = 1;
    private GameObject player;
    [SerializeField]
    private PainterDirection painter;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Vector3 originalScale;
    private Rigidbody2D rb;
    private int attackCount = 0;
    public float moveSpeed = 2f;
    private Color originalColor;
    private bool canMove = true;
    private float disableMoveTime = 5f;
    public GameObject laserPrefab;
    public float minX = -9f;
    public float maxX = 9f;
    public float minY = -3f;
    public float maxY = 5f;
    private GameObject[] laserArr = new GameObject[5];
    private int laserIndex = 0;
    [SerializeField]
    private GameObject attackEffectPrefab;
    private int i;
    [SerializeField]
    public UIdirector uid;
    public float fadeDuration = 2f;
    GameObject port;
    // Start is called before the first frame update
    void Start()
    {
        originalScale = transform.localScale;
        player = GameObject.Find("Painter");
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("CheckPlayerPosition", 0f, 2f);
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        capsuleCollider = GetComponent<CapsuleCollider2D>();

        port = GameObject.Find("port4");
        port.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        float playerX = player.transform.position.x;

        if (-9f < playerX && playerX < -8f)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) && canMove)
            {
                player.transform.position = new Vector3(9.3f, player.transform.position.y, 0f);
                DisableMovement();
            }
        }

        if (8f < playerX && playerX < 9f)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) && canMove)
            {
                player.transform.position = new Vector3(-8.5f, player.transform.position.y, 0f);
                DisableMovement();
            }
        }
    }

    private void DisableMovement()
    {
        canMove = false;
        StartCoroutine(EnableMovementAfterDelay(disableMoveTime));
    }

    private IEnumerator EnableMovementAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        canMove = true;
    }

    public CapsuleCollider2D capsuleCollider;
    void CheckPlayerPosition()
    {
        if (player.transform.position.x < transform.position.x)
        {
            spriteRenderer.flipX = false;
            capsuleCollider.offset = new Vector2(0.3f, capsuleCollider.offset.y);
        }
        else
        {
            spriteRenderer.flipX = true;
            capsuleCollider.offset = new Vector2(-0.3f, capsuleCollider.offset.y);
        }


        attackCount++;
        if (attackCount > 4)
        {
            animator.Play("WIL_attack2");
            attackCount = 0;
            StartCoroutine(ExecuteBossPattern()); // 보스 패턴 실행
            laserIndex = 0;
            return;
        }

        if (player != null)
        {
            float distance = player.transform.position.x - transform.position.x;

            if (Mathf.Abs(distance) > 5f)
            {
                animator.Play("WIL_walk");

                // 플레이어 위치 쪽으로 x축 이동
                float targetX = player.transform.position.x - Mathf.Sign(distance) * 4f;
                targetX = Mathf.Clamp(targetX, player.transform.position.x - 4f, player.transform.position.x + 4f);

                Vector3 targetPosition = new Vector3(targetX, transform.position.y, transform.position.z);
                Vector3 moveDirection = (targetPosition - transform.position).normalized;
                rb.AddForce(moveDirection * moveSpeed, ForceMode2D.Force);
            }
            else if (Mathf.Abs(distance) <= 5f)
            {
                // 플레이어의 위치와 보스의 위치 차이가 5 이하일 때
                animator.Play("WIL_attack");

                // 공격 이펙트 생성
                StartCoroutine(SpawnAttackEffect());
            }

        }
    }
     IEnumerator SpawnAttackEffect()
    {
        yield return new WaitForSeconds(0.6f); // 이펙트 생성 지연 시간
        GameObject attackEffect;
        if (!spriteRenderer.flipX)
        {
            float spawnX = transform.position.x - 1.5f;
            float spawnY = -2.4f;
            Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0f);

            // 이펙트 생성
            Quaternion spawnRotation = Quaternion.Euler(0f, 0f, 180f);
            attackEffect = Instantiate(attackEffectPrefab, spawnPosition, spawnRotation);

            // 이펙트 이동 및 제거
            float speed = 5f; // 이펙트의 이동 속도
            Vector3 targetPosition = spawnPosition + new Vector3(10f, 0f, 0f); // 목표 이동 위치
            while (attackEffect.transform.position.x < targetPosition.x)
            {
                attackEffect.transform.Translate(Vector3.right * speed * Time.deltaTime);
                yield return null;
            }
        }
        else
        {
            float spawnX = transform.position.x + 1.5f;
            float spawnY = -2.4f;
            Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0f);

            // 이펙트 생성
            attackEffect = Instantiate(attackEffectPrefab, spawnPosition, Quaternion.identity);

            // 이펙트 이동 및 제거
            float speed = 5f; // 이펙트의 이동 속도
            Vector3 targetPosition = spawnPosition + new Vector3(10f, 0f, 0f); // 목표 이동 위치
            while (attackEffect.transform.position.x < targetPosition.x)
            {
                attackEffect.transform.Translate(Vector3.right * speed * Time.deltaTime);
                yield return null;
            }
        }
        Destroy(attackEffect);
    }

    IEnumerator ExecuteBossPattern()
    {
        laserIndex = 0;
        for (i = 0; i < 5; i++)
        {
            SpawnLaser();
            yield return new WaitForSeconds(0.25f);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("playerAttackEffect"))
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("WIL_attack2")) return; // WIL_attack2 애니메이션 상태일 때는 타격 판정을 처리하지 않음
            WIL_HP--;
            if (WIL_HP < 0)
            {
                Vector3 portPos = new Vector3(transform.position.x, -2.95f, 0);
                StartCoroutine(FadeOutObject());
                port.SetActive(true);
                port.transform.position = portPos;
            }
            spriteRenderer.color = Color.red;

            // 0.1초 후에 보스 스프라이트의 색을 원래 색으로 돌려줌
            StartCoroutine(ResetSpriteColor(0.1f));
            if (uid.painter.toLife < 5)
            {
                uid.painter.toLife++;
            }
        }
    }

    IEnumerator FadeOutObject()
    {
        float currentTime = 0f;
        Color originalColor = spriteRenderer.color;

        while (currentTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, currentTime / fadeDuration);
            Color newColor = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            spriteRenderer.color = newColor;

            currentTime += Time.deltaTime;
            yield return null;
        }

        // 오브젝트를 완전히 사라지게 처리
        spriteRenderer.enabled = false;
        Destroy(gameObject);
    }

    IEnumerator ResetSpriteColor(float delay)
    {
        yield return new WaitForSeconds(delay);
        // 보스 스프라이트의 색을 원래 색으로 돌려줌
        spriteRenderer.color = originalColor;
    }

    void SpawnLaser()
    {
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        Vector3 spawnPosition = new Vector3(randomX, randomY, 0f);
        Quaternion spawnRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));

        GameObject laser = Instantiate(laserPrefab, spawnPosition, spawnRotation);
        laser.GetComponent<Laser>().wil = this; // wil 변수 할당

        laserArr[laserIndex++] = laser;

        // 각 레이저마다 사라질 시간 계산
        float destroyTime = 0.75f + (6 - laserIndex) * 0.25f;
        if (i == 0) destroyTime += 0.25f;
        Destroy(laser, destroyTime);
    }
}
