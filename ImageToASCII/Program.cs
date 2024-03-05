using BigGustave;

class Program
{
    public static void Main(string[] args)
    {
        Pixel pixel;
        char symbol;

        string pathASCII = @"C:\Users\Danil\source\repos\ImageToASCII\ImageToASCII\ASCII.txt";
        string pathImage = "C:\\Users\\Danil\\source\\repos\\ImageToASCII\\ImageToASCII\\image.png";

        FileInfo fi = new FileInfo(pathASCII); // txt file
        FileInfo imageFI = new FileInfo(pathImage); // png file

        FileStream txtFS = fi.OpenWrite();
        FileStream imageFS = imageFI.OpenRead();

        StreamWriter symbolWriter = new StreamWriter(txtFS);
        Png image = Png.Open(imageFS);

        int brightness = 0;

        Console.WriteLine(image.Header.Height);
        Console.WriteLine(image.Header.Width);

        for (int i = 0; i < image.Header.Height - 1; i++)
        {
            for (int j = 0; j < image.Header.Width - 1; j++)
            {
                pixel = image.GetPixel(j, i);
                brightness = (int)(0.299 * pixel.R + 0.587 * pixel.G + 0.114 * pixel.B);
                if (brightness >= 230) symbol = '.';
                else if (brightness >= 200) symbol = '^';
                else if (brightness >= 170) symbol = '/';
                else if (brightness >= 140) symbol = ':';
                else if (brightness >= 100) symbol = '%';
                else if (brightness >= 70) symbol = '#';
                else if (brightness >= 40) symbol = '▓';
                else if (brightness >= 20) symbol = '█';
                else symbol = '█';
                symbolWriter.Write(symbol);
            }
            symbolWriter.WriteLine();
        }

        symbolWriter.Close();
        txtFS.Close();
        imageFS.Close();
    }
}