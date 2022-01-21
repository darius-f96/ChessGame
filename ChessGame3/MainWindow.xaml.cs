using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        Game currentGame;
        Player p1,p2;
        string currentTurn;
        string currentColor;
        Cell currentlySelectedCell, whiteKing, blackKing;

        CollectionViewSource playerViewSource, playerTakeViewSource, playerMoveViewSource;

        ChessGameEntitiesModel cgem = new ChessGameEntitiesModel();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
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
            ResetGame();
        }

        private void ResetGame()
        {
            //initial piece placement
            gameBoard.PlaceInitialPieces();

            //disable chess board until game starts
            boardGrid.IsEnabled = false;
            for (int i = 0; i < gameBoard.Size; i++)
            {
                for (int j = 0; j < gameBoard.Size; j++)
                {
                    SetCellText(i, j);
                }
            }
            //assign kings for tracking
            whiteKing = gameBoard.Grid[0, 4];
            blackKing = gameBoard.Grid[7, 3];

        }

        private void SetCellText(int i, int j)
        {
            if (gameBoard.Grid[i, j].Occupied)
            {
                boardCell[i, j].Content = gameBoard.Grid[i, j].OccupiedBy.PieceColor + gameBoard.Grid[i, j].OccupiedBy.Piece.ToString();
            }
            else
            {
                boardCell[i, j].Content = "";
            }
        }

        private void selectCell(object sender, RoutedEventArgs e)
        {
            Button selectedCell = (Button)sender;
            int currentrow = (int)selectedCell.GetValue(Grid.RowProperty);
            int currentcolumn = (int)selectedCell.GetValue(Grid.ColumnProperty);
            Cell currentCell = gameBoard.Grid[currentrow, currentcolumn];
            bool bCheck;

            if (!IsPieceControlAllowed(currentCell) && currentlySelectedCell == null)
            {
                return;
            }

            if (currentColor == "white")
            {
                bCheck = gameBoard.CheckCheck(whiteKing, "white");
            }
            else
            {
                bCheck = gameBoard.CheckCheck(blackKing, "black");
            }

            if (bCheck)
            {
                if (gameBoard.CheckMate(currentCell))
                {
                    MessageBox.Show("CHECK MATE!");
                    EndGame();

                }
            }

            //first selection of the player
            if (currentlySelectedCell == null)
            {    
                if (bCheck && currentCell.OccupiedBy.Piece != PieceOptions.King)
                {
                    MessageBox.Show("CHECK!!!!");
                    return;
                }
                currentlySelectedCell = currentCell;

                gameBoard.GetNextLegalMoves(currentCell.OccupiedBy.Piece.ToString(), currentCell);

                for (int i = 0; i < gameBoard.Size; i++)
                {
                    for (int j = 0; j < gameBoard.Size; j++)
                    {
                        if (gameBoard.Grid[i, j].LegalMove)
                        {
                            //ligh yellow
                            boardCell[i, j].Style = (Style)FindResource("legalmovebtn");
                        }
                        else { boardCell[i, j].Style = (Style)FindResource("clearbtn"); }
                    }
                }
            }
            //if select same cell twice in a row, then just clear selection
            else if (currentrow == currentlySelectedCell.File & currentcolumn == currentlySelectedCell.Rank)
            {
                //clear currentlySelectedCell
                currentlySelectedCell = null;

                //clear colors
                for (int i = 0; i < gameBoard.Size; i++)
                {
                    for (int j = 0; j < gameBoard.Size; j++)
                    {
                        boardCell[i, j].Style = (Style)FindResource("clearbtn");
                    }
                }
            }
            //move of the player
            else
            {
                if (currentlySelectedCell.OccupiedBy.PieceColor != currentCell.OccupiedBy.PieceColor && currentCell.LegalMove)
                {
                    PlayerMove(currentlySelectedCell, currentCell);
                    //clear currentlySelectedCell
                    currentlySelectedCell = null;

                    //clear colors
                    for (int i = 0; i < gameBoard.Size; i++)
                    {
                        for (int j = 0; j < gameBoard.Size; j++)
                        {
                            boardCell[i, j].Style = (Style)FindResource("clearbtn");
                        }
                    }
                }
            }

        }

        public void PlayerMove(Cell from, Cell to)
        {
            Player p, enemy;
            if (currentTurn == p1.PlayerName)
            {
                p = p1;
                enemy = p2;
            }
            else
            {
                p = p2;
                enemy = p1;
            }

            //register take
            if (to.Occupied && from.OccupiedBy.PieceColor != to.OccupiedBy.PieceColor && to.LegalMove)
            {

                //register take
                PlayerTake pt = new PlayerTake();
                pt.TakerId = p.Id;
                pt.TakenId = enemy.Id;
                pt.PieceNameTaken = to.OccupiedBy.Piece.ToString();
                pt.PieceNameTaker = from.OccupiedBy.Piece.ToString();
                pt.GameId = currentGame.Id;
                cgem.PlayerTakes.Add(pt);

                //put player piece in spot of enemy piece
                to.OccupiedBy.Piece = from.OccupiedBy.Piece;
                to.OccupiedBy.PieceColor = from.OccupiedBy.PieceColor;

                //king tracking
                if (to.OccupiedBy.Piece == PieceOptions.King)
                {
                    if (to.OccupiedBy.PieceColor == "white")
                    {
                        whiteKing = to;
                    }
                    else
                    {
                        blackKing = to;
                    }
                }

                //clear current cell
                from.Occupied = false;
                from.OccupiedBy.Piece = PieceOptions.Empty;
                from.OccupiedBy.PieceColor = "";

            }
            //register move
            else if (to.LegalMove & !to.Occupied)
            {
                //create the move
                PlayerMove pm = new PlayerMove();
                pm.GameId = currentGame.Id;
                pm.PlayerId = p.Id;
                pm.FromFile = from.File;
                pm.FromRank = from.Rank;
                pm.ToRank = to.Rank;
                pm.ToFile = to.File;
                pm.PieceName = from.OccupiedBy.Piece.ToString();
                cgem.PlayerMoves.Add(pm);

                //king tracking
                if (to.OccupiedBy.Piece == PieceOptions.King)
                {
                    if (to.OccupiedBy.PieceColor == "white")
                    {
                        whiteKing = to;
                    }
                    else
                    {
                        blackKing = to;
                    }
                }

                //assign the to cell
                to.Occupied = true;
                to.OccupiedBy.Piece = from.OccupiedBy.Piece;
                to.OccupiedBy.PieceColor = from.OccupiedBy.PieceColor;

                //clear current cell
                from.Occupied = false;
                from.OccupiedBy.Piece = PieceOptions.Empty;
                from.OccupiedBy.PieceColor = "";
            }

            //save to db
            cgem.SaveChanges();
            //set text for board cells
            SetCellText(from.File, from.Rank);
            SetCellText(to.File, to.Rank);
            SwitchTurn();
            BindDataList();
            BindDataListPlayerTake();
            playerMoveViewSource.View.Refresh();
            playerTakeViewSource.View.Refresh();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            playerMoveViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("playerMoveViewSource")));
            playerViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("playerViewSource")));
            playerTakeViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("playerTakeViewSource")));
            //playerTakeViewSource.Source = cgem.PlayerTakes.Local;

            playerViewSource.Source = cgem.Players.Local;
            cgem.Players.Load();

            cmbPlayer1.ItemsSource = cgem.Players.Local;
            cmbPlayer1.DisplayMemberPath = "PlayerName";
            cmbPlayer1.SelectedValuePath = "Id";

            cmbPlayer2.ItemsSource = cgem.Players.Local;
            cmbPlayer2.DisplayMemberPath = "PlayerName";
            cmbPlayer2.SelectedValuePath = "Id";
        }

        //ends game declaring winner
        private void btnResign_Click(object sender, RoutedEventArgs e)
        {
            EndGame();
        }

        private void EndGame()
        {
            if (currentTurn == p1.PlayerName)
            {
                p2.PlayerWins++;
                currentGame.WinnerId = p2.Id;
                var vp = cgem.Players.FirstOrDefault(p => p.Id == p2.Id);
                vp.PlayerWins = p2.PlayerWins;
                lbP1CurrentTurn.Visibility = Visibility.Hidden;
            }
            else
            {
                p1.PlayerWins++;
                currentGame.WinnerId = p1.Id;
                var vp = cgem.Players.FirstOrDefault(p => p.Id == p1.Id);
                vp.PlayerWins = p1.PlayerWins;
                lbP2CurrentTurn.Visibility = Visibility.Hidden;
            }

            cmbPlayer1.IsEnabled = true;
            cmbPlayer2.IsEnabled = true;
            currentTurn = "";

            var endgame = cgem.Games.FirstOrDefault(g => g.Id == currentGame.Id);
            endgame.WinnerId = currentGame.WinnerId;



            cgem.SaveChanges();

            currentGame = null;
            //disable chess board
            boardGrid.IsEnabled = false;
            btnResign.IsEnabled = false;
            bntStart.IsEnabled = true;
        }

        //starts the game, disables combo boxes
        private void bntStart_Click(object sender, RoutedEventArgs e)
        {
            ResetGame();
            p2 = new Player();
            p1 = new Player();
            currentGame = new Game();
            p1 = (Player)cmbPlayer1.SelectedItem;
            p2 = (Player)cmbPlayer2.SelectedItem;
            if (p1 == null || p2 == null)
            {
                MessageBox.Show("Please select both players!");
                return;
            }

            if (p1.Id == p2.Id)
            {
                MessageBox.Show("You need an opponent to start the game!");
                return;
            }

            currentGame.Player1Id = p1.Id;
            currentGame.Player2Id = p2.Id;
            currentGame.Player1Color = "white";
            currentGame.Player2Color = "black";
            p1.LastPlayedColor = "white";
            p2.LastPlayedColor = "black";

            //add game to db, update player color fields
            cgem.Games.Add(currentGame);
            var editedplayer1 = cgem.Players.FirstOrDefault(pl => pl.Id == p1.Id);
            editedplayer1.LastPlayedColor = p1.LastPlayedColor;
            var editedplayer2 = cgem.Players.FirstOrDefault(pl => pl.Id == p2.Id);
            editedplayer2.LastPlayedColor = p2.LastPlayedColor;
            cgem.SaveChanges();

            currentTurn = p1.PlayerName;
            currentColor = p1.LastPlayedColor;
            lbP1CurrentTurn.Visibility = Visibility.Visible;

            cmbPlayer1.IsEnabled = false;
            cmbPlayer2.IsEnabled = false;

            //enable chess board
            boardGrid.IsEnabled = true;
            btnResign.IsEnabled = true;
            bntStart.IsEnabled = false;
        }

        private void SwitchTurn()
        {
            if (currentTurn == p1.PlayerName)
            {
                currentTurn = p2.PlayerName;
                lbP2CurrentTurn.Visibility = Visibility.Visible;
                lbP1CurrentTurn.Visibility = Visibility.Hidden;
                currentColor = p2.LastPlayedColor;
            }
            else if (currentTurn == p2.PlayerName)
            {
                currentTurn = p1.PlayerName;
                lbP1CurrentTurn.Visibility = Visibility.Visible;
                lbP2CurrentTurn.Visibility = Visibility.Hidden;
                currentColor = p1.LastPlayedColor;
            }
            else
            {
                lbP1CurrentTurn.Visibility = Visibility.Hidden;
                lbP2CurrentTurn.Visibility = Visibility.Hidden;
            }
        }

        private Boolean IsPieceControlAllowed(Cell c)
        {
            if (currentColor == c.OccupiedBy.PieceColor)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void BindDataList()
        {
            var queryPlayerMoves = from pm in cgem.PlayerMoves where pm.GameId == currentGame.Id
                                   join p in cgem.Players on pm.PlayerId equals p.Id
                                   join g in cgem.Games on pm.GameId equals g.Id
                                   select new { pm.PlayerId, pm.FromFile, pm.FromRank, pm.ToFile, pm.ToRank, p.PlayerName, pm.PieceName};

           playerMoveViewSource.Source = queryPlayerMoves.ToList();
        }
        private void BindDataListPlayerTake()
        {
            var queryPlayerTakes = from pt in cgem.PlayerTakes
                                   where pt.GameId == currentGame.Id
                                   join p in cgem.Players on pt.TakerId equals p.Id
                                   join pl in cgem.Players on pt.TakenId equals pl.Id
                                   join g in cgem.Games on pt.GameId equals g.Id
                                   select new { takerpn = p.PlayerName, pt.PieceNameTaken, pt.PieceNameTaker, takenpn = pl.PlayerName };

            playerTakeViewSource.Source = queryPlayerTakes.ToList();
        }

        private Boolean CheckNextMove(Cell nextCell)
        {
           
            if(gameBoard.CheckCheck(nextCell, currentColor))
            {
                return false;
            }
            return true;
        }
    }
}
