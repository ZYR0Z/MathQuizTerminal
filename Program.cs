Random rd1 = new Random();

/*Console.WriteLine("Choose a difficulty between 1-3!");*/

int difficulty = DropDownMenu(" -) Easy", " -) Medium"," -) Hard");

int score = 0;

Console.Clear();
Exercise(difficulty);

void Exercise(int difficulty)
{
    switch (difficulty)
    {
        case 1:
            int random_1 = rd1.Next(1, 20);
            int random_2 = rd1.Next(1, 20);
            Console.WriteLine("Calculate: " + random_1 + "+" + random_2);
            int result_easy = random_1 + random_2;

            int input_result = Convert.ToInt32(Console.ReadLine());

            console_output(result_easy, input_result);
            break;

        case 2:
            int random_3 = rd1.Next(1, 20);
            int random_4 = rd1.Next(1, 20);
            Console.WriteLine("Calculate: " + random_3 + "*" + random_4);
            int result_medium = random_3 * random_4;

            int input_result_medium = Convert.ToInt32(Console.ReadLine());

            console_output(result_medium, input_result_medium);
            break;

        case 3:
            int random_5 = rd1.Next(1, 200);
            int random_6 = rd1.Next(2, 5);

            while ((random_5 % random_6) != 0)
            {
                random_5 = rd1.Next(1, 200);
                random_6 = rd1.Next(2, 5);
            }

            Console.WriteLine("Calulate: " + random_5 + "/" + random_6);
            int result_hard = random_5 / random_6;
            int input_result_hard = Convert.ToInt32(Console.ReadLine());

            console_output(result_hard, input_result_hard);
            break;

        default:
            Console.WriteLine("Choose a difficulty between 1-3!");
            break;

    }
}

void console_output(int eingabe_ergebnis, int ergebnis)
{
    if (eingabe_ergebnis == ergebnis)
    {
        Console.WriteLine("Correct!");
        score++;
        Console.Clear();
        Write_Score(score.ToString());
        Exercise(difficulty);
    }
    else if (eingabe_ergebnis == --ergebnis || eingabe_ergebnis == ++ergebnis)
    {
        Console.WriteLine("Close!");
        SaveHighScore(score);
        Write_Score(score.ToString());
        Retry(score);
    }
    else
    {
        Console.WriteLine("Not even close!");
        SaveHighScore(score);
        Retry(score);
        /*Write_Score(score.ToString());*/
    }
}

void Retry(int score)
{
    Console.Clear();
    Console.WriteLine($"Your score was: {score}");
    Console.WriteLine("Would you like to retry? (y/n)");

    ResetScore();

    string answer = Convert.ToString(Console.ReadLine());

    if (answer == "y")
    {
        Console.Clear();
        Exercise(difficulty);
    }
    else
    {
        Environment.Exit(0);
    }
}

void ResetScore ()
{
    score = 0;
}

void Write_Score(string score)
{
    int cursor_Top = Console.CursorTop;
    int cursor_Left = Console.CursorLeft;

    int length = score.Length + 15;
    int lines = score.Length + 13;
        string lines_string = "";

    Console.SetCursorPosition(Console.WindowWidth - length, Console.WindowHeight - 5);

    for (int i = 0; i < lines; i++)
    {
            lines_string = lines_string + "═";
    }

    int highScore = GetHighScore();

    Write_Color("╔" + lines_string + "╗", ConsoleColor.DarkCyan);
    Console.SetCursorPosition(Console.WindowWidth - length, Console.WindowHeight - 4);
    Write_Color($"║ Score: {score}     ║", ConsoleColor.DarkCyan);
    Console.SetCursorPosition(Console.WindowWidth - length, Console.WindowHeight - 3);
    Write_Color($"║ Highscore: {highScore} ║", ConsoleColor.DarkCyan);
    Console.SetCursorPosition(Console.WindowWidth - length, Console.WindowHeight - 2);
    Write_Color("╚" + lines_string + "╝", ConsoleColor.DarkCyan);

    Console.SetCursorPosition(cursor_Left, cursor_Top);
}


void Write_Color(string text, ConsoleColor color)
{
    ConsoleColor oldcolor = Console.ForegroundColor;
    Console.ForegroundColor = color;
    Console.WriteLine(text);
    Console.ForegroundColor = oldcolor;
}

void SaveHighScore (int score)
{
    string path = "./score/highscore.txt";

    StreamReader sr = new StreamReader(path);
    
    if (Convert.ToInt32(sr.ReadLine()) < Convert.ToInt32(score))
    {
        sr.Close();
        StreamWriter sw = new StreamWriter(path, false);
            sw.WriteLine(score);
            sw.Close();
    }
}

int GetHighScore()
{
    string path = "./score/highscore.txt";

    StreamReader sr = new StreamReader(path);
    int highScore = Convert.ToInt32(sr.ReadLine());
    sr.Close();

    return highScore;
}

int DropDownMenu(params string[] options)
{
    int currentSelection = 0;

    ConsoleKey key;

    Console.CursorVisible = false;

    do
    {
        Console.Clear();
        Console.WriteLine("Choose a difficulty between 1-3!");

        for (int i = 0; i < options.Length; i++)
        {
            Console.SetCursorPosition(i % 1, 1 + i);

            if (i == currentSelection)
                Console.ForegroundColor = ConsoleColor.DarkCyan;

            Console.Write(options[i]);

            Console.ResetColor();
        }

        key = Console.ReadKey(true).Key;

        switch (key)
        {
            case ConsoleKey.UpArrow:
                {
                    if (currentSelection >= 1)
                        currentSelection -= 1;
                    break;
                }
            case ConsoleKey.DownArrow:
                {
                    if (currentSelection + 1 < options.Length)
                        currentSelection += 1;
                    break;
                }
        }
    } while (key != ConsoleKey.Enter);

    Console.CursorVisible = true;
    Console.SetCursorPosition(0, 0);
    
    return currentSelection + 1;
}