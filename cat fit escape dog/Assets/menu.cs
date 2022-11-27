using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class menu : MonoBehaviour
{
    public void goplay()
    {
        SceneManager.LoadScene(1/*game scene*/);
    }
    public void goquit()
    {
        Application.Quit();
    }
    public AudioMixer audioMixer;
    public void setvolume(float volume)
    {
        audioMixer.SetFloat("MasterAUDIO", volume);
    }                  /*exposed parameter*/
}
