using UnityEngine;
using System.Collections;
using System.Net;
using System.Text;
using System.Net.Security;
using System;
using System.IO;

public class Server : MonoBehaviour {

    private int Port = 10000;
    string Message = "Message list";
    Vector2 scrollPosition;
    int PCsNumber = 46;
    //分别存储每台机器每日发送过来的所有模型号
    ArrayList ClientMessageInfoList;

    float windowHeight = 50f;
    float windowWidth=70f;
    int row = 8;
    int col = 8;
    float interval=30;

    float debugHeight = 630;
    float debugWidht = 1120;
    float hScaleRate;
    float wScaleRate;
    ArrayList windowRectList;
	// Use this for initialization
	void Start () {
        CalResolution();
        
        //init 
        ClientMessageInfoList = new ArrayList(PCsNumber);
        for (int i = 0; i < PCsNumber; i++)
        {
            ArrayList tempList = new ArrayList();
            tempList.Add(-1);
            ClientMessageInfoList.Add(tempList);
        }

        //windowHeight = (Screen.height-100) / row - interval;
        //windowWidth = Screen.width / col - interval;
        //Debug.Log(ClientMessageInfoList.Count+" : "+ windowWidth+" : "+ windowHeight);

        windowRectList = new ArrayList();

        //for (int i = 0; i < ClientMessageInfoList.Count; i++)
        //{
        //    int currentCol = i % col;
        //    int currentRow = i / row;

        //    Rect temp = new Rect(currentCol * (windowWidth + interval), currentRow * (windowHeight + interval) + 100, windowWidth, windowHeight);
        //    windowRectList.Add(temp);
        //}

        ArrayList stringPositon = LoadFile(Application.dataPath, "PositionLog.txt");
        for (int i = 0; i < ClientMessageInfoList.Count; i++)
        {
            string tempS = (string)stringPositon[i];
            string[] leftAndRight = tempS.Split('s');
            float left = float.Parse(leftAndRight[0]);
            float top = float.Parse(leftAndRight[1]);
            int currentCol = i % col;
            int currentRow = i / row;

            Rect tempR = new Rect(left * wScaleRate, top * hScaleRate, windowWidth, windowHeight);
            windowRectList.Add(tempR);
        }
	}
    void CalResolution()
    {
        hScaleRate = Screen.height / debugHeight;
        wScaleRate = Screen.width / debugWidht;
        Debug.Log(hScaleRate + " : " + wScaleRate);
        //windowWidth = windowWidth * wScaleRate;
        //windowHeight = windowWidth * hScaleRate;
    }
    //generate log file
    void OnApplicationQuit()
    {
        Debug.Log("Generate log file.");
        
        CreatePositionRecord();

        CreateLog();
    }
    void CreatePositionRecord()
    {
        StreamWriter sw;
        FileInfo t = new FileInfo(Application.dataPath + "//" + "PositionLog.txt");

        sw = t.CreateText();
        for (int i = 0; i < windowRectList.Count; i++)
        {
            string info = "";
            Rect temp = (Rect)windowRectList[i];
            info = temp.xMin + "s" + temp.yMin;
            sw.WriteLine(info);
        }


        sw.Close();
        sw.Dispose();
    }
    void CreateLog()
    {
        StreamWriter sw;
        FileInfo t = new FileInfo(Application.dataPath + "//" + DateTime.Now.Date.Month + "-" + DateTime.Now.Date.Day + "-" + DateTime.Now.Date.Year + " Log.txt");
        if (!t.Exists)
        {
            sw = t.CreateText();
            sw.WriteLine("["+DateTime.Now.Date+" 日志文件]");
            sw.WriteLine("[创建于 "+ DateTime.Now+"]");
            for (int i = 0; i < PCsNumber; i++)
            {
                ArrayList tempList = (ArrayList)ClientMessageInfoList[i];
                string name = i + " 号机:";
                string info = "";
                info += name.PadLeft(6);
                foreach (object tempInt in tempList)
                {
                    //if (tempInt.ToString() != "-1")
                    //{
                        info += " " + tempInt.ToString().PadLeft(3);
                        Debug.Log(info.ToString().PadLeft(3));
                   // }
                }
                sw.WriteLine(info);
            }
        }
        else
        {
            ArrayList oldFileInfo = LoadFile(Application.dataPath, DateTime.Now.Date.Month + "-" + DateTime.Now.Date.Day + "-" + DateTime.Now.Date.Year + " Log.txt");
            //存在即打开并扩展
            sw = t.CreateText();
            sw.WriteLine(oldFileInfo[0]);
            sw.WriteLine("[修改于 " + DateTime.Now+"]");
            for (int i = 0; i < PCsNumber; i++)
            {
                ArrayList tempList = (ArrayList)ClientMessageInfoList[i];
                string info = (string)oldFileInfo[i + 2];
                foreach (object tempInt in tempList)
                {
                    //if (tempInt.ToString() != "-1")
                    //{
                    info += " " + tempInt.ToString().PadLeft(3);
                    Debug.Log(info.ToString().PadLeft(3));
                    //}
                }
                sw.WriteLine(info);
            }
        }
        
        sw.Close();
        sw.Dispose();
    }
    ArrayList LoadFile(string path, string name)
    {
        StreamReader sr = null;
        try
        {
            sr = File.OpenText(path + "//" + name);
        }
        catch (Exception e)
        {
            Debug.Log("can't find it");
            return null;
        }
        string line;
        ArrayList arrlist = new ArrayList();
        while ((line = sr.ReadLine()) != null)
        {
            arrlist.Add(line);
        }
        sr.Close();
        sr.Dispose();
        return arrlist;
    }
    void OnGUI()
    {
        Debug.Log(Screen.height + " : " + Screen.width);
        CalResolution();
        switch (Network.peerType)
        {
            case NetworkPeerType.Disconnected:
                StartServer();
                break;
            case NetworkPeerType.Server:
                OnServer();
                break;
            case NetworkPeerType.Client:

                break;
            case NetworkPeerType.Connecting:

                break;
        }
        
    }

    void StartServer()
    {
        if(GUILayout.Button("Create Server"))
        {
            NetworkConnectionError error = Network.InitializeServer(10, Port, false);
            Debug.Log("Connection Condition:" + error);

            //NetworkViewID viewID = Network.AllocateViewID();
            //this.networkView.viewID = viewID;
        }
    }
    bool isFirst = false;
    void OnServer()
    {
        GUILayout.Label("服务器已建立  -  关闭时自动生成日志文件   "+DateTime.Now);

        int length = Network.connections.Length;
        GUILayout.Label("总机器数:" + PCsNumber);
        GUILayout.Label("处于连接状态机器数:" + length);
        //for (int i = 0; i < length; i++)
        //{
        //    GUILayout.Label("Client ID: " + i);
        //    GUILayout.Label("Client IP: " + Network.connections[i].ipAddress);
        //    GUILayout.Label("Client Port: " + Network.connections[i].port);
        //}
        
        //if (GUILayout.Button("DisConnection"))
        //{
        //    Network.Disconnect();
        //    Message += "Disconnect";
        //}

        //scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(500), GUILayout.Height(600));
        //GUILayout.Box(Message);
        //GUILayout.EndScrollView();
        CreateDisplayWindow();
    }
    void CreateDisplayWindow()
    {
        //for (int i = 0; i < ClientMessageInfoList.Count; i++)
        //{
        //    int currentCol=i%col;
        //    int currentRow=i/row;
        //    GUILayout.Window(
        //        i,
        //        new Rect(currentCol * (windowWidth + interval), currentRow * (windowHeight + interval) + 100, windowWidth, windowHeight),
        //        AddWindow,
        //        i + " 号机");
        //}
        for (int i = 0; i < windowRectList.Count; i++)
        {
            windowRectList[i] = (Rect)GUILayout.Window(
            i,
            (Rect)windowRectList[i],
            AddWindow,
            i + " 号机");
        }
    }
    private void AddWindow(int windowId)
    {
        GUILayout.BeginVertical();
        ArrayList temp = (ArrayList)ClientMessageInfoList[windowId];
        GUILayout.Label("模型号 :" + temp[temp.Count - 1]);
        GUILayout.EndVertical();
        GUI.DragWindow(new Rect(0,0,Screen.width,Screen.height));
    }
    [RPC]
    void RequestMessage(string message, NetworkViewID viewID, NetworkMessageInfo info)
    {
        //split message to name and message
        string[] nameAndMessage = SplitMessage(message);
        Message += "\n" + "Sender:  " + nameAndMessage[0] + " 号机: " + nameAndMessage[1];
        //get the arraylist by name(index)
        ArrayList temp = (ArrayList)ClientMessageInfoList[Int32.Parse(nameAndMessage[0].ToString())];
        temp.Add(Int32.Parse(nameAndMessage[1]));
    }
    string[] SplitMessage(string message)
    {
        string[] result = message.Split('_');
        return result;
    }

}
