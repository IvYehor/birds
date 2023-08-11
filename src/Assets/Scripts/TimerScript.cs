using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    public SpawnerScript spawner;
    public GameObject winLoseMenu;

    public Transform playerTransform;
    public Text distText;

    public FinishScript finish;

    public Text timeText;

    public float timer;
    public float maxTime;

    public bool isCounting;

    public Color standartColor;
    public Color redColor;

    void Start()
    {
        timer = 0f;
        timeText.color = standartColor;
        finish = spawner.SpawnFinish().GetComponent<FinishScript>();
        finish.DistText = distText;
        finish.playerTransform = playerTransform;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) 
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (!isCounting && !finish.finished) 
        {
            if (Input.GetAxis("Vertical") > 0.1f || Input.GetKeyDown(KeyCode.Space)) 
            {
                isCounting = true;
            }
        }

        if (isCounting) 
        {
            timer += Time.deltaTime;
            if (timer > maxTime) 
            {
                timeText.color = Color.Lerp(timeText.color, redColor, 0.1f);
            }

            string text = timer.ToString();
            timeText.text = text.Replace(",", ".").Substring(0, text.Length - 4) + " / " + maxTime.ToString() + "s";
            //Debug.Log(text + "  ->  " + timeText.text);
        }

        if (finish.finished && !winLoseMenu.activeInHierarchy) 
        {
            Time.timeScale = 0f;
            winLoseMenu.SetActive(true);
            if(timer <= maxTime)
                winLoseMenu.transform.Find("WinLoseText").GetComponent<Text>().text = "You win!";
            else
                winLoseMenu.transform.Find("WinLoseText").GetComponent<Text>().text = "You lose";
            winLoseMenu.transform.Find("TimeText").GetComponent<Text>().text = $"Time: {timer} sec.";
        }
    }

    public void RestartButtonScript() 
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
