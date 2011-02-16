﻿using BlazorWasmTicTacToe.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWasmTicTacToe.Models
{
    public class WinningPlay
    {
        public List<string> WinningMoves { get; set; }
        public EvaluationDirection WinningDirection { get; set; }
        public PieceStyle WinningStyle { get; set; }
    }
}
