using System;
using System.Collections.Generic;
using System.Linq;


public class Dic
{
    public char symbol;
    public decimal interval;
    public decimal intervalFirstValue;
    public decimal intervalSecondValue;

    public void readDic()
    {
        Console.WriteLine("{" + symbol + " ; " + interval + "}");
    }

    public void readIntFirstV()
    {
        Console.WriteLine("{" + symbol + " ; " + intervalFirstValue + "}");
    }

    public void readDicFirstSecondValue()
    {
    
        Console.WriteLine(symbol + "{" + intervalFirstValue + " , " + intervalSecondValue + "}");

    }
}
public class Program
{
    public List<int> usedPositions = new List<int>();


    public List<Dic> createDic(string input)
    {
        List<Dic> symbolDic = new List<Dic>();
        int inputLen = input.Length;
        decimal intervalTempValue;
        int i = 0;
        foreach (char oneChar in input.Distinct())
        {
            intervalTempValue = (decimal)(input.Count(x => x == oneChar)) / (decimal)inputLen;
            if (i == 0)
            {
                symbolDic.Add(new Dic { symbol = oneChar, interval = intervalTempValue, intervalFirstValue = 0, intervalSecondValue = Math.Abs(1 - intervalTempValue) });
            }
            else
            {
                symbolDic.Add(new Dic { symbol = oneChar, interval = intervalTempValue, intervalFirstValue = symbolDic[i-1].intervalSecondValue, intervalSecondValue = symbolDic[i-1].intervalSecondValue+ intervalTempValue });

            }
            i++;
        }
        return symbolDic;
    }

    public void divideDic(char input, List<Dic> symbolDic, int start, int end)
    {
        int actualPositionOfDivide = 0;
        for (int i = start; i != end; i++)
        {
            if (symbolDic[i].symbol == input && !usedPositions.Contains(i))
            {
                actualPositionOfDivide = i;
                usedPositions.Add(i);
                break;
            }
        }

        int tempIToRecreateDic = 0;
        int counterOfElementsInList = symbolDic.Count();
        for (int i = start; i != end; i++)
        {
            if (tempIToRecreateDic == 0)
            {
                symbolDic.Add(new Dic
                {
                    symbol = symbolDic[tempIToRecreateDic].symbol,
                    interval = symbolDic[actualPositionOfDivide].interval * symbolDic[tempIToRecreateDic].interval,
                    intervalFirstValue = symbolDic[actualPositionOfDivide].intervalFirstValue,
                    intervalSecondValue = symbolDic[actualPositionOfDivide].intervalFirstValue + (symbolDic[actualPositionOfDivide].interval / (i+2))
                });
            }
            else
            {
                symbolDic.Add(new Dic
                {
                    symbol = symbolDic[tempIToRecreateDic].symbol,
                    interval = symbolDic[actualPositionOfDivide].interval * symbolDic[tempIToRecreateDic].interval,
                    intervalFirstValue = symbolDic[counterOfElementsInList-1].intervalSecondValue,
                    intervalSecondValue = symbolDic[counterOfElementsInList - 1].intervalSecondValue  + (symbolDic[actualPositionOfDivide].interval * symbolDic[tempIToRecreateDic].interval)
                });
            }
            counterOfElementsInList++;
            tempIToRecreateDic++;
        }


    }

    public static void Main()
    {
        Program program = new Program();
        List<Dic> symbolDic = new List<Dic>();
        Console.WriteLine("Podaj ciąg znaków: ");
        string input = Console.ReadLine();
        symbolDic = program.createDic(input);
        foreach (var line in symbolDic)
        {
            line.readDic();
        }
        foreach(var line in symbolDic)
        {
            line.readDicFirstSecondValue();
        }
        int i = 1;
        foreach (var character in input)
        {
            program.divideDic(character, symbolDic, ((input.Count() * i) - input.Count()), (input.Count() * i));
            i++;
        }
        Console.WriteLine("ilosc operacji: " + (i));

        int cntr = 0;
        foreach (var line in symbolDic)
        {
            line.readDicFirstSecondValue();
            cntr++;
            if(cntr==4)
            {
                cntr = 0;
                Console.Write("\n");
            }
        }
    }
}
