using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMusic : MonoBehaviour
{
    private static StartMusic instance;

    public AudioClip bgm; // 재생할 BGM AudioClip

    private AudioSource audioSource;
    private int previousSceneIndex;

    private bool isFirstEntry = true; // Scene 1에 처음 들어갔는지 여부를 나타내는 변수

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Scene 전환 시 파괴되지 않도록 설정
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = bgm;
        audioSource.loop = true;
        audioSource.Play();
    }

    private void Update()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (currentSceneIndex != previousSceneIndex)
        {
            if (currentSceneIndex != 0 && currentSceneIndex != 1)
            {
                audioSource.Pause();
            }
            else
            {
                if (currentSceneIndex == 1 && isFirstEntry)
                {
                    isFirstEntry = false;
                }
                else
                {
                    audioSource.Play();
                }
            }

            previousSceneIndex = currentSceneIndex;
        }
    }
}