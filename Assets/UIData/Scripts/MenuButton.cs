using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MenuButton : MonoBehaviour
{
	[SerializeField] MenuButtonController menuButtonController;
	[SerializeField] Animator animator;
	[SerializeField] AnimatorFunctions animatorFunctions;
	[SerializeField] int thisIndex;

	 public Button yourButton;

    // Update is called once per frame
    void Update()
    {
		if(menuButtonController.index == thisIndex)
		{
			animator.SetBool ("selected", true);
			if(Input.GetAxis ("Submit") == 1){
				
				animator.SetBool ("pressed", true);
				Action(thisIndex);
			}else if (animator.GetBool ("pressed")){
				animator.SetBool ("pressed", false);
				animatorFunctions.disableOnce = true;
			}
		}else{
			animator.SetBool ("selected", false);
		}
    }

	public void Action(int a)
	{
		if(a == 0 )  // next scene
		{
			// SceneManager.LoadScene(1);
			yourButton.onClick.Invoke();
		}
		else if(a == 1)    // how to play
		{
			yourButton.onClick.Invoke();
		}
		else if(a == 2)    // quit
		{
			Application.Quit();
		}
	}

	public void MouseHoverEnter()
	{
		menuButtonController.index = thisIndex ;
		// Debug.Log("fsdfsdsdgsg"+thisIndex);
		// animator.SetBool ("pressed", true);
		// animator.SetBool ("selected", true);
	}
	public void MouseHoverExit()
	{
		// animator.SetBool ("selected", false);
	}

	
	
}
