using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using System.Drawing;
using Console = Colorful.Console;
using Colorful;
using CSCore.CoreAudioAPI;
using System.IO;

namespace _6314110007
{
    class Music
    {
        public string name;
        public string passw;
        public string nameplaylist;
        public string playlist;
        public string fileuser = "user.txt";
        public string fileplaylist = "playlist.txt";
        BinarySearchTree<string> treeuser = new BinarySearchTree<string>();
        BinarySearchTree<string> treeplaylist = new BinarySearchTree<string>();
        public Music()
        {
            StreamReader user = new System.IO.StreamReader(fileuser);
            string readuser;
            while ((readuser = user.ReadLine()) != null)
            {
                treeuser.Insert(readuser);
            }
            user.Close();
            StreamReader playlist = new System.IO.StreamReader(fileplaylist);
            string readplaylist;
            while ((readplaylist = playlist.ReadLine()) != null)
            {
                treeplaylist.Insert(readplaylist);
            }
            playlist.Close();
        }
        public void GetPlaylists()
        {
            playlist += name + ":" + nameplaylist;
        }
        public string[] PrintTreeUser()
        {
            string data = "";
            treeuser.PrintInOrder(ref data);
            string[] split = data.Split(new Char[] { '\n' });
            return split;
        }
        public string[] PrintTreePlaylist()
        {
            string data = "";
            treeplaylist.PrintInOrder(ref data);
            string[] split = data.Split(new Char[] { '\n' });
            return split;
        }
        public void ReadUser(string[] user, ref bool chkname, ref bool chkpassw)
        {
            for (int i = 0; i < user.Length - 1; i++)
            {
                string[] split = user[i].Split(new Char[] { ':' });
                if (name == split[0])
                {
                    chkname = true;
                    if (passw == split[1])
                    {
                        chkpassw = true;
                    }
                    break;
                }
            }
        }
        public void ReadUser(string[] user, ref bool chkname)
        {
            for (int i = 0; i < user.Length - 1; i++)
            {
                string[] split = user[i].Split(new Char[] { ':' });
                if (name == split[0])
                {
                    chkname = true;
                    break;
                }
            }
        }
        public void RePassword(string[] user, string name, string passw)
        {
            string old = "";
            string newpassw = "";
            for (int i = 0; i < user.Length - 1; i++)
            {
                string[] split = user[i].Split(new Char[] { ':' });
                if (name == split[0])
                {
                    old = user[i];
                    if (treeuser.Contains(old))
                        treeuser.Remove(old);
                    newpassw = split[0] + ":" + passw;
                    treeuser.Insert(newpassw);
                    break;
                }
            }
        }
        public string Login()
        {
            bool chkname, chkpassw;
            Console.Clear();
            int newpassw = 0;
            string[] user = PrintTreeUser();
            do
            {
                Console.Clear();
                Console.Write("\n\n\n\n\n\t\t\t\t            Login\n\n");
                Console.WriteLine("============================================================================================");
                chkname = false; chkpassw = false;
                Console.Write("\t\t\t     Username: ");
                name = Console.ReadLine();
                Console.Write("\t\t\t     Password: ");
                passw = Console.ReadLine();
                Console.WriteLine("============================================================================================");
                ReadUser(user, ref chkname, ref chkpassw);
                //Console.Write(treeuser.Find("Pim:1234"));
                //Console.ReadKey();

                if (!chkpassw)
                {
                    Console.WriteLine("\t\t\t\tUsername or password is invalid.", Color.Crimson);
                    if (chkname)
                        newpassw++;
                    else newpassw = 0;
                    if (newpassw == 3 && chkname)
                    {
                        newpassw = 0;
                        Console.Write("\t\t\t New Password: ");
                        passw = Console.ReadLine();
                        string passw2;
                        do
                        {
                            Console.Write("\t\t\t  Re-Password: ");
                            passw2 = Console.ReadLine();
                        } while (passw != passw2);
                        RePassword(user, name, passw);
                        Console.WriteLine("============================================================================================");
                        Console.WriteLine("\t\t\t\t   modified successfully.");
                        chkpassw = true;
                        SaveFile();
                    }
                    Console.ReadKey();
                }
            } while (!chkpassw);
            return name;
        }
        public void Register()
        {

            bool chkname;
            do
            {
                Console.Clear();
                Console.Write("\n\n\n\n\n\t\t\t\t         Register\n\n");
                Console.WriteLine("============================================================================================");
                chkname = false;
                do
                {
                    Console.Write("\t\t\t     Username: ");
                    name = Console.ReadLine();
                } while (name == "");
                string[] user = PrintTreeUser();
                ReadUser(user, ref chkname);
                if (chkname)
                {
                    Console.WriteLine("============================================================================================");
                    Console.WriteLine("\t\t\tUsername already exists, please press to try again.", Color.Crimson);
                    Console.ReadKey();
                }
            }
            while (chkname);

            do
            {
                Console.Write("\t\t\t     Password: ");
                passw = Console.ReadLine();
            } while (passw == "");
            string passw2;
            do
            {
                Console.Write("\t\t\t  Re-Password: ");
                passw2 = Console.ReadLine();
            } while (passw != passw2);

            Console.WriteLine("============================================================================================");
            Console.WriteLine("\t\t\t\t   Registed successfully.");
            treeuser.Insert(name + ":" + passw);
            SaveFile();
            Console.ReadKey();
        }
        public int Menu()
        {
            string nameapp = @"
                                                                                                          
          ____                                                                                            
        ,'  , `.                               ,---,                                               ____   
     ,-+-,.' _ |                             ,--.' |                                             ,'  , `. 
  ,-+-. ;   , ||          ,--,               |  |  :       __  ,-.    ,---.      ,---.        ,-+-,.' _ | 
 ,--.'|'   |  ;|        ,'_ /|    .--.--.    :  :  :     ,' ,'/ /|   '   ,'\    '   ,'\    ,-+-. ;   , || 
|   |  ,', |  ':   .--. |  | :   /  /    '   :  |  |,--. '  | |' |  /   /   |  /   /   |  ,--.'|'   |  || 
|   | /  | |  || ,'_ /| :  . |  |  :  /`./   |  :  '   | |  |   ,' .   ; ,. : .   ; ,. : |   |  ,', |  |, 
'   | :  | :  |, |  ' | |  . .  |  :  ;_     |  |   /' : '  :  /   '   | |: : '   | |: : |   | /  | |--'  
;   . |  ; |--'  |  | ' |  | |   \  \    `.  '  :  | | | |  | '    '   | .; : '   | .; : |   : |  | ,     
|   : |  | ,     :  | : ;  ; |    `----.   \ |  |  ' | : ;  : |    |   :    | |   :    | |   : |  |/      
|   : '  |/      '  :  `--'   \  /  /`--'  / |  :  :_:,' |  , ;     \   \  /   \   \  /  |   | |`-'       
;   | |`-'       :  ,      .-./ '--'.     /  |  | ,'      ---'       `----'     `----'   |   ;/           
|   ;/            `--`----'       `--'---'   `--''                                       '---'            
'---'                                                                                                     
                                                                                                          
";
            short curItem = 0, c;
            ConsoleKeyInfo key;
            string[] menu = new string[] { "Music", "Playlists", "Info", "Account", "Logout" };

            do
            {

                Console.Clear();
                Console.WriteLine(nameapp, Color.HotPink);
                Console.Write($"\t\t\t\t\t  Hi {name}, hope you enjoy!\n\n");
                Console.WriteLine("=========================================================================================================");
                for (c = 0; c < menu.Length; c++)
                {
                    if (curItem == c)
                    {

                        Console.Write("\t\t\t      ");
                        Console.BackgroundColor = Color.HotPink;
                        Console.WriteLine("_.:*~*:._.:*~*:._" + menu[c] + "_.:*~*:._.:*~*:._", Color.Ivory);
                    }
                    else
                    {
                        Console.BackgroundColor = Color.Black;
                        Console.Write("\t\t\t\t\t    ");
                        Console.WriteLine(menu[c]);
                    }
                }
                Console.BackgroundColor = Color.Black;
                Console.WriteLine("=========================================================================================================");

                key = Console.ReadKey(true);
                if (key.Key.ToString() == "DownArrow")
                {
                    curItem++;
                    if (curItem > menu.Length - 1) curItem = 0;
                }
                else if (key.Key.ToString() == "UpArrow")
                {
                    curItem--;
                    if (curItem < 0) curItem = Convert.ToInt16(menu.Length - 1);
                }



            } while (key.KeyChar != 13);
            return curItem;
        }
        public void Admin()
        {
            Console.Clear();
            Console.WriteLine("\n\n\n\n\n\t\t\t\t\t       [Database]", Color.HotPink);
            string[] database = PrintTreeUser();
            string[] playlist = PrintTreePlaylist();
            int count = 0;
            Console.WriteLine("=========================================================================================================");
            Console.WriteLine("\t\t\t       Username\t\tPassword\tPlaylist",Color.HotPink);
            Console.WriteLine("=========================================================================================================");
            foreach (string read in database)
            {
                int i = 0;
                string[] split = read.Split(new Char[] { ':' });
                if (read != "")
                {
                    if (count % 10 == 0 && count != 0)
                    {
                        Spot(25); Console.Write("\n\nPress any key to Continue...", Color.HotPink); Console.ReadKey(); Console.Clear();
                        Console.WriteLine("\n\n\n\n\n\t\t\t\t\t       [Database]", Color.HotPink);
                        Console.WriteLine("=========================================================================================================");
                        Console.WriteLine("\t\t\t       Username\t\tPassword\tPlaylist", Color.HotPink);
                        Console.WriteLine("=========================================================================================================");

                    }
                    count++;                                  
                    Console.Write($"\t\t\t       {split[0]}\t\t{split[1]}\t\t",Color.HotPink);
                    foreach(string pl in playlist)
                    {
                        string[] split2 = pl.Split(new Char[] { ':' });
                        if(pl != "")
                        {
                            if (split[0] == split2[0]) i++;
                        }                     
                    }
                    Console.Write(i + "\n",Color.HotPink);                   
                }          
            }
            Console.WriteLine("=========================================================================================================");
            Console.Write("\t\t\t\t");
            Console.BackgroundColor = Color.HotPink;
            Console.WriteLine($" *** Total number of members: {count-1} *** \n",Color.Black);
            Console.BackgroundColor = Color.Black;

        }
        public int Spot(int n)
        {
            System.Threading.Thread.Sleep(20);

            if (n == 0)
            { Console.Write("oOo."); return 0; }
            else
            {
                Console.Write(".oOo"); return Spot(n - 1);
            }
        }
        public string[][] ReadPlaylist()
        {
            string[] user = PrintTreePlaylist(); int arraysize = 0;
            foreach (string read in user)
            {
                string[] split = read.Split(new Char[] { ':' });
                if (split[0] == name)
                {
                    arraysize++;
                }
            }
            string[][] playlist = new string[arraysize][];
            int i = 0;
            foreach (string read in user)
            {
                string[] split = read.Split(new Char[] { ':' });
                if (split[0] == name)
                {
                    playlist[i] = new string[split.Length];
                    for (int r = 0; r < split.Length; r++)
                        playlist[i][r] = split[r];
                    i++;
                }
            }
            return playlist;
        }
        public int MenuPlaylist(ref int lastsize, ref string[] chosen, string[] allmusic, ref bool cancle, ref bool done)
        {
            string[][] playlist = ReadPlaylist();
            short curItem = 0, c;
            ConsoleKeyInfo key;
            string[] menu = new string[playlist.Length + 2];
            lastsize = menu.Length;
            for (int i = 0; i < menu.Length; i++)
            {
                if (i == menu.Length - 1) menu[i] = "Cancel";
                else if (i == 0) menu[i] = "[+]playlist";
                else menu[i] = playlist[i - 1][1];
            }
            do
            {
                Console.Clear();
                Console.Write($"\n\n\n\n\n\t\t\t\t\t       My playlists\n\n");
                Console.WriteLine("=========================================================================================================");
                for (c = 0; c < menu.Length; c++)
                {
                    if (curItem == c)
                    {

                        Console.Write("\t\t\t         ");
                        if (menu[c] == "Cancel") Console.BackgroundColor = Color.Crimson;
                        else Console.BackgroundColor = Color.HotPink;
                        Console.WriteLine("_.:*~*:._.:*~*:._" + menu[c] + "_.:*~*:._.:*~*:._", Color.Ivory);
                    }
                    else
                    {
                        Console.BackgroundColor = Color.Black;
                        Console.Write("\t\t\t\t\t       ");
                        Console.WriteLine(menu[c]);
                    }
                }
                Console.BackgroundColor = Color.Black;
                Console.WriteLine("=========================================================================================================");

                key = Console.ReadKey(true);
                if (key.Key.ToString() == "DownArrow")
                {
                    curItem++;
                    if (curItem > menu.Length - 1) curItem = 0;
                }
                else if (key.Key.ToString() == "UpArrow")
                {
                    curItem--;
                    if (curItem < 0) curItem = Convert.ToInt16(menu.Length - 1);
                }



            } while (key.KeyChar != 13);
            if (curItem == 0)
            {
                Playlist(allmusic, ref cancle, ref done);
            }
            else if (curItem != menu.Length - 1 && curItem != 0)
            {
                chosen = new string[playlist[curItem - 1].Length - 2];
                int cut = 2;
                for (int i = 0; i < chosen.Length; i++)
                {
                    chosen[i] = playlist[curItem - 1][cut++];
                }
            }
            return curItem;
        }
        public void Playlist(string[] allmusic, ref bool cancle, ref bool done)
        {
            LinkedList<string> musiclist = new LinkedList<string>();
            LinkedListIterator<string> theItr;
            Console.Clear();
            Console.Write("\n\n\n\n\n\t\t\t          Give your playlist a name.\n\n");
            Console.WriteLine("============================================================================================");
            do
            {
                Console.Write("\t\t\t     Name: ");
                nameplaylist = Console.ReadLine();
            } while (nameplaylist == "");

            string added = "", current = "";
            theItr = musiclist.Zeroth();
            int ans;
            cancle = true;
            done = true;
            bool dl = true;
            do
            {
                if (dl)
                {
                    int music = Program.Listen(allmusic);
                    musiclist.Insert(allmusic[music], theItr);
                    current = theItr.Retrieve();

                    added += "\t\t\t     " + Program.SplitText(allmusic[music]) + "is added!\n";
                    Console.WriteLine(added, Color.HotPink);
                    Console.ReadKey();
                }
                ans = AddPlaylists();
                dl = true;
                if (ans == 2)
                {
                    cancle = false;
                    //musiclist.MakeEmpty();
                    DeletePlaylist(ref musiclist, allmusic, ref added);
                    Console.WriteLine(added, Color.HotPink);
                    Console.ReadKey();
                    dl = false;
                }
                else if (ans == 1)
                {
                    done = false;
                }
            } while (ans != 1);
            if (ans != 2) PrintList(musiclist);
            SaveFile();
        }
        public void PrintList<AnyType>(LinkedList<AnyType> theList)
        {

            GetPlaylists();
            LinkedListIterator<AnyType> itr = theList.First();
            for (; itr.IsValid(); itr.Advance())
            {
                playlist += ":" + itr.Retrieve();
            }
            treeplaylist.Insert(playlist);
            playlist = "";
        }
        public void DeletePlaylist(ref LinkedList<string> musiclist, string[] menu, ref string added)
        {
            int num = Program.Listen(menu);
            bool chk = true;
            LinkedListIterator<string> itr = musiclist.First();
            if (musiclist.IsEmpty())
            {

            }

            for (; itr.IsValid(); itr.Advance())
            {
                string tmp = itr.Retrieve();
                if (tmp.ToString() == menu[num])
                {
                    musiclist.Remove(menu[num]);
                    added += "\t\t\t     1 " + Program.SplitText(menu[num]) + "is deleted!\n";
                    chk = false;
                    break;
                }
            }
            if (chk)
            {
                added += "\t\t\t     Not Found: " + Program.SplitText(menu[num]) + "\n";
            }
        }
        public int AddPlaylists()
        {
            short curItem = 0, c;
            ConsoleKeyInfo key;
            string[] pause = new string[] { "Add", "Done", "Delete" };
            do
            {
                Console.Clear();
                Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\t\t\t\t\t   =========================");
                Console.Write("\t\t\t\t\t   ");
                for (c = 0; c < pause.Length; c++)
                {
                    if (curItem == c)
                    {
                        Console.BackgroundColor = Color.HotPink;
                        Console.Write("  " + pause[c] + "  ", Color.Ivory);
                    }
                    else
                    {
                        Console.BackgroundColor = Color.Black;
                        Console.Write("  " + pause[c] + "  ");
                    }
                }
                Console.BackgroundColor = Color.Black;
                Console.WriteLine("\n\t\t\t\t\t   =========================");

                key = Console.ReadKey(true);
                if (key.Key.ToString() == "RightArrow")
                {
                    curItem++;
                    if (curItem > pause.Length - 1) curItem = 0;
                }
                else if (key.Key.ToString() == "LeftArrow")
                {
                    curItem--;
                    if (curItem < 0) curItem = Convert.ToInt16(pause.Length - 1);
                }



            } while (key.KeyChar != 13);
            return curItem;
        }
        public int MenuAccount()
        {
            short curItem = 0, c;
            ConsoleKeyInfo key;
            string[] menu = new string[] { "Username", "Password", "Delete Account", "Cancel" };
            do
            {
                Console.Clear();
                Console.Write("\n\n\n\n\n\t\t\t\t            Edit your account\n\n");
                Console.WriteLine("=========================================================================================================");
                for (c = 0; c < menu.Length; c++)
                {
                    if (curItem == c)
                    {

                        Console.Write("\t\t\t         ");
                        if (menu[c] == "Cancel") Console.BackgroundColor = Color.Crimson;
                        else Console.BackgroundColor = Color.HotPink;
                        Console.WriteLine("_.:*~*:._.:*~*:._" + menu[c] + "_.:*~*:._.:*~*:._", Color.Ivory);
                    }
                    else
                    {
                        Console.BackgroundColor = Color.Black;
                        Console.Write("\t\t\t\t\t       ");
                        Console.WriteLine(menu[c]);
                    }
                }
                Console.BackgroundColor = Color.Black;
                Console.WriteLine("=========================================================================================================");

                key = Console.ReadKey(true);
                if (key.Key.ToString() == "DownArrow")
                {
                    curItem++;
                    if (curItem > menu.Length - 1) curItem = 0;
                }
                else if (key.Key.ToString() == "UpArrow")
                {
                    curItem--;
                    if (curItem < 0) curItem = Convert.ToInt16(menu.Length - 1);
                }



            } while (key.KeyChar != 13);
            return curItem;
        }
        public void Account(ref int menu)
        {
            Console.Clear();
            int edit = -1;
            string oldname = name;
            edit = MenuAccount();
            if (edit != 2 && edit != 3)
            {
                Console.Clear();
                Console.Write("\n\n\n\n\t\t\t\t            Edit your account");
                Console.Write("\n\t\t            Please check out your new personal information carefully.\n\n");
                Console.WriteLine("=========================================================================================================");

                if (edit == 0)
                {
                    bool chkname;
                    do
                    {
                        chkname = false;
                        do
                        {
                            Console.Write("\t\t\t     New Username: ");
                            name = Console.ReadLine();
                        } while (name == "");
                        string[] username = PrintTreeUser();
                        ReadUser(username, ref chkname);
                    } while (chkname);
                    ReAccount(oldname);

                }

                else if (edit == 1)
                {
                    do
                    {
                        Console.Write("\t\t\t     New Password: ");
                        passw = Console.ReadLine();
                    } while (passw == "");
                    oldname = name;
                    ReAccount(oldname);
                }
                Console.WriteLine("=========================================================================================================");
                Console.WriteLine("\t\t\t\t\t   modified successfully.");
                if (edit == 0) ReAccount2(oldname);
                SaveFile();
                Console.ReadKey();
            }
            if (edit == 2)
            {
                DeleteAccount(ref menu);
                SaveFile();
            }

        }
        public void DeleteAccount(ref int menu)
        {
            short curItem = 0, c;
            ConsoleKeyInfo key;
            string[] pause = new string[] { "Yes", "No" };
            do
            {

                Console.Clear();
                Console.Write("\n\n\n\n\n\n\n\n\n\n\n\n\n\t\t\t         To ensure that you would like to delete your account.");
                Console.WriteLine("\n\t\t\t\t\t\t    =============");
                Console.Write("\t\t\t\t\t\t    ");
                for (c = 0; c < pause.Length; c++)
                {
                    if (curItem == c)
                    {
                        Console.BackgroundColor = Color.HotPink;
                        Console.Write("  " + pause[c] + "  ", Color.Ivory);
                    }
                    else
                    {
                        Console.BackgroundColor = Color.Black;
                        Console.Write("  " + pause[c] + "  ");
                    }
                }
                Console.BackgroundColor = Color.Black;
                Console.WriteLine("\n\t\t\t\t\t\t    =============");

                key = Console.ReadKey(true);
                if (key.Key.ToString() == "RightArrow")
                {
                    curItem++;
                    if (curItem > pause.Length - 1) curItem = 0;
                }
                else if (key.Key.ToString() == "LeftArrow")
                {
                    curItem--;
                    if (curItem < 0) curItem = Convert.ToInt16(pause.Length - 1);
                }



            } while (key.KeyChar != 13);
            if (curItem == 0)
            {
                DeleteAccount2();
                menu = 4;
            }
        }
        public void DeleteAccount2()
        {
            string[] user = PrintTreePlaylist();
            foreach (string read in user)
            {
                if (read != "")
                {
                    string[] split = read.Split(new Char[] { ':' });
                    if (split[0] == name)
                    {
                        if (treeplaylist.Contains(read))
                            treeplaylist.Remove(read);
                    }
                }

            }
            string[] user2 = PrintTreeUser();
            foreach (string read in user2)
            {
                if (read != "")
                {
                    string[] split = read.Split(new Char[] { ':' });
                    if (split[0] == name)
                    {
                        if (treeuser.Contains(read))
                            treeuser.Remove(read);
                    }
                }

            }
        }
        public void ReAccount(string old)
        {
            string[] user = PrintTreeUser();
            foreach (string read in user)
            {
                if (read != "")
                {
                    string[] split = read.Split(new Char[] { ':' });
                    if (split[0] == old)
                    {
                        if (treeuser.Contains(read))
                            treeuser.Remove(read);
                        treeuser.Insert(name + ":" + passw);
                    }
                }

            }
        }
        public void ReAccount2(string old)
        {
            string[] user = PrintTreePlaylist();
            foreach (string read in user)
            {
                if (read != "")
                {
                    string[] split = read.Split(new Char[] { ':' });
                    if (split[0] == old)
                    {
                        playlist = "";
                        nameplaylist = split[1];
                        GetPlaylists();
                        for (int i = 2; i < split.Length; i++)
                        {
                            playlist += ":" + split[i];
                        }

                        if (treeplaylist.Contains(read))
                            treeplaylist.Remove(read);
                        treeplaylist.Insert(playlist);
                    }
                }
            }
        }
        public void SaveFile()
        {
            string data = "";
            treeuser.PrintInOrder(ref data);
            StreamWriter sw = new StreamWriter(fileuser);
            sw.Write(data);
            sw.Close();
            data = "";
            treeplaylist.PrintInOrder(ref data);
            StreamWriter sw2 = new StreamWriter(fileplaylist);
            sw2.Write(data);
            sw2.Close();
        }

    }
    class Program
    {
        public static string SplitText(string words)
        {
            string[] split = words.Split(new Char[] { '_' });
            string full = "";
            foreach (string s in split)
            {
                if (s.Trim() != "") full += s + " ";
            }
            return full;
        }
        public static int Start()
        {
            short curItem = 0, c;
            ConsoleKeyInfo key;
            string[] menu = new string[] { "Login", "Register", "Exit" };
            do
            {
                Console.Clear();
                Console.Write("\n\n\n\n\n\n\n\n\n\n\n\n\n",Color.HotPink);
                for (c = 0; c < menu.Length; c++)
                {
                    if (curItem == c)
                    {

                        Console.Write("\t\t\t\t      ");
                        Console.BackgroundColor = Color.HotPink;
                        Console.WriteLine("_.:*~*:._.:*~*:._" + menu[c] + "_.:*~*:._.:*~*:._", Color.Ivory);
                    }
                    else
                    {
                        Console.BackgroundColor = Color.Black;
                        Console.Write("\t\t\t\t\t\t     ");
                        Console.WriteLine(menu[c]);
                    }
                }
                Console.BackgroundColor = Color.Black;
                Console.Write("\n\n\n\n\n\n\n\n\n\n\n\n\t\t\t\t\t     Poshnun Oupjan 6314110007", Color.HotPink);
               

                key = Console.ReadKey(true);
                if (key.Key.ToString() == "DownArrow")
                {
                    curItem++;
                    if (curItem > menu.Length - 1) curItem = 0;
                }
                else if (key.Key.ToString() == "UpArrow")
                {
                    curItem--;
                    if (curItem < 0) curItem = Convert.ToInt16(menu.Length - 1);
                }



            } while (key.KeyChar != 13);
            return curItem;
        }
        public static void Mushroom(string[] allmusic, int i)

        {
            Console.Clear();
            Console.Write(@"
           ('
           '|
           |'", Color.LightGray);
            Console.Write(@"
          [::]
          [::]   ", Color.SaddleBrown); Console.Write(@"_......_", Color.Red);
            Console.Write(@"
          [::]", Color.SaddleBrown); Console.Write(@".-'      ", Color.Red); Console.Write(@"_.-", Color.White); Console.Write(@".", Color.Red);
            Console.Write(@"
          [:", Color.SaddleBrown); Console.Write(@".'    ", Color.Red); Console.Write(@".-. '-._.-", Color.White); Console.Write(@"`.", Color.Red);
            Console.Write(@"
          [", Color.SaddleBrown); Console.Write(@"/ ", Color.Red); Console.Write(@"/\   |  \        ", Color.White); Console.Write(@"`-..", Color.Red);
            Console.Write(@"
          / ", Color.Red); Console.Write(@"/ |   `-.'      .-.   ", Color.White); Console.Write(@"`-.", Color.Red);
            Console.Write(@"
         /", Color.Red); Console.Write(@"  `-'            (   `.    ", Color.White); Console.Write(@"`.", Color.Red);
            Console.Write(@"
        |          ", Color.Red); Console.Write(@" /\      `-._/      ", Color.White); Console.Write(@"\            ", Color.Red);
            Console.Write(@"
        '    ", Color.Red); Console.Write(@".'\   /  `.           ", Color.White); Console.Write(@"_.-'|  ", Color.Red);
            Console.Write(@"
       /    ", Color.Red); Console.Write(@"/  /   \_.-'        ", Color.White); Console.Write(@"_.'", Color.Red); Console.Write(@":;:", Color.Peru); Console.Write(@"/", Color.Red);
            Console.Write(@"
     .'      ", Color.Red); Console.Write(@"\_/            ", Color.White); Console.Write(@"_.-'", Color.Red); Console.Write(@":;", Color.Peru); Console.Write(@"_.-'", Color.Red);
            Console.Write(@"
    /", Color.Red); Console.Write(@"   .-.             ", Color.White); Console.Write(@"_.-' ", Color.Red); Console.Write(@"\;", Color.Peru); Console.Write(@".-'", Color.Red);
            Console.Write(@"
   /   ", Color.Red); Console.Write(@"(   \       ", Color.White); Console.Write(@"_..-'     ", Color.Red); Console.Write(@"|", Color.Peru);
            Console.Write(@"
   \    ", Color.Red); Console.Write(@"`._/  ", Color.White); Console.Write(@"_..-'    ", Color.Red); Console.Write(@".--.  ", Color.SaddleBrown); Console.Write(@"|", Color.Peru);
            Console.Write(@"
    `-.....-'", Color.Red); Console.Write(@"/  ", Color.Peru); Console.Write(@"_ _  .'    '.", Color.SaddleBrown); Console.Write(@"|", Color.Peru);
            Console.Write(@"
             |", Color.Peru); Console.Write(@" |_|_| |      | ", Color.SaddleBrown); Console.Write(@"\  ", Color.Peru); Console.Write(@"(", Color.DarkSeaGreen); Console.Write(@"o", Color.Orchid); Console.Write(@")", Color.DarkSeaGreen);
            Console.Write(@"
        (", Color.DarkSeaGreen); Console.Write(@"o", Color.Orchid); Console.Write(@")", Color.DarkSeaGreen); Console.Write(@"  |", Color.Peru); Console.Write(@" |_|_| |      | ", Color.SaddleBrown); Console.Write(@"| ", Color.Peru); Console.Write(@"(\'/)", Color.DarkSeaGreen);
            Console.Write(@"
       (\'/)", Color.DarkSeaGreen); Console.Write(@"/  ", Color.Peru); Console.Write(@"''''' |     o|  ", Color.SaddleBrown); Console.Write(@"\", Color.Peru); Console.Write(@";:;", Color.DarkSeaGreen);
            Console.Write(@"
        :;", Color.DarkSeaGreen); Console.Write(@"  |        ", Color.Peru); Console.Write(@"|      |  ", Color.SaddleBrown); Console.Write(@"|", Color.Peru); Console.Write(@"/)", Color.DarkSeaGreen);
            Console.Write(@"
         ;:", Color.DarkSeaGreen); Console.Write(@" `-.._   ", Color.Peru); Console.Write(@" /__..--'", Color.SaddleBrown); Console.Write(@"\.' ", Color.Peru); Console.Write(@";:", Color.DarkSeaGreen);
            Console.Write(@"
             :;", Color.DarkSeaGreen); Console.Write(@"  `--' ", Color.Peru); Console.Write(@":;   :;", Color.DarkSeaGreen);
            string allmu = SplitText(allmusic[i]);
            Console.WriteLine($"\n\t    {allmu} \t", Color.Ivory);
        }
        public static int Listen(string[] allmusic)
        {

            short curItem = 0, c;
            ConsoleKeyInfo key;
            do
            {
                Console.Clear();
                Console.Write("\n\n\n\n\n\t\t\t            Select the music below.\n\n");
                Console.WriteLine("============================================================================================");
                for (c = 0; c < allmusic.Length; c++)
                {
                    if (curItem == c)
                    {

                        Console.Write("\t\t   ");
                        Console.BackgroundColor = Color.HotPink;
                        string allmu = SplitText(allmusic[c]);
                        Console.WriteLine("_.:*~*:._.:*~*:._" + allmu + "_.:*~*:._.:*~*:._", Color.Ivory);
                    }
                    else
                    {
                        Console.BackgroundColor = Color.Black;
                        Console.Write("\t\t\t\t");
                        string allmu = SplitText(allmusic[c]);
                        Console.WriteLine(allmu);
                    }
                }
                Console.BackgroundColor = Color.Black;
                Console.WriteLine("============================================================================================");

                key = Console.ReadKey(true);
                if (key.Key.ToString() == "DownArrow")
                {
                    curItem++;
                    if (curItem > allmusic.Length - 1) curItem = 0;
                }
                else if (key.Key.ToString() == "UpArrow")
                {
                    curItem--;
                    if (curItem < 0) curItem = Convert.ToInt16(allmusic.Length - 1);
                }



            } while (key.KeyChar != 13);
            return curItem;
        }
        static void Main(string[] args)
        {
            string[] allmusic = new string[]{ "Heavy_mellow_mood-stargazer",
                "Return_of_soapy_smith",
                "The_sunny_way",
                "Power_juice"};
            bool exit = true, cancel = true, done = true;
            int start = -1, menu = -1, lastsize = 0;
            string tail = ".wav";
            string user = "";
            string[] chosen = new string[0];
            Console.ForegroundColor = Color.Aquamarine;
            do
            {
                user = "";
                Music player = new Music();
                start = Start();
                switch (start)
                {
                    case 0:
                        user = player.Login(); menu = -1;
                        break;
                    case 1:
                        player.Register(); menu = -1;
                        break;
                    case 2:
                        exit = false; menu = 4;
                        break;
                }
                while (menu != 4)
                {
                    cancel = true; done = true;
                    if (user != "***")
                    {
                        menu = player.Menu();
                        switch (menu)
                        {
                            case 0:
                                Open(allmusic, tail);
                                break;
                            case 1:
                                int menuplaylist = player.MenuPlaylist(ref lastsize, ref chosen, allmusic, ref cancel, ref done);
                                if (menuplaylist == 0 && done && cancel) player.Playlist(allmusic, ref cancel, ref done);
                                else if (!cancel) ;
                                else if (!done) ;
                                else if (menuplaylist == lastsize - 1) ;
                                else Open(chosen, tail);
                                break;
                            case 2:
                                Info();
                                break;
                            case 3:
                                player.Account(ref menu);
                                break;
                            case 4:
                                break;
                        }
                        player.SaveFile();
                    }
                    else
                    {
                        player.Admin();
                        Console.WriteLine("Press any key to Exit...", Color.HotPink);
                        Console.ReadKey();menu = 4;
                    }
                    
                }
              
            } while (exit);
        }
        public static void Info()
        {
            Console.Clear();
            Console.WriteLine(@"===================================================================================================
 * What's Mushroom?
===================================================================================================
    You're probably reading this.Mushroom is sort of developed music program. 
    It was inspired by music applications.I've picked 2 from all available data structures 
    for the program: Binary Search Tree and Linked-list.
          __
       .-'  |
      /   <\|                Here's a small ASCII picture of 
     /     \'                Santa Claus, drawn by -:
     |_.- o-o                
     / C  -._)\
    /',        |             If you would like to know where 
   |   `-,_,__,'             I acquired the pictures,
   (,,)====[_]=|             I've included links to their websites below.
     '.   ____/ 
      | -|-|_
      |____)_) [n4biS]

    www.asciiart.eu /
    patorjk.com / software / taag /#p=display&f=Pebbles&t=Type%20Something%20
    en.wikipedia.org / wiki / Box - drawing_character
    * And here, music websites where I took the music for creating Mushroom.
    www.freesoundtrackmusic.com
    www.pacdv.com / sounds / index.html
 ================================================================================================== ", Color.Aquamarine);
            Console.WriteLine("Press any key to go back menu.", Color.Aquamarine);
            Console.ReadKey();
        }
        public static void Open(string[] allmusic, string tail)
        {

            int listen = Listen(allmusic);
            string now = "";

            for (int i = listen; i < allmusic.Length; i++)
            {
                SoundPlayer soundPlayer = new SoundPlayer();
                now = allmusic[i] + tail;
                Mushroom(allmusic, i);
                soundPlayer.SoundLocation = now;
                soundPlayer.LoadAsync();
                soundPlayer.Play();
                double s;
                for (int before = 0; before < 5; before++)
                    System.Threading.Thread.Sleep(500);
                var keyRead = false;
                bool stop = true;
                if (Console.KeyAvailable)
                {
                    Console.ReadKey(false); // true = hide input
                    keyRead = false;
                }
                int selectfrompause;
                do
                {

                    selectfrompause = -1;
                    keyRead = Console.KeyAvailable;
                    if (keyRead) selectfrompause = Pause(allmusic, i);
                    if (selectfrompause == 1)
                    {
                        stop = false;
                        i = allmusic.Length - 1;
                        soundPlayer.Stop();
                    }
                    else if (selectfrompause == 0)
                    {
                        stop = false;
                        i -= 2;
                        if (i < -1) i = Convert.ToInt16(allmusic.Length - 2);
                        soundPlayer.Stop();
                    }
                    else if (selectfrompause == 3)
                    {
                        stop = false;
                        if (i == allmusic.Length - 1) i = -1;
                        soundPlayer.Stop();
                    }
                    else if (selectfrompause == 2)
                    {
                        Console.Clear();
                        Mushroom(allmusic, i);
                    }
                    s = CheckSound();
                    //Console.WriteLine($"s: {s}");
                } while (s > 0 && stop);
                stop = true;
                soundPlayer.Stop();
            }


        }
        public static int Pause(string[] allmusic, int i)
        {
            Console.OutputEncoding = System.Text.Encoding.GetEncoding(28591);
            short curItem = 0, c;
            ConsoleKeyInfo key;

            string[] pause = new string[] { "<<", "■", "▶", ">>" };
            do
            {
                Console.OutputEncoding = Encoding.Unicode;
                Console.Clear();
                Mushroom(allmusic, i);
                Console.WriteLine("\t       ===============");
                Console.Write("\t       ");
                for (c = 0; c < pause.Length; c++)
                {
                    if (curItem == c)
                    {
                        if (c == 1) Console.BackgroundColor = Color.Crimson;
                        else if (c == 2) Console.BackgroundColor = Color.LimeGreen;
                        else Console.BackgroundColor = Color.HotPink;
                        Console.Write(" " + pause[c] + " ", Color.Ivory);
                    }
                    else
                    {
                        Console.BackgroundColor = Color.Black;
                        Console.Write(" " + pause[c] + " ");
                    }
                }
                Console.BackgroundColor = Color.Black;
                Console.WriteLine("\n\t       ===============");

                key = Console.ReadKey(true);
                if (key.Key.ToString() == "RightArrow")
                {
                    curItem++;
                    if (curItem > pause.Length - 1) curItem = 0;
                }
                else if (key.Key.ToString() == "LeftArrow")
                {
                    curItem--;
                    if (curItem < 0) curItem = Convert.ToInt16(pause.Length - 1);
                }



            } while (key.KeyChar != 13);
            return curItem;
        }
        public static AudioSessionManager2 GetDefaultAudioSessionManager2(DataFlow dataFlow)
        {
            using (var enumerator = new MMDeviceEnumerator())
            {
                using (var device = enumerator.GetDefaultAudioEndpoint(dataFlow, Role.Multimedia))
                {
                    //Console.WriteLine("DefaultDevice: " + device.FriendlyName);
                    var sessionManager = AudioSessionManager2.FromMMDevice(device);
                    return sessionManager;
                }
            }
        }
        public static double CheckSound()
        {
            double s = 0;
            double s2 = 0;
            double total = 0;

            using (var sessionManager = GetDefaultAudioSessionManager2(DataFlow.Render))
            {
                using (var sessionEnumerator = sessionManager.GetSessionEnumerator())
                {
                    int i = 0;
                    foreach (var session in sessionEnumerator)
                    {
                        using (var audioMeterInformation = session.QueryInterface<AudioMeterInformation>())
                        {
                            s = audioMeterInformation.GetPeakValue();

                        }
                        if (i == 0) s2 = s;
                        i++;
                        total = s2 + s;
                    }
                }
            }
            return total;
        }
    }
}
