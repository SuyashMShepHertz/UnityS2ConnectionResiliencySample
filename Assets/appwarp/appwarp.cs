using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using com.shephertz.app42.gaming.multiplayer.client;
using com.shephertz.app42.gaming.multiplayer.client.events;
using com.shephertz.app42.gaming.multiplayer.client.listener;
using com.shephertz.app42.gaming.multiplayer.client.command;
using com.shephertz.app42.gaming.multiplayer.client.message;
using com.shephertz.app42.gaming.multiplayer.client.transformer;

//using UnityEditor;

using AssemblyCSharp;

public class appwarp : MonoBehaviour 
{
	public string host = "127.0.0.1";
	public int port = 12346;
	public string appKey = "baefee1c-f958-443b-9";
	public string roomid = "1568064612";
	public static string username = "";
	
	public bool useUDP = true;
	
	Listener listen;
	
	void Start () {
		WarpClient.initialize (appKey, host,port);
		listen = GetComponent<Listener>();
		WarpClient.GetInstance().AddConnectionRequestListener(listen);
		WarpClient.GetInstance().AddChatRequestListener(listen);
		WarpClient.GetInstance().AddLobbyRequestListener(listen);
		WarpClient.GetInstance().AddNotificationListener(listen);
		WarpClient.GetInstance().AddRoomRequestListener(listen);
		WarpClient.GetInstance().AddUpdateRequestListener(listen);
		WarpClient.GetInstance().AddZoneRequestListener(listen);

		WarpClient.setRecoveryAllowance (60);
		// join with a unique name (current time stamp)
		username = System.DateTime.UtcNow.Ticks.ToString();
		
		//EditorApplication.playmodeStateChanged += OnEditorStateChanged;
	}
	
	void Update () {
		
		if (Input.GetKeyDown(KeyCode.Escape)) {
        	Application.Quit();
    	}
		WarpClient.GetInstance().Update();
	}
	
	void OnGUI()
	{
		GUI.contentColor = Color.black;
		GUI.Label(new Rect(240,10,400,500), listen.getDebug());

		if(GUI.Button(new Rect(10,10,100, 30), "Get State"))
		{
			byte state = WarpClient.GetInstance().GetConnectionState();
			switch(state)
			{
			case WarpConnectionState.CONNECTED:
				listen.onLog("State : Connected");
				break;
			case WarpConnectionState.CONNECTING:
				listen.onLog("State : Connecting");
				break;
			case WarpConnectionState.DISCONNECTED:
				listen.onLog("State : Disconnected");
				break;
			case WarpConnectionState.DISCONNECTING:
				listen.onLog("State : Disconnecting");
				break;
			case WarpConnectionState.RECOVERING:
				listen.onLog("State : Recovering");
				break;
			}
		}

		if(GUI.Button(new Rect(120,10,100, 30), "Disconnect"))
		{
			WarpClient.GetInstance().Disconnect();
		}

		if(GUI.Button(new Rect(10,50,100, 30), "Connect"))
		{
			WarpClient.GetInstance().Connect(username,"");
		}

		if(GUI.Button(new Rect(120,50,100, 30), "Recover"))
		{
			WarpClient.GetInstance().RecoverConnection();
		}

		if(GUI.Button(new Rect(10,90,100, 30), "Join Room"))
		{
			WarpClient.GetInstance().JoinRoom(roomid);
		}

		if(GUI.Button(new Rect(120,90,100, 30), "Send Chat"))
		{
			WarpClient.GetInstance().SendChat("Hi");
		}
	}
	
	/*void OnEditorStateChanged()
	{
    	if(EditorApplication.isPlaying == false) 
		{
			WarpClient.GetInstance().Disconnect();
    	}
	}*/
	
	void OnApplicationQuit()
	{
		//WarpClient.GetInstance().Disconnect();
	}
	
}
