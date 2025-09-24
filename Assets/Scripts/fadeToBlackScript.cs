using UnityEngine;

public class fadeToBlackScript : MonoBehaviour
{
    [SerializeField] menuLogic menuLogicRef;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void menuGoToGameScene()
    {
        menuLogicRef.goToGameScene();
    }
    public void quitGame()
    {
        Application.Quit();
    }
}
