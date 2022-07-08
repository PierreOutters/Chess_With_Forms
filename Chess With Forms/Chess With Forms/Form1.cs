using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;

// To add:
// Allow for en Passent (if in check)
// Add message on CheckMate or StaleMate
// Not allow to castle if in check or has been in check
// Allow range of choice for promotion
// Add timers

namespace Chess_With_Forms
{
    public partial class Form1 : Form
    {
        private int n;
        private PictureBox[,] squareBoard;
        private string[,] board;
        private string[,] reverseBoard = new string[8,8];
        private char col = 'w';
        private List<string[,]> moves = new List<string[,]>();
        public Form1(string[,] inboard)
        {
            board = inboard;
            InitializeComponent();
        }
        private void MakeMove()
        {
            for (int y = 0; y < n; y++)
            {
                for (int x = 0; x < n; x++)
                {
                    squareBoard[y, x].Click += (sender1, e1) =>
                    {
                        PictureBox piece = sender1 as PictureBox;
                        bool isAvailable = false;
                        foreach (char c in piece.Name)
                        {
                            if (c == 'A')
                            {
                                isAvailable = true;
                            }
                        }
                        if (piece.Image != null && isAvailable == false && piece.Name[0] == col)
                        {
                            ResetColours(true);
                            squareBoard[piece.Top / 60, piece.Left / 60].BackColor = Color.Red;
                            MakePieceMove(sender1, e1);
                        }
                        else if (piece.Image != null && isAvailable == false && piece.Name[0] != col)
                        {
                            ResetColours(true);
                            squareBoard[piece.Top / 60, piece.Left / 60].BackColor = Color.Red;
                        }
                        else if (piece.Image == null && isAvailable == false)
                        {
                            ResetColours(true);
                        }
                    };
                }
            }
        }
        private void MakePieceMove(object sender1, EventArgs e1)
        {
            PictureBox piece = sender1 as PictureBox;
            if (piece.Name[1] == 'R')
            {
                foreach (int[] i in RookAvailable(piece))
                {
                    if (CheckAvailable(i, piece))
                    {
                        ShowAvailable(i);
                    }
                }
            }
            else if (piece.Name[1] == 'p')
            {
                foreach (int[] i in PawnAvailable(piece))
                {
                    if (CheckAvailable(i, piece))
                    {
                        ShowAvailable(i);
                    }
                }
            }
            else if (piece.Name[1] == 'B')
            {
                foreach (int[] i in BishopAvailable(piece))
                {
                    if (CheckAvailable(i, piece))
                    {
                        ShowAvailable(i);
                    }
                }
            }
            else if (piece.Name[1] == 'Q')
            {
                foreach (int[] i in QueenAvailable(piece))
                {
                    if (CheckAvailable(i, piece))
                    {
                        ShowAvailable(i);
                    }
                }
            }
            else if (piece.Name[1] == 'k')
            {
                foreach (int[] i in KnightAvailable(piece))
                {
                    if (CheckAvailable(i, piece))
                    {
                        ShowAvailable(i);
                    }
                }
            }
            else if (piece.Name[1] == 'K')
            {
                foreach (int[] i in KingAvailable(piece))
                {
                    if (CheckAvailable(i, piece))
                    {
                        ShowAvailable(i);
                    }
                }
            }
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    squareBoard[i, j].Click += (sender2, e2) =>
                    {
                        PictureBox piece2 = sender2 as PictureBox;
                        for (int k = 0; k < piece2.Name.Length; k++)
                        {
                            if (piece2.Name[k] == 'A')
                            {
                                ResetColours(false);
                                RemoveLastMove();
                                int y1 = piece.Top / 60, y2 = piece2.Top / 60, x1 = piece.Left / 60, x2 = piece2.Left / 60;
                                HasMoved(piece);
                                RemoveDoubleMoved(piece);
                                DoubleMoved(piece, piece2);
                                EnPassent(piece, piece2);
                                Castle(piece, piece2);
                                string name = piece.Name;
                                board[y1, x1] = "_";
                                board[y2, x2] = name;
                                ShowLastMove(y1, y2, x1, x2);
                                Promote();
                                moves.Add(board);
                                ReverseBoard();
                                SetupPieces();
                                bool colour = false;
                                if (col == 'w')
                                {
                                    col = 'b';
                                    colour = true;
                                }
                                else
                                {
                                    col = 'w';
                                }
                                if (CheckCanMove(colour))
                                {
                                    if (CheckCheck(colour))
                                    {
                                        Environment.Exit(0);
                                    }
                                    else
                                    {
                                        Environment.Exit(0);
                                    }
                                }
                            }
                        }
                        piece = sender2 as PictureBox;
                    };
                }
            }
        }
        private void ReverseBoard()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    reverseBoard[i, j] = board[7 - i, 7 - j];
                }
            }
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    board[i, j] = reverseBoard[i, j];
                }
            }
        }
        private void SetupPieces()
        {
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    if (board[y, x].Length > 1)
                    {
                        if (board[y, x][0].ToString() + board[y, x][1].ToString() == "bR")
                        {
                            squareBoard[y, x].Image = Properties.Resources.Black_Rook;
                        }
                        else if (board[y, x][0].ToString() + board[y, x][1].ToString() == "bk")
                        {
                            squareBoard[y, x].Image = Properties.Resources.Black_Knight;
                        }
                        else if (board[y, x][0].ToString() + board[y, x][1].ToString() == "bB")
                        {
                            squareBoard[y, x].Image = Properties.Resources.Black_Bishop;
                        }
                        else if (board[y, x][0].ToString() + board[y, x][1].ToString() == "bQ")
                        {
                            squareBoard[y, x].Image = Properties.Resources.Black_Queen;
                        }
                        else if (board[y, x][0].ToString() + board[y, x][1].ToString() == "bK")
                        {
                            squareBoard[y, x].Image = Properties.Resources.Black_King;
                        }
                        else if (board[y, x][0].ToString() + board[y, x][1].ToString() == "bp")
                        {
                            squareBoard[y, x].Image = Properties.Resources.Black_Pawn;
                        }
                        else if (board[y, x][0].ToString() + board[y, x][1].ToString() == "wR")
                        {
                            squareBoard[y, x].Image = Properties.Resources.White_Rook;
                        }
                        else if (board[y, x][0].ToString() + board[y, x][1].ToString() == "wk")
                        {
                            squareBoard[y, x].Image = Properties.Resources.White_Knight;
                        }
                        else if (board[y, x][0].ToString() + board[y, x][1].ToString() == "wB")
                        {
                            squareBoard[y, x].Image = Properties.Resources.White_Bishop;
                        }
                        else if (board[y, x][0].ToString() + board[y, x][1].ToString() == "wQ")
                        {
                            squareBoard[y, x].Image = Properties.Resources.White_Queen;
                        }
                        else if (board[y, x][0].ToString() + board[y, x][1].ToString() == "wK")
                        {
                            squareBoard[y, x].Image = Properties.Resources.White_King;
                        }
                        else if (board[y, x][0].ToString() + board[y, x][1].ToString() == "wp")
                        {
                            squareBoard[y, x].Image = Properties.Resources.White_Pawn;
                        }
                        squareBoard[y, x].Name = board[y, x];
                    }
                    else
                    {
                        squareBoard[y, x].Image = null;
                        squareBoard[y, x].Name = "";
                    }
                }
            }
        }
        private void ShowAvailable(int[] i)
        {
            if (board[i[0], i[1]] == "_")
            {
                squareBoard[i[0], i[1]].Image = Properties.Resources.available_place_gray;
                squareBoard[i[0], i[1]].Name = "Av";
            }
            else
            {
                squareBoard[i[0], i[1]].BackgroundImage = Properties.Resources.Available_On_Pieces1;
                squareBoard[i[0], i[1]].Name += "A";
            }
        }
        private void Promote()
        {
            //if (/*piece.Name[1] == 'p' && piece.Top / 60 == 6 && piece.Name[0] == 'b' || piece.Name[1] == 'p' && */piece.Top / 60 == 1 && piece.Name[0] == 'w')
            /*{
                string pieceName = "";
                foreach (char c in piece.Name)
                {
                    if (c != 'p')
                    {
                        pieceName += c.ToString();
                    }
                    else if (c == 'p')
                    {
                        pieceName += "Q";
                    }
                }
                piece.Name = pieceName;
            }*/
            for (int i = 0; i < 8; i++)
            {
                if (board[0, i].Length > 1)
                {
                    if (board[0, i].Contains('p'))
                    {
                        string pieceName = "";
                        foreach (char c in board[0, i])
                        {
                            if (c != 'p')
                            {
                                pieceName += c.ToString();
                            }
                            else if (c == 'p')
                            {
                                pieceName += "Q";
                            }
                        }
                        board[0, i] = pieceName;
                    }
                }
            }
            //SetupPieces();
        }
        private bool CheckAvailable (int[] i, PictureBox piece)
        {
            bool colour = true; bool isAvailable = true;
            if (col == 'w')
            {
                colour = false;
            }
            RemoveCheck(colour);
            int y1 = piece.Top / 60, y2 = i[0], x1 = piece.Left / 60, x2 = i[1];
            string name = piece.Name;
            string temp1 = board[y1, x1], temp2 = board[y2, x2];
            board[y1, x1] = "_";
            board[y2, x2] = name;
            EnPassent(piece, squareBoard[i[0], i[1]]);
            if (CheckCheck(colour))
            {
                isAvailable = false;
            }
            board[y1, x1] = temp1; board[y2, x2] = temp2;
            return isAvailable;
        }
        private void RemoveCheck(bool colour)
        {
            char c;
            if (colour)
            {
                c = 'b';
            }
            else
            {
                c = 'w';
            }
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (board[i, j].Contains('K') && board[i, j].Contains('C') && board[i, j].Contains(c))
                    {
                        string pieceName = "", name = board[i, j];
                        for (int k = 0; k < name.Length; k++)
                        {
                            if (name[k] != 'C')
                            {
                                pieceName += name[k];
                            }
                        }
                        board[i, j] = pieceName;
                    }
                }
            }
        }
        private void GoThroughAvailable(int i, int j, bool colour)
        {
            char c;
            if (colour)
            {
                c = 'w';
            }
            else
            {
                c = 'b';
            }
            PictureBox piece = squareBoard[i, j];
            if (board[i, j].Contains('R') && board[i, j].Contains(c))
            {
                RookAvailable(piece);
            }
            else if (board[i, j].Contains('p') && board[i, j].Contains(c))
            {
                PawnAvailable(piece);
            }
            else if (board[i, j].Contains('B') && board[i, j].Contains(c))
            {
                BishopAvailable(piece);
            }
            else if (board[i, j].Contains('k') && board[i, j].Contains(c))
            {
                KnightAvailable(piece);
            }
            else if (board[i, j].Contains('Q') && board[i, j].Contains(c))
            {
                QueenAvailable(piece);
            }
            else if (board[i, j].Contains('K') && board[i, j].Contains(c))
            {
                KingAvailable(piece);
            }
        }
        private bool CheckCanMove(bool colour)
        {
            char col0 = 'w';
            if (colour)
            {
                col0 = 'b';
            }
            bool isCheckMate = true;
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    if (board[y, x][0] == col0)
                    {
                        if (board[y, x].Contains('p'))
                        {
                            foreach (int[] i in PawnAvailable(squareBoard[y, x]))
                            {
                                if (CheckAvailable(i, squareBoard[y, x]))
                                {
                                    isCheckMate = false;
                                }
                            }
                        }
                        else if (board[y, x].Contains('R'))
                        {
                            foreach (int[] i in RookAvailable(squareBoard[y, x]))
                            {
                                if (CheckAvailable(i, squareBoard[y, x]))
                                {
                                    isCheckMate = false;
                                }
                            }
                        }
                        else if (board[y, x].Contains('B'))
                        {
                            foreach (int[] i in BishopAvailable(squareBoard[y, x]))
                            {
                                if (CheckAvailable(i, squareBoard[y, x]))
                                {
                                    isCheckMate = false;
                                }
                            }
                        }
                        else if (board[y, x].Contains('k'))
                        {
                            foreach (int[] i in KnightAvailable(squareBoard[y, x]))
                            {
                                if (CheckAvailable(i, squareBoard[y, x]))
                                {
                                    isCheckMate = false;
                                }
                            }
                        }
                        else if (board[y, x].Contains('Q'))
                        {
                            foreach (int[] i in QueenAvailable(squareBoard[y, x]))
                            {
                                if (CheckAvailable(i, squareBoard[y, x]))
                                {
                                    isCheckMate = false;
                                }
                            }
                        }
                        else if (board[y, x].Contains('K'))
                        {
                            foreach (int[] i in KingAvailable(squareBoard[y, x]))
                            {
                                if (CheckAvailable(i, squareBoard[y, x]))
                                {
                                    isCheckMate = false;
                                }
                            }
                        }
                    }
                }
            }
            return isCheckMate;
        }
        private bool CheckCheck(bool colour)
        {
            char c;
            if (colour)
            {
                c = 'b';
            }
            else
            {
                c = 'w';
            }
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    GoThroughAvailable(i, j, colour);
                    if (board[i, j].Contains('K') && board[i, j].Contains('C') && board[i, j].Contains(c))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        private void Castle(PictureBox piece, PictureBox piece2)
        {
            string pieceName;
            if (piece2.Left / 60 - piece.Left / 60 == 2 && piece.Name[0] == 'w')
            {
                pieceName = board[7, 7] + "M";
                board[7, 7] = "_";
                board[7, 5] = pieceName;
            }
            else if (piece2.Left / 60 - piece.Left / 60 == -2 && piece.Name[0] == 'w')
            {
                pieceName = board[7, 0] + "M";
                board[7, 0] = "_";
                board[7, 3] = pieceName;
            }
            else if (piece2.Left / 60 - piece.Left / 60 == 2 && piece.Name[0] == 'b')
            {
                pieceName = board[7, 7] + "M";
                board[7, 7] = "_";
                board[7, 4] = pieceName;
            }
            else if (piece2.Left / 60 - piece.Left / 60 == -2 && piece.Name[0] == 'b')
            {
                pieceName = board[7, 0] + "M";
                board[7, 0] = "_";
                board[7, 2] = pieceName;
            }
        }
        private void EnPassent(PictureBox piece, PictureBox piece2)
        {
            if (board[piece2.Top / 60, piece2.Left / 60] == "_" && piece.Name[1] == 'p')
            {
                if (piece.Left / 60 - piece2.Left / 60 == - 1 || piece.Left / 60 - piece2.Left / 60 == 1)
                {
                    board[piece2.Top / 60 + 1, piece2.Left / 60] = "_";
                }
            }
        }
        private void DoubleMoved(PictureBox piece, PictureBox piece2)
        {
            if (piece.Name[1] == 'p')
            {
                if (piece.Top / 60 - piece2.Top / 60 == 2 || piece.Top / 60 - piece2.Top / 60 == -2)
                {
                    piece.Name += "D";
                    board[piece.Top / 60, piece.Left / 60] += "D";
                }
            }
        }
        private void RemoveDoubleMoved(PictureBox piece)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (squareBoard[7 - i, 7 - j] != piece)
                    {
                        string pieceName = "";
                        if (squareBoard[7 - i, 7 - j].Name.Contains('D'))
                        {
                            foreach (char c in squareBoard[7 - i, 7 - j].Name)
                            {
                                if (c != 'D')
                                {
                                    pieceName += c.ToString();
                                }
                            }
                            squareBoard[7 - i, 7 - j].Name = pieceName;
                            board[7 - i, 7 - j] = pieceName;
                        }
                    }
                }
            }
        }
        private void HasMoved(PictureBox piece)
        {
            bool beforeMoved = false;
            foreach (char c in piece.Name)
            {
                if (c == 'M')
                {
                    beforeMoved = true;
                }
            }
            if (!beforeMoved)
            {
                piece.Name += 'M';
            }
        }
        private void ResetColours(bool resetAv)
        {
            bool colour = false;
            for (int y = 0; y < n; y++)
            {
                for (int x = 0; x < n; x++)
                {
                    if (colour)
                    {
                        if (squareBoard[y, x].BackColor != Color.FromArgb(246, 190, 0))
                        {
                            squareBoard[y, x].BackColor = Color.Green;
                        }
                        colour = false;
                    }
                    else
                    {
                        if (squareBoard[y, x].BackColor != Color.Orange)
                        {
                            squareBoard[y, x].BackColor = Color.FromArgb(255, 225, 217, 209);
                        }
                        colour = true;
                    }
                    if (x == 7)
                    {
                        colour = !colour;
                    }
                    if (squareBoard[y, x].BackgroundImage != null)
                    {
                        squareBoard[y, x].BackgroundImage = null;
                        List<char> list = new List<char>();
                        for (int i = 0; i < squareBoard[y, x].Name.Length; i++)
                        {
                            if (squareBoard[y, x].Name[i] != 'A')
                            {
                                list.Add(squareBoard[y, x].Name[i]);
                            }
                            squareBoard[y, x].Name = list.ToString();
                        }
                        SetupPieces();
                    }
                    if (resetAv)
                    {
                        if (squareBoard[y, x].Name == "Av")
                        {
                            squareBoard[y, x].Image = null;
                            squareBoard[y, x].Name = "";
                        }
                    }
                }
            }
        }
        private void RemoveLastMove()
        {
            for (int y = 0; y < n; y++)
            {
                for (int x = 0; x < n; x++)
                {
                    if (squareBoard[y, x].BackColor == Color.FromArgb(246, 190, 0))
                    {
                        squareBoard[y, x].BackColor = Color.Green;
                    }
                    else if (squareBoard[y, x].BackColor == Color.Orange)
                    {
                        squareBoard[y, x].BackColor = Color.FromArgb(255, 225, 217, 209);
                    }
                }
            }
        }
        private void ShowLastMove(int y1, int y2, int x1, int x2)
        {
            bool colour = false;
            for (int y = 0; y < n; y++)
            {
                for (int x = 0; x < n; x++)
                {
                    if (colour)
                    {
                        if (y == 7 - y1 && x == 7 - x1 || y == 7 - y2 && x == 7 - x2)
                        {
                            squareBoard[y, x].BackColor = Color.FromArgb(246, 190, 0);
                        }
                        colour = false;
                    }
                    else
                    {
                        if (y == 7 - y1 && x == 7 - x1 || y == 7 - y2 && x == 7 - x2)
                        {
                            squareBoard[y, x].BackColor = Color.Orange;
                        }
                        colour = true;
                    }
                    if (x == 7)
                    {
                        colour = !colour;
                    }
                }
            }
        }
        private List<int[]> PawnAvailable(PictureBox piece) // Redo
        {
            List<int[]> list = new List<int[]>();
            bool hasmoved = false;
            foreach (char c in piece.Name)
            {
                if (c == 'M')
                {
                    hasmoved = true;
                }
            }
            if (col == piece.Name[0])
            {
                if (board[piece.Top / 60 - 1, piece.Left / 60] == "_")
                {
                    list.Add(new int[] { piece.Top / 60 - 1, piece.Left / 60 });
                    if (!hasmoved)
                    {
                        if (board[piece.Top / 60 - 2, piece.Left / 60] == "_")
                            list.Add(new int[] { piece.Top / 60 - 2, piece.Left / 60 });
                    }
                }
                if (piece.Left / 60 != 7)
                {
                    if (board[piece.Top / 60 - 1, piece.Left / 60 + 1] != "_")
                    {
                        if (board[piece.Top / 60 - 1, piece.Left / 60 + 1][0] != piece.Name[0])
                        {
                            if (board[piece.Top / 60 - 1, piece.Left / 60 + 1].Contains('K'))
                            {
                                board[piece.Top / 60 - 1, piece.Left / 60 + 1] += "C";
                            }
                            list.Add(new int[] { piece.Top / 60 - 1, piece.Left / 60 + 1 });
                        }
                    }
                    if (board[piece.Top / 60, piece.Left / 60 + 1] != "_")
                    {
                        if (board[piece.Top / 60, piece.Left / 60 + 1].Contains('D'))
                        {
                            list.Add(new int[] { piece.Top / 60 - 1, piece.Left / 60 - 1 });
                        }
                    }
                }
                if (piece.Left / 60 != 0)
                {
                    if (board[piece.Top / 60 - 1, piece.Left / 60 - 1] != "_")
                    {
                        if (board[piece.Top / 60 - 1, piece.Left / 60 - 1][0] != piece.Name[0])
                        {
                            if (board[piece.Top / 60 - 1, piece.Left / 60 - 1].Contains('K'))
                            {
                                board[piece.Top / 60 - 1, piece.Left / 60 - 1] += "C";
                            }
                            list.Add(new int[] { piece.Top / 60 - 1, piece.Left / 60 - 1 });
                        }
                    }
                    if (board[piece.Top / 60, piece.Left / 60 - 1] != "_")
                    {
                        if (board[piece.Top / 60, piece.Left / 60 - 1].Contains('D'))
                        {
                            list.Add(new int[] { piece.Top / 60 - 1, piece.Left / 60 - 1 });
                        }
                    }
                }
            }
            else
            {
                if (board[piece.Top / 60 + 1, piece.Left / 60] == "_")
                {
                    list.Add(new int[] { piece.Top / 60 + 1, piece.Left / 60 });
                    if (!hasmoved)
                    {
                        if (board[piece.Top / 60 + 2, piece.Left / 60] == "_")
                        {
                            list.Add(new int[] { piece.Top / 60 + 2, piece.Left / 60 });
                        }
                    }
                }
                if (piece.Left / 60 != 7)
                {
                    if (board[piece.Top / 60 + 1, piece.Left / 60 + 1] != "_")
                    {
                        if (!board[piece.Top / 60 + 1, piece.Left / 60 + 1].Contains(piece.Name[0]))
                        {
                            if (board[piece.Top / 60 + 1, piece.Left / 60 + 1].Contains('K'))
                            {
                                board[piece.Top / 60 + 1, piece.Left / 60 + 1] += "C";
                            }
                            list.Add(new int[] { piece.Top / 60 + 1, piece.Left / 60 + 1 });
                        }
                    }
                }
                if (piece.Left / 60 != 0)
                {
                    if (board[piece.Top / 60 + 1, piece.Left / 60 - 1] != "_")
                    {
                        if (!board[piece.Top / 60 + 1, piece.Left / 60 - 1].Contains(piece.Name[0]))
                        {
                            if (board[piece.Top / 60 + 1, piece.Left / 60 - 1].Contains('K'))
                            {
                                board[piece.Top / 60 + 1, piece.Left / 60 - 1] += "C";
                            }
                            list.Add(new int[] { piece.Top / 60 + 1, piece.Left / 60 - 1 });
                        }
                    }
                }
            }
            return list;
        }
        private List<int[]> RookAvailable(PictureBox piece)
        {
            List<int[]> list = new List<int[]>();
            int x = piece.Left / 60, y = piece.Top / 60;
            for (int i = y - 1; i >= 0 / 60; i--)
            {
                if (board[i, x] != "_")
                {
                    if (board[i, x][0] != piece.Name[0])
                    {
                        if (board[i, x].Contains('K'))
                        {
                            board[i, x] += "C";
                        }
                        list.Add(new int[] { i, x });
                    }
                    break;
                }
                list.Add(new int[] { i, x });
            }
            for (int i = y + 1; i < 8; i++)
            {
                if (board[i, x] != "_")
                { 
                    if (board[i, x][0] != piece.Name[0])
                    {
                        if (board[i, x].Contains('K'))
                        {
                            board[i, x] += "C";
                        }
                        list.Add(new int[] { i, x });
                    }
                    break;
                }
                list.Add(new int[] { i, x });
            }
            for (int i = x - 1; i >= 0; i--)
            {
                if (board[y, i] != "_")
                {
                    if (board[y, i][0] != piece.Name[0])
                    {
                        if (board[y, i].Contains('K'))
                        {
                            board[y, i] += "C";
                        }
                        list.Add(new int[] { piece. Top / 60, i });
                    }
                    break;
                }
                list.Add(new int[] { y, i });
            }
            for (int i = x + 1; i < 8; i++)
            {
                if (board[y, i] != "_")
                {
                    if (board[y, i][0] != piece.Name[0])
                    {
                        if (board[y, i].Contains('K'))
                        {
                            board[y, i] += "C";
                        }
                        list.Add(new int[] { y, i });
                    }
                    break;
                }
                list.Add(new int[] { y, i });
            }
            return list;
        }
        private List<int[]> BishopAvailable(PictureBox piece)
        {
            List<int[]> list = new List<int[]>();
            int y = piece.Top / 60 + 1;
            for (int x = piece.Left / 60 + 1; x < 8; x++)
            {
                if (y > 7)
                {
                    break;
                }
                if (board[y, x] != "_")
                {
                    if (board[y, x][0] != piece.Name[0])
                    {
                        if (board[y, x].Contains('K'))
                        {
                            board[y, x] += "C";
                        }
                        list.Add(new int[] { y, x });
                    }
                    break;
                }
                list.Add(new int[] { y, x });
                y++;
            }
            y = piece.Top / 60 - 1;
            for (int x = piece.Left / 60 + 1; x < 8; x++)
            {
                if (y < 0)
                {
                    break;
                }
                if (board[y, x] != "_")
                {
                    if (board[y, x][0] != piece.Name[0])
                    {
                        if (board[y, x].Contains('K'))
                        {
                            board[y, x] += "C";
                        }
                        list.Add(new int[] { y, x });
                    }
                    break;
                }
                list.Add(new int[] { y, x });
                y--;
            }
            y = piece.Top / 60 + 1;
            for (int x = piece.Left / 60 - 1; x >= 0; x--)
            {
                if (y > 7)
                {
                    break;
                }
                if (board[y, x] != "_")
                {
                    if (board[y, x][0] != piece.Name[0])
                    {
                        if (board[y, x].Contains('K'))
                        {
                            board[y, x] += "C";
                        }
                        list.Add(new int[] { y, x });
                    }
                    break;
                }
                list.Add(new int[] { y, x });
                y++;
            }
            y = piece.Top / 60 - 1;
            for (int x = piece.Left / 60 - 1; x >= 0; x--)
            {
                if (y <  0)
                {
                    break;
                }
                if (board[y, x] != "_")
                {
                    if (board[y, x][0] != piece.Name[0])
                    {
                        if (board[y, x].Contains('K'))
                        {
                            board[y, x] += "C";
                        }
                        list.Add(new int[] { y, x });
                    }
                    break;
                }
                list.Add(new int[] { y, x });
                y--;
            }
            return list;
        }
        private List<int[]> QueenAvailable(PictureBox piece)
        {
            List<int[]> list = new List<int[]>();
            int x1 = piece.Left / 60, y1 = piece.Top / 60;
            for (int i = y1 - 1; i >= 0 / 60; i--)
            {
                if (board[i, x1] != "_")
                {
                    if (board[i, x1][0] != piece.Name[0])
                    {
                        if (board[i, x1].Contains('K'))
                        {
                            board[i, x1] += "C";
                        }
                        list.Add(new int[] { i, x1 });
                    }
                    break;
                }
                list.Add(new int[] { i, x1 });
            }
            for (int i = y1 + 1; i < 8; i++)
            {
                if (board[i, x1] != "_")
                {
                    if (board[i, x1][0] != piece.Name[0])
                    {
                        if (board[i, x1].Contains('K'))
                        {
                            board[i, x1] += "C";
                        }
                        list.Add(new int[] { i, x1 });
                    }
                    break;
                }
                list.Add(new int[] { i, x1 });
            }
            for (int i = x1 - 1; i >= 0; i--)
            {
                if (board[y1, i] != "_")
                {
                    if (board[y1, i][0] != piece.Name[0])
                    {
                        if (board[y1, i].Contains('K'))
                        {
                            board[y1, i] += "C";
                        }
                        list.Add(new int[] { piece.Top / 60, i });
                    }
                    break;
                }
                list.Add(new int[] { y1, i });
            }
            for (int i = x1 + 1; i < 8; i++)
            {
                if (board[y1, i] != "_")
                {
                    if (board[y1, i][0] != piece.Name[0])
                    {
                        if (board[y1, i].Contains('K'))
                        {
                            board[y1, i] += "C";
                        }
                        list.Add(new int[] { y1, i });
                    }
                    break;
                }
                list.Add(new int[] { y1, i });
            }
            int y = piece.Top / 60 + 1;
            for (int x = piece.Left / 60 + 1; x < 8; x++)
            {
                if (y > 7)
                {
                    break;
                }
                if (board[y, x] != "_")
                {
                    if (board[y, x][0] != piece.Name[0])
                    {
                        if (board[y, x].Contains('K'))
                        {
                            board[y, x] += "C";
                        }
                        list.Add(new int[] { y, x });
                    }
                    break;
                }
                list.Add(new int[] { y, x });
                y++;
            }
            y = piece.Top / 60 - 1;
            for (int x = piece.Left / 60 + 1; x < 8; x++)
            {
                if (y < 0)
                {
                    break;
                }
                if (board[y, x] != "_")
                {
                    if (board[y, x][0] != piece.Name[0])
                    {
                        if (board[y, x].Contains('K'))
                        {
                            board[y, x] += "C";
                        }
                        list.Add(new int[] { y, x });
                    }
                    break;
                }
                list.Add(new int[] { y, x });
                y--;
            }
            y = piece.Top / 60 + 1;
            for (int x = piece.Left / 60 - 1; x >= 0; x--)
            {
                if (y > 7)
                {
                    break;
                }
                if (board[y, x] != "_")
                {
                    if (board[y, x][0] != piece.Name[0])
                    {
                        if (board[y, x].Contains('K'))
                        {
                            board[y, x] += "C";
                        }
                        list.Add(new int[] { y, x });
                    }
                    break;
                }
                list.Add(new int[] { y, x });
                y++;
            }
            y = piece.Top / 60 - 1;
            for (int x = piece.Left / 60 - 1; x >= 0; x--)
            {
                if (y < 0)
                {
                    break;
                }
                if (board[y, x] != "_")
                {
                    if (board[y, x][0] != piece.Name[0])
                    {
                        if (board[y, x].Contains('K'))
                        {
                            board[y, x] += "C";
                        }
                        list.Add(new int[] { y, x });
                    }
                    break;
                }
                list.Add(new int[] { y, x });
                y--;
            }
            return list;
        }
        private List<int[]> KnightAvailable(PictureBox piece)
        {
            List<int[]> list = new List<int[]>();
            int y = piece.Top / 60, x = piece.Left / 60;
            if (y - 2 >= 0 && x - 1 >= 0)
            {
                if (board[y - 2, x - 1] == "_")
                {
                    list.Add(new int[] { y - 2, x - 1 });
                }
                else
                {
                    if (board[y - 2, x - 1][0] != piece.Name[0])
                    {
                        if (board[y - 2, x - 1].Contains('K'))
                        {
                            board[y - 2, x - 1] += "C";
                        }
                        list.Add(new int[] { y - 2, x - 1 });
                    }
                }
            }
            if (y - 2 >= 0 && x + 1 < 8)
            {
                if (board[y - 2, x + 1] == "_")
                {
                    list.Add(new int[] { y - 2, x + 1 });
                }
                else
                {
                    if (board[y - 2, x + 1][0] != piece.Name[0])
                    {
                        if (board[y - 2, x + 1].Contains('K'))
                        {
                            board[y - 2, x + 1] += "C";
                        }
                        list.Add(new int[] { y - 2, x + 1 });
                    }
                }
            }
            if (y - 1 >= 0 && x - 2 >= 0)
            {
                if (board[y - 1, x - 2] == "_")
                {
                    list.Add(new int[] { y - 1, x - 2 });
                }
                else
                {
                    if (board[y - 1, x - 2][0] != piece.Name[0])
                    {
                        if (board[y - 1, x - 2].Contains('K'))
                        {
                            board[y - 1, x - 2] += "C";
                        }
                        list.Add(new int[] { y - 1, x - 2 });
                    }
                }
            }
            if (y - 1 >= 0 && x + 2 < 8)
            {
                if (board[y - 1, x + 2] == "_")
                {
                    list.Add(new int[] { y - 1, x + 2 });
                }
                else
                {
                    if (board[y - 1, x + 2][0] != piece.Name[0])
                    {
                        if (board[y - 1, x + 2].Contains('K'))
                        {
                            board[y - 1, x + 2] += "C";
                        }
                        list.Add(new int[] { y - 1, x + 2 });
                    }
                }
            }
            if (y + 2 < 8 && x - 1 >= 0)
            {
                if (board[y + 2, x - 1] == "_")
                {
                    list.Add(new int[] { y + 2, x - 1 });
                }
                else
                {
                    if (board[y + 2, x - 1][0] != piece.Name[0])
                    {
                        if (board[y + 2, x - 1].Contains('K'))
                        {
                            board[y + 2, x - 1] += "C";
                        }
                        list.Add(new int[] { y + 2, x - 1 });
                    }
                }
            }
            if (y + 2 < 8 && x + 1 < 8)
            {
                if (board[y + 2, x + 1] == "_")
                {
                    list.Add(new int[] { y + 2, x + 1 });
                }
                else
                {
                    if (board[y + 2, x + 1][0] != piece.Name[0])
                    {
                        if (board[y + 2, x + 1].Contains('K'))
                        {
                            board[y + 2, x + 1] += "C";
                        }
                        list.Add(new int[] { y + 2, x + 1 });
                    }
                }
            }
            if (y + 1 < 8 && x - 2 >= 0)
            {
                if (board[y + 1, x - 2] == "_")
                {
                    list.Add(new int[] { y + 1, x - 2 });
                }
                else
                {
                    if (board[y + 1, x - 2][0] != piece.Name[0])
                    {
                        if (board[y + 1, x - 2].Contains('K'))
                        {
                            board[y + 1, x - 2] += "C";
                        }
                        list.Add(new int[] { y + 1, x - 2 });
                    }
                }
            }
            if (y + 1 < 8 && x + 2 < 8)
            {
                if (board[y + 1, x + 2] == "_")
                {
                    list.Add(new int[] { y + 1, x + 2 });
                }
                else
                {
                    if (board[y + 1, x + 2][0] != piece.Name[0])
                    {
                        if (board[y + 1, x + 2].Contains('K'))
                        {
                            board[y + 1, x + 2] += "C";
                        }
                        list.Add(new int[] { y + 1, x + 2 });
                    }
                }
            }
            return list;
        }
        private List<int[]> KingAvailable(PictureBox piece)
        {
            List<int[]> list = new List<int[]>();
            int y = piece.Top / 60, x = piece.Left / 60;
            if (y + 1 < 8)
            {
                if (board[y + 1, x] == "_")
                {
                    list.Add(new int[] { y + 1, x });
                }
                else
                {
                    if (board[y + 1, x][0] != piece.Name[0])
                    {
                        if (board[y + 1, x].Contains('K'))
                        {
                            board[y + 1, x] += "C";
                        }
                        list.Add(new int[] { y + 1, x });
                    }
                }
            }
            if (y + 1 < 8 && x + 1 < 8)
            {
                if (board[y + 1, x + 1] == "_")
                {
                    list.Add(new int[] { y + 1, x + 1});
                }
                else
                {
                    if (board[y + 1, x + 1][0] != piece.Name[0])
                    {
                        if (board[y + 1, x + 1].Contains('K'))
                        {
                            board[y + 1, x + 1] += "C";
                        }
                        list.Add(new int[] { y + 1, x + 1});
                    }
                }
            }
            if (x + 1 < 8)
            {
                if (board[y, x + 1] == "_")
                {
                    list.Add(new int[] { y, x + 1 });
                }
                else
                {
                    if (board[y, x + 1][0] != piece.Name[0])
                    {
                        if (board[y, x + 1].Contains('K'))
                        {
                            board[y, x + 1] += "C";
                        }
                        list.Add(new int[] { y, x + 1 });
                    }
                }
            }
            if (y - 1 >= 0 && x + 1 < 8)
            {
                if (board[y - 1, x + 1] == "_")
                {
                    list.Add(new int[] { y - 1, x + 1 });
                }
                else
                {
                    if (board[y - 1, x + 1][0] != piece.Name[0])
                    {
                        if (board[y - 1, x + 1].Contains('K'))
                        {
                            board[y - 1, x+ 1] += "C";
                        }
                        list.Add(new int[] { y - 1, x + 1 });
                    }
                }
            }
            if (y - 1 >= 0)
            {
                if (board[y - 1, x] == "_")
                {
                    list.Add(new int[] { y - 1, x });
                }
                else
                {
                    if (board[y - 1, x][0] != piece.Name[0])
                    {
                        if (board[y - 1, x].Contains('K'))
                        {
                            board[y - 1, x] += "C";
                        }
                        list.Add(new int[] { y - 1, x });
                    }
                }
            }
            if (y + 1 < 8 && x - 1 >= 0)
            {
                if (board[y + 1, x - 1] == "_")
                {
                    list.Add(new int[] { y + 1, x - 1 });
                }
                else
                {
                    if (board[y + 1, x - 1][0] != piece.Name[0])
                    {
                        if (board[y + 1, x - 1].Contains('K'))
                        {
                            board[y + 1, x - 1] += "C";
                        }
                        list.Add(new int[] { y + 1, x - 1 });
                    }
                }
            }
            if (x - 1 >= 0)
            {
                if (board[y, x - 1] == "_")
                {
                    list.Add(new int[] { y, x - 1 });
                }
                else
                {
                    if (board[y, x - 1][0] != piece.Name[0])
                    {
                        if (board[y, x - 1].Contains('K'))
                        {
                            board[y, x - 1] += "C";
                        }
                        list.Add(new int[] { y, x - 1 });
                    }
                }
            }
            if (y - 1 >= 0 && x - 1 >= 0)
            {
                if (board[y - 1, x - 1] == "_")
                {   
                    list.Add(new int[] { y - 1, x - 1 });
                }
                else
                {
                    if (board[y - 1, x - 1][0] != piece.Name[0])
                    {
                        if (board[y - 1, x - 1].Contains('K'))
                        {
                            board[y - 1, x - 1] += "C";
                        }
                        list.Add(new int[] { y - 1, x - 1 });
                    }
                }
            }
            if (!piece.Name.Contains('M'))
            {
                for (int i = 0; i < x; i++)
                {
                    if (board[7, i].Length > 1)
                    {
                        if (board[7, i].Contains('R'))
                        {
                            if (!board[7, i].Contains('M'))
                            {
                                bool canCastle = true;
                                for (int j = i + 1; j < x; j++)
                                {
                                    if (board[7, j] != "_")
                                    {
                                        canCastle = false;
                                    }
                                }
                                if (canCastle)
                                {
                                    list.Add(new int[] { y, x - 2 });
                                }
                                break;
                            }
                        }
                    }
                }
                for (int i = 7; i > x; i--)
                {
                    if (board[7, i].Length > 1)
                    {
                        if (board[7, i].Contains('R'))
                        {
                            if (!board[7, i].Contains('M'))
                            {
                                bool canCastle = true;
                                for (int j = x + 1; j < i; j++)
                                {
                                    if (board[7, j] != "_")
                                    {
                                        canCastle = false;
                                    }
                                }
                                if (canCastle)
                                {
                                    list.Add(new int[] { y, x + 2 });
                                }
                                break;
                            }
                        }
                    }
                }
            }
            return list;
        }
        private void Setup()
        {
            n = 8;
            squareBoard = new PictureBox[n, n];
            int left, top = 2;
            bool colour = false;
            for (int y = 0; y < n; y++)
            {
                left = 2;
                for (int x = 0; x < n; x++)
                {
                    squareBoard[y, x] = new PictureBox();
                    if (colour)
                    {
                        squareBoard[y, x].BackColor = Color.Green;
                        colour = false;
                    }
                    else
                    {
                        squareBoard[y, x].BackColor = Color.FromArgb(255, 225, 217, 209);
                        colour = true;
                    }
                    squareBoard[y, x].Location = new Point(left, top);
                    squareBoard[y, x].Size = new Size((Boardpanel.Width - 2) / 8, (Boardpanel.Height - 2) / 8);
                    left += (Boardpanel.Width - 2) / 8;
                    Boardpanel.Controls.Add(squareBoard[y, x]);
                    if (x == 7)
                    {
                        colour = !colour;
                    }
                }
                top += (Boardpanel.Height - 2) / 8;
            }
            moves.Add(board);
            SetupPieces();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Setup();
            MakeMove();
        }
    }   
}