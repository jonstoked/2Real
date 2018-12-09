using UnityEngine;
using UnityEngine.UI;
using System.Collections;
//this script  created by Bilgin Sahin (bilginsahin1979@gmail.com) and used with kind permission.
//switch the materials for demo purposes
namespace SantaClaus
{
	public class SwitchMaterials : MonoBehaviour
	{
		public GameObject targetObject;
		public Material[] materials;
		public int index = 0;
		public Text txt_Material;

		void Start ()
		{
			UpdateText ();
		}

		void Update ()
		{
			if (Input.GetKeyDown (KeyCode.Y)) {

				index--;
				if (index < 0)
					index = materials.Length - 1;

				targetObject.GetComponent<Renderer>().material = materials [index];
				UpdateText ();

			} else if (Input.GetKeyDown (KeyCode.C)) {

				index++;
				if (index > materials.Length - 1)
					index = 0;

				targetObject.GetComponent<Renderer>().material = materials [index];
				UpdateText ();
			}
		}

		void UpdateText ()
		{
			switch (index) {
			case 0:
				txt_Material.text = "Material: " + "Unity 5 standard PBR";
				break;
			case 1:
				txt_Material.text = "Material: " + "U4 bumped spec";
				break;
			case 2:
				txt_Material.text = "Material: " + "Unlit";
				break;
			case 3:
				txt_Material.text = "Material: " + "Vertex Lit";
				break;
			case 4:
				txt_Material.text = "Material: " + "U4 mobile bumped spec";
				break;
			}
		}
		public void SwitchSceneZombie (){
			Application.LoadLevel(1);

		}
		public void SwitchSceneSanta (){
			Application.LoadLevel(0);
			
		}

	}
}
