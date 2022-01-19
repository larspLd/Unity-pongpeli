using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButton : MonoBehaviour
{

    GameObject loaderObject;
    LevelLoader loader;

    public void startGame() {
        StateController.randomNumber = Random.Range(1f, 6f);

        loaderObject = GameObject.Find("SceneManager");
        loader = loaderObject.GetComponent<LevelLoader>();
        
        loader.PlayGame();
    }
}
