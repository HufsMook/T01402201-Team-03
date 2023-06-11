using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIdirector : MonoBehaviour
{
    [SerializeField]
    public PainterDirection painter;
    [SerializeField]
    GameObject UI0, UI1, UI2, UI3, UI4, UI5;
    [SerializeField]
    GameObject life1, life2, life3, life4, life5;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (painter.HP == 0)
        {
            SceneManager.LoadScene("Dead End");
        }
        if (painter.toLife == 0)
        {
            UI0.SetActive(true);
            UI1.SetActive(false);
            UI2.SetActive(false);
            UI3.SetActive(false);
            UI4.SetActive(false);
            UI5.SetActive(false);

            if (painter.HP == 1)
            {
                life1.SetActive(true);
                life2.SetActive(false);
                life3.SetActive(false);
                life4.SetActive(false);
                life5.SetActive(false);
            }

            else if (painter.HP == 2)
            {
                life1.SetActive(true);
                life2.SetActive(true);
                life3.SetActive(false);
                life4.SetActive(false);
                life5.SetActive(false);
            }

            else if (painter.HP == 3)
            {
                life1.SetActive(true);
                life2.SetActive(true);
                life3.SetActive(true);
                life4.SetActive(false);
                life5.SetActive(false);
            }

            else if (painter.HP == 4)
            {
                life1.SetActive(true);
                life2.SetActive(true);
                life3.SetActive(true);
                life4.SetActive(true);
                life5.SetActive(false);
            }

            else if (painter.HP == 5)
            {
                life1.SetActive(true);
                life2.SetActive(true);
                life3.SetActive(true);
                life4.SetActive(true);
                life5.SetActive(true);
            }
        }

        else if (painter.toLife == 1)
        {
            UI0.SetActive(false);
            UI1.SetActive(true);
            UI2.SetActive(false);
            UI3.SetActive(false);
            UI4.SetActive(false);
            UI5.SetActive(false);

            if (painter.HP == 1)
            {
                life1.SetActive(true);
                life2.SetActive(false);
                life3.SetActive(false);
                life4.SetActive(false);
                life5.SetActive(false);
            }

            else if (painter.HP == 2)
            {
                life1.SetActive(true);
                life2.SetActive(true);
                life3.SetActive(false);
                life4.SetActive(false);
                life5.SetActive(false);
            }

            else if (painter.HP == 3)
            {
                life1.SetActive(true);
                life2.SetActive(true);
                life3.SetActive(true);
                life4.SetActive(false);
                life5.SetActive(false);
            }

            else if (painter.HP == 4)
            {
                life1.SetActive(true);
                life2.SetActive(true);
                life3.SetActive(true);
                life4.SetActive(true);
                life5.SetActive(false);
            }

            else if (painter.HP == 5)
            {
                life1.SetActive(true);
                life2.SetActive(true);
                life3.SetActive(true);
                life4.SetActive(true);
                life5.SetActive(true);
            }
        }

        else if (painter.toLife == 2)
        {
            UI0.SetActive(false);
            UI1.SetActive(false);
            UI2.SetActive(true);
            UI3.SetActive(false);
            UI4.SetActive(false);
            UI5.SetActive(false);

            if (painter.HP == 1)
            {
                life1.SetActive(true);
                life2.SetActive(false);
                life3.SetActive(false);
                life4.SetActive(false);
                life5.SetActive(false);
            }

            else if (painter.HP == 2)
            {
                life1.SetActive(true);
                life2.SetActive(true);
                life3.SetActive(false);
                life4.SetActive(false);
                life5.SetActive(false);
            }

            else if (painter.HP == 3)
            {
                life1.SetActive(true);
                life2.SetActive(true);
                life3.SetActive(true);
                life4.SetActive(false);
                life5.SetActive(false);
            }

            else if (painter.HP == 4)
            {
                life1.SetActive(true);
                life2.SetActive(true);
                life3.SetActive(true);
                life4.SetActive(true);
                life5.SetActive(false);
            }

            else if (painter.HP == 5)
            {
                life1.SetActive(true);
                life2.SetActive(true);
                life3.SetActive(true);
                life4.SetActive(true);
                life5.SetActive(true);
            }
        }

        else if (painter.toLife == 3)
        {
            UI0.SetActive(false);
            UI1.SetActive(false);
            UI2.SetActive(false);
            UI3.SetActive(true);
            UI4.SetActive(false);
            UI5.SetActive(false);

            if (painter.HP == 1)
            {
                life1.SetActive(true);
                life2.SetActive(false);
                life3.SetActive(false);
                life4.SetActive(false);
                life5.SetActive(false);
            }

            else if (painter.HP == 2)
            {
                life1.SetActive(true);
                life2.SetActive(true);
                life3.SetActive(false);
                life4.SetActive(false);
                life5.SetActive(false);
            }

            else if (painter.HP == 3)
            {
                life1.SetActive(true);
                life2.SetActive(true);
                life3.SetActive(true);
                life4.SetActive(false);
                life5.SetActive(false);
            }

            else if (painter.HP == 4)
            {
                life1.SetActive(true);
                life2.SetActive(true);
                life3.SetActive(true);
                life4.SetActive(true);
                life5.SetActive(false);
            }

            else if (painter.HP == 5)
            {
                life1.SetActive(true);
                life2.SetActive(true);
                life3.SetActive(true);
                life4.SetActive(true);
                life5.SetActive(true);
            }
        }

        else if (painter.toLife == 4)
        {
            UI0.SetActive(false);
            UI1.SetActive(false);
            UI2.SetActive(false);
            UI3.SetActive(false);
            UI4.SetActive(true);
            UI5.SetActive(false);

            if (painter.HP == 1)
            {
                life1.SetActive(true);
                life2.SetActive(false);
                life3.SetActive(false);
                life4.SetActive(false);
                life5.SetActive(false);
            }

            else if (painter.HP == 2)
            {
                life1.SetActive(true);
                life2.SetActive(true);
                life3.SetActive(false);
                life4.SetActive(false);
                life5.SetActive(false);
            }

            else if (painter.HP == 3)
            {
                life1.SetActive(true);
                life2.SetActive(true);
                life3.SetActive(true);
                life4.SetActive(false);
                life5.SetActive(false);
            }

            else if (painter.HP == 4)
            {
                life1.SetActive(true);
                life2.SetActive(true);
                life3.SetActive(true);
                life4.SetActive(true);
                life5.SetActive(false);
            }

            else if (painter.HP == 5)
            {
                life1.SetActive(true);
                life2.SetActive(true);
                life3.SetActive(true);
                life4.SetActive(true);
                life5.SetActive(true);
            }
        }

        else if (painter.toLife == 5)
        {
            UI0.SetActive(false);
            UI1.SetActive(false);
            UI2.SetActive(false);
            UI3.SetActive(false);
            UI4.SetActive(false);
            UI5.SetActive(true);

            if (painter.HP == 1)
            {
                life1.SetActive(true);
                life2.SetActive(false);
                life3.SetActive(false);
                life4.SetActive(false);
                life5.SetActive(false);
            }

            else if (painter.HP == 2)
            {
                life1.SetActive(true);
                life2.SetActive(true);
                life3.SetActive(false);
                life4.SetActive(false);
                life5.SetActive(false);
            }

            else if (painter.HP == 3)
            {
                life1.SetActive(true);
                life2.SetActive(true);
                life3.SetActive(true);
                life4.SetActive(false);
                life5.SetActive(false);
            }

            else if (painter.HP == 4)
            {
                life1.SetActive(true);
                life2.SetActive(true);
                life3.SetActive(true);
                life4.SetActive(true);
                life5.SetActive(false);
            }

            else if (painter.HP == 5)
            {
                life1.SetActive(true);
                life2.SetActive(true);
                life3.SetActive(true);
                life4.SetActive(true);
                life5.SetActive(true);
            }
        }
    }
}
