using UnityEngine;
using UnityEngine.SceneManagement;

public class restartHelp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //SceneManager.UnloadSceneAsync("SampleScene_vertical").completed += (x) => {
            SceneManager.LoadScene("SampleScene_vertical");   
        //};
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
