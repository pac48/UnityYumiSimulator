using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Single;
using Mirror;
using RosSharp.RosBridgeClient;

public class Yumi : YumiMalePlayer
{
    public ArticulationBody root;
    public ArticulationJacobian J = new ArticulationJacobian(120, 14);
    public static Matrix JR = new DenseMatrix(6, 7);
    public static Matrix JL = new DenseMatrix(6, 7);
    public static Matrix eye = (Matrix)Matrix<float>.Build.DenseIdentity(7);
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
    private List<Action> functions = new List<Action>();
    private PlayerType playerType = new PlayerType();



   void Start()
    {
      //  player = GameObject.Find("server2client").GetComponent<Server2Client>().player;
        foreach (var joint in gameObject.GetComponentsInChildren<ArticulationBody>())
        {
            if (joint.name.Contains("l") && joint.name.Length == 2)
                leftInds.Add(joint.index-1);
            if (joint.name.Contains("r") && joint.name.Length == 2)
                rightInds.Add(joint.index-1);
        }
        base.Start();
    }
   void  FixedUpdate()
    {
        base.FixedUpdate();

    }
    
    override public void FixedUpdateServer()
    {
        FixedUpdateHuman();
    }
    override public void FixedUpdateHuman(){
        var tmp = jointAngles.ToList();
        GetSyncWithInds(jointAngles, tmp);
        root.SetJointPositions(tmp);
        tmp = jointVelocities.ToList();
        GetSyncWithInds(jointVelocities, tmp);
        root.SetJointVelocities(jointVelocities.ToList());
    }
    override public void  FixedUpdateYumi()
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
        var jointAnglesNew = jointAngles.ToList();
        var jointVelocitiesNew = jointVelocities.ToList();
        root.GetJointPositions(jointAnglesNew);
        root.GetJointVelocities(jointVelocitiesNew);
        CmdSetJoints(jointAnglesNew, jointVelocitiesNew);
        root.SetDriveTargetVelocities(vals);

    }
    
    [Command]
    void CmdSetJoints(List<float> jointAnglesNew, List<float> jointVelocitiesNew)
    {
        SetSyncWithInds(jointAngles, jointAnglesNew);
       SetSyncWithInds(jointVelocities, jointVelocitiesNew);

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
        fillMatrix(indR*6 - 6, rightInds, JR);
        fillMatrix(indL*6 - 6, leftInds, JL);
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


 
}
