using System;
using System.Collections.Generic;
using System.Linq;

public class Coding
{
    public int i;
    public int j;
    public string k;

}
public class Program
{

    public static List<Coding> LZ77Compress(string input)
    {
        List<Coding> dic = new List<Coding>();

        string window = String.Empty;


        bool match;
        int matchIndex;
        string matchArray;

        string s = string.Empty;
        int d = 0;
        int l = 0;
        char c;
        while (input != "")
        {
            match = false;
            if (input.Length >= 4)
            {
                matchArray = input.Substring(0 + 4);
                input = input.Substring(4);
            }
            else
            {
                matchArray = input.Substring(0 + input.Length);
                //input = input.Substring(4);
            }


            matchIndex = -1; // -1 = nie ma
            for (int i = 0; i < 4; i++)
            {
                if (input.Length - 1 < i)
                {
                    break;
                }
                if (matchArray[i] == input[i])
                {
                    match = true;
                    matchIndex = i;
                    matchArray += input[i];
                }
                else
                {
                    break;
                }
            }

            if (match == true)
            {
                d = matchIndex;
                l = matchArray.Length;
                if (input.Length - 1 >= matchIndex + 1)
                {
                    c = input[matchIndex + 1];
                }
                else
                {
                    c = '_';
                }
            }
            else
            {
                d = 0;
                l = 0;
                c = input[0];
            }

            dic.Add(new Coding { i = d, j = l, k = c.ToString() });
            if (window != string.Empty)
            {
                window = window.Substring(l + 1);
            }
            if (l + 1 < input.Length)
            {
                s = input.Substring(0, l + 1);
                window += s;
                input = input.Substring(l + 1);
            }

            if (input.Length == 1)
            {
                input = "";
            }
            //input = input.Substring(l+1);

        }
        return dic;
    }


    public static string Decode(List<Coding> dic)
    {
        string decodedString = string.Empty;
        foreach (var item in dic)
        {
            for (int i = 0 + item.i; i < (item.j + item.i); i++)
            {
                decodedString += decodedString[i - item.i];
            }
            if (item.k != "_")
            {
                decodedString += item.k;
            }
        }
        return decodedString;
    }


    public static void Main()
    {
        List<Coding> Dictionary = new List<Coding>();
        Dictionary = LZ77Compress("ababcbababaaa");
        foreach (var item in Dictionary)
        {
            Console.WriteLine(item.i + " " + item.j + " " + item.k);
        }

        List<Coding> testDec = new List<Coding>();

        testDec.Add(new Coding { i = 0, j = 0, k = "a" });
        testDec.Add(new Coding { i = 0, j = 0, k = "b" });
        testDec.Add(new Coding { i = 2, j = 2, k = "c" });
        testDec.Add(new Coding { i = 0, j = 3, k = "a" });
        testDec.Add(new Coding { i = 0, j = 2, k = "a" });
        testDec.Add(new Coding { i = 2, j = 2, k = "a" });
        testDec.Add(new Coding { i = 0, j = 1, k = "_" });

        Console.WriteLine(Decode(testDec));
    }
}

