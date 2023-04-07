using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GroundRestart : MonoBehaviour
{
    void onTriggerExit()
	{
		SceneManager.LoadScene("SampleScene");
	}
}
