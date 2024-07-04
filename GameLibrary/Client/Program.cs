using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using GamesLibrary;
using GameContext;
using System.Collections;

namespace CodeFirstSample
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using (GameContextDB context = new GameContextDB())
                {
                    List<Game> gameList = context.Games.ToList();

                    if (gameList == null || gameList.Count == 0)
                    {
                        Genre genreA = new Genre { Title = "Adventure" };
                        Genre genreB = new Genre { Title = "Strategy" };
                        Genre genreC = new Genre { Title = "Simulation" };
                        context.Genres.Add(genreA);
                        context.Genres.Add(genreB);
                        context.Genres.Add(genreC);

                        Studio studioA = new Studio { Title = "Ubisoft" };
                        Studio studioB = new Studio { Title = "Electronic Arts" };
                        context.Studios.Add(studioA);
                        context.Studios.Add(studioB);

                        Game gameA = new Game { Title = "Assassin's Creed", Studio = studioA, Genres = new List<Genre> { genreA }, ReleaseDate = new DateTime(2007, 11, 13) };
                        Game gameB = new Game { Title = "The Sims", Studio = studioB, Genres = new List<Genre> { genreB, genreC }, ReleaseDate = new DateTime(2000, 2, 4) };
                        Game gameC = new Game { Title = "FIFA 21", Studio = studioB, Genres = new List<Genre> { genreB }, ReleaseDate = new DateTime(2020, 10, 9) };
                        context.Games.Add(gameA);
                        context.Games.Add(gameB);
                        context.Games.Add(gameC);
                    }
                    context.SaveChanges();

                    gameList = context.Games.ToList();
                    Console.WriteLine("-------------------------------------------");
                    foreach (var game in gameList)
                    {
                        Console.WriteLine("Title: " + game.Title);
                        Console.WriteLine("Studio: " + game.Studio.Title);
                        Console.Write("Genre: ");
                        foreach (var genre in game.Genres)
                        {
                            Console.Write(" " + genre.Title);
                        }
                        Console.WriteLine("\nRelease Date: " + game.ReleaseDate);
                        Console.WriteLine();
                    }




                    gameList = context.Games.ToList();

                    if (gameList.Count < 4)
                    {
                        foreach (var game in gameList)
                        {
                            game.Mode = GameMode.MultiPlayer;
                        }
                        Studio studioC = new Studio { Title = "Valve" };
                        context.Add(studioC);

                        Genre genreD = new Genre { Title = "Puzzle" };
                        context.Genres.Add(genreD);

                        Game gameD = new Game { Title = "Portal", Studio = studioC, Genres = new List<Genre> { genreD }, Mode = GameMode.SinglePlayer, ReleaseDate = new DateTime(2007, 10, 10) };
                        context.Games.Add(gameD);
                        context.SaveChanges();
                    }

                    gameList = context.Games.ToList();

                    Console.WriteLine("-------------------------------------------");
                    foreach (var game in gameList)
                    {
                        Console.WriteLine("Title: " + game.Title);
                        Console.WriteLine("Studio: " + game.Studio.Title);
                        Console.Write("Genre: ");
                        foreach (var genre in game.Genres)
                        {
                            Console.Write(" " + genre.Title);
                        }
                        Console.WriteLine("\nRelease Date: " + game.ReleaseDate);
                        Console.WriteLine("Game Mode: " + game.Mode);
                        Console.WriteLine();
                    }





                    gameList = context.Games.ToList();

                    if (gameList.Count < 5)
                    {
                        Random rand = new Random();
                        foreach (var game in gameList)
                        {
                            game.SaledCopies = rand.Next(1000, 10001);
                        }
                        Studio studioD = new Studio { Title = "Epic Games" };
                        context.Add(studioD);

                        Genre genreE = new Genre { Title = "Battle Royale" };
                        context.Genres.Add(genreE);

                        Game gameE = new Game { Title = "Fortnite", Studio = studioD, Genres = new List<Genre> { genreE }, Mode = GameMode.MultiPlayer, ReleaseDate = new DateTime(2017, 7, 25), SaledCopies = 10000 };
                        context.Games.Add(gameE);
                        context.SaveChanges();
                    }

                    gameList = context.Games.ToList();

                    Console.WriteLine("-------------------------------------------");
                    foreach (var game in gameList)
                    {
                        Console.WriteLine("Title: " + game.Title);
                        Console.WriteLine("Studio: " + game.Studio.Title);
                        Console.Write("Genre: ");
                        foreach (var genre in game.Genres)
                        {
                            Console.Write(" " + genre.Title);
                        }
                        Console.WriteLine("\nRelease Date: " + game.ReleaseDate);
                        Console.WriteLine("Game Mode: " + game.Mode);
                        Console.WriteLine("Sales: " + game.SaledCopies);
                        Console.WriteLine();
                    }

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
