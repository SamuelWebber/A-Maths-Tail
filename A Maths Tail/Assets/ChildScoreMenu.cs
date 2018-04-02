using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChildScoreMenu : MonoBehaviour {
    public Text Name;
    int id;
    public List<float> scores = new List<float>();
    string GetScoresURL = "https://amathstail.000webhostapp.com/GetScores.php";
    public BarChart barChart;
    public BarChart barChart2;
    public BarChart barChart3;
    public GameObject panel;
    public GameObject panel2;
    public GameObject panel3;
    List<Bar> bars = new List<Bar>();

    // Use this for initialization, get child's name and id, then get all scores for the child
    void Start () {
        Name.text = PlayerPrefs.GetString("ChildName");
        id = PlayerPrefs.GetInt("ChildID");
        StartCoroutine(GetScores());
        for (int i = 0; i < 12; i++)
        {
            scores.Add(0);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //Get all scores for the childID and update lists accordingly
    public IEnumerator GetScores() {
        WWWForm form = new WWWForm();
        form.AddField("ChildIDPost", id);
        WWW website = new WWW(GetScoresURL, form);
        yield return website;
        Debug.Log(website.text);
        if (!website.text.Contains("empty"))
        {
            string[] websiteScores = website.text.Split(';');
            for (int i = 0; i < websiteScores.Length-1; i++)
            {
                string[] websiteParts = websiteScores[i].Split(',');
                scores[int.Parse(websiteParts[0])-1] = float.Parse(websiteParts[1]);
            }
            
        }
        GenerateAllCategories();
        GenerateNumber();
        GenerateMeasurement();
        panel2.SetActive(false);
        panel3.SetActive(false);
    }

    //Generate bar chart for all categories
    public void GenerateAllCategories()
    {
        List<float> categories = new List<float>();
        categories.Add((scores[0] + scores[1] + scores[2] + scores[3]) / 4);
        categories.Add(scores[4]);
        categories.Add(scores[5]);
        categories.Add((scores[6] + scores[7] + scores[8]) / 3);
        categories.Add(scores[9]);
        categories.Add(scores[10]);
        categories.Add(scores[11]);
        List<string> labels = new List<string>();
        labels.Add("Number");
        labels.Add("Algebra");
        labels.Add("Ratio");
        labels.Add("Measurement");
        labels.Add("Geometry");
        labels.Add("Statistics");
        labels.Add("Reading");
        barChart.DisplayGraph(categories, labels);
    }

    //Generate bar chart for number puzzles
    public void GenerateNumber()
    {
        List<float> number = new List<float>();
        number.Add(scores[0]);
        number.Add(scores[1]);
        number.Add(scores[2]);
        number.Add(scores[3]);
        List<string> labels = new List<string>();
        labels.Add("Place Value");
        labels.Add("Addition & subtraction");
        labels.Add("Multiplication & Division");
        labels.Add("Decimals, Fractions, Percentages");
        barChart2.DisplayGraph(number, labels);
    }

    //Generate barchart for measurement
    public void GenerateMeasurement()
    {
        List<float> measurement = new List<float>();
        measurement.Add(scores[6]);
        measurement.Add(scores[7]);
        measurement.Add(scores[8]);
        List<string> labels = new List<string>();
        labels.Add("Measurement Units");
        labels.Add("Time & Dates");
        labels.Add("Money");
        barChart3.DisplayGraph(measurement, labels);
    }

    //Show all categories scores
    public void ShowAllCategories()
    {
        panel.SetActive(true);
        panel2.SetActive(false);
        panel3.SetActive(false);
    }

    //Show number scores
    public void ShowNumber()
    {
        panel.SetActive(false);
        panel2.SetActive(true);
        panel3.SetActive(false);
    }

    //Show measurement scores
    public void ShowMeasurement()
    {
        panel.SetActive(false);
        panel2.SetActive(false);
        panel3.SetActive(true);
    }

    //Go back to select child menu
    public void SelectChild()
    {
        SceneManager.LoadScene("Select Child");
    }
}
