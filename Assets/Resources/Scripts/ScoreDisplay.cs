using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class ScoreDisplay : MonoBehaviour {
    private static ScoreDisplay _instance;
    public static ScoreDisplay Instance
    {
        get { return _instance; }
    }
    public Text maxText;
    public Text currentText;

    private int currentMaxHeight;
    private int currentHeight = 0;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    // Use this for initialization
    void Start () {
        currentMaxHeight = PlayerPrefs.GetInt("maxHeight");
        maxText.text = currentMaxHeight.ToString();
        currentText.text = "0";

	}

    public void resetScore()
    {
        currentMaxHeight = 0;
        maxText.text = currentMaxHeight.ToString();
        saveHeights();

    }

	

    public void blockPlaced(Vector3 blockPosition)
    {
        float blockHeight = blockPosition.y;
        if (blockHeight > currentHeight)
        {
            currentHeight = (int)blockHeight;
            currentText.text = currentHeight.ToString();
            if (currentHeight > currentMaxHeight)
            {
                currentMaxHeight = currentHeight;
                maxText.text = currentMaxHeight.ToString();
                saveHeights();
            }
        }
    }

    private void saveHeights()
    {
        
        {
            PlayerPrefs.SetInt("maxHeight", currentMaxHeight);
            PlayerPrefs.Save();
        }
    }

}
