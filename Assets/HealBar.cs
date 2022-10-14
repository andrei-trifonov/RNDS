using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;



	
public class HealBar : MonoBehaviour {
	[SerializeField] private float[] Borders = new float[2];
	[SerializeField] private  Material mat;
	[SerializeField] private Image barMaterial;
	[SerializeField] private float fillTime = 5f;
	[SerializeField] private  float decreaseTime = 2.5f;
	[SerializeField] private  ActionZone m_ActionZone;

	public float outputValue;
	public bool Finished; 
	public float outPercentage;
	public float fillPercentage;
	private float increaseAmount;
	private float decreaseAmount;
	private bool Clicked;
	private bool Switched;
	private bool Triggered;

	private float initialFillPercentage;
	// Use this for initialization
	
	// Update is called once per frame

	public void SetTriggered(bool state)
	{
		Triggered = state;
	}

	public void SetStateStart()
	{
		Switched = false;
		outputValue = Borders[0];
		initialFillPercentage = mat.GetFloat("_Fillpercentage");
		fillPercentage = initialFillPercentage;
		outPercentage = fillPercentage;
		mat.SetFloat("_Fillpercentage", fillPercentage);
		increaseAmount = Time.fixedDeltaTime / fillTime;
		decreaseAmount =  Time.fixedDeltaTime / decreaseTime;
	}
	public void SetClicked(bool state)
	{
		Clicked = state;
	}

	private void Start()
	{
		mat = Instantiate(mat);
		barMaterial.material = mat;
		SetStateStart();
		
	}

	public void OnClick()
	{

		
		m_ActionZone.StartInteraction();
		
	}
	public void OnUnClick()
	{
		m_ActionZone.EndInteraction();
		
	}
	void FixedUpdate () {
		outputValue = Borders[0] + outPercentage * (Borders[1] - Borders[0]);
		//IF HOLDING 
		if (Triggered)
			mat.SetFloat("_Fillpercentage", fillPercentage);
		if (Clicked) {
			
			//IF STILL NOT FULL
	
			if (fillPercentage < 1f) {
				
				fillPercentage += increaseAmount;
				if (!Switched)
					outPercentage +=  increaseAmount;
				else
					outPercentage -=  increaseAmount;
				fillPercentage = Mathf.Clamp(fillPercentage, 0f, 1f);
				outPercentage = Mathf.Clamp(outPercentage, 0f, 1f);
				
			
			}
			else
			{
				fillPercentage = 0;
				mat.SetFloat("_Fillpercentage", fillPercentage);
				Finished = true;
				Switched = !Switched;
				m_ActionZone.InteractionFinished();
				
			}
			
		}
		else {
			if (!Finished)
			{
				if (fillPercentage > 0f)
				{
					
					fillPercentage -= decreaseAmount;
					if (!Switched)
						outPercentage -= decreaseAmount;
					else
						outPercentage += decreaseAmount;
					fillPercentage = Mathf.Clamp(fillPercentage, 0f, 1f);
					outPercentage = Mathf.Clamp(outPercentage, 0f, 1f);
					
				}
			}
			

		}
		
	}




	public void Restart()
	{
		mat.SetFloat("_Fillpercentage", 0);
		Clicked = false;
		Finished = false;
	
	}

}


