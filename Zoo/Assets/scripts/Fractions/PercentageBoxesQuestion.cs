using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PercentageBoxesQuestion : MonoBehaviour, IQuestion {
	public string[] percentAnswers = new string[2];
	public string[] fractionAnswers = new string[2];

	public string[] correctPercentAnswers = new string[2];
	public string[] correctFractionAnswers = new string[2];

	public int buildQuestion(GameObject pQuestionPanel){
		this.transform.SetParent (pQuestionPanel.transform, false);

		Text questionText = pQuestionPanel.GetComponentInChildren<Text>();
		questionText.text = "Complete the table below by putting a <b>fraction or a percentage</b>" +
			" in\neach of the four empty boxes.\n";

		GameObject tablePanel = Instantiate (Prefabs.instance.TablePanelPF) as GameObject;
		tablePanel.transform.SetParent (pQuestionPanel.transform, false);

		GameObject headers = Utils.findChild (tablePanel, "headers");

		GameObject percentageHeaderPanel = Instantiate (Prefabs.instance.TableHeaderPanelPF) as GameObject;
		percentageHeaderPanel.transform.SetParent (headers.transform, false);
		GameObject percentageHeaderText = Instantiate (Prefabs.instance.TableHeaderTextPF) as GameObject;
		percentageHeaderText.transform.SetParent (percentageHeaderPanel.transform, false);
		percentageHeaderText.GetComponent<Text> ().text = "Percentage";

		GameObject fractionHeaderPanel = Instantiate (Prefabs.instance.TableHeaderPanelPF) as GameObject;
		fractionHeaderPanel.transform.SetParent (headers.transform, false);
		GameObject fractionHeaderText = Instantiate (Prefabs.instance.TableHeaderTextPF) as GameObject;
		fractionHeaderText.transform.SetParent (fractionHeaderPanel.transform, false);
		fractionHeaderText.GetComponent<Text> ().text = "Fraction";

		List<int> possibleIndexes = new List<int> ();
		for (int i = 0; i < (Utils.percentToFraction.Length/5); i++) {
			possibleIndexes.Add (i);
		}
		int[] indexes = new int[5];
		int index = 0;
		for (int i = 0; i < 5; i++) {
			index = Random.Range (0, possibleIndexes.Count);
			indexes[i] = possibleIndexes[index];
			possibleIndexes.RemoveAt(index);
		}

		GameObject cells = Utils.findChild (tablePanel, "cells");

		for (int i = 0; i < 5; i++){
			GameObject percentageCellPanel = Instantiate (Prefabs.instance.TableCellPanelPF) as GameObject;
			percentageCellPanel.transform.SetParent (cells.transform, false);
			GameObject percentageNumberPanel;
			if (i < 3){
				percentageNumberPanel = Utils.getFractionPanel(null, 
				                                               Utils.percentToFraction[indexes[i],0], 
				                                               Utils.percentToFraction[indexes[i],1],
				                                               Utils.percentToFraction[indexes[i],2],
				                                               "%");
			}
			else {
				percentageNumberPanel = Instantiate (Prefabs.instance.TableCellButtonPF) as GameObject;
				Button cellButton = percentageNumberPanel.GetComponent<Button>();
				cellButton.name = "percent"+i;
				cellButton.onClick.AddListener(delegate { buttonClicked(cellButton); });
			}
			percentageNumberPanel.transform.SetParent (percentageCellPanel.transform, false);
		}

		for (int i = 0; i < 5; i++){
			GameObject fractionCellPanel = Instantiate (Prefabs.instance.TableCellPanelPF) as GameObject;
			fractionCellPanel.transform.SetParent (cells.transform, false);
			GameObject fractionNumberPanel = Utils.getFractionPanel(null,
			                                                        Utils.percentToFraction[indexes[i],3], 
			                                                        Utils.percentToFraction[indexes[i],4],
			                                                        null);
			fractionNumberPanel.transform.SetParent (fractionCellPanel.transform, false);
		}

		return 1;
	}

	public int markAnswer(){
		int marks = 0;
		bool allCorrect = false;

		if (allCorrect) {
			marks++;
		}
		
		return marks;
	}

	void buttonClicked(Button pButton){
		Debug.Log ("Button clicked");
	}
}
