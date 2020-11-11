using System;
using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using System.IO;
using System.Collections.Generic;
public class UDPReceive : MonoBehaviour {

    public static UDPReceive instance;
    public int port = 11111;
    private  byte[] lastReceivedUDPPacket;
    public  string recdata = null;
    private Thread receiveThread;
    private IPEndPoint ipEndPoint;
    private UdpClient client;

    void Awake()
    {
        instance = this;
        init();
    }
    private void init()
    {
        ipEndPoint = new IPEndPoint(IPAddress.Any, 0);
        client = new UdpClient(port);
        receiveThread = new Thread(ReceiveData);
        receiveThread.IsBackground = true;
        receiveThread.Start();
    }

    private void ReceiveData()
    {
        while (true)
        {
            Thread.Sleep(10);
            try
            {
                byte[] data = client.Receive(ref ipEndPoint);
                if (data != null) lastReceivedUDPPacket = data;

                recdata = Encoding.Default.GetString(lastReceivedUDPPacket);
                Debug.Log(recdata);
            }
            catch (Exception err)
            {
                Debug.Log(err.ToString());
            }
        }
    }
    void OnDisable()
    {
        if (receiveThread != null)
        {
            receiveThread.Abort();
            client.Close();
        }
    }
}
