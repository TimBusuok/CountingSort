int[] array = {-10, -5 ,-9, 0, 2, 3, 1, 3, 1, 0, 1};
int[] sortedArray = CountingSortExtended(array);

//CountingSort(array);

Console.WriteLine(string.Join(", ", sortedArray));

void CountingSort(int[] inputArray)
{
    int[] countres = new int[10];//массив повторений

    for(int i = 0; i < inputArray.Length; i++)
    {
        //countres[inputArray[i]]++; 
        int ourNumber = inputArray[i];
        countres[ourNumber]++;
    }

    int index = 0;
    for(int i = 0; i < countres.Length; i++)
    {
        for(int j = 0; j < countres[i];j++)
        {
            inputArray[index] = i;
            index++;
        }
    }
}

int[] CountingSortExtended(int[] inputArray)
{
    int max = inputArray.Max();
    int min = inputArray.Min();

    int offset = -min; 

    int[] sortedArray = new int[inputArray.Length];

    int[] countres = new int[max + offset + 1];

    for(int i = 0; i < inputArray.Length; i++)
    {
        countres[inputArray[i] + offset]++;
    }
    
    int index = 0;
    for(int i = 0; i < countres.Length; i++)
    {
        for(int j = 0; j < countres[i];j++)
        {
            sortedArray[index] = i - offset;
            index++;
        }
    }
    return sortedArray;
}