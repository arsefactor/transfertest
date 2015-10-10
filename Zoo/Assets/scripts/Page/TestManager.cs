using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class TestManager : MonoBehaviour {
	public enum TestType { ALL, MATH_SIMPLE }
	//private static int sA4Width = 4961;
	//private static int sA4Height = 7016;

	public GameObject mainPanel;
	public int NumberOfQuestions = 54;

	List<GameObject> pages = new List<GameObject>();
	int currentPageIndex = 0;
	GameObject currentPage;

	List<IQuestion> questions = new List<IQuestion> ();
	int totalMarks = 0;

	public Button nextPageButton;
	public Button prevPageButton;
	public Button submitTestButton;
	public Button newTestButton;
	public Button upButton;
	public Button downButton;

	public Text resultText;

	// Use this for initialization
	void Start () {
		newTest ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	void buildTest(int pNumberOfQuestions, TestType pTestType){
		int currentQuestion = 1;
		int currentPageNumber = 1;
		int currentPageMarks = 0;
		totalMarks = 0;
		GameObject nextPage = buildPage (currentPageNumber);
		pages.Add(nextPage);

		while (currentQuestion <= pNumberOfQuestions) {
			GameObject questionPage = buildPage(0);
			int marks = buildQuestion (questionPage, currentQuestion, pTestType);
			totalMarks += marks;
			GameObject question = findChild(questionPage, "body/question");

			if ((getPageSpaceLeft(nextPage)-31) < question.GetComponent<RectTransform>().sizeDelta.y){
				completePage (nextPage, currentPageMarks);	
				nextPage.transform.SetParent(null, false);
				currentPageNumber++;
				currentPageMarks = 0;
				nextPage = buildPage(currentPageNumber);
				pages.Add(nextPage);
			}
			currentPageMarks += marks;
			question.transform.SetParent(findChild(nextPage, "body").transform, false);
			questionPage.transform.SetParent(null, false);
			currentQuestion++;
		}
		completePage (nextPage, currentPageMarks);
		nextPage.transform.SetParent(null, false);
		currentPageIndex = 0;
		currentPage = pages [0];

		updatePageView (currentPage);
	}

	void updateButtonStates(){
		if (currentPageIndex >= (pages.Count - 1)) {
			nextPageButton.interactable = false;
		} else {
			nextPageButton.interactable = true;
		}

		if (currentPageIndex > 0) {
			prevPageButton.interactable = true;
		} else {
			prevPageButton.interactable = false;
		}
	}

	public void newTest(){
		resultText.gameObject.SetActive(false);
		newTestButton.gameObject.SetActive(false);
		submitTestButton.gameObject.SetActive(true);

		foreach (GameObject page in pages) {
			Destroy (page);
		}

		pages.Clear ();
		questions.Clear ();
		buildTest (NumberOfQuestions, TestType.ALL);
	}

	public void submitTest(){
		submitTestButton.gameObject.SetActive(false);
		newTestButton.gameObject.SetActive(true);

		int result = 0;
		foreach (IQuestion question in questions){
			result += question.markAnswer();
		}
		resultText.text = "Result = " + ((result*100) / totalMarks) + "%";
		resultText.gameObject.SetActive(true);
	}

	public void nextPage(){
		currentPageIndex++;
		updatePageView (pages [currentPageIndex]);
	}

	public void prevPage(){
		currentPageIndex--;
		updatePageView (pages [currentPageIndex]);
	}

	public void scrollUp(){
		ScrollRect sr = mainPanel.GetComponentInChildren<ScrollRect> ();
		sr.verticalNormalizedPosition = 1;
	}

	public void scrollDown(){
		ScrollRect sr = mainPanel.GetComponentInChildren<ScrollRect> ();
		sr.verticalNormalizedPosition = 0;
	}


	void updatePageView(GameObject pPage){
		currentPage.transform.SetParent (null, false);
		currentPage = pPage;
		currentPage.transform.SetParent (mainPanel.transform, false);
		ScrollRect sr = mainPanel.GetComponentInChildren<ScrollRect> ();
		sr.content = currentPage.GetComponent<RectTransform> ();
		sr.verticalNormalizedPosition = 1;
		updateButtonStates ();
	}


	GameObject findChild(GameObject pParent, string pName){
		return pParent.transform.Find (pName).gameObject;
	}

	float getPageSpaceLeft(GameObject pPage){
		RectTransform pageRect = pPage.GetComponent<RectTransform> ();
		RectTransform pageHeaderRect = findChild(pPage, "header").GetComponent<RectTransform> ();
		RectTransform pageBodyRect = findChild(pPage, "body").GetComponent<RectTransform> ();

		float pageSize = pageRect.sizeDelta.y;
		float usedSize = pageHeaderRect.sizeDelta.y + pageBodyRect.sizeDelta.y;
		float spaceLeft = pageSize - usedSize - 20;

		return spaceLeft;
	}

	GameObject buildPage(int pPageNumber){
		GameObject page = Instantiate (Prefabs.instance.PageImagePF) as GameObject;
		page.transform.SetParent(mainPanel.transform, false);

		// header
		GameObject pageHeaderText = Instantiate (Prefabs.instance.PageHeaderTextPF) as GameObject;
		pageHeaderText.transform.SetParent (page.transform, false);
		pageHeaderText.GetComponent<Text> ().text = ""+pPageNumber;
		pageHeaderText.name = "header";

		GameObject pageBodyPanel = Instantiate (Prefabs.instance.PageBodyPanelPF) as GameObject;
		pageBodyPanel.transform.SetParent (page.transform, false);
		pageBodyPanel.name = "body";

		return page;
	}

	int buildQuestion(GameObject pTempPage, int pQuestionNumber, TestType pType){

		GameObject fullQuestionPanel = Instantiate (Prefabs.instance.FullQuestionPanelPF) as GameObject;
		fullQuestionPanel.name = "question";
		fullQuestionPanel.transform.SetParent (findChild(pTempPage,"body").transform, false);

		GameObject linedQuestionPanel = Instantiate (Prefabs.instance.LinedQuestionPanelPF) as GameObject;
		linedQuestionPanel.transform.SetParent (fullQuestionPanel.transform, false);
		GameObject emptyImage = Instantiate (Prefabs.instance.EmptyImagePF) as GameObject;
		emptyImage.transform.SetParent (linedQuestionPanel.transform, false);

		GameObject numberedQuestion = Instantiate (Prefabs.instance.NumberedQuestionPanelPF) as GameObject;
		numberedQuestion.transform.SetParent (linedQuestionPanel.transform, false);
		numberedQuestion.GetComponentInChildren<Text>().text = pQuestionNumber + ".";
		GameObject questionPanel = Instantiate (Prefabs.instance.QuestionPanelPF) as GameObject;
		questionPanel.transform.SetParent (numberedQuestion.transform, false);

		IQuestion question;
		int whichQuestion = Random.Range (3, 5);
		switch (whichQuestion) {
		case 2:
			question = questionPanel.gameObject.AddComponent<PercentageBoxesQuestion> () as IQuestion;
			break;
		case 3:
			question = questionPanel.gameObject.AddComponent<ShadedSquareQuestion> () as IQuestion;
			break;
		default:
			question = questionPanel.gameObject.AddComponent<AlgebraQuestion> () as IQuestion;
			break;
		}

		questions.Add (question);
		int marks = question.buildQuestion (questionPanel);

		GameObject lineImage = Instantiate (Prefabs.instance.LineImagePF) as GameObject;
		lineImage.transform.SetParent (linedQuestionPanel.transform, false);

		GameObject rhsQuestionPanel = Instantiate (Prefabs.instance.RHSQuestionPanelPF) as GameObject;
		rhsQuestionPanel.transform.SetParent (fullQuestionPanel.transform, false);

		Canvas.ForceUpdateCanvases ();

		return marks;
	}



	void completePage(GameObject pPage, int pMarks){
		Canvas.ForceUpdateCanvases ();
		GameObject pageFooterPanel = Instantiate (Prefabs.instance.PageFooterPanelPF) as GameObject;
		pageFooterPanel.transform.SetParent (findChild (pPage, "body").transform, false);
		GameObject rhsFooterPanel = Instantiate (Prefabs.instance.RHSQuestionPanelPF) as GameObject;
		rhsFooterPanel.transform.SetParent (pageFooterPanel.transform, false);
		GameObject marks = Instantiate (Prefabs.instance.TextPF) as GameObject;
		marks.GetComponent<Text> ().text = "(" + pMarks + ")";

		LayoutElement footerLayout = pageFooterPanel.GetComponent<LayoutElement> ();
	
		footerLayout.preferredHeight = getPageSpaceLeft (pPage);
		if (footerLayout.preferredHeight > 50) {
			GameObject extraPageFooterPanel = Instantiate (Prefabs.instance.PageFooterPanelPF) as GameObject;
			extraPageFooterPanel.transform.SetParent (findChild (pPage, "body").transform, false);
			GameObject extraRhsFooterPanel = Instantiate (Prefabs.instance.RHSQuestionPanelPF) as GameObject;
			extraRhsFooterPanel.transform.SetParent (extraPageFooterPanel.transform, false);
			LayoutElement extraFooterLayout = extraPageFooterPanel.GetComponent<LayoutElement> ();
			extraFooterLayout.preferredHeight = 30;
			footerLayout.preferredHeight -= 30;

			marks.transform.SetParent (findChild (extraRhsFooterPanel, "PanelB").transform, false);
		} else {
			marks.transform.SetParent (findChild (rhsFooterPanel, "PanelB").transform, false);
		}
		
		LayoutRebuilder.MarkLayoutForRebuild (pPage.GetComponent<RectTransform> ());
	}
}
