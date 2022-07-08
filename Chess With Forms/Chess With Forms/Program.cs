using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;

namespace Chess_With_Forms
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static string[,] CreateBoard()
        {
            string[,] board =
            {
                { "bR", "bk", "bB", "bQ", "bK", "bB", "bk", "bR"},
                { "bp", "bp", "bp", "bp", "bp", "bp", "bp", "bp"},
                { "_", "_", "_", "_", "_", "_", "_", "_"},
                { "_", "_", "_", "_", "_", "_", "_", "_"},
                { "_", "_", "_", "_", "_", "_", "_", "_"},
                { "_", "_", "_", "_", "_", "_", "_", "_"},
                { "wp", "wp", "wp", "wp", "wp", "wp", "wp", "wp"},
                { "wR", "wk", "wB", "wQ", "wK", "wB", "wk", "wR"},
            };
            return board;
        }
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            string[,] board = CreateBoard();
            Application.Run(new Form1(board));
        }
    }
}