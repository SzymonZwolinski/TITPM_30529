using System;
using System.Linq;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

internal class TreeNode
{
    public int value; // ilosc wystąpien znaku (suma)
    public char character = ' '; // przechowywany znak
    public char bit; // wartość bitu krawędzi wchodzącej do danego węzła

    public TreeNode parent;
    public TreeNode lChild;
    public TreeNode rChild;

    public override string ToString()
    {
        return $"{character}: {value}";
    }

}

internal class codedChars
{
    public char character;
    public string codeBit;

}
public class Program
{
    #region Pomoc
    private static string Wczytaj_lancuch()
    {
        Console.WriteLine("Podaj łańcuch znaków");
        string temp = Console.ReadLine();
        return temp;
    }

    private static int rozmiarTablicy(string slowo)
    {
        int iloscWystapien = 0;
        foreach (var znak in slowo.Distinct())
        {
            iloscWystapien++;
        }
        return iloscWystapien;
    }

    static string SortString(string input)
    {
        char[] characters = input.ToArray();
        Array.Sort(characters);
        return new string(characters);
    }
    #endregion 


    public static void Main()
    {
        List<TreeNode> korzen = new List<TreeNode>();
        List<codedChars> szyfr = new List<codedChars>();

        string slowo = Wczytaj_lancuch();
        var slowoSortowane = SortString(slowo);
        var rozmTablicy = rozmiarTablicy(slowoSortowane);

        TreeNode[] tablicaDrzew = new TreeNode[rozmTablicy];

        int iDoKolejnychGaleziDrzewa = 0;
        foreach (char znak in slowoSortowane.Distinct())
        {
            tablicaDrzew[iDoKolejnychGaleziDrzewa] = new TreeNode();
            tablicaDrzew[iDoKolejnychGaleziDrzewa].value = slowoSortowane.ToString().Count(x => x == znak);
            tablicaDrzew[iDoKolejnychGaleziDrzewa].character = znak;
            iDoKolejnychGaleziDrzewa++;
        }

        var posortowanaTablicaDrzew = tablicaDrzew.OrderBy(x => x.value).ToArray();
        int aktualnyKorzen = 0;

        #region Tworzenie Drzewa
        if (rozmTablicy > 2)
        {

            for (int i = 0; i <= rozmTablicy; i = i + 2)
            {
                if (i >= rozmTablicy)
                {
                    //Gdy wyjdzie poza zakres 

                    break;
                }
                if (i -1 == rozmTablicy)
                {
                    // został jeden nieparzysty

                    korzen.Add(new TreeNode() { value = korzen[aktualnyKorzen].value + posortowanaTablicaDrzew[i-1].value, lChild = korzen[aktualnyKorzen].value > posortowanaTablicaDrzew[i-1].value ? korzen[aktualnyKorzen] : posortowanaTablicaDrzew[i-1], rChild = korzen[aktualnyKorzen].value > posortowanaTablicaDrzew[i-1].value ? posortowanaTablicaDrzew[i-1] : korzen[aktualnyKorzen] });
                    if (korzen[aktualnyKorzen + 1].lChild == posortowanaTablicaDrzew[i])
                    {
                        posortowanaTablicaDrzew[i-1].bit = '0';
                        korzen[aktualnyKorzen-1].bit = '1';
                    }
                    else
                    {
                       korzen[aktualnyKorzen-1].bit = '1';
                        posortowanaTablicaDrzew[i - 1].bit = '0';
                    }
                    aktualnyKorzen++;
                    break;
                }
                else
                {
                    if (korzen.Count != 0)
                    {                        
                        if (posortowanaTablicaDrzew[i].value > korzen[aktualnyKorzen-1].value && i+1 <= rozmTablicy)
                        {
                            korzen.Add(new TreeNode() { value = posortowanaTablicaDrzew[i].value + posortowanaTablicaDrzew[i + 1].value, lChild = posortowanaTablicaDrzew[i].value > posortowanaTablicaDrzew[i + 1].value ? posortowanaTablicaDrzew[i] : posortowanaTablicaDrzew[i + 1], rChild = posortowanaTablicaDrzew[i].value > posortowanaTablicaDrzew[i + 1].value ? posortowanaTablicaDrzew[i + 1] : posortowanaTablicaDrzew[i] });
                            if (korzen[aktualnyKorzen].lChild == posortowanaTablicaDrzew[i])
                            {
                                posortowanaTablicaDrzew[i].bit = '0';
                                posortowanaTablicaDrzew[i + 1].bit = '1';
                            }
                            else
                            {
                                posortowanaTablicaDrzew[i].bit = '1';
                                posortowanaTablicaDrzew[i + 1].bit = '0';
                            }
                            aktualnyKorzen++;
                            
                        }
                        else
                        {
                            korzen.Add(new TreeNode() { value = korzen[aktualnyKorzen-1].value + posortowanaTablicaDrzew[i].value, lChild = korzen[aktualnyKorzen-1].value > posortowanaTablicaDrzew[i].value ? korzen[aktualnyKorzen-1] : posortowanaTablicaDrzew[i], rChild = korzen[aktualnyKorzen-1].value > posortowanaTablicaDrzew[i].value ? posortowanaTablicaDrzew[i] : korzen[aktualnyKorzen-1] });
                            if (korzen[aktualnyKorzen].lChild == posortowanaTablicaDrzew[i])
                            {
                                posortowanaTablicaDrzew[i].bit = '0';
                                korzen[aktualnyKorzen-1].bit = '1';
                            }
                            else
                            {
                                korzen[aktualnyKorzen-1].bit = '1';
                                posortowanaTablicaDrzew[i].bit = '0';
                            }
                            aktualnyKorzen++;
                            
                        }
                    }
                    else
                    {
                        //utworzenie pierwszej gałęzo-korzenia
                        korzen.Add(new TreeNode() { value = posortowanaTablicaDrzew[i].value + posortowanaTablicaDrzew[i + 1].value, lChild = posortowanaTablicaDrzew[i].value > posortowanaTablicaDrzew[i + 1].value ? posortowanaTablicaDrzew[i] : posortowanaTablicaDrzew[i + 1], rChild = posortowanaTablicaDrzew[i].value > posortowanaTablicaDrzew[i + 1].value ? posortowanaTablicaDrzew[i + 1] : posortowanaTablicaDrzew[i] });
                        if (korzen[aktualnyKorzen].lChild == posortowanaTablicaDrzew[i])
                        {
                            posortowanaTablicaDrzew[i].bit = '0';
                            posortowanaTablicaDrzew[i + 1].bit = '1';
                        }
                        else
                        {
                            posortowanaTablicaDrzew[i].bit = '1';
                            posortowanaTablicaDrzew[i + 1].bit = '0';
                        }
                        aktualnyKorzen++;
                    }
                }

                
            }
        }
        else
        {
            Console.WriteLine("Za mała ilosć znaków do utworzenia korzenia");
        }
        if(rozmTablicy%2 == 0)
        {

            //Gdy parzyste to korzeń łączy górne gałęzie
            korzen.Add(new TreeNode() { value= korzen[aktualnyKorzen-1].value + korzen[aktualnyKorzen-2].value, lChild = korzen[aktualnyKorzen-1].value > korzen[aktualnyKorzen-2].value ? korzen[aktualnyKorzen - 1] : korzen[aktualnyKorzen-2], rChild = korzen[aktualnyKorzen - 1].value > korzen[aktualnyKorzen-2].value ? korzen[aktualnyKorzen-2] : korzen[aktualnyKorzen-1] });
            if (korzen[aktualnyKorzen].lChild == korzen[aktualnyKorzen-1])
            {
                korzen[aktualnyKorzen - 1].bit = '0';
                korzen[aktualnyKorzen - 2].bit = '1';
            }
            else
            {
                korzen[aktualnyKorzen - 1].bit = '1';
                korzen[aktualnyKorzen - 2].bit = '0';
            }
            aktualnyKorzen++;
        }
        #endregion

        string tempBitAdderL = "";
        string tempBitAdderR = "";
        foreach(var a in korzen)
        {
            Console.WriteLine(a.character);
        }
        for (int i = korzen.Count - 1; i >= 0; i--)
        {
            if (korzen[i].lChild.character != ' ')
            { 
                if(tempBitAdderR.Length>0)
                {
                    tempBitAdderL += tempBitAdderR;
                }
                tempBitAdderL += korzen[i].lChild.bit.ToString();
                szyfr.Add(new codedChars { character = korzen[i].lChild.character, codeBit = tempBitAdderL });
                tempBitAdderL = "";
            }
            else
            {
                tempBitAdderL = tempBitAdderL + korzen[i].lChild.bit.ToString()  ;
            }

            if (korzen[i].rChild.character != ' ')
            {
                if (tempBitAdderL.Length > 0)
                {
                    tempBitAdderR += tempBitAdderL;
                }
                tempBitAdderR += korzen[i].rChild.bit.ToString();
                szyfr.Add(new codedChars { character = korzen[i].rChild.character, codeBit = tempBitAdderR });
                tempBitAdderR = "";
            }
            else
            {
                tempBitAdderR = tempBitAdderR + korzen[i].rChild.bit.ToString();
            }
        }
        foreach(var a in szyfr)
        {
            Console.WriteLine("znak: " + a.character + "   Kod:" + a.codeBit);
        }
        string codedString = string.Empty;
       for(int i = 0; i< slowo.Length; i++)
        {
            foreach(var znak in szyfr)
            {
                if(znak.character == slowo[i])
                {
                    codedString += znak.codeBit;
                }
            }
        }
        Console.WriteLine(codedString);
    }
        
    
}




