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
using System.Windows.Shapes;
using Lab4.Models;

namespace Lab4.Views {
    /// <summary>
    /// Логика взаимодействия для FillGridWindow.xaml
    /// </summary>
    public partial class FillGridWindow : Window {
        private Rectangle[,] rectangles;
        private readonly int playerCount;
        private int curPlayer;

        public List<WaterGrid> Grids;

        private bool isSelected;
        private bool isDelete;
        private int selectedX;
        private int selectedY;

        private int typeShipSelected;
        private int shipDir;

        private int fourCount;
        private int threeCount;
        private int twoCount;
        private int oneCount;

        private readonly Brush empty;
        private readonly Brush avaliable;
        private readonly Brush block;
        private readonly Brush ship;
        private readonly Brush bufferShip;
        public FillGridWindow(int playerCount) {
            InitializeComponent();

            this.playerCount = playerCount;
            curPlayer = 1;
            Grids = new List<WaterGrid>();

            empty = Brushes.Gray;
            avaliable = Brushes.LimeGreen;
            block = Brushes.Tomato;
            ship = Brushes.Yellow;
            bufferShip = Brushes.Blue;

            isSelected = false;
            isDelete = false;
            selectedX = -1;
            selectedY = -1;

            typeShipSelected = -1;
            shipDir = 1;

            fourCount = 1;
            threeCount = 2;
            twoCount = 3;
            oneCount = 4;
            rectangles = new Rectangle[10,10];

            for (int i = 0; i < 10; i++) {
                for (int j = 0; j < 10; j++) {
                    rectangles[i,j] = new Rectangle {
                        Fill = empty,
                        Margin = new Thickness(2)
                    };
                    rectangles[i, j].MouseDown += Rectangle_MouseDown;
                    Grid.SetColumn(rectangles[i, j], j + 1);
                    Grid.SetRow(rectangles[i, j], i + 1);
                    waterGrid.Children.Add(rectangles[i, j]);
                }
            }
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e) {
            if ((sender as Rectangle).Fill.Equals(avaliable) && selectedX == -1) {
                for (int i = 0; i < 10; i++) {
                    for (int j = 0; j < 10; j++) {
                        if (rectangles[i,j].Equals(sender as Rectangle)) {
                            for (int k = 0; k < typeShipSelected; k++) {
                                if (shipDir == 1) {
                                    rectangles[i, j + k].Fill = bufferShip;
                                } else {
                                    rectangles[i + k, j].Fill = bufferShip;
                                }
                            }
                            selectedX = j;
                            selectedY = i;
                            return;
                        }
                    }
                }
            }

            if (isDelete && (sender as Rectangle).Fill.Equals(ship)) {
                for (int i = 0; i < 10; i++) {
                    for (int j = 0; j < 10; j++) {
                        if (rectangles[i, j].Equals(sender as Rectangle)) {
                            int len = 0;
                            int x = j-1;
                            int y = i;
                            while (x >= 0 && rectangles[y,x].Fill.Equals(ship)) {
                                rectangles[y, x--].Fill = empty;
                                len++;
                            }

                            x = j + 1;
                            y = i;
                            while (x < 10 && rectangles[y, x].Fill.Equals(ship)) {
                                rectangles[y, x++].Fill = empty;
                                len++;
                            }

                            x = j;
                            y = i-1;
                            while (y >= 0 && rectangles[y, x].Fill.Equals(ship)) {
                                rectangles[y--, x].Fill = empty;
                                len++;
                            }

                            x = j;
                            y = i + 1;
                            while (y < 10 && rectangles[y, x].Fill.Equals(ship)) {
                                rectangles[y++, x].Fill = empty;
                                len++;
                            }

                            rectangles[i, j].Fill = empty;
                            len++;

                            switch (len) {
                                case 1: oneCount++; break;
                                case 2: twoCount++; break;
                                case 3: threeCount++; break;
                                case 4: fourCount++; break;
                                default:
                                    throw new InvalidOperationException();
                            }

                            isDelete = false;
                            deleteButton.Content = "Удалить";
                            if (fourCount > 0) fourButton.IsEnabled = true;
                            else fourButton.IsEnabled = false;

                            if (threeCount > 0) threeButton.IsEnabled = true;
                            else threeButton.IsEnabled = false;

                            if (twoCount > 0) twoButton.IsEnabled = true;
                            else twoButton.IsEnabled = false;

                            if (oneCount > 0) oneButton.IsEnabled = true;
                            else oneButton.IsEnabled = false;

                            UpdateControlButtons();

                            return;
                        }
                    }
                }
            }
        }

        private void UpdateGrid() {

            if (!isSelected) {
                for (int i = 0; i < 10; i++) {
                    for (int j = 0; j < 10; j++) {
                        if (!rectangles[i, j].Fill.Equals(ship)) {
                            rectangles[i, j].Fill = empty;
                        }
                    }
                }
                return;
            }

            for (int i = 0; i < 10; i++) {
                for (int j = 0; j < 10; j++) {
                    if (!rectangles[i,j].Fill.Equals(ship)) {

                        if (shipDir == 1) {
                            if (j + typeShipSelected > 10) {
                                rectangles[i, j].Fill = block;
                            } else {
                                bool f = true;
                                for (int k = 0; k < typeShipSelected; k++) {
                                    if (!CheckAvailable(i, j+k)) {
                                        f = false;
                                        break;
                                    }
                                }
                                if (f) {
                                    rectangles[i, j].Fill = avaliable;
                                } else {
                                    rectangles[i, j].Fill = block;
                                }
                            }
                        } else {
                            if (i + typeShipSelected > 10) {
                                rectangles[i, j].Fill = block;
                            } else {
                                bool f = true;
                                for (int k = 0; k < typeShipSelected; k++) {
                                    if (!CheckAvailable(i+k, j)) {
                                        f = false;
                                        break;
                                    }
                                }
                                if (f) {
                                    rectangles[i, j].Fill = avaliable;
                                } else {
                                    rectangles[i, j].Fill = block;
                                }
                            }
                        }
                    }
                }
            }
        }

        private bool CheckAvailable(int i, int j) {
            if (i != 9 && rectangles[i + 1, j].Fill.Equals(ship)) {
                return false;
            } else if (i != 9 && j != 9 && rectangles[i + 1, j + 1].Fill.Equals(ship)) {
                return false;
            } else if (i != 0 && j != 9 && rectangles[i - 1, j + 1].Fill.Equals(ship)) {
                return false;
            } else if (i != 0 && j != 0 && rectangles[i - 1, j - 1].Fill.Equals(ship)) {
                return false;
            } else if (i != 9 && j != 0 && rectangles[i + 1, j - 1].Fill.Equals(ship)) {
                return false;
            } else if (i != 0 && rectangles[i - 1, j].Fill.Equals(ship)) {
                return false;
            } else if (j != 0 && rectangles[i, j - 1].Fill.Equals(ship)) {
                return false;
            } else if (j != 9 && rectangles[i, j + 1].Fill.Equals(ship)) {
                return false;
            } else {
                return true;
            }
        }

        private void UpdateControlButtons() {
            rotateLeftButton.IsEnabled = isSelected;
            rotateRightButton.IsEnabled = isSelected;
            addButton.IsEnabled = isSelected;
            deleteButton.IsEnabled = !isSelected;
            clearButton.IsEnabled = !isSelected;

            if (isSelected) {
                shipStatus.Text = "Расположите корабль на доступное место";
            } else {
                shipStatus.Text = "Выберите доступный корабль";
            }

            if (fourCount == 0 && threeCount == 0 && twoCount == 0 && oneCount == 0) {
                doneButton.IsEnabled = true;
            } else {
                doneButton.IsEnabled = false;
            }
        }

        private void FourButton_Click(object sender, RoutedEventArgs e) {
            isSelected = !isSelected;

            if (isSelected) {
                threeButton.IsEnabled = false;
                twoButton.IsEnabled = false;
                oneButton.IsEnabled = false;
                typeShipSelected = 4;
            } else {
                if (threeCount > 0) threeButton.IsEnabled = true;
                else threeButton.IsEnabled = false;

                if (twoCount > 0) twoButton.IsEnabled = true;
                else twoButton.IsEnabled = false;

                if (oneCount > 0) oneButton.IsEnabled = true;
                else oneButton.IsEnabled = false;
                typeShipSelected = -1;
            }

            if (selectedX != -1 || selectedY != -1) {
                selectedX = -1;
                selectedY = -1;
            }

            UpdateGrid();
            UpdateControlButtons();
        }

        private void ThreeButton_Click(object sender, RoutedEventArgs e) {
            isSelected = !isSelected;

            if (isSelected) {
                typeShipSelected = 3;
                fourButton.IsEnabled = false;
                twoButton.IsEnabled = false;
                oneButton.IsEnabled = false;
            } else {
                if (fourCount > 0) fourButton.IsEnabled = true;
                else fourButton.IsEnabled = false;

                if (twoCount > 0) twoButton.IsEnabled = true;
                else twoButton.IsEnabled = false;

                if (oneCount > 0) oneButton.IsEnabled = true;
                else oneButton.IsEnabled = false;
                typeShipSelected = -1;
            }

            if (selectedX != -1 || selectedY != -1) {
                selectedX = -1;
                selectedY = -1;
            }

            UpdateGrid();
            UpdateControlButtons();
        }

        private void TwoButton_Click(object sender, RoutedEventArgs e) {
            isSelected = !isSelected;

            if (isSelected) {
                fourButton.IsEnabled = false;
                threeButton.IsEnabled = false;
                oneButton.IsEnabled = false;
                typeShipSelected = 2;
            } else {
                if (fourCount > 0) fourButton.IsEnabled = true;
                else fourButton.IsEnabled = false;

                if (threeCount > 0) threeButton.IsEnabled = true;
                else threeButton.IsEnabled = false;

                if (oneCount > 0) oneButton.IsEnabled = true;
                else oneButton.IsEnabled = false;
                typeShipSelected = -1;
            }

            if (selectedX != -1 || selectedY != -1) {
                selectedX = -1;
                selectedY = -1;
            }

            UpdateGrid();
            UpdateControlButtons();
        }

        private void OneButton_Click(object sender, RoutedEventArgs e) {
            isSelected = !isSelected;

            if (isSelected) {
                fourButton.IsEnabled = false;
                threeButton.IsEnabled = false;
                twoButton.IsEnabled = false;
                typeShipSelected = 1;
            } else {
                if (fourCount > 0) fourButton.IsEnabled = true;
                else fourButton.IsEnabled = false;

                if (threeCount > 0) threeButton.IsEnabled = true;
                else threeButton.IsEnabled = false;

                if (twoCount > 0) twoButton.IsEnabled = true;
                else twoButton.IsEnabled = false;
                typeShipSelected = -1;
            }

            if (selectedX != -1 || selectedY != -1) {
                selectedX = -1;
                selectedY = -1;
            }

            UpdateGrid();
            UpdateControlButtons();
        }

        private void RotateButton_Click(object sender, RoutedEventArgs e) {
            if (shipDir == 1) shipDir = 2;
            else shipDir = 1;
            UpdateGrid();
            selectedX = -1;
            selectedY = -1;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e) {
            if (selectedX != -1) {
                for (int i = 0; i < typeShipSelected; i++) {
                    if (shipDir == 1) {
                        rectangles[selectedY, selectedX + i].Fill = ship;
                    } else {
                        rectangles[selectedY + i, selectedX].Fill = ship;
                    }
                }

                switch (typeShipSelected) {
                    case 1: oneCount--; break;
                    case 2: twoCount--; break;
                    case 3: threeCount--; break;
                    case 4: fourCount--; break;
                }

                selectedX = -1;
                selectedY = -1;
                typeShipSelected = -1;
                isSelected = false;
                UpdateGrid();
                UpdateControlButtons();
                if (fourCount > 0) fourButton.IsEnabled = true;
                else fourButton.IsEnabled = false;

                if (threeCount > 0) threeButton.IsEnabled = true;
                else threeButton.IsEnabled = false;

                if (twoCount > 0) twoButton.IsEnabled = true;
                else twoButton.IsEnabled = false;

                if (oneCount > 0) oneButton.IsEnabled = true;
                else oneButton.IsEnabled = false;
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e) {
            fourCount = 1;
            threeCount = 2;
            twoCount = 3;
            oneCount = 4;

            for (int i =0; i < 10; i++) {
                for (int j = 0; j < 10; j++) {
                    rectangles[i, j].Fill = empty;
                }
            }

            UpdateGrid();
            if (fourCount > 0) fourButton.IsEnabled = true;
            else fourButton.IsEnabled = false;

            if (threeCount > 0) threeButton.IsEnabled = true;
            else threeButton.IsEnabled = false;

            if (twoCount > 0) twoButton.IsEnabled = true;
            else twoButton.IsEnabled = false;

            if (oneCount > 0) oneButton.IsEnabled = true;
            else oneButton.IsEnabled = false;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e) {
            if (isDelete) {
                isDelete = false;
                deleteButton.Content = "Удалить";
                if (fourCount > 0) fourButton.IsEnabled = true;
                else fourButton.IsEnabled = false;

                if (threeCount > 0) threeButton.IsEnabled = true;
                else threeButton.IsEnabled = false;

                if (twoCount > 0) twoButton.IsEnabled = true;
                else twoButton.IsEnabled = false;

                if (oneCount > 0) oneButton.IsEnabled = true;
                else oneButton.IsEnabled = false;

                UpdateControlButtons();
                return;
            }

            isDelete = true;
            fourButton.IsEnabled = false;
            threeButton.IsEnabled = false;
            twoButton.IsEnabled = false;
            oneButton.IsEnabled = false;
            clearButton.IsEnabled = false;
            doneButton.IsEnabled = false;
            deleteButton.Content = "Отмена";
        }

        private void DoneButton_Click(object sender, RoutedEventArgs e) {
            int[] g = new int[10 * 10];

            for (int i =0; i < 10; i++) {
                for (int j = 0; j < 10; j++) {
                    if (rectangles[i,j].Fill.Equals(ship)) {
                        g[i * 10 + j] = 1;
                    } else {
                        g[i * 10 + j] = 0;
                    }
                }
            }

            Grids.Add(new WaterGrid(g));
            curPlayer++;

            fourCount = 1;
            threeCount = 2;
            twoCount = 3;
            oneCount = 4;

            for (int i = 0; i < 10; i++) {
                for (int j = 0; j < 10; j++) {
                    rectangles[i, j].Fill = empty;
                }
            }

            UpdateGrid();
            if (fourCount > 0) fourButton.IsEnabled = true;
            else fourButton.IsEnabled = false;

            if (threeCount > 0) threeButton.IsEnabled = true;
            else threeButton.IsEnabled = false;

            if (twoCount > 0) twoButton.IsEnabled = true;
            else twoButton.IsEnabled = false;

            if (oneCount > 0) oneButton.IsEnabled = true;
            else oneButton.IsEnabled = false;

            playerText.Text = "Заполнение поля Игрока 2";

            if (curPlayer != playerCount) {
                if (playerCount == 2) {
                    Window.GetWindow(this).DialogResult = true;
                    Window.GetWindow(this).Close();
                } else {
                    Grids.Add(new WaterGrid());
                    Window.GetWindow(this).DialogResult = true;
                    Window.GetWindow(this).Close();
                }
            }
        }
    }
}
