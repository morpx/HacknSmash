using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour {

    SpriteRenderer weapon;
    Sprite rusty_sword;
    Sprite stick;
	// Use this for initialization
	void Start () {
        weapon = GetComponent<SpriteRenderer>();
        Sprite stick = Resources.Load<Sprite>("stick");
        weapon.sprite = stick;
        //Debug.Log(stick);
        //Debug.Log(sprite);
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void changeSprite(string spriteName)
    {
        
    }
}
