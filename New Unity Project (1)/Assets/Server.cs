using SimpleTCP;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;

public class Server : MonoBehaviour
{
    /******* STRUCTURE PAQUET : commande;id;infos...  ***************/

    SimpleTcpServer server;



    private List<Character> listPlayer;

    bool partyStarted;

    void Awake()
    {
        UnityThread.initUnityThread();
    }

    void ClientConnected(object sender, TcpClient e)
    {

        
        print("Client connecté");
        
    }

    void ClientDisconnected(object sender, TcpClient e)
    {
        //partyStart();
    }


    // Start is called before the first frame update


    void Start()
    {

        partyStarted = false;
        listPlayer = new List<Character>();
        server = new SimpleTcpServer().Start(8910);
        server.Delimiter = 0x13;
        int cpt = 0;
        server.DelimiterDataReceived += (sender, msg) =>
        {
            UnityThread.executeInUpdate(() =>
            {
                String[] param = msg.MessageString.Split(';');
                cpt = listPlayer.Count;
                int id = -1;
                
                switch (param[0])
                {

                    case "mage":

                        listPlayer.Add(new Mage(cpt));
                        msg.ReplyLine("auth;" + cpt.ToString());
                        break;
                    case "knight":

                        listPlayer.Add(new Knight(cpt));
                        msg.ReplyLine("auth;" + cpt.ToString());
                        break;
                    case "archer":

                        listPlayer.Add(new Archer(cpt));
                        msg.ReplyLine("auth;" + cpt.ToString());
                        break;
                    case "start":
                        partyStart();
                        break;
                    case "STARTOBJ":

                        server.BroadcastLine(msg.MessageString);

                        
                        break;
                    case "OBJ":
                        server.BroadcastLine(msg.MessageString);

                        break;
                    case "OBJATT":
                        server.BroadcastLine(msg.MessageString);
                        break;
                    case "c":
                        id = Int32.Parse(param[1]);
                        listPlayer[id].setPos(float.Parse(param[2]), float.Parse(param[3]), float.Parse(param[4]));
                        listPlayer[id].setRot(float.Parse(param[5]), float.Parse(param[6]), float.Parse(param[7]));

                        listPlayer[id].setLftPos(float.Parse(param[8]), float.Parse(param[9]), float.Parse(param[10]));
                        listPlayer[id].setLftRot(float.Parse(param[11]), float.Parse(param[12]), float.Parse(param[13]));

                        listPlayer[id].setRgtPos(float.Parse(param[14]), float.Parse(param[15]), float.Parse(param[16]));
                        listPlayer[id].setRgtRot(float.Parse(param[17]), float.Parse(param[18]), float.Parse(param[19]));
                        break;



                }

            });

        };

        server.ClientConnected += ClientConnected;
        server.ClientDisconnected += ClientDisconnected;
    }


    public int randomSeed()
    {
        System.Random random = new System.Random();
        return random.Next(10, 10000);
    }

    public void partyStart()
    {
        foreach (Character c in listPlayer)
        {
            print(c.getId()+" "+c.GetType().Name);
            server.BroadcastLine("s;"+c.getId()+";"+c.GetType().Name);

        }
        server.BroadcastLine("seed;" + randomSeed().ToString());
        print("seed;" + randomSeed().ToString());
        partyStarted = true;
    }
        // Update is called once per frame
    void FixedUpdate()
    {
        if (partyStarted)
        {
            foreach (Character c in listPlayer)
            {
                server.BroadcastLine("c;" + c.getId() + ";" + c.getPos().x + ";" + c.getPos().y + ";" + c.getPos().z + ";" + c.getRot().x + ";" + c.getRot().y + ";" + c.getRot().z
                    + ";" + c.getLftPos().x + ";" + c.getLftPos().y + ";" + c.getLftPos().z + ";" + c.getLftRot().x + ";" + c.getLftRot().y + ";" + c.getLftRot().z
                    + ";" + c.getRgtPos().x + ";" + c.getRgtPos().y + ";" + c.getRgtPos().z + ";" + c.getRgtRot().x + ";" + c.getRgtRot().y + ";" + c.getRgtRot().z
                    );
            }
        }

    }
}
