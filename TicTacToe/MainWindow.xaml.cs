using System;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Private members
        

        private MarkType[] mResults;

        private bool mPlayer1Turn;

        private bool mGameEnded;

        #endregion

        #region Constructor
        public MainWindow()
        {
            InitializeComponent();
            NewGame();
        }
        #endregion

        private void NewGame()
        {
            
            mResults = new MarkType[9];

            for(var i = 0; i < mResults.Length; i++ )
            {
                mResults[i] = MarkType.Free;
            }

            mPlayer1Turn = true;

            // iterate every button on the grid
            Container.Children.Cast<Button>().ToList().ForEach(button =>
            {
                button.Content = string.Empty;
                button.Background = Brushes.White;
                button.Foreground = Brushes.Blue;
                
            });

            mGameEnded = false;

        }

        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(mGameEnded)
            {
                NewGame();
                return;
            }

            // cast the sender to a button
            var button = (Button)sender;

            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);

            var index = column + (row * 3);

            // dont do anything if the cell is not free
             if (mResults[index] != MarkType.Free) { return; }

            //set the cell value based on which player turn it is
            //**mResults[index] = mPlayer1Turn ? MarkType.Cross : MarkType.Nought;

            mResults[index] =MarkType.Cross;

            //**button.Content = mPlayer1Turn ? "X" : "O";

            button.Content = "X";

            //**if (!mPlayer1Turn) { button.Foreground = Brushes.Red; }
            // To flip bool value based i.e if true becomes false
            //**mPlayer1Turn ^= true;

            mPlayer1Turn = false;

            // Check for a winner
            CheckWinner();

            if (mGameEnded)
            {
                NewGame();
                return;
            }

            // Ai turn to play
            TacBot();

            // Check for a winner
            CheckWinner();

        }

        private void TacBot()
        {

            if (CheckHorizontalWinCom()) { return; }

            if (CheckVerticalWinCom()) { return; }

            if (CheckDiagonalWinCom()) { return; }

            if (CheckVerticalWinPly()) { return; }

            if (CheckHorizontalWinPly()) { return; }

            if (CheckDiagonalWinPly()) { return; }

            if (CheckLogicDiagonal()) { return; }

            if (CheckLogicConered()) { return; }

            if (CheckLogickasie()) { return; }

            if (CheckCentre()) { return; }

            if (CheckConers()) { return; }

            if (CheckSides()) { return; }
                       
           
        }

        private bool CheckLogickasie()
        {
            if ((mResults[0] == MarkType.Cross && mResults[7] == MarkType.Cross) &&
               (mResults[1] == MarkType.Free && mResults[2] == MarkType.Free &&
                mResults[3] == MarkType.Free && mResults[4] == MarkType.Nought &&
                mResults[5] == MarkType.Free && mResults[6] == MarkType.Free &&
                mResults[8] == MarkType.Free))
            {

                // play at buttom left

                Button0_2.Foreground = Brushes.Red;

                Button0_2.Content = "O";
                mResults[6] = MarkType.Nought;

                mPlayer1Turn = true;

                
                return true;
            }

            else if ((mResults[2] == MarkType.Cross && mResults[7] == MarkType.Cross) &&
               (mResults[8] == MarkType.Free && mResults[0] == MarkType.Free &&
                mResults[3] == MarkType.Free && mResults[4] == MarkType.Nought &&
                mResults[5] == MarkType.Free && mResults[1] == MarkType.Free &&
                mResults[6] == MarkType.Free))
            {
                // play at buttom right
                Button2_2.Foreground = Brushes.Red;

                Button2_2.Content = "O";
                mResults[8] = MarkType.Nought;

                mPlayer1Turn = true;
                                
                return true;
            }

            else if ((mResults[5] == MarkType.Cross && mResults[6] == MarkType.Cross) &&
              (mResults[8] == MarkType.Free && mResults[0] == MarkType.Free &&
               mResults[3] == MarkType.Free && mResults[4] == MarkType.Nought &&
               mResults[2] == MarkType.Free && mResults[1] == MarkType.Free &&
               mResults[7] == MarkType.Free))
            {
                // play at buttom right
                Button2_2.Foreground = Brushes.Red;

                Button2_2.Content = "O";
                mResults[8] = MarkType.Nought;

                mPlayer1Turn = true;

                return true;
            }
            return false;
        }

        private bool CheckLogicDiagonal()
        {
            if ((mResults[0] == MarkType.Cross && mResults[8] == MarkType.Cross) &&
               ( mResults[1] == MarkType.Free && mResults[2] == MarkType.Free &&
                mResults[3] == MarkType.Free && mResults[4] == MarkType.Nought &&
                mResults[5] == MarkType.Free && mResults[6] == MarkType.Free &&
                mResults[7] == MarkType.Free) )
            {
                CheckSides();
                return true;
            }

            else if ((mResults[2] == MarkType.Cross && mResults[6] == MarkType.Cross) &&
               (mResults[8] == MarkType.Free && mResults[0] == MarkType.Free &&
                mResults[3] == MarkType.Free && mResults[4] == MarkType.Nought &&
                mResults[5] == MarkType.Free && mResults[1] == MarkType.Free &&
                mResults[7] == MarkType.Free))
            {
                CheckSides();
                return true;
            }

            return false;
        }

        private bool CheckLogicConered()
        {
            if ((mResults[5] == MarkType.Cross && mResults[7] == MarkType.Cross) &&
               (mResults[1] == MarkType.Free && mResults[2] == MarkType.Free &&
                mResults[3] == MarkType.Free && mResults[4] == MarkType.Nought &&
                mResults[0] == MarkType.Free && mResults[6] == MarkType.Free &&
                mResults[8] == MarkType.Free))
            {

                //play at buttom right
                    Button2_2.Foreground = Brushes.Red;

                    Button2_2.Content = "O";
                    mResults[8] = MarkType.Nought;

                    mPlayer1Turn = true;

                    return true;
            }

            else if ((mResults[3] == MarkType.Cross && mResults[7] == MarkType.Cross) &&
               (mResults[8] == MarkType.Free && mResults[0] == MarkType.Free &&
                mResults[2] == MarkType.Free && mResults[4] == MarkType.Nought &&
                mResults[5] == MarkType.Free && mResults[1] == MarkType.Free &&
                mResults[6] == MarkType.Free))
            {

                // play at button left
                Button0_2.Foreground = Brushes.Red;

                Button0_2.Content = "O";
                mResults[6] = MarkType.Nought;

                mPlayer1Turn = true;

                return true;
            }

            else if ((mResults[1] == MarkType.Cross && mResults[3] == MarkType.Cross) &&
               (mResults[8] == MarkType.Free && mResults[0] == MarkType.Free &&
                mResults[2] == MarkType.Free && mResults[4] == MarkType.Nought &&
                mResults[5] == MarkType.Free && mResults[7] == MarkType.Free &&
                mResults[6] == MarkType.Free))
            {
                // play at top left
                Button0_0.Foreground = Brushes.Red;

                Button0_0.Content = "O";
                mResults[0] = MarkType.Nought;

                mPlayer1Turn = true;

                
                return true;
            }

            else if ((mResults[1] == MarkType.Cross && mResults[5] == MarkType.Cross) &&
              (mResults[8] == MarkType.Free && mResults[0] == MarkType.Free &&
               mResults[2] == MarkType.Free && mResults[4] == MarkType.Nought &&
               mResults[7] == MarkType.Free && mResults[3] == MarkType.Free &&
               mResults[6] == MarkType.Free))
            {

                // play at top right
                Button2_0.Foreground = Brushes.Red;

                Button2_0.Content = "O";
                mResults[2] = MarkType.Nought;

                mPlayer1Turn = true;

                return true;
            }

            return false;
        }
        
        private bool CheckSides()
        {
            // Check up 
            if (mResults[1] == MarkType.Free)
            {

                Button1_0.Foreground = Brushes.Red;

                Button1_0.Content = "O";
                mResults[1] = MarkType.Nought;

                mPlayer1Turn = true;

                return true;

            }

            // Check down
            if (mResults[7] == MarkType.Free)
            {

                Button1_2.Foreground = Brushes.Red;

                Button1_2.Content = "O";
                mResults[7] = MarkType.Nought;

                mPlayer1Turn = true;

                return true;

            }

            // Check left
            if (mResults[3] == MarkType.Free)
            {

                Button0_1.Foreground = Brushes.Red;

                Button0_1.Content = "O";
                mResults[3] = MarkType.Nought;

                mPlayer1Turn = true;

                return true;

            }

            // Check right
            if (mResults[5] == MarkType.Free)
            {

                Button2_1.Foreground = Brushes.Red;

                Button2_1.Content = "O";
                mResults[5] = MarkType.Nought;

                mPlayer1Turn = true;

                return true;

            }

            return false;

        }

        private bool CheckCentre()
        {
            //play at center if free
            if (mResults[4] == MarkType.Free)
            {


                Button1_1.Foreground = Brushes.Red;

                Button1_1.Content = "O";
                mResults[4] = MarkType.Nought;

                mPlayer1Turn = true;

                return true;

            }

            return false;
        }

        private bool CheckConers()
        {
            //check top left
            if (mResults[0] == MarkType.Free)
            {


                Button0_0.Foreground = Brushes.Red;

                Button0_0.Content = "O";
                mResults[0] = MarkType.Nought;

                mPlayer1Turn = true;

                return true;

            }

            //check top right
            if (mResults[2] == MarkType.Free)
            {


                Button2_0.Foreground = Brushes.Red;

                Button2_0.Content = "O";
                mResults[2] = MarkType.Nought;

                mPlayer1Turn = true;

                return true;

            }

            //check buttom left
            if (mResults[6] == MarkType.Free)
            {


                Button0_2.Foreground = Brushes.Red;

                Button0_2.Content = "O";
                mResults[6] = MarkType.Nought;

                mPlayer1Turn = true;

                return true;

            }

            //check buttom right
            if (mResults[8] == MarkType.Free)
            {


                Button2_2.Foreground = Brushes.Red;

                Button2_2.Content = "O";
                mResults[8] = MarkType.Nought;

                mPlayer1Turn = true;

                return true;

            }

            return false;
        }
        
        private bool CheckDiagonalWinCom()
        {
            #region Diagonal Left

            // Check for Diagonal Left 0x1x1
            if ((mResults[0] == MarkType.Free && mResults[4] == MarkType.Nought && mResults[8] == MarkType.Nought))
            {
                Button0_0.Foreground = Brushes.Red;

                Button0_0.Content = "O";
                mResults[0] = MarkType.Nought;

                mPlayer1Turn = true;
                return true;
            }

            // Check for Diagonal Left 1x0x1
            if ((mResults[0] == MarkType.Nought && mResults[4] == MarkType.Free && mResults[8] == MarkType.Nought))
            {
                Button1_1.Foreground = Brushes.Red;

                Button1_1.Content = "O";
                mResults[4] = MarkType.Nought;

                mPlayer1Turn = true;
                return true;
            }


            // Check for Diagonal Left 1x1x0
            if ((mResults[0] == MarkType.Nought && mResults[4] == MarkType.Nought && mResults[8] == MarkType.Free))
            {
                Button2_2.Foreground = Brushes.Red;

                Button2_2.Content = "O";
                mResults[8] = MarkType.Nought;

                mPlayer1Turn = true;
                return true;
            }


            #endregion

            #region Diagonal Right

            // Check for Diagonal Right 0x1x1
            if ((mResults[2] == MarkType.Free && mResults[4] == MarkType.Nought && mResults[6] == MarkType.Nought))
            {
                Button2_0.Foreground = Brushes.Red;

                Button2_0.Content = "O";
                mResults[2] = MarkType.Nought;

                mPlayer1Turn = true;
                return true;
            }

            // Check for Diagonal Right 1x0x1
            if ((mResults[2] == MarkType.Nought && mResults[4] == MarkType.Free && mResults[6] == MarkType.Nought))
            {
                Button1_1.Foreground = Brushes.Red;

                Button1_1.Content = "O";
                mResults[4] = MarkType.Nought;

                mPlayer1Turn = true;
                return true;
            }

            // Check for Diagonal Right 1x1x0
            if ((mResults[2] == MarkType.Nought && mResults[4] == MarkType.Nought && mResults[6] == MarkType.Free))
            {
                Button0_2.Foreground = Brushes.Red;

                Button0_2.Content = "O";
                mResults[6] = MarkType.Nought;

                mPlayer1Turn = true;
                return true;
            }


            #endregion

            return false;
        }

        private bool CheckVerticalWinCom()
        {
            #region Vertical a
            // Check for Verticala 0/1/1
            if ((mResults[0] == MarkType.Free && mResults[3] == MarkType.Nought && mResults[6] == MarkType.Nought))
            {
                Button0_0.Foreground = Brushes.Red;

                Button0_0.Content = "O";
                mResults[0] = MarkType.Nought;

                mPlayer1Turn = true;
                return true;
            }

            // Check for Verticala 1/0/1
            if ((mResults[0] == MarkType.Nought && mResults[3] == MarkType.Free && mResults[6] == MarkType.Nought))
            {
                Button0_1.Foreground = Brushes.Red;

                Button0_1.Content = "O";
                mResults[3] = MarkType.Nought;

                mPlayer1Turn = true;
                return true;
            }

            // Check for Verticala 1/1/0
            if ((mResults[0] == MarkType.Nought && mResults[3] == MarkType.Nought && mResults[6] == MarkType.Free))
            {
                Button0_2.Foreground = Brushes.Red;

                Button0_2.Content = "O";
                mResults[6] = MarkType.Nought;

                mPlayer1Turn = true;
                return true;
            }

            #endregion

            #region Vertical b

            // Check for Verticalb 0/1/1
            if ((mResults[1] == MarkType.Free && mResults[4] == MarkType.Nought && mResults[7] == MarkType.Nought))
            {
                Button1_0.Foreground = Brushes.Red;

                Button1_0.Content = "O";
                mResults[1] = MarkType.Nought;

                mPlayer1Turn = true;
                return true;
            }

            // Check for Verticalb 1/0/1
            if ((mResults[1] == MarkType.Nought && mResults[4] == MarkType.Free && mResults[7] == MarkType.Nought))
            {
                Button1_1.Foreground = Brushes.Red;

                Button1_1.Content = "O";
                mResults[4] = MarkType.Nought;

                mPlayer1Turn = true;
                return true;
            }

            // Check for Verticalb 1/1/0
            if ((mResults[1] == MarkType.Nought && mResults[4] == MarkType.Nought && mResults[7] == MarkType.Free))
            {
                Button1_2.Foreground = Brushes.Red;

                Button1_2.Content = "O";
                mResults[7] = MarkType.Nought;

                mPlayer1Turn = true;
                return true;
            }

            #endregion

            #region Vertical c

            // Check for Verticalc 0/1/1
            if ((mResults[2] == MarkType.Free && mResults[5] == MarkType.Nought && mResults[8] == MarkType.Nought))
            {
                Button2_0.Foreground = Brushes.Red;

                Button2_0.Content = "O";
                mResults[2] = MarkType.Nought;

                mPlayer1Turn = true;
                return true;
            }

            // Check for Verticalc 1/0/1
            if ((mResults[2] == MarkType.Nought && mResults[5] == MarkType.Free && mResults[8] == MarkType.Nought))
            {
                Button2_1.Foreground = Brushes.Red;

                Button2_1.Content = "O";
                mResults[5] = MarkType.Nought;

                mPlayer1Turn = true;
                return true;
            }

            // Check for Verticalc 1/1/0
            if ((mResults[2] == MarkType.Nought && mResults[5] == MarkType.Nought && mResults[8] == MarkType.Free))
            {
                Button2_2.Foreground = Brushes.Red;

                Button2_2.Content = "O";
                mResults[8] = MarkType.Nought;

                mPlayer1Turn = true;
                return true;
            }

            #endregion

            return false;
        }

        private bool CheckHorizontalWinCom()
        {

            #region Horizontal a
            // Check for horizontala 0-1-1 win
            if (mResults[0] == MarkType.Free && mResults[1] == MarkType.Nought && mResults[2] == MarkType.Nought)
            {

                Button0_0.Foreground = Brushes.Red;

                Button0_0.Content = "O";
                mResults[0] = MarkType.Nought;

                mPlayer1Turn = true;
                return true;

            }

            // Check for horizontala 1-0-1 win
            if (mResults[0] == MarkType.Nought && mResults[1] == MarkType.Free && mResults[2] == MarkType.Nought)
            {

                Button1_0.Foreground = Brushes.Red;

                Button1_0.Content = "O";
                mResults[1] = MarkType.Nought;

                mPlayer1Turn = true;
                return true;

            }

            // Check for horizontala 1-1-0 win
            if (mResults[0] == MarkType.Nought && mResults[1] == MarkType.Nought && mResults[2] == MarkType.Free)
            {

                Button2_0.Foreground = Brushes.Red;

                Button2_0.Content = "O";
                mResults[2] = MarkType.Nought;

                mPlayer1Turn = true;
                return true;

            }

            #endregion

            #region Horizontal b
            // Check for horizontalb 0-1-1 win
            if (mResults[3] == MarkType.Free && mResults[4] == MarkType.Nought && mResults[5] == MarkType.Nought)
            {

                Button0_1.Foreground = Brushes.Red;

                Button0_1.Content = "O";
                mResults[3] = MarkType.Nought;

                mPlayer1Turn = true;
                return true;

            }

            // Check for horizontalb 1-0-1 win
            if (mResults[3] == MarkType.Nought && mResults[4] == MarkType.Free && mResults[5] == MarkType.Nought)
            {

                Button1_1.Foreground = Brushes.Red;

                Button1_1.Content = "O";
                mResults[4] = MarkType.Nought;

                mPlayer1Turn = true;
                return true;

            }

            // Check for horizontalb 1-1-0 win
            if (mResults[3] == MarkType.Nought && mResults[4] == MarkType.Nought && mResults[5] == MarkType.Free)
            {

                Button2_1.Foreground = Brushes.Red;

                Button2_1.Content = "O";
                mResults[5] = MarkType.Nought;

                mPlayer1Turn = true;
                return true;

            }

            #endregion

            #region Horizontal c

            // Check for horizontalc 0-1-1 win
            if (mResults[6] == MarkType.Free && mResults[7] == MarkType.Nought && mResults[8] == MarkType.Nought)
            {

                Button0_2.Foreground = Brushes.Red;

                Button0_2.Content = "O";
                mResults[6] = MarkType.Nought;

                mPlayer1Turn = true;
                return true;

            }

            // Check for horizontalc 1-0-1 win
            if (mResults[6] == MarkType.Nought && mResults[7] == MarkType.Free && mResults[8] == MarkType.Nought)
            {

                Button1_2.Foreground = Brushes.Red;

                Button1_2.Content = "O";
                mResults[7] = MarkType.Nought;

                mPlayer1Turn = true;
                return true;

            }

            // Check for horizontalc 1-1-0 win
            if (mResults[6] == MarkType.Nought && mResults[7] == MarkType.Nought && mResults[8] == MarkType.Free)
            {

                Button2_2.Foreground = Brushes.Red;

                Button2_2.Content = "O";
                mResults[8] = MarkType.Nought;

                mPlayer1Turn = true;
                return true;

            }
            #endregion

            return false;

        }



        private bool CheckDiagonalWinPly()
        {
            #region Diagonal Left

            // Check for Diagonal Left 0x1x1
            if ((mResults[0] == MarkType.Free && mResults[4] == MarkType.Cross && mResults[8] == MarkType.Cross))
            {
                Button0_0.Foreground = Brushes.Red;

                Button0_0.Content = "O";
                mResults[0] = MarkType.Nought;

                mPlayer1Turn = true;
                return true;
            }

            // Check for Diagonal Left 1x0x1
            if ((mResults[0] == MarkType.Cross && mResults[4] == MarkType.Free && mResults[8] == MarkType.Cross))
            {
                Button1_1.Foreground = Brushes.Red;

                Button1_1.Content = "O";
                mResults[4] = MarkType.Nought;

                mPlayer1Turn = true;
                return true;
            }


            // Check for Diagonal Left 1x1x0
            if ((mResults[0] == MarkType.Cross && mResults[4] == MarkType.Cross && mResults[8] == MarkType.Free))
            {
                Button2_2.Foreground = Brushes.Red;

                Button2_2.Content = "O";
                mResults[8] = MarkType.Nought;

                mPlayer1Turn = true;
                return true;
            }


            #endregion

            #region Diagonal Right

            // Check for Diagonal Right 0x1x1
            if ((mResults[2] == MarkType.Free && mResults[4] == MarkType.Cross && mResults[6] == MarkType.Cross))
            {
                Button2_0.Foreground = Brushes.Red;

                Button2_0.Content = "O";
                mResults[2] = MarkType.Nought;

                mPlayer1Turn = true;
                return true;
            }

            // Check for Diagonal Right 1x0x1
            if ((mResults[2] == MarkType.Cross && mResults[4] == MarkType.Free && mResults[6] == MarkType.Cross))
            {
                Button1_1.Foreground = Brushes.Red;

                Button1_1.Content = "O";
                mResults[4] = MarkType.Nought;

                mPlayer1Turn = true;
                return true;
            }

            // Check for Diagonal Right 1x1x0
            if ((mResults[2] == MarkType.Cross && mResults[4] == MarkType.Cross && mResults[6] == MarkType.Free))
            {
                Button0_2.Foreground = Brushes.Red;

                Button0_2.Content = "O";
                mResults[6] = MarkType.Nought;

                mPlayer1Turn = true;
                return true;
            }


            #endregion

            return false;
        }

        private bool CheckVerticalWinPly()
        {
            #region Vertical a
            // Check for Verticala 0/1/1
            if ((mResults[0] == MarkType.Free && mResults[3] == MarkType.Cross && mResults[6] == MarkType.Cross))
            {
                Button0_0.Foreground = Brushes.Red;

                Button0_0.Content = "O";
                mResults[0] = MarkType.Nought;

                mPlayer1Turn = true;
                return true;
            }

            // Check for Verticala 1/0/1
            if ((mResults[0] == MarkType.Cross && mResults[3] == MarkType.Free && mResults[6] == MarkType.Cross))
            {
                Button0_1.Foreground = Brushes.Red;

                Button0_1.Content = "O";
                mResults[3] = MarkType.Nought;

                mPlayer1Turn = true;
                return true;
            }

            // Check for Verticala 1/1/0
            if ((mResults[0] == MarkType.Cross && mResults[3] == MarkType.Cross && mResults[6] == MarkType.Free))
            {
                Button0_2.Foreground = Brushes.Red;

                Button0_2.Content = "O";
                mResults[6] = MarkType.Nought;

                mPlayer1Turn = true;
                return true;
            }

            #endregion

            #region Vertical b

            // Check for Verticalb 0/1/1
            if ((mResults[1] == MarkType.Free && mResults[4] == MarkType.Cross && mResults[7] == MarkType.Cross))
            {
                Button1_0.Foreground = Brushes.Red;

                Button1_0.Content = "O";
                mResults[1] = MarkType.Nought;

                mPlayer1Turn = true;
                return true;
            }

            // Check for Verticalb 1/0/1
            if ((mResults[1] == MarkType.Cross && mResults[4] == MarkType.Free && mResults[7] == MarkType.Cross))
            {
                Button1_1.Foreground = Brushes.Red;

                Button1_1.Content = "O";
                mResults[4] = MarkType.Nought;

                mPlayer1Turn = true;
                return true;
            }

            // Check for Verticalb 1/1/0
            if ((mResults[1] == MarkType.Cross && mResults[4] == MarkType.Cross && mResults[7] == MarkType.Free))
            {
                Button1_2.Foreground = Brushes.Red;

                Button1_2.Content = "O";
                mResults[7] = MarkType.Nought;

                mPlayer1Turn = true;
                return true;
            }

            #endregion

            #region Vertical c

            // Check for Verticalc 0/1/1
            if ((mResults[2] == MarkType.Free && mResults[5] == MarkType.Cross && mResults[8] == MarkType.Cross))
            {
                Button2_0.Foreground = Brushes.Red;

                Button2_0.Content = "O";
                mResults[2] = MarkType.Nought;

                mPlayer1Turn = true;
                return true;
            }

            // Check for Verticalc 1/0/1
            if ((mResults[2] == MarkType.Cross && mResults[5] == MarkType.Free && mResults[8] == MarkType.Cross))
            {
                Button2_1.Foreground = Brushes.Red;

                Button2_1.Content = "O";
                mResults[5] = MarkType.Nought;

                mPlayer1Turn = true;
                return true;
            }

            // Check for Verticalc 1/1/0
            if ((mResults[2] == MarkType.Cross && mResults[5] == MarkType.Cross && mResults[8] == MarkType.Free))
            {
                Button2_2.Foreground = Brushes.Red;

                Button2_2.Content = "O";
                mResults[8] = MarkType.Nought;

                mPlayer1Turn = true;
                return true;
            }

            #endregion

            return false;
        }

        private bool CheckHorizontalWinPly()
        {

            #region Horizontal a
            // Check for horizontala 0-1-1 win
            if (mResults[0] == MarkType.Free && mResults[1] == MarkType.Cross && mResults[2] == MarkType.Cross)
            {

                Button0_0.Foreground = Brushes.Red;

                Button0_0.Content = "O";
                mResults[0] = MarkType.Nought;

                mPlayer1Turn = true;
                return true;

            }

            // Check for horizontala 1-0-1 win
            if (mResults[0] == MarkType.Cross && mResults[1] == MarkType.Free && mResults[2] == MarkType.Cross)
            {

                Button1_0.Foreground = Brushes.Red;

                Button1_0.Content = "O";
                mResults[1] = MarkType.Nought;

                mPlayer1Turn = true;
                return true;

            }

            // Check for horizontala 1-1-0 win
            if (mResults[0] == MarkType.Cross && mResults[1] == MarkType.Cross && mResults[2] == MarkType.Free)
            {

                Button2_0.Foreground = Brushes.Red;

                Button2_0.Content = "O";
                mResults[2] = MarkType.Nought;

                mPlayer1Turn = true;
                return true;

            }

            #endregion

            #region Horizontal b
            // Check for horizontalb 0-1-1 win
            if (mResults[3] == MarkType.Free && mResults[4] == MarkType.Cross && mResults[5] == MarkType.Cross)
            {

                Button0_1.Foreground = Brushes.Red;

                Button0_1.Content = "O";
                mResults[3] = MarkType.Nought;

                mPlayer1Turn = true;
                return true;

            }

            // Check for horizontalb 1-0-1 win
            if (mResults[3] == MarkType.Cross && mResults[4] == MarkType.Free && mResults[5] == MarkType.Cross)
            {

                Button1_1.Foreground = Brushes.Red;

                Button1_1.Content = "O";
                mResults[4] = MarkType.Nought;

                mPlayer1Turn = true;
                return true;

            }

            // Check for horizontalb 1-1-0 win
            if (mResults[3] == MarkType.Cross && mResults[4] == MarkType.Cross && mResults[5] == MarkType.Free)
            {

                Button2_1.Foreground = Brushes.Red;

                Button2_1.Content = "O";
                mResults[5] = MarkType.Nought;

                mPlayer1Turn = true;
                return true;

            }

            #endregion

            #region Horizontal c

            // Check for horizontalc 0-1-1 win
            if (mResults[6] == MarkType.Free && mResults[7] == MarkType.Cross && mResults[8] == MarkType.Cross)
            {

                Button0_2.Foreground = Brushes.Red;

                Button0_2.Content = "O";
                mResults[6] = MarkType.Nought;

                mPlayer1Turn = true;
                return true;

            }

            // Check for horizontalc 1-0-1 win
            if (mResults[6] == MarkType.Cross && mResults[7] == MarkType.Free && mResults[8] == MarkType.Cross)
            {

                Button1_2.Foreground = Brushes.Red;

                Button1_2.Content = "O";
                mResults[7] = MarkType.Nought;

                mPlayer1Turn = true;
                return true;

            }

            // Check for horizontalc 1-1-0 win
            if (mResults[6] == MarkType.Cross && mResults[7] == MarkType.Cross && mResults[8] == MarkType.Free)
            {

                Button2_2.Foreground = Brushes.Red;

                Button2_2.Content = "O";
                mResults[8] = MarkType.Nought;

                mPlayer1Turn = true;
                return true;

            }
            #endregion

            return false;

        }

        private void CheckWinner()
        {
            #region Horizontal Wins
            // Check for horizontal wins
            // Row 0
            if (mResults[0] != MarkType.Free && ( mResults[0] & mResults[1] & mResults[2] ) == mResults[0] )
            {
                // Game ends
                mGameEnded = true;

                //Highlight Winning cells in Green
                Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.Green;

            }
            // Check for horizontal wins
            // Row 1
            if (mResults[3] != MarkType.Free && (mResults[3] & mResults[4] & mResults[5]) == mResults[3])
            {
                // Game ends
                mGameEnded = true;

                //Highlight Winning cells in Green
                Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.Green;

            }

            // Check for horizontal wins
            // Row 2
            if (mResults[6] != MarkType.Free && (mResults[6] & mResults[7] & mResults[8]) == mResults[6])
            {
                // Game ends
                mGameEnded = true;

                //Highlight Winning cells in Green
                Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.Green;

            }

            #endregion


            #region Vertical wins
            // Check for Vertical wins
            // Column 0
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[3] & mResults[6]) == mResults[0])
            {
                // Game ends
                mGameEnded = true;

                //Highlight Winning cells in Green
                Button0_0.Background = Button0_1.Background = Button0_2.Background = Brushes.Green;

            }

            // Check for Vertical wins
            // Column 1
            if (mResults[1] != MarkType.Free && (mResults[1] & mResults[4] & mResults[7]) == mResults[1])
            {
                // Game ends
                mGameEnded = true;

                //Highlight Winning cells in Green
                Button1_0.Background = Button1_1.Background = Button1_2.Background = Brushes.Green;

            }

            // Check for Vertical wins
            // Column 1
            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[5] & mResults[8]) == mResults[2])
            {
                // Game ends
                mGameEnded = true;

                //Highlight Winning cells in Green
                Button2_0.Background = Button2_1.Background = Button2_2.Background = Brushes.Green;

            }

            #endregion

            #region Diagonal wins

            // Check for Diagonal left
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[4] & mResults[8]) == mResults[0])
            {
                // Game ends
                mGameEnded = true;

                //Highlight Winning cells in Green
                Button0_0.Background = Button1_1.Background = Button2_2.Background = Brushes.Green;

            }

            // Check for Diagonal Right
            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[4] & mResults[6]) == mResults[2])
            {
                // Game ends
                mGameEnded = true;

                //Highlight Winning cells in Green
                Button2_0.Background = Button1_1.Background = Button0_2.Background = Brushes.Green;

            }

            #endregion

            #region No wins
            // if no results are free i.e f isnt free
            if (!mResults.Any(f => f == MarkType.Free))
            {
                //Game ended
                mGameEnded = true;

                // Turn all cells orange
                Container.Children.Cast<Button>().ToList().ForEach(button =>
                {
                    button.Background = Brushes.Orange;
                });
            }

            #endregion
        }
    }
}
