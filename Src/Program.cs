using System;
using System.Linq;
using System.Threading;

class hangman_prgm
{
    public static string[,] hangman_disp = new string[,] { {"|", "-", "-", "-", "-", ""}, { "|", " ", " ", " " /*Head*/, " ", ""},
            { "|", " ", " " /*Left Arm*/, " " /*Torso*/, " " /*Right Arm*/, ""}, {"|", " ", "", " " /*Left Leg*/, " " /*Right Leg*/, ""} };
    public static short add_limbs = 0;
    public static short letters_added = 0;

    static void Main(string[] args)
    {
        Console.WriteLine("[---Welcome to Hangman---]");
        Console.WriteLine("[---Created by: Rim032---]\n");
        Thread.Sleep(250);

        string[] letter_bar = { "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_" };
        bool game_over = false;
        string[] chosen_word = generate_rand_word().Split('.');


        hangman_display(chosen_word, "", letter_bar);
        string correct_letter = hangman_guess(chosen_word);


        while (game_over == false)
        {
            hangman_display(chosen_word, correct_letter, letter_bar);
            correct_letter = hangman_guess(chosen_word);

            if (hangman_win_lose_determine(letter_bar, chosen_word) == 1)
            {
                Console.WriteLine("\n");

                hangman_display(chosen_word, correct_letter, letter_bar);
                Console.WriteLine("\nYou lost!");

                game_over = true;
            }
            else if (hangman_win_lose_determine(letter_bar, chosen_word) == 2)
            {
                Console.WriteLine("\n");

                hangman_display(chosen_word, correct_letter, letter_bar);
                Console.WriteLine("\nYou win!");

                game_over = true;
            }
        }

        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    }

    private static string generate_rand_word()
    {
        string[] word_list = {"A.P.P.L.E", "S.H.A.R.P", "H.U.M.A.N", "C.H.E.M.I.S.T.R.Y", "T.A.B.L.E", "S.E.A.L", "R.A.I.N.B.O.W", "H.A.M.M.E.R",
            "T.U.R.T.L.E", "M.O.U.N.T.A.I.N", "S.T.E.E.L", "B.A.K.E.R.Y", "C.L.O.T.H", "B.L.E.N.D", "K.E.Y", "L.E.M.O.N", "B.U.L.W.A.R.K", "B.O.N.G.O", "S.C.O.T.C.H"};

        Random rand = new Random();
        int rand_index = rand.Next(0, word_list.Length);

        return word_list[rand_index];
    }

    private static void hangman_display(string[] correct_word, string correct_letter, string[] letter_bar)
    {
        //Displaying the actual "hangman."
        for (int j = 0; j < 4; j++)
        {
            for (int k = 0; k < 6; k++)
            {
                if (k == 5)
                {
                    Console.WriteLine("");
                }
                else
                {
                    Console.Write(hangman_disp[j, k]);
                }
            }
        }
        Console.WriteLine();


        //Changing the letter bar if needed.
        for (int a = 0; a < correct_word.Length; a++)
        {
            if (letter_bar[a] == "_" && correct_letter != null)
            {
                if (correct_word[a] == correct_letter)
                {
                    letter_bar[a] = correct_letter;
                    letters_added++;
                    break;
                }
            }
        }

        //Displaying the number bar.
        for (int b = 0; b < correct_word.Length; b++)
        {
            Console.Write(letter_bar[b]);
            Console.Write(" ");
        }

        return;
    }

    private static string hangman_guess(string[] correct_word)
    {
        string user_attempt;
        short correct_arr_pos = 0;
        bool _letter_is_correct = false;

        Console.WriteLine("\nGuess a letter!");
        try
        {
            user_attempt = (Console.ReadLine()).ToLower();

            for (int n = 0; n < correct_word.Length; n++)
            {
                if (user_attempt != null && user_attempt == correct_word[n].ToLower())
                {
                    _letter_is_correct = true;
                    correct_arr_pos = (short)n;
                }
            }

            if (_letter_is_correct == false)
            {
                add_limbs++;
                hangman_disp_add(add_limbs);
            }
        }
        catch (Exception error)
        {
            Console.WriteLine("\n" + error.Message + "\n");
        }

        return correct_word[correct_arr_pos];
    }

    private static void hangman_disp_add(int add_limbs)
    {
        switch (add_limbs)
        {
            case 1:
                hangman_disp[1, 3] = "O";
                break;
            case 2:
                hangman_disp[2, 3] = "|";
                break;
            case 3:
                hangman_disp[2, 2] = "/";
                break;
            case 4:
                hangman_disp[2, 4] = "\\";
                break;
            case 5:
                hangman_disp[3, 3] = "/";
                break;
            case 6:
                hangman_disp[3, 4] = "\\";
                break;
        }
    }

    private static short hangman_win_lose_determine(string[] letter_bar, string[] correct_word)
    {
        short game_end_c = 0;

        if (hangman_disp[1, 3] == "O" && hangman_disp[2, 3] == "|" && hangman_disp[2, 2] == "/" && hangman_disp[2, 4] == "\\" && hangman_disp[3, 3] == "/" && hangman_disp[3, 4] == "\\")
        {
            game_end_c = 1;
        }

        if (letters_added >= correct_word.Length)
        {
            game_end_c = 2;
        }

        return game_end_c;
    }
}