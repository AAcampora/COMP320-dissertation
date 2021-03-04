using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuHandler : MonoBehaviour
{
    Scene noHelp;
    Scene withHelp;
    // Start is called before the first frame update
    void Start()
    {
        //Operation Fire Flight
        noHelp = SceneManager.GetSceneByName("NoHelp");
        //Operation Rampaging storm
        withHelp = SceneManager.GetSceneByName("withHelp");
    }

    public void BeginGame()
    {
        int state = Random.Range(0, 2);

        if(state == 0)
        {
            SceneManager.LoadScene("NoHelp");
        }
        else if(state == 1)
        {
            SceneManager.LoadScene("withHelp");
        }
        Debug.Log(state);
    }
}
