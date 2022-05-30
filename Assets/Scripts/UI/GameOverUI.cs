using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Retry(){
        SceneManager.LoadScene(0); 
        GameManager.gameOverUI.SetActive(false);
        GameManager.scoreUI.SetActive(true);
    }
    public void Quit(){
        Debug.Log("ÇIKIŞ YAPTIM");
        Application.Quit();
    }
    public void OnMouseOver() {
        anim.SetTrigger("ButtonHover");
    }
}
