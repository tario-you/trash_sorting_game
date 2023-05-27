using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class trash : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sp;

    private float speed = 7f;
    private float fallSpeed = -0.8f;
    private float moveHorizontal = 0;

    private int category;
    private int score = 0;
    private int lives = 3;

    private Text scoreText, gameOverText;

    private Button retryButton, leftButton, rightButton, dropButton;

    [SerializeField]
    private Sprite[] eWasteSprites, nonEWasteSprites;
    [SerializeField]
    private GameObject heart1,heart2,heart3;
    [SerializeField]
    private GameObject game, gameover;
    [SerializeField]
    private GameObject[] tohide;
    [SerializeField]
    private GameObject retryObject, leftObject, rightObject, dropObject;
    [SerializeField]
    private Button QuitButton;

    void Awake(){
        retryButton = retryObject.GetComponent<Button>();
        retryButton.onClick.AddListener(restart);
        leftButton = leftObject.GetComponent<Button>();
        leftButton.onClick.AddListener(Left);
        rightButton = rightObject.GetComponent<Button>();
        rightButton.onClick.AddListener(Right);
        dropButton = dropObject.GetComponent<Button>();
        dropButton.onClick.AddListener(Drop);

        QuitButton.onClick.AddListener(QuitGame);
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();

        category = Random.Range(1,3);
        UpdateSprite();

        scoreText = GameObject.Find("scoreText").GetComponent<Text>();
        scoreText.transform.position = new Vector3(0f, -361.5f/75f, -29f/75f);

        gameOverText = GameObject.Find("gameOverText").GetComponent<Text>();
        gameOverText.enabled = false;

        retryObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) && lives > 0){
            Right();
        }else if (Input.GetKeyDown(KeyCode.LeftArrow) && lives > 0){
            Left();
        }

        rb.velocity = new Vector2 (moveHorizontal*speed, fallSpeed);

        scoreText.text = "Score: " + score;


        if (lives > 0 && Input.GetKeyDown("space")){
            Drop();
        }

        if (lives <= 0){
            sp.enabled = false;
            gameOverText.enabled = true;
            retryObject.SetActive(true);
            leftObject.SetActive(false);
            dropObject.SetActive(false);
            rightObject.SetActive(false);

            // for (int i=0;i<tohide.Length;i++){
            //     tohide[i].GetComponent<SpriteRenderer>().enabled = false;
            // }

            scoreText.transform.position = new Vector3(0f, -82f/75f, -29f/75f);

            fallSpeed = 0;
            speed = 0;
        }
    }

    void UpdateSprite(){
        if (category == 1){
            int rand = Random.Range(0, eWasteSprites.Length);
            sp.sprite = eWasteSprites[rand];
        }else{
            int rand = Random.Range(0, nonEWasteSprites.Length);
            sp.sprite = nonEWasteSprites[rand];
        }
    }

    void UpdateLives(){
        if (lives == 3){
            heart3.GetComponent<SpriteRenderer>().enabled = true;
            heart2.GetComponent<SpriteRenderer>().enabled = true;
            heart1.GetComponent<SpriteRenderer>().enabled = true;
        }else if (lives == 2){
            heart3.GetComponent<SpriteRenderer>().enabled = false;
        }else if (lives == 1){
            heart2.GetComponent<SpriteRenderer>().enabled = false;
        }else{
            heart1.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    void OnCollisionEnter2D(Collision2D col){
        if (col.gameObject.tag == "trashcan"){
            rb.position = new Vector2 (0.0f, 3.48f);


            string colName = col.gameObject.name;
            int colNum;
            if (colName.Contains("e waste")){
                colNum = 1;
            }else{
                colNum = 2;
            }

            // 1 = ewaste
            // 2 = not ewaste

            // Debug.Log("touched " + colNum + " and I am " + category);

            if (colNum == category){
                score ++;
                fallSpeed *= 1.053f;
                speed *= 1.035f;
            }else{
                lives --;
                fallSpeed *= 0.9f;
                UpdateLives();
            }
            category = Random.Range(1,3);
            UpdateSprite();
            moveHorizontal = 0f;
        }
    }

    public void restart(){
        score = 0;
        lives = 3;
        UpdateLives();
        sp.enabled = true;
        gameOverText.enabled = false;

        scoreText.transform.position = new Vector3(0f, -4.8f, -29f/75f);

        fallSpeed = -0.8f;
        speed = 7;
        retryObject.SetActive(false);
        leftObject.SetActive(true);
        dropObject.SetActive(true);
        rightObject.SetActive(true);
    }

    void Left(){
        moveHorizontal = -1f;
    }

    void Right(){
        moveHorizontal = 1f;
    }

    void Drop(){
        rb.position = new Vector2 (rb.position.x, -1.3f);
    }

    void QuitGame(){
        Application.Quit();
    }
}
