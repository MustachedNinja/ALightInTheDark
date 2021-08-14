using UnityEngine;

public class PausePanel : MonoBehaviour
{
    public void TogglePause() {
        GameObject child = transform.GetChild(0).gameObject;
        child.SetActive(!child.activeSelf);
        // gameObject.SetActive(!gameObject.activeSelf);
    }
}
