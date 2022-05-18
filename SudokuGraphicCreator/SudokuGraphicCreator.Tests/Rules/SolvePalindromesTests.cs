﻿using NUnit.Framework;
using SudokuGraphicCreator.Model;
using SudokuGraphicCreator.Rules;
using SudokuGraphicCreator.Stores;
using System;
using System.Collections.ObjectModel;
using System.Threading;

namespace SudokuGraphicCreator.Tests.Rules
{
    public class SolvePalindromesTests
    {
        [Test]
        public void SolvePalindromes_OneSolution()
        {
            string inputString = "100000000000200810005103002020600003000800740000700005000502006000900170800000000";
            int[,] givenNumber = SudokuRulesUtilities.CreateArrayFromInputString(inputString, 9, 9);
            Sudoku sudoku = new Sudoku(9, 3, 3);
            sudoku.GivenNumbers = givenNumber;
            sudoku.Variants.Add(SudokuType.Classic);
            sudoku.Variants.Add(SudokuType.Palindrome);
            SudokuStore.Instance.Sudoku = sudoku;

            ObservableCollection<Tuple<int, int>> positions = new ObservableCollection<Tuple<int, int>>();
            positions.Add(new Tuple<int, int>(0, 2));
            positions.Add(new Tuple<int, int>(0, 3));
            positions.Add(new Tuple<int, int>(1, 2));
            positions.Add(new Tuple<int, int>(1, 3));
            SudokuStore.Instance.Sudoku.SudokuVariants.Add(new Line(SudokuElementType.Palindromes, positions));

            int countSolution = 0;
            int[,] solution = new int[9, 9];
            using var ctSource = new CancellationTokenSource();
            SolveSudoku.Solve(givenNumber, 9, 0, 0, ref countSolution, solution, ctSource.Token);
            Assert.That(countSolution == 1, "count was " + countSolution);
            string solutionString = "162498537374256819985173462527641983639825741418739625741582396253964178896317254";
            Assert.That(SudokuRulesUtilities.CreateArrayFromInputString(solutionString, 9, 9), Is.EqualTo(solution));
        }

        [Test]
        public void SolvePalindromes_ZeroSolution()
        {
            string inputString = "100000000000200810005103002020600003000800740000700005000502006000900170800000000";
            int[,] givenNumber = SudokuRulesUtilities.CreateArrayFromInputString(inputString, 9, 9);
            Sudoku sudoku = new Sudoku(9, 3, 3);
            sudoku.GivenNumbers = givenNumber;
            sudoku.Variants.Add(SudokuType.Classic);
            sudoku.Variants.Add(SudokuType.Palindrome);
            SudokuStore.Instance.Sudoku = sudoku;

            ObservableCollection<Tuple<int, int>> positions = new ObservableCollection<Tuple<int, int>>();
            positions.Add(new Tuple<int, int>(0, 2));
            positions.Add(new Tuple<int, int>(0, 3));
            positions.Add(new Tuple<int, int>(1, 2));
            positions.Add(new Tuple<int, int>(1, 3));
            positions.Add(new Tuple<int, int>(1, 4));
            SudokuStore.Instance.Sudoku.SudokuVariants.Add(new Line(SudokuElementType.Palindromes, positions));

            int countSolution = 0;
            int[,] solution = new int[9, 9];
            using var ctSource = new CancellationTokenSource();
            SolveSudoku.Solve(givenNumber, 9, 0, 0, ref countSolution, solution, ctSource.Token);
            Assert.That(countSolution == 0, "count was " + countSolution);
        }
    }
}
