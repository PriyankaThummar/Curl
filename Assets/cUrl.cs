

using Defective.JSON;
using System.Collections;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
[System.Serializable]
public struct UserData
{
    public string system_response;
    public string engagement_id;
    public string customer_state;
}
[System.Serializable]
public struct responseList
{
    public string hide_in_customer_history;
    public string registered_entities;
    public string whiteboard_template;
    public string customer_state;
    public string [] placeholder_aliases;
    public string show_feedback;
    public string to_state_function;
    public string placeholder;
    public string show_in_history;
    public datalist data;
    public string overwrite_whiteboard;
    public string start_timestamp;
    public string console;
    public string name;
    public string title;
    public response_channelsobj response_channels;
    public string whiteboard;
    public state_optionsobj state_options;
    public string response_id;
    public string whiteboard_title;
    public string timestamp;
    public string maintain_whiteboard;
    public string wait;
    public string type;
    public string options;
    public string engagement_id;
}
[System.Serializable]
public struct datalist
{
    public slideshowlist[] slideshow;
}[System.Serializable]
public struct state_optionsobj
{
    public string cs_top_three;
    public string cs_must_have;
    public string cs_enquiry;
    public string cs_mt1;
    public string cs_mt2;
    public string cs_mt3;
}
[System.Serializable]
public struct response_channelsobj
{
    public string voice;
    public string frames;
    public string shapes;
}
[System.Serializable]
public struct slideshowlist
{
    public string image;
    public string caption;
}

public class cUrl : MonoBehaviour
{
    public string serverURL;
    public UserData user;
    public responseList userresponse;
    public Text placeholderText;
    public AudioSource audioSource;
   
    public void OnButtonclick(Text customer_state)
    {
        
        user.customer_state = customer_state.text;
        StartCoroutine(Getcustomer_state());
        
    }
    IEnumerator Getcustomer_state()
    {
        JSONObject prodcut = new JSONObject();
        prodcut.AddField("system_response", user.system_response);
        prodcut.AddField("engagement_id", user.engagement_id);
        prodcut.AddField("customer_state", user.customer_state);

        serverURL = "https://test.iamdave.ai/conversation/exhibit_aldo/74710c52-42a5-3e65-b1f0-2dc39ebe42c2";
        UnityWebRequest www = UnityWebRequest.Post(serverURL, prodcut);
        www.SetRequestHeader("X-I2CE-ENTERPRISE-ID", "dave_expo");
        www.SetRequestHeader("X-I2CE-USER-ID", "74710c52-42a5-3e65-b1f0-2dc39ebe42c2");
        www.SetRequestHeader("X-I2CE-API-KEY", "NzQ3MTBjNTItNDJhNS0zZTY1LWIxZjAtMmRjMzllYmU0MmMyMTYwNzIyMDY2NiAzNw__");
        yield return www.Send();

        if (!string.IsNullOrEmpty(www.error))
        {
           print(www.error);
        }
        else
        {
            userresponse = JsonUtility.FromJson<responseList>(www.downloadHandler.text);
            placeholderText.text = userresponse.placeholder;
          StartCoroutine (DownloadSound(userresponse.response_channels.voice));
            print(www.downloadHandler.text);
           
        }
    }
  
    IEnumerator DownloadSound(string aduioURL)
    {

        if (aduioURL.Length > 0)
        {
            WWW www = new WWW(aduioURL);
            yield return www;




            AudioClip myClip = www.GetAudioClipCompressed(true, AudioType.WAV);
            if (!string.IsNullOrEmpty(www.error))
            {
            }
            else
            {
                if (www.isDone)
                {
                    audioSource.clip=myClip;
                    audioSource.Play();
                }
            }
            }
    }
   
}
