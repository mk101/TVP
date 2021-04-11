using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4.Models {
    public class WaterGrid {
        public int Size { get; }

        /// <summary>
        /// Сетка кораблей размером [Size]*[Size]
        /// Значения:
        /// - 0  Пустая клетка
        /// - 1  Корабль
        /// - 2  Корабль ранен
        /// - 3  Корабль убит
        /// - 4 Мимо
        /// </summary>
        private readonly int[] grid;

        public WaterGrid() {
            Random random = new Random();
            Size = 10;
            grid = new int[Size * Size];

            int dir = random.Next(0, 2);

            int x, y;

            if (dir == 0) {
                x = random.Next(0, 7);
                y = random.Next(0, 10);
                for (int i = 0; i < 4; i++) {
                    At(y, x + i) = 1;
                }
            } else {
                x = random.Next(0, 10);
                y = random.Next(0, 7);
                for (int i = 0; i < 4; i++) {
                    At(y + i, x) = 1;
                }
            }

            int r = 2;
            bool f = true;
            while (r > 0) {
                dir = random.Next(0, 2);

                if (dir == 0) {
                    x = random.Next(0, 8);
                    y = random.Next(0, 10);
                    f = true;
                    for (int i = 0; i < 3; i++) {
                        if (!CheckAvailable(y, x+i)) {
                            f = false;
                            break;
                        }
                    }
                    if (!f) continue;
                    for (int i = 0; i < 3; i++) {
                        At(y, x + i) = 1;
                    }
                } else {
                    x = random.Next(0, 10);
                    y = random.Next(0, 8);
                    f = true;
                    for (int i = 0; i < 3; i++) {
                        if (!CheckAvailable(y+i, x)) {
                            f = false;
                            break;
                        }
                    }
                    if (!f) continue;
                    for (int i = 0; i < 3; i++) {
                        At(y + i, x) = 1;
                    }
                }
                r--;
            }

            r = 3;

            while (r > 0) {
                dir = random.Next(0, 2);

                if (dir == 0) {
                    x = random.Next(0, 9);
                    y = random.Next(0, 10);
                    f = true;
                    for (int i = 0; i < 2; i++) {
                        if (!CheckAvailable(y, x + i)) {
                            f = false;
                            break;
                        }
                    }
                    if (!f) continue;
                    for (int i = 0; i < 2; i++) {
                        At(y, x + i) = 1;
                    }
                } else {
                    x = random.Next(0, 10);
                    y = random.Next(0, 9);
                    f = true;
                    for (int i = 0; i < 2; i++) {
                        if (!CheckAvailable(y + i, x)) {
                            f = false;
                            break;
                        }
                    }
                    if (!f) continue;
                    for (int i = 0; i < 2; i++) {
                        At(y + i, x) = 1;
                    }
                }
                r--;
            }

            r = 4;

            while (r > 0) {
                dir = random.Next(0, 2);

                if (dir == 0) {
                    x = random.Next(0, 10);
                    y = random.Next(0, 10);
                    f = true;
                    for (int i = 0; i < 1; i++) {
                        if (!CheckAvailable(y, x + i)) {
                            f = false;
                            break;
                        }
                    }
                    if (!f) continue;
                    for (int i = 0; i < 1; i++) {
                        At(y, x + i) = 1;
                    }
                } else {
                    x = random.Next(0, 10);
                    y = random.Next(0, 10);
                    f = true;
                    for (int i = 0; i < 1; i++) {
                        if (!CheckAvailable(y + i, x)) {
                            f = false;
                            break;
                        }
                    }
                    if (!f) continue;
                    for (int i = 0; i < 1; i++) {
                        At(y + i, x) = 1;
                    }
                }
                r--;
            }
        }

        private bool CheckAvailable(int i, int j) {
            if (i != 9 && At(i+1, j) == 1) {
                return false;
            } else if (i != 9 && j != 9 && At(i+1,j+1) == 1) {
                return false;
            } else if (i != 0 && j != 9 && At(i -1, j+1) == 1) {
                return false;
            } else if (i != 0 && j != 0 && At(i-1, j-1) == 1) {
                return false;
            } else if (i != 9 && j != 0 && At(i+1,j-1) == 1) {
                return false;
            } else if (i != 0 && At(i-1,j) == 1) {
                return false;
            } else if (j != 0 && At(i,j-1) == 1) {
                return false;
            } else if (j != 9 && At(i,j+1) == 1) {
                return false;
            } else {
                return true;
            }
        }

        public WaterGrid(int[] grid) {
            Size = 10;
            if (grid.Length != Size*Size) {
                throw new Exception("Некорректный размер сетки");
            }
            this.grid = new int[Size * Size];
            Array.Copy(grid, this.grid, grid.Length);
        }

        /// <summary>
        /// Функция проверки выстрела
        /// </summary>
        /// <param name="col">Столбец</param>
        /// <param name="row">Строка</param>
        /// <returns>
        /// -1, елси выстрел не попал в корабль
        ///  1, если попал, но не убил
        ///  2, если убил
        /// </returns>
        public int Fire(char col, int row) {
            return Fire(FromLetter(col), row);
        }

        public int Fire(int col, int row) {
            row--;

            if (At(row, col) == 1) {
                At(row, col) = 2;
                List<int> dirs = new List<int>();
                if (col != Size -1 && At(row, col+1) >= 1) {
                    dirs.Add(1);
                }

                if (col != 0 && At(row, col-1) >= 1) {
                    dirs.Add(-1);
                }
                
                if (row != Size -1 && At(row+1, col) >= 1) {
                    dirs.Add(2);
                }

                if (row != 0 && At(row-1, col) >= 1) {
                    dirs.Add(-2);
                }

                if (dirs.Count != 0) {
                    int res = 0;
                    for (int i =0; i < dirs.Count; i++) {
                        res += CheckKill(row, col, dirs[i]) ? 1 : 0;
                    }

                    if (res != dirs.Count) {
                        return 1;
                    } else {
                        for (int i = 0; i < dirs.Count; i++) {
                            FillKill(row, col, dirs[i]);
                        }

                        return 2;
                    }
                } else {
                    At(row, col) = 3;
                    return 2;
                }
            }
            if (At(row, col) == 0) {
                At(row, col) = 4;
            }
            return -1;
        }

        private void FillKill(int row, int col, int dir) {
            if (dir < -2 || dir > 2 || dir == 0) {
                throw new InvalidOperationException();
            }

            if (dir == 1) {
                while (At(row, col) == 2 || At(row, col) == 3) {
                    At(row, col++) = 3;
                    if (col == 10) break;
                }
            } else if (dir == -1) {
                while (At(row, col) == 2 || At(row, col) == 3) {
                    At(row, col--) = 3;
                    if (col == -1) break;
                }
            } else if (dir == 2) {
                while (At(row, col) == 2 || At(row, col) == 3) {
                    At(row++, col) = 3;
                    if (row == 10) break;
                }
            } else {
                while (At(row, col) == 2 || At(row, col) == 3) {
                    At(row--, col) = 3;
                    if (row == -1) break;
                }
            }
        }

        private bool CheckKill(int row, int col, int dir) {
            if (dir < -2 || dir > 2 || dir == 0) {
                throw new InvalidOperationException();
            }

            if (dir == 1) {
                col++;
                while (At(row, col) == 2) {
                    if (col != Size-1) {
                        col++;
                    } else {
                        return true;
                    }
                }
                if (At(row, col) == 0 || At(row, col) == 4) {
                    return true;
                } else {
                    return false;
                }
            } else if (dir == -1) {
                col--;
                while (At(row, col) == 2) {
                    if (col != 0) {
                        col--;
                    } else {
                        return true;
                    }
                }
                if (At(row, col) == 0 || At(row, col) == 4) {
                    return true;
                } else {
                    return false;
                }
            } else if (dir == 2) {
                row++;
                while (At(row, col) == 2) {
                    if (row != Size-1) {
                        row++;
                    } else {
                        return true;
                    }
                }
                if (At(row, col) == 0 || At(row, col) == 4) {
                    return true;
                } else {
                    return false;
                }
            } else {
                row--;
                while (At(row, col) == 2) {
                    if (row != 0) {
                        row--;
                    } else {
                        return true;
                    }
                }
                if (At(row, col) == 0 || At(row, col) == 4) {
                    return true;
                } else {
                    return false;
                }
            }
        }

        public static int FromLetter(char letter) {
            char[] a = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j' };
            return Array.IndexOf(a, char.ToLower(letter));
        }

        public static char ToLetter(int col) {
            char[] a = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j' };
            return a[col - 1];
        }

        private ref int At(int row, int col) {
            return ref grid[row * Size + col];
        }

        public int GetAt(int i, int j) {
            return At(i, j);
        }
    }
}
