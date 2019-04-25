using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shenon_Fano
{
    class Model
    {
        public Character[] myCharacters;
        double sum1 = 0, sum2 = 0;


        public Model(string text)
        {
            myCharacters = new Character[CountUnicLetters(text)];

            int f = CountUnicLetters(text);
            setArrayOfUnicCharacters(myCharacters,text);
            findAmount(myCharacters, text);
            findP(myCharacters, text.Length);
            SortByP(myCharacters);
            findCode(myCharacters);

        }

        void Shannon(Character[] miniCharsArray)
        {

            int idHalf=0;
            double tempSum = 0;
            int k = 0;
            sum1 = 0;
            sum2 = 0;
            double countPHalf = CountP(miniCharsArray) / 2;

            while (sum2 < countPHalf)
            {
                sum1 += miniCharsArray[k].P;
                sum2 = sum1 + miniCharsArray[k+1].P;

                k++;
            }

            if ((sum2-countPHalf) < (countPHalf - sum1))
            {
                for(int i = 0; i < miniCharsArray.Length; i++)
                {
                    tempSum += miniCharsArray[i].P;
                    if (tempSum == sum2)
                    {
                        idHalf = i;
                    }
                }
            }
            else
            {
                for (int i = 0; i < miniCharsArray.Length; i++)
                {
                    tempSum += miniCharsArray[i].P;
                    if (tempSum == sum1)
                    {
                        idHalf = i;
                    }
                }
            }

            Character[] tempArrayUpper = createArrayBefore(miniCharsArray, idHalf);
            Character[] tempArrayBelow = createArrayAfter(miniCharsArray, idHalf);

            if (tempArrayUpper.Length > 1)
            {
                
                Shannon(tempArrayUpper);
            }
              if(tempArrayBelow.Length > 1)
            {
                Shannon(tempArrayBelow);

            }

        }

        void findCode(Character[] chars)
        {
            Shannon(chars);
        }

        Character[] createArrayBefore(Character[] arr,int id)
        {
            Character[] temp=new Character[id+1];

            for(int i = 0; i < temp.Length; i++) // Не включно
            {
                temp[i] = arr[i];
                temp[i].Code += "0";
            }
            return temp;
        }

        Character[] createArrayAfter(Character[] arr, int id)
        {
            Character[] temp = new Character[arr.Length-id-1];

            for (int i = 0; i < temp.Length; i++)
            {
                temp[i] = arr[id+1];
                temp[i].Code += "1";
                id++;
            }
            return temp;
        }


        void findP(Character[] chars, int allLetters)
        {
            foreach(Character c in chars)
            {
                c.P = c.Amount / allLetters;
            }
        }
        void findAmount(Character[] arr,string str)
        {
            int k = 0;
            for (int i = 0; i < str.Length; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    if (str[j] == str[i])
                    {
                        k++;
                    }
                }

                for(int g=0; g < arr.Length; g++)
                {
                    if (arr[g].Letter==str[i])
                    {
                        arr[g].Amount = k+1;
                    }
                    
                }

                k = 0;
            }
        }

        public double CountP(Character[] characters)
        {
            double temp = 0;
            foreach(Character c in characters)
            {
                temp += c.P;
            }
            return temp;
        }
        public void setArrayOfUnicCharacters(Character[] arr,string str)
        {
            int k = -1;
            for (int i = 0; i < str.Length; i++)
            {
                bool appears = false;
                for (int j = 0; j < i; j++)
                {
                    if (str[j] == str[i])
                    {
                        appears = true;
                        break;
                    }
                }

                if (!appears)
                {
                    Character c = new Character();
                    c.Letter = str[i];
                    arr[++k] = c;
                }
            }
        }

        public int CountUnicLetters(string str)
        {
            int count = 0;

            for (int i = 0; i < str.Length; i++)
            {
                bool appears = false;
                for (int j = 0; j < i; j++)
                {
                    if (str[j] == str[i])
                    {
                        appears = true;
                        break;
                    }
                }

                if (!appears)
                {
                    count++;
                }
            }
            return count;
        }

        public void SortByP(Character[] letters)
        {
            Character tempLetter = new Character();
                for (int j = 0; j <= letters.Length - 2; j++)
                {
                    for (int i = 0; i <= letters.Length - 2; i++)
                    {
                        if (letters[i].P > letters[i + 1].P)
                        {
                            tempLetter = letters[i + 1];
                            letters[i + 1] = letters[i];
                            letters[i] = tempLetter;
                        }
                    }
                }
        }
    }

    class Character
    {
        public char Letter { get; set; }
        public double P { get; set; }

        public double Amount { get; set; }

        public string Code { get; set; } = "";

        public override string ToString()
        {
            return Letter +"      "+ P +"      "+ Code+"\n";
        }
    }
}
