using SimpleTCP;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class client : MonoBehaviour
{
    // Start is called before the first frame update

    public float moveSpeed = 5f;
    private List<Character> listPlayer;
    private bool partyStarted;
    private Vector3 velocity = Vector3.zero;
    [SerializeField]
    AudioSource audManager;

    [SerializeField]
    Text coText;
    [SerializeField]
    Toggle KniTog;
    [SerializeField]
    Toggle MagTog;
    [SerializeField]
    Toggle ArchTog;

    [SerializeField]
    ParticleSystem burst;
    [SerializeField]
    AudioClip coSound;
    [SerializeField]
    AudioClip decoSound;

    SimpleTcpClient cli;

    [SerializeField]
    InputField IPField;

    //void Awake()
    //{
    //    UnityThread.initUnityThread();
    //}

    private int myId;
    Vector3 coord, rot;

    IEnumerator SendData()
    {

        while (true && partyStarted)
        {
            print(listPlayer.Count);

            try
            {
                
                cli.WriteLine("c;" + myId + ";" + this.transform.position.x + ";" + this.transform.position.y + ";" + this.transform.position.z);

                ////Moves Forward and back along z axis                           //Up/Down
                //listPlayer[myId].setPos(Vector3.forward * Time.deltaTime * Input.GetAxis("Vertical") * moveSpeed);
                ////Moves Left and right along x Axis                               //Left/Right
                //listPlayer[myId].setPos(Vector3.right * Time.deltaTime * Input.GetAxis("Horizontal") * moveSpeed);
                //print("J'envoie mes coord");
                //cli.WriteLine("c;" + myId + ";" + listPlayer[myId].getPos().x + ";" + listPlayer[myId].getPos().y + ";" + listPlayer[myId].getPos().z);
            }
            catch (Exception e)
            {

            }
            yield return new WaitForSeconds(0.1f);
        }

    }

    void Start()
    {
        partyStarted = false;
        listPlayer = new List<Character>();
        //StartCoroutine(SendData());
    }







    public void deco()
    {
        UnityThread.executeInUpdate(() =>
        {
            listPlayer.Add(new Mage(10));
        });
        if (cli != null)
        {
            cli.Disconnect();
            Instantiate(burst);
            burst.Play();
            audManager.PlayOneShot(decoSound);
        }
    }





    public void create()
    {
        
        //port 8910
        cli = new SimpleTcpClient().Connect("192.168.137.111", 8910);
        cli.Delimiter = 0x13;
        cli.WriteLine("knight");



        cli.DelimiterDataReceived += (sender, msg) =>
        {

            String[] param = msg.MessageString.Split(';');

            int id = Int32.Parse(param[1]);
            switch (param[0])
            {

                case "auth":
                    myId = id;
                    break;
                case "s":

                    partyStarted = true;





                    if (param[2] == "Mage")
                    {

                        listPlayer.Add(new Mage(id));
                    }
                    if (param[2] == "Knight")
                    {
                        listPlayer.Add(new Knight(id));
                    }
                    if (param[2] == "Archer")
                    {
                        listPlayer.Add(new Archer(id));

                    }

                    break;
                case "c":
                    if (id != myId)
                    {
                        listPlayer[id].setPos(float.Parse(param[2]), float.Parse(param[3]), float.Parse(param[4]));

                    }

                    break;

            }




        };



    }







    void FixedUpdate()
    {
        print(listPlayer.Count);

        try
        {
            cli.WriteLine("c;" + myId + ";" + this.transform.position.x + ";" + this.transform.position.y + ";" + this.transform.position.z);

        }
        catch (Exception e)
        {
            Debug.Log("Impossible d'envoyer les informations " + e.Message);
        }



    }



}

