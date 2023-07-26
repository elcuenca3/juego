using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioInterfaz : MonoBehaviour
{
	public static Musica instance;
	bool muted = false;
	audioSource audioSource;

	[SerializeField] Image img;
	[SerializeField] Sprite spriteMuted;
	[SerializeField] Sprite sprite;


	void Awake(){
		if(instance == null){
			instance = this;
				DontDestroyOnLoad(this.gameObject);
			}else{
				Destroy(this.gameObject);
			}
			audioSource = this.GetComponent<AudioSource>();
	}
		void Update(){
		if(Input.GetKeyDown(KeyCode.M)){
			muted = !muted;
		}
		if(muted){
			audioSource.enabled = false;
		}else{
			audioSource.enabled = true;
		}
	}
}
