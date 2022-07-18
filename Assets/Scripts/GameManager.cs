using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public bool gameStarted = false;
    private int score = 0;
    private int highScore = 0;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI startText;
    public TextMeshProUGUI titleText;

    public float fadeTo = 0;
    public float time = 0;

    private int firstBlock;
    private int lastBlock;

    public GameObject collectible;

    public List<GameObject> blocksList = new List<GameObject>();

    private void Awake()
    {
        if (PlayerPrefs.HasKey("HighScore"))
        {
            highScore = PlayerPrefs.GetInt("HighScore");
            highScoreText.text = "High Score:" + highScore.ToString();
        }
        firstBlock = 0;
        lastBlock = blocksList.Count - 1;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            gameStarted = true;
            titleText.gameObject.SetActive(false);
            startText.gameObject.SetActive(false);
        }
        
        if (Time.time - time >0.9f)
        {
            time = Time.time;
            fadeTo = fadeTo * -1 + 1;
        }

        startText.CrossFadeAlpha(fadeTo, 0.6f, false);

    }

    internal void IncreaseScore()
    {
        score++;
        scoreText.text = "Score:" + score.ToString();

        if (score > highScore)
        {
            highScore = score;
            highScoreText.text = "High Score:" + highScore.ToString();
        }
    }

    internal void RestartGame()
    {
        PlayerPrefs.SetInt("HighScore",highScore);
        SceneManager.LoadScene(0);
    }

    public void MoveBlock()
    {
        Vector3 positionToMove;

        if (Random.Range(0, 11) < 5)
        {
            positionToMove = blocksList[lastBlock].transform.position + new Vector3(1.2727f, 0, 1.2727f);
        }
        else
        {
            positionToMove = blocksList[lastBlock].transform.position + new Vector3(-1.2727f, 0, 1.2727f);
        }

        blocksList[firstBlock].transform.position = positionToMove;
        CreateCollectible(positionToMove + new Vector3(0, 1.5f, 0));

        lastBlock = firstBlock;
        firstBlock = (firstBlock + 1) % blocksList.Count;
    }

    private void CreateCollectible(Vector3 position)
    {
        if (Random.Range(0, 20) < 4)
        {
            Instantiate(collectible, position, collectible.transform.rotation);
        }
    }
}
