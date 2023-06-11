using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScene : MonoBehaviour
{
    public Text blinkingText;
    public float blinkSpeed = 1f;
    public string exhibitionSceneName = "Exhibition";

    private float timer = 0f;
    private bool isVisible = true;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= blinkSpeed)
        {
            isVisible = !isVisible;
            timer = 0f;
        }

        float alpha = isVisible ? 1f : 0f;
        Color textColor = blinkingText.color;
        textColor.a = Mathf.Lerp(textColor.a, alpha, Time.deltaTime * 10f);
        blinkingText.color = textColor;

        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene(exhibitionSceneName);
        }
    }
}


