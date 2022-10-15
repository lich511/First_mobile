using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public Text _Text;
    // Start is called before the first frame update
    void Start()
    {
        if (DataHolder._Status == true)
            _Text.text = "Win";
    }

    public void Restart()
	{
        DataHolder._Status = false;
        SceneManager.LoadScene(0);
	}
}
