using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour {

    SpriteRenderer weapon;

	void Start () {
        weapon = GetComponent<SpriteRenderer>();
	}

	void Update () {
	    
	}

    public void changeSprite(string spriteName)
    {
        Sprite newWeapon = Resources.Load<Sprite>(spriteName);
        weapon.sprite = newWeapon;
    }
}
