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
    /// Логика взаимодействия для GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window {
        private readonly WaterGrid p1;
        private readonly WaterGrid p2;
        private readonly bool isP2Bot;
        private readonly string p2Name;
        
        private int p1Score;
        private int p2Score;

        private int selectedX;
        private int selectedY;

        private readonly Brush empty;
        private readonly Brush injury;
        private readonly Brush kill;
        private readonly Brush miss;

        private int turn;

        private StringBuilder logger;

        private Rectangle[,] rectangles;

        public GameWindow(WaterGrid p1, WaterGrid p2, bool isP2Bot) {
            InitializeComponent();

            empty = Brushes.Gray;
            injury = Brushes.LightGoldenrodYellow;
            kill = Brushes.LimeGreen;
            miss = Brushes.Tomato;

            selectedX = -1;
            selectedY = -1;

            p1Score = 0;
            p2Score = 0;

            turn = 1;

            logger = new StringBuilder();

            this.p1 = p1;
            this.p2 = p2;
            this.isP2Bot = isP2Bot;
            if (isP2Bot) {
                player2Label.Text = "Бот";
                p2Name = "Бот";
            } else {
                p2Name = "Игрок 2";
            }

            rectangles = new Rectangle[10, 10];
            for (int i = 0; i < 10; i++) {
                for (int j = 0; j < 10; j++) {
                    rectangles[i, j] = new Rectangle {
                        Fill = empty,
                        Margin = new Thickness(2)
                    };
                    rectangles[i, j].MouseDown += Rectangle_MouseDown;
                    Grid.SetColumn(rectangles[i, j], j + 1);
                    Grid.SetRow(rectangles[i, j], i + 1);
                    waterGrid.Children.Add(rectangles[i, j]);
                }
            }

            logger.Append("Ход Игрок 1\n");
            logArea.Text = logger.ToString();
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e) {
            for (int i = 0; i < 10; i++) {
                for (int j = 0; j < 10; j++) {
                    if (rectangles[i, j].Equals(sender as Rectangle)) {
                        if (selectedX != -1) {
                            rectangles[selectedY, selectedX].Stroke = Brushes.Transparent;
                        }
                        selectedX = j;
                        selectedY = i;
                        UpdateGrid();
                    }
                }
            }

            turnButton.IsEnabled = true;
        }

        private void UpdateGrid() {
            for (int i =0; i < 10; i++) {
                for (int j = 0; j < 10; j++) {
                    if (turn == 1) {
                        int g = p2.GetAt(i, j);
                        if (g <= 1) {
                            rectangles[i, j].Fill = empty;
                        } else if (g == 2) {
                            rectangles[i, j].Fill = injury;
                        } else if (g == 3) {
                            rectangles[i, j].Fill = kill;
                        } else {
                            rectangles[i, j].Fill = miss;
                        }
                    } else if (turn == 2 && !isP2Bot) {
                        int g = p1.GetAt(i, j);
                        if (g <= 1) {
                            rectangles[i, j].Fill = empty;
                        } else if (g == 2) {
                            rectangles[i, j].Fill = injury;
                        } else if (g == 3) {
                            rectangles[i, j].Fill = kill;
                        } else {
                            rectangles[i, j].Fill = miss;
                        }
                    }
                }
            }

            if (selectedX != -1) {
                rectangles[selectedY, selectedX].Stroke = Brushes.Orange;
                rectangles[selectedY, selectedX].StrokeThickness = 3;
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e) {
            if (selectedX != -1)
                rectangles[selectedY, selectedX].Stroke = Brushes.Transparent;
            selectedX = -1;
            selectedY = -1;

            turnButton.IsEnabled = false;
        }

        private void TurnButton_Click(object sender, RoutedEventArgs e) {
            if (p2.GetAt(selectedY, selectedX) >= 2) return;
            if (turn == 1) {
                logger.Append($"Игрок 1: выстрел {char.ToUpper(WaterGrid.ToLetter(selectedX + 1))}{selectedY + 1}\n");
                int res = p2.Fire(selectedX, selectedY + 1);
                if (res == -1) {
                    logger.Append($"{p2Name}: Мимо\n");
                    logArea.Text = logger.ToString();
                    logScroll.ScrollToEnd();
                    UpdateGrid();
                    if (!isP2Bot) {
                        turn = 2;
                        selectedX = -1;
                        selectedY = -1;
                    } else {
                        logArea.Text = logger.ToString();
                        logScroll.ScrollToEnd();
                        UpdateGrid();
                        while (true) {
                            res = BotTurn();
                            if (res == -1) {
                                logger.Append($"Игрок 1: Мимо\n");
                                logArea.Text = logger.ToString();
                                logScroll.ScrollToEnd();
                                UpdateGrid();
                                break;
                            } else if (res == 1) {
                                logger.Append($"Игрок 1: Попал\n");
                                p2Score++;
                                scoreLabel.Text = $"{p1Score} - {p2Score}";
                                logArea.Text = logger.ToString();
                                logScroll.ScrollToEnd();
                                UpdateGrid();
                            } else {
                                logger.Append($"Игрок 1: Убил\n");
                                p2Score++;
                                scoreLabel.Text = $"{p1Score} - {p2Score}";
                                logArea.Text = logger.ToString();
                                logScroll.ScrollToEnd();
                                UpdateGrid();
                                for (int i = 0; i < 10; i++) {
                                    for (int j = 0; j < 10; j++) {
                                        if (p1.GetAt(i, j) == 1) {
                                            return;
                                        }
                                    }
                                }

                                MessageBox.Show("Бот выиграл!", "Поражение");
                            }
                        }
                    }
                } else if (res == 1) {
                    logger.Append($"{p2Name}: Попал\n");
                    p1Score++;
                    scoreLabel.Text = $"{p1Score} - {p2Score}";
                    logArea.Text = logger.ToString();
                    logScroll.ScrollToEnd();
                    UpdateGrid();
                } else {
                    logger.Append($"{p2Name}: Убил\n");
                    p1Score++;
                    scoreLabel.Text = $"{p1Score} - {p2Score}";
                    logArea.Text = logger.ToString();
                    logScroll.ScrollToEnd();
                    UpdateGrid();
                    for (int i =0; i < 10; i++) {
                        for (int j = 0; j < 10; j++) {
                            if (p2.GetAt(i,j) == 1) {
                                return;
                            }
                        }
                    }

                    MessageBox.Show("Игрок 1 выиграл!", "Победа");
                }
            } else {
                logger.Append($"Игрок 2: выстрел {char.ToUpper(WaterGrid.ToLetter(selectedX + 1))}{selectedY + 1}\n");
                int res = p1.Fire(selectedX, selectedY + 1);
                if (res == -1) {
                    logger.Append($"Игрок 1: Мимо\n");
                    turn = 1;
                    selectedX = -1;
                    selectedY = -1;
                } else if (res == 1) {
                    logger.Append($"Игрок 1: Попал\n");
                    p2Score++;
                    scoreLabel.Text = $"{p1Score} - {p2Score}";
                    logArea.Text = logger.ToString();
                    logScroll.ScrollToEnd();
                    UpdateGrid();
                } else {
                    logger.Append($"Игрок 1: Убил\n");
                    p2Score++;
                    scoreLabel.Text = $"{p1Score} - {p2Score}";
                    logArea.Text = logger.ToString();
                    logScroll.ScrollToEnd();
                    UpdateGrid();

                    for (int i = 0; i < 10; i++) {
                        for (int j = 0; j < 10; j++) {
                            if (p1.GetAt(i, j) == 1) {
                                return;
                            }
                        }
                    }

                    MessageBox.Show("Игрок 2 выиграл!", "Победа");
                }
            }
            
        }

        private int BotTurn() {
            int[,] map = new int[10, 10];
            int injX = -1;
            int injY = -1;
            Random rand = new Random();
            for (int i = 0; i < 10; i++) {
                for (int j = 0; j < 10; j++) {
                    map[i, j] = p1.GetAt(i, j);
                    if (map[i, j] == 1) map[i, j] = 0;
                    if (map[i,j] == 2) {
                        injX = j;
                        injY = i;
                    }
                }
            }

            if (injX != -1) {
                if (injX != 9 && BotCanFire(injY, injX+1, map)) {
                    logger.Append($"Бот: выстрел {char.ToUpper(WaterGrid.ToLetter(injX+2))}{injY + 1}\n");
                    return p1.Fire(injX+1, injY + 1);
                } else if (injX != 0 && BotCanFire(injY, injX-1, map)) {
                    logger.Append($"Бот: выстрел {char.ToUpper(WaterGrid.ToLetter(injX))}{injY + 1}\n");
                    return p1.Fire(injX - 1, injY + 1);
                } else if (injY != 9 && BotCanFire(injY + 1, injX, map)) {
                    logger.Append($"Бот: выстрел {char.ToUpper(WaterGrid.ToLetter(injX+1))}{injY + 2}\n");
                    return p1.Fire(injX, injY + 2);
                } else if (injY != 0 && BotCanFire(injY - 1, injX, map)) {
                    logger.Append($"Бот: выстрел {char.ToUpper(WaterGrid.ToLetter(injX+1))}{injY}\n");
                    return p1.Fire(injX, injY);
                }
            }

            while (true) {
                int x = rand.Next(0, 10);
                int y = rand.Next(0, 10);

                if (map[y, x] == 0 && BotCanFire(y, x, map)) {
                    logger.Append($"Бот: выстрел {char.ToUpper(WaterGrid.ToLetter(x + 1))}{y + 1}\n");
                    return p1.Fire(x, y + 1);
                }
            }

            //for (int i = 0; i < 10; i++) {
            //    for (int j = 0; j < 10; j++) {
            //if (map[i, j] == 0 && BotCanFire(i, j, map)) {
            //    logger.Append($"Бот: выстрел {char.ToUpper(WaterGrid.ToLetter(j + 1))}{i + 1}");
            //    return p1.Fire(j, i + 1);
            //}
            //    }
            //}

            //throw new InvalidOperationException();
        }

        private bool BotCanFire(int i, int j, int[,] map) {
            //if (i != 9 && map[i + 1, j] >= 2) {
            //    return false;
            //} else if (i != 9 && j != 9 && map[i + 1, j + 1] >= 2 && map[i + 1, j + 1] != 4) {
            //    return false;
            //} else if (i != 0 && j != 9 && map[i - 1, j + 1] >= 2 && map[i - 1, j + 1] != 4) {
            //    return false;
            //} else if (i != 0 && j != 0 && map[i - 1, j - 1] >= 2 && map[i - 1, j - 1] != 4) {
            //    return false;
            //} else if (i != 9 && j != 0 && map[i + 1, j - 1] >= 2 && map[i + 1, j - 1] != 4) {
            //    return false;
            //} else if (i != 0 && map[i - 1, j] >= 2 && map[i - 1, j] != 4) {
            //    return false;
            //} else if (j != 0 && map[i, j - 1] >= 2 && map[i, j - 1] != 4) {
            //    return false;
            //} else if (j != 9 && map[i, j + 1] >= 2 && map[i, j + 1] != 4) {
            //    return false;
            //} else {
            //    return true;
            //}

            return map[i,j] < 3;
        }
    }
}
