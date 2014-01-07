/***********************************************************/
/*************    BATTLESHIPS by Joshua Ong    *************/
/***********************************************************/

// Correct Ship Placement & Display of Sea ✓
// Computer Response to Player Moves ✓
// Computer Move Production & Test ✓
// Game End Detection ✓
// Breakdown Into Appropriate Methods ✓
// Effective Data ✓
// Two Player version ✓
// Automatic Win Detection for the Computer Player ✓
// Appropriate Identifier Names ✓
// consistent & Appropriate Layout ✓

using System;
using System.Threading;

class BattleShipsFinal
{
    // Declares values within the class
    const int SEA_SIZE = 10;

    const int STATE_EMPTY = 0;
    const int STATE_ATTACKED = 1;
    const int STATE_MISSED = 2;
    const int STATE_UNKNOWN = 7;

    const int SHIP_BATTLESHIP = 3;
    const int SHIP_CRUISER = 4;
    const int SHIP_SUBMARINE = 5;
    const int SHIP_ROWINGBOAT = 6;

    static bool validGameMode = false;
    static bool ValidInput = false;
    static bool ValidAttack = false;
    static bool validPlayerShipsPlacement = false;
    static bool validMultiplayerShipPlacement = false;
    static bool validTestModeAttack = false;
    static bool validSinglePlayerAttack = false;
    static bool validMultiplayerAttack = false;  
    static bool GameOver = false;
    
    static int gameMode;
    static int xCoord = 0;
    static int yCoord = 0;
    static int CPUBoatHitCount = 0;
    static int CPUMovesCounter = 0;
    static int CPUBoatCount = 0;
    static int playerBoatHitCount = 0;
    static int playerMovesCounter = 0;

    static string gameModeString;
    static string playerInput = "";
    static string error = "";
    static string lineLong = "===============================================================================================================================================================";





    //**** My Libarary ****//
    // Centres strings in a WriteLine
    public static void centerString(string s)
    {
        Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (s.Length / 2)) + "}", s));
    }

    // Centre strings in a Write
    public static void centerStringWrite(string s)
    {
        Console.Write(String.Format("{0," + ((Console.WindowWidth / 2) + (s.Length / 2)) + "}", s));
    }




    //**** User Interface ****//
    // Prints Game Title
    public static void printTitle()
    {
        string title1 = "########     ###    ######## ######## ##       ########  ######  ##     ## #### ########   ###### ";
        string title2 = "##     ##   ## ##      ##       ##    ##       ##       ##    ## ##     ##  ##  ##     ## ##    ##";
        string title3 = "##     ##  ##   ##     ##       ##    ##       ##       ##       ##     ##  ##  ##     ## ##      ";
        string title4 = "########  ##     ##    ##       ##    ##       ######    ######  #########  ##  ########   ###### ";
        string title5 = "##     ## #########    ##       ##    ##       ##             ## ##     ##  ##  ##              ##";
        string title6 = "##     ## ##     ##    ##       ##    ##       ##       ##    ## ##     ##  ##  ##        ##    ##";
        string title7 = "########  ##     ##    ##       ##    ######## ########  ######  ##     ## #### ##         ###### ";
        
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(lineLong + "\n");
        centerString(title1);
        centerString(title2);
        centerString(title3);
        Console.ForegroundColor = ConsoleColor.Blue;
        centerString(title4);
        centerString(title5);
        Console.ForegroundColor = ConsoleColor.DarkBlue;
        centerString(title6);
        centerString(title7);
        Console.WriteLine("\n" + lineLong);
        Console.ResetColor();
    }

    // Prints Main Menu
    public static void startScreen()
    {
        string WelcomeMessage = "Welcome to the game of Battleships.";
        string Aim = "The aim of the game is to defeat the enemy ships.";
        string How = "To do this you enter sea coordinates in a turn-by-turn basis.\n";
        string Mode1 = "1. Test Mode";
        string Mode1Desc = "'A visible CPU board for testing purposes only'";
        string Mode2 = "2. Single Player Mode";
        string Mode2Desc = "'Pre-set CPU ships but hidden in a sea'";
        string Mode3 = "3. Multiplayer Mode";
        string Mode3Desc = "'Both CPU & player boards, aswell as player ship coordinate entry'";

        Console.WriteLine("");
        Console.ForegroundColor = ConsoleColor.Cyan;
        centerString(WelcomeMessage);
        centerString(Aim);
        centerString(How);

        Console.WriteLine("");
        Console.ForegroundColor = ConsoleColor.Cyan;
        centerString(Mode1);
        Console.ResetColor();
        Console.ForegroundColor = ConsoleColor.Blue;
        centerString(Mode1Desc);
        
        Console.WriteLine("");
        Console.ForegroundColor = ConsoleColor.Cyan;
        centerString(Mode2);
        Console.ResetColor();
        Console.ForegroundColor = ConsoleColor.Blue;
        centerString(Mode2Desc);
        
        Console.WriteLine("");
        Console.ForegroundColor = ConsoleColor.Cyan;
        centerString(Mode3);
        Console.ResetColor();
        Console.ForegroundColor = ConsoleColor.Blue;
        centerString(Mode3Desc);
        
        Console.ForegroundColor = ConsoleColor.DarkBlue;
        Console.WriteLine("\n" + lineLong);
        Console.ResetColor();

        do
        {
            try // Asks for game mode until a valid choice is made 
            {
                do
                {
                    string Message1 = "Please select your desired game mode: ";

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("");
                    centerStringWrite(Message1);
                    Console.ForegroundColor = ConsoleColor.Green;
                    gameModeString = Console.ReadLine();
                    Console.ResetColor();
                    gameMode = int.Parse(gameModeString);

                    if ((gameMode > 3) || (gameMode < 1))
                    {
                        error = "Please choose a number either 1, 2, or 3.";
                        Console.ForegroundColor = ConsoleColor.Red;
                        centerString(error);
                        Console.ResetColor();
                    }
                    else 
                    {
                        validGameMode = true;
                    }
                }
                while ((gameMode > 3) || (gameMode < 1)); // Selected game mode must be between 1 & 3 to be valid.
            }
            catch (FormatException) // Catches FormatExceptions & loops until there's no exception.
            {
                error = " Please enter a number in digit form (e.g. 3)";

                Console.ForegroundColor = ConsoleColor.Red;
                centerString(error);
                Console.ResetColor();
            }
            catch (OverflowException)
            {
                error = " Please either choose game mode 1, 2, or 3. There are no hidden easter eggs here.";

                Console.ForegroundColor = ConsoleColor.Red;
                centerString(error);
                //Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (error.Length / 2)) + "}", error));
                Console.ResetColor();
            }
        }
        while (!validGameMode);
    }

    // Prints Test Mode Introduction
    public static void testMode()
    {
        string notice1 = "!!! Please note, this mode is strictly for demonstration purposes only !!!"; // Confirms that test mode has been entered.
        string notice2 = "... or for cheats";

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(lineLong);
        centerString(notice1);
        centerString(notice2);
        Console.WriteLine(lineLong);
        Console.ResetColor();
    }

    // Prints Single-Player Mode Introduction
    public static void singleMode()
    {
        string playerImage1 = " ##############                                                ";
        string playerImage2 = " #           #                                                 ";
        string playerImage3 = " ##################                                            ";
        string playerImage4 = " ##################                                            ";
        string playerImage5 = " #############                                                 ";
        string playerImage6 = " #           #                                                 ";
        string playerImage7 = " #  #     #  #                                                 ";
        string playerImage8 = " #           #    ____________________________________________ ";
        string playerImage9 = " #           #   (                                            )";
        string playerImage10 = "#   #####   #  <  You really think you can beat me? Lets go! )";
        string playerImage11 = "#           #   (____________________________________________)";
        string playerImage12 = "#############                                                 ";

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("");
        centerString(playerImage1);
        centerString(playerImage2);
        centerString(playerImage3);
        centerString(playerImage4);
        centerString(playerImage5);
        centerString(playerImage6);
        centerString(playerImage7);
        centerString(playerImage8);
        centerString(playerImage9);
        centerString(playerImage10);
        centerString(playerImage11);
        centerString(playerImage12);
        Console.ForegroundColor = ConsoleColor.DarkBlue;
        Console.WriteLine("\n" + lineLong);
        Console.ResetColor();
        Thread.Sleep(2500);
    }

    // Prints Multi-Player Mode Introduction
    public static void multiMode()
    {
        string playerImage1 = " ##############                                        ";
        string playerImage2 = " #           #                                         ";
        string playerImage3 = " ##################                                    ";
        string playerImage4 = " ##################                                    ";
        string playerImage5 = " #############                                         ";
        string playerImage6 = " #           #                                         ";
        string playerImage7 = " #  #     #  #                                         ";
        string playerImage8 = " #           #    ____________________________________ ";
        string playerImage9 = " #           #   (                                    )";
        string playerImage10 = "#   #####   #  <  Place your ships then bring it on! )";
        string playerImage11 = "#           #   (____________________________________)";
        string playerImage12 = "#############                                         ";

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("");
        centerString(playerImage1);
        centerString(playerImage2);
        centerString(playerImage3);
        centerString(playerImage4);
        centerString(playerImage5);
        centerString(playerImage6);
        centerString(playerImage7);
        centerString(playerImage8);
        centerString(playerImage9);
        centerString(playerImage10);
        centerString(playerImage11);
        centerString(playerImage12);
        Console.ForegroundColor = ConsoleColor.DarkBlue;
        Console.WriteLine("\n" + lineLong);
        Console.ResetColor();
        Thread.Sleep(2500);
    }




    //**** Sea Arrays ****//
    // Creates CPU & Player Board Arrays
    static int[,] seaPlayerArray = new int[SEA_SIZE, SEA_SIZE];
    static int[,] seaCPUArray = new int[SEA_SIZE, SEA_SIZE];

    // Sets All Player Arrays to 0 (empties Player sea)
    public static void emptyPlayerSea()
    {
        for (int row = 0; row < 10; row++)
        {
            for (int column = 0; column < 10; column++)
            {
                seaPlayerArray[column, row] = STATE_EMPTY;
            }
        }
    }

    // Sets All CPU Arrays to 0 (empties CPU sea)
    public static void emptyCPUSea()
    {
        for (int row = 0; row < 10; row++)
        {
            for (int column = 0; column < 10; column++)
            {
                seaCPUArray[column, row] = STATE_EMPTY;
            }
        }
    }




    //**** Ship Placement ****//
    // Prints Ship Placement Instructions/Intro
    public static void shipInstructions()
    {
        string notice = "Your going to have to place all of your 7 ships to play. There are:";
        string boats1 = "2x Battleships";
        string boats2 = "3x Cruisers";
        string boats3 = "1x Submarine";
        string boats4 = "1x Rowingboat";
        string how = "To place your ships simply follow the on screen instructions.";
        string format = "When asked for coordinates, enter them as digits (e.g. '3' not 'three')";

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("");
        centerString(notice);
        Console.WriteLine("");
        centerString(boats1); 
        centerString(boats2); 
        centerString(boats3); 
        centerString(boats4);
        Console.WriteLine("");
        centerString(how);
        centerString(format);
        Console.ForegroundColor = ConsoleColor.DarkBlue;
        Console.WriteLine("\n" + lineLong);
        Console.ResetColor();
    }

    // Places Pre-Determined (Test Mode) CPU Ships on Board
    public static void placeCPUShips()
    {
        seaCPUArray[0, 0] = SHIP_CRUISER;
        seaCPUArray[0, 5] = SHIP_BATTLESHIP;
        seaCPUArray[2, 0] = SHIP_CRUISER;
        seaCPUArray[2, 7] = SHIP_BATTLESHIP;
        seaCPUArray[4, 3] = SHIP_CRUISER;
        seaCPUArray[5, 6] = SHIP_SUBMARINE;
        seaCPUArray[8, 8] = SHIP_ROWINGBOAT;
    }

    // Places Random CPU Ships on Board
    public static void placeRandomCPUShips()
    {
        int boatX;
        int boatY;

        do
        {
            Random randCPUShipPlacement = new Random();                                             // Creates new random stream
            boatX = randCPUShipPlacement.Next(0, 10);                                               // Generates a random x coordinate
            boatY = randCPUShipPlacement.Next(0, 10);                                               // Generates a random y coordinate

            if (seaCPUArray[boatX, boatY] == STATE_EMPTY)
            {
                if ((CPUBoatCount == 0) || (CPUBoatCount==1))                                       // 2x battleships
                {
                    seaCPUArray[boatX, boatY] = SHIP_BATTLESHIP;
                }
                else if ((CPUBoatCount == 2) || (CPUBoatCount == 3) || (CPUBoatCount == 4))         // 3x cruisers
                {
                    seaCPUArray[boatX, boatY] = SHIP_CRUISER;                                       
                }
                else if (CPUBoatCount == 5)                                                         // 1x submarine
                {
                    seaCPUArray[boatX, boatY] = SHIP_SUBMARINE;
                }
                else if (CPUBoatCount == 6)                                                         // 1x rowingboat
                {
                    seaCPUArray[boatX, boatY] = SHIP_ROWINGBOAT; 
                }
                CPUBoatCount++;                                                                     // Increases CPU Boat Count by 1 to help determine what type of boat needs to be placed next
            }
        }
        while (CPUBoatCount < 7);
    }

    // Asks, Validates & Places Player Ships for Multiplayer Mode
    public static void askPlayerShips()
    {
        do
        {
            int BattleshipX, BattleshipY;
            int Battleship2X, Battleship2Y;
            int CruiserX, CruiserY;
            int Cruiser2X, Cruiser2Y;
            int Cruiser3X, Cruiser3Y;
            int SubmarineX, SubmarineY;
            int RowingboatX, RowingboatY;

            bool ValidBattleship = false;
            bool ValidBattleship2 = false;
            bool ValidCruiser = false;
            bool ValidCruiser2 = false;
            bool ValidCruiser3 = false;
            bool ValidSubmarine = false;
            bool ValidRowingboat = false;

            /* Battleship Entry & Validation */
            do
            {
                string enterXCord = "Enter the X coordinate for your battleship: ";
                string enterYCord = "Enter the Y coordinate for your battleship: ";
                
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("");
                centerStringWrite(enterXCord);                                                                // Asks for x coordinate of battleship
                Console.ForegroundColor = ConsoleColor.Green;
                BattleshipX = int.Parse(Console.ReadLine());
                
                Console.ForegroundColor = ConsoleColor.Yellow;
                centerStringWrite(enterYCord);                                                                // Asks for y coordinate of battleship
                Console.ForegroundColor = ConsoleColor.Green;
                BattleshipY = int.Parse(Console.ReadLine());
                Console.ResetColor();

                if ((BattleshipX >= 0 && BattleshipX < 10) && (BattleshipY >= 0 && BattleshipY < 10))       // Checks the x & y coordinates are between 0-9
                {
                    string placed = "One battleship placed at " + BattleshipX + ", " + BattleshipY;
                    seaPlayerArray[BattleshipX, BattleshipY] = SHIP_BATTLESHIP;                             // Valid battleship is placed
                    Console.ForegroundColor = ConsoleColor.Green;
                    centerStringWrite(placed);
                    Console.ResetColor();
                    ValidBattleship = true;                                                                 // Exits do construction.
                }
                else                                                                                        // If not between 0 & 9, it re-asks entry.
                {
                    error = "Please enter values between the range 0-9";
                    Console.ForegroundColor = ConsoleColor.Red;
                    centerStringWrite(error);
                    Console.ResetColor();
                }
            } while (!ValidBattleship);                                                                     // Loops until a valid battleship coordinate has been entered.

            /* Battleship 2 Entry & Validation */
            do
            {
                string enterXCord = "Enter the X coordinate for your second battleship: ";
                string enterYCord = "Enter the Y coordinate for your second battleship: ";

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n");
                centerStringWrite(enterXCord);                                                                // Asks for x coordinate of second battleship
                Console.ForegroundColor = ConsoleColor.Green;
                Battleship2X = int.Parse(Console.ReadLine());
                
                Console.ForegroundColor = ConsoleColor.Yellow;
                centerStringWrite(enterYCord);                                                                // Asks for y coordinate of second battleship
                Console.ForegroundColor = ConsoleColor.Green;
                Battleship2Y = int.Parse(Console.ReadLine()); 
                Console.ResetColor();

                if ((Battleship2X >= 0 && Battleship2X < 10) && (Battleship2Y >= 0 && Battleship2Y < 10))   // Checks the x & y coordinates are between 0-9
                {
                    if ((Battleship2X == BattleshipX) && (Battleship2Y == BattleshipY))                     // Checks there's not already a boat there
                    {
                        error = "Please enter values where you haven't already place a boat";
                        Console.ForegroundColor = ConsoleColor.Red;
                        centerStringWrite(error);
                        Console.ResetColor();
                        ValidBattleship2 = false;
                    }
                    else
                    {
                        string placed = "That's your second battleship placed at " + Battleship2X + ", " + Battleship2Y;
                        seaPlayerArray[Battleship2X, Battleship2Y] = SHIP_BATTLESHIP;                       // Valid second battleship is placed
                        Console.ForegroundColor = ConsoleColor.Green;
                        centerStringWrite(placed);
                        Console.ResetColor();
                        ValidBattleship2 = true;                                                            // Exits do construction.
                    }
                }
                else                                                                                        // If not between 0 & 9, it re-asks entry.
                {
                    error = "Please enter values between the range 0-9";
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(String.Format("{0," + ((Console.WindowWidth / 2) + (error.Length / 2)) + "}", error));
                    Console.ResetColor();
                }
            } while (!ValidBattleship2);                                                                    // Loops until a valid second battleship coordinate has been entered.

            /* Cruiser Entry & Validation */
            do
            {
                string enterXCord = "Enter the X coordinate for your cruiser: ";
                string enterYCord = "Enter the Y coordinate for your cruiser: ";
                
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n");
                centerStringWrite(enterXCord);                                                                // Asks for x coordinate of cruiser
                Console.ForegroundColor = ConsoleColor.Green;
                CruiserX = int.Parse(Console.ReadLine());
                Console.ForegroundColor = ConsoleColor.Yellow;

                centerStringWrite(enterYCord);                                                                // Asks for y coordinate of cruiser
                Console.ForegroundColor = ConsoleColor.Green;
                CruiserY = int.Parse(Console.ReadLine());
                Console.ResetColor();

                if ((CruiserX >= 0 && CruiserX < 10) && (CruiserY >= 0 && CruiserY < 10))                   // Checks the x & y coordinates are between 0-9
                {
                                                                                                            // Checks there's not already a boat there
                    if (((CruiserX == BattleshipX) && (CruiserY == BattleshipY)) || ((CruiserX == Battleship2X) && (CruiserY == Battleship2Y)))
                    {
                        error = "Please enter values where you haven't already place a boat";
                        Console.ForegroundColor = ConsoleColor.Red;
                        centerStringWrite(error); 
                        Console.ResetColor();
                        ValidCruiser = false;
                    }
                    else                                                                                    
                    {
                        string placed = "Cruiser now at " + CruiserX + ", " + CruiserY;
                        seaPlayerArray[CruiserX, CruiserY] = SHIP_CRUISER;                                  // Valid cruiser is placed
                        Console.ForegroundColor = ConsoleColor.Green;
                        centerStringWrite(placed); 
                        Console.ResetColor();
                        ValidCruiser = true;
                    }
                }
                else                                                                                        // If not between 0 & 9, it re-asks entry.
                {
                    error = "Please enter values between the range 0-9";
                    Console.ForegroundColor = ConsoleColor.Red;
                    centerStringWrite(error); ;
                    Console.ResetColor();
                    ValidCruiser = false;
                }
            } while (!ValidCruiser);                                                                        // Loops until a valid cruiser coordinate has been entered.

            /* Cruiser 2 Entry & Validation */
            do
            {
                string enterXCord = "Enter the X coordinate for your second cruiser: ";
                string enterYCord = "Enter the Y coordinate for your second cruiser: ";

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n");
                centerStringWrite(enterXCord);                                                                // Asks for x coordinate of second cruiser
                Console.ForegroundColor = ConsoleColor.Green;
                Cruiser2X = int.Parse(Console.ReadLine());
                
                Console.ForegroundColor = ConsoleColor.Yellow;
                centerStringWrite(enterYCord);                                                                // Asks for y coordinate of second cruiser
                Console.ForegroundColor = ConsoleColor.Green;
                Cruiser2Y = int.Parse(Console.ReadLine());
                Console.ResetColor();

                if ((Cruiser2X >= 0 && Cruiser2X < 10) && (Cruiser2Y >= 0 && Cruiser2Y < 10))               // Checks the x & y coordinates are between 0-9
                {
                                                                                                            // Checks there's not already a boat there
                    if (((Cruiser2X == BattleshipX) && (Cruiser2Y == BattleshipY)) || ((Cruiser2X == Battleship2X) && (Cruiser2Y == Battleship2Y)) || ((Cruiser2X == CruiserX) && (Cruiser2Y == CruiserY)))
                    {
                        error = "Please enter values where you haven't already place a boat";
                        Console.ForegroundColor = ConsoleColor.Red;
                        centerStringWrite(error);
                        Console.ResetColor();
                        ValidCruiser2 = false;
                    }
                    else                                                                                    
                    {
                        string placed = "Roger that. Cruiser 2 at " + Cruiser2X + ", " + Cruiser2Y;
                        seaPlayerArray[Cruiser2X, Cruiser2Y] = SHIP_CRUISER;                                // Valid second cruiser is placed
                        Console.ForegroundColor = ConsoleColor.Green;
                        centerStringWrite(placed);
                        Console.ResetColor();
                        ValidCruiser2 = true;
                    }
                }
                else                                                                                        // If not between 0 & 9, it re-asks entry.
                {
                    error = "Please enter values between the range 0-9";
                    Console.ForegroundColor = ConsoleColor.Red;
                    centerStringWrite(error); 
                    Console.ResetColor();
                    ValidCruiser2 = false;
                }
            } while (!ValidCruiser2);                                                                       // Loops until a valid second cruiser coordinate has been entered.

            /* Cruiser 3 Entry & Validation */
            do
            {
                string enterXCord = "Enter the X coordinate for your third cruiser: ";
                string enterYCord = "Enter the Y coordinate for your third cruiser: ";
                
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n");
                centerStringWrite(enterXCord);                                                                // Asks for x coordinate of third cruiser
                Console.ForegroundColor = ConsoleColor.Green;
                Cruiser3X = int.Parse(Console.ReadLine());
                
                Console.ForegroundColor = ConsoleColor.Yellow;
                centerStringWrite(enterYCord);                                                                // Asks for y coordinate of third cruiser
                Console.ForegroundColor = ConsoleColor.Green;
                Cruiser3Y = int.Parse(Console.ReadLine());
                Console.ResetColor();

                if ((Cruiser3X >= 0 && Cruiser3X < 10) && (Cruiser3Y >= 0 && Cruiser3Y < 10))               // Checks the x & y coordinates are between 0-9
                {
                                                                                                            // Checks there's not already a boat there  
                    if (((Cruiser3X == BattleshipX) && (Cruiser3Y == BattleshipY)) || ((Cruiser3X == Battleship2X) && (Cruiser3Y == Battleship2Y)) || ((Cruiser3X == CruiserX) && (Cruiser3Y == CruiserY)) || ((Cruiser3X == Cruiser2X) && (Cruiser3Y == Cruiser2Y)))
                    {
                        error = "Please enter values where you haven't already place a boat";
                        Console.ForegroundColor = ConsoleColor.Red;
                        centerStringWrite(error); 
                        Console.ResetColor();
                        ValidCruiser3 = false;
                    }
                    else 
                    {
                        string placed = "That's the last cruiser at " + Cruiser3X + ", " + Cruiser3Y;
                        seaPlayerArray[Cruiser3X, Cruiser3Y] = SHIP_CRUISER;                                // Valid thirdcruiser is placed
                        Console.ForegroundColor = ConsoleColor.Green;
                        centerStringWrite(placed);
                        Console.ResetColor();
                        ValidCruiser3 = true;
                    }
                }
                else                                                                                        // If not between 0 & 9, it re-asks entry.
                {
                    error = "Please enter values between the range 0-9";
                    Console.ForegroundColor = ConsoleColor.Red;
                    centerStringWrite(error); 
                    Console.ResetColor();
                    ValidCruiser3 = false;
                }
            } while (!ValidCruiser3);                                                                       // Loops until a valid second cruiser coordinate has been entered.

            /* Submarine Entry & Validation */
            do
            {
                string enterXCord = "Enter the X coordinate for your submarine: ";
                string enterYCord = "Enter the Y coordinate for your submarine: ";
                
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n");
                centerStringWrite(enterXCord);                                                                // Asks for x coordinate of submarine
                Console.ForegroundColor = ConsoleColor.Green;
                SubmarineX = int.Parse(Console.ReadLine());
                
                Console.ForegroundColor = ConsoleColor.Yellow;
                centerStringWrite(enterYCord);                                                                // Asks for y coordinate of submarine
                Console.ForegroundColor = ConsoleColor.Green;
                SubmarineY = int.Parse(Console.ReadLine());
                Console.ResetColor();

                if ((SubmarineX >= 0 && SubmarineX < 10) && (SubmarineY >= 0 && SubmarineY < 10))           // Checks the x & y coordinates are between 0-9
                {
                    
                    if (((SubmarineX == BattleshipX) && (SubmarineY == BattleshipY)) || ((SubmarineX == Battleship2X) && (SubmarineY == Battleship2Y)) || ((SubmarineX == CruiserX) && (SubmarineY == CruiserY)) || ((SubmarineX == Cruiser2X) && (SubmarineY == Cruiser2Y)) || ((SubmarineX == Cruiser3X) && (SubmarineY == Cruiser3Y)))
                    {
                        error = "Please enter values where you haven't already place a boat.";
                        Console.ForegroundColor = ConsoleColor.Red;
                        centerStringWrite(error); 
                        Console.ResetColor();
                        ValidSubmarine = false;
                    }
                    else 
                    {
                        string placed = "The boys are underwater at " + SubmarineX + ", " + SubmarineY;
                        seaPlayerArray[SubmarineX, SubmarineY] = SHIP_SUBMARINE;                            // Valid submarine is placed
                        Console.ForegroundColor = ConsoleColor.Green;
                        centerStringWrite(placed);
                        Console.ResetColor();
                        ValidSubmarine = true;
                    }
                }
                else                                                                                        // If not between 0 & 9, it re-asks entry.
                {
                    error = "Please enter values between the range 0-9";
                    Console.ForegroundColor = ConsoleColor.Red;
                    centerStringWrite(error); 
                    Console.ResetColor();
                    ValidSubmarine = false;
                }
            } while (!ValidSubmarine);                                                                      // Loops until a valid submarine coordinate has been entered.

            /* Rowingboat Entry & Validation */
            do
            {
                string enterXCord = "Enter the X coordinate for your rowingboat: ";
                string enterYCord = "Enter the Y coordinate for your rowingboat: ";

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n");
                centerStringWrite(enterXCord);                                                                // Asks for X coordinate of rowingboat
                Console.ForegroundColor = ConsoleColor.Green;
                RowingboatX = int.Parse(Console.ReadLine());
                
                Console.ForegroundColor = ConsoleColor.Yellow;
                centerStringWrite(enterYCord);                                                                // Asks for y coordinate of rowingboat
                Console.ForegroundColor = ConsoleColor.Green;
                RowingboatY = int.Parse(Console.ReadLine());
                Console.ResetColor();

                if ((RowingboatX >= 0 && RowingboatX < 10) && (RowingboatY >= 0 && RowingboatY < 10))       // Checks the x & y coordinates are between 0-9
                {
                    
                    if (((RowingboatX == BattleshipX) && (RowingboatY == BattleshipY)) || ((RowingboatX == Battleship2X) && (RowingboatY == Battleship2Y)) || ((RowingboatX == CruiserX) && (RowingboatY == CruiserY)) || ((RowingboatX == Cruiser2X) && (RowingboatY == Cruiser2Y)) || ((RowingboatX == Cruiser3X) && (RowingboatY == Cruiser3Y)) || ((RowingboatX == SubmarineX) && (RowingboatY == SubmarineY)))
                    {
                        error = "Please enter values where you haven't already place a boat";
                        Console.ForegroundColor = ConsoleColor.Red;
                        centerStringWrite(error); 
                        Console.ResetColor();
                        ValidRowingboat = false;
                    }
                    else
                    {
                        string placed = "The rowingboat's been dispated at " + RowingboatX + ", " + RowingboatY;
                        seaPlayerArray[RowingboatX, RowingboatY] = SHIP_ROWINGBOAT;
                        Console.ForegroundColor = ConsoleColor.Green;
                        centerStringWrite(placed);
                        Console.ResetColor();
                        ValidRowingboat = true;
                    }
                }
                else                                                                                        // If not between 0 & 9, it re-asks entry.
                {
                    error = "Please enter values between the range 0-9";
                    Console.ForegroundColor = ConsoleColor.Red;
                    centerStringWrite(error); 
                    Console.ResetColor();
                    ValidRowingboat = false;
                }
            } while (!ValidRowingboat);                                                                     // Loops until a valid rowingboat coordinate has been entered.

            validPlayerShipsPlacement = true;                                                                          // If all values are valid, game continues.
            Console.ForegroundColor = ConsoleColor.Green;
            string allShipsPlaced = "That's all the boats placed. Time to show them what we're made off!"; 
            Console.WriteLine("\n");
            centerStringWrite(allShipsPlaced);                                                             // Prints boat placement complete message
            Console.ResetColor();
        } while (!validPlayerShipsPlacement);                                                                          // Loops until all boats are valid placements.
    }




    //**** Print Boards ****//
    // Prints Test-Mode CPU Board (Un-Masked)
    public static void testPrintCPUBoard()
    {
        Console.WriteLine("");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("==========================");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("            CPU           ");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("==========================");
        Console.ResetColor();

        // Names Axis Loop
        Console.Write(" (x) ");
        for (int column = 0; column < 10; column++)
        {
            Console.Write(column + " ");
        }
        Console.WriteLine();
        Console.Write("(y) ");
        for (int column = 0; column < 10; column++)
        {
            Console.Write("--");
        }
        Console.Write("\n");

        // Print Board Loop
        for (int row = 0; row < 10; row++)
        {
            Console.Write(" " + row + " | ");

            for (int column = 0; column < 10; column++)
            {
                if (seaCPUArray[column, row] == SHIP_BATTLESHIP)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("B ", seaCPUArray[column, row]);
                    Console.ResetColor();
                }
                if (seaCPUArray[column, row] == SHIP_CRUISER)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("C ", seaCPUArray[column, row]);
                    Console.ResetColor();
                }
                if (seaCPUArray[column, row] == SHIP_SUBMARINE)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("S ", seaCPUArray[column, row]);
                    Console.ResetColor();
                }
                if (seaCPUArray[column, row] == SHIP_ROWINGBOAT)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("R ", seaCPUArray[column, row]);
                    Console.ResetColor();
                }
                if (seaCPUArray[column, row] == STATE_ATTACKED)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("X ");
                    Console.ResetColor();
                }
                if (seaCPUArray[column, row] == STATE_MISSED)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("O ");
                    Console.ResetColor();
                }
                if (seaCPUArray[column, row] == STATE_EMPTY)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write("- ");
                    Console.ResetColor();
                }
            }
            Console.Write("\n");
        }
    }

    // Prints Player Board (Un-Masked)
    public static void printPlayerBoard()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("==============================");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("            Player            ");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("==============================");
        Console.ResetColor();

        // Names Axis Loop
        Console.Write(" (x) ");
        for (int column = 0; column < 10; column++)
        {
            Console.Write(column + " ");
        }
        Console.WriteLine();
        Console.Write("(y) ");
        for (int column = 0; column < 10; column++)
        {
            Console.Write("--");
        }
        Console.Write("\n");

        // Print Board Loop
        for (int row = 0; row < 10; row++)
        {
            Console.Write(" " + row + " | ");

            for (int column = 0; column < 10; column++)
            {

                if (seaPlayerArray[column, row] == SHIP_BATTLESHIP)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("B ");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                if (seaPlayerArray[column, row] == SHIP_CRUISER)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("C ");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                if (seaPlayerArray[column, row] == SHIP_SUBMARINE)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("S ");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                if (seaPlayerArray[column, row] == SHIP_ROWINGBOAT)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("R ");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                if (seaPlayerArray[column, row] == STATE_ATTACKED)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("X ");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                if (seaPlayerArray[column, row] == STATE_MISSED)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("O ");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                if (seaPlayerArray[column, row] == STATE_EMPTY)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write("- ");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
            }
            Console.Write("\n");
        }
    }

    // Prints CPU Board (Masked)
    public static void printCPUBoard()
    {
        Console.WriteLine("");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("==============================");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("              CPU             ");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("==============================");
        Console.ResetColor();

        // Names Axis Loop
        Console.Write(" (x) ");
        for (int column = 0; column < 10; column++)
        {
            Console.Write(column + " ");
        }
        Console.WriteLine();
        Console.Write("(y) ");
        for (int column = 0; column < 10; column++)
        {
            Console.Write("--");
        }
        Console.Write("\n");

        // Print Board Loop
        for (int row = 0; row < 10; row++)
        {
            Console.Write(" " + row + " | ");

            for (int column = 0; column < 10; column++)
            {

                if ((seaCPUArray[column, row] == SHIP_BATTLESHIP) || (seaCPUArray[column, row] == SHIP_CRUISER) || (seaCPUArray[column, row] == SHIP_SUBMARINE) || (seaCPUArray[column, row] == SHIP_ROWINGBOAT))
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write("- ");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                else if (seaCPUArray[column, row] == STATE_ATTACKED)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("X ");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                else if (seaCPUArray[column, row] == STATE_MISSED)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("O ");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                else if (seaCPUArray[column, row] == STATE_EMPTY)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write("- ");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("There's an error in my code in printCPUBoard.");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
            }
            Console.Write("\n");
        }
    }




    //**** Ship Shooting ****//
    // Asks for coordinates and validates selection
    public static void shootInput()
    {
        do
        {
            Console.Write("\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Please select a grid you'd like to shoot (x,y): ");
            Console.ForegroundColor = ConsoleColor.Green;
            playerInput = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.Gray;
            playerInput = playerInput.Trim(); // Removies any spaces on either end

            if (playerInput.Contains(",")) // Input must include a ','
            {
                int firstCommaPos = playerInput.IndexOf(",");
                int lastCommaPos = playerInput.LastIndexOf(",");

                if (firstCommaPos == lastCommaPos) // There must only be one ','
                {
                    if (firstCommaPos != 0 && firstCommaPos != playerInput.Length - 1) // Checks comma isn't first character
                    {
                        string xCoordString = playerInput.Substring(0, firstCommaPos);
                        xCoordString = xCoordString.Trim(); // Removes any spaces on either end
                        xCoord = int.Parse(xCoordString);

                        string yCoordString = playerInput.Substring(firstCommaPos + 1);
                        yCoordString = yCoordString.Trim(); // Removes any spaces on either end
                        yCoord = int.Parse(yCoordString);

                        if ((xCoord >= 0) && (xCoord < 10))
                        {
                            if ((yCoord >= 0) && (yCoord < 10))
                            {
                                ValidInput = true; // Set to "true" so loop can exit
                            }
                            else
                            {
                                error = "Your Y value is too large";
                                ValidInput = false;
                            }
                        }
                        else
                        {
                            if ((yCoord >= 0) && (yCoord < 10))
                            {
                                error = "Your X value is too large";
                                ValidInput = false;
                            }
                            else
                            {
                                error = "Your values are too large";
                            }
                        }
                    }
                    else
                    {
                        error = "Input should include both an x & y value in the format (x,y)";
                        ValidInput = false;
                    }
                }
                else
                {
                    error = "Input should only have one comma";
                    ValidInput = false;
                }
            }
            else
            {
                error = "Input should contain a ',' in between x & y values e.g. (1,1)";
                ValidInput = false;
            }
            if (!ValidInput)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(error);
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        } while (!ValidInput);
    }

    // Search sea using input coordinates & reports on action (e.g. miss/hit)
    public static void boatSearch(int x, int y)
    {
        string Message1 = "";
        
        do
        {
            playerMovesCounter++;                                                   // Increases player moves counter by 1
            
            if (seaCPUArray[x, y] == STATE_ATTACKED)                
            {   // If the square has already been attacked, it's a wasted shot
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(lineLong);
                Message1 = "'You've already shot there dummy. What a waste of a shot!'";
                centerString(Message1);
                Console.WriteLine(lineLong);
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            if (seaCPUArray[x, y] == STATE_MISSED)
            {   // If the square has already been attacked, it's a wasted shot
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(lineLong);
                Message1 = "'Ha ha ha! You wasted your shot!'";
                centerString(Message1);
                Console.WriteLine(lineLong);
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            if (seaCPUArray[x, y] != STATE_ATTACKED)
            {
                if (seaCPUArray[x, y] == STATE_EMPTY)
                {   // If the square is empty then the player has missed
                    seaCPUArray[x, y] = STATE_MISSED;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(lineLong);
                    Message1 = "'Ha ha! Missed me!'";
                    centerString(Message1);
                    Console.WriteLine(lineLong);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    ValidAttack = true;
                }
                if (seaCPUArray[x, y] != STATE_EMPTY)
                {   // If not already attacked or empty sea, there must be a boat
                    if (seaCPUArray[x, y] != STATE_MISSED)
                    {
                        if (seaCPUArray[x, y] == SHIP_BATTLESHIP)
                        {
                            seaCPUArray[x, y] = STATE_ATTACKED;
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine(lineLong);
                            Message1 = "'You sank my battleship.'";
                            centerString(Message1);
                            Console.WriteLine(lineLong);
                            Console.ForegroundColor = ConsoleColor.Gray;
                        }
                        if (seaCPUArray[x, y] == SHIP_CRUISER)
                        {
                            seaCPUArray[x, y] = STATE_ATTACKED;
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine(lineLong);
                            Message1 = "'Oh no! my cruiser.'";
                            centerString(Message1);
                            Console.WriteLine(lineLong);
                            Console.ForegroundColor = ConsoleColor.Gray;
                        }
                        if (seaCPUArray[x, y] == SHIP_ROWINGBOAT)
                        {
                            seaCPUArray[x, y] = STATE_ATTACKED;
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine(lineLong);
                            Message1 = "'Man overboard!' 'Hey, that was a family heirloom rowing boat.'";
                            centerString(Message1);
                            Console.WriteLine(lineLong);
                            Console.ForegroundColor = ConsoleColor.Gray;
                        }
                        if (seaCPUArray[x, y] == SHIP_SUBMARINE)
                        {
                            seaCPUArray[x, y] = STATE_ATTACKED;
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine(lineLong);
                            Message1 = "Blup blup blup! 'My Submarine!'";
                            centerString(Message1);
                            Console.WriteLine(lineLong);
                            Console.ForegroundColor = ConsoleColor.Gray;
                        }

                        ValidAttack = true;                                          // Exits loop
                        playerBoatHitCount++;                                        // Increases boat hit count
                         
                        if ((CPUBoatHitCount == 7) || (playerBoatHitCount == 7))     // Checks to see if all boats (7) have been sank
                        {
                            GameOver = true;
                        }
                    }
                }
            }
        } while (!ValidAttack);                                                     // Continues reading inputs until attack is valid

        if ((CPUBoatHitCount == 7) || (playerBoatHitCount == 7))                    // Checks to see if all boats (7) have been sank
        {
            GameOver = true;
        }
    }

    // Generates Random CPU shots at Player Board
    public static void RandCPUShots()
    {
        bool validCPUPlacement = false;
        int AttackX;
        int AttackY;

        do
        {
            Random randAttack = new Random();                       // Creates a random number generator
            AttackX = randAttack.Next(0, 10);                       // Generates integer between 0 - 9 for the x coordinate
            AttackY = randAttack.Next(0, 10);                       // Generates integer between 0 - 9 for the y coordinate

            string attack = "I shall attack : " + AttackX + "," + AttackY;
            string hit = "Haha! I've hit your ship!";
            string missed = "No! I missed!";

            if ((seaPlayerArray[AttackX, AttackY] == SHIP_BATTLESHIP) || (seaPlayerArray[AttackX, AttackY] == SHIP_CRUISER) || (seaPlayerArray[AttackX, AttackY] == SHIP_SUBMARINE) || (seaPlayerArray[AttackX, AttackY] == SHIP_ROWINGBOAT))
            {   // If the random numbers generated produce an array where a boat is placed, the CPU is guaranteed a hit
                Console.WriteLine("");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(lineLong);
                //Console.WriteLine("I shall attack: {0},{1}", AttackX, AttackY);
                centerString(attack);
                Console.WriteLine(lineLong);
                Thread.Sleep(1000);

                seaPlayerArray[AttackX, AttackY] = STATE_ATTACKED;

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(lineLong);
                centerString(hit);
                Console.WriteLine(lineLong + "\n");
                Console.ResetColor();

                CPUMovesCounter++;                                  // Used as a debug tool to check that the CPU doesn't attack the same space twice
                CPUBoatHitCount++;                                  // Used to detect game end
                // Console.WriteLine("Moves: " + CPUMovesCounter);
                // Console.WriteLine("Hit: " + CPUBoatHitCount);

                validCPUPlacement = true;
            }
            else if (seaPlayerArray[AttackX, AttackY] == STATE_EMPTY)
            {   // If it's empty, then the CPU has missed
                Console.WriteLine("");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(lineLong);
                centerString(attack);
                Console.WriteLine(lineLong);
                Thread.Sleep(1000);

                seaPlayerArray[AttackX, AttackY] = STATE_MISSED;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(lineLong);
                centerString(missed);
                Console.WriteLine(lineLong + "\n");
                Console.ResetColor();

                CPUMovesCounter++;
                // Console.WriteLine("Moves: " + CPUMovesCounter);
                // Console.WriteLine("Hit: " + CPUBoatHitCount);

                validCPUPlacement = true;
            }
            else if (seaPlayerArray[AttackX, AttackY] == STATE_MISSED)
            {   // The CPU can't shoot at squares it's already shot at
                validCPUPlacement = false;
            }
            else if (seaPlayerArray[AttackX, AttackY] == STATE_ATTACKED)
            {   // The CPU can't shoot at squares it's already shot at
                validCPUPlacement = false;
            }
        }
        while (!validCPUPlacement);

        /* Game End Detection */
        if ((CPUBoatHitCount == 7) || (playerBoatHitCount == 7))    // Checks to see if all boats (7) have been sank
        {
            GameOver = true;
        }
    }




    //**** Game Over ****//
    // Prints end of game when the player has won
    public static void gameOverWin()
    {
        string playerImage1 = "##############                                       ";
        string playerImage2 = "#           #                                        ";
        string playerImage3 = "##################                                   ";
        string playerImage4 = "##################                                   ";
        string playerImage5 = "#############                                        ";
        string playerImage6 = "#           #                                        ";
        string playerImage7 = "#  #     #  #                                        ";
        string playerImage8 = "#           #    ___________________________________ ";
        string playerImage9 = "#           #   (                                   )";
        string playerImage10 = "#   #####   #  <  Blast! You defeated all my ships! )";
        string playerImage11 = "#           #   (___________________________________)";
        string playerImage12 = "#############                                        ";

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(lineLong + "\n");

        // Prints the 12 strings declared above
        centerString(playerImage1);
        centerString(playerImage2);
        centerString(playerImage3);
        centerString(playerImage4);
        centerString(playerImage5);
        centerString(playerImage6);
        centerString(playerImage7);
        centerString(playerImage8);
        centerString(playerImage9);
        centerString(playerImage10);
        centerString(playerImage11);
        centerString(playerImage12);

        Console.WriteLine("\n" + lineLong);

        string PressAnyKey = "Press any key to continue ... ";
        string Message1 = "You did it in";
        string Message2 = "moves";

        Console.WriteLine("");
        centerString(Message1);
        Console.WriteLine(String.Format("{0," + (Console.WindowWidth / 2) + "}", playerMovesCounter));
        centerString(Message2);
        Console.WriteLine("");
        Console.ForegroundColor = ConsoleColor.Yellow;
        centerString(PressAnyKey); // Requires key press to advance to next stage so that users have ample time to read messages
        Console.ResetColor();
        Console.ReadKey();
    }

    // Prints end of game when the player has lost
    public static void gameOverLoss()
    {
        string playerImage1 = "##############                                           ";
        string playerImage2 = "#           #                                            ";
        string playerImage3 = "##################                                       ";
        string playerImage4 = "##################                                       ";
        string playerImage5 = "#############                                            ";
        string playerImage6 = "#           #                                            ";
        string playerImage7 = "#  #     #  #                                            ";
        string playerImage8 = "#           #    _______________________________________ ";
        string playerImage9 = "#           #   (                                       )";
        string playerImage10 = "#   #####   #  <  Hahaha! You're no match for my fleet! )";
        string playerImage11 = "#           #   (_______________________________________)";
        string playerImage12 = "#############                                            ";

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(lineLong + "\n");

        // Prints the 12 strings declared above
        centerString(playerImage1);
        centerString(playerImage2);
        centerString(playerImage3);
        centerString(playerImage4);
        centerString(playerImage5);
        centerString(playerImage6);
        centerString(playerImage7);
        centerString(playerImage8);
        centerString(playerImage9);
        centerString(playerImage10);
        centerString(playerImage11);
        centerString(playerImage12);

        Console.WriteLine("\n" + lineLong);
        
        
        string PressAnyKey = "Press any key to continue ... ";
        
        Console.WriteLine("");
        Console.ForegroundColor = ConsoleColor.Yellow;
        centerString(PressAnyKey); // Requires key press to advance to next stage so that users have ample time to read messages
        Console.ResetColor();
        Console.ReadKey();
    }

    // Asks the player if they wish to play again
    public static void playAgain()
    {
        string playerImage1 = " ##############                                 ";
        string playerImage2 = " #           #                                  ";
        string playerImage3 = " ##################                             ";
        string playerImage4 = " ##################                             ";
        string playerImage5 = " #############                                  ";
        string playerImage6 = " #           #                                  ";
        string playerImage7 = " #  #     #  #                                  ";
        string playerImage8 = " #           #    ____________________________  ";
        string playerImage9 = " #           #   (                             )";
        string playerImage10 = "#   #####   #  <  Do you want to play again? )";
        string playerImage11 = "#           #   (____________________________)";
        string playerImage12 = "#############                                 ";

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(lineLong + "\n");

        // Prints the 12 strings declared above
        centerString(playerImage1);
        centerString(playerImage2);
        centerString(playerImage3);
        centerString(playerImage4);
        centerString(playerImage5);
        centerString(playerImage6);
        centerString(playerImage7);
        centerString(playerImage8);
        centerString(playerImage9);
        centerString(playerImage10);
        centerString(playerImage11);
        centerString(playerImage12);

        Console.WriteLine("\n" + lineLong);

        string Message1 = "Y/N: ";
        
        Console.ForegroundColor = ConsoleColor.Yellow;
        centerStringWrite(Message1);
        
        Console.ForegroundColor = ConsoleColor.Green;
        string playAgain = Console.ReadLine();
        Console.ResetColor();
        playAgain = playAgain.Trim();

        if (playAgain == "y" || playAgain == "Y")
        {
            Console.Clear();
            emptyCPUSea();
            emptyPlayerSea();
            Main();
        }
        else
        {
            string Message2 = "Thanks for playing!";
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("");
            centerString(Message2);
            Console.ResetColor();
            Thread.Sleep(2500);
            Console.Clear();
        }
    }

    // Resets player scores so player can have multiple games
    public static void resetScores()
    {
        GameOver = false;
        CPUBoatHitCount = 0;
        CPUMovesCounter = 0;
        playerBoatHitCount = 0;
        playerMovesCounter = 0;
    }
    
    // Resets integers so that you can play multiple games
    public static void resetIntegers()
    {
        xCoord = 0;
        yCoord = 0;
    }




    //**** Game Modes ****//
    // Runs test mode
    public static void gameModeTM()
    {
        printTitle();                               // Clears console & prints the title
        testMode();                                 // Prints test mode instructions
        emptyCPUSea();                              // Empties the CPU sea array
        placeCPUShips();                            // Pre-determined CPU ships used
        testPrintCPUBoard();                        // Unmasked CPU board printed

        do                                          // Game loops until 7 boats on either team have been sunk i.e GameOver bool
        {
            do
            {
                try
                {
                    shootInput();                   // User enters the coordinates they wish to shoot
                    printTitle();                   // Console cleared. The title is reprinted
                    boatSearch(xCoord, yCoord);     // The array is then shot at using the coordinates from shootInput()
                    validTestModeAttack = true;     // Bool set to true so do-while loop can exit
                }
                catch                               // If there's a runtime error, catch it
                {
                    printTitle();                   // Cleared. Reprint the title
                    Console.ForegroundColor = ConsoleColor.Red;
                    error = " You entered " + playerInput + ". These are invalid coordinates. Please try again.";
                    Console.WriteLine(lineLong);
                    centerString(error);            // Prints error message inbetween linebreaks
                    Console.WriteLine(lineLong);
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
            }
            while (!validTestModeAttack);

            testPrintCPUBoard();                    // If the attack is valid, the unmasked CPU sea is printed
            resetIntegers();                        // Avoids any previous numbers been carried over

        } while (!GameOver);
    }
    
    // Runs single player mode
    public static void gameModeSP()
    {
        Console.Clear();
        printTitle();
        singleMode();                               // Prints single player mode instructions
        Console.Clear();
        printTitle();
        emptyCPUSea();
        //placeCPUShips();
        placeRandomCPUShips();                      // Random CPU ships used        
        printCPUBoard();                            // Masked CPU board printed

        do                                          // Game loops until 7 boats on either team have been sunk
        {
            do
            {
                try
                {
                    shootInput();
                    Console.Clear();
                    printTitle();
                    boatSearch(xCoord, yCoord);
                    validSinglePlayerAttack = true;
                }
                catch
                {
                    Console.Clear();
                    printTitle();
                    Console.ForegroundColor = ConsoleColor.Red;
                    error = " You entered " + playerInput + ". These are invalid coordinates. Please try again.";
                    Console.WriteLine(lineLong);
                    centerString(error);
                    Console.WriteLine(lineLong);
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
            }
            while (!validSinglePlayerAttack);

            printCPUBoard();
            resetIntegers();                        // Avoids any previous numbers been carried over

        } while (!GameOver);
    }
    
    // Runs multiplayer mode
    public static void gameModeMP()
    {
        Console.Clear();
        printTitle();
        multiMode();                                // Prints multiplayer mode instructions
        Console.Clear();
        printTitle();
        emptyPlayerSea();
        emptyCPUSea();
        shipInstructions();                         // Prints ship placement instructions

        do                                          // Asks for ship placement until all valid ships have been placed
        {
            try
            {
                askPlayerShips();
                validMultiplayerShipPlacement = true;
            }
            catch (FormatException)
            {
                Console.Clear();
                Console.ResetColor();
                printTitle();
                error = "You seem to have entered an invalid input (such as text or blank spaces). Please start again.";
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(lineLong);
                centerString(error);
                Console.WriteLine(lineLong);
                Console.ForegroundColor = ConsoleColor.Gray;
                emptyPlayerSea();
            }
        }
        while (!validMultiplayerShipPlacement);
            
        Thread.Sleep(2500);
        //placeCPUShips();
        placeRandomCPUShips();                      // Random CPU ships used
        Console.Clear();
        printTitle();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(lineLong);
        string gameReady = "The ships are placed. Lets play some battleships!";
        centerString(gameReady);
        Console.WriteLine(lineLong);
        Console.ResetColor();
        printCPUBoard();
        Console.WriteLine("");
        printPlayerBoard();
        
        do                                          // Game loops until 7 boats on either team have been sunk
        {
            do
            {
                try
                {
                    shootInput();
                    Console.Clear();
                    printTitle();
                    boatSearch(xCoord, yCoord);
                    validMultiplayerAttack = true;
                }
                catch
                {
                    Console.Clear();
                    printTitle();
                    Console.ForegroundColor = ConsoleColor.Red;
                    error = " You entered " + playerInput + ". These are invalid coordinates. Please try again.";
                    Console.WriteLine(lineLong);
                    centerString(error);
                    Console.WriteLine(lineLong);
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
            }
            while (!validMultiplayerAttack);

            printCPUBoard();
            Thread.Sleep(1000);
            RandCPUShots();
            printPlayerBoard();
            resetIntegers();                        // Avoids any previous numbers been carried over

        } while (!GameOver);                        // Loops until all boats on either side have been sunk
    }




    //**** Battleships Game ****//
    // Main method
    static void Main()
    {
        Console.SetWindowSize(160, 80);                 // Sets console to full-screen
        Console.Title = "Battleships by Joshua Ong";    // Sets console title

        printTitle();                                   // Prints Battleships title
        startScreen();                                  // Prints main menu & game mode is selected here

        //* Test Mode *//
        if (gameMode == 1)
        {
            gameModeTM();                               // Runs test mode
        }

        //* Single Player *//
        if (gameMode == 2)
        {
            gameModeSP();                               // Runs single player mode

        }

        /* Multiplayer */
        if (gameMode == 3)
        {
            gameModeMP();                               // Runs multiplayer mode
        }

        /* Game End Detecticion */
        if (CPUBoatHitCount == 7)                       // CPU wins if they hit 7 boats first
        {
            printTitle();                               // Cleared. Reprint the title
            gameOverLoss();                             // Announces the CPU as the winner
        }
        else if (playerBoatHitCount == 7)               // Player wins if they hit 7 boats first
        {
            printTitle();                               // Cleared. Reprint the title
            gameOverWin();                              // Announces the player as the winner
        }

        resetScores();                                  // Resets the scores (boats hit etc) so multiple games can be played
        printTitle();                                   // Title is printed
        playAgain();                                    // Option to play again printed
    }
}