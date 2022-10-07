/******************************************************************************

                              Online C++ Compiler.
               Code, Compile, Run and Debug C++ program online.
Write your code in this editor and press "Run" button to compile and execute it.

*******************************************************************************/

#include <iostream>
#include <bitset>
#include <string>
#include <sstream>
#include <math.h>
using namespace std;

string BinaryStringToText(string binaryString) {
    string text = "";
    stringstream sstream(binaryString);
    while (sstream.good())
    {
        bitset<8> bits;
        sstream >> bits;
        text += char(bits.to_ulong());
    }
    return text;
}

string Xor (string BinarneSlowo, string BinarnyKlucz)
{
  string wynik;
  for (int i = 0; i < BinarneSlowo.length (); i++)
    {
          if(BinarnyKlucz[i] == BinarneSlowo[i])
          {
              wynik +="0";
          }
          else
    	{
    	  wynik += "1";
    	}
    }
  cout << wynik << endl << BinarneSlowo << endl << BinarnyKlucz<<endl<<endl;
  return wynik;
}

string Szyfrowanie (string slowo, string klucz)
{
  string wynik;
  string tempStringBit;
  string kluczToBit =
    bitset < 8 > (klucz[0]).to_string () + bitset < 8 >
    (klucz[0]).to_string () + bitset < 8 > (klucz[0]).to_string () + bitset <
    8 > (klucz[0]).to_string ();
  string wynikXor;
  for (int i =0 ; i <slowo.length (); i++)
    {

      tempStringBit =
	bitset < 8 > (slowo[i]).to_string () + bitset < 8 >
	(slowo[i - 1]).to_string () + bitset < 8 >
	(slowo[i - 2]).to_string () + bitset < 8 >
	(slowo[i - 3]).to_string ();
      wynikXor = Xor (tempStringBit, kluczToBit);
      

      if (i == 0)
	{
	  break;
	}
    }
    wynik = BinaryStringToText(wynikXor);

    return wynik;
}

string Odszyfrowanie(string slowo, string klucz)
{
    string wynik;
    string tempStringBit;
    string kluczToBit = bitset < 8 > (klucz[0]).to_string () + bitset < 8 >
    (klucz[0]).to_string () + bitset < 8 > (klucz[0]).to_string () + bitset <
    8 > (klucz[0]).to_string ();
    string wynikXor;
    for (int i =0; i < slowo.length(); i++)
    {

      tempStringBit =
	bitset < 8 > (slowo[i]).to_string () + bitset < 8 >
	(slowo[i - 1]).to_string () + bitset < 8 >
	(slowo[i - 2]).to_string () + bitset < 8 >
	(slowo[i - 3]).to_string ();
      wynikXor = Xor (tempStringBit, kluczToBit);
      if (i == 0)
	{
	  break;
	}
    }
    
    wynik = BinaryStringToText(wynikXor);
    return wynik;
}


int
main ()
{
  
  string a = Szyfrowanie ("aaaa", "a");
  string b = Odszyfrowanie(a,"a");
  cout << a <<"    "<< b << endl;
  return 0;
}
