using UnityEngine.UI;
using UnityEngine;

public class UIscoreBlaBla : MonoBehaviour
{
    [SerializeField]private Text score;
    GameManager gm;

    void Start()
    {
         if (gm == null)
        {
            gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        }
        score = GetComponent<Text>();
    }
    void Update()
    {
        score.text = "Score : "+gm.score.ToString();
    }
}
