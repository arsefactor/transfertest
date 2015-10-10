using UnityEngine;
using System.Collections;

public class Prefabs : MonoBehaviour {

	public GameObject PageImagePF;
	public GameObject PageBodyPanelPF;
	public GameObject PageHeaderTextPF;
	public GameObject FullQuestionPanelPF;
	public GameObject LinedQuestionPanelPF;
	public GameObject NumberedQuestionPanelPF;
	public GameObject LineImagePF;
	public GameObject EmptyImagePF;
	public GameObject QuestionPanelPF;
	public GameObject RHSQuestionPanelPF;
	public GameObject PageFooterPanelPF;
	public GameObject TextPF;
	public GameObject InputFieldPF;
	public GameObject HPanelPF;
	public GameObject TablePanelPF;
	public GameObject TableHeaderPanelPF;
	public GameObject TableHeaderTextPF;
	public GameObject TableCellPanelPF;
	public GameObject TableCellTextPF;
	public GameObject TableCellButtonPF;
	public GameObject TableCellInputFieldPF;
	public GameObject NumberPanelPF;
	public GameObject NumberTextPF;

	public GameObject AnswerPanelPF;
	public GameObject AnswerTogglePF;

	public GameObject ShadedSquareImagePF;
	public GameObject ShadedSquare3ImagePF;

	private static Prefabs _instance;
	
	public static Prefabs instance
	{
		get
		{
			if(_instance == null)
			{
				_instance = GameObject.FindObjectOfType<Prefabs>();
				
				//Tell unity not to destroy this object when loading a new scene!
				DontDestroyOnLoad(_instance.gameObject);
			}
			
			return _instance;
		}
	}
	
	void Awake() 
	{
		if(_instance == null)
		{
			//If I am the first instance, make me the Singleton
			_instance = this;
			DontDestroyOnLoad(this);
		}
		else
		{
			//If a Singleton already exists and you find
			//another reference in scene, destroy it!
			if(this != _instance)
				Destroy(this.gameObject);
		}
	}


}
