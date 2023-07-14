using System;
using System.Collections.Generic;
using UnityEngine;

public class Match : MonoBehaviour
{
    /// <summary>
    ///Check Match
    // function about that returns if any match exists after items that represented by given two indexes are swapped.
    // Definition of "Match": There must be 3 or more same colored items in vertical and/or horizontal order in board.
    /// </summary>
    public static bool MatchExists(GameBoard board, int index1, int index2)
    {
        // Swap the items at the given indexes
        int[] _grid = board.Grid;
        int _height = board.Height;
        int _width = board.Width;
        //
        SwapItem(index1, index2, _grid);

        // Check for horizontal matches
        for (int row = 0; row < _height; row++)
        {
            int count = 1;
            for (int col = 1; col < _width; col++)
            {
                if (_grid[row * _width + col] == _grid[row * _width + col - 1])
                {
                    count++;
                    if (count >= 3)
                    {
                        // Match found
                        SwapItem(index1, index2, _grid);
                        return true;
                    }
                }
                else
                {
                    count = 1;
                }
            }
        }

        // Check for vertical matches
        for (int col = 0; col < _width; col++)
        {
            int count = 1;
            for (int row = 1; row < _height; row++)
            {
                if (_grid[row * _width + col] == _grid[(row - 1) * _width + col])
                {
                    count++;
                    if (count >= 3)
                    {
                        // Match found
                        SwapItem(index1, index2, _grid);
                        return true;
                    }
                }
                else
                {
                    count = 1;
                }
            }
        }

        // No match found
        SwapItem(index1, index2, _grid);
        return false;
    }
    private static void SwapItem(int index1, int index2, int[] _grid)
    {
        int temp = _grid[index1];
        _grid[index1] = _grid[index2];
        _grid[index2] = temp;
    }

    /// <summary>
    ///Find All Possible Matches
    // function about that returns tuple list of two indexes in board that creates at least one match after swap.
    // These tuples should be unique such that if a {1,2} possible match exists, {2,1} must be ignored.
    // Definition of "Possible Match": Items represented by two indexes must create a match after swapped.
    /// </summary>
    public static List<Tuple<int, int>> GetAllPossibleMatches(GameBoard board)
    {
        int width = board.Width;
        int height = board.Height;
        int[] grid = board.Grid;
        List<Tuple<int, int>> matches = new List<Tuple<int, int>>();

        int[] adjacentRows = { -1, 0, 0, 1 };
        int[] adjacentCols = { 0, -1, 1, 0 };

        for (int row = 0; row < height; row++)
        {
            for (int col = 0; col < width; col++)
            {
                int index = GetIndex(row, col, width);

                for (int i = 0; i < adjacentRows.Length; i++)
                {
                    int adjRow = row + adjacentRows[i];
                    int adjCol = col + adjacentCols[i];

                    if (IsValidIndex(adjRow, adjCol, height, width))
                    {
                        int adjIndex = GetIndex(adjRow, adjCol, width);

                        bool matchExists = MatchExists(board, index, adjIndex);

                        var tuple = new Tuple<int, int>(index, adjIndex);

                        if (matchExists && !matches.Contains(tuple))
                        {
                            var uniqueTuple = new Tuple<int, int>(adjIndex, index);
                            matches.Add(uniqueTuple);
                        }
                    }
                }
            }
        }

        return matches;
    }
    private static bool IsValidIndex(int row, int col, int rows, int cols)
    {
        return row >= 0 && row < rows && col >= 0 && col < cols;
    }
    private static int GetIndex(int row, int col, int _height)
    {
        return row * _height + col;
    }

    /// <summary>
    ///Shuffle
    // function about that shuffles positions of items of given board.
    // After the shuffle there must be at least one possible match exists
    // and there shouldn't be any already formed match exists in board.
    /// </summary>
    public static void Shuffle(GameBoard board)
    {
        int width = board.Width;
        int height = board.Height;
        int[] grid = board.Grid;

        // Shuffle the items until configuration is found
        while (true)
        {
            System.Random random = new System.Random();
            List<int[]> swaps = new List<int[]>();

            for (int row = 0; row < width; row++)
            {
                for (int col = 0; col < height; col++)
                {
                    int randomRow = random.Next(width);
                    int randomCol = random.Next(height);

                    // Collect the pair of items to be swapped
                    int[] swap = new int[] { row, col, randomRow, randomCol };
                    swaps.Add(swap);
                }
            }

            // Perform the swaps
            foreach (int[] swap in swaps)
            {
                ShuffleItems(grid, swap);
            }

            // Check if there is at least one possible match
            var possibleMatches = GetAllPossibleMatches(board);
            if (possibleMatches.Count >= 1)
            {
                // Check if there are no pre-existing matches
                if (!IsAnyMatchExistsInBoard(board))
                {
                    //configuration found
                    return;
                }
            }
        }
    }
    private static void ShuffleItems(int[] grid, int[] swap)
    {
        int index1 = swap[0];
        int index2 = swap[1];

        int temp = grid[index1];
        grid[index1] = grid[index2];
        grid[index2] = temp;
    }

    public static bool IsAnyMatchExistsInBoard(GameBoard board)
    {
        int width = board.Width;
        int height = board.Height;
        int[] grid = board.Grid;

        // Check for horizontal and vertical matches
        for (int i = 0; i < grid.Length; i++)
        {
            int col = i % width;
            int row = i / width;

            // Horizontal match
            if (col < width - 2 && grid[i] == grid[i + 1] && grid[i] == grid[i + 2])
                return true;

            // Vertical match
            if (row < height - 2 && grid[i] == grid[i + width] && grid[i] == grid[i + 2 * width])
                return true;
        }

        return false;
    }
}
