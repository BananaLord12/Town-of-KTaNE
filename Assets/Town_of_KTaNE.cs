using System.Collections;
using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;
using System.Linq;
using rnd = UnityEngine.Random;
using UnityEngine;
using KModkit;

public class Town_of_KTaNE : MonoBehaviour {

    // Unity shit
    public KMAudio Audio;
    public KMBombInfo Bomb;
    public KMSelectable[] Players;
    public KMSelectable Moon_button;
    public TextMesh[] Players_Letter;
    public TextMesh Player_joined;
    public TextMesh Role_mesh;
    public TextMesh Message_mesh;
    public TextMesh MessageRole_mesh;
    public TextMesh Submit_mesh;
    public TextMesh Return_mesh;
    public TextMesh Moon_number;
    public KMSelectable[] RoleArrows;
    public KMSelectable RolesDisplay;
    public KMSelectable[] MessageArrows;
    public KMSelectable MessagesDisplay;
    public KMSelectable[] MessageRoleArrows;
    public KMSelectable MessageRoleDisplay;
    public KMSelectable Display;
    public KMSelectable Submit;
    public KMSelectable Return;
    public KMSelectable BUTTon;
    public GameObject Roles;
    public GameObject Arrows_to_Players;
    public GameObject[] Player1_Arrows, Player2_Arrows, Player3_Arrows, Player4_Arrows, Player5_Arrows, Player6_Arrows, Player7_Arrows, Player8_Arrows;
    public AudioClip[] audioclips;

    //Scripts
    public ResourceScript resource;

    private bool modsolved = false;
    static int _modIDCount = 1;
    public int _modID;
    private bool islistokay = false;
    List<string> messagessolved = new List<string>();
    List<string> messagessubmit = new List<string>();
    public string selectedrole;
    public string desiredrole;
    int counter = 0;
    public int index = 0;
    public int index1 = 0;
    public int night = 0;
    bool isSheriff, isConsig, isTracker, isEscort, isConsort = false;
    bool[] isPlayersub = new bool[8];
    bool ismoonpressed = false;
    bool isRoleSelected = false;
    public bool isAnimating = false;

    // Use this for initialization
    private void Awake()
    {
        _modID = _modIDCount++;
    }

    void Start()
    {
        StartCoroutine(UpdateHighlights());
        resource = new ResourceScript(Bomb,_modID,this);
        Audio.PlaySoundAtTransform("Selection", transform);
        StartCoroutine(resource.Name_Chooser(Player_joined));
        resource.Top_letters = new List<char>();
        resource.Bottom_letters = new List<char>();
        resource.Right_letters = new List<char>();
        resource.Left_letters = new List<char>();
        for (int i = 0; i <= 7; i++)
        {
            if (i == 0 || i == 1)
            {
                GenerateLetter(1, i);
            }
            if (i == 2 || i == 3)
            {
                GenerateLetter(2, i);
            }
            if (i == 4 || i == 5)
            {
                GenerateLetter(3, i);
            }
            if (i == 6 || i == 7)
            {
                GenerateLetter(4, i);
            }
        }
        Moon_button.OnInteract += delegate () { if (!ismoonpressed && !isAnimating) { ismoonpressed = true; resource.Roles_Fight(); } return false; };
        foreach(KMSelectable button in Players)
        {
            button.OnInteract += delegate () { if (ismoonpressed) { Submission(button); } else { GetComponent<KMBombModule>().HandleStrike(); Debug.LogFormat("[Town of KTaNE #{0}] It clearly states in the manual that when you are READY you should CLICK on the MOON first!", _modID); } return false; };
            button.OnHighlight += delegate () { if (ismoonpressed) { Player_joined.text = resource.Players_name[Array.IndexOf(Players, button)]; } return; };
            button.OnHighlightEnded += delegate () { if (ismoonpressed) { Player_joined.text = ""; } return; };
        }
        foreach(KMSelectable arrow in RoleArrows)
        {
            arrow.OnInteract += delegate () { RoleArrowPress(arrow); return false; };
        }
        foreach (KMSelectable msg in MessageArrows)
        {
            msg.OnInteract += delegate () { MessageArrowPress(msg); return false; };
        }
        foreach(KMSelectable msgrole in MessageRoleArrows)
        {
            msgrole.OnInteract += delegate () { MessageRoleArrowPress(msgrole); return false; };
        }
        Submit.OnInteract += delegate () { SubmitBUTT(); return false; };
        Return.OnInteract += delegate () { ReturntoGame(); return false; };
        MessagesDisplay.OnInteract += delegate () { MessageDisplay(); return false; };
        MessageRoleDisplay.OnInteract += delegate () { MessagesRoleDisplay(); return false; };
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (!modsolved)
        {
            if (!resource.isnamespresent 
                && !resource.roles_chosen)
            {
                resource.roles_chosen = true;
                resource.GetRoles(Players);
            }
            if (!resource.isarrowschosen && resource.roles_chosen)
            {
                Visiting();
            }
        }
    }

    void GenerateLetter(int n, int i)
    {
        if (n == 1)
        {
            int random = rnd.Range(0, 26);
            char c = resource.AlphabetLetters[random];
            while (c.EqualsAny('B', 'F', 'P', 'Q', 'R', 'S', 'T', 'V', 'X') && !resource.checkedbool)
            {
                switch (c)
                {
                    case 'B':
                        if (resource.checkedletter[0]) { random = rnd.Range(0, 26); c = resource.AlphabetLetters[random]; break; }
                        resource.checkedbool = true;
                        resource.checkedletter[0] = true;
                        break;
                    case 'F':
                        if (resource.checkedletter[1]) { random = rnd.Range(0, 26); c = resource.AlphabetLetters[random]; break; }
                        resource.checkedbool = true;
                        resource.checkedletter[1] = true;
                        break;
                    case 'P':
                        if (resource.checkedletter[2]) { random = rnd.Range(0, 26); c = resource.AlphabetLetters[random]; break; }
                        resource.checkedbool = true;
                        resource.checkedletter[2] = true;
                        break;
                    case 'Q':
                        if (resource.checkedletter[3]) { random = rnd.Range(0, 26); c = resource.AlphabetLetters[random]; break; }
                        resource.checkedbool = true;
                        resource.checkedletter[3] = true;
                        break;
                    case 'R':
                        if (resource.checkedletter[4]) { random = rnd.Range(0, 26); c = resource.AlphabetLetters[random]; break; }
                        resource.checkedbool = true;
                        resource.checkedletter[4] = true;
                        break;
                    case 'S':
                        if (resource.checkedletter[5]) { random = rnd.Range(0, 26); c = resource.AlphabetLetters[random]; break; }
                        resource.checkedbool = true;
                        resource.checkedletter[5] = true;
                        break;
                    case 'T':
                        if (resource.checkedletter[6]) { random = rnd.Range(0, 26); c = resource.AlphabetLetters[random]; break; }
                        resource.checkedbool = true;
                        resource.checkedletter[6] = true;
                        break;
                    case 'V':
                        if (resource.checkedletter[7]) { random = rnd.Range(0, 26); c = resource.AlphabetLetters[random]; break; }
                        resource.checkedbool = true;
                        resource.checkedletter[7] = true;
                        break;
                    case 'X':
                        if (resource.checkedletter[8]) { random = rnd.Range(0, 26); c = resource.AlphabetLetters[random]; break; }
                        resource.checkedbool = true;
                        resource.checkedletter[8] = true;
                        break;
                    default:
                        break;
                }
            }
            resource.checkedbool = false;
            var temp = resource.AlphabetLetters[random].ToString();
            resource.player_letter[i] = Convert.ToChar(temp);
            Players_Letter[i].text = temp;
            resource.Top_letters.Add(resource.AlphabetLetters[random]);
        }
        if (n == 2)
        {
            int random = rnd.Range(0, 26);
            char c = resource.AlphabetLetters[random];
            while (c.EqualsAny('C', 'Q', 'Z', 'R', 'E', 'K', 'I', 'W', 'Y') && !resource.checkedbool)
            {
                switch (c)
                {
                    case 'C':
                        if (resource.checkedletter[0]) { random = rnd.Range(0, 26); c = resource.AlphabetLetters[random]; break; }
                        resource.checkedbool = true;
                        resource.checkedletter[0] = true;
                        break;
                    case 'Q':
                        if (resource.checkedletter[1]) { random = rnd.Range(0, 26); c = resource.AlphabetLetters[random]; break; }
                        resource.checkedbool = true;
                        resource.checkedletter[1] = true;
                        break;
                    case 'Z':
                        if (resource.checkedletter[2]) { random = rnd.Range(0, 26); c = resource.AlphabetLetters[random]; break; }
                        resource.checkedbool = true;
                        resource.checkedletter[2] = true;
                        break;
                    case 'R':
                        if (resource.checkedletter[3]) { random = rnd.Range(0, 26); c = resource.AlphabetLetters[random]; break; }
                        resource.checkedbool = true;
                        resource.checkedletter[3] = true;
                        break;
                    case 'E':
                        if (resource.checkedletter[4]) { random = rnd.Range(0, 26); c = resource.AlphabetLetters[random]; break; }
                        resource.checkedbool = true;
                        resource.checkedletter[4] = true;
                        break;
                    case 'K':
                        if (resource.checkedletter[5]) { random = rnd.Range(0, 26); c = resource.AlphabetLetters[random]; break; }
                        resource.checkedbool = true;
                        resource.checkedletter[5] = true;
                        break;
                    case 'I':
                        if (resource.checkedletter[6]) { random = rnd.Range(0, 26); c = resource.AlphabetLetters[random]; break; }
                        resource.checkedbool = true;
                        resource.checkedletter[6] = true;
                        break;
                    case 'W':
                        if (resource.checkedletter[7]) { random = rnd.Range(0, 26); c = resource.AlphabetLetters[random]; break; }
                        resource.checkedbool = true;
                        resource.checkedletter[7] = true;
                        break;
                    case 'Y':
                        if (resource.checkedletter[8]) { random = rnd.Range(0, 26); c = resource.AlphabetLetters[random]; break; }
                        resource.checkedbool = true;
                        resource.checkedletter[8] = true;
                        break;
                    default:
                        break;
                }
            }
            resource.checkedbool = false;
            var temp = resource.AlphabetLetters[random].ToString();
            resource.player_letter[i] = Convert.ToChar(temp);
            Players_Letter[i].text = temp;
            resource.Right_letters.Add(resource.AlphabetLetters[random]);
        }
        if (n == 3)
        {
            int random = rnd.Range(0, 26);
            char c = resource.AlphabetLetters[random];
            while (c.EqualsAny('M', 'G', 'J', 'U', 'W', 'P', 'A', 'N', 'V') && !resource.checkedbool)
            {
                switch (c)
                {
                    case 'M':
                        if (resource.checkedletter[0]) { random = rnd.Range(0, 26); c = resource.AlphabetLetters[random]; break; }
                        resource.checkedbool = true;
                        resource.checkedletter[0] = true;
                        break;
                    case 'G':
                        if (resource.checkedletter[1]) { random = rnd.Range(0, 26); c = resource.AlphabetLetters[random]; break; }
                        resource.checkedbool = true;
                        resource.checkedletter[1] = true;
                        break;
                    case 'J':
                        if (resource.checkedletter[2]) { random = rnd.Range(0, 26); c = resource.AlphabetLetters[random]; break; }
                        resource.checkedbool = true;
                        resource.checkedletter[2] = true;
                        break;
                    case 'U':
                        if (resource.checkedletter[3]) { random = rnd.Range(0, 26); c = resource.AlphabetLetters[random]; break; }
                        resource.checkedbool = true;
                        resource.checkedletter[3] = true;
                        break;
                    case 'W':
                        if (resource.checkedletter[4]) { random = rnd.Range(0, 26); c = resource.AlphabetLetters[random]; break; }
                        resource.checkedbool = true;
                        resource.checkedletter[4] = true;
                        break;
                    case 'P':
                        if (resource.checkedletter[5]) { random = rnd.Range(0, 26); c = resource.AlphabetLetters[random]; break; }
                        resource.checkedbool = true;
                        resource.checkedletter[5] = true;
                        break;
                    case 'A':
                        if (resource.checkedletter[6]) { random = rnd.Range(0, 26); c = resource.AlphabetLetters[random]; break; }
                        resource.checkedbool = true;
                        resource.checkedletter[6] = true;
                        break;
                    case 'N':
                        if (resource.checkedletter[7]) { random = rnd.Range(0, 26); c = resource.AlphabetLetters[random]; break; }
                        resource.checkedbool = true;
                        resource.checkedletter[7] = true;
                        break;
                    case 'V':
                        if (resource.checkedletter[8]) { random = rnd.Range(0, 26); c = resource.AlphabetLetters[random]; break; }
                        resource.checkedbool = true;
                        resource.checkedletter[8] = true;
                        break;
                    default:
                        break;
                }
            }
            resource.checkedbool = false;
            var temp = resource.AlphabetLetters[random].ToString();
            resource.player_letter[i] = Convert.ToChar(temp);
            Players_Letter[i].text = temp;
            resource.Bottom_letters.Add(resource.AlphabetLetters[random]);
        }
        if (n == 4)
        {
            int random = rnd.Range(0, 26);
            char c = resource.AlphabetLetters[random];
            while (c.EqualsAny('X', 'Y', 'G', 'I', 'L', 'M', 'W', 'K', 'A') && !resource.checkedbool)
            {
                switch (c)
                {
                    case 'X':
                        if (resource.checkedletter[0]) { random = rnd.Range(0, 26); c = resource.AlphabetLetters[random]; break; }
                        resource.checkedbool = true;
                        resource.checkedletter[0] = true;
                        break;
                    case 'Y':
                        if (resource.checkedletter[1]) { random = rnd.Range(0, 26); c = resource.AlphabetLetters[random]; break; }
                        resource.checkedbool = true;
                        resource.checkedletter[1] = true;
                        break;
                    case 'G':
                        if (resource.checkedletter[2]) { random = rnd.Range(0, 26); c = resource.AlphabetLetters[random]; break; }
                        resource.checkedbool = true;
                        resource.checkedletter[2] = true;
                        break;
                    case 'I':
                        if (resource.checkedletter[3]) { random = rnd.Range(0, 26); c = resource.AlphabetLetters[random]; break; }
                        resource.checkedbool = true;
                        resource.checkedletter[3] = true;
                        break;
                    case 'L':
                        if (resource.checkedletter[4]) { random = rnd.Range(0, 26); c = resource.AlphabetLetters[random]; break; }
                        resource.checkedbool = true;
                        resource.checkedletter[4] = true;
                        break;
                    case 'M':
                        if (resource.checkedletter[5]) { random = rnd.Range(0, 26); c = resource.AlphabetLetters[random]; break; }
                        resource.checkedbool = true;
                        resource.checkedletter[5] = true;
                        break;
                    case 'W':
                        if (resource.checkedletter[6]) { random = rnd.Range(0, 26); c = resource.AlphabetLetters[random]; break; }
                        resource.checkedbool = true;
                        resource.checkedletter[6] = true;
                        break;
                    case 'K':
                        if (resource.checkedletter[7]) { random = rnd.Range(0, 26); c = resource.AlphabetLetters[random]; break; }
                        resource.checkedbool = true;
                        resource.checkedletter[7] = true;
                        break;
                    case 'A':
                        if (resource.checkedletter[8]) { random = rnd.Range(0, 26); c = resource.AlphabetLetters[random]; break; }
                        resource.checkedbool = true;
                        resource.checkedletter[8] = true;
                        break;
                    default:
                        break;
                }
            }
            resource.checkedbool = false;
            var temp = resource.AlphabetLetters[random].ToString();
            resource.player_letter[i] = Convert.ToChar(temp);
            Players_Letter[i].text = temp;
            resource.Left_letters.Add(resource.AlphabetLetters[random]);
        }
    }

    void Visiting()
    {
        resource.isarrowschosen = true;
        for (int i = 0; i <= 7; i++)
        {
            if (resource.Players_roles[i].Equals("Veteran"))
            {
                continue;
            }
            int random = rnd.Range(0, 7);
            GameObject[] arrows = ReturnPlayerArrows(i);
            arrows[random].SetActive(true);
            resource.visiting_arrows[i] = int.Parse(arrows[random].name[16].ToString());
        }
        Debug.LogFormat("[Town of KTaNE #{0}] In clockwise order starting from Player 1, the arrows are {1},{2},{3},{4},{5},{6},{7},{8}",_modID, resource.visiting_arrows[0], resource.visiting_arrows[1], resource.visiting_arrows[2], resource.visiting_arrows[3], resource.visiting_arrows[4], resource.visiting_arrows[5], resource.visiting_arrows[6], resource.visiting_arrows[7]);

    }
    GameObject[] ReturnPlayerArrows(int i)
    {
        switch (i)
        {
            case 0:
                return Player1_Arrows;
            case 1:
                return Player2_Arrows;
            case 2:
                return Player3_Arrows;
            case 3:
                return Player4_Arrows;
            case 4:
                return Player5_Arrows;
            case 5:
                return Player6_Arrows;
            case 6:
                return Player7_Arrows;
            case 7:
                return Player8_Arrows;
        }
        return null;
    }

    void Submission(KMSelectable button)
    {
        this.BUTTon = button;
        string player = button.ToString();
        char c = player.ElementAt(7);
        var num = int.Parse(c.ToString());
        if (isPlayersub[num - 1] == true)
        {
            return;
        }
        foreach (KMSelectable player1 in Players)
        {
            player1.gameObject.SetActive(false);
        }
        Arrows_to_Players.SetActive(false);
        Moon_button.gameObject.SetActive(false);

        RolesDisplay.GetComponent<Renderer>().enabled = true;
        RolesDisplay.Highlight.gameObject.SetActive(true);
        Role_mesh.GetComponent<Renderer>().enabled = true;

        RoleArrows[0].GetComponent<Renderer>().enabled = true;
        RoleArrows[0].Highlight.gameObject.SetActive(true);

        RoleArrows[1].GetComponent<Renderer>().enabled = true;
        RoleArrows[1].Highlight.gameObject.SetActive(true);

        MessagesDisplay.GetComponent<Renderer>().enabled = true;
        MessagesDisplay.Highlight.gameObject.SetActive(true);
        Message_mesh.GetComponent<Renderer>().enabled = true;

        MessageArrows[0].GetComponent<Renderer>().enabled = true;
        MessageArrows[0].Highlight.gameObject.SetActive(true);

        MessageArrows[1].GetComponent<Renderer>().enabled = true;
        MessageArrows[1].Highlight.gameObject.SetActive(true);

        Submit.GetComponent<Renderer>().enabled = true;
        Submit.Highlight.gameObject.SetActive(true);
        Submit_mesh.GetComponent<Renderer>().enabled = true;

        Return.GetComponent<Renderer>().enabled = true;
        Return.Highlight.gameObject.SetActive(true);
        Return_mesh.GetComponent<Renderer>().enabled = true;

        desiredrole = resource.Players_roles[num - 1];

        RolesDisplay.OnInteract += delegate ()
        {
            selectedrole = Role_mesh.text;
            if(desiredrole == selectedrole)
            {
                switch (selectedrole)
                {
                    case "Sheriff":
                        isSheriff = true;
                        break;
                    case "Consigliere":
                        isConsig = true;
                        break;
                    case "Tracker":
                        isTracker = true;
                        break;
                    case "Escort":
                        isEscort = true;
                        break;
                    case "Consort":
                        isConsort = true;
                        break;
                    default:
                        break;
                }
                isRoleSelected = true;
            }
            else
            {
                GetComponent<KMBombModule>().HandleStrike();
            }
            return false;
        };
    }

    IEnumerator UpdateHighlights()
    {
        yield return new WaitForSeconds(0.7f);
        RoleArrows[0].Highlight.gameObject.SetActive(false);
        RoleArrows[1].Highlight.gameObject.SetActive(false);
        RolesDisplay.Highlight.gameObject.SetActive(false);
        MessageArrows[0].Highlight.gameObject.SetActive(false);
        MessageArrows[1].Highlight.gameObject.SetActive(false);
        MessagesDisplay.Highlight.gameObject.SetActive(false);
        MessageRoleArrows[0].Highlight.gameObject.SetActive(false);
        MessageRoleArrows[1].Highlight.gameObject.SetActive(false);
        MessageRoleDisplay.Highlight.gameObject.SetActive(false);
        Display.Highlight.gameObject.SetActive(false);
        Submit.Highlight.gameObject.SetActive(false);
        Return.Highlight.gameObject.SetActive(false);
        yield break;
    }

    void MessageDisplay()
    {
        if (!isRoleSelected)
        {
            GetComponent<KMBombModule>().HandleStrike();
            Debug.LogFormat("[Town of KTaNE #{0}] Once again, the manual CLEARLY states that a role must be selected first, but you decided to submit a message? Then have a strike :)",_modID);
        }
        var text = Message_mesh.text;
        bool isspecialmsg = false;
        if (text == "Your target's role is ")
        {
            MessageRoleArrows[0].Highlight.gameObject.SetActive(true);
            MessageRoleArrows[0].GetComponent<Renderer>().enabled = true;

            MessageRoleArrows[1].Highlight.gameObject.SetActive(true);
            MessageRoleArrows[1].GetComponent<Renderer>().enabled = true;

            MessageRoleDisplay.Highlight.gameObject.SetActive(true);
            MessageRoleDisplay.GetComponent<Renderer>().enabled = true;
            MessageRole_mesh.GetComponent<Renderer>().enabled = true;

            Display.Highlight.gameObject.SetActive(true);
            Display.GetComponent<Renderer>().enabled = true;
            isspecialmsg = true;
        }
        if (text == "Your target visited ")
        {
            MessageRoleArrows[0].Highlight.gameObject.SetActive(true);
            MessageRoleArrows[0].GetComponent<Renderer>().enabled = true;

            MessageRoleArrows[1].Highlight.gameObject.SetActive(true);
            MessageRoleArrows[1].GetComponent<Renderer>().enabled = true;

            MessageRoleDisplay.Highlight.gameObject.SetActive(true);
            MessageRoleDisplay.GetComponent<Renderer>().enabled = true;
            MessageRole_mesh.GetComponent<Renderer>().enabled = true;

            Display.Highlight.gameObject.SetActive(true);
            Display.GetComponent<Renderer>().enabled = true;
        }
        if (!isspecialmsg)
        {
            messagessolved.Add(text);
        }
    }
    
    void MessagesRoleDisplay()
    {
        var text24 = MessageRole_mesh.text;
        if(Message_mesh.text == "Your target's role is ")
        {
            var ressult = "Your target's role is " + MessageRole_mesh.text+".";
            messagessolved.Add(ressult);
        }
        else if (Message_mesh.text == "Your target visited ")
        {
            var ressult = "Your target visited " + MessageRole_mesh.text + ".";
            messagessolved.Add(ressult);
        }
    }

    void RoleArrowPress(KMSelectable butt)
    {
        int index69 = Array.IndexOf(RoleArrows,butt);
        switch (index69)
        {
            case 0:
                if (index == 0)
                {
                    index = 0;
                }
                else
                {
                    index--;
                }
                Role_mesh.text = resource.Roles_index[index];
                break;
            case 1:
                if (index == 25)
                {
                    index = 25;
                }
                else
                {
                    index++;
                }
                Role_mesh.text = resource.Roles_index[index];
                break;
        }
    }
    void MessageRoleArrowPress(KMSelectable butt)
    {
        int index69 = Array.IndexOf(MessageRoleArrows, butt);
        switch (index69)
        {
            case 0:
                if (index == 0)
                {
                    index = 0;
                }
                else
                {
                    index--;
                }
                MessageRole_mesh.text = resource.Roles_index[index];
                break;
            case 1:
                if (index == 25)
                {
                    index = 25;
                }
                else
                {
                    index++;
                }
                MessageRole_mesh.text = resource.Roles_index[index];
                break;
        }
    }
    void MessageArrowPress(KMSelectable butt)
    {
        int index420 = Array.IndexOf(MessageArrows, butt);
        switch (index420)
        {
            case 0:
                var text1 = Role_mesh.text;
                if (text1 == "Sheriff")
                {
                    if (index1 == 0)
                    {
                        index1 = 0;
                    }
                    else
                    {
                        index1--;
                    }
                    Message_mesh.text = resource.Sheriff_Messages[index1];
                }
                else if (text1 == "Consigliere")
                {
                    if (index1 == 0)
                    {
                        index1 = 0;
                    }
                    else
                    {
                        index1--;
                    }
                    Message_mesh.text = resource.Consig_Messages[index1];
                }
                else if (text1 == "Tracker")
                {
                    if (index1 == 0)
                    {
                        index1 = 0;
                    }
                    else
                    {
                        index1--;
                    }
                    Message_mesh.text = resource.Tracker_Messages[index1];
                }
                else if (text1 == "Escort")
                {
                    if (index1 == 0)
                    {
                        index1 = 0;
                    }
                    else
                    {
                        index1--;
                    }
                    Message_mesh.text = resource.Escort_Messages[index1];
                }
                else if (text1 == "Consort")
                {
                    if (index1 == 0)
                    {
                        index1 = 0;
                    }
                    else
                    {
                        index1--;
                    }
                    Message_mesh.text = resource.Escort_Messages[index1];
                }
                else
                {
                    if (index1 == 0)
                    {
                        index1 = 0;
                    }
                    else
                    {
                        index1--;
                    }
                    Message_mesh.text = resource.Fake_Messages[index1];
                }
                break;
            case 1:
                var text2 = Role_mesh.text;
                if (text2 == "Sheriff")
                {
                    if (index1 == 10)
                    {
                        index1 = 10;
                    }
                    else
                    {
                        index1++;
                    }
                    Message_mesh.text = resource.Sheriff_Messages[index1];
                }
                else if (text2 == "Consigliere")
                {
                    if (index1 == 9)
                    {
                        index1 = 9;
                    }
                    else
                    {
                        index1++;
                    }
                    Message_mesh.text = resource.Consig_Messages[index1];
                }
                else if (text2 == "Tracker")
                {
                    if (index == 9)
                    {
                        index = 9;
                    }
                    else
                    {
                        index1++;
                    }
                    Message_mesh.text = resource.Tracker_Messages[index1];
                }
                else if (text2 == "Escort")
                {
                    if (index1 == 9)
                    {
                        index1 = 9;
                    }
                    else
                    {
                        index1++;
                    }
                    Message_mesh.text = resource.Escort_Messages[index1];
                }
                else if (text2 == "Consig")
                {
                    if (index1 == 9)
                    {
                        index1 = 9;
                    }
                    else
                    {
                        index1++;
                    }
                    Message_mesh.text = resource.Escort_Messages[index1];
                }
                else
                {
                    if (index1 == 8)
                    {
                        index1 = 8;
                    }
                    else
                    {
                        index1++;
                    }
                    Message_mesh.text = resource.Fake_Messages[index1];
                }
                break;
        }
    }

    void SubmitBUTT()
    {
        string player = BUTTon.ToString();
        char c = player.ElementAt(7);
        var num = int.Parse(c.ToString());
        messagessubmit = new List<string>();
        foreach (var msg in resource.GetMessages()[num-1].Where(x => x != null))
        {
            if (resource.GetMessages()[num - 1].Where(x => x != null).Count() == 0)
            {
                continue;
            }
            messagessubmit.Add(msg.ToLower().Trim());
        }
        bool ismatched = false;
        for(int i = 0; i < messagessolved.Count; i++)
        {
            if(messagessolved.Count() > messagessubmit.Count())
            {
                break;
            }
            if(messagessubmit.Contains(messagessolved[i].ToLower().Trim()))
            {
                ismatched = true;
            }
        }
        if (messagessubmit.Count() == 0 && messagessolved.Count()==0)
        {
            ismatched = true;
        }
        if (!ismatched)
        {
            messagessolved.Clear();
            GetComponent<KMBombModule>().HandleStrike();
            return;
        }
        if (night == 4)
        {
            night = 4;
        }
        else
        {
            night = int.Parse(Moon_number.text);
            night++;
            Moon_number.text = night.ToString();
        }
        if (night == 4)
        {
            GetComponent<KMBombModule>().HandlePass();
        }
        foreach (KMSelectable player1 in Players)
        {
            player1.gameObject.SetActive(true);
        }
        Arrows_to_Players.SetActive(true);
        Moon_button.gameObject.SetActive(true);

        RolesDisplay.GetComponent<Renderer>().enabled = false;
        RolesDisplay.Highlight.gameObject.SetActive(false);
        Role_mesh.GetComponent<Renderer>().enabled = false;

        RoleArrows[0].GetComponent<Renderer>().enabled = false;
        RoleArrows[0].Highlight.gameObject.SetActive(false);

        RoleArrows[1].GetComponent<Renderer>().enabled = false;
        RoleArrows[1].Highlight.gameObject.SetActive(false);

        MessagesDisplay.GetComponent<Renderer>().enabled = false;
        MessagesDisplay.Highlight.gameObject.SetActive(false);
        Message_mesh.GetComponent<Renderer>().enabled = false;

        MessageArrows[0].GetComponent<Renderer>().enabled = false;
        MessageArrows[0].Highlight.gameObject.SetActive(false);

        MessageArrows[1].GetComponent<Renderer>().enabled = false;
        MessageArrows[1].Highlight.gameObject.SetActive(false);

        Display.GetComponent<Renderer>().enabled = false;
        Display.Highlight.gameObject.SetActive(false);

        Submit.GetComponent<Renderer>().enabled = false;
        Submit.Highlight.gameObject.SetActive(false);
        Submit_mesh.GetComponent<Renderer>().enabled = false;

        Return.GetComponent<Renderer>().enabled = false;
        Return.Highlight.gameObject.SetActive(false);
        Return_mesh.GetComponent<Renderer>().enabled = false;

        MessageRoleArrows[0].Highlight.gameObject.SetActive(false);
        MessageRoleArrows[0].GetComponent<Renderer>().enabled = false;

        MessageRoleArrows[1].Highlight.gameObject.SetActive(false);
        MessageRoleArrows[1].GetComponent<Renderer>().enabled = false;

        MessageRoleDisplay.Highlight.gameObject.SetActive(false);
        MessageRoleDisplay.GetComponent<Renderer>().enabled = false;
        MessageRole_mesh.GetComponent<Renderer>().enabled = false;
        StartCoroutine(UpdateHighlights());        
        index = 0;
        index1 = 0;
        foreach(var message in messagessubmit)
        {
            if(message == "You are dead!")
            {
                Players_Letter[num-1].color = new Color32(255, 255, 0, 255);
            }
        }
        if (isPlayersub[num - 1] == true)
        {
            return;
        }
        else
        {
            Players_Letter[num - 1].color = new Color32(0, 255, 0, 255);
            isPlayersub[num - 1] = true;
        }
        messagessolved.Clear();
    }

    void ReturntoGame()
    {
        foreach (KMSelectable player1 in Players)
        {
            player1.gameObject.SetActive(true);
        }
        Arrows_to_Players.SetActive(true);
        Moon_button.gameObject.SetActive(true);

        RolesDisplay.GetComponent<Renderer>().enabled = false;
        RolesDisplay.Highlight.gameObject.SetActive(false);
        Role_mesh.GetComponent<Renderer>().enabled = false;

        RoleArrows[0].GetComponent<Renderer>().enabled = false;
        RoleArrows[0].Highlight.gameObject.SetActive(false);

        RoleArrows[1].GetComponent<Renderer>().enabled = false;
        RoleArrows[1].Highlight.gameObject.SetActive(false);

        MessagesDisplay.GetComponent<Renderer>().enabled = false;
        MessagesDisplay.Highlight.gameObject.SetActive(false);
        Message_mesh.GetComponent<Renderer>().enabled = false;

        MessageArrows[0].GetComponent<Renderer>().enabled = false;
        MessageArrows[0].Highlight.gameObject.SetActive(false);

        MessageArrows[1].GetComponent<Renderer>().enabled = false;
        MessageArrows[1].Highlight.gameObject.SetActive(false);

        Display.GetComponent<Renderer>().enabled = false;
        Display.Highlight.gameObject.SetActive(false);

        Submit.GetComponent<Renderer>().enabled = false;
        Submit.Highlight.gameObject.SetActive(false);
        Submit_mesh.GetComponent<Renderer>().enabled = false;

        Return.GetComponent<Renderer>().enabled = false;
        Return.Highlight.gameObject.SetActive(false);
        Return_mesh.GetComponent<Renderer>().enabled = false;

        MessageRoleArrows[0].Highlight.gameObject.SetActive(false);
        MessageRoleArrows[0].GetComponent<Renderer>().enabled = false;

        MessageRoleArrows[1].Highlight.gameObject.SetActive(false);
        MessageRoleArrows[1].GetComponent<Renderer>().enabled = false;

        MessageRoleDisplay.Highlight.gameObject.SetActive(false);
        MessageRoleDisplay.GetComponent<Renderer>().enabled = false;
        MessageRole_mesh.GetComponent<Renderer>().enabled = false;
        StartCoroutine(UpdateHighlights());
        index = 0;
        index1 = 0;
    }

}
