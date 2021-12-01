using UnityEngine;
using UnityEngine.SceneManagement;

public class gotolevel : MonoBehaviour
{
    [SerializeField] string sceneName;
    [SerializeField] string triggeringTag;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "player") SceneManager.LoadScene(1);
    }
}
