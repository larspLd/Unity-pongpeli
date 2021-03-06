using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Ball : MonoBehaviour
{
    // Melkein kaikki pelin logiikasta löytyy täältä. Voit vaihtaa kaikkia public asetuksia Ball objektissa.
    public Rigidbody rb;
    public float speed;
    private float startingSpeed;
    public float maxSpeed;
    bool launch; 

    public AudioSource pongSound;
    public AudioSource goalSound;

    public int player1Score;
    public int player2Score;
    public int scoreToWin;

    GameObject textObject;
    TMP_Text scoreText;

    GameObject lightObject;
    LightController LightController;

    GameObject loaderObject;
    LevelLoader loader;

    public ParticleSystem ParticleSystem;
    public ParticleSystem GoalExplosion;
    public Material material;
    public Material goalExplosionMaterial;

    public Animator animation;
    public Animator animation2;

    public Paddle paddle1;
    public Paddle paddle2;

    void Start()
    {
        startingSpeed = speed;

        player1Score = 0;
        player2Score = 0;

        lightObject = GameObject.Find("Player1Light");
        LightController = lightObject.GetComponent<LightController>();
        
        textObject = GameObject.Find("ScoreText");
        scoreText = textObject.GetComponent<TMP_Text>();

        // Kaikki particle värit valkosiksi kun alotat pelin.
        material.color = Color.white;
        goalExplosionMaterial.color = Color.white;
    }

    public void Launch() {
        float x = Random.Range(0, 2) == 0 ? -1 : 1;
        float y = Random.Range(0, 2) == 0 ? -1 : 1;
        Vector3 direction = new Vector3(x, 0, y);
        rb.velocity = new Vector3(speed * x, 0, speed * y);
        
    }

    private void OnCollisionEnter(Collision collider) {
        Paddle paddle = collider.gameObject.GetComponent<Paddle>();

        // Jos osuu paddle objektia.
        if (paddle != null) {    
            pongSound.Play();


            // Vaihtaa värin pelaajan väriksi.
            if (paddle.isPlayer2) {
                material.color = StateController.player2Color;
                goalExplosionMaterial.color = StateController.player2Color;
            } else {
                material.color = StateController.player1Color;
                goalExplosionMaterial.color = StateController.player1Color;
            }
            // Kun osut palloa siitä tulee nopeampi.
            if (speed < maxSpeed) {
                speed += 10f; 
                rb.velocity = new Vector3(rb.velocity.x + speed * 0.005f* rb.velocity.x, 0, rb.velocity.z + speed * 0.005f * rb.velocity.z);
            }
        }
    }

    private void Update() {
        // Valmis
        if (!launch) {
            if (paddle1.ready && paddle2.ready) {
                animation.SetTrigger("PlayersReady");
                animation2.SetTrigger("PlayersReady");
                launch = true;

                StartCoroutine(waitForLaunch());
            }
        }

        // Scoring
        if(transform.position.x < -333) {
            player2Score++;
            LightController.intensityBias -= 10;
            goalSound.Play();
            reset();
        } else if (transform.position.x > 333) {
            player1Score++;
            LightController.intensityBias += 10;
            goalSound.Play();
            reset();
        }
    }
    private void reset() {
        GoalExplosion.transform.position = new Vector3(transform.position.x, transform.position.y + 50, transform.position.z);

        GoalExplosion.Play();

        transform.position = new Vector3(0, 1, 0);
        rb.velocity = new Vector3(0, 0, 0);
        ParticleSystem.Clear();

        speed = startingSpeed;

        if (player1Score == scoreToWin) {
            scoreText.text = "Pelaaja 1 voittaa!";
            StartCoroutine(restartGame());

        } else if (player2Score == scoreToWin) {
            scoreText.text = "Pelaaja 2 voittaa!";
            StartCoroutine(restartGame());
            
        } else {
            scoreText.text = player1Score.ToString() + " - " + player2Score.ToString();
            StartCoroutine(waitForLaunch());
        }
    }

    IEnumerator waitForLaunch() {
        yield return new WaitForSeconds(2);
        Launch();
    }

    IEnumerator restartGame() {
        loaderObject = GameObject.Find("SceneManager");
        loader = loaderObject.GetComponent<LevelLoader>();
        
        yield return new WaitForSeconds(4);
        loader.PlayGame("MenuScene");
    }
}
