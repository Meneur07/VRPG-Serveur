using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character
{
    
    protected int id;
    protected GameObject headset;
    protected GameObject leftController;
    protected GameObject rightController;
    protected bool isMe;
    public int getId()
    {
        return id;
    }





    public void setPos(Vector3 p)
    {

        UnityThread.executeInUpdate(() =>
        {
            headset.transform.position = p;
        });
    }
    public void setPos(float x, float y, float z)
    {

        UnityThread.executeInUpdate(() =>
        {

            headset.transform.position = new Vector3(x, y, z);

        });

    }
    public Vector3 getPos()
    {
        return headset.transform.position;
    }
    public Vector3 getLftPos()
    {
        return leftController.transform.position;
    }
    public Vector3 getRgtPos()
    {
        return rightController.transform.position;
    }
    public Vector3 getRot()
    {

        return headset.transform.rotation.eulerAngles;
    }
    public void setRot(Vector3 r)
    {
        UnityThread.executeInUpdate(() =>
        {
            headset.transform.rotation = Quaternion.Euler(r);

        });

    }
    public void setRot(float x, float y, float z)
    {
        UnityThread.executeInUpdate(() =>
        {
            headset.transform.rotation = Quaternion.Euler(new Vector3(x, y, z));

        });

    }

    public void setLftPos(Vector3 p)
    {

        UnityThread.executeInUpdate(() =>
        {
            leftController.transform.position = p;
        });
    }
    public void setLftPos(float x, float y, float z)
    {

        UnityThread.executeInUpdate(() =>
        {
            leftController.transform.position = new Vector3(x, y, z);

        });

    }
    public Quaternion getLftRot()
    {

        return leftController.transform.rotation;
    }
    public void setLftRot(Vector3 r)
    {
        UnityThread.executeInUpdate(() =>
        {
            leftController.transform.rotation = Quaternion.Euler(r);
        });

    }
    public void setLftRot(float x, float y, float z)
    {
        UnityThread.executeInUpdate(() =>
        {
            leftController.transform.rotation = Quaternion.Euler(new Vector3(x, y, z));
        });

    }

    public void setRgtPos(Vector3 p)
    {

        UnityThread.executeInUpdate(() =>
        {
            rightController.transform.position = p;
        });
    }
    public void setRgtPos(float x, float y, float z)
    {

        UnityThread.executeInUpdate(() =>
        {
            rightController.transform.position = new Vector3(x, y, z);

        });

    }
    public Quaternion getRgtRot()
    {

        return rightController.transform.rotation;
    }
    public void setRgtRot(Vector3 r)
    {
        UnityThread.executeInUpdate(() =>
        {
            rightController.transform.rotation = Quaternion.Euler(r);
        });

    }
    public void setRgtRot(float x, float y, float z)
    {
        UnityThread.executeInUpdate(() =>
        {
            rightController.transform.rotation = Quaternion.Euler(new Vector3(x, y, z));
        });

    }

    public abstract void createGameObject();
    public abstract void attack();


}


public class Mage : Character
{

    public Mage(int _id, bool isMe = false)
    {
        this.isMe = isMe;
        createGameObject();
        
        id = _id;

    }

    public override void attack()
    {
        throw new System.NotImplementedException();
    }

    public override void createGameObject()
    {
        UnityThread.executeInUpdate(() =>
        {
            if (!isMe)
            {
                headset = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                headset.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            }

            leftController = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            leftController.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            rightController = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            rightController.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        });


    }
}

public class Knight : Character
{

    public Knight(int _id, bool isMe = false)
    {
        this.isMe = isMe;
        createGameObject();

        id = _id;

    }





    public override void attack()
    {
        throw new System.NotImplementedException();
    }

    public override void createGameObject()
    {

        UnityThread.executeInUpdate(() =>
        {

            if (!isMe)
            {
                headset = GameObject.CreatePrimitive(PrimitiveType.Cube);
                headset.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            }

            leftController = GameObject.CreatePrimitive(PrimitiveType.Cube);
            leftController.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            rightController = GameObject.CreatePrimitive(PrimitiveType.Cube);
            rightController.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        });


    }
}

public class Master : Character
{
    public Master(int _id)
    {
        id = _id;
    }

    public override void attack()
    {
        Debug.Log("Le maitre du jeu ne peux pas attacker");
    }

    public override void createGameObject()
    {
        Debug.Log("Le maitre du jeu ne peux pas avoir d'objet");
    }
}

public class Archer : Character
{

    public Archer(int _id,bool isMe = false)
    {
        this.isMe = isMe;
        createGameObject();
        id = _id;

    }





    public override void attack()
    {
        throw new System.NotImplementedException();
    }

    public override void createGameObject()
    {
        UnityThread.executeInUpdate(() =>
        {
            if (!isMe)
            {
                headset = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                headset.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            }

            leftController = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            leftController.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            rightController = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            rightController.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);

        });



    }
}
