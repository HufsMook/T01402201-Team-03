using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMusic : MonoBehaviour
{
    private static StartMusic instance;

    public AudioClip bgm; // ����� BGM AudioClip

    private AudioSource audioSource;
    private int previousSceneIndex;

    private bool isFirstEntry = true; // Scene 1�� ó�� ������ ���θ� ��Ÿ���� ����

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Scene ��ȯ �� �ı����� �ʵ��� ����
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