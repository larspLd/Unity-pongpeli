using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButton : MonoBehaviour
{

    // Menu Uusi Peli nappi.

    GameObject loaderObject;
    LevelLoader loader;

    public void startGame() {

        loaderObject = GameObject.Find("SceneManager");
        loader = loaderObject.GetComponent<LevelLoader>();
        
        loader.PlayGame("GameScene");
    }
}
