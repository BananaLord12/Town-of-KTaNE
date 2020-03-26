using System.Collections;
using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;
using System.Linq;
using rnd = UnityEngine.Random;
using UnityEngine;
using KModkit;

public class Town_of_Roles : MonoBehaviour
{

    public KMAudio Audio;
    public KMBombInfo Bomb;
    public KMSelectable[] Players;
    public KMSelectable Moon_button;
    public TextMesh[] Players_Letter;
    public TextMesh Player_joined;
    public GameObject[] Player1_Arrows;
    public GameObject[] Player2_Arrows;
    public GameObject[] Player3_Arrows;
    public GameObject[] Player4_Arrows;
    public GameObject[] Player5_Arrows;
    public GameObject[] Player6_Arrows;
    public GameObject[] Player7_Arrows;
    public GameObject[] Player8_Arrows;

    string[] Roles_index = new string[] { "Ambusher", "Arsonist", "Bodyguard", "Bulletproof", "Consigliere", "Consort", "Sheriff", "Disguiser", "Doctor", "Escort", "Framer", "Godfather", "Guardian Angel", "Hypnotist", "Jailor", "Jester", "Lookout", "Mafioso", "Serial Killer", "Spy", "Tracker", "Vampire", "Vampire Hunter", "Veteran", "Vigilante", "Werewolf" };
    string[] Players_roles = new string[8];
    string[] Players_name = new string[8];
    char[] player_letter = new char[8];
    int[] role_prios;

    List<char> Top_letters, Bottom_letters, Right_letters, Left_letters;
    bool isnamespresent = false;
    private bool modsolved = false;
    bool roles_chosen = false;
    bool isarrowschosen = false;
    bool[] checkedletter = new bool[9];
    bool isambattdead = false;
    bool checkedbool = false;
    bool isroleblocked = false;
    int x;
    int y;


    //Role,priority level, can visit, attack, defence
    string[][] Roles = new string[][] { new string[] { "Ambusher", "3", "Yes", "Basic", "None" },
                                        new string[] { "Arsonist", "5", "Yes", "None", "Basic"},
                                        new string[] { "Bodyguard", "3", "Yes", "Basic", "None"},
                                        new string[] { "Bulletproof", "0", "Yes", "None", "Powerful"},
                                        new string[] { "Consigliere", "4", "Yes", "None","None"},
                                        new string[] { "Consort", "2", "Yes", "None","None"},
                                        new string[] { "Sheriff", "4", "Yes", "None","None"},
                                        new string[] { "Disguiser", "3", "Yes", "None","None"},
                                        new string[] { "Doctor", "2", "Yes", "None", "None"},
                                        new string[] { "Escort", "2", "Yes", "None", "None"},
                                        new string[] { "Framer", "3", "Yes", "None", "None"},
                                        new string[] { "Godfather", "5", "Yes", "Basic","Basic"},
                                        new string[] { "Guardian Angel", "3", "Yes", "None", "None"},
                                        new string[] { "Hypnotist", "3", "Yes", "None","None"},
                                        new string[] { "Jailor", "1", "Yes", "None", "Powerful"},
                                        new string[] { "Jester", "6", "Yes", "None","None"},
                                        new string[] { "Lookout", "3", "Yes", "None","None"},
                                        new string[] { "Mafioso", "5", "Yes", "Basic","None"},
                                        new string[] { "Serial Killer", "0", "Yes", "Basic","Basic"},
                                        new string[] { "Spy", "6", "Yes", "None","None"},
                                        new string[] { "Tracker", "4", "Yes", "None","None"},
                                        new string[] { "Vampire", "5", "Yes", "Basic","None"},
                                        new string[] { "Vampire Hunter", "5", "Yes", "Basic","None"},
                                        new string[] { "Veteran", "0", "No", "Basic","None"},
                                        new string[] { "Vigilante", "5", "Yes", "Basic","None"},
                                        new string[] { "Werewolf", "5", "Yes", "Powerful","Basic"} };
    string[] Player_Names = new string[] { "BananaLord", "Sam", "Limeboy", "Katarina", "EpicToast", "LeGeND", "samfun", "Strike", "Cooldoom5", "Weird", "Peanut", "Pruz", "Kusane", "Deaf", "Finder", "Jack", "Fang", "CrunchyBot", "Mr. Porcu", "Emik", "Danny", "Zanu", "Andrio", "LordKabewm", "Usernam3", "Tach", "Vinco", "Qkrisi", "yabbaguy", "Spookdood", "Grunkle", "Nico", "GhostSalt","Betshet" };
    string[][] Messages = new string[8][];
    string[] Target_msg = new string[100];
    int[] visiting_arrows = new int[8];
    string AlphabetLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    // Use this for initialization
    void Start()
    {
        StartCoroutine(Name_Chooser());
        Top_letters = new List<char>();
        Bottom_letters = new List<char>();
        Right_letters = new List<char>();
        Left_letters = new List<char>();
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
    }
    void GenerateLetter(int n, int i)
    {
        if (n == 1)
        {
            int random = rnd.Range(0, 26);
            char c = AlphabetLetters[random];
            while (c.EqualsAny('B', 'F', 'P', 'Q', 'R', 'S', 'T', 'V', 'X') && !checkedbool)
            {
                switch (c)
                {
                    case 'B':
                        if (checkedletter[0]) { random = rnd.Range(0, 26); c = AlphabetLetters[random]; break; }
                        checkedbool = true;
                        checkedletter[0] = true;
                        break;
                    case 'F':
                        if (checkedletter[1]) { random = rnd.Range(0, 26); c = AlphabetLetters[random]; break; }
                        checkedbool = true;
                        checkedletter[1] = true;
                        break;
                    case 'P':
                        if (checkedletter[2]) { random = rnd.Range(0, 26); c = AlphabetLetters[random]; break; }
                        checkedbool = true;
                        checkedletter[2] = true;
                        break;
                    case 'Q':
                        if (checkedletter[3]) { random = rnd.Range(0, 26); c = AlphabetLetters[random]; break; }
                        checkedbool = true;
                        checkedletter[3] = true;
                        break;
                    case 'R':
                        if (checkedletter[4]) { random = rnd.Range(0, 26); c = AlphabetLetters[random]; break; }
                        checkedbool = true;
                        checkedletter[4] = true;
                        break;
                    case 'S':
                        if (checkedletter[5]) { random = rnd.Range(0, 26); c = AlphabetLetters[random]; break; }
                        checkedbool = true;
                        checkedletter[5] = true;
                        break;
                    case 'T':
                        if (checkedletter[6]) { random = rnd.Range(0, 26); c = AlphabetLetters[random]; break; }
                        checkedbool = true;
                        checkedletter[6] = true;
                        break;
                    case 'V':
                        if (checkedletter[7]) { random = rnd.Range(0, 26); c = AlphabetLetters[random]; break; }
                        checkedbool = true;
                        checkedletter[7] = true;
                        break;
                    case 'X':
                        if (checkedletter[8]) { random = rnd.Range(0, 26); c = AlphabetLetters[random]; break; }
                        checkedbool = true;
                        checkedletter[8] = true;
                        break;
                    default:
                        break;
                }
            }
            checkedbool = false;
            var temp = AlphabetLetters[random].ToString();
            player_letter[i] = Convert.ToChar(temp);
            Players_Letter[i].text = temp;
            Top_letters.Add(AlphabetLetters[random]);
        }
        if (n == 2)
        {
            int random = rnd.Range(0, 26);
            char c = AlphabetLetters[random];
            while (c.EqualsAny('C', 'Q', 'Z', 'R', 'E', 'K', 'I', 'W', 'Y') && !checkedbool)
            {
                switch (c)
                {
                    case 'C':
                        if (checkedletter[0]) { random = rnd.Range(0, 26); c = AlphabetLetters[random]; break; }
                        checkedbool = true;
                        checkedletter[0] = true;
                        break;
                    case 'Q':
                        if (checkedletter[1]) { random = rnd.Range(0, 26); c = AlphabetLetters[random]; break; }
                        checkedbool = true;
                        checkedletter[1] = true;
                        break;
                    case 'Z':
                        if (checkedletter[2]) { random = rnd.Range(0, 26); c = AlphabetLetters[random]; break; }
                        checkedbool = true;
                        checkedletter[2] = true;
                        break;
                    case 'R':
                        if (checkedletter[3]) { random = rnd.Range(0, 26); c = AlphabetLetters[random]; break; }
                        checkedbool = true;
                        checkedletter[3] = true;
                        break;
                    case 'E':
                        if (checkedletter[4]) { random = rnd.Range(0, 26); c = AlphabetLetters[random]; break; }
                        checkedbool = true;
                        checkedletter[4] = true;
                        break;
                    case 'K':
                        if (checkedletter[5]) { random = rnd.Range(0, 26); c = AlphabetLetters[random]; break; }
                        checkedbool = true;
                        checkedletter[5] = true;
                        break;
                    case 'I':
                        if (checkedletter[6]) { random = rnd.Range(0, 26); c = AlphabetLetters[random]; break; }
                        checkedbool = true;
                        checkedletter[6] = true;
                        break;
                    case 'W':
                        if (checkedletter[7]) { random = rnd.Range(0, 26); c = AlphabetLetters[random]; break; }
                        checkedbool = true;
                        checkedletter[7] = true;
                        break;
                    case 'Y':
                        if (checkedletter[8]) { random = rnd.Range(0, 26); c = AlphabetLetters[random]; break; }
                        checkedbool = true;
                        checkedletter[8] = true;
                        break;
                    default:
                        break;
                }
            }
            checkedbool = false;
            var temp = AlphabetLetters[random].ToString();
            player_letter[i] = Convert.ToChar(temp);
            Players_Letter[i].text = temp;
            Right_letters.Add(AlphabetLetters[random]);
        }
        if (n == 3)
        {
            int random = rnd.Range(0, 26);
            char c = AlphabetLetters[random];
            while (c.EqualsAny('M', 'G', 'J', 'U', 'W', 'P', 'A', 'N', 'V') && !checkedbool)
            {
                switch (c)
                {
                    case 'M':
                        if (checkedletter[0]) { random = rnd.Range(0, 26); c = AlphabetLetters[random]; break; }
                        checkedbool = true;
                        checkedletter[0] = true;
                        break;
                    case 'G':
                        if (checkedletter[1]) { random = rnd.Range(0, 26); c = AlphabetLetters[random]; break; }
                        checkedbool = true;
                        checkedletter[1] = true;
                        break;
                    case 'J':
                        if (checkedletter[2]) { random = rnd.Range(0, 26); c = AlphabetLetters[random]; break; }
                        checkedbool = true;
                        checkedletter[2] = true;
                        break;
                    case 'U':
                        if (checkedletter[3]) { random = rnd.Range(0, 26); c = AlphabetLetters[random]; break; }
                        checkedbool = true;
                        checkedletter[3] = true;
                        break;
                    case 'W':
                        if (checkedletter[4]) { random = rnd.Range(0, 26); c = AlphabetLetters[random]; break; }
                        checkedbool = true;
                        checkedletter[4] = true;
                        break;
                    case 'P':
                        if (checkedletter[5]) { random = rnd.Range(0, 26); c = AlphabetLetters[random]; break; }
                        checkedbool = true;
                        checkedletter[5] = true;
                        break;
                    case 'A':
                        if (checkedletter[6]) { random = rnd.Range(0, 26); c = AlphabetLetters[random]; break; }
                        checkedbool = true;
                        checkedletter[6] = true;
                        break;
                    case 'N':
                        if (checkedletter[7]) { random = rnd.Range(0, 26); c = AlphabetLetters[random]; break; }
                        checkedbool = true;
                        checkedletter[7] = true;
                        break;
                    case 'V':
                        if (checkedletter[8]) { random = rnd.Range(0, 26); c = AlphabetLetters[random]; break; }
                        checkedbool = true;
                        checkedletter[8] = true;
                        break;
                    default:
                        break;
                }
            }
            checkedbool = false;
            var temp = AlphabetLetters[random].ToString();
            player_letter[i] = Convert.ToChar(temp);
            Players_Letter[i].text = temp;
            Bottom_letters.Add(AlphabetLetters[random]);
        }
        if (n == 4)
        {
            int random = rnd.Range(0, 26);
            char c = AlphabetLetters[random];
            while (c.EqualsAny('X', 'Y', 'G', 'I', 'L', 'M', 'W', 'K', 'A') && !checkedbool)
            {
                switch (c)
                {
                    case 'X':
                        if (checkedletter[0]) { random = rnd.Range(0, 26); c = AlphabetLetters[random]; break; }
                        checkedbool = true;
                        checkedletter[0] = true;
                        break;
                    case 'Y':
                        if (checkedletter[1]) { random = rnd.Range(0, 26); c = AlphabetLetters[random]; break; }
                        checkedbool = true;
                        checkedletter[1] = true;
                        break;
                    case 'G':
                        if (checkedletter[2]) { random = rnd.Range(0, 26); c = AlphabetLetters[random]; break; }
                        checkedbool = true;
                        checkedletter[2] = true;
                        break;
                    case 'I':
                        if (checkedletter[3]) { random = rnd.Range(0, 26); c = AlphabetLetters[random]; break; }
                        checkedbool = true;
                        checkedletter[3] = true;
                        break;
                    case 'L':
                        if (checkedletter[4]) { random = rnd.Range(0, 26); c = AlphabetLetters[random]; break; }
                        checkedbool = true;
                        checkedletter[4] = true;
                        break;
                    case 'M':
                        if (checkedletter[5]) { random = rnd.Range(0, 26); c = AlphabetLetters[random]; break; }
                        checkedbool = true;
                        checkedletter[5] = true;
                        break;
                    case 'W':
                        if (checkedletter[6]) { random = rnd.Range(0, 26); c = AlphabetLetters[random]; break; }
                        checkedbool = true;
                        checkedletter[6] = true;
                        break;
                    case 'K':
                        if (checkedletter[7]) { random = rnd.Range(0, 26); c = AlphabetLetters[random]; break; }
                        checkedbool = true;
                        checkedletter[7] = true;
                        break;
                    case 'A':
                        if (checkedletter[8]) { random = rnd.Range(0, 26); c = AlphabetLetters[random]; break; }
                        checkedbool = true;
                        checkedletter[8] = true;
                        break;
                    default:
                        break;
                }
            }
            checkedbool = false;
            var temp = AlphabetLetters[random].ToString();
            player_letter[i] = Convert.ToChar(temp);
            Players_Letter[i].text = temp;
            Left_letters.Add(AlphabetLetters[random]);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!modsolved)
        {
            if (!isnamespresent && !roles_chosen)
            {
                roles_chosen = true;
                GetRoles();
            }
            if (!isarrowschosen && roles_chosen)
            {
                Visiting();
            }
        }
    }

    IEnumerator Name_Chooser()
    {
        isnamespresent = true;
        int i = 0;
        while (isnamespresent)
        {
            if (i == 8)
            {
                Player_joined.text = "";
                isnamespresent = false;
                yield break;
            }
            string format = "{0} has joined the Town!";
            var random = rnd.Range(0, Player_Names.Length);
            while (Players_name.Contains(Player_Names[random]))
            {
                random = rnd.Range(0, Player_Names.Length);
            }
            string chosen_playername = Player_Names[random];
            Player_joined.text = String.Format(format, chosen_playername);
            Players_name[i] = chosen_playername;
            i++;
            yield return new WaitForSeconds(0.1f);
        }
    }

    void GetRoles()
    {
        foreach (KMSelectable Player in Players)
        {
            if (Player.ToString() == "Player_1 (KMSelectable)")
            {
                string role = getCollumn1Roles(player_letter[0]);
                Debug.LogFormat("<Town of Idiots> {0}'s assinged role is {1}", Players_name[0], role);
                Players_roles[0] = role;
            }
            else if (Player.ToString() == "Player_2 (KMSelectable)")
            {
                string role = getCollumn1Roles(player_letter[1]);
                Debug.LogFormat("<Town of Idiots> {0}'s assinged role is {1}", Players_name[1], role);
                Players_roles[1] = role;
            }
            else if (Player.ToString() == "Player_3 (KMSelectable)")
            {
                string role = getCollumn3Roles(player_letter[2]);
                Debug.LogFormat("<Town of Idiots> {0}'s assinged role is {1}", Players_name[2], role);
                Players_roles[2] = role;
            }
            else if (Player.ToString() == "Player_4 (KMSelectable)")
            {
                string role = getCollumn3Roles(player_letter[3]);
                Debug.LogFormat("<Town of Idiots> {0}'s assinged role is {1}", Players_name[3], role);
                Players_roles[3] = role;
            }
            else if (Player.ToString() == "Player_5 (KMSelectable)")
            {
                string role = getCollumn2Roles(player_letter[4]);
                Debug.LogFormat("<Town of Idiots> {0}'s assinged role is {1}", Players_name[4], role);
                Players_roles[4] = role;
            }
            else if (Player.ToString() == "Player_6 (KMSelectable)")
            {
                string role = getCollumn2Roles(player_letter[5]);
                Debug.LogFormat("<Town of Idiots> {0}'s assinged role is {1}", Players_name[5], role);
                Players_roles[5] = role;
            }
            else if (Player.ToString() == "Player_7 (KMSelectable)")
            {
                string role = getCollumn4Roles(player_letter[6]);
                Debug.LogFormat("<Town of Idiots> {0}'s assinged role is {1}", Players_name[6], role);
                Players_roles[6] = role;
            }
            else if (Player.ToString() == "Player_8 (KMSelectable)")
            {
                string role = getCollumn4Roles(player_letter[7]);
                Debug.LogFormat("<Town of Idiots> {0}'s assinged role is {1}", Players_name[7], role);
                Players_roles[7] = role;
            }
        }
        Debug.LogFormat("<Town of Idiots> All of the player's assigned roles are {0},{1},{2},{3},{4},{5},{6},{7}", Players_roles[0], Players_roles[1], Players_roles[2], Players_roles[3], Players_roles[4], Players_roles[5], Players_roles[6], Players_roles[7]);
    }

    void Visiting()
    {
        isarrowschosen = true;
        for (int i = 0; i <= 7; i++)
        {
            if (Players_roles[i].Equals("Veteran"))
            {
                continue;
            }
            int random = rnd.Range(0, 7);
            GameObject[] arrows = ReturnPlayerArrows(i);
            arrows[random].SetActive(true);
            visiting_arrows[i] = int.Parse(arrows[random].name[16].ToString());
        }
        Debug.LogFormat("<Town of Idiots> In clockwise order starting from Player 1, the arrows are {0},{1},{2},{3},{4},{5},{6},{7}", visiting_arrows[0], visiting_arrows[1], visiting_arrows[2], visiting_arrows[3], visiting_arrows[4], visiting_arrows[5], visiting_arrows[6], visiting_arrows[7]);
        Roles_Fight();
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

    void Roles_Fight()
    {
        for (int i = 0; i <= 7; i++)
        {
            switch (Players_roles[i])
            {
                case "Ambusher":
                    int x = 0;
                    List<string> targeted_roles = new List<string>();
                    List<int> targetedrole_pos = new List<int>();
                    List<int> escort_pos = new List<int>();

                    //checks if there is anyone visiting the ambusher's target
                    foreach (int arrow in visiting_arrows)
                    {
                        int Ambusher_arrow = visiting_arrows[i];
                        if (i == x)
                        {
                            x++;
                            continue;
                        }
                        if (arrow == (i + 1))
                        {
                            string target_visited = Players_roles[x];
                            if (target_visited.EqualsAny("Escort", "Consort"))
                            {
                                isroleblocked = true;
                                Debug.Log("<Town of Idiots> The Ambusher has been roleblock by Escort");
                            }
                            else
                            {
                                Debug.LogFormat("<Town of Idiots> The Ambusher has been visited by {0}", target_visited);
                            }
                        }
                        if (arrow == Ambusher_arrow)
                        {
                            string target_visited = Players_roles[x];
                            targeted_roles.Add(target_visited);
                            targetedrole_pos.Add(x);
                            Debug.LogFormat("<Town of Idiots> The targets that the Ambusher has targeted are {0}", target_visited);
                        }
                        x++;
                    }
                    if (isroleblocked)
                    {
                        break;
                    }
                    if (targeted_roles.Count >= 1)
                    {
                        Debug.Log(targeted_roles.Count);
                        int[] role_prios = new int[targeted_roles.Count];
                        List<string[]> Role_info = new List<string[]>();
                        x = 0;
                        foreach (string role in targeted_roles)
                        {
                            int role_index = Array.IndexOf(Roles_index, role);
                            string[] role_info = Roles[role_index];
                            Role_info.Add(role_info);
                            role_prios[x] = int.Parse(role_info[1]);
                            x++;
                        }
                        int[] rp_copy = new int[targeted_roles.Count];
                        role_prios.CopyTo(rp_copy, 0);
                        Array.Sort(rp_copy);
                        foreach(int rp in rp_copy)
                        {
                            Debug.Log(rp);
                        }
                        int targetedpos = targetedrole_pos.ElementAt(Array.IndexOf(role_prios, rp_copy[0]));
                        string def = Role_info.ElementAt(Array.IndexOf(role_prios, rp_copy[0]))[4];
                        Debug.Log(def);
                        string visitedrole;
                        x = 0;
                        foreach (int arrow in visiting_arrows)
                        {
                            int Targeted_arrow = visiting_arrows[targetedpos];
                            if (i == x)
                            {
                                x++;
                                continue;
                            }
                            if (arrow == Targeted_arrow)
                            {
                                visitedrole = Players_roles[x];
                                if (Players_roles[x].EqualsAny("Mafioso", "Consigliere", "Godfather", "Consort", "Disguiser", "Framer", "Hypnotist"))
                                {
                                    break;
                                }
                                Debug.LogFormat("<Town of Idiots> The Role that the Ambusher is attacking is {0}", Players_roles[x]);
                                break;
                            }
                            x++;
                        }
                        bool targetprot = false;
                        int y = 0;
                        foreach (int arrow in visiting_arrows)
                        {
                            if(y == visiting_arrows[i])
                            {
                                y++;
                                continue;
                            }
                            if (y == x)
                            {
                                string role = Players_roles[y];
                                Debug.Log(role);
                                if (def == "None")
                                {
                                    Target_msg[targetedpos] = "You are dead!";
                                    Debug.Log("Target has died" + targetedpos);
                                    break;
                                }
                                if (role.Equals("Doctor"))
                                {
                                    Target_msg[0] = "You were attacked and healed by a doctor!";
                                    Debug.Log("You tried to attack the target but it was healed by Doctor");
                                    targetprot = true;
                                    break;
                                }
                                else if (role.Equals("Bodyguard"))
                                {
                                    Target_msg[1] = "You were attacked and protected by a bodyguard!";
                                    Debug.Log("You tried to attack the target but it was protected by BG");
                                    targetprot = true;
                                    break;
                                }
                                else if (role.Equals("Guardian Angel"))
                                {
                                    Target_msg[2] = "You were attacked and healed by a Guardian Angel!";
                                    Debug.Log("You tried to attack the target but it was healed by GA");
                                    targetprot = true;
                                    break;
                                }
                                break;
                            }
                            y++;
                        }
                    }
                    break;
                case "Arsonist":
                    var Arsonist_arrow = visiting_arrows[i];
                    Debug.LogFormat("The Arsonist is visiting {0}",Arsonist_arrow);
                    Debug.LogFormat("The Arsonist has doused {0}", Players_roles[Arsonist_arrow-1]);
                    break;
                case "Bodyguard":
                    x = 0;
                    targeted_roles = new List<string>();
                    targetedrole_pos = new List<int>();
                    foreach (int arrow in visiting_arrows)
                    {
                        int Bodyguard_arrow = visiting_arrows[i];
                        if (i == x)
                        {
                            x++;
                            continue;
                        }
                        if (arrow == (i + 1))
                        {
                            string target_visited = Players_roles[x];
                            if (target_visited.EqualsAny("Escort", "Consort"))
                            {
                                isroleblocked = true;
                                Debug.Log("<Town of Idiots> The Bodyguard has been roleblock by Escort/Consort");
                                break;
                            }
                        }
                        if(arrow == Bodyguard_arrow)
                        {
                            string target_visited = Players_roles[x];
                            targeted_roles.Add(target_visited);
                            targetedrole_pos.Add(x);
                            Debug.LogFormat("<Town of Idiots> The Bodyguard's target is protected from Basic and Powerful attacks and will fight {0}",target_visited);
                            break;
                        }
                        x++;
                    }
                    int[] role_prios1 = new int[targeted_roles.Count];
                    List<string[]> Role_info1 = new List<string[]>();
                    x = 0;
                    foreach (string role in targeted_roles)
                    {
                        int role_index1 = Array.IndexOf(Roles_index, role);
                        string[] role_info1 = Roles[role_index1];
                        Role_info1.Add(role_info1);
                        role_prios1[x] = int.Parse(role_info1[1]);
                        x++;
                    }
                    int[] rp_copy1 = new int[targeted_roles.Count];
                    role_prios1.CopyTo(rp_copy1, 0);
                    Array.Sort(rp_copy1);
                    int targetedpos1 = targetedrole_pos.ElementAt(Array.IndexOf(role_prios1, rp_copy1[0]));
                    string def1 = Role_info1.ElementAt(Array.IndexOf(role_prios1, rp_copy1[0]))[4];
                    Debug.Log(def1);
                    y = 0;
                    foreach (int arrow in visiting_arrows)
                    {
                        if (y == visiting_arrows[i])
                        {
                            y++;
                            continue;
                        }
                        if (y == x)
                        {
                            string role = Players_roles[y];
                            Debug.Log(role);
                            if (def1 == "None")
                            {
                                Target_msg[targetedpos1] = "You are dead!";
                                Debug.Log("Target has died" + targetedpos1);
                                break;
                            }
                            if (role.Equals("Doctor"))
                            {
                                Target_msg[0] = "You were attacked and healed by a doctor!";
                                Debug.Log("You tried to attack the target but it was healed by Doctor");
                                break;
                            }
                            else if (role.Equals("Bodyguard"))
                            {
                                Target_msg[1] = "You were attacked and protected by a bodyguard!";
                                Debug.Log("You tried to attack the target but it was protected by BG");
                                break;
                            }
                            else if (role.Equals("Guardian Angel"))
                            {
                                Target_msg[2] = "You were attacked and healed by a Guardian Angel!";
                                Debug.Log("You tried to attack the target but it was healed by GA");
                                break;
                            }
                            break;
                        }
                        y++;
                    }
                    break;
                case "Bulletproof":
                    Debug.LogFormat("The Bulletproof is visiting someone");
                    break;
                case "Consigliere":
                    Debug.LogFormat("The Consigliere is visiting someone");
                    break;
                case "Consort":
                    Debug.LogFormat("The Consort is visiting someone");
                    break;
                case "Sheriff":
                    Debug.LogFormat("The Sheriff is visiting someone");
                    break;
                case "Disguiser":
                    Debug.LogFormat("The Digsuiser is visiting someone");
                    break;
                case "Doctor":
                    Debug.LogFormat("The Doctor is visiting someone");
                    break;
                case "Escort":
                    Debug.LogFormat("The Escort is visiting someone");
                    break;
                case "Framer":
                    Debug.LogFormat("The Framer is visiting someone");
                    break;
                case "Godfather":
                    Debug.LogFormat("The Godfather is visiting someone");
                    break;
                case "Guardian Angel":
                    Debug.LogFormat("The Guardian Angel is visiting someone");
                    break;
                case "Hypnotist":
                    Debug.LogFormat("The Hypnotist is visiting someone");
                    break;
                case "Jailor":
                    Debug.LogFormat("The Jailor is visiting someone");
                    break;
                case "Jester":
                    Debug.LogFormat("The Jester is visiting someone");
                    break;
                case "Lookout":
                    Debug.LogFormat("The Lookout is visiting someone");
                    break;
                case "Mafioso":
                    Debug.LogFormat("The Mafioso is visiting someone");
                    break;
                case "Serial Killer":
                    Debug.LogFormat("The Serial Killer is visiting someone");
                    break;
                case "Spy":
                    Debug.LogFormat("The Spy is visiting someone");
                    break;
                case "Tracker":
                    Debug.LogFormat("The Tracker is visiting someone");
                    break;
                case "Vampire":
                    Debug.LogFormat("The Vampire is visiting someone");
                    break;
                case "Vampire Hunter":
                    Debug.LogFormat("The Vampire Hunter is visiting someone");
                    break;
                case "Veteran":
                    Debug.LogFormat("The Visiting is visiting noone");
                    break;
                case "Vigilante":
                    Debug.LogFormat("The Vigilante is visiting someone");
                    break;
                case "Werewolf":
                    Debug.LogFormat("The Werewolf is visiting someone");
                    break;

            }
        }
    }

    public string getCollumn1Roles(char l)
    {
        switch (l)
        {
            case 'A':
                return "Sheriff";
            case 'B':
                return "Mafioso";
            case 'C':
                return "Doctor";
            case 'D':
                return "Framer";
            case 'E':
                return "Vigilante";
            case 'F':
                return "Godfather";
            case 'G':
                return "Escort";
            case 'H':
                return "Serial Killer";
            case 'I':
                return "Lookout";
            case 'J':
                return "Hypnotist";
            case 'K':
                return "Consigliere";
            case 'L':
                return "Consort";
            case 'M':
                return "Tracker";
            case 'N':
                return "Disguiser";
            case 'O':
                return "Bodyguard";
            case 'P':
                return "Jester";
            case 'Q':
                return "Veteran";
            case 'R':
                return "Werewolf";
            case 'S':
                return "Jailor";
            case 'T':
                return "Arsonist";
            case 'U':
                return "Bulletproof";
            case 'V':
                return "Ambusher";
            case 'W':
                return "Guardian Angel";
            case 'X':
                return "Vampire";
            case 'Y':
                return "Spy";
            case 'Z':
                return "Vampire Hunter";
        }
        return "";
    }

    public string getCollumn2Roles(char l)
    {
        switch (l)
        {
            case 'A':
                return "Arsonist";
            case 'B':
                return "Bulletproof";
            case 'C':
                return "Sheriff";
            case 'D':
                return "Doctor";
            case 'E':
                return "Escort";
            case 'F':
                return "Framer";
            case 'G':
                return "Godfather";
            case 'H':
                return "Hypnotist";
            case 'I':
                return "Consort";
            case 'J':
                return "Jester";
            case 'K':
                return "Guardian Angel";
            case 'L':
                return "Lookout";
            case 'M':
                return "Mafioso";
            case 'N':
                return "Ambusher";
            case 'O':
                return "Bodyguard";
            case 'P':
                return "Jailor";
            case 'Q':
                return "Vampire Hunter";
            case 'R':
                return "Spy";
            case 'S':
                return "Serial Killer";
            case 'T':
                return "Tracker";
            case 'U':
                return "Veteran";
            case 'V':
                return "Vampire";
            case 'W':
                return "Werewolf";
            case 'X':
                return "Vigilante";
            case 'Y':
                return "Consigliere";
            case 'Z':
                return "Disguiser";
        }
        return "";
    }

    public string getCollumn3Roles(char l)
    {
        switch (l)
        {
            case 'A':
                return "Disguiser";
            case 'B':
                return "Vampire Hunter";
            case 'C':
                return "Mafioso";
            case 'D':
                return "Spy";
            case 'E':
                return "Werewolf";
            case 'F':
                return "Lookout";
            case 'G':
                return "Bodyguard";
            case 'H':
                return "Bulletproof";
            case 'I':
                return "Arsonist";
            case 'J':
                return "Vigilante";
            case 'K':
                return "Jailor";
            case 'L':
                return "Hypnotist";
            case 'M':
                return "Escort";
            case 'N':
                return "Consort";
            case 'O':
                return "Serial Killer";
            case 'P':
                return "Doctor";
            case 'Q':
                return "Godfather";
            case 'R':
                return "Veteran";
            case 'S':
                return "Sheriff";
            case 'T':
                return "Framer";
            case 'U':
                return "Guardian Angel";
            case 'V':
                return "Tracker";
            case 'W':
                return "Ambusher";
            case 'X':
                return "Consigliere";
            case 'Y':
                return "Vampire";
            case 'Z':
                return "Jester";
        }
        return "";
    }

    public string getCollumn4Roles(char l)
    {
        switch (l)
        {
            case 'A':
                return "Vampire";
            case 'B':
                return "Vigilante";
            case 'C':
                return "Serial Killer";
            case 'D':
                return "Disguiser";
            case 'E':
                return "Consort";
            case 'F':
                return "Bulletproof";
            case 'G':
                return "Jester";
            case 'H':
                return "Consigliere";
            case 'I':
                return "Veteran";
            case 'J':
                return "Lookout";
            case 'K':
                return "Ambusher";
            case 'L':
                return "Werewolf";
            case 'M':
                return "Jailor";
            case 'N':
                return "Tracker";
            case 'O':
                return "Spy";
            case 'P':
                return "Escort";
            case 'Q':
                return "Doctor";
            case 'R':
                return "Bodyguard";
            case 'S':
                return "Sheriff";
            case 'T':
                return "Vampire Hunter";
            case 'U':
                return "Hypnotist";
            case 'V':
                return "Guardian Angel";
            case 'W':
                return "Arsonist";
            case 'X':
                return "Mafioso";
            case 'Y':
                return "Godfather";
            case 'Z':
                return "Framer";

        }
        return "";
    }
}
