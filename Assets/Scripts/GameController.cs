/*
 *  Author: Marcos A. Guerine
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
    public GameObject urubu;
    public GameObject explo;
    public GameObject ball;
    public GameObject almirante;
    public GameObject floor;
    public GameObject wallR;
    public GameObject wallL;
    public GameObject gameOver;

    public Slider slider;
    public Text textpoint;
    public int points;
    public int n_vulture;

    public static GameController gameInstance = null;
    public bool gameIsOver = false;


    private void Awake() {
        // Singleton for instantiate game manager only once
        if (gameInstance == null) {
            gameInstance = this;
        }
    }

    // Start is called before the first frame update
    void Start() {
        // number of vultures instantiated
        n_vulture = 0;
        // number of game points
        points = 0;
        gameIsOver = false;
    }

    // Update is called once per frame
    void Update() {

        if (Input.GetKeyDown(KeyCode.Space)) {
            if (Almirante.almiranteInstance.isFacingRight)
                Instantiate(ball, new Vector3(almirante.transform.position.x + 0.75f, almirante.transform.position.y + 0.7f, almirante.transform.position.z), Quaternion.identity);
            else
                Instantiate(ball, new Vector3(almirante.transform.position.x - 0.75f, almirante.transform.position.y + 0.7f, almirante.transform.position.z), Quaternion.identity);
        }



        // Give a small random chance to instantiate a new vulture
        if (Random.Range(1, 1000) <= 10 && n_vulture < 8 && Time.timeScale > 0) {
            Instantiate(urubu, new Vector3(Random.Range(-7, 7), 4.2f, 0.0f), Quaternion.identity);
            addUrubu();
        }

    }

    public void addUrubu() {
        n_vulture++;
    }

    public void remUrubu() {
        n_vulture--;
    }

    public void UpdatePoints(int update) {
        points += update;
        if (points < 0) {
            points = 0;
            textpoint.text = "Total de urubus abatidos: " + points.ToString();
            GameOver();


        }
        textpoint.text = "Urubus abatidos: " + points.ToString();


    }

    public void GameOver() {
        gameIsOver = true;
        gameOver.SetActive(true);
        Time.timeScale = 0f;
        GameController.gameInstance.GetComponent<AudioSource>().Pause();
    }

    public void ResetGame() {
        gameOver.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene("PlayingScene"); //Load scene called Game
        Start();

    }

    public void MainMenu() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu"); //Load scene called Game
    }


    public void OnCollisionEnter2D(Collision2D colisao) {
        if (colisao.gameObject.tag == "Bola") {
            Destroy(colisao.gameObject);
        }

        if (colisao.gameObject.tag == "Urubu") {
            Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), colisao.collider);
        }

        if (colisao.gameObject.tag == "Shit") {

        }
    }


}
