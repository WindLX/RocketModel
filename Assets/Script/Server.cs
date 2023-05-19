using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using UnityEngine;
using RocketModel.Models;
using RocketMvvmBase;

public class Server : MonoBehaviour
{
    private Action<object> SendTemperature;
    private Action<object> SendPressure;
    private Action<object> SendAcc;
    private Action<object> SendAngu;
    private Action<object> SendAltitude;
    private Action<object> SendPitch;
    private Action<object> SendYaw;
    private Action<object> SendRoll;
    private Action<object> SendPosture;

    private List<MessageDataModel> data = new();
    private Action<MessageDataModel> OnUpdateData;
    private int index = 0;

    void Start()
    {
        EventBus.Instance.Publish("Temperature", ref SendTemperature);
        EventBus.Instance.Publish("Pressure", ref SendPressure);
        EventBus.Instance.Publish("Acc", ref SendAcc);
        EventBus.Instance.Publish("Angu", ref SendAngu);
        EventBus.Instance.Publish("Altitude", ref SendAltitude);
        EventBus.Instance.Publish("Posture", ref SendPosture);
        EventBus.Instance.Publish("Yaw", ref SendYaw);
        EventBus.Instance.Publish("Pitch", ref SendPitch);
        EventBus.Instance.Publish("Roll", ref SendRoll);
        StartServer();
        OnUpdateData += (v) =>
        {
            // Debug.Log(v.ToString());
            data.Add(v);
        };
    }

    private void FixedUpdate()
    {
        if (data.Count > 0 && index < data.Count - 1)
        {
            SendTemperature?.Invoke(data[index].Temperature);
            SendPressure?.Invoke(data[index].Pressure);
            SendAcc?.Invoke(data[index].Acc);
            SendAngu?.Invoke(data[index].AnguSpeed);
            SendAltitude?.Invoke(data[index].Altitude);
            SendPitch?.Invoke(data[index].Posture.Y);
            SendRoll?.Invoke(data[index].Posture.X);
            SendYaw?.Invoke(data[index].Posture.Z);
            SendPosture?.Invoke(data[index].Posture);
            // Debug.Log($"index: {index}, posture:{data[index].Posture}");
            index++;
        }
    }

    private bool StartServer()
    {
        try
        {
            var ipEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), int.Parse("8080"));
            var serverSocket = new Socket(ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(ipEndPoint);
            serverSocket.Listen(5);
            Thread acceptMsgReqThd = new Thread(AcceptConnectReqHandler);
            acceptMsgReqThd.IsBackground = true;
            acceptMsgReqThd.Start(serverSocket);
            return true;
        }
        catch
        {
            return false;
        }
    }

    private void AcceptConnectReqHandler(object socket)
    {
        try
        {
            Socket serverSocket = (Socket)socket;
            while (true)
            {
                Socket clientSocket = serverSocket.Accept();
                Debug.Log($"Client conneting {clientSocket.AddressFamily}");
                Thread acceptMsgReqThd = new Thread(ReciveMsgReqHandler);
                acceptMsgReqThd.IsBackground = true;
                acceptMsgReqThd.Start(clientSocket);
            }
        }
        catch (Exception e)
        {
            Debug.Log($"Server connect error: {e}");
        }
    }

    private void ReciveMsgReqHandler(object socket)
    {
        Socket clientSocket = (Socket)socket;
        try
        {
            while (true)
            {
                if (clientSocket == null)
                {
                    continue;
                }
                byte[] buffer = new byte[1024];
                int received = clientSocket.Receive(buffer);
                if (received == 0)
                    break;
                var response = Encoding.UTF8.GetString(buffer, 0, received);
                var data = MessageDataConverter.RawMessageToDataConverter(response);
                if (data != MessageDataModel.Zero)
                {
                    OnUpdateData?.Invoke(data);
                }
            }
        }
        catch (Exception e)
        {
            SocketException socketExp = e as SocketException;
            if (socketExp != null && socketExp.NativeErrorCode == 10054)
            {
                Debug.Log($"Socket close: {e}");
            }
            else
            {
                Debug.Log($"Receive error: {e}");
            }
            Thread.CurrentThread.Abort();
        }
    }
}