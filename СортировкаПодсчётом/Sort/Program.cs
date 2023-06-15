const int THREADS_NUM = 4; //число потоков
const int N = 100000;
object locker = new object();



//int[] array = {-10, -5 ,-9, 0, 2, 3, 1, 3, 1, 0, 1};
//int[] sortedArray = CountingSortExtended(array);
Random rand = new Random();
int[] resSerial = new int[N].Select(r => rand.Next(0, 5)).ToArray();
int[] resParallel = new int[N];

Array.Copy(resSerial, resParallel, N);

// Console.WriteLine(string.Join(", ", resSerial));

CountingSortExtended(resSerial);
PraperParallelCountingSort(resParallel);
Console.WriteLine(EqualityMatrix(resSerial, resParallel));

// Console.WriteLine(string.Join(", ", resSerial));
// Console.WriteLine(string.Join(", ", resParallel));



void PraperParallelCountingSort(int[] inputArray)
{
    int max = inputArray.Max();
    int min = inputArray.Min();

    int offset = -min;
    int[] countres = new int[max + offset + 1];

    int eachTreadsCale = N / THREADS_NUM;

    var treadsParall= new List<Thread>();

    for(int i = 0; i < THREADS_NUM;i++)
    {
        int startsPos = i * eachTreadsCale;
        int endPos = (i + 1) * eachTreadsCale;
        if (i == THREADS_NUM - 1) endPos = N;
        treadsParall.Add(new Thread(() => CountingSortParalell(inputArray, countres, offset, startsPos, endPos)));
        treadsParall[i].Start();
    }

    foreach(var threads in treadsParall)
    {
        threads.Join();
    }

    int index = 0;
    for(int i = 0; i < countres.Length; i++)
    {
        for(int j = 0; j < countres[i];j++)
        {
            inputArray[index] = i - offset;
            index++;
        }
    }
}

void CountingSortParalell(int[] inputArray, int[] countres, int offset, int startPos, int endPos)
{
    for(int i = startPos; i < endPos; i++)
    {
        lock (locker)
        {
            countres[inputArray[i] + offset]++;    
        }   
    }
}


void CountingSortExtended(int[] inputArray)
{
    int max = inputArray.Max();
    int min = inputArray.Min();

    int offset = -min; 

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
            inputArray[index] = i - offset;
            index++;
        }
    }
}

bool EqualityMatrix(int[] fmatrix, int[] smatrix)
{
    bool res = true;

    for (int i = 0; i < N; i++)
    {
        res = res && (fmatrix[i] == smatrix[i]);
    }

    return res;
}