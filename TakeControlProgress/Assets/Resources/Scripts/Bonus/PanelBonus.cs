using UnityEngine.UI;
using UnityEngine;

public class PanelBonus : MonoBehaviour {

    public GameObject[] currentStateStars;

    [SerializeField]
    private Slider slider;

    [SerializeField]
    private Text textTimer;

    [SerializeField]
    private GameObject panelGameOver;

    [SerializeField]
    private GameObject panelWin;

    public float timer;

    public bool gameOver;

    public bool wasCorrect;

    public int pluser;
     
    private void OnEnable()
    {
        panelGameOver.SetActive(false);
        panelWin.SetActive(false);
        timer = 11;
        slider.value = 10;
        pluser = 2;

        if (wasCorrect)
        {
            timer = 11;
            slider.value = 10;
            pluser = 10;
        }
    }

    private void Update()
    {
        textTimer.text = ""+(int)timer;

        if (timer > 0)
        {
            timer -= Time.deltaTime;

            slider.value -= Time.deltaTime * 10;
        }
        else
        {
            gameOver = true;

            if (gameOver)
            {
                panelGameOver.SetActive(true);
            }
            else
            {
                panelWin.SetActive(true);
            }
        }
    }

    public void OnPressButton()
    {
        slider.value += pluser;

        if (slider.value >= 100)
        {
            timer = 0;
            panelWin.SetActive(true);
        }    
    }
}
