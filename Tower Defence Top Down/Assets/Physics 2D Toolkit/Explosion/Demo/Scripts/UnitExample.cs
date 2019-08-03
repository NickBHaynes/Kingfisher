using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
public class UnitExample : MonoBehaviour {


	[SerializeField] private SpriteRenderer spriteRendererScript = null;
	[SerializeField] private Sprite halfHealthSprite = null;
	[SerializeField] private Sprite LowHealthSprite = null;

	[SerializeField] private float health = 3f;

	[SerializeField] private bool destroy = false;

	private float healthMax = 0f;

	void Awake () {
		healthMax = health;
		spriteRendererScript = GetComponent<SpriteRenderer> ();
	}

	public void TakeExplosionDamage (float damage) {
		health -= damage;
		SetUpSprite ();
	}

	private void SetUpSprite () {
		if (health <= 0f) {
			if (destroy)
				Destroy (this.gameObject);
			else
				spriteRendererScript.sprite = LowHealthSprite;
		} else if (health <= healthMax * 0.333f) {
			spriteRendererScript.sprite = LowHealthSprite;
		} else if (health <= healthMax * 0.666f) {
			spriteRendererScript.sprite = halfHealthSprite;
		}
	}
		
	// ----------------------- Collision Damage Example -----------------------

	//[SerializeField] protected float collisionDamageTolerance = 3f;

	//void OnCollisionEnter2D (Collision2D collisionInfo) {
	//	TakeCollisionDamage (collisionInfo.relativeVelocity.magnitude);
	//}

	//public void TakeCollisionDamage (float damage) {
	//	if (damage > collisionDamageTolerance)
	//		health -= damage;
	//	SetUpSprite ();
	//}


}
