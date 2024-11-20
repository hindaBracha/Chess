using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    internal class soldier
    {
        public string name;
        public char h1 = '1';
        public soldier(string name)
        {
            this.name = name;
        }

        public void isOk(Button[,] game, int i, int j)
        {

            if (name.ToString()[name.Length - 1] == h1)
                check1(game, i, j);
            else
                check2(game, i, j);
        }

        private void check2(Button[,] game, int i, int j)
        {

        }

        public void check1(Button[,] game, int i, int j)
        {
            if (game[i, j - 1].Name == "")
            {
                game[i, j - 1].BackColor = Color.Green;
                if (j == 6 && game[i, j - 2].Name == "")//אם הוא עוד לא זז ולא פעם אחת
                    game[i, j - 2].BackColor = Color.Green;
            }

            if (i > 0 && i < 7)
            {
                if (game[i - 1, j - 1].Name.ToString() != "" &&
                    game[i - 1, j - 1].Name.ToString()[name.Length - 1] != h1)
                    game[i - 1, j - 1].BackColor = Color.Red;
                if (game[i + 1, j - 1].Name.ToString() != "" &&
                         game[i + 1, j - 1].Name.ToString()[name.Length - 1] != h1)
                    game[i + 1, j - 1].BackColor = Color.Red;
            }
            if (i == 0)
                if (game[i + 1, j - 1].Name != "")
                {
                    game[i + 1, j - 1].BackColor = Color.Red;
                    if (j == 6 && game[i, j - 2].Name == "")//אם הוא עוד לא זז ולא פעם אחת
                        game[i, j - 2].BackColor = Color.Green;
                }
            if (i == 7)
            {
                if (game[i - 1, j - 1].Name != "")
                    game[i - 1, j - 1].BackColor = Color.Red;
                if (j == 6 && game[i, j - 2].Name == "")//אם הוא עוד לא זז ולא פעם אחת
                    game[i, j - 2].BackColor = Color.Green;

            }
        }

    }

}
