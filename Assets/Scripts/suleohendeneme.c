using System;

class Program
{
    static void Main()
    {
        // Rastgele sayılar oluştur
        Random rnd = new Random();
        int rN1, rN2, rN3, rN4;

        rN1 = rnd.Next(0, 10);
        do
        {
            rN2 = rnd.Next(0, 10);
        } while (rN1 == rN2);

        do
        {
            rN3 = rnd.Next(0, 10);
        } while (rN1 == rN3 || rN2 == rN3);

        do
        {
            rN4 = rnd.Next(0, 10);
        } while (rN1 == rN4 || rN2 == rN4 || rN3 == rN4);

        string secretNumber = $"{rN1}{rN2}{rN3}{rN4}";
        string guessedNumber;
        int attempts = 15;
        bool guessedCorrectly = false;

        Console.WriteLine("Welcome to the Number Guessing Game!");
        Console.WriteLine("You have 15 attempts to guess the 4-digit number.");

        // Kullanıcının tahminlerini al
        for (int j = 0; j < attempts; j++)
        {
            int a = 0, b = 0;

            Console.Write("\nEnter your guess (4 digits): ");
            guessedNumber = Console.ReadLine();

            // Tahminin geçerli olup olmadığını kontrol et
            if (guessedNumber.Length != 4 || !int.TryParse(guessedNumber, out _))
            {
                Console.WriteLine("Invalid input. Please enter exactly 4 digits.");
                j--; // Hatalı girişte hakkı düşürme
                continue;
            }

            // Tahmin kontrolü
            for (int i = 0; i < 4; i++)
            {
                if (secretNumber[i] == guessedNumber[i])
                {
                    a++;
                }
                else if (secretNumber.Contains(guessedNumber[i]))
                {
                    b++;
              

