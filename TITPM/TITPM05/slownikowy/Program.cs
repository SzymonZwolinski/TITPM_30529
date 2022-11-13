using System;
using System.Linq;
using System.Collections.Generic;

public class Dic
{
    public int index;
    public string value;

    public void readDic()
    {
        Console.WriteLine(index + ".  " + value);
    }
}

public class Program
{
    #region createDic
    public static List<Dic> dictionary = new List<Dic>();
    public static List<int> code = new List<int>();

    public static string SortString(string input)
    {
        char[] characters = input.ToArray();
        Array.Sort(characters);
        return new string(characters);
    }

    public static void createDic(string input)
    {
        input = SortString(input);
        int i = 1;
        foreach (var character in input.Distinct())
        {
            dictionary.Add(new Dic { index = i, value = character.ToString() });
            i++;
        }
    }

    public static void LZW(string input)
    {
        string c = input[0].ToString();
        int lenOfInput = input.Count();
        int lenOfDic = dictionary.Last().index;

        string s = string.Empty;
        for (int i = 1; i < lenOfInput; i++)
        {
            s = input[i].ToString();
            if (dictionary.Any(x => x.value == (c + s)))
            {
                c = c + s;
            }
            else
            {
                code.Add(dictionary[dictionary.FindIndex(x => x.value == c)].index);
                lenOfDic++;
                dictionary.Add(new Dic { index = lenOfDic, value = (c + s) });
                c = s;
            }

        }
        code.Add(dictionary[dictionary.FindIndex(x => x.value == c)].index);
    }

    #endregion
    public static void ToFile()
    {
        string tempLinesToAddDicValues = string.Empty;
        foreach (var item in dictionary)
        {
            tempLinesToAddDicValues += item.index + " " + item.value+ "\n";
        }
        File.WriteAllText("LZWfile.txt", tempLinesToAddDicValues);
    }

    #region ProgramToDecode
    public static List<Dic> listToRecreateDic= new List<Dic>();
    public static void FromFile()
    {
        int len;
        int indexOfSpace;
        int counterOfLines=0;
        int count = 1;
        foreach(string line in File.ReadLines("LZWfile.txt"))
        {
            counterOfLines++;
        }
        foreach (string line in File.ReadLines("LZWfile.txt"))
        {
            if (count != counterOfLines)
            {
                len = line.Length - 1;
                indexOfSpace = line.IndexOf(' ');
                listToRecreateDic.Add(new Dic
                {
                    index = int.Parse(line.Substring(0, indexOfSpace)),
                    value = line.Substring(indexOfSpace, len)
                });
                count++;
            }
            else
            {
                break;
            }


        }
    }
    public static void DecodeFromList(List<int> code)
    {
        Console.Write("\n");
        Console.Write("\n");
        foreach (var codedChar in code)
        {
            Console.Write(listToRecreateDic.FirstOrDefault(x => x.index == codedChar).value);
        }
    }
    #endregion
    public static void Main()
    {
        string input = string.Empty;
        Console.WriteLine("Podaj słowo: ");
        input = Console.ReadLine();

        createDic(input);

        Console.WriteLine("Slownik Podstawowy:");
        foreach (var item in dictionary)
        {
            item.readDic();
        }
        Console.Write("\n");
        LZW(input);

        foreach (var item in dictionary)
        {
            item.readDic();
        }
        foreach (var integer in code)
        {
            Console.Write(integer);
            Console.Write(" ");
        }

        Console.Write("\n");

        foreach (var integer in code)
        {
            Console.Write(dictionary[integer - 1].value);
            Console.Write(" ");
        }

        ToFile();
        FromFile();
        DecodeFromList(code);
    }
}

