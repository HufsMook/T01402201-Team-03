using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss1Manager : MonoBehaviour
{
    private int nextPattern = 0;
    private int direction = -1;
    private Rigidbody2D rigid2D;
    private Animator animator;
    private GameObject player;
    private bool IsAttack;
    private Color originalColor;
    private SpriteRenderer spriteRenderer;

    private static readonly int JUMP = 1;
    private static readonly int SCRATCH = 2;
    //private static readonly int BULLET = 3;

    public GameObject shadowPrefab;
    public GameObject scratchPrefab;

    private int Hp = 40;
    public float attackRate = 2f;
    GameObject port;
    public UIdirector uid;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Painter");
        this.rigid2D = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
        StartCoroutine(jump());
        InvokeRepeating("UpdatePlayerPosition", 5f, 5f);
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;

        port = GameObject.Find("port");
        port.SetActive(false);

    }

    // 플레이어의 위치를 업데이트하는 함수
    void UpdatePlayerPosition()
    {
        if (player != null)
        {
            // 플레이어의 현재 위치를 가져옵니다.
            Vector3 playerPosition = player.transform.position;

            // 여기서 플레이어 위치를 사용하여 원하는 작업을 수행할 수 있습니다.
            // 예를 들어, 플레이어의 위치를 다른 오브젝트에 전달하거나 플레이어와의 거리를 계산할 수 있습니다.
            // 이 예시에서는 플레이어의 위치를 디버그 로그에 출력합니다.
            Debug.Log("Player position: " + playerPosition);
        }
    }

    void nextPatternPlay()
    {
        switch (nextPattern)
        {
            case 1:
                StartCoroutine(jump());
                break;
            case 2:
                StartCoroutine(scratch());
                break;
        }
    }
    IEnumerator jump()
    {
        if (!IsAttack)
        {
            UpdatePlayerPosition();
            animator.Play("Snowman_attack2");

            yield return new WaitForSeconds(1);
            this.transform.position = new Vector3(player.transform.position.x, 20, transform.position.z);


            nextPattern = SCRATCH;
            yield return new WaitForSeconds(3f);
            nextPatternPlay();
        }
    }

    IEnumerator scratch()
    {
        yield return new WaitForSeconds(5f);
        if (!IsAttack) // IsAttack이 False일 때만 실행
        {
            IsAttack = true;
            UpdatePlayerPosition();
            animator.Play("Snowman_attack1");

            yield return new WaitForSeconds(0.5f);
            GameObject scratch;

            scratch = Instantiate(scratchPrefab, new Vector3(player.transform.position.x + 2, -1, -3), Quaternion.Euler(0f, 20f, 0f)) as GameObject; //스크래치 생성 부분
            scratch.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);

            yield return new WaitForSeconds(0.5f);
            Destroy(scratch);

            IsAttack = false;
            nextPattern = JUMP;

            yield return new WaitForSeconds(3f);
            nextPatternPlay();
        }
    }

    /*IEnumerator shot()
    {
        Debug.Log("kk");
        timeAfterAttack = 0f;
        timeAfterAttack += Time.deltaTime;

        if (timeAfterAttack >= attackRate)
        {
            timeAfterAttack = 0f;
            GameObject bullet;

            bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.transform.localScale = new Vector3(5, 5, 5);
        }

        nextPattern = JUMP;
        yield return new WaitForSeconds(3f);
        nextPatternPlay();
    }*/

    void OnTriggerEnter2D(Collider2D other)
    {
        // 타격 이벤트 처리 코드
        Debug.Log("타격 !");

        // spriteRenderer 초기화 및 null 체크
        if (other.CompareTag("playerAttackEffect") && spriteRenderer != null)
        {
            Debug.Log("타격확인!");
            // 보스 스프라이트의 색을 흰색으로 변경
            spriteRenderer.color = Color.red;

            // 0.1초 후에 보스 스프라이트의 색을 원래 색으로 돌려줌
            StartCoroutine(ResetSpriteColor(0.1f));
            Hp -= 1;
            if (uid.painter.toLife < 5)
            {
                uid.painter.toLife++;
            }
        }
    }


    IEnumerator ResetSpriteColor(float delay)
    {
        yield return new WaitForSeconds(delay);

        // 보스 스프라이트의 색을 원래 색으로 돌려줌
        spriteRenderer.color = originalColor;
    }

    void Update()
    {
        if (Hp <= 0)
        {
            Vector3 portPos = new Vector3(transform.position.x, -2.95f, 0);
            StartCoroutine(FadeOutObject());
            Debug.Log("aawkedmqwlfeknmwlkfmlk");
            port.SetActive(true);
            port.transform.position = portPos;

            Debug.Log("nextScene");
        }
    }
    public float fadeDuration = 2f;
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
}