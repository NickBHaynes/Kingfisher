using UnityEngine;
using System.Collections.Generic;
using ExplosionForce2D.PropertyAttributes;

#if UNITY_EDITOR
	using UnityEditor;
#endif


public class DemoSceneManager_1 : MonoBehaviour {

	[SerializeField] private GameObject explosion = null;

	[SerializeField] private SpriteRenderer explosionSpriteRenderer = null;
	[SerializeField] private Sprite normalStateSprite = null;
	[SerializeField] private Sprite explodeStateSprite = null;

	[SerializeField] private GameObject currentDemoBuilding = null;
	[SerializeField] private GameObject newDemoBuildingReference = null;

	[SerializeField] private Vector3 spawnLocation = default(Vector3);

	[SerializeField] private RectTransform SelectedImageRect = null;
	[SerializeField] private List<RectTransform> ExplosionsTextRectList = new List<RectTransform> ();

	[SerializeField] private bool sendExplosionDamage = true;
	[BoolConditionalHide("sendExplosionDamage",true,false)] [SerializeField] private float explosionDamage = 1f;
	[BoolConditionalHide("sendExplosionDamage",true,false)] [SerializeField] private string methodToCall = "TakeExplosionDamage";
	[BoolConditionalHide("sendExplosionDamage",true,false)] [SerializeField] private bool modifieDamageByDistance = false;


	private Transform explosionTransform;
	private UniversalExplosion explosionScript;
	private int currentSelectedExplosion = 0;

	private Vector3 mousePosition = default(Vector3);

	void Start () {
		explosionTransform = explosion.transform;
		explosionScript = explosion.GetComponent<UniversalExplosion> ();
		explosionScript.onClick = true;
	}

	void Update () {
		MovementHandler();
		SpriteSwapHandler();
		SwitchExplosionsInputCapture();
		if (Input.GetKeyUp(KeyCode.R) || Input.GetMouseButtonUp(1))
			Restart();
    }

	public void Restart () {
		if (currentDemoBuilding)
			Destroy (currentDemoBuilding.gameObject);
		currentDemoBuilding = Instantiate (newDemoBuildingReference,spawnLocation,default(Quaternion)) as GameObject;
	}

	private void MovementHandler () {
		mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		mousePosition.z = 0f;
		explosionTransform.position = mousePosition;
	}

	private void SpriteSwapHandler () {
		if (Input.GetMouseButtonDown (0))
			explosionSpriteRenderer.sprite = explodeStateSprite;
		else if (Input.GetMouseButtonUp (0))
			explosionSpriteRenderer.sprite = normalStateSprite;
	}

	private void SwitchExplosionsInputCapture () {
		if (Input.GetKeyUp (KeyCode.Alpha1) || Input.GetKeyUp (KeyCode.Keypad1))
			SwitchExplosion (0);
		else if (Input.GetKeyUp (KeyCode.Alpha2) || Input.GetKeyUp (KeyCode.Keypad2))
			SwitchExplosion (1);
		else if (Input.GetKeyUp (KeyCode.Alpha3) || Input.GetKeyUp (KeyCode.Keypad3))
			SwitchExplosion (2);
		else if (Input.GetKeyUp (KeyCode.Alpha4) || Input.GetKeyUp (KeyCode.Keypad4))
			SwitchExplosion (3);
		else if (Input.GetKeyUp (KeyCode.Alpha5) || Input.GetKeyUp (KeyCode.Keypad5))
			SwitchExplosion (4);
		else if (Input.GetKeyUp (KeyCode.Alpha6) || Input.GetKeyUp (KeyCode.Keypad6))
			SwitchExplosion (5);
		else if (Input.GetKeyUp (KeyCode.Alpha7) || Input.GetKeyUp (KeyCode.Keypad7))
			SwitchExplosion (6);

		if (Input.GetKeyUp (KeyCode.DownArrow) || Input.GetKeyUp (KeyCode.S)) {
			if (currentSelectedExplosion + 1 <= 6)
				SwitchExplosion (currentSelectedExplosion + 1);
			else
				SwitchExplosion (0);
		} else if (Input.GetKeyUp (KeyCode.UpArrow) || Input.GetKeyUp (KeyCode.W)) {
			if (currentSelectedExplosion - 1 >= 0)
				SwitchExplosion (currentSelectedExplosion - 1);
			else
				SwitchExplosion (6);
		}
	}

	void SwitchExplosion (int index) {
		if (currentSelectedExplosion == index)
			return;
		if (explosionScript)
			Destroy (explosionScript);
		currentSelectedExplosion = index;
		switch (currentSelectedExplosion) {
		case 0:
			explosionScript = explosion.AddComponent (typeof(SingleExplosion)) as SingleExplosion;
			if (sendExplosionDamage) {
				SingleExplosion singleExplosionScript = explosionScript.GetComponent<SingleExplosion> ();
				singleExplosionScript.sendExplosionDamage = true;

				singleExplosionScript.explosionDamage = explosionDamage;
				singleExplosionScript.methodToCall = methodToCall;
				singleExplosionScript.modifyDamageByDistance = modifieDamageByDistance;
			}
			break;
		case 1:
			explosionScript = explosion.AddComponent(typeof(UpliftExplosion)) as UpliftExplosion;
			if (sendExplosionDamage) {
				UpliftExplosion upliftExplosionScript = explosionScript.GetComponent<UpliftExplosion> ();
				upliftExplosionScript.sendExplosionDamage = true;

				upliftExplosionScript.explosionDamage = explosionDamage;
				upliftExplosionScript.methodToCall = methodToCall;
				upliftExplosionScript.modifyDamageByDistance = modifieDamageByDistance;
			}
			break;
		case 2:
			explosionScript = explosion.AddComponent(typeof(DoubleExplosion)) as DoubleExplosion;
			if (sendExplosionDamage) {
				DoubleExplosion doubleExplosionScript = explosionScript.GetComponent<DoubleExplosion> ();
				doubleExplosionScript.sendFirstExplosionDamage = true;
				doubleExplosionScript.sendSecondExplosionDamage = true;

				doubleExplosionScript.firstExplosionDamage = explosionDamage;
				doubleExplosionScript.firstMethodToCall = methodToCall;
				doubleExplosionScript.firstModifyDamageByDistance = modifieDamageByDistance;

				doubleExplosionScript.secondExplosionDamage = explosionDamage;
				doubleExplosionScript.secondMethodToCall = methodToCall;
				doubleExplosionScript.secondModifieDamageByDistance = modifieDamageByDistance;
			}
			break;
		case 3:
			explosionScript = explosion.AddComponent(typeof(TripleExplosion)) as TripleExplosion;
			if (sendExplosionDamage) {
				TripleExplosion trippleExplosionScript = explosionScript.GetComponent<TripleExplosion> ();
				trippleExplosionScript.sendFirstExplosionDamage = true;
				trippleExplosionScript.sendSecondExplosionDamage = true;
				trippleExplosionScript.sendThirdExplosionDamage = true;

				trippleExplosionScript.firstExplosionDamage = explosionDamage;
				trippleExplosionScript.firstMethodToCall = methodToCall;
				trippleExplosionScript.firstModifyDamageByDistance = modifieDamageByDistance;

				trippleExplosionScript.secondExplosionDamage = explosionDamage;
				trippleExplosionScript.secondMethodToCall = methodToCall;
				trippleExplosionScript.secondModifyDamageByDistance = modifieDamageByDistance;

				trippleExplosionScript.thirdExplosionDamage = explosionDamage;
				trippleExplosionScript.thirdMethodToCall = methodToCall;
				trippleExplosionScript.thirdModifyDamageByDistance = modifieDamageByDistance;
			}
			break;
		case 4:
			explosionScript = explosion.AddComponent(typeof(AttractiveExplosion)) as AttractiveExplosion;
			if (sendExplosionDamage) {
				AttractiveExplosion attractiveExplosionScript = explosionScript.GetComponent<AttractiveExplosion> ();
				attractiveExplosionScript.sendExplosionDamage = true;

				attractiveExplosionScript.explosionDamage = explosionDamage;
				attractiveExplosionScript.methodToCall = methodToCall;
				attractiveExplosionScript.modifyDamageByDistance = modifieDamageByDistance;
			}
			break;
		case 5:
			explosionScript = explosion.AddComponent(typeof(UnstableExplosion)) as UnstableExplosion;
			if (sendExplosionDamage) {
				UnstableExplosion unstableExplosionScript = explosionScript.GetComponent<UnstableExplosion> ();
				unstableExplosionScript.sendExplosionDamage = true;

				unstableExplosionScript.explosionDamage = explosionDamage;
				unstableExplosionScript.methodToCall = methodToCall;
				unstableExplosionScript.modifyDamageByDistance = modifieDamageByDistance;
			}
			break;
		case 6:
			explosionScript = explosion.AddComponent(typeof(PulsingExplosion)) as PulsingExplosion;
			if (sendExplosionDamage) {
				PulsingExplosion pulsingExplosionScript = explosionScript.GetComponent<PulsingExplosion> ();
				pulsingExplosionScript.sendExplosionDamage = true;

				pulsingExplosionScript.explosionDamage = explosionDamage;
				pulsingExplosionScript.methodToCall = methodToCall;
				pulsingExplosionScript.modifyDamageByDistance = modifieDamageByDistance;
			}
			break;
		}
		explosionScript.onClick = true;
	
		SelectedImageRect.position = ExplosionsTextRectList [index].position;

		#if UNITY_EDITOR
			Selection.activeGameObject = explosion;
		#endif
	}
}
