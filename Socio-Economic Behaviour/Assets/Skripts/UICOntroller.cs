using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class UICOntroller : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;


    //GameController gameController;

    private void Start()
    {
        //gameController = GameObject.FindGameObjectsWithTag("GameController").GetComponent<GameController>();
    }


    public void UpdateScore()
    {
       // scoreText.text = gameController.score.ToString();
    }

}
