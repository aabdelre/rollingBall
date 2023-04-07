using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
	public float speed;
	public Text countText;
	public Text exitText;
	public Text winText;
	public int jumpspeed = 0;
	public SphereCollider col;
	public LayerMask groundLayers;
	public AudioSource asource;
	public AudioClip aclip;
	
	private Rigidbody rb;
	private int count;
	
    void Start()
    {
        rb = GetComponent<Rigidbody>();
		count = 0;
		//exitTetx.text = "Press ESC to Exit!";
		SetCountText();
		winText.text = "";
		//exitText.text = "Press ESC to Exit!";
    }

	void Update() 
	{
		if (isGrounded() && Input.GetKeyDown(KeyCode.Space))   {
			rb.AddForce(Vector3.up * jumpspeed, ForceMode.Impulse); 
		}
		if (Input.GetKey("escape")) 
		{	
			//Debug.Log("You Pressed Exit!");
			Application.Quit();
		}
	}
	
	private bool isGrounded() 
	{	
		return Physics.CheckCapsule(col.bounds.center, new Vector3(col.bounds.center.x, col.bounds.min.y, col.bounds.center.z), col.radius * 0.9f, groundLayers);
	}

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rb.AddForce (movement * speed);
    }
	
	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag("Pick Up"))
		{
			other.gameObject.SetActive (false);
			count = count + 1;
			SetCountText ();
			asource.PlayOneShot(aclip);
		}
	}
	
	IEnumerator win() 
	{	
		winText.text = "You Win!";
		yield return new WaitForSeconds(5);
		SceneManager.LoadScene("SampleScene");
	}
	
	void SetCountText () 
	{
		countText.text = "Count: " + count.ToString() + "/20";
		if (count == 20) 
		{
			StartCoroutine(win());
		}
	}
	
}
