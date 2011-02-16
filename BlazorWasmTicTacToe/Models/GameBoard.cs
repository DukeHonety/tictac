using BlazorWasmTicTacToe.Enums;
using System;
using System.Collections.Generic;

namespace BlazorWasmTicTacToe.Models
{
    public class GameBoard
    {
        public GamePiece[,] Board { get; set; }

        public PieceStyle CurrentTurn = PieceStyle.X;

        public bool GameComplete => GetWinner() != null || IsADraw();

        int[] iData = new int[9];

        //-1 = X, 1 = O, 0 = Stop
        PieceStyle iTurn = PieceStyle.O; 
        PieceStyle iOpponent = PieceStyle.X;

        int iLastMove = -1;

        public GameBoard()
        {
            Reset();
        }

        public void Reset()
        {
            Board = new GamePiece[3, 3];

            //Populate the Board with blank pieces
            for (int i = 0; i <= 2; i++)
            {
                for (int j = 0; j <= 2; j++)
                {
                    Board[i, j] = new GamePiece();
                }
            }
            iData = new int[9];
            iLastMove = -1;
        }

        //Given the coordinates of the space that was clicked...
        public void PieceClicked(int x, int y)
        {
            //If the game is complete, do nothing
            if (GameComplete) { return; }

            //If the space is not already claimed...
            if (CurrentTurn == PieceStyle.X)
            {
                GamePiece clickedSpace = Board[x, y];
                if (clickedSpace.Style == PieceStyle.Blank)
                {
                    //Set the marker to the current turn marker (X or O), then make it the other player's turn
                    clickedSpace.Style = CurrentTurn;
                    nextTurn();
                    
                }
            }
        }
        public void AI()
        {
            int ii = 0;
            int jj = 0;
            if (GameComplete) { return; }

            if (CurrentTurn == PieceStyle.O)
            {
                /*
                    00 10 20
                    01 11 21
                    02 12 22
                    */

                switch (RunImpossibleBot())
                {
                    case 0:
                        ii = 0;
                        jj = 0;
                        break;

                    case 1:
                        ii = 1;
                        jj = 0;
                        break;

                    case 2:
                        ii = 2;
                        jj = 0;
                        break;

                    case 3:
                        ii = 0;
                        jj = 1;
                        break;

                    case 4:
                        ii = 1;
                        jj = 1;
                        break;

                    case 5:
                        ii = 2;
                        jj = 1;
                        break;

                    case 6:
                        ii = 0;
                        jj = 2;
                        break;

                    case 7:
                        ii = 1;
                        jj = 2;
                        break;

                    case 8:
                        ii = 2;
                        jj = 2;
                        break;

                    default:
                        break;
                }

                GamePiece clickedSpaceAI = Board[ii, jj];
                                        
                if (clickedSpaceAI.Style == PieceStyle.Blank)
                {
                    clickedSpaceAI.Style = CurrentTurn;
                    nextTurn();
                }
            }
        }

        public int RunImpossibleBot()
        {
            if (iLastMove == -1)
            {
                if (Board[0, 0].Style == iOpponent)
                {
                    iLastMove = 8;
                }
                else if (Board[2, 0].Style == iOpponent)
                {
                    iLastMove = 6;
                }
                else if (Board[0, 2].Style == iOpponent)
                {
                    iLastMove = 2;
                }
                else if (Board[2, 2].Style == iOpponent)
                {
                    iLastMove = 0;
                }
                else if (Board[0, 0].Style == PieceStyle.Blank)
                {
                    iLastMove = 0;
                }
                else
                {
                    iLastMove = 4;
                }
            }
            else if (iData[iLastMove] != 0)
            {
                CheckWinAndDontLose(iOpponent);
                CheckWinAndDontLose(iTurn);

                if (iData[iLastMove] != 0)
                {
                    if (iLastMove == 4)
                    {
                        if (Board[0, 0].Style == PieceStyle.Blank)
                        {
                            iLastMove = 0;
                        }
                        else if (Board[2, 0].Style == PieceStyle.Blank)
                        {
                            iLastMove = 2;
                        }
                        else if (Board[0, 2].Style == PieceStyle.Blank)
                        {
                            iLastMove = 6;
                        }
                        else if (Board[2, 2].Style == PieceStyle.Blank)
                        {
                            iLastMove = 8;
                        }
                        else iLastMove = RandomPosition();
                    }
                    else if (iLastMove == 0 || iLastMove == 2 || iLastMove == 6 || iLastMove == 8)
                    {
                        if (Board[1, 1].Style == PieceStyle.Blank)
                        {
                            iLastMove = 4;
                        }
                        else if (Board[0, 0].Style == PieceStyle.Blank && Board[2, 2].Style == iOpponent)
                        {
                            iLastMove = 0;
                        }
                        else if (Board[2, 0].Style == PieceStyle.Blank && Board[0, 2].Style == iOpponent)
                        {
                            iLastMove = 2;
                        }
                        else if (Board[0, 2].Style == PieceStyle.Blank && Board[2, 0].Style == iOpponent)
                        {
                            iLastMove = 6;
                        }
                        else if (Board[2, 2].Style == PieceStyle.Blank && Board[0, 0].Style == iOpponent)
                        {
                            iLastMove = 8;
                        }
                        else if ((Board[0, 0].Style == iOpponent && Board[2, 2].Style == iOpponent) || (Board[2, 0].Style == iOpponent && Board[0, 2].Style == iOpponent))
                        {
                            if (Board[1, 0].Style == PieceStyle.Blank)
                            {
                                iLastMove = 1;
                            }
                            else if (Board[0, 1].Style == PieceStyle.Blank)
                            {
                                iLastMove = 3;
                            }
                            else if (Board[2, 1].Style == PieceStyle.Blank)
                            {
                                iLastMove = 5;
                            }
                            else if (Board[1, 2].Style == PieceStyle.Blank)
                            {
                                iLastMove = 7;
                            }
                        }
                        else if (Board[0, 0].Style == PieceStyle.Blank)
                        {
                            iLastMove = 0;
                        }
                        else if (Board[2, 0].Style == PieceStyle.Blank)
                        {
                            iLastMove = 2;
                        }
                        else if (Board[0, 2].Style == PieceStyle.Blank)
                        {
                            iLastMove = 6;
                        }
                        else if (Board[2, 2].Style == PieceStyle.Blank)
                        {
                            iLastMove = 8;
                        }
                        else iLastMove = RandomPosition();
                    }
                    else if (iLastMove == 1 || iLastMove == 3 || iLastMove == 5 || iLastMove == 7)
                    {
                        if (Board[1, 1].Style == PieceStyle.Blank)
                        {
                            iLastMove = 4;
                        }
                        else if (Board[0, 0].Style == PieceStyle.Blank && Board[2, 0].Style == iOpponent && Board[0, 1].Style == iOpponent)
                        {
                            iLastMove = 0;
                        }
                        else if (Board[2, 0].Style == PieceStyle.Blank && Board[1, 0].Style == iOpponent && Board[2, 1].Style == iOpponent)
                        {
                            iLastMove = 2;
                        }
                        else if (Board[0, 2].Style == PieceStyle.Blank && Board[0, 1].Style == iOpponent && Board[1, 2].Style == iOpponent)
                        {
                            iLastMove = 6;
                        }
                        else if (Board[2, 2].Style == PieceStyle.Blank && Board[2, 1].Style == iOpponent && Board[1, 2].Style == iOpponent)
                        {
                            iLastMove = 8;
                        }
                        else if (Board[0, 0].Style == PieceStyle.Blank && ((Board[1, 0].Style == iOpponent) || (Board[0, 1].Style == iOpponent)))
                        {
                            iLastMove = 0;
                        }
                        else if (Board[2, 0].Style == PieceStyle.Blank && ((Board[1, 0].Style == iOpponent) || (Board[2, 1].Style == iOpponent)))
                        {
                            iLastMove = 2;
                        }
                        else if (Board[0, 2].Style == PieceStyle.Blank && ((Board[0, 1].Style == iOpponent) || (Board[1, 2].Style == iOpponent)))
                        {
                            iLastMove = 6;
                        }
                        else if (Board[2, 2].Style == PieceStyle.Blank && ((Board[2, 1].Style == iOpponent) || (Board[1, 2].Style == iOpponent)))
                        {
                            iLastMove = 8;
                        }
                        else if (Board[0, 0].Style == PieceStyle.Blank)
                        {
                            iLastMove = 0;
                        }
                        else if (Board[2, 0].Style == PieceStyle.Blank)
                        {
                            iLastMove = 2;
                        }
                        else if (Board[0, 2].Style == PieceStyle.Blank)
                        {
                            iLastMove = 6;
                        }
                        else if (Board[2, 2].Style == PieceStyle.Blank)
                        {
                            iLastMove = 8;
                        }
                        else if (Board[1, 0].Style == PieceStyle.Blank)
                        {
                            iLastMove = 1;
                        }
                        else if (Board[0, 1].Style == PieceStyle.Blank)
                        {
                            iLastMove = 3;
                        }
                        else if (Board[2, 1].Style == PieceStyle.Blank)
                        {
                            iLastMove = 5;
                        }
                        else if (Board[1, 2].Style == PieceStyle.Blank)
                        {
                            iLastMove = 7;
                        }
                        else iLastMove = RandomPosition();
                    }
                }

            }
            int asd = 0;
            if (iTurn == PieceStyle.X)
            {
                asd = -1;
            }
            else if(iTurn == PieceStyle.O)
            {
                asd = 1;
            }
            iData[iLastMove] = asd;
            return iLastMove;
        }
        void CheckWinAndDontLose(PieceStyle turn)
        {
            if (Board[0, 0].Style == PieceStyle.Blank && ((Board[1, 0].Style == turn && Board[2, 0].Style == turn) || (Board[0, 1].Style == turn && Board[0, 2].Style == turn) || (Board[1, 1].Style == turn && Board[2, 2].Style == turn)))
                iLastMove = 0;
            else if (Board[1, 0].Style == PieceStyle.Blank && ((Board[0, 0].Style == turn && Board[2, 0].Style == turn) || (Board[1, 1].Style == turn && Board[1, 2].Style == turn)))
                iLastMove = 1;
            else if (Board[2, 0].Style == PieceStyle.Blank && ((Board[0, 0].Style == turn && Board[1, 0].Style == turn) || (Board[2, 1].Style == turn && Board[2, 2].Style == turn) || (Board[1, 1].Style == turn && Board[0, 2].Style == turn)))
                iLastMove = 2;
            else if (Board[0, 1].Style == PieceStyle.Blank && ((Board[0, 0].Style == turn && Board[0, 2].Style == turn) || (Board[1, 1].Style == turn && Board[2, 1].Style == turn)))
                iLastMove = 3;
            else if (Board[1, 1].Style == PieceStyle.Blank && ((Board[0, 0].Style == turn && Board[2, 2].Style == turn) || (Board[2, 0].Style == turn && Board[0, 2].Style == turn) || (Board[1, 0].Style == turn && Board[1, 2].Style == turn) || (Board[0, 1].Style == turn && Board[2, 1].Style == turn)))
                iLastMove = 4;
            else if (Board[2, 1].Style == PieceStyle.Blank && ((Board[2, 0].Style == turn && Board[2, 2].Style == turn) || (Board[0, 1].Style == turn && Board[1, 1].Style == turn)))
                iLastMove = 5;
            else if (Board[0, 2].Style == PieceStyle.Blank && ((Board[0, 0].Style == turn && Board[0, 1].Style == turn) || (Board[1, 2].Style == turn && Board[2, 2].Style == turn) || (Board[2, 0].Style == turn && Board[1, 1].Style == turn)))
                iLastMove = 6;
            else if (Board[1, 2].Style == PieceStyle.Blank && ((Board[1, 0].Style == turn && Board[1, 1].Style == turn) || (Board[0, 2].Style == turn && Board[2, 2].Style == turn)))
                iLastMove = 7;
            else if (Board[2, 2].Style == PieceStyle.Blank && ((Board[2, 0].Style == turn && Board[2, 1].Style == turn) || (Board[0, 2].Style == turn && Board[1, 2].Style == turn) || (Board[0, 0].Style == turn && Board[1, 1].Style == turn)))
                iLastMove = 8;
        }
        int RandomPosition()
        {
            int Count = 0;
            for (int i = 0; i <= 8; i++)
            {
                if (iData[i] == 0)
                {
                    Count++;
                }
            }
            Random rnd = new Random();
            int iRandom = rnd.Next(1, Count);

            Count = 0;
            for (int i = 0; i <= 8; i++)
            {
                if (iData[i] == 0)
                {
                    Count++;
                    if (Count == iRandom)
                    {
                        return i;
                    }
                }
            }
            return -1;
        }

        private void nextTurn()
        {
            CurrentTurn = CurrentTurn == PieceStyle.X ? PieceStyle.O : PieceStyle.X;

            if (CurrentTurn == PieceStyle.O)
            {
                AI();
            }
        }

        public bool IsADraw()
        {
            int pieceBlankCount = 0;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    pieceBlankCount = this.Board[i, j].Style == PieceStyle.Blank
                                        ? pieceBlankCount + 1
                                        : pieceBlankCount;
                }
            }

            return pieceBlankCount == 0;
        }

        public WinningPlay GetWinner()
        {
            WinningPlay winningPlay = null;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    foreach (EvaluationDirection evalDirection in (EvaluationDirection[])Enum.GetValues(typeof(EvaluationDirection)))
                    {
                        winningPlay = EvaluatePieceForWinner(i, j, evalDirection);
                        if (winningPlay != null) { return winningPlay; }
                    }
                }
            }

            return winningPlay;

        }

        private WinningPlay EvaluatePieceForWinner(int i, int j, EvaluationDirection dir)
        {
            GamePiece currentPiece = Board[i, j];
            if (currentPiece.Style == PieceStyle.Blank)
            {
                return null;
            }

            int inARow = 1;
            int iNext = i;
            int jNext = j;

            var winningMoves = new List<string>();

            while (inARow < 3)
            {
                switch (dir)
                {
                    case EvaluationDirection.Up:
                        jNext -= 1;
                        break;
                    case EvaluationDirection.UpRight:
                        iNext += 1;
                        jNext -= 1;
                        break;
                    case EvaluationDirection.Right:
                        iNext += 1;
                        break;
                    case EvaluationDirection.DownRight:
                        iNext += 1;
                        jNext += 1;
                        break;
                }
                if (iNext < 0 || iNext >= 3 || jNext < 0 || jNext >= 3) { break; }
                if (Board[iNext, jNext].Style == currentPiece.Style)
                {
                    winningMoves.Add($"{iNext},{jNext}");
                    inARow++;
                }
                else
                {
                    return null;
                }
            }

            if (inARow >= 3)
            {
                winningMoves.Add($"{i},{j}");

                return new WinningPlay()
                {
                    WinningMoves = winningMoves,
                    WinningStyle = currentPiece.Style,
                    WinningDirection = dir,
                };
            }

            return null;
        }

        public string GetGameCompleteMessage()
        {
            var winningPlay = GetWinner();
            return winningPlay != null ? $"{winningPlay.WinningStyle} Wins!" : "Draw!";
        }

        public bool IsGamePieceAWinningPiece(int i, int j)
        {
            var winningPlay = GetWinner();
            return winningPlay?.WinningMoves?.Contains($"{i},{j}") ?? false;
        }
    }
}
