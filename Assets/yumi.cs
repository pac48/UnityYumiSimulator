using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using UnityEngine.UIElements;
using MathNet.Numerics.LinearAlgebra;
using System.Data;
using MathNet.Numerics.LinearAlgebra.Single;
using Mirror;
using RosSharp.RosBridgeClient;

public class Yumi : NetworkBehaviour
{
    //System.Threading.Thread SocketThread;
   // volatile bool keepReading = false;
   // public List<Transform> tList;
    public ArticulationBody root;
    public Transform effectorLT;
    public Transform hand;
    public Vector effectorL = new DenseVector(6);
    public Transform effectorRT;
    public Vector effectorR = new DenseVector(6);
    public ArticulationJacobian J = new ArticulationJacobian(120, 14);
    public static Matrix JR = new DenseMatrix(6, 7);
    public static Matrix JL = new DenseMatrix(6, 7);
    public static Matrix eye = (Matrix)Matrix<float>.Build.DenseIdentity(7);
    public Vector target = new DenseVector(6) { -.7f, .3f, 3.5f, 0, 0, 0 };
    private List<int> leftInds = new List<int>(); //{ 7, 8, 9, 10, 11, 12, 13 };
    private List<int> rightInds = new List<int>();// { 0, 1, 2, 3, 4, 5, 6 };
    public List<ArticulationBody> jointsR;
    public List<ArticulationBody> jointsL;
    public static List<float> jointVelR = new List<float>() { 0, 0, 0, 0, 0, 0, 0 };
    public static List<float> jointVelL = new List<float>() { 0, 0, 0, 0, 0, 0, 0 };
    public static List<float> handVelR = null;
    public static List<float> handVelL = null;
    public SyncList<float> jointAngles = new SyncList<float>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    public SyncList<float> jointVelocities = new SyncList<float>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    public GameObject player;


    void Start()
    {
        player = MyNetworkManager.singleton.playerPrefab;
        foreach (var joint in gameObject.GetComponentsInChildren<ArticulationBody>())
        {
            if (joint.name.Contains("l") && joint.name.Length == 2)
                leftInds.Add(joint.index-1);
            if (joint.name.Contains("r") && joint.name.Length == 2)
                rightInds.Add(joint.index-1);
        }

    }
    
    // Update is called once per frame
    void printJ(Matrix Ji) {
        String str = "";
        for (int r = 0; r < Ji.RowCount; r++)
        {
            for (int c = 0; c < Ji.ColumnCount; c++)
            {
                str += Ji[r, c].ToString() + " ";
            }
            str += "\n";
        }
        Debug.Log(str);
    }
    void printJ(Vector Ji)
    {
        String str = "";
        for (int r = 0; r < Ji.Count; r++)
        {
            str += Ji[r].ToString() + " ";
            str += "\n";
        }
        Debug.Log(str);
    }

    void printJ(ArticulationJacobian Ji)
    {
        String str = "start: \n";
        for (int r = 0; r < Ji.rows; r++)
        {
            for (int c = 0; c < Ji.columns; c++)
            {
                str += Ji[r, c].ToString() + " ";
            }
            str += "\n";
        }
        str += "end: \n";
        Debug.Log(str);
    }
    void FixedUpdate()
    {
      //handVelR = new List<float>() { .1f, 0, 0, 0, 0, 0};//null;
     // handVelL = new List<float>() { .1f, 0, 0, 0, 0, 0 };//null;
         if (handVelL != null || handVelR != null) 
            getJacobian();
        if (handVelL != null)
        {
            setHandVel(handVelL, true);
        }
        if (handVelR != null)
        {
            setHandVel(handVelR, false);
        }

        var vals = jointVelR.Concat(jointVelL).ToList();
        int count = 0;
        foreach (var i in leftInds)
        {
            vals[i] = jointVelL[count];
            count++;
        }
        count = 0;
        foreach (var i in rightInds)
        {
            vals[i] = jointVelR[count];
            count++;
        }

       // var c = GetComponent<NetworkIdentity>();
      
        
        if (player.name.Contains("yumi"))
        {
             
             //root.GetJointPositions(jointAngles);
            // if (client)
            CmdSetJoints();
            root.SetDriveTargetVelocities(vals);
        }
        else
        {
            var tmp = jointAngles.ToList();
            GetSyncWithInds(jointAngles, tmp);
            root.SetJointPositions(tmp);
            tmp = jointVelocities.ToList();
            GetSyncWithInds(jointVelocities, tmp);
            root.SetJointVelocities(jointVelocities.ToList());
            
        }
    }
    
    //[Command]
    void CmdSetJoints()
    {
       // m_health = health;
       var tmp = jointAngles.ToList();
       root.GetJointPositions(tmp);
       //for (int i = 0; i < tmp.Count; i++)
       //    jointAngles[i] = tmp[i];

       SetSyncWithInds(jointAngles, tmp);
       
       tmp = jointVelocities.ToList();
       root.GetJointVelocities(tmp);
       SetSyncWithInds(jointVelocities, tmp);
       //for (int i = 0; i < tmp.Count; i++)
       //    jointVelocities[i] = tmp[i];
      
    }
    
    void SetSyncWithInds(SyncList<float> vals, List<float> jointVals){
        
        for (int i = 0; i < leftInds.Count; i++)
        {
            vals[i] = jointVals[leftInds[i]]; // l1, l2, l3...
        }
        for (int i = 0; i < rightInds.Count; i++)
        {
            vals[i+7] = jointVals[rightInds[i]]; // r1, r2, r3...
        }
    }
    
    void GetSyncWithInds(SyncList<float> vals, List<float> jointVals){
        
        for (int i = 0; i < leftInds.Count; i++)
        {
            jointVals[leftInds[i]] = vals[i]; // l1, l2, l3...
        }
        for (int i = 0; i < rightInds.Count; i++)
        {
            jointVals[rightInds[i]] = vals[i+7];  // r1, r2, r3...
        }
    }
    
    void setJointVelocities(List<float> valuesL, List<float> valuesR)
    {
        jointsR[0].SetDriveTargetVelocities(valuesR);
        int ind = -1;
        foreach (ArticulationBody j in jointsR) {
            ind++;
            // j.jointVelocity = new ArticulationReducedSpace(valuesR[ind]);
            //j.jointAcceleration = new ArticulationReducedSpace(0.0f);

        }
        ind = -1;
        foreach (ArticulationBody j in jointsL) {
            ind++;
            j.jointVelocity = new ArticulationReducedSpace(valuesL[ind]);
            j.jointAcceleration = new ArticulationReducedSpace(0.0f);
        }
    }
    void setJointAcceleration(List<float> values)
    {
        List<float> current = new List<float>();
        root.GetJointAccelerations(current);
        for (int i = 0; i < current.Count; i++)
        {
            current[i] = values[i];
        }
        ArticulationReducedSpace acc = new ArticulationReducedSpace(0.0f);
        root.SetJointAccelerations(values);
        root.SetJointVelocities(values);
    }


    void fillMatrix(int startRow, List<int> cols, Matrix Ji)
    {
        int row = -1;
        for (int r = startRow; r < startRow + 6; r++) {
            row += 1;
            int col = -1;
            foreach (int c in cols) {
                col += 1;
                Ji[row, col] = J[r, c];
            }

        }
    }
    void getJacobian()
    {
        root.GetDenseJacobian(ref J);
        int indR = GameObject.Find("r7").GetComponent<ArticulationBody>().index;
        int indL = GameObject.Find("l7").GetComponent<ArticulationBody>().index;
        // find last block of J for R
     //   int lastR = 0;
     //   int lastL = 0;
     //   for (int r = 0; r < J.rows; r++) {
     //       if (J[r, rightInds[0]] != 0 && J[r, rightInds[1]] != 0)
    //            lastR = r;
    //        if (J[r, leftInds[0]] != 0 && J[r, leftInds[1]] != 0)
    //            lastL = r;
    //    }
        fillMatrix(indR*6 - 6, rightInds, JR);
        fillMatrix(indL*6 - 6, leftInds, JL);

       // printJ(JL);
      //  Debug.Log("LR");
       // printJ(JR);
      //  Debug.Log("RL");
       // printJ(J);
      //  Debug.Log("RL");
    }

    public static void setHandVel(List<float> vel, bool left) {
        Vector v = new DenseVector(6);
        for (int i = 0; i < vel.Count; i++)
        {
            v[i] = vel[i];
        }
        if (left)
        {    
            Vector edL = (Vector)((JL.Transpose() * JL + .1f * eye).Inverse() * JL.Transpose() * 2f * v);
            jointVelL = edL.ToList();
        }
        else {
            Vector edR = (Vector)((JR.Transpose() * JR + .1f * eye).Inverse() * JR.Transpose() * 2f * v);
            jointVelR = edR.ToList();
        }


    }
}



     /*
            void startServer()
    {
        SocketThread = new System.Threading.Thread(networkCode);
        SocketThread.IsBackground = true;
        SocketThread.Start();
    }


   
    private string getIPAddress()
    {
        IPHostEntry host;
        string localIP = "";
        host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (IPAddress ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                localIP = ip.ToString();
            }

        }
        return localIP;
    }


    Socket listener;
    Socket handler;

    void networkCode()
    {
        string data;

        // Data buffer for incoming data.
        byte[] bytes = new Byte[1024];

        // host running the application.
        Debug.Log("Ip " + getIPAddress().ToString());
        IPAddress[] ipArray = Dns.GetHostAddresses(getIPAddress());
        IPEndPoint localEndPoint = new IPEndPoint(ipArray[0], 5000);

        // Create a TCP/IP socket.
        listener = new Socket(ipArray[0].AddressFamily,
            SocketType.Stream, ProtocolType.Tcp);

        // Bind the socket to the local endpoint and 
        // listen for incoming connections.

        try
        {
            listener.Bind(localEndPoint);
            listener.Listen(10);

            // Start listening for connections.
            while (true)
            {
                keepReading = true;

                // Program is suspended while waiting for an incoming connection.
                Debug.Log("Waiting for Connection");     //It works

                handler = listener.Accept();
                Debug.Log("Client Connected");     //It doesn't work
                data = null;

                // An incoming connection needs to be processed.
                while (keepReading)
                {
                    bytes = new byte[192];
                    int bytesRec = handler.Receive(bytes);
                    Debug.Log("Received from Server");

                    if (bytesRec <= 0)
                    {
                        keepReading = false;
                        handler.Disconnect(true);
                        break;
                    }

                    // data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                    jointVelMsg.Fill(bytes);
                    //Debug.Log(msg.value);
                   // if (data.IndexOf("<EOF>") > -1)
                  //  {
                   //     break;
                  //  }

                    System.Threading.Thread.Sleep(1);
                }

                System.Threading.Thread.Sleep(1);
               // byte[] bytes2 = new Byte[10];
               // listener.Send(bytes2);
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
    }

    void stopServer()
    {
        keepReading = false;

        //stop thread
        if (SocketThread != null)
        {
            SocketThread.Abort();
        }

        if (handler != null && handler.Connected)
        {
            handler.Disconnect(false);
            Debug.Log("Disconnected!");
        }
    }

    void OnDisable()
    {
        stopServer();
    }
}

public class JointVelMsg {
    public Vector<float> values;
    public int length;
    public JointVelMsg() {
        length = 6;
        values = new DenseVector(length);
        //for (int i = 0; i < length; i++){
        //    values.Add(0.0f);
        //}
        }
    public void Fill(byte[] v)
    {
        for (int i = 0; i < length; i++)
        {
            values[i] = (float) BitConverter.ToDouble(v, i * 8);
            Debug.Log(values[i]);
        }
    }
  
   
} 
      */