/**
* Copyright 2015 IBM Corp. All Rights Reserved.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
*      http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*
*/

using UnityEngine;
using IBM.Watson.DeveloperCloud.Services.ToneAnalyzer.v3;
using IBM.Watson.DeveloperCloud.Utilities;
using IBM.Watson.DeveloperCloud.Logging;
using System.Collections;
using IBM.Watson.DeveloperCloud.Connection;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class ExampleToneAnalyzer : MonoBehaviour
{
    private string _username = "9e62d186-2f47-420c-b36b-de121c11b5db";
    private string _password = "GOAQy8Hb1e4H";
    private string _url = "https://gateway.watsonplatform.net/tone-analyzer/api";
    
    private ToneAnalyzer _toneAnalyzer;
    private string _toneAnalyzerVersionDate = "2017-05-26";

    private string _stringToTestTone = "I hate this game!";
    private bool _analyzeToneTested = false;

    List<string> emotions = new List<string>();

    void Start()
    {
        LogSystem.InstallDefaultReactors();

        //  Create credential and instantiate service
        Credentials credentials = new Credentials(_username, _password, _url);

        _toneAnalyzer = new ToneAnalyzer(credentials);
        _toneAnalyzer.VersionDate = _toneAnalyzerVersionDate;
    }

    private IEnumerator Examples()
    {
        //  Analyze tone
        if (!_toneAnalyzer.GetToneAnalyze(OnGetToneAnalyze, OnFail, _stringToTestTone))
            Log.Debug("ExampleToneAnalyzer.Examples()", "Failed to analyze!");

        while (!_analyzeToneTested)
            yield return null;

        Log.Debug("ExampleToneAnalyzer.Examples()", "Tone analyzer examples complete.");
    }

    private void OnGetToneAnalyze(ToneAnalyzerResponse resp, Dictionary<string, object> customData)
    {
        Log.Debug("ExampleToneAnalyzer.OnGetToneAnalyze()", "{0}", customData["json"].ToString());
        string data = customData["json"].ToString();
        //Parse the json data to extract only the emotions
        data = Regex.Replace(data, "{\"document_tone\":{\"tone_categories\":", "");
        data = (data.Split(']'))[0];
        data = Regex.Replace(data, @"[{\\}]", "");
        data = (data.Split('['))[2];
        data = Regex.Replace(data, "\"score\":", "{");
        data = Regex.Replace(data, "\"tone_id\":", "");
        data = Regex.Replace(data, "\"tone_id\":", "");
        data = Regex.Replace(data, "\"tone_name\":", "");
        data = Regex.Replace(data, ",{", "}:{")+"}";
        //Seperate the emotions for analysis and store in ArrayList
        string[] emotionsData = data.Split(':');
        for (int i = 0; i < emotionsData.Length; i++)
        {
            emotions.Add(emotionsData[i]);
        }
        _analyzeToneTested = true;
    }

    private void OnFail(RESTConnector.Error error, Dictionary<string, object> customData)
    {
        Log.Error("ExampleRetrieveAndRank.OnFail()", "Error received: {0}", error.ToString());
    }

    public List<string> GetEmotions() {
        return emotions;
    }
}
