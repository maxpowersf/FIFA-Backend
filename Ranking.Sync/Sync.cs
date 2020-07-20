using OfficeOpenXml;
using Ranking.Application.Interfaces;
using Ranking.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Ranking.Sync
{
    public class Sync
    {
        static string pathExcel = @"C:/Publish/Goalscorers.xlsx";

        static List<GoalscorerSync> GoalscorersInExcel;
        static List<Goalscorer> GoalscorersToAdd;

        private readonly ITournamentService _tournamentService;
        private readonly IPlayerService _playerService;
        private readonly IGoalscorerService _goalscorerService;

        public Sync(
            ITournamentService tournamentService,
            IPlayerService playerService,
            IGoalscorerService goalscorerService
            )
        {
            this._tournamentService = tournamentService;
            this._playerService = playerService;
            this._goalscorerService = goalscorerService;
        }

        public void Run()
        {
            GoalscorersInExcel = new List<GoalscorerSync>();
            GoalscorersToAdd = new List<Goalscorer>();

            try
            {
                ConsoleMessage("/********************* Sync Goalscorers *********************/", ConsoleColor.DarkCyan);
                ConsoleMessage("Insert TournamentID: ");

                string strTournament = Console.ReadLine();

                int tournamentId;
                if (!int.TryParse(strTournament, out tournamentId) || tournamentId <= 0)
                {
                    ConsoleMessage("Tournament ID must be a number greater than 0", ConsoleColor.DarkRed, true);
                    Run();
                }

                ConsoleMessage("Retrieving tournament..", ConsoleColor.DarkMagenta);
                Tournament tournament = _tournamentService.Get(tournamentId).Result;
                if (tournament == null)
                {
                    ConsoleMessage("Tournament not found", ConsoleColor.DarkRed, true);
                    Run();
                }

                ConsoleMessage(tournament.Name, ConsoleColor.DarkGreen);

                ConsoleMessage("Reading Excel file..", ConsoleColor.DarkMagenta);
                LoadExcelFile();

                if (GoalscorersInExcel.Count <= 0)
                {
                    ConsoleMessage("Excel file appears to be empty", ConsoleColor.DarkRed, true);
                    Run();
                }

                ConsoleMessage($"{GoalscorersInExcel.Count} goalscorers read", ConsoleColor.DarkGreen);

                CheckIfContinue();

                ConsoleMessage("Processing file data..", ConsoleColor.DarkMagenta);
                ProcessData(tournament);

                if (GoalscorersToAdd.Count <= 0)
                {
                    ConsoleMessage("No players were processed", ConsoleColor.DarkRed, true);
                    Run();
                }

                ConsoleMessage(GoalscorersToAdd.Count + " players processed", ConsoleColor.DarkGreen);

                CheckIfContinue();

                ConsoleMessage("Saving..", ConsoleColor.DarkMagenta);
                try
                {
                    _goalscorerService.Add(GoalscorersToAdd).Wait();
                    ConsoleMessage("Finished syncing goalscorers successfully", ConsoleColor.DarkGreen);
                }
                catch(Exception ex)
                {
                    ConsoleMessage($"Error saving data: {ex.Message}", ConsoleColor.DarkRed);
                }

                ConsoleMessage("Press Enter to restart..", addNewLine: true);
                if (Console.ReadKey().Key == ConsoleKey.Enter)
                {
                    Run();
                }
            }
            catch(Exception ex)
            {
                ConsoleMessage($"Error: {ex.Message}", ConsoleColor.DarkRed);
            }
        }

        private void LoadExcelFile()
        {
            try
            {
                using (ExcelPackage package = new ExcelPackage(new FileInfo(pathExcel)))
                {
                    ExcelWorksheet workSheet = package.Workbook.Worksheets[0];

                    int rw = workSheet.Dimension.Rows;
                    for (int rCnt = 2; rCnt <= rw; rCnt++)
                    {
                        try
                        {
                            if (workSheet.Cells[rCnt, 1].Value != null)
                            {
                                string player = (string)workSheet.Cells[rCnt, 1].Value;
                                string team = (string)workSheet.Cells[rCnt, 2].Value;
                                double goals = (double)workSheet.Cells[rCnt, 3].Value;
                                bool goldenBoot = Convert.ToBoolean(workSheet.Cells[rCnt, 4].Value);

                                GoalscorerSync goalscorer = new GoalscorerSync(player, team, Convert.ToInt32(goals), goldenBoot);

                                GoalscorersInExcel.Add(goalscorer);
                            }
                        }
                        catch (Exception ex)
                        {
                            ConsoleMessage($"Row: {rCnt} - Error reading from file. {ex.Message}", ConsoleColor.DarkRed);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ConsoleMessage($"There was a problem reading the file. {ex.Message}", ConsoleColor.DarkRed);
            }
        }

        private void ProcessData(Tournament tournament)
        {
            foreach(GoalscorerSync goalscorer in GoalscorersInExcel)
            {
                try
                {
                    Player player = _playerService.Get(goalscorer.Player, goalscorer.Team).Result;
                    if (player == null)
                    {
                        ConsoleMessage($"Player: {goalscorer.Player} - Player not found", ConsoleColor.DarkRed);
                        continue;
                    }

                    Goalscorer newGoalscorer = new Goalscorer
                    {
                        TournamentID = tournament.Id,
                        PlayerID = player.Id,
                        Goals = goalscorer.Goals,
                        GoldenBoot = goalscorer.GoldenBoot
                    };

                    GoalscorersToAdd.Add(newGoalscorer);
                }
                catch (Exception ex)
                {
                    ConsoleMessage($"Player: {goalscorer.Player} - Error processing data: {ex.Message}", ConsoleColor.DarkRed);
                }
            }
        }

        public void ConsoleMessage(string msg, ConsoleColor? color = null, bool addNewLine = false)
        {
            if (color.HasValue)
            {
                Console.ForegroundColor = color.Value;
            }

            Console.WriteLine(msg);
            Console.ResetColor();

            if(addNewLine)
            {
                Console.WriteLine();
            }
        }

        public void CheckIfContinue()
        {
            ConsoleMessage("Would you like to continue? Press Enter to continue..");
            if (Console.ReadKey().Key != ConsoleKey.Enter)
            {
                Run();
            }
        }
    }
}
