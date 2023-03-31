using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class timer : MonoBehaviour
{
    public float timeRemaining = 60;
    public TextMeshProUGUI timertext;
    public GameObject object1;
    public AudioSource sound;
    private void Start()
    {
        object1.SetActive(true);
        GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>().Stop();
    }
    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            if (timeRemaining <= 0)
            {
                sound.enabled = false;
                SceneManager.LoadScene("Ending");
            }
            if (timeRemaining <= 15)
            {
                timertext.color = Color.red;
                timertext.fontSize = 72;
            }
            if (timeRemaining <= 30)
            {
                sound.enabled = true;
                timertext.color = Color.yellow;
                timertext.fontSize = 64;
            }

            timertext.text = timeRemaining.ToString().Substring(0, 5);
        }


    }
}