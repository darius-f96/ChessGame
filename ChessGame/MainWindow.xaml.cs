using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ChessBoard;

namespace ChessGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //game board 
        static Board gameBoard = new Board(8);

        //each button will be a piece
        public Button[,] boardCell = new Button[gameBoard.Size, gameBoard.Size];
        public MainWindow()
        {
            InitializeComponent();
            populateGrid();

        }

        private void populateGrid()
        {
            int buttonSize = (int)(boardGrid.Width / gameBoard.Size);
            boardGrid.Height = boardGrid.Width;
            RowDefinition row1;
            ColumnDefinition col1;

            //instantiate rows and columns
            for (int i = 0; i < gameBoard.Size; i++)
            {
                row1 = new RowDefinition();
                col1 = new ColumnDefinition();
                boardGrid.RowDefinitions.Add(row1);
                boardGrid.ColumnDefinitions.Add(col1);
            }

            for (int i = 0; i < gameBoard.Size; i++)
            {
                for (int j = 0; j < gameBoard.Size; j++)
                {

                    boardCell[i, j] = new Button();
                    boardCell[i, j].Height = buttonSize;
                    boardCell[i, j].Width = buttonSize;

                    boardCell[i, j].Click += selectCell;

                    //now make them visible in grid
                    Grid.SetRow(boardCell[i, j], i);
                    Grid.SetColumn(boardCell[i, j], j);
                    boardGrid.Children.Add(boardCell[i, j]);
                }
            }

            //initial piece placement
            gameBoard.PlaceInitialPieces();
        }

        private void selectCell(object sender, RoutedEventArgs e)
        {
            Button selectedCell = (Button)sender;
            int currentrow = (int)selectedCell.GetValue(Grid.RowProperty);
            int currentcolumn = (int)selectedCell.GetValue(Grid.ColumnProperty);

            Cell currentCell = gameBoard.Grid[currentrow, currentcolumn];

            gameBoard.GetNextLegalMoves(currentCell.OccupiedBy.ToString(), currentCell);

            for (int i = 0; i < gameBoard.Size; i++)
            {
                for (int j = 0; j < gameBoard.Size; j++)
                {
                    boardCell[i, j].Style = (Style)FindResource("clearbtn");
                    if (gameBoard.Grid[i,j].LegalMove)
                    {
                        //ligh yellow
                        boardCell[i, j].Style = (Style)FindResource("legalmovebtn");
                    }
                }
            }
        }

    }
}
