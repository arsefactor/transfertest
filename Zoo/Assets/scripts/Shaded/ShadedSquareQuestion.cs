using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShadedSquareQuestion : MonoBehaviour, IQuestion {
	int answer = 0;
	InputField answerInputField;

	public int buildQuestion(GameObject pQuestionPanel){
		this.transform.SetParent (pQuestionPanel.transform, false);

		Text text = pQuestionPanel.GetComponentInChildren<Text>();
		text.text = "Look at the square below.\n";

		bool shaded3 = (Random.Range (0, 2) == 0);

		GameObject shadedSquareImage;
		if (shaded3) {
			shadedSquareImage = Instantiate (Prefabs.instance.ShadedSquare3ImagePF) as GameObject;
		}
		else {
			shadedSquareImage = Instantiate (Prefabs.instance.ShadedSquareImagePF) as GameObject;
		}
		shadedSquareImage.transform.SetParent (pQuestionPanel.transform, false);
	
		int r = -(Random.Range (0, 4) * 90);
		shadedSquareImage.GetComponent<RectTransform>().Rotate (0,0,r);

		int x = Random.Range (2, 13);
		answer = 4 * x;

		string shadedType = "shaded";
		if (shaded3){
			shadedType = "unshaded";
		}

		GameObject questionText = Instantiate (Prefabs.instance.TextPF) as GameObject;
		questionText.transform.SetParent (pQuestionPanel.transform, false);
		questionText.GetComponent<Text>().text = "The area of the <b>" + shadedType + " triangle</b> is <b>" + x + "cm" + '\u00B2' + ".</b> Find the <b>area</b> of the\n" +
			"square. Write your answer in the space below.";

		GameObject hPanel = Instantiate (Prefabs.instance.HPanelPF) as GameObject;
		hPanel.transform.SetParent (pQuestionPanel.transform, false);

		GameObject answerField = Instantiate (Prefabs.instance.InputFieldPF) as GameObject;
		answerInputField = answerField.GetComponent<InputField> ();
		answerField.transform.SetParent (hPanel.transform, false);

		GameObject units = Instantiate (Prefabs.instance.TextPF) as GameObject;
		units.transform.SetParent (hPanel.transform, false);
		units.GetComponent<Text> ().text = "cm" + '\u00B2';

		return 1;
	}

	public int markAnswer(){
		int marks = 0;

		answerInputField.interactable = false;
		if ((answerInputField.text.Equals("") == false) && (answer == int.Parse (answerInputField.text.Trim ()))) {
			marks++;
			answerInputField.gameObject.GetComponent<Image>().color = Color.green;
		} else {
			answerInputField.gameObject.GetComponent<Image>().color = Color.red;
		}
		
		return marks;
	}
}
