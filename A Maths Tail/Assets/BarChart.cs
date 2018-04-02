using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarChart : MonoBehaviour {
    public Bar prefab;
    float barchartHeight;

	// Use this for initialization
	void Start () {
        //Get the height of the screen and add it to the Bar Chart panels height
        barchartHeight = Screen.height + GetComponent<RectTransform>().sizeDelta.y - 100;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //Get scores and labels and display them in the barchart
    public void DisplayGraph(List<float> scores,List<string> labels)
    {
        //For each score in the scores, create a bar setting the size of the bar equal to the size of the y axes
        for (int i = 0; i < scores.Count; i++)
        {
            Bar newBar = Instantiate(prefab) as Bar;
            newBar.transform.SetParent(transform);
            RectTransform rect = newBar.bar.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(rect.sizeDelta.x, barchartHeight * (scores[i]/100));
            if (labels.Count <= i)
            {
                newBar.label.text = "UNDEFINED";
            }
            //Add label to each bar
            newBar.label.text = labels[i];
            newBar.value.text = "" + (int) scores[i];
        }
    }
}
