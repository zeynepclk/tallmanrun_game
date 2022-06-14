using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class score : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public Text txt;

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex==1)
            txt.text = player.GetComponent<playerController>().point.ToString();
        else if (SceneManager.GetActiveScene().buildIndex == 2)
            txt.text = PlayerPrefs.GetString("score");
    }
   
}
