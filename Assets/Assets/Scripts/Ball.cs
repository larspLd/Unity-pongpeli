using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Ball : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody rb;
    public float speed;
    private float startingSpeed;
    public float maxSpeed;

    public int player1Score;
    public int player2Score;

    GameObject textObject;
    TMP_Text scoreText;

    GameObject lightObject;
    LightController LightController;

    GameObject loaderObject;
    LevelLoader loader;

    void Start()
    {
        startingSpeed = speed;

        Launch();
        player1Score = 0;
        player2Score = 0;

        lightObject = GameObject.Find("Player1Light");
        LightController = lightObject.GetComponent<LightController>();
        
        textObject = GameObject.Find("ScoreText");
        scoreText = textObject.GetComponent<TMP_Text>();
    }

    private void Launch() {
        float x = Random.Range(0, 2) == 0 ? -1 : 1;
        float y = Random.Range(0, 2) == 0 ? -1 : 1;
        Vector3 direction = new Vector3(x, 0, y);
        rb.velocity = new Vector3(speed * x, 0, speed * y);
        
    }

    private void OnCollisionEnter(Collision collider) {
        Paddle paddle = collider.gameObject.GetComponent<Paddle>();

        if (paddle != null) {
            if (speed < maxSpeed) {
                speed += 10f; 
                rb.velocity = new Vector3(rb.velocity.x + speed * 0.005f* rb.velocity.x, 0, rb.velocity.z + speed * 0.005f * rb.velocity.z);
            }
        }
    }

    private void Update() {

        if(transform.position.x < -333) {
            player2Score++;
            LightController.intensityBias -= 10;
            reset();
        } else if (transform.position.x > 333) {
            player1Score++;
            LightController.intensityBias += 10;
            reset();
        }
    }
    private void reset() {

        transform.position = new Vector3(0, 1, 0);
        rb.velocity = new Vector3(0, 0, 0);

        speed = startingSpeed;

        if (player1Score == 10) {
            scoreText.text = "Pelaaja 1 voittaa!";
            StartCoroutine(restartGame());

        } else if (player2Score == 10) {
            scoreText.text = "Pelaaja 2 voittaa!";
            StartCoroutine(restartGame());
            
        } else {
            scoreText.text = player1Score.ToString() + " - " + player2Score.ToString();
            StartCoroutine(waitForLaunch());
        }
    }

    IEnumerator waitForLaunch() {
        yield return new WaitForSeconds(1);
        Launch();
    }

    IEnumerator restartGame() {
        loaderObject = GameObject.Find("SceneManager");
        loader = loaderObject.GetComponent<LevelLoader>();
        
        yield return new WaitForSeconds(4);
        loader.PlayGame("MenuScene");
    }
}
