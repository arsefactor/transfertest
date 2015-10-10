using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Utils : MonoBehaviour {

	public static int[,] percentToFraction = new int[12,5]{
		{10, 0,0,1,10},
		{20, 0,0,1,5},
		{25, 0,0,1,4},
		{33, 1,3,1,3},
		{40, 0,0,2,5},
		{50, 0,0,1,2},
		{60, 0,0,3,5},
		{66, 2,3,2,3},
		{70, 0,0,7,10},
		{75, 0,0,3,4},
		{80, 0,0,4,5},
		{90, 0,0,9,10}
	};

	public static GameObject findChild(GameObject pParent, string pName){
		return pParent.transform.Find (pName).gameObject;
	}

	public static GameObject getFractionPanel(string pPrefix, int pNumerator, int pDenominator, string pPostfix){
		return getFractionPanel (pPrefix, -999, pNumerator, pDenominator, pPostfix);
	}

	public static GameObject getFractionPanel(string pPrefix, int pNumber, int pNumerator, int pDenominator, string pPostfix){
		GameObject numberPanel = Instantiate (Prefabs.instance.NumberPanelPF) as GameObject;

		if (pPrefix != null) {
			GameObject prefixText = Instantiate (Prefabs.instance.NumberTextPF) as GameObject;
			prefixText.transform.SetParent (numberPanel.transform, false);
			prefixText.GetComponent<Text> ().text = pPrefix;
		}

		if (pNumber != -999) {
			GameObject numberText = Instantiate (Prefabs.instance.NumberTextPF) as GameObject;
			numberText.transform.SetParent (numberPanel.transform, false);
			numberText.GetComponent<Text> ().text = ""+pNumber;
		}
		
		if (pDenominator != 0){
			GameObject numeratorPanel = Instantiate (Prefabs.instance.NumberPanelPF) as GameObject;
			numeratorPanel.transform.SetParent (numberPanel.transform, false);
			numeratorPanel.GetComponent<HorizontalLayoutGroup>().padding.top += 2;

			GameObject numeratorText = Instantiate (Prefabs.instance.NumberTextPF) as GameObject;
			numeratorText.transform.SetParent (numeratorPanel.transform, false);
			numeratorText.GetComponent<Text> ().text = "<size=10>"+pNumerator+"</size>";
			
			GameObject slashText = Instantiate (Prefabs.instance.NumberTextPF) as GameObject;
			slashText.transform.SetParent (numberPanel.transform, false);
			slashText.GetComponent<Text> ().text = "/";

			GameObject denominatorPanel = Instantiate (Prefabs.instance.NumberPanelPF) as GameObject;
			denominatorPanel.transform.SetParent (numberPanel.transform, false);
			denominatorPanel.GetComponent<HorizontalLayoutGroup>().padding.top += 9;

			GameObject denominatorText = Instantiate (Prefabs.instance.NumberTextPF) as GameObject;
			denominatorText.transform.SetParent (denominatorPanel.transform, false);
			denominatorText.GetComponent<Text> ().text = "<size=10>"+pDenominator+"</size>";
		}

		if (pPostfix != null) {
			GameObject postfixText = Instantiate (Prefabs.instance.NumberTextPF) as GameObject;
			postfixText.transform.SetParent (numberPanel.transform, false);
			postfixText.GetComponent<Text> ().text = pPostfix;
		}
		
		return numberPanel;
	}
}
