using UnityEngine;
using System.Collections;
using System.Net;
using System.Text;
using System.Net.Security;
using System;
using System.IO;

/// <summary>
/// ModelNumber: 所要发送的模型号, 可通过playerPrefab获取.这部分没写
/// debug时,通过 ModelNumber= GUILayout.TextArea(ModelNumber) 来决定ModelNumber值
/// 
/// MyIDNumber.txt :每台机子的序号. 根据现场的机子不同,即时改写内容
/// ServerIP.txt :现场主机的IP号. 同上
/// </summary>
public class Client : MonoBehaviour {

    private int Port = 10000;
    string IP = "192.168.1.100";
    //记录所要发送的模型号 (string)
    string ModelNumber = "";
    string Message = "";

    //在unity运行时.把下列提及的文件放在assets目录下
    //导出后 即放在相应的Data file中
    //自身id号
    //每台机器的程序的Data文件夹中加入MyIDNumber.txt文件.并把内容相应改为对应的机器号
    ArrayList MyIDNmuber;

    //所要连接的主机的IP
    //每台机器的程序的Data文件夹中加入ServerIP.txt文件.并把内容相应改为主机号
    ArrayList ServerIP;

    bool isConnected = false;

    //分别存储每台机器每日发送过来的所有模型号
    ArrayList ClientMessageInfoList;
    int PCsNumber = 46;

    // Use this for initialization
    void Start()
    {
        MyIDNmuber = LoadFile(Application.dataPath, "MyIDNumber.txt");
        ServerIP = LoadFile(Application.dataPath, "ServerIP.txt");
        Debug.Log(MyIDNmuber[0]);
        Debug.Log(ServerIP[0]);

        //init 
        ClientMessageInfoList = new ArrayList(PCsNumber);
        for (int i = 0; i < PCsNumber; i++)
        {
            ArrayList tempList = new ArrayList();
            tempList.Add(-1);
            ClientMessageInfoList.Add(tempList);

        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnGUI()
    {
        switch (Network.peerType)
        {
            case NetworkPeerType.Disconnected:
                StartConnect();
                break;
            case NetworkPeerType.Server:
                
                break;
            case NetworkPeerType.Client:
                OnClient();
                break;
            case NetworkPeerType.Connecting:

                break;
        }
    }

    void StartConnect()
    {

        IP = GUILayout.TextArea(IP);

        //auto connect
        //if connected, the Network.peerType turn to NetworkPeerType.Client.
        if (!isConnected)
        {
            NetworkConnectionError error = Network.Connect(ServerIP[0].ToString(), Port);
            Debug.Log("Connection Condition:" + error);
            if (error.ToString() == "NoError")
            {
                isConnected = true;
            }
        }
    }


    void OnClient()
    {
        GUILayout.BeginHorizontal();

        //debug所用
        ModelNumber= GUILayout.TextArea(ModelNumber);

        //
        if (GUILayout.Button("Send"))
        {
            SendMessageToServer();
        }
        GUILayout.EndHorizontal();

        if (GUILayout.Button("Disconnection"))
        {
            Network.Disconnect();
        }
    }
    void SendMessageToServer()
    {
        //use rpc
        NetworkViewID viewID = Network.AllocateViewID();
        //MyIDNmuber[0].ToString() + "_" + ModelNumber
        //=>  主机号_模型号  (如: 10_22)
        this.networkView.RPC("RequestMessage", RPCMode.Others, MyIDNmuber[0].ToString() + "_" + ModelNumber, viewID);

        Debug.Log("input message: " + ModelNumber);
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
