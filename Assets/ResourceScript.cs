using System.Collections;
using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;
using System.Linq;
using rnd = UnityEngine.Random;
using UnityEngine;
using KModkit;

public class ResourceScript {

    public string[] Roles_index = new string[] { "Ambusher", "Arsonist", "Bodyguard", "Bulletproof", "Consigliere", "Consort", "Sheriff", "Disguiser", "Doctor", "Escort", "Framer", "Godfather", "Guardian Angel", "Hypnotist", "Jailor", "Jester", "Lookout", "Mafioso", "Serial Killer", "Spy", "Tracker", "Vampire", "Vampire Hunter", "Veteran", "Vigilante", "Werewolf" };
    public string[] Players_roles = new string[8];
    public string[] Players_name = new string[8];
    public char[] player_letter = new char[8];
    public int[] role_prios;
    public int spy_position;
    public KMBombInfo Bomb;
    public Town_of_KTaNE tok;

    public List<char> Top_letters, Bottom_letters, Right_letters, Left_letters;
    public bool isnamespresent = false;

    public bool roles_chosen = false;
    public bool isarrowschosen = false;
    public bool[] checkedletter = new bool[9];
    public bool isambattdead = false;
    public bool checkedbool = false;
    public bool isroleblocked = false;
    public bool isprotected = false;
    public bool[] isroleblock = new bool[8];
    public bool[] isjailed = new bool[8];
    public bool[] isframed = new bool[8];
    public bool ismafiamember;
    public bool isGodfatherpresent = false;
    public bool ismafiosopresent = false;
    public bool isspypresent = false;
    public bool ismessagesdone = false;
    public int _modID = 0;
    public List<int> Spy_pos = new List<int>();
    public int x;
    public int y;

    //Role,priority level, can visit, attack, defence
    public string[][] Roles = new string[][] { new string[] { "Ambusher", "3", "Yes", "Basic", "None" },
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
    public string[] Player_Names = new string[] { "BananaLord", "Sam", "Limeboy", "Katarina", "EpicToast", "LeGeND", "samfun", "Strike", "Cooldoom5", "Weird", "Peanut", "Pruz", "Kusane", "Deaf", "Finder", "Jack", "Fang", "CrunchyBot", "Mr. Porcu", "Emik", "Danny", "Zanu", "Andrio", "LordKabewm", "Usernam3", "Tach", "Vinco", "Qkrisi", "yabbaguy", "Spookdood", "Grunkle", "Nico", "GhostSalt", "Betshet", "DragonManiac","Xmaster","Arceus", "PlaymationWizz" };
    public string[] Fake_Messages = new string[] { "You were jailed!", "You were attacked and healed by a doctor!", "You were attacked and protected by a bodyguard!", "You were attacked and healed by a Guardian Angel!", "You were attacked but your defense was too strong!", "You were role-blocked!", "Your target was jailed!","You were converted by a vampire!", "You are dead!" };
    public string[] Sheriff_Messages = new string[] { "You were jailed!", "You were attacked and healed by a doctor!", "You were attacked and protected by a bodyguard!", "You were attacked and healed by a Guardian Angel!", "You were attacked but your defense was too strong!", "You were role-blocked!", "Your target was jailed!", "You were converted by a vampire!", "You are dead!","Your target is good!","Your Target is evil" };
    public string[] Consig_Messages = new string[] { "You were jailed!", "You were attacked and healed by a doctor!", "You were attacked and protected by a bodyguard!", "You were attacked and healed by a Guardian Angel!", "You were attacked but your defense was too strong!", "You were role-blocked!", "Your target was jailed!", "You were converted by a vampire!", "You are dead!","Your target's role is " };
    public string[] Tracker_Messages = new string[] { "You were jailed!", "You were attacked and healed by a doctor!", "You were attacked and protected by a bodyguard!", "You were attacked and healed by a Guardian Angel!", "You were attacked but your defense was too strong!", "You were role-blocked!", "Your target was jailed!", "You were converted by a vampire!", "You are dead!","Your target visited " };
    public string[] Escort_Messages = new string[] { "You were jailed!", "You were attacked and healed by a doctor!", "You were attacked and protected by a bodyguard!", "You were attacked and healed by a Guardian Angel!", "You were attacked but your defense was too strong!", "You were role-blocked!", "Your target was jailed!", "You were converted by a vampire!", "You are dead!", "Someone tried to roleblock you and failed." };
    public string[][] Messages = new string[][] {

        new string[16],
        new string[16],
        new string[16],
        new string[16],
        new string[16],
        new string[16],
        new string[16],
        new string[16],
    };

    public int[] visiting_arrows = new int[8];
    public string AlphabetLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    public ResourceScript(KMBombInfo Bomb, int _modID)
    {
        this.Bomb = Bomb;
        this._modID = _modID;
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

    public IEnumerator Name_Chooser(TextMesh Player_joined)
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

    public void GetRoles(KMSelectable[] Players)
    {

        foreach (KMSelectable Player in Players)
        {
            if (Player.ToString() == "Player_1 (KMSelectable)")
            {
                string role = getCollumn1Roles(player_letter[0]);
                Debug.LogFormat("[Town of KTaNE #{0}] {1}'s assinged role is {2}", _modID, Players_name[0], role);
                Players_roles[0] = role;
            }
            else if (Player.ToString() == "Player_2 (KMSelectable)")
            {
                string role = getCollumn1Roles(player_letter[1]);
                Debug.LogFormat("[Town of KTaNE #{0}] {1}'s assinged role is {2}",_modID, Players_name[1], role);
                Players_roles[1] = role;
            }
            else if (Player.ToString() == "Player_3 (KMSelectable)")
            {
                string role = getCollumn3Roles(player_letter[2]);
                Debug.LogFormat("[Town of KTaNE #{0}] {1}'s assinged role is {2}", _modID, Players_name[2], role);
                Players_roles[2] = role;
            }
            else if (Player.ToString() == "Player_4 (KMSelectable)")
            {
                string role = getCollumn3Roles(player_letter[3]);
                Debug.LogFormat("[Town of KTaNE #{0}] {1}'s assinged role is {2}", _modID, Players_name[3], role);
                Players_roles[3] = role;
            }
            else if (Player.ToString() == "Player_5 (KMSelectable)")
            {
                string role = getCollumn2Roles(player_letter[4]);
                Debug.LogFormat("[Town of KTaNE #{0}] {1}'s assinged role is {2}", _modID, Players_name[4], role);
                Players_roles[4] = role;
            }
            else if (Player.ToString() == "Player_6 (KMSelectable)")
            {
                string role = getCollumn2Roles(player_letter[5]);
                Debug.LogFormat("[Town of KTaNE #{0}] {1}'s assinged role is {2}", _modID, Players_name[5], role);
                Players_roles[5] = role;
            }
            else if (Player.ToString() == "Player_7 (KMSelectable)")
            {
                string role = getCollumn4Roles(player_letter[6]);
                Debug.LogFormat("[Town of KTaNE #{0}] {1}'s assinged role is {2}", _modID, Players_name[6], role);
                Players_roles[6] = role;
            }
            else if (Player.ToString() == "Player_8 (KMSelectable)")
            {
                string role = getCollumn4Roles(player_letter[7]);
                Debug.LogFormat("[Town of KTaNE #{0}] {1}'s assinged role is {2}", _modID, Players_name[7], role);
                Players_roles[7] = role;
            }
        }
        foreach (var role in Players_roles)
        {
            if (role.Equals("Godfather"))
            {
                isGodfatherpresent = true;
            }
            if (role.Equals("Mafioso"))
            {
                ismafiosopresent = true;
            }
        }
        roles_chosen = true;
        Debug.LogFormat("[Town of KTaNE #{0}] All of the player's assigned roles are {1},{2},{3},{4},{5},{6},{7},{8}", _modID, Players_roles[0], Players_roles[1], Players_roles[2], Players_roles[3], Players_roles[4], Players_roles[5], Players_roles[6], Players_roles[7]);
    }

    public void Roles_Fight()
    {
        for (int i = 0; i <= 7; i++)
        {
            //if (Messages[i] != null)
            //{
            //    foreach (var str in Messages[i])
            //    {
            //        player_msg.Add(str);
            //    }
            //}
            switch (Players_roles[i])
            {
                case "Ambusher":
                    int x = 0;
                    List<string> targeted_roles = new List<string>();
                    List<int> targetedrole_pos = new List<int>();
                    List<int> escort_pos = new List<int>();
                    if (isJailed(i, visiting_arrows))
                    {
                        Debug.LogFormat("[Town of KTaNE #{0}] Ambusher has been jailed", _modID);
                        Messages[i][0] = "You were jailed!";
                        break;
                    }
                    if (isRoleblocked(i, visiting_arrows))
                    {
                        Debug.LogFormat("[Town of KTaNE #{0}] Ambusher has been roleblocked", _modID);
                        Messages[i][5] = "You were role-blocked!";
                        break;
                    }
                    int Ambusher_arrow = visiting_arrows[i];
                    if (istargetprotbyBG(Ambusher_arrow,i))
                    {
                        break;
                    }
                    //checks if there is anyone visiting the ambusher's target         
                    foreach (int arrow in visiting_arrows)
                    {
                        if (i == x)
                        {
                            x++;
                            continue;
                        }
                        if (arrow == Ambusher_arrow)
                        {
                            string target_visited = Players_roles[x];
                            targeted_roles.Add(target_visited);
                            targetedrole_pos.Add(x);
                            Debug.LogFormat("[Town of KTaNE #{0}] The targets that the Ambusher has targeted are {1}", _modID, target_visited);
                        }
                        x++;
                    }
                    if (targeted_roles.Count >= 1)
                    {
                        string[] definfo = GetRoleDefence(targeted_roles.ToArray(), targetedrole_pos).Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                        string def = definfo[0];
                        int targetedpos = int.Parse(definfo[1]);
                        if (isMafiaMember(targetedpos,i))
                        {
                            break;
                        }
                        if (isjailed[targetedpos - 1])
                        {
                            Debug.LogFormat("[Town of KTaNE #{0}] Your target was in jail!", _modID);
                            Messages[i][6] = "Your target was in jail!";
                            break;
                        }
                        isGONNADIEHARD(targetedpos, i, def);
                    }
                    ismessagesdone = true;
                    break;
                case "Arsonist":
                    if (isJailed(i, visiting_arrows))
                    {
                        Debug.LogFormat("[Town of KTaNE #{0}] Arsonist has been jailed", _modID);
                        Messages[i][0] = "You were jailed!";
                        break;
                    }
                    if (isRoleblocked(i, visiting_arrows))
                    {
                        Debug.LogFormat("[Town of KTaNE #{0}] Arsonist has been roleblocked", _modID);
                        Messages[i][5] = "You were role-blocked!";
                        break;
                    }
                    var Arsonist_arrow = visiting_arrows[i];

                    Debug.LogFormat("[Town of KTaNE #{0}] The Arsonist is visiting {1}", _modID, Players_roles[Arsonist_arrow - 1]);
                    if (istargetprotbyBG(Arsonist_arrow, i))
                    {
                        break;
                    }
                    if (isjailed[Arsonist_arrow - 1])
                    {
                        Debug.LogFormat("[Town of KTaNE #{0}] Your target was in jail!", _modID);
                        Messages[i][6] = "Your target was in jail!";
                        break;
                    }
                    Debug.LogFormat("[Town of KTaNE #{0}] The Arsonist has doused {1}", _modID, Players_roles[Arsonist_arrow - 1]);
                    ismessagesdone = true;
                    break;
                case "Bodyguard":
                    x = 0;
                    targeted_roles = new List<string>();
                    targetedrole_pos = new List<int>();
                    if (isJailed(i, visiting_arrows))
                    {
                        Debug.LogFormat("[Town of KTaNE #{0}] Bodyguard has been jailed", _modID);
                        Messages[i][0] = "You were jailed!";
                        break;
                    }
                    if (isRoleblocked(i, visiting_arrows))
                    {
                        Debug.LogFormat("[Town of KTaNE #{0}] Bodyguard has been roleblocked", _modID);
                        Messages[i][5] = "You were role-blocked!";
                        break;
                    }
                    int Bodyguard_arrow = visiting_arrows[i];
                    if (!istargetprotbyBG(Bodyguard_arrow, i))
                    {
                        break;
                    }
                    foreach (int arrow in visiting_arrows)
                    {
                        if (i == x)
                        {
                            x++;
                            continue;
                        }
                        if (arrow == Bodyguard_arrow)
                        {
                            string target_visited = Players_roles[x];
                            targeted_roles.Add(target_visited);
                            targetedrole_pos.Add(x);
                            Debug.LogFormat("[Town of KTaNE #{0}] The Bodyguard's target is protected from Basic and Powerful attacks and will fight {1}", _modID, target_visited);
                            break;
                        }
                        x++;
                    }
                    if (targeted_roles.Count >= 1)
                    {
                        string[] definfo = GetRoleDefence(targeted_roles.ToArray(), targetedrole_pos).Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                        string def = definfo[0];
                        int targetedpos = int.Parse(definfo[1]);
                        if (isjailed[targetedpos - 1])
                        {
                            Debug.LogFormat("[Town of KTaNE #{0}]Your target was in jail!", _modID);
                            Messages[i][6] = "Your target was in jail!";
                            break;
                        }
                        isGONNADIEHARD(targetedpos, i, def);
                    }
                    ismessagesdone = true;
                    break;
                case "Bulletproof":
                    if (isJailed(i, visiting_arrows))
                    {
                        Debug.LogFormat("[Town of KTaNE #{0}] Bulletproof has been jailed", _modID);
                        Messages[i][0] = "You were jailed!";
                        break;
                    }
                    if (isRoleblocked(i, visiting_arrows))
                    {
                        Debug.LogFormat("[Town of KTaNE #{0}] Bulletproof has been roleblocked", _modID);
                        Messages[i][5] = "You were role-blocked!";
                        break;
                    }
                    var Bulletproof_arrow = visiting_arrows[i];

                    Debug.LogFormat("[Town of KTaNE #{0}] The Bulletproof is visiting {1}", _modID, Players_roles[Bulletproof_arrow-1]);
                    if (istargetprotbyBG(Bulletproof_arrow, i))
                    {
                        break;
                    }
                    if (isjailed[Bulletproof_arrow - 1])
                    {
                        Debug.LogFormat("[Town of KTaNE #{0}] Your target was in jail!", _modID);
                        Messages[i][6] = "Your target was in jail!";
                        break;
                    }
                    ismessagesdone = true;
                    break;
                case "Consigliere":
                    if (isJailed(i, visiting_arrows))
                    {
                        Debug.LogFormat("[Town of KTaNE #{0}] Consigliere has been jailed", _modID);
                        Messages[i][0] = "You were jailed!";
                        break;
                    }
                    if (isRoleblocked(i, visiting_arrows))
                    {
                        Debug.LogFormat("[Town of KTaNE #{0}] Consigliere has been roleblocked", _modID);
                        Messages[i][5] = "You were role-blocked!";
                        break;
                    }
                    var Consigliere_arrow = visiting_arrows[i];
                    Debug.LogFormat("[Town of KTaNE #{0}] The Consigliere is visiting {1}", _modID, Players_roles[Consigliere_arrow - 1]);
                    if (istargetprotbyBG(Consigliere_arrow, i))
                    {
                        break;
                    }
                    if (isjailed[Consigliere_arrow - 1])
                    {
                        Debug.LogFormat("[Town of KTaNE #{0}]Your target was in jail!", _modID);
                        Messages[i][6] = "Your target was in jail!";
                        break;
                    }
                    var consigtarget = Players_roles[Consigliere_arrow - 1];
                    var consigmessage = "Your target’s role is " + consigtarget + ".";
                    Messages[i][13] = consigmessage;
                    ismessagesdone = true;
                    break;
                case "Consort":
                    if (isJailed(i, visiting_arrows))
                    {
                        Debug.LogFormat("[Town of KTaNE #{0}] Consort has been jailed", _modID);
                        Messages[i][0] = "You were jailed!";
                        break;
                    }
                    if (isRoleblocked(i, visiting_arrows))
                    {
                        Debug.LogFormat("[Town of KTaNE #{0}] Someone tried to roleblock you and failed.",_modID);
                        Messages[i][5] = "Someone tried to roleblock you and failed!";
                    }
                    x = 0;
                    var Consort_arrow = visiting_arrows[i];
                    Debug.LogFormat("[Town of KTaNE #{0}] The Consort is visiting {1}", _modID, Players_roles[Consort_arrow - 1]);
                    if (istargetprotbyBG(Consort_arrow, i))
                    {
                        break;
                    }
                    if (isjailed[Consort_arrow - 1])
                    {
                        Debug.LogFormat("[Town of KTaNE #{0}] Your target was in jail!", _modID);
                        Messages[i][6] = "Your target was in jail!";
                        break;
                    }
                    isroleblock[visiting_arrows[i] - 1] = true;
                    ismessagesdone = true;
                    break;
                case "Sheriff":
                    if (isJailed(i, visiting_arrows))
                    {
                        Debug.LogFormat("[Town of KTaNE #{0}] Sheriff has been jailed", _modID);
                        Messages[i][0] = "You were jailed!";
                        break;
                    }
                    if (isRoleblocked(i, visiting_arrows))
                    {
                        Debug.LogFormat("[Town of KTaNE #{0}] Sheriff has been roleblocked", _modID);
                        Messages[i][5] = "You were role-blocked!";
                        break;
                    }
                    var Sheriff_arrow = visiting_arrows[i];
                    if (isjailed[Sheriff_arrow - 1])
                    {
                        Debug.LogFormat("[Town of KTaNE #{0}] Your target was in jail!", _modID);
                        Messages[i][6] = "Your target was in jail!";
                        break;
                    }
                    Debug.LogFormat("[Town of KTaNE #{0}] The Sheriff is visiting {1}",_modID ,Players_roles[Sheriff_arrow - 1]);
                    if (istargetprotbyBG(Sheriff_arrow, i))
                    {
                        break;
                    }
                    if (isframed[Sheriff_arrow - 1])
                    {
                        Debug.LogFormat("[Town of KTaNE #{0}] The Framer has framed the Sheriff's target", _modID);
                        Debug.LogFormat("[Town of KTaNE #{0}] Your target is evil!",_modID);
                        Messages[i][15] = "Your target is evil!";
                        break;
                    }
                    if (Players_roles[Sheriff_arrow - 1].EqualsAny("Ambusher", "Consort", "Framer", "Hypnotist", "Mafioso", "Serial Killer", "Vampire", "Werewolf", "Arsonist"))
                    {
                        Debug.LogFormat("[Town of KTaNE #{0}] Your target is evil", _modID);
                        Messages[i][15] = "Your target is evil!";
                    }
                    else
                    {
                        Debug.LogFormat("[Town of KTaNE #{0}] Your target is good", _modID);
                        Messages[i][15] = "Your target is good!";
                    }
                    ismessagesdone = true;
                    break;
                case "Disguiser":
                    if (isJailed(i, visiting_arrows))
                    {
                        Debug.LogFormat("[Town of KTaNE #{0}] Digsuiser has been jailed", _modID);
                        Messages[i][0] = "You were jailed!";
                        break;
                    }
                    if (isRoleblocked(i, visiting_arrows))
                    {
                        Debug.LogFormat("[Town of KTaNE #{0}] Digsuiser has been roleblocked", _modID);
                        Messages[i][5] = "You were role-blocked!";
                        break;
                    }
                    var Disguiser_arrow = visiting_arrows[i];
                    Debug.LogFormat("[Town of KTaNE #{0}] The Digsuiser is visiting {1}", _modID, Players_roles[Disguiser_arrow - 1]);
                    if (istargetprotbyBG(Disguiser_arrow, i))
                    {
                        break;
                    }
                    if (isjailed[i])
                    {
                        Debug.LogFormat("[Town of KTaNE #{0} ]Your target was in jail!", _modID);
                        Messages[i][6] = "Your target was in jail!";
                        break;
                    }
                    Players_roles[i] = Players_roles[Disguiser_arrow - 1];
                    ismessagesdone = true;
                    break;
                case "Doctor":
                    if (isJailed(i, visiting_arrows))
                    {
                        Debug.LogFormat("[Town of KTaNE #{0}] Doctor has been jailed", _modID);
                        Messages[i][0] = "You were jailed!";
                        break;
                    }
                    if (isRoleblocked(i, visiting_arrows))
                    {
                        Debug.LogFormat("[Town of KTaNE #{0}] Doctor has been roleblocked", _modID);
                        Messages[i][5] = "You were role-blocked!";
                        break;
                    }
                    var Doctor_arrow = visiting_arrows[i];
                    Debug.LogFormat("[Town of KTaNE #{0}] The Doctor is visiting {1}", _modID, Players_roles[Doctor_arrow - 1]);
                    if (istargetprotbyBG(Doctor_arrow, i))
                    {
                        break;
                    }
                    if (isjailed[Doctor_arrow - 1])
                    {
                        Debug.LogFormat("[Town of KTaNE #{0}] Your target was in jail!", _modID);
                        Messages[i][6] = "Your target was in jail!";
                        break;
                    }
                    x = 0;
                    foreach (var arrow in visiting_arrows)
                    {
                        if (i == x)
                        {
                            x++;
                            continue;
                        }
                        if (arrow == Doctor_arrow)
                        {
                            Debug.LogFormat("[Town of KTaNE #{0}] The Doctor's Target is protected from basic immunity but {1} visited them", _modID, Players_roles[x]);
                        }
                        x++;
                    }
                    ismessagesdone = true;
                    break;
                case "Escort":
                    if (isJailed(i, visiting_arrows))
                    {
                        Debug.LogFormat("[Town of KTaNE #{0}] Escort has been jailed", _modID);
                        Messages[i][0] = "You were jailed!";
                        break;
                    }
                    if (isRoleblocked(i, visiting_arrows))
                    {
                        Debug.LogFormat("[Town of KTaNE #{0}] Someone tried to roleblock you and failed.", _modID);
                        Messages[i][15] = "Someone tried to roleblock you and failed!";
                    }
                    var Escort_arrow = visiting_arrows[i];
                    Debug.LogFormat("[Town of KTaNE #{0}] The Escort is visiting {1}", _modID, Players_roles[Escort_arrow - 1]);
                    if (istargetprotbyBG(Escort_arrow, i))
                    {
                        break;
                    }
                    if (isjailed[Escort_arrow - 1])
                    {
                        Debug.LogFormat("[Town of KTaNE #{0}] Your target was in jail!", _modID);
                        Messages[i][6] = "Your target was in jail!";
                        break;
                    }
                    isroleblock[visiting_arrows[i] - 1] = true;
                    ismessagesdone = true;
                    break;
                case "Framer":
                    if (isJailed(i, visiting_arrows))
                    {
                        Debug.LogFormat("[Town of KTaNE #{0}] Framer has been jailed", _modID);
                        Messages[i][0] = "You were jailed!";
                        break;
                    }
                    if (isRoleblocked(i, visiting_arrows))
                    {
                        Debug.LogFormat("[Town of KTaNE #{0}] Framer has been roleblocked", _modID);
                        Messages[i][5] = "You were role-blocked!";
                        break;
                    }
                    var Framer_arrow = visiting_arrows[i];
                    Debug.LogFormat("[Town of KTaNE #{0}] The Framer is visiting {1}", _modID, Players_roles[Framer_arrow - 1]);
                    if (istargetprotbyBG(Framer_arrow, i))
                    {
                        break;
                    }
                    if (isjailed[Framer_arrow - 1])
                    {
                        Debug.LogFormat("[Town of KTaNE #{0}] Your target was in jail!", _modID);
                        Messages[i][6] = "Your target was in jail!";
                        break;
                    }
                    isframed[Framer_arrow - 1] = true;
                    ismessagesdone = true;
                    break;
                case "Godfather":
                    if (isJailed(i, visiting_arrows))
                    {
                        Debug.LogFormat("[Town of KTaNE #{0}] Godfather has been jailed", _modID);
                        Messages[i][0] = "You were jailed!";
                        break;
                    }
                    if (isRoleblocked(i, visiting_arrows))
                    {
                        Debug.LogFormat("[Town of KTaNE #{0}] Godfather has been roleblocked", _modID);
                        Messages[i][5] = "You were role-blocked!";
                        break;
                    }
                    var Godfather_arrow = visiting_arrows[i];
                    Debug.LogFormat("[Town of KTaNE #{0}] The Godfather is visiting {1}", _modID, Players_roles[Godfather_arrow - 1]);
                    if (istargetprotbyBG(Godfather_arrow, i))
                    {
                        break;
                    }
                    if (isjailed[Godfather_arrow - 1])
                    {
                        Debug.LogFormat("[Town of KTaNE #{0}] Your target was in jail!", _modID);
                        Messages[i][6] = "Your target was in jail!";
                        break;
                    }
                    x = 0;
                    if (ismafiosopresent)
                    {
                        if (isMafiaMember(Godfather_arrow,i))
                        {
                            break;
                        }
                        Debug.LogFormat("[Town of KTaNE #{0}] The Godfather has sent the Mafioso after the {1}", _modID, Players_roles[Godfather_arrow - 1]);
                        string[] targeted_rolesa = new string[] { Players_roles[Godfather_arrow - 1] };
                        List<int> targetedrole_posa = new List<int>();
                        targetedrole_posa.Add(Godfather_arrow - 1);
                        string[] definfo = GetRoleDefence(targeted_rolesa, targetedrole_posa).Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                        string def = definfo[0];
                        int targetedpos = int.Parse(definfo[1]);
                        isGONNADIEHARD(targetedpos, Array.IndexOf(Players_roles, "Mafioso"), def);
                    }
                    else
                    {
                        if (isMafiaMember(Godfather_arrow,i))
                        {
                            break;
                        }
                        string[] targeted_rolesa = new string[] { Players_roles[Godfather_arrow - 1] };
                        List<int> targetedrole_posa = new List<int>();
                        targetedrole_posa.Add(Godfather_arrow - 1);
                        string[] definfo = GetRoleDefence(targeted_rolesa, targetedrole_posa).Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                        string def = definfo[0];
                        int targetedpos = int.Parse(definfo[1]);
                        isGONNADIEHARD(targetedpos, i, def);
                    }
                    ismessagesdone = true;
                    break;
                case "Guardian Angel":
                    if (isJailed(i, visiting_arrows))
                    {
                        Debug.LogFormat("[Town of KTaNE #{0}] Guardian Angel has been jailed", _modID);
                        Messages[i][0] = "You were jailed!";
                        break;
                    }
                    if (isRoleblocked(i, visiting_arrows))
                    {
                        Debug.LogFormat("[Town of KTaNE #{0}] Guardian Angel has been roleblocked", _modID);
                        Messages[i][5] = "You were role-blocked!";
                        break;
                    }
                    var GuardianAngel_arrow = visiting_arrows[i];
                    Debug.LogFormat("[Town of KTaNE #{0}] The Guardian Angel is visiting {1}", _modID, Players_roles[GuardianAngel_arrow - 1]);
                    if (istargetprotbyBG(GuardianAngel_arrow, i))
                    {
                        break;
                    }
                    if (isjailed[GuardianAngel_arrow - 1])
                    {
                        Debug.LogFormat("[Town of KTaNE #{0}] Your target was in jail!", _modID);
                        Messages[i][6] = "Your target was in jail!";
                        break;
                    }
                    Debug.LogFormat("[Town of KTaNE #{0}] The Guardian Angel's target is protected from all attacks", _modID);
                    ismessagesdone = true;
                    break;
                case "Hypnotist":
                    List<string> hypno = new List<string>();
                    if (isJailed(i, visiting_arrows))
                    {
                        Debug.LogFormat("[Town of KTaNE #{0}] Hypnotist has been jailed", _modID);
                        Messages[i][0] = "You were jailed!";
                        break;
                    }
                    if (isRoleblocked(i, visiting_arrows))
                    {
                        Debug.LogFormat("[Town of KTaNE #{0}] Hypnotist has been roleblocked", _modID);
                        Messages[i][5] = "You were role-blocked!";
                        break;
                    }
                    var Hypnotist_arrow = visiting_arrows[i];
                    Debug.LogFormat("[Town of KTaNE #{0}] The Hypnotist is visiting {1}", _modID, Players_roles[Hypnotist_arrow - 1]);
                    if (istargetprotbyBG(Hypnotist_arrow, i))
                    {
                        break;
                    }
                    if (isjailed[Hypnotist_arrow - 1])
                    {
                        Debug.LogFormat("[Town of KTaNE #{0}] Your target was in jail!", _modID);
                        Messages[i][6] = "Your target was in jail!";
                        break;
                    }
                    var bomb = Bomb.GetSerialNumberNumbers().Last() == 9 ? 0 : Bomb.GetSerialNumberNumbers().Last();
                    var fm = Fake_Messages[bomb-1];
                    hypno.Add(fm);
                    Debug.LogFormat("[Town of KTaNE #{0}] {1}",_modID,fm);
                    Messages[Hypnotist_arrow - 1][12] = fm;
                    ismessagesdone = true;
                    break;
                case "Jailor":
                    if (isRoleblocked(i, visiting_arrows))
                    {
                        Debug.LogFormat("[Town of KTaNE #{0}] Jailor has been roleblocked", _modID);
                        Messages[i][5] = "You were role-blocked!";
                        break;
                    }
                    var Jailor_arrow = visiting_arrows[i];
                    Debug.LogFormat("[Town of KTaNE #{0}] The Jailor has jailed {1}", _modID, Players_roles[Jailor_arrow - 1]);
                    if (istargetprotbyBG(Jailor_arrow, i))
                    {
                        break;
                    }
                    if (Players_roles[Jailor_arrow - 1] == "Serial Killer")
                    {
                        Debug.LogFormat("[Town of KTaNE #{0}] The Jailor has been killed by the Serial Killer", _modID);
                    }
                    isroleblock[Jailor_arrow - 1] = true;
                    isjailed[Jailor_arrow - 1] = true;
                    ismessagesdone = true;
                    break;
                case "Jester":
                    if (isJailed(i, visiting_arrows))
                    {
                        Debug.LogFormat("[Town of KTaNE #{0}] Jester has been jailed", _modID);
                        Messages[i][0] = "You were jailed!";
                        break;
                    }
                    if (isRoleblocked(i, visiting_arrows))
                    {
                        Debug.LogFormat("[Town of KTaNE #{0}] Jester has been roleblocked", _modID);
                        Messages[i][5] = "You were role-blocked!";
                        break;
                    }
                    var Jester_arrow = visiting_arrows[i];
                    Debug.LogFormat("[Town of KTaNE #{0}] The Jester is visiting {1}", _modID, Players_roles[Jester_arrow - 1]);
                    if (istargetprotbyBG(Jester_arrow, i))
                    {
                        break;
                    }
                    if (isjailed[Jester_arrow - 1])
                    {
                        Debug.LogFormat("[Town of KTaNe #{0}] Your target was in jail!", _modID);
                        Messages[i][6] = "Your target was in jail!";
                        break;
                    }
                    ismessagesdone = true;
                    break;
                case "Lookout":
                    if (isJailed(i, visiting_arrows))
                    {
                        Debug.LogFormat("[Town of KTaNE #{0}] Lookout has been jailed", _modID);
                        Messages[i][0] = "You were jailed!";
                        break;
                    }
                    if (isRoleblocked(i, visiting_arrows))
                    {
                        Debug.LogFormat("[Town of KTaNE #{0}] Lookout has been roleblocked", _modID);
                        Messages[i][5] = "You were role-blocked!";
                        break;
                    }
                    var Lookout_arrow = visiting_arrows[i];
                    Debug.LogFormat("[Town of KTaNE #{0}] The Lookout is visiting {1}", _modID, Players_roles[Lookout_arrow - 1]);
                    if (istargetprotbyBG(Lookout_arrow, i))
                    {
                        break;
                    }
                    if (isjailed[Lookout_arrow - 1])
                    {
                        Debug.LogFormat("[Town of KTaNE #{0}] Your target was in jail!", _modID);
                        Messages[i][6] = "Your target was in jail!";
                        break;
                    }
                    List<string> LO_list = new List<string>();
                    x = 0;
                    foreach (var arrow in visiting_arrows)
                    {
                        if (i == x)
                        {
                            x++;
                            continue;
                        }
                        if (arrow == Lookout_arrow)
                        {
                            var visited_role = Players_roles[x];
                            LO_list.Add(visited_role);
                            Debug.LogFormat("[Town of KTaNE #{0}] The Lookout's target has been visited by {1}", _modID, Players_roles[x]);
                        }
                        x++;
                    }
                    Debug.LogFormat("[Town of KTaNE #{0}] The amount of people that has visited the Lookout's target is {1}", _modID, LO_list.Count);
                    ismessagesdone = true;
                    break;
                case "Mafioso":
                    if (isJailed(i, visiting_arrows))
                    {
                        Debug.LogFormat("[Town of KTaNE #{0}] Mafioso has been jailed", _modID);
                        Messages[i][0] = "You were jailed!";
                        break;
                    }
                    if (isRoleblocked(i, visiting_arrows))
                    {
                        Debug.LogFormat("[Town of KTaNE #{0}] Mafioso has been roleblocked", _modID);
                        Messages[i][5] = "You were role-blocked!";
                        break;
                    }
                    var Mafioso_arrow = visiting_arrows[i];
                    Debug.LogFormat("[Town of KTaNE #{0}] The Mafioso is visiting {1}", _modID, Players_roles[Mafioso_arrow - 1]);
                    if (istargetprotbyBG(Mafioso_arrow, i))
                    {
                        break;
                    }
                    if (isjailed[Mafioso_arrow - 1])
                    {
                        Debug.LogFormat("[Town of KTaNE #{0}] Your target was in jail!", _modID);
                        Messages[i][6] = "Your target was in jail!";
                        break;
                    }
                    if (!isGodfatherpresent)
                    {
                        Debug.LogFormat("[Town of KTaNE #{0}] Godfather is not present and mafioso will kill his target", _modID);
                        if (isMafiaMember(Mafioso_arrow,i))
                        {
                            break;
                        }
                        string[] targeted_rolesa = new string[] { Players_roles[Mafioso_arrow - 1] };
                        List<int> targetedrole_posa = new List<int>();
                        targetedrole_posa.Add(Mafioso_arrow - 1);
                        string[] definfo = GetRoleDefence(targeted_rolesa, targetedrole_posa).Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                        string def = definfo[0];
                        int targetedpos = int.Parse(definfo[1]);
                        isGONNADIEHARD(targetedpos, i, def);
                    }
                    ismessagesdone = true;
                    break;
                case "Serial Killer":
                    if (isJailed(i, visiting_arrows))
                    {
                        Debug.LogFormat("[Town of KTaNE #{0}] Serial Killer has been jailed", _modID);
                        Messages[i][1] = "You were jailed!";
                        break;
                    }
                    var SerialKiller_arrow = visiting_arrows[i];
                    Debug.LogFormat("[Town of KTaNE #{0}] The Serial Killer is visiting {1}", _modID, Players_roles[SerialKiller_arrow - 1]);
                    if (istargetprotbyBG(SerialKiller_arrow, i))
                    {
                        break;
                    }
                    if (isjailed[SerialKiller_arrow - 1])
                    {
                        Debug.LogFormat("[Town of KTaNE #{0}] Your target is jailed", _modID);
                        Messages[i][6] = "Your target was in jail!";
                        break;
                    }
                    string[] targeted_rolesb = new string[] { Players_roles[SerialKiller_arrow - 1] };
                    List<int> targetedrole_posb = new List<int>();
                    targetedrole_posb.Add(SerialKiller_arrow - 1);
                    string[] definfo1 = GetRoleDefence(targeted_rolesb, targetedrole_posb).Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                    string def1 = definfo1[0];
                    int targetedpos1 = int.Parse(definfo1[1]);
                    isGONNADIEHARD(targetedpos1, i, def1);
                    break;
                case "Spy":
                    if (isJailed(i, visiting_arrows))
                    {
                        Debug.LogFormat("[Town of KTaNE #{0}] Spy has been jailed", _modID);
                        Messages[i][0] = "You were jailed!";
                        break;
                    }
                    if (isRoleblocked(i, visiting_arrows))
                    {
                        Debug.LogFormat("[Town of KTaNE #{0}] Spy has been roleblocked", _modID);

                        Messages[i][5] = "You were role-blocked!";
                        break;
                    }
                    isspypresent = true;
                    Spy_pos.Add(i);
                    ismessagesdone = true;
                    break;
                case "Tracker":
                    if (isJailed(i, visiting_arrows))
                    {
                        Debug.LogFormat("[Town of KTaNE #{0}] Tracker has been jailed", _modID);
                        Messages[i][0] = "You were jailed!";
                        break;
                    }
                    if (isRoleblocked(i, visiting_arrows))
                    {
                        Debug.LogFormat("[Town of KTaNE #{0}] Tracker has been roleblocked", _modID);
                        Messages[i][5] = "You were role-blocked!";
                        break;
                    }
                    var Tracker_arrow = visiting_arrows[i];
                    Debug.LogFormat("[Town of KTaNE #{0}] The Tracker is visiting {1}", _modID, Players_roles[Tracker_arrow - 1]);
                    if (istargetprotbyBG(Tracker_arrow, i))
                    {
                        break;
                    }
                    if (isjailed[Tracker_arrow - 1])
                    {
                        Debug.LogFormat("[Town of KTaNE #{0}] Your target was in jail!", _modID);
                        Messages[i][6] = "Your target was in jail!";
                        break;
                    }
                    var target_arrow = visiting_arrows[Tracker_arrow - 1];
                    if (target_arrow == 0)
                    {

                    }
                    else
                    {
                        Debug.LogFormat("[Town of KTaNE #{0}] The Tracker has tracked his target to {1}", _modID, Players_roles[target_arrow - 1]);
                    }
                    ismessagesdone = true;
                    Messages[i][14] = "Your target visited "+Players_roles[target_arrow - 1];
                    break;
                case "Vampire":
                    List<string> vamp = new List<string>();
                    if (isJailed(i, visiting_arrows))
                    {
                        Debug.LogFormat("[Town of KTaNE #{0}] Vampire has been jailed", _modID);
                        Messages[i][0] = "You were jailed!";
                        break;
                    }
                    if (isRoleblocked(i, visiting_arrows))
                    {
                        Debug.LogFormat("[Town of KTaNE #{0}] Vampire has been roleblocked", _modID);
                        Messages[i][5] = "You were role-blocked!";
                        break;
                    }
                    var Vampire_arrow = visiting_arrows[i];
                    Debug.LogFormat("[Town of KTaNE #{0}] The Vampire is visiting {1}", _modID, Players_roles[Vampire_arrow - 1]);
                    if (istargetprotbyBG(Vampire_arrow, i))
                    {
                        break;
                    }
                    if (isjailed[Vampire_arrow - 1])
                    {
                        Debug.LogFormat("[Town of KTaNE #{0}] Your target was in jail!", _modID);
                        Messages[i][6] = "Your target was in jail!";
                        break;
                    }
                    if (Players_roles[Vampire_arrow - 1] == "Vampire Hunter")
                    {
                        Debug.LogFormat("[Town of KTaNE #{0}] The Vampire Hunter has STAKED the Vampire", _modID);
                        break;
                    }
                    Debug.LogFormat("[Town of KTaNE #{0}] The Vampire has converted the {1}", _modID, Players_roles[Vampire_arrow - 1]);
                    vamp.Add("You were converted by a Vampire!");
                    ismessagesdone = true;
                    break;
                case "Vampire Hunter":
                    if (isJailed(i, visiting_arrows))
                    {
                        Debug.LogFormat("[Town of KTaNE #{0}] Vampire Hunter has been jailed", _modID);
                        Messages[i][0] = "You were jailed!";
                        break;
                    }
                    if (isRoleblocked(i, visiting_arrows))
                    {
                        Debug.LogFormat("[Town of KTaNE #{0}] Vampire Hunter has been roleblocked", _modID);
                        Messages[i][5] = "You were role-blocked!";
                        break;
                    }
                    var VampireHunter_arrow = visiting_arrows[i];
                    Debug.LogFormat("[Town of KTaNE #{0}] The Vampire Hunter is visiting {1}", _modID, Players_roles[VampireHunter_arrow - 1]);
                    if (istargetprotbyBG(VampireHunter_arrow, i))
                    {
                        break;
                    }
                    if (isjailed[VampireHunter_arrow - 1])
                    {
                        Debug.LogFormat("[Town of KTaNE #{0}] Your target was in jail!", _modID);
                        Messages[i][6] = "Your target was in jail!";
                        break;
                    }
                    if (Players_roles[VampireHunter_arrow - 1] == "Vampire")
                    {
                        Debug.LogFormat("[Town of KTaNE #{0}] The Vampire Hunter has STAKED the Vampire", _modID);
                        break;
                    }
                    ismessagesdone = true;
                    break;
                case "Veteran":
                    List<string> targeted_roles1 = new List<string>();
                    List<int> targetedrole_pos1 = new List<int>();
                    if (isJailed(i, visiting_arrows))
                    {
                        Debug.LogFormat("[Town of KTaNE #{0}] Veteran has been jailed", _modID);
                        Messages[i][0] = "You were jailed!";
                        break;
                    }
                    x = 0;
                    foreach(var arrow in visiting_arrows)
                    {
                        if(i == x)
                        {
                            x++;
                            continue;
                        }
                        if(arrow == (i))
                        {
                            targeted_roles1.Add(Players_roles[x]);
                            targetedrole_pos1.Add(x);
                            Debug.LogFormat("[Town of KTaNE #{0}] The Veteran has been visited by {1}", _modID, Players_roles[x]);
                        }
                        x++;

                    }
                    foreach (var roleposition in targetedrole_pos1)
                    {
                        var role = Players_roles[roleposition];
                        int role_index = Array.IndexOf(Roles_index, role);
                        string[] role_info = Roles[role_index];
                        string def = role_info[4];
                        isGONNADIEHARD(roleposition + 1, i, def);
                    }
                    ismessagesdone = true;
                    break;
                case "Vigilante":
                    if (isJailed(i, visiting_arrows))
                    {
                        Debug.LogFormat("[Town of KTaNE #{0}] Vigilante has been jailed", _modID);
                        Messages[i][0] = "You were jailed!";
                        break;
                    }
                    if (isRoleblocked(i, visiting_arrows))
                    {
                        Debug.LogFormat("[Town of KTaNE #{0}] Vigilante has been roleblocked", _modID);
                        Messages[i][5] = "You were role-blocked!";
                        break;
                    }
                    var Vigilante_arrow = visiting_arrows[i];
                    Debug.LogFormat("[Town of KTaNE #{0}] The Vigilante is visiting {1}", _modID, Players_roles[Vigilante_arrow - 1]);
                    if (istargetprotbyBG(Vigilante_arrow, i))
                    {
                        break;
                    }
                    if (isjailed[Vigilante_arrow - 1])
                    {
                        Debug.LogFormat("[Town of KTaNE #{0}] Your target was in jail!", _modID);
                        Messages[i][6] = "Your target was in jail!";
                        break;
                    }
                    string[] targeted_rolesc = new string[] { Players_roles[Vigilante_arrow - 1] };
                    List<int> targetedrole_posc = new List<int>();
                    targetedrole_posc.Add(Vigilante_arrow - 1);
                    string[] definfo2 = GetRoleDefence(targeted_rolesc, targetedrole_posc).Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                    string def2 = definfo2[0];
                    int targetedpos2 = int.Parse(definfo2[1]);
                    isGONNADIEHARD(targetedpos2, i, def2);
                    ismessagesdone = true;
                    break;
                case "Werewolf":
                    List<string> targeted_roles2 = new List<string>();
                    List<int> targetedrole_pos2 = new List<int>();
                    if (isJailed(i, visiting_arrows))
                    {
                        Debug.LogFormat("[Town of KTaNE #{0}] Werewolf has been jailed", _modID);
                        Messages[i][0] = "You were jailed!";
                        break;
                    }
                    if (isRoleblocked(i, visiting_arrows))
                    {
                        Debug.LogFormat("[Town of KTaNE #{0}] Werewolf has been roleblocked", _modID);
                        Messages[i][5] = "You were role-blocked!";
                        break;
                    }
                    var Werewolf_arrow = visiting_arrows[i];
                    Debug.LogFormat("[Town of KTaNE #{0}] The Werewolf is visiting {1}", _modID, Players_roles[Werewolf_arrow - 1]);
                    if (istargetprotbyBG(Werewolf_arrow, i))
                    {
                        break;
                    }
                    if (isjailed[Werewolf_arrow - 1])
                    {
                        Debug.LogFormat("[Town of KTaNE #{0}] Your target was in jail!", _modID);
                        Messages[i][6] = "Your target was in jail!";
                        break;
                    }
                    x = 0;
                    foreach (var arrow in visiting_arrows)
                    {
                        if (i == x)
                        {
                            x++;
                            continue;
                        }
                        if (arrow == Werewolf_arrow)
                        {
                            targeted_roles2.Add(Players_roles[x]);
                            targetedrole_pos2.Add(x);
                            Debug.LogFormat("[Town of KTaNE #{0}] The Werewolf's target has been visited by {1}", _modID, Players_roles[x]);
                        }
                        x++;

                    }
                    var role1 = Players_roles[Werewolf_arrow-1];
                    int role_index1 = Array.IndexOf(Roles_index, role1);
                    string[] role_info1 = Roles[role_index1];
                    string def3 = role_info1[4];
                    isGONNADIEHARD(Werewolf_arrow, i, def3);
                    foreach (var roleposition in targetedrole_pos2)
                    {
                        var role = Players_roles[roleposition];
                        int role_index = Array.IndexOf(Roles_index, role);
                        string[] role_info = Roles[role_index];
                        string def = role_info[4];
                        isGONNADIEHARD(roleposition + 1, i, def);
                    }
                    ismessagesdone = true;
                    break;
            }
        }
        if (isspypresent)
        {
            foreach (var spypos in Spy_pos)
            {
                var Spy_arrow1 = visiting_arrows[spypos];
                Debug.LogFormat("[Town of KTaNE #{0}] The Spy is visiting {1}", _modID, Players_roles[Spy_arrow1 - 1]);
                if (istargetprotbyBG(Spy_arrow1, spypos))
                {
                    break;
                }
                if (isjailed[Spy_arrow1 - 1])
                {
                    Debug.LogFormat("[Town of KTaNE #{0}] Your target was in jail!", _modID);
                    Messages[spypos][6] = "Your target was in jail!";
                    break;
                }
                if (Messages[Spy_arrow1 - 1] == null)
                {
                    break;
                }
                else
                {
                    foreach (var msg in Messages[Spy_arrow1-1])
                    {
                        if (msg == null) { break; };
                        Debug.Log(msg);
                    }
                }
            }

        }
        //ShowMessages();
    }

    public bool isJailed(int player, int[] visiting_arrows)
    {
        int x = 0;
        if (isjailed[player])
        {
            return true;
        }
        foreach (var arrow in visiting_arrows)
        {
            if (player == x)
            {
                x++;
                continue;
            }
            if (arrow == (player + 1))
            {
                string target_visited = Players_roles[x];
                if (target_visited.Equals("Jailor"))
                {
                    isjailed[player] = true;
                    Debug.LogFormat("[Town of KTaNE #{0}] The " + Players_roles[player] + " has been jailed", _modID);
                    return true;
                }
            }
            x++;
        }
        return false;
    }
    public bool isRoleblocked(int player, int[] visiting_arrows)
    {
        int x = 0;
        if (isroleblock[player])
        {
            return true;
        }
        //if (Players_roles[player].EqualsAny("Escort", "Consort"))
        //{
        //    return true;
        //}
        foreach (var arrow in visiting_arrows)
        {
            if (player == x)
            {
                x++;
                continue;
            }
            if (arrow == (player + 1))
            {
                string target_visited = Players_roles[x];
                if (target_visited.EqualsAny("Escort", "Consort"))
                {
                    isroleblock[player] = true;
                    Debug.LogFormat("[Town of KTaNE #{0}] The " + Players_roles[player] + " has been roleblock by Escort/Consort", _modID);
                    return true;
                }
            }
            x++;
        }
        return false;
    }
    public string GetRoleDefence(string[] targeted_roles, List<int> targetedrole_pos)
    {

        int[] role_prios = new int[targeted_roles.Count()];
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
        int[] rp_copy = new int[targeted_roles.Count()];
        role_prios.CopyTo(rp_copy, 0);
        Array.Sort(rp_copy);
        int targetedpos = targetedrole_pos.ElementAt(Array.IndexOf(role_prios, rp_copy[0])) + 1;
        return Role_info.ElementAt(Array.IndexOf(role_prios, rp_copy[0]))[4] + ":" + targetedpos;
    }
    public bool isMafiaMember(int targetedpos, int player)
    {
        int x = 0;
        foreach (int arrow in visiting_arrows)
        {
            int Targeted_arrow = visiting_arrows[targetedpos-1];
            if (targetedpos == x)
            {
                x++;
                continue;
            }
            if (arrow == Targeted_arrow)
            {
                if (Players_roles[x].EqualsAny("Mafioso", "Consigliere", "Godfather", "Consort", "Disguiser", "Framer", "Hypnotist"))
                {
                    return true;
                }
                Debug.LogFormat("[Town of KTaNE #{0}] The Role that the " + Players_roles[player]+" is attacking is {1}", _modID, Players_roles[targetedpos-1]);
                break;
            }
            x++;
        }
        return false;
    }

    public bool istargetprotbyBG(int playerarrow, int player)
    {
        int x = 0;
        foreach (int arrow in visiting_arrows)
        {
            int Targeted_arrow = visiting_arrows[playerarrow - 1];
            if (playerarrow == x)
            {
                x++;
                continue;
            }
            if (arrow == Targeted_arrow)
            {
                if (Players_roles[x].Equals("Jailor"))
                {
                    Debug.Log("True " + Players_roles[x]);
                    return true;
                }
                break;
            }
        }
        return false;
    }

    public void isGONNADIEHARD(int targetedpos, int i, string def)
    {
        bool targetprot = false;
        int y = 0;
        foreach (int arrow in visiting_arrows)
        {
            if (y == visiting_arrows[i])
            {
                y++;
                continue;
            }
            if (arrow == targetedpos)
            {
                string role = Players_roles[y];
                if (role.Equals("Doctor"))
                {
                    Debug.LogFormat("[Town of KTaNE #{0}] You tried to attack the target but it was healed by Doctor", _modID);
                    Messages[targetedpos-1][1] = "You were attacked and healed by a doctor!";
                    targetprot = true;
                    break;
                }
                else if (role.Equals("Bodyguard"))
                {
                    Debug.LogFormat("[Town of KTaNE #{0}] You tried to attack the target but it was protected by BG", _modID);
                    Messages[targetedpos-1][2] = "You were attacked and protected by a bodyguard!";
                    targetprot = true;
                    break;
                }
                else if (role.Equals("Guardian Angel"))
                {
                    Debug.LogFormat("[Town of KTaNE #{0}] You tried to attack the target but it was healed by GA", _modID);
                    Messages[targetedpos-1][3] = "You were attacked and healed by a Guardian Angel!";
                    targetprot = true;
                    break;
                }
            }
            y++;
        }
        if (def == "None" && !targetprot)
        {
            Debug.LogFormat("[Town of KTaNE #{0}] Target has died " + targetedpos, _modID);
            Messages[targetedpos-1][11] = "You are dead!";
        }
        else if(def != "None")
        {
            Debug.LogFormat("[Town of KTaNE #{0}] The target's defence was too high!", _modID);
            Messages[targetedpos-1][4] = "You were attacked but your defense was too strong!";
        }
        return;
    }

    //void ShowMessages()
    //{
    //    int i = 0;
    //    foreach(var message in Messages)
    //    {
    //        i++;
    //        Debug.LogFormat("Messages for Player {0}!",i);
    //        if(message == null) { continue; }
    //        foreach(var msg in message)
    //        {
    //            if(msg == null) { continue; }
    //            Debug.Log(msg);
    //        }
    //    }
    //}
}
