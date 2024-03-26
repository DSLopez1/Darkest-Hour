using UnityEngine;
using UnityEngine.SceneManagement;


public class NoInput : MonoBehaviour
{
    [SerializeField] 
    private float delayBeforeLoading = 3f;
    [SerializeField] string sceneNameToLoad;

    
    private float timeElapsed;

    private void Update()
    {
        timeElapsed += Time.deltaTime;

        if (timeElapsed > delayBeforeLoading)
        {
            SceneManager.LoadScene(sceneNameToLoad);
        }
    }
}
