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

public class yumi : MonoBehaviour
{
    System.Threading.Thread SocketThread;
    volatile bool keepReading = false;
    public List<Transform> tList;
    public ArticulationBody root;
    public Transform effectorLT;
    public Vector effectorL = new DenseVector(6);
    public Transform effectorRT;
    public Vector effectorR = new DenseVector(6);
    public JointVelMsg jointVelMsg;
    public ArticulationJacobian J = new ArticulationJacobian(120, 14);
    public Matrix JR = new DenseMatrix(6, 7);
    public Matrix JL = new DenseMatrix(6, 7);
    public Matrix eye = (Matrix)Matrix<float>.Build.DenseIdentity(7);
    public Vector target = new DenseVector(6) { -.7f, .3f, 3.5f, 0, 0, 0};
    private List<int> leftInds = new List<int>() { 7, 8, 9, 10, 11, 12, 13 };
    private List<int> rightInds = new List<int>() { 0, 1, 2, 3, 4, 5, 6 };
    public List<ArticulationBody> jointsR;
    public List<ArticulationBody> jointsL;
    void Start()
    {
         jointVelMsg  = new JointVelMsg();
         Application.runInBackground = true;
         startServer();
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
        getJacobian();
        effectorL[0] = effectorLT.position.x;
        effectorL[1] = effectorLT.position.y;
        effectorL[2] = effectorLT.position.z;
        effectorR[0] = effectorRT.position.x;
        effectorR[1] = effectorRT.position.y;
        effectorR[2] = effectorRT.position.z;
        if (jointVelMsg.values.Count > 0){ 
        for (int i = 0; i < jointVelMsg.values.Count; i++) {
                target[i] = jointVelMsg.values[i];
            }
        printJ(effectorR);
        Vector dirR = (Vector)(target - effectorR);
        Vector dirL = (Vector)(target - effectorL);
        Vector edR = (Vector)((JR.Transpose() * JR + .01f * eye).Inverse() * JR.Transpose() * 2f * dirR);
        Vector edL = (Vector)((JL.Transpose() * JL + .01f * eye).Inverse() * JL.Transpose() * 2f * dirL);
        edR = (Vector) (JR.PseudoInverse() * jointVelMsg.values); 
        setJointVelocities(edL.ToList<float>(), edR.ToList<float>());// jointVelMsg.values);
    }
    }
    void setJointVelocities(List<float> valuesL, List<float> valuesR)
    {
        int ind = -1;
        foreach (ArticulationBody j in jointsR) {
            ind++;
            j.jointVelocity = new ArticulationReducedSpace(valuesR[ind]);
        
        }
        ind = -1;
        foreach (ArticulationBody j in jointsL){
            ind++;
            j.jointVelocity = new ArticulationReducedSpace(valuesL[ind]);
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
        for (int r = startRow; r < startRow+6; r++){
            row += 1;
            int col = -1;
            foreach (int c in cols){
                col += 1;
                Ji[row, col] = J[r, c];
            }

        }
    }
    void getJacobian()
    {
        root.GetDenseJacobian(ref J);
        // find least block of J for R
        int lastR = 0;
        int lastL = 0;
        for (int r = 0; r < J.rows; r++) {
            if (J[r, rightInds[0]] != 0 && J[r, rightInds[1]] != 0)
                lastR = r;
            if (J[r, leftInds[0]] != 0 && J[r, leftInds[1]] != 0)
                lastL = r;
        }
        fillMatrix(lastR - 5, rightInds, JR);
        fillMatrix(lastL - 5, leftInds, JL);
        
        printJ(J);
    }

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