namespace MyGame
{
    public partial class Form1 : Form
    {
      

        #region  //משתנים
        string[] img1 = { "טורה2", "סוס2", "רץ2", "מלכה2", "מלך2", "רץ2", "סוס2", "טורה2", "חייל2" };
        string[] img2 = { "טורה1", "סוס1", "רץ1", "מלכה1", "מלך1", "רץ1", "סוס1", "טורה1", "חייל1" };
        Button[,] game = new Button[8, 8];
        Button C;
        private int i = 0;
        private int j = 0;
        private int ii;
        private int jj;
        private int KingI1 = 4;
        private int KingI2 = 4;
        private int KingJ1 = 7;
        private int KingJ2 = 0;
        private bool Mate = false;//(מט-(אם אחד מהמלכים נאכל נגמר המשחק
        private bool isFind = false;// אם לחץ על מקום ובחר להזיז 
        private bool isRook11 = false;// אם הזיז את הטורה הימינית / השמאלית
        private bool isRook12 = false;
        private bool isRook21 = false;
        private bool isRook22 = false;
        private bool isCastling1 = false;//אם המלך זז(לצורך הצרחה) -יש לדעת
        private bool isCastling2 = false;
        private bool originalState = true;// כדי לצבוע
        private char h1 = '1';//יסדר את התורים
        private string s;
        public Form1()
        {
            InitializeComponent();
        }
        #endregion

        #region //onLoad
        private void Form1_Load(object sender, EventArgs e)
        {
            for (i = 0; i < 8; i++)
            {
                for (j = 0; j < 8; j++)
                {
                    Button b = new Button();//יצירת פקד                 
                    b.Size = new Size(panel1.Width / 8, panel1.Height / 8);
                    b.Font = new Font("Fb Art", 20);
                    b.Location = new Point(i * b.Width, j * b.Height);
                    if (j == 0) //ליצור את כול הכלים
                    {
                        b.Name = img1[i];
                        b.Image = Image.FromFile(Application.StartupPath + "\\" + img1[i] + ".png", false);
                    }
                    if (j == 7)
                    {
                        b.Name = img2[i];
                        b.Image = Image.FromFile(Application.StartupPath + "\\" + img2[i] + ".png", false);

                    }
                    if (j == 1)//ליצור את החיילים
                    {
                        b.Name = img1[img1.Length - 1];
                        b.Image = Image.FromFile(Application.StartupPath + "\\" + img1[img1.Length - 1] + ".png", false);
                    }
                    if (j == 6)//ליצור את החיילים
                    {
                        b.Name = img2[img2.Length - 1];
                        b.Image = Image.FromFile(Application.StartupPath + "\\" + img2[img2.Length - 1] + ".png", true);
                        ////// b.ImageAlign = ContentAlignment.TopCenter;
                        // b.ImageAlign = ContentAlignment.BottomCenter;
                    }

                    b.Click += B_Click;
                    b.Tag = i + ";" + j;
                    game[i, j] = b;
                    panel1.Controls.Add(b);//שיוך למכיל
                }

                Console.WriteLine();
            }
            if (originalState)//כדי למחוק את הצבע מהלוח
            {

                double secondsToReturn = 0.3;

                Task.Delay(TimeSpan.FromSeconds(secondsToReturn)).ContinueWith(_ =>
                {
                    IsColo();

                });
            }
        }
        #endregion

        #region //onClick
        private void B_Click(object sender, EventArgs e)
        {

            //if (Pat())//אם הגיע למצב של פאט נגמר המשחק --אין למלך לאיפה ללכת// או שנגמר כול השחקנים
            //{
            //    IsEnabled();//נועל את הלוח
            //}
            if (!((sender as Button).Name == "" || (sender as Button).Name.ToString()[(sender as Button).Name.Length - 1] != h1))
            {
                //אם מה שנישלח הוא לא ריק וגם מה שנישלח הוא שווה למה שנישלח קודם
                //הכונה בזמן שהשחק מתלבט מה ללחוץ בירצוני להראות לו את האופציות
                C = (sender as Button);
                char c = C.Tag.ToString()[0];
                char c2 = C.Tag.ToString()[2];
                i = c - 48;
                j = c2 - 48;
                ii = i;
                jj = j;
                h1 = C.Name.ToString()[C.Name.Length - 1];
                switch (game[i, j].Name)
                {
                    case ("טורה1"):
                        {
                            Col(i, j, false);
                            Row(i, j, false);
                        }
                        break;
                    case ("מלכה1"):
                        {
                            Col(i, j, false);
                            Row(i, j, false);
                            Dag(i, j, false);
                        }
                        break;
                    case ("רץ1"):
                        {
                            Dag(i, j, false);
                        }
                        break;
                    case ("סוס1"):
                        {
                            Knight(i, j, false);
                        }
                        break;
                    case ("מלך1"):
                        {
                            King(i, j, false);
                        }
                        break;
                    case ("מלך2"):
                        {
                            King(i, j, false);
                        }
                        break;
                    case ("טורה2"):
                        {
                            Col(i, j, false);
                            Row(i, j, false);
                        }
                        break;
                    case ("מלכה2"):
                        {
                            Col(i, j, false);
                            Row(i, j, false);
                            Dag(i, j, false);
                        }
                        break;
                    case ("רץ2"):
                        {
                            Dag(i, j, false);
                        }
                        break;
                    case ("סוס2"):
                        {
                            Knight(i, j, false);
                        }
                        break;
                    case ("חייל1"):
                        {
                            if (IfChess())
                            {
                                if (game[i, j - 1].Name == "")
                                {
                                    s = game[i, j - 1].Name;
                                    game[i, j - 1].Name = h1.ToString();
                                    if (!IfChess())
                                        game[i, j - 1].BackColor = Color.Green;
                                    game[i, j - 1].Name = s;
                                    if (j == 6 && game[i, j - 2].Name == "")//אם הוא עוד לא זז ולא פעם אחת
                                    {
                                        s = game[i, j - 2].Name;
                                        game[i, j - 2].Name = h1.ToString();
                                        if (!IfChess())
                                            game[i, j - 2].BackColor = Color.Green;
                                        game[i, j - 2].Name = s;
                                    }
                                }
                                if (i > 0 && i < 7)
                                {
                                    if (game[i - 1, j - 1].Name.ToString() != "" &&
                                        game[i - 1, j - 1].Name.ToString()[game[i - 1, j - 1].Name.Length - 1] != h1)
                                    {
                                        s = game[i - 1, j - 1].Name;
                                        game[i - 1, j - 1].Name = h1.ToString();
                                        if (!IfChess())
                                            game[i - 1, j - 1].BackColor = Color.Red;
                                        game[i - 1, j - 1].Name = s;
                                    }
                                    if (game[i + 1, j - 1].Name.ToString() != "" &&
                                        game[i + 1, j - 1].Name.ToString()[game[i + 1, j - 1].Name.Length - 1] != h1)
                                    {
                                        s = game[i + 1, j - 1].Name;
                                        game[i + 1, j - 1].Name = h1.ToString();
                                        if (!IfChess())
                                            game[i + 1, j - 1].BackColor = Color.Red;
                                        game[i + 1, j - 1].Name = s;
                                    }
                                }
                                if (i == 0)
                                    if (game[i + 1, j - 1].Name != "")
                                    {
                                        s = game[i + 1, j - 1].Name;
                                        game[i + 1, j - 1].Name = h1.ToString();
                                        if (!IfChess())
                                            game[i + 1, j - 1].BackColor = Color.Red;
                                        game[i + 1, j - 1].Name = s;
                                        if (j == 6 && game[i, j - 2].Name == "")//אם הוא עוד לא זז ולא פעם אחת
                                        {
                                            s = game[i, j - 2].Name;
                                            game[i, j - 2].Name = h1.ToString();
                                            if (!IfChess())
                                                game[i, j - 2].BackColor = Color.Green;
                                            game[i, j - 2].Name = s;
                                        }
                                    }
                                if (i == 7)
                                {
                                    if (game[i - 1, j - 1].Name != "")
                                    {
                                        s = game[i - 1, j - 1].Name;
                                        game[i - 1, j - 1].Name = h1.ToString();
                                        if (!IfChess())
                                            game[i - 1, j - 1].BackColor = Color.Red;
                                        game[i - 1, j - 1].Name = s;
                                    }
                                    if (j == 6 && game[i, j - 2].Name == "")//אם הוא עוד לא זז ולא פעם אחת
                                    {
                                        s = game[i, j - 2].Name;
                                        game[i, j - 2].Name = h1.ToString();
                                        if (!IfChess())
                                            game[i, j - 2].BackColor = Color.Green;
                                        game[i, j - 2].Name = s;
                                    }
                                }
                            }
                            else
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
                                        game[i - 1, j - 1].Name.ToString()[game[i - 1, j - 1].Name.Length - 1] != h1)
                                        game[i - 1, j - 1].BackColor = Color.Red;
                                    if (game[i + 1, j - 1].Name.ToString() != "" &&
                                             game[i + 1, j - 1].Name.ToString()[game[i + 1, j - 1].Name.Length - 1] != h1)
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
                        break;
                    case ("חייל2"):
                        {
                            if (IfChess())
                            {
                                if (game[i, j + 1].Name == "")
                                {
                                    s = game[i, j + 1].Name;
                                    game[i, j + 1].Name = h1.ToString();
                                    if (!IfChess())
                                        game[i, j + 1].BackColor = Color.Green;
                                    game[i, j + 1].Name = s;
                                    if (j == 1 && game[i, j + 2].Name == "")//אם הוא עוד לא זז ולא פעם אחת
                                    {
                                        s = game[i, j + 2].Name;
                                        game[i, j + 2].Name = h1.ToString();
                                        if (!IfChess())
                                            game[i, j + 2].BackColor = Color.Green;
                                        game[i, j + 2].Name = s;
                                    }
                                }

                                if (i > 0 && i < 7)
                                {
                                    if (game[i + 1, j + 1].Name.ToString() != "" &&
                                        game[i + 1, j + 1].Name.ToString()[game[i + 1, j + 1].Name.Length - 1] != h1)
                                    {
                                        s = game[i + 1, j + 1].Name;
                                        game[i + 1, j + 1].Name = h1.ToString();
                                        if (!IfChess())
                                            game[i + 1, j + 1].BackColor = Color.Red;
                                        game[i + 1, j + 1].Name = s;
                                    }
                                    if (game[i - 1, j + 1].Name != "" &&
                                   game[i - 1, j + 1].Name.ToString()[game[i - 1, j + 1].Name.Length - 1] != h1)
                                    {
                                        s = game[i - 1, j + 1].Name;
                                        game[i - 1, j + 1].Name = h1.ToString();
                                        if (!IfChess())
                                            game[i - 1, j + 1].BackColor = Color.Red;
                                        game[i - 1, j + 1].Name = s;
                                    }
                                }
                                if (i == 0)
                                    if (game[i + 1, j + 1].Name.ToString() != "" && game[i + 1, j + 1].Name.ToString()[game[i + 1, j + 1].Name.Length - 1] != h1)
                                    {
                                        s = game[i + 1, j + 1].Name;
                                        game[i + 1, j + 1].Name = h1.ToString();
                                        if (!IfChess())
                                            game[i + 1, j + 1].BackColor = Color.Red;
                                        game[i + 1, j + 1].Name = s;
                                        if (j == 1 && game[i, j + 2].Name == "" && game[i, j + 1].Name == "")//אם הוא עוד לא זז ולא פעם אחת
                                        {
                                            s = game[i, j + 2].Name;
                                            game[i, j + 2].Name = h1.ToString();
                                            if (!IfChess())
                                                game[i, j + 2].BackColor = Color.Green;
                                            game[i, j + 2].Name = s;
                                        }
                                    }
                                if (i == 7)
                                {
                                    if (game[i - 1, j + 1].Name != "" && game[i - 1, j + 1].Name.ToString()[game[i - 1, j + 1].Name.Length - 1] != h1)
                                    {
                                        s = game[i - 1, j + 1].Name;
                                        game[i - 1, j + 1].Name = h1.ToString();
                                        if (!IfChess())
                                            game[i - 1, j + 1].BackColor = Color.Red;
                                        game[i - 1, j + 1].Name = s;
                                    }
                                    if (j == 1 && game[i, j + 2].Name == "" && game[i, j + 1].Name == "")//אם הוא עוד לא זז ולא פעם אחת
                                    {
                                        s = game[i, j + 2].Name;
                                        game[i, j + 2].Name = h1.ToString();
                                        if (!IfChess())
                                            game[i, j + 2].BackColor = Color.Green;
                                        game[i, j + 2].Name = s;
                                    }
                                }
                            }
                            else
                            {
                                if (game[i, j + 1].Name == "")
                                {
                                    game[i, j + 1].BackColor = Color.Green;
                                    if (j == 1 && game[i, j + 2].Name == "")//אם הוא עוד לא זז ולא פעם אחת
                                        game[i, j + 2].BackColor = Color.Green;
                                }

                                if (i > 0 && i < 7)
                                {
                                    if (game[i + 1, j + 1].Name.ToString() != "" &&
                                        game[i + 1, j + 1].Name.ToString()[game[i + 1, j + 1].Name.Length - 1] != h1)
                                        game[i + 1, j + 1].BackColor = Color.Red;
                                    if (game[i - 1, j + 1].Name.ToString() != "" &&
                                       game[i - 1, j + 1].Name.ToString()[game[i - 1, j + 1].Name.Length - 1] != h1)
                                        game[i - 1, j + 1].BackColor = Color.Red;
                                }
                                if (i == 0)
                                    if (game[i + 1, j + 1].Name != "")
                                    {
                                        game[i + 1, j + 1].BackColor = Color.Red;
                                        if (j == 1 && game[i, j + 2].Name == "")//אם הוא עוד לא זז ולא פעם אחת
                                            game[i, j + 2].BackColor = Color.Green;
                                    }
                                if (i == 7)
                                {
                                    if (game[i - 1, j + 1].Name != "")
                                        game[i - 1, j + 1].BackColor = Color.Red;
                                    if (j == 1 && game[i, j + 2].Name == "")//אם הוא עוד לא זז ולא פעם אחת
                                        game[i, j + 2].BackColor = Color.Green;
                                }
                            }
                        }
                        break;
                }
            }
            else
            {

                if (C == null)
                    return;
                char c = (sender as Button).Tag.ToString()[0];
                char c2 = (sender as Button).Tag.ToString()[2];
                i = c - 48;
                j = c2 - 48;
                //C - is the butten frev
                ii = C.Tag.ToString()[0] - 48;
                jj = C.Tag.ToString()[2] - 48;
                switch (C.Name)
                {
                    case ("טורה1"):
                        {
                            Row(i, j, true);
                            Col(i, j, true);
                        }
                        break;
                    case ("מלכה1"):
                        {
                            Col(i, j, true);
                            Row(i, j, true);
                            Dag(i, j, true);
                        }
                        break;
                    case ("רץ1"):
                        {
                            Dag(i, j, true);
                        }
                        break;
                    case ("סוס1"):
                        {
                            Knight(i, j, true);
                        }
                        break;
                    case ("מלך1"):
                        {
                            King(i, j, true);
                        }
                        break;
                    case ("מלך2"):
                        {
                            King(i, j, true);
                        }
                        break;
                    case ("טורה2"):
                        {
                            Col(i, j, true);
                            Row(i, j, true);
                        }
                        break;
                    case ("מלכה2"):
                        {
                            Col(i, j, true);
                            Row(i, j, true);
                            Dag(i, j, true);
                        }
                        break;
                    case ("רץ2"):
                        {
                            Dag(i, j, true);
                        }
                        break;
                    case ("סוס2"):
                        {
                            Knight(i, j, true);
                        }
                        break;
                    case ("חייל1"):
                        {
                            if (j + 1 == jj)
                            {
                                if ((i == ii && game[i, j].Name == "") ||
                                    (i > 0 && ii - 1 == i && game[i, j].Name.ToString()[game[i, j].Name.Length - 1] != h1)
                                    || (i < 8 && ii + 1 == i && game[i, j].Name != "" && game[i, j].Name.ToString()[game[i, j].Name.Length - 1] != h1)
                                    || (i < 8 && i > 0 && ((ii + 1 == i) || (ii - 1 == i)) && (game[i, j].Name != "" && game[i, j].Name.ToString()[game[i, j].Name.Length - 1] != h1)))
                                {
                                    if (IfChess())
                                    {
                                        s = game[i, j].Name;
                                        game[i, j].Name = C.Name;
                                        if (!IfChess())
                                        {
                                            game[i, j].Name = s;
                                            if (game[i, j].Name != "")
                                                game[i, j].BackColor = Color.Black;
                                            game[i, j].Name = C.Name;
                                            game[i, j].Image = C.Image;  game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                            if (h1 == '1')
                                                h1 = '2';
                                            else
                                                h1 = '1';
                                        }
                                        else game[i, j].Name = s;
                                    }
                                    else
                                    {
                                        if (game[i, j].Name != "")
                                            game[i, j].BackColor = Color.Black;
                                        game[i, j].Name = C.Name;
                                        game[i, j].Image = C.Image;  game[i, j].Image = C.Image; C.Name = null;C.Image = null;
                                        if (h1 == '1')
                                            h1 = '2';
                                        else
                                            h1 = '1';
                                    }
                                }
                            }
                            else if (j + 2 == jj && jj == 6)
                                if (i == ii && game[i, j].Name == "" && game[i, j + 1].Name == "")
                                {
                                    if (IfChess())
                                    {
                                        s = game[i, j].Name;
                                        game[i, j].Name = C.Name;
                                        if (!IfChess())
                                        {
                                            game[i, j].Name = s;
                                            if (game[i, j].Name != "")
                                                game[i, j].BackColor = Color.Black;
                                            game[i, j].Name = C.Name;
                                            game[i, j].Image = C.Image;  game[i, j].Image = C.Image; C.Name = null;C.Image = null;
                                            if (h1 == '1')
                                                h1 = '2';
                                            else
                                                h1 = '1';
                                        }
                                        else game[i, j].Name = s;
                                    }
                                    else
                                    {
                                        if (game[i, j].Name != "")
                                            game[i, j].BackColor = Color.Black;
                                        game[i, j].Name = C.Name;
                                        game[i, j].Image = C.Image;
                                         game[i, j].Image = C.Image; C.Name = null;C.Image = null;
                                        C.Image = null;
                                        if (h1 == '1')
                                            h1 = '2';
                                        else
                                            h1 = '1';
                                    }
                                }
                        }
                        break;
                    case ("חייל2"):
                        {
                            if (j - 1 == jj)
                            {
                                if ((i == ii && game[i, j].Name == "") ||
                                    (i > 0 && ii - 1 == i && game[i, j].Name.ToString()[game[i, j].Name.Length - 1] != h1)
                                    || (i < 8 && ii + 1 == i && game[i, j].Name != "" && game[i, j].Name.ToString()[game[i, j].Name.Length - 1] != h1)
                                    || (i < 8 && i > 0 && ((ii - 1 == i) || (ii + 1 == i)) && (game[i, j].Name != "" && game[i, j].Name.ToString()[game[i, j].Name.Length - 1] != h1)))
                                {
                                    if (IfChess())
                                    {
                                        s = game[i, j].Name;
                                        game[i, j].Name = C.Name;
                                        if (!IfChess())
                                        {
                                            game[i, j].Name = s;
                                            if (game[i, j].Name != "")
                                                game[i, j].BackColor = Color.Black;
                                            game[i, j].Name = C.Name;
                                            game[i, j].Image = C.Image;  game[i, j].Image = C.Image; C.Name = null;C.Image = null;
                                            if (h1 == '1')
                                                h1 = '2';
                                            else
                                                h1 = '1';
                                        }
                                        else game[i, j].Name = s;
                                    }
                                    else
                                    {
                                        if (game[i, j].Name != "")
                                            game[i, j].BackColor = Color.Black;
                                        game[i, j].Name = C.Name;
                                        game[i, j].Image = C.Image;  game[i, j].Image = C.Image; C.Name = null;C.Image = null;
                                        if (h1 == '1')
                                            h1 = '2';
                                        else
                                            h1 = '1';
                                    }
                                }
                            }
                            else if (jj == j - 2 && jj == 1)
                            {
                                if (i == ii && game[i, j].Name == "" && game[i, j - 1].Name == "")
                                {
                                    if (IfChess())
                                    {
                                        s = game[i, j].Name;
                                        game[i, j].Name = C.Name;
                                        if (!IfChess())
                                        {
                                            game[i, j].Name = s;
                                            game[i, j].Name = C.Name;
                                            game[i, j].Image = C.Image;  game[i, j].Image = C.Image; C.Name = null;C.Image = null;
                                            if (h1 == '1')
                                                h1 = '2';
                                            else
                                                h1 = '1';
                                        }
                                        else game[i, j].Name = s;
                                    }
                                    else
                                    {
                                        game[i, j].Name = C.Name;
                                        game[i, j].Image = C.Image;  game[i, j].Image = C.Image; C.Name = null;C.Image = null;
                                        if (h1 == '1')
                                            h1 = '2';
                                        else
                                            h1 = '1';
                                    }
                                }
                            }
                        }
                        break;
                }
            }
            if (originalState)//כדי למחוק את הצבע מהלוח
            {

                double secondsToReturn = 0.3;

                Task.Delay(TimeSpan.FromSeconds(secondsToReturn)).ContinueWith(_ =>
                {
                    IsColo();

                });
            }
            isFind = false;
            if (Mate)
            {
                IsEnabled(); //נועל את הלוח
            }
        }
        #endregion

        #region//צובע את כול הלוח
        private void IsColo()
        {
            for (i = 0; i < 8; i++)
            {
                int Colo = 0;
                if (i % 2 == 0)
                    Colo = +1;
                for (j = 0; j < 8; j++)
                {
                    if (Colo % 2 == 0)
                        game[j, i].BackColor = Color.WhiteSmoke;
                    else
                        game[j, i].BackColor = Color.LightGray;
                    Colo++;

                    //if (Colo % 2 == 0)
                    //    game[j, i].BackColor = Color.WhiteSmoke;
                    //else
                    //    game[j, i].BackColor = Color.MistyRose;
                    //Colo++;
                    //if (Colo % 2 == 0)
                    //    game[j, i].BackColor = Color.Chartreuse;
                    //else
                    //    game[j, i].BackColor = Color.RoyalBlue;
                    //Colo++;
                    //if (Colo % 2 == 0)
                    //    game[j, i].BackColor = Color.Chartreuse;
                    //else
                    //    game[j, i].BackColor = Color.PaleTurquoise;
                    //Colo++;
                    //if (Colo % 2 == 0)
                    //    game[j, i].BackColor = Color.CadetBlue;
                    //else
                    //    game[j, i].BackColor = Color.BlanchedAlmond;
                    //Colo++;
                    //if (Colo % 2 == 0)
                    //    game[j, i].BackColor = Color.BlueViolet;
                    //else
                    //    game[j, i].BackColor = Color.BlanchedAlmond;
                    //Colo++;

                    //if (Colo % 2 == 0)
                    //    game[j, i].BackColor = Color.Gainsboro;
                    //else
                    //    game[j, i].BackColor = Color.BlanchedAlmond;
                    //Colo++;

                    // if (Colo % 2 == 0)
                    //    game[j, i].BackColor = Color.Gray;
                    //else
                    //    game[j, i].BackColor = Color.BlanchedAlmond;
                    //Colo++;
                }
            }

        }
        #endregion

        #region  //Enabled
        private void IsEnabled()
        {
            for (i = 0; i < 8; i++)
            {
                for (j = 0; j < 8; j++)
                {
                    game[j, i].Enabled = false;
                }
            }
        }
        #endregion

        #region//מלך
        public void King(int Mi, int Mj, bool correct)
        {

            int MinKingI1 = KingI1;
            int MinKingJ1 = KingJ1;
            int MinKingI2 = KingI2;
            int MinKingJ2 = KingJ2;
            if (h1 == '1')
            {
                KingI1 = Mi;
                KingJ1 = Mj;
            }
            else
            {
                KingI2 = Mi;
                KingJ2 = Mj;
            }
            if (!correct)
            {
                if (Mi > 0 && Mi < 7 && Mj > 0 && Mj < 7)
                {

                    if (game[Mi - 1, Mj].Name == "")
                    {
                        if (h1 == '1')
                        {
                            KingI1 = Mi - 1;
                            if (!IfChess())
                                game[Mi - 1, Mj].BackColor = Color.Green;
                            KingI1 += 1;
                        }
                        else
                        {
                            KingI2 = Mi - 1;
                            if (!IfChess())
                                game[Mi - 1, Mj].BackColor = Color.Green;
                            KingI2 += 1;
                        }
                    }
                    else if (game[Mi - 1, Mj].Name.ToString()[game[Mi - 1, Mj].Name.Length - 1] != h1)
                    {
                        if (h1 == '1')
                        {
                            KingI1 = Mi - 1;
                            if (!IfChess())
                                game[Mi - 1, Mj].BackColor = Color.Green;
                            KingI1 += 1;
                        }
                        else
                        {
                            KingI2 = Mi - 1;
                            if (!IfChess())
                                game[Mi - 1, Mj].BackColor = Color.Red;
                            KingI2 += 1;
                        }
                    }
                    if (game[Mi, Mj - 1].Name == "")
                    {
                        if (h1 == '1')
                        {
                            KingJ1 = Mi - 1;
                            if (!IfChess())
                                game[Mi, Mj - 1].BackColor = Color.Green;
                            KingJ1 += 1;
                        }
                        else
                        {
                            KingJ2 = Mi - 1;
                            if (!IfChess())
                                game[Mi, Mj - 1].BackColor = Color.Green;
                            KingJ2 += 1;
                        }
                    }
                    else if (game[Mi, Mj - 1].Name.ToString()[game[Mi, Mj - 1].Name.Length - 1] != h1)
                    {
                        if (h1 == '1')
                        {
                            KingJ1 = Mi - 1;
                            if (!IfChess())
                                game[Mi, Mj - 1].BackColor = Color.Red;
                            KingJ1 += 1;
                        }
                        else
                        {
                            KingJ2 = Mi - 1;
                            if (!IfChess())
                                game[Mi, Mj - 1].BackColor = Color.Red;
                            KingJ2 += 1;
                        }
                    }
                    if (game[Mi - 1, Mj - 1].Name == "")
                    {
                        if (h1 == '1')
                        {
                            KingI1 = Mi - 1;
                            KingJ1 = Mi - 1;
                            if (!IfChess())
                                game[Mi - 1, Mj - 1].BackColor = Color.Green;
                            KingJ1 += 1; KingI1 += 1;
                        }
                        else
                        {
                            KingI2 = Mi - 1;
                            KingJ2 = Mi - 1;
                            if (!IfChess())
                                game[Mi - 1, Mj - 1].BackColor = Color.Green;
                            KingJ2 += 1; KingI2 += 1;
                        }
                    }
                    else if (game[Mi - 1, Mj - 1].Name.ToString()[game[Mi - 1, Mj - 1].Name.Length - 1] != h1)
                    {
                        if (h1 == '1')
                        {
                            KingI1 = Mi - 1;
                            KingJ1 = Mi - 1;
                            if (!IfChess())
                                game[Mi - 1, Mj - 1].BackColor = Color.Red;
                            KingJ1 += 1; KingI1 += 1;
                        }
                        else
                        {
                            KingI2 = Mi - 1;
                            KingJ2 = Mi - 1;
                            if (!IfChess())
                                game[Mi - 1, Mj - 1].BackColor = Color.Red;
                            KingJ2 += 1; KingI2 += 1;
                        }
                    }
                    if (game[Mi + 1, Mj].Name == "")
                    {
                        if (h1 == '1')
                        {
                            KingI1 = Mi + 1;
                            if (!IfChess())
                                game[Mi + 1, Mj].BackColor = Color.Green;
                            KingI1 -= 1;
                        }
                        else
                        {
                            KingI2 = Mi + 1;
                            if (!IfChess())
                                game[Mi + 1, Mj].BackColor = Color.Green;
                            KingI2 -= 1;
                        }
                    }
                    else if (game[Mi + 1, Mj].Name.ToString()[game[Mi + 1, Mj].Name.Length - 1] != h1)
                    {
                        if (h1 == '1')
                        {
                            KingI1 = Mi + 1;
                            if (!IfChess())
                                game[Mi + 1, Mj].BackColor = Color.Red;
                            KingI1 -= 1;
                        }
                        else
                        {
                            KingI2 = Mi + 1;
                            if (!IfChess())
                                game[Mi + 1, Mj].BackColor = Color.Red;
                            KingI2 -= 1;
                        }
                    }
                    if (game[Mi, Mj + 1].Name == "")
                    {
                        if (h1 == '1')
                        {
                            KingJ1 = Mj + 1;
                            if (!IfChess())
                                game[Mi, Mj + 1].BackColor = Color.Green;
                            KingJ1 -= 1;
                        }
                        else
                        {
                            KingJ2 = Mj + 1;
                            if (!IfChess())
                                game[Mi, Mj + 1].BackColor = Color.Green;
                            KingJ2 -= 1;
                        }
                    }
                    else if (game[Mi, Mj + 1].Name.ToString()[game[Mi, Mj + 1].Name.Length - 1] != h1)
                    {
                        if (h1 == '1')
                        {
                            KingJ1 = Mj + 1;
                            if (!IfChess())
                                game[Mi, Mj + 1].BackColor = Color.Red;
                            KingJ1 -= 1;
                        }
                        else
                        {
                            KingJ2 = Mj + 1;
                            if (!IfChess())
                                game[Mi, Mj + 1].BackColor = Color.Red;
                            KingJ2 -= 1;
                        }
                    }
                    if (game[Mi + 1, Mj + 1].Name == "")
                    {
                        if (h1 == '1')
                        {
                            KingI1 = Mi + 1; KingJ1 = Mj + 1;
                            if (!IfChess())
                                game[Mi + 1, Mj + 1].BackColor = Color.Green;
                            KingI1 -= 1; KingJ1 -= 1;
                        }
                        else
                        {
                            KingI2 = Mi + 1; KingJ2 = Mj + 1;
                            if (!IfChess())
                                game[Mi + 1, Mj + 1].BackColor = Color.Green;
                            KingI2 -= 1; KingJ2 -= 1;
                        }
                    }
                    else if (game[Mi + 1, Mj + 1].Name.ToString()[game[Mi + 1, Mj + 1].Name.Length - 1] != h1)
                    {
                        if (h1 == '1')
                        {
                            KingI1 = Mi + 1; KingJ1 = Mj + 1;
                            if (!IfChess())
                                game[Mi + 1, Mj + 1].BackColor = Color.Red;
                            KingI1 -= 1; KingJ1 -= 1;
                        }
                        else
                        {
                            KingI2 = Mi + 1; KingJ2 = Mj + 1;
                            if (!IfChess())
                                game[Mi + 1, Mj + 1].BackColor = Color.Red;
                            KingI2 -= 1; KingJ2 -= 1;
                        }
                    }
                    if (game[Mi - 1, Mj + 1].Name == "")
                    {
                        if (h1 == '1')
                        {
                            KingI1 = Mi - 1; KingJ1 = Mj + 1;
                            if (!IfChess())
                                game[Mi - 1, Mj + 1].BackColor = Color.Green;
                            KingI1 += 1; KingJ1 -= 1;
                        }
                        else
                        {
                            KingI2 = Mi - 1; KingJ2 = Mj + 1;
                            if (!IfChess())
                                game[Mi - 1, Mj + 1].BackColor = Color.Green;
                            KingI2 += 1; KingJ2 -= 1;
                        }
                    }
                    else if (game[Mi - 1, Mj + 1].Name.ToString()[game[Mi - 1, Mj + 1].Name.Length - 1] != h1)
                    {
                        if (h1 == '1')
                        {
                            KingI1 = Mi - 1; KingJ1 = Mj + 1;
                            if (!IfChess())
                                game[Mi - 1, Mj + 1].BackColor = Color.Red;
                            KingI1 += 1; KingJ1 -= 1;
                        }
                        else
                        {
                            KingI2 = Mi - 1; KingJ2 = Mj + 1;
                            if (!IfChess())
                                game[Mi - 1, Mj + 1].BackColor = Color.Red;
                            KingI2 += 1; KingJ2 -= 1;
                        }
                    }
                    if (game[Mi + 1, Mj - 1].Name == "")
                    {
                        if (h1 == '1')
                        {
                            KingI1 = Mi + 1; KingJ1 = Mj - 1;
                            if (!IfChess())
                                game[Mi + 1, Mj - 1].BackColor = Color.Green;
                            KingI1 -= 1; KingJ1 += 1;
                        }
                        else
                        {
                            KingI2 = Mi + 1; KingJ2 = Mj - 1;
                            if (!IfChess())
                                game[Mi + 1, Mj - 1].BackColor = Color.Green;
                            KingI2 -= 1; KingJ2 += 1;
                        }
                    }
                    else if (game[Mi + 1, Mj - 1].Name.ToString()[game[Mi + 1, Mj - 1].Name.Length - 1] != h1)
                    {
                        if (h1 == '1')
                        {
                            KingI1 = Mi + 1; KingJ1 = Mj - 1;
                            if (!IfChess())
                                game[Mi + 1, Mj - 1].BackColor = Color.Red;
                            KingI1 -= 1; KingJ1 += 1;
                        }
                        else
                        {
                            KingI2 = Mi + 1; KingJ2 = Mj - 1;
                            if (!IfChess())
                                game[Mi + 1, Mj - 1].BackColor = Color.Red;
                            KingI2 -= 1; KingJ2 += 1;
                        }
                    }
                }
                else if (Mi == 7 && Mj > 0 && Mj < 7)
                {
                    if (game[Mi - 1, Mj].Name == "")
                    {
                        if (h1 == '1')
                        {
                            KingI1 = Mi - 1;
                            if (!IfChess())
                                game[Mi - 1, Mj].BackColor = Color.Green;
                            KingI1 += 1;
                        }
                        else
                        {
                            KingI2 = Mi - 1;
                            if (!IfChess())
                                game[Mi - 1, Mj].BackColor = Color.Green;
                            KingI2 += 1;
                        }
                    }
                    else if (game[Mi - 1, Mj].Name.ToString()[game[Mi - 1, Mj].Name.Length - 1] != h1)
                    {
                        if (h1 == '1')
                        {
                            KingI1 = Mi - 1;
                            if (!IfChess())
                                game[Mi - 1, Mj].BackColor = Color.Green;
                            KingI1 += 1;
                        }
                        else
                        {
                            KingI2 = Mi - 1;
                            if (!IfChess())
                                game[Mi - 1, Mj].BackColor = Color.Red;
                            KingI2 += 1;
                        }
                    }

                    if (game[Mi, Mj - 1].Name == "")
                    {
                        if (h1 == '1')
                        {
                            KingJ1 = Mi - 1;
                            if (!IfChess())
                                game[Mi, Mj - 1].BackColor = Color.Green;
                            KingJ1 += 1;
                        }
                        else
                        {
                            KingJ2 = Mi - 1;
                            if (!IfChess())
                                game[Mi, Mj - 1].BackColor = Color.Green;
                            KingJ2 += 1;
                        }
                    }
                    else if (game[Mi, Mj - 1].Name.ToString()[game[Mi, Mj - 1].Name.Length - 1] != h1)
                    {
                        if (h1 == '1')
                        {
                            KingJ1 = Mi - 1;
                            if (!IfChess())
                                game[Mi, Mj - 1].BackColor = Color.Red;
                            KingJ1 += 1;
                        }
                        else
                        {
                            KingJ2 = Mi - 1;
                            if (!IfChess())
                                game[Mi, Mj - 1].BackColor = Color.Red;
                            KingJ2 += 1;
                        }
                    }

                    if (game[Mi, Mj + 1].Name == "")
                    {
                        if (h1 == '1')
                        {
                            KingJ1 = Mj + 1;
                            if (!IfChess())
                                game[Mi, Mj + 1].BackColor = Color.Green;
                            KingJ1 -= 1;
                        }
                        else
                        {
                            KingJ2 = Mj + 1;
                            if (!IfChess())
                                game[Mi, Mj + 1].BackColor = Color.Green;
                            KingJ2 -= 1;
                        }
                    }
                    else if (game[Mi, Mj + 1].Name.ToString()[game[Mi, Mj + 1].Name.Length - 1] != h1)
                    {
                        if (h1 == '1')
                        {
                            KingJ1 = Mj + 1;
                            if (!IfChess())
                                game[Mi, Mj + 1].BackColor = Color.Red;
                            KingJ1 -= 1;
                        }
                        else
                        {
                            KingJ2 = Mj + 1;
                            if (!IfChess())
                                game[Mi, Mj + 1].BackColor = Color.Red;
                            KingJ2 -= 1;
                        }
                    }

                    if (game[Mi - 1, Mj - 1].Name == "")
                    {
                        if (h1 == '1')
                        {
                            KingI1 = Mi - 1;
                            KingJ1 = Mi - 1;
                            if (!IfChess())
                                game[Mi - 1, Mj - 1].BackColor = Color.Green;
                            KingJ1 += 1; KingI1 += 1;
                        }
                        else
                        {
                            KingI2 = Mi - 1;
                            KingJ2 = Mi - 1;
                            if (!IfChess())
                                game[Mi - 1, Mj - 1].BackColor = Color.Green;
                            KingJ2 += 1; KingI2 += 1;
                        }
                    }
                    else if (game[Mi - 1, Mj - 1].Name.ToString()[game[Mi - 1, Mj - 1].Name.Length - 1] != h1)
                    {
                        if (h1 == '1')
                        {
                            KingI1 = Mi - 1;
                            KingJ1 = Mi - 1;
                            if (!IfChess())
                                game[Mi - 1, Mj - 1].BackColor = Color.Red;
                            KingJ1 += 1; KingI1 += 1;
                        }
                        else
                        {
                            KingI2 = Mi - 1;
                            KingJ2 = Mi - 1;
                            if (!IfChess())
                                game[Mi - 1, Mj - 1].BackColor = Color.Red;
                            KingJ2 += 1; KingI2 += 1;
                        }
                    }

                    if (game[Mi - 1, Mj + 1].Name == "")
                    {
                        if (h1 == '1')
                        {
                            KingI1 = Mi - 1; KingJ1 = Mj + 1;
                            if (!IfChess())
                                game[Mi - 1, Mj + 1].BackColor = Color.Green;
                            KingI1 += 1; KingJ1 -= 1;
                        }
                        else
                        {
                            KingI2 = Mi - 1; KingJ2 = Mj + 1;
                            if (!IfChess())
                                game[Mi - 1, Mj + 1].BackColor = Color.Green;
                            KingI2 += 1; KingJ2 -= 1;
                        }
                    }
                    else if (game[Mi - 1, Mj + 1].Name.ToString()[game[Mi - 1, Mj + 1].Name.Length - 1] != h1)
                    {
                        if (h1 == '1')
                        {
                            KingI1 = Mi - 1; KingJ1 = Mj + 1;
                            if (!IfChess())
                                game[Mi - 1, Mj + 1].BackColor = Color.Red;
                            KingI1 += 1; KingJ1 -= 1;
                        }
                        else
                        {
                            KingI2 = Mi - 1; KingJ2 = Mj + 1;
                            if (!IfChess())
                                game[Mi - 1, Mj + 1].BackColor = Color.Red;
                            KingI2 += 1; KingJ2 -= 1;
                        }
                    }

                }
                else if (Mi == 7 && Mj == 0)
                {
                    if (game[Mi, Mj + 1].Name == "")
                    {
                        if (h1 == '1')
                        {
                            KingJ1 = Mj + 1;
                            if (!IfChess())
                                game[Mi, Mj + 1].BackColor = Color.Green;
                            KingJ1 -= 1;
                        }
                        else
                        {
                            KingJ2 = Mj + 1;
                            if (!IfChess())
                                game[Mi, Mj + 1].BackColor = Color.Green;
                            KingJ2 -= 1;
                        }
                    }
                    else if (game[Mi, Mj + 1].Name.ToString()[game[Mi, Mj + 1].Name.Length - 1] != h1)
                    {
                        if (h1 == '1')
                        {
                            KingJ1 = Mj + 1;
                            if (!IfChess())
                                game[Mi, Mj + 1].BackColor = Color.Red;
                            KingJ1 -= 1;
                        }
                        else
                        {
                            KingJ2 = Mj + 1;
                            if (!IfChess())
                                game[Mi, Mj + 1].BackColor = Color.Red;
                            KingJ2 -= 1;
                        }
                    }

                    if (game[Mi - 1, Mj + 1].Name == "")
                    {
                        if (h1 == '1')
                        {
                            KingI1 = Mi - 1; KingJ1 = Mj + 1;
                            if (!IfChess())
                                game[Mi - 1, Mj + 1].BackColor = Color.Green;
                            KingI1 += 1; KingJ1 -= 1;
                        }
                        else
                        {
                            KingI2 = Mi - 1; KingJ2 = Mj + 1;
                            if (!IfChess())
                                game[Mi - 1, Mj + 1].BackColor = Color.Green;
                            KingI2 += 1; KingJ2 -= 1;
                        }
                    }
                    else if (game[Mi - 1, Mj + 1].Name.ToString()[game[Mi - 1, Mj + 1].Name.Length - 1] != h1)
                    {
                        if (h1 == '1')
                        {
                            KingI1 = Mi - 1; KingJ1 = Mj + 1;
                            if (!IfChess())
                                game[Mi - 1, Mj + 1].BackColor = Color.Red;
                            KingI1 += 1; KingJ1 -= 1;
                        }
                        else
                        {
                            KingI2 = Mi - 1; KingJ2 = Mj + 1;
                            if (!IfChess())
                                game[Mi - 1, Mj + 1].BackColor = Color.Red;
                            KingI2 += 1; KingJ2 -= 1;
                        }
                    }

                    if (game[Mi - 1, Mj].Name == "")
                    {
                        if (h1 == '1')
                        {
                            KingI1 = Mi - 1;
                            if (!IfChess())
                                game[Mi - 1, Mj].BackColor = Color.Green;
                            KingI1 += 1;
                        }
                        else
                        {
                            KingI2 = Mi - 1;
                            if (!IfChess())
                                game[Mi - 1, Mj].BackColor = Color.Green;
                            KingI2 += 1;
                        }
                    }
                    else if (game[Mi - 1, Mj].Name.ToString()[game[Mi - 1, Mj].Name.Length - 1] != h1)
                    {
                        if (h1 == '1')
                        {
                            KingI1 = Mi - 1;
                            if (!IfChess())
                                game[Mi - 1, Mj].BackColor = Color.Green;
                            KingI1 += 1;
                        }
                        else
                        {
                            KingI2 = Mi - 1;
                            if (!IfChess())
                                game[Mi - 1, Mj].BackColor = Color.Red;
                            KingI2 += 1;
                        }
                    }
                }
                else if (Mi == 7 && Mj == 7)
                {
                    if (game[Mi - 1, Mj].Name == "")
                    {
                        if (h1 == '1')
                        {
                            KingI1 = Mi - 1;
                            if (!IfChess())
                                game[Mi - 1, Mj].BackColor = Color.Green;
                            KingI1 += 1;
                        }
                        else
                        {
                            KingI2 = Mi - 1;
                            if (!IfChess())
                                game[Mi - 1, Mj].BackColor = Color.Green;
                            KingI2 += 1;
                        }
                    }
                    else if (game[Mi - 1, Mj].Name.ToString()[game[Mi - 1, Mj].Name.Length - 1] != h1)
                    {
                        if (h1 == '1')
                        {
                            KingI1 = Mi - 1;
                            if (!IfChess())
                                game[Mi - 1, Mj].BackColor = Color.Green;
                            KingI1 += 1;
                        }
                        else
                        {
                            KingI2 = Mi - 1;
                            if (!IfChess())
                                game[Mi - 1, Mj].BackColor = Color.Red;
                            KingI2 += 1;
                        }
                    }
                    if (game[Mi, Mj - 1].Name == "")
                    {
                        if (h1 == '1')
                        {
                            KingJ1 = Mi - 1;
                            if (!IfChess())
                                game[Mi, Mj - 1].BackColor = Color.Green;
                            KingJ1 += 1;
                        }
                        else
                        {
                            KingJ2 = Mi - 1;
                            if (!IfChess())
                                game[Mi, Mj - 1].BackColor = Color.Green;
                            KingJ2 += 1;
                        }
                    }
                    else if (game[Mi, Mj - 1].Name.ToString()[game[Mi, Mj - 1].Name.Length - 1] != h1)
                    {
                        if (h1 == '1')
                        {
                            KingJ1 = Mi - 1;
                            if (!IfChess())
                                game[Mi, Mj - 1].BackColor = Color.Red;
                            KingJ1 += 1;
                        }
                        else
                        {
                            KingJ2 = Mi - 1;
                            if (!IfChess())
                                game[Mi, Mj - 1].BackColor = Color.Red;
                            KingJ2 += 1;
                        }
                    }
                    if (game[Mi - 1, Mj - 1].Name == "")
                    {
                        if (h1 == '1')
                        {
                            KingI1 = Mi - 1;
                            KingJ1 = Mi - 1;
                            if (!IfChess())
                                game[Mi - 1, Mj - 1].BackColor = Color.Green;
                            KingJ1 += 1; KingI1 += 1;
                        }
                        else
                        {
                            KingI2 = Mi - 1;
                            KingJ2 = Mi - 1;
                            if (!IfChess())
                                game[Mi - 1, Mj - 1].BackColor = Color.Green;
                            KingJ2 += 1; KingI2 += 1;
                        }
                    }
                    else if (game[Mi - 1, Mj - 1].Name.ToString()[game[Mi - 1, Mj - 1].Name.Length - 1] != h1)
                    {
                        if (h1 == '1')
                        {
                            KingI1 = Mi - 1;
                            KingJ1 = Mi - 1;
                            if (!IfChess())
                                game[Mi - 1, Mj - 1].BackColor = Color.Red;
                            KingJ1 += 1; KingI1 += 1;
                        }
                        else
                        {
                            KingI2 = Mi - 1;
                            KingJ2 = Mi - 1;
                            if (!IfChess())
                                game[Mi - 1, Mj - 1].BackColor = Color.Red;
                            KingJ2 += 1; KingI2 += 1;
                        }
                    }
                }
                else if (Mj == 7 && Mi > 0 && Mi < 7)
                {
                    if (game[Mi - 1, Mj].Name == "")
                    {
                        if (h1 == '1')
                        {
                            KingI1 = Mi - 1;
                            if (!IfChess())
                                game[Mi - 1, Mj].BackColor = Color.Green;
                            KingI1 += 1;
                        }
                        else
                        {
                            KingI2 = Mi - 1;
                            if (!IfChess())
                                game[Mi - 1, Mj].BackColor = Color.Green;
                            KingI2 += 1;
                        }
                    }
                    else if (game[Mi - 1, Mj].Name.ToString()[game[Mi - 1, Mj].Name.Length - 1] != h1)
                    {
                        if (h1 == '1')
                        {
                            KingI1 = Mi - 1;
                            if (!IfChess())
                                game[Mi - 1, Mj].BackColor = Color.Green;
                            KingI1 += 1;
                        }
                        else
                        {
                            KingI2 = Mi - 1;
                            if (!IfChess())
                                game[Mi - 1, Mj].BackColor = Color.Red;
                            KingI2 += 1;
                        }
                    }
                    if (game[Mi, Mj - 1].Name == "")
                    {
                        if (h1 == '1')
                        {
                            KingJ1 = Mj - 1;
                            if (!IfChess())
                                game[Mi, Mj - 1].BackColor = Color.Green;
                            KingJ1 += 1;
                        }
                        else
                        {
                            KingJ2 = Mj - 1;
                            if (!IfChess())
                                game[Mi, Mj - 1].BackColor = Color.Green;
                            KingJ2 += 1;
                        }
                    }
                    else if (game[Mi, Mj - 1].Name.ToString()[game[Mi, Mj - 1].Name.Length - 1] != h1)
                    {
                        if (h1 == '1')
                        {
                            KingJ1 = Mj - 1;
                            if (!IfChess())
                                game[Mi, Mj - 1].BackColor = Color.Red;
                            KingJ1 += 1;
                        }
                        else
                        {
                            KingJ2 = Mj - 1;
                            if (!IfChess())
                                game[Mi, Mj - 1].BackColor = Color.Red;
                            KingJ2 += 1;
                        }
                    }
                    if (game[Mi - 1, Mj - 1].Name == "")
                    {
                        if (h1 == '1')
                        {
                            KingI1 = Mi - 1;
                            KingJ1 = Mj - 1;
                            if (!IfChess())
                                game[Mi - 1, Mj - 1].BackColor = Color.Green;
                            KingJ1 += 1; KingI1 += 1;
                        }
                        else
                        {
                            KingI2 = Mi - 1;
                            KingJ2 = Mj - 1;
                            if (!IfChess())
                                game[Mi - 1, Mj - 1].BackColor = Color.Green;
                            KingJ2 += 1; KingI2 += 1;
                        }
                    }
                    else if (game[Mi - 1, Mj - 1].Name.ToString()[game[Mi - 1, Mj - 1].Name.Length - 1] != h1)
                    {
                        if (h1 == '1')
                        {
                            KingI1 = Mi - 1;
                            KingJ1 = Mj - 1;
                            if (!IfChess())
                                game[Mi - 1, Mj - 1].BackColor = Color.Red;
                            KingJ1 += 1; KingI1 += 1;
                        }
                        else
                        {
                            KingI2 = Mi - 1;
                            KingJ2 = Mj - 1;
                            if (!IfChess())
                                game[Mi - 1, Mj - 1].BackColor = Color.Red;
                            KingJ2 += 1; KingI2 += 1;
                        }
                    }
                    if (game[Mi + 1, Mj].Name == "")
                    {
                        if (h1 == '1')
                        {
                            KingI1 = Mi + 1;
                            if (!IfChess())
                                game[Mi + 1, Mj].BackColor = Color.Green;
                            KingI1 -= 1;
                        }
                        else
                        {
                            KingI2 = Mi + 1;
                            if (!IfChess())
                                game[Mi + 1, Mj].BackColor = Color.Green;
                            KingI2 -= 1;
                        }
                    }
                    else if (game[Mi + 1, Mj].Name.ToString()[game[Mi + 1, Mj].Name.Length - 1] != h1)
                    {
                        if (h1 == '1')
                        {
                            KingI1 = Mi + 1;
                            if (!IfChess())
                                game[Mi + 1, Mj].BackColor = Color.Red;
                            KingI1 -= 1;
                        }
                        else
                        {
                            KingI2 = Mi + 1;
                            if (!IfChess())
                                game[Mi + 1, Mj].BackColor = Color.Red;
                            KingI2 -= 1;
                        }
                    }
                    if (game[Mi + 1, Mj - 1].Name == "")
                    {
                        if (h1 == '1')
                        {
                            KingI1 = Mi + 1; KingJ1 = Mj - 1;
                            if (!IfChess())
                                game[Mi + 1, Mj - 1].BackColor = Color.Green;
                            KingI1 -= 1; KingJ1 += 1;
                        }
                        else
                        {
                            KingI2 = Mi + 1; KingJ2 = Mj - 1;
                            if (!IfChess())
                                game[Mi + 1, Mj - 1].BackColor = Color.Green;
                            KingI2 -= 1; KingJ2 += 1;
                        }
                    }
                    else if (game[Mi + 1, Mj - 1].Name.ToString()[game[Mi + 1, Mj - 1].Name.Length - 1] != h1)
                    {
                        if (h1 == '1')
                        {
                            KingI1 = Mi + 1; KingJ1 = Mj - 1;
                            if (!IfChess())
                                game[Mi + 1, Mj - 1].BackColor = Color.Red;
                            KingI1 -= 1; KingJ1 += 1;
                        }
                        else
                        {
                            KingI2 = Mi + 1; KingJ2 = Mj - 1;
                            if (!IfChess())
                                game[Mi + 1, Mj - 1].BackColor = Color.Red;
                            KingI2 -= 1; KingJ2 += 1;
                        }
                    }
                }
                else if (Mj == 7 && Mi == 0)
                {
                    if (game[Mi, Mj - 1].Name == "")
                    {
                        if (h1 == '1')
                        {
                            KingJ1 = Mi - 1;
                            if (!IfChess())
                                game[Mi, Mj - 1].BackColor = Color.Green;
                            KingJ1 += 1;
                        }
                        else
                        {
                            KingJ2 = Mi - 1;
                            if (!IfChess())
                                game[Mi, Mj - 1].BackColor = Color.Green;
                            KingJ2 += 1;
                        }
                    }
                    else if (game[Mi, Mj - 1].Name.ToString()[game[Mi, Mj - 1].Name.Length - 1] != h1)
                    {
                        if (h1 == '1')
                        {
                            KingJ1 = Mi - 1;
                            if (!IfChess())
                                game[Mi, Mj - 1].BackColor = Color.Red;
                            KingJ1 += 1;
                        }
                        else
                        {
                            KingJ2 = Mi - 1;
                            if (!IfChess())
                                game[Mi, Mj - 1].BackColor = Color.Red;
                            KingJ2 += 1;
                        }
                    }
                    if (game[Mi + 1, Mj - 1].Name == "")
                    {
                        if (h1 == '1')
                        {
                            KingI1 = Mi + 1; KingJ1 = Mj - 1;
                            if (!IfChess())
                                game[Mi + 1, Mj - 1].BackColor = Color.Green;
                            KingI1 -= 1; KingJ1 += 1;
                        }
                        else
                        {
                            KingI2 = Mi + 1; KingJ2 = Mj - 1;
                            if (!IfChess())
                                game[Mi + 1, Mj - 1].BackColor = Color.Green;
                            KingI2 -= 1; KingJ2 += 1;
                        }
                    }
                    else if (game[Mi + 1, Mj - 1].Name.ToString()[game[Mi + 1, Mj - 1].Name.Length - 1] != h1)
                    {
                        if (h1 == '1')
                        {
                            KingI1 = Mi + 1; KingJ1 = Mj - 1;
                            if (!IfChess())
                                game[Mi + 1, Mj - 1].BackColor = Color.Red;
                            KingI1 -= 1; KingJ1 += 1;
                        }
                        else
                        {
                            KingI2 = Mi + 1; KingJ2 = Mj - 1;
                            if (!IfChess())
                                game[Mi + 1, Mj - 1].BackColor = Color.Red;
                            KingI2 -= 1; KingJ2 += 1;
                        }
                    }
                    if (game[Mi + 1, Mj].Name == "")
                    {
                        if (h1 == '1')
                        {
                            KingI1 = Mi + 1;
                            if (!IfChess())
                                game[Mi + 1, Mj].BackColor = Color.Green;
                            KingI1 -= 1;
                        }
                        else
                        {
                            KingI2 = Mi + 1;
                            if (!IfChess())
                                game[Mi + 1, Mj].BackColor = Color.Green;
                            KingI2 -= 1;
                        }
                    }
                    else if (game[Mi + 1, Mj].Name.ToString()[game[Mi + 1, Mj].Name.Length - 1] != h1)
                    {
                        if (h1 == '1')
                        {
                            KingI1 = Mi + 1;
                            if (!IfChess())
                                game[Mi + 1, Mj].BackColor = Color.Red;
                            KingI1 -= 1;
                        }
                        else
                        {
                            KingI2 = Mi + 1;
                            if (!IfChess())
                                game[Mi + 1, Mj].BackColor = Color.Red;
                            KingI2 -= 1;
                        }
                    }

                }
                else if (Mi == 0 && Mj < 7 && Mj > 0)
                {
                    if (game[Mi, Mj - 1].Name == "")
                    {
                        if (h1 == '1')
                        {
                            KingJ1 = Mi - 1;
                            if (!IfChess())
                                game[Mi, Mj - 1].BackColor = Color.Green;
                            KingJ1 += 1;
                        }
                        else
                        {
                            KingJ2 = Mi - 1;
                            if (!IfChess())
                                game[Mi, Mj - 1].BackColor = Color.Green;
                            KingJ2 += 1;
                        }
                    }
                    else if (game[Mi, Mj - 1].Name.ToString()[game[Mi, Mj - 1].Name.Length - 1] != h1)
                    {
                        if (h1 == '1')
                        {
                            KingJ1 = Mi - 1;
                            if (!IfChess())
                                game[Mi, Mj - 1].BackColor = Color.Red;
                            KingJ1 += 1;
                        }
                        else
                        {
                            KingJ2 = Mi - 1;
                            if (!IfChess())
                                game[Mi, Mj - 1].BackColor = Color.Red;
                            KingJ2 += 1;
                        }
                    }
                    if (game[Mi + 1, Mj - 1].Name == "")
                    {
                        if (h1 == '1')
                        {
                            KingI1 = Mi + 1; KingJ1 = Mj - 1;
                            if (!IfChess())
                                game[Mi + 1, Mj - 1].BackColor = Color.Green;
                            KingI1 -= 1; KingJ1 += 1;
                        }
                        else
                        {
                            KingI2 = Mi + 1; KingJ2 = Mj - 1;
                            if (!IfChess())
                                game[Mi + 1, Mj - 1].BackColor = Color.Green;
                            KingI2 -= 1; KingJ2 += 1;
                        }
                    }
                    else if (game[Mi + 1, Mj - 1].Name.ToString()[game[Mi + 1, Mj - 1].Name.Length - 1] != h1)
                    {
                        if (h1 == '1')
                        {
                            KingI1 = Mi + 1; KingJ1 = Mj - 1;
                            if (!IfChess())
                                game[Mi + 1, Mj - 1].BackColor = Color.Red;
                            KingI1 -= 1; KingJ1 += 1;
                        }
                        else
                        {
                            KingI2 = Mi + 1; KingJ2 = Mj - 1;
                            if (!IfChess())
                                game[Mi + 1, Mj - 1].BackColor = Color.Red;
                            KingI2 -= 1; KingJ2 += 1;
                        }
                    }
                    if (game[Mi + 1, Mj].Name == "")
                    {
                        if (h1 == '1')
                        {
                            KingI1 = Mi + 1;
                            if (!IfChess())
                                game[Mi + 1, Mj].BackColor = Color.Green;
                            KingI1 -= 1;
                        }
                        else
                        {
                            KingI2 = Mi + 1;
                            if (!IfChess())
                                game[Mi + 1, Mj].BackColor = Color.Green;
                            KingI2 -= 1;
                        }
                    }
                    else if (game[Mi + 1, Mj].Name.ToString()[game[Mi + 1, Mj].Name.Length - 1] != h1)
                    {
                        if (h1 == '1')
                        {
                            KingI1 = Mi + 1;
                            if (!IfChess())
                                game[Mi + 1, Mj].BackColor = Color.Red;
                            KingI1 -= 1;
                        }
                        else
                        {
                            KingI2 = Mi + 1;
                            if (!IfChess())
                                game[Mi + 1, Mj].BackColor = Color.Red;
                            KingI2 -= 1;
                        }
                    }
                    if (game[Mi, Mj + 1].Name == "")
                    {
                        if (h1 == '1')
                        {
                            KingJ1 = Mj + 1;
                            if (!IfChess())
                                game[Mi, Mj + 1].BackColor = Color.Green;
                            KingJ1 -= 1;
                        }
                        else
                        {
                            KingJ2 = Mj + 1;
                            if (!IfChess())
                                game[Mi, Mj + 1].BackColor = Color.Green;
                            KingJ2 -= 1;
                        }
                    }
                    else if (game[Mi, Mj + 1].Name.ToString()[game[Mi, Mj + 1].Name.Length - 1] != h1)
                    {
                        if (h1 == '1')
                        {
                            KingJ1 = Mj + 1;
                            if (!IfChess())
                                game[Mi, Mj + 1].BackColor = Color.Red;
                            KingJ1 -= 1;
                        }
                        else
                        {
                            KingJ2 = Mj + 1;
                            if (!IfChess())
                                game[Mi, Mj + 1].BackColor = Color.Red;
                            KingJ2 -= 1;
                        }
                    }
                    if (game[Mi + 1, Mj + 1].Name == "")
                    {
                        if (h1 == '1')
                        {
                            KingI1 = Mi + 1; KingJ1 = Mj + 1;
                            if (!IfChess())
                                game[Mi + 1, Mj + 1].BackColor = Color.Green;
                            KingI1 -= 1; KingJ1 -= 1;
                        }
                        else
                        {
                            KingI2 = Mi + 1; KingJ2 = Mj + 1;
                            if (!IfChess())
                                game[Mi + 1, Mj + 1].BackColor = Color.Green;
                            KingI2 -= 1; KingJ2 -= 1;
                        }
                    }
                    else if (game[Mi + 1, Mj + 1].Name.ToString()[game[Mi + 1, Mj + 1].Name.Length - 1] != h1)
                    {
                        if (h1 == '1')
                        {
                            KingI1 = Mi + 1; KingJ1 = Mj + 1;
                            if (!IfChess())
                                game[Mi + 1, Mj + 1].BackColor = Color.Red;
                            KingI1 -= 1; KingJ1 -= 1;
                        }
                        else
                        {
                            KingI2 = Mi + 1; KingJ2 = Mj + 1;
                            if (!IfChess())
                                game[Mi + 1, Mj + 1].BackColor = Color.Red;
                            KingI2 -= 1; KingJ2 -= 1;
                        }
                    }
                }
                else if (Mj == 0 && Mi > 0 && Mi < 7)
                {
                    if (game[Mi - 1, Mj + 1].Name == "")
                    {
                        if (h1 == '1')
                        {
                            KingI1 = Mi - 1; KingJ1 = Mj + 1;
                            if (!IfChess())
                                game[Mi - 1, Mj + 1].BackColor = Color.Green;
                            KingI1 += 1; KingJ1 -= 1;
                        }
                        else
                        {
                            KingI2 = Mi - 1; KingJ2 = Mj + 1;
                            if (!IfChess())
                                game[Mi - 1, Mj + 1].BackColor = Color.Green;
                            KingI2 += 1; KingJ2 -= 1;
                        }
                    }
                    else if (game[Mi - 1, Mj + 1].Name.ToString()[game[Mi - 1, Mj + 1].Name.Length - 1] != h1)
                    {
                        if (h1 == '1')
                        {
                            KingI1 = Mi - 1; KingJ1 = Mj + 1;
                            if (!IfChess())
                                game[Mi - 1, Mj + 1].BackColor = Color.Red;
                            KingI1 += 1; KingJ1 -= 1;
                        }
                        else
                        {
                            KingI2 = Mi - 1; KingJ2 = Mj + 1;
                            if (!IfChess())
                                game[Mi - 1, Mj + 1].BackColor = Color.Red;
                            KingI2 += 1; KingJ2 -= 1;
                        }
                    }
                    if (game[Mi - 1, Mj].Name == "")
                    {
                        if (h1 == '1')
                        {
                            KingI1 = Mi - 1;
                            if (!IfChess())
                                game[Mi - 1, Mj].BackColor = Color.Green;
                            KingI1 += 1;
                        }
                        else
                        {
                            KingI2 = Mi - 1;
                            if (!IfChess())
                                game[Mi - 1, Mj].BackColor = Color.Green;
                            KingI2 += 1;
                        }
                    }
                    else if (game[Mi - 1, Mj].Name.ToString()[game[Mi - 1, Mj].Name.Length - 1] != h1)
                    {
                        if (h1 == '1')
                        {
                            KingI1 = Mi - 1;
                            if (!IfChess())
                                game[Mi - 1, Mj].BackColor = Color.Green;
                            KingI1 += 1;
                        }
                        else
                        {
                            KingI2 = Mi - 1;
                            if (!IfChess())
                                game[Mi - 1, Mj].BackColor = Color.Red;
                            KingI2 += 1;
                        }
                    }

                    if (game[Mi + 1, Mj].Name == "")
                    {
                        if (h1 == '1')
                        {
                            KingI1 = Mi + 1;
                            if (!IfChess())
                                game[Mi + 1, Mj].BackColor = Color.Green;
                            KingI1 -= 1;
                        }
                        else
                        {
                            KingI2 = Mi + 1;
                            if (!IfChess())
                                game[Mi + 1, Mj].BackColor = Color.Green;
                            KingI2 -= 1;
                        }
                    }
                    else if (game[Mi + 1, Mj].Name.ToString()[game[Mi + 1, Mj].Name.Length - 1] != h1)
                    {
                        if (h1 == '1')
                        {
                            KingI1 = Mi + 1;
                            if (!IfChess())
                                game[Mi + 1, Mj].BackColor = Color.Red;
                            KingI1 -= 1;
                        }
                        else
                        {
                            KingI2 = Mi + 1;
                            if (!IfChess())
                                game[Mi + 1, Mj].BackColor = Color.Red;
                            KingI2 -= 1;
                        }
                    }
                    if (game[Mi, Mj + 1].Name == "")
                    {
                        if (h1 == '1')
                        {
                            KingJ1 = Mj + 1;
                            if (!IfChess())
                                game[Mi, Mj + 1].BackColor = Color.Green;
                            KingJ1 -= 1;
                        }
                        else
                        {
                            KingJ2 = Mj + 1;
                            if (!IfChess())
                                game[Mi, Mj + 1].BackColor = Color.Green;
                            KingJ2 -= 1;
                        }
                    }
                    else if (game[Mi, Mj + 1].Name.ToString()[game[Mi, Mj + 1].Name.Length - 1] != h1)
                    {
                        if (h1 == '1')
                        {
                            KingJ1 = Mj + 1;
                            if (!IfChess())
                                game[Mi, Mj + 1].BackColor = Color.Red;
                            KingJ1 -= 1;
                        }
                        else
                        {
                            KingJ2 = Mj + 1;
                            if (!IfChess())
                                game[Mi, Mj + 1].BackColor = Color.Red;
                            KingJ2 -= 1;
                        }
                    }
                    if (game[Mi + 1, Mj + 1].Name == "")
                    {
                        if (h1 == '1')
                        {
                            KingI1 = Mi + 1; KingJ1 = Mj + 1;
                            if (!IfChess())
                                game[Mi + 1, Mj + 1].BackColor = Color.Green;
                            KingI1 -= 1; KingJ1 -= 1;
                        }
                        else
                        {
                            KingI2 = Mi + 1; KingJ2 = Mj + 1;
                            if (!IfChess())
                                game[Mi + 1, Mj + 1].BackColor = Color.Green;
                            KingI2 -= 1; KingJ2 -= 1;
                        }
                    }
                    else if (game[Mi + 1, Mj + 1].Name.ToString()[game[Mi + 1, Mj + 1].Name.Length - 1] != h1)
                    {
                        if (h1 == '1')
                        {
                            KingI1 = Mi + 1; KingJ1 = Mj + 1;
                            if (!IfChess())
                                game[Mi + 1, Mj + 1].BackColor = Color.Red;
                            KingI1 -= 1; KingJ1 -= 1;
                        }
                        else
                        {
                            KingI2 = Mi + 1; KingJ2 = Mj + 1;
                            if (!IfChess())
                                game[Mi + 1, Mj + 1].BackColor = Color.Red;
                            KingI2 -= 1; KingJ2 -= 1;
                        }
                    }
                }
                else if (Mj == 0 && Mi == 0)
                {
                    if (game[Mi + 1, Mj].Name == "")
                    {
                        if (h1 == '1')
                        {
                            KingI1 = Mi + 1;
                            if (!IfChess())
                                game[Mi + 1, Mj].BackColor = Color.Green;
                            KingI1 -= 1;
                        }
                        else
                        {
                            KingI2 = Mi + 1;
                            if (!IfChess())
                                game[Mi + 1, Mj].BackColor = Color.Green;
                            KingI2 -= 1;
                        }
                    }
                    else if (game[Mi + 1, Mj].Name.ToString()[game[Mi + 1, Mj].Name.Length - 1] != h1)
                    {
                        if (h1 == '1')
                        {
                            KingI1 = Mi + 1;
                            if (!IfChess())
                                game[Mi + 1, Mj].BackColor = Color.Red;
                            KingI1 -= 1;
                        }
                        else
                        {
                            KingI2 = Mi + 1;
                            if (!IfChess())
                                game[Mi + 1, Mj].BackColor = Color.Red;
                            KingI2 -= 1;
                        }
                    }
                    if (game[Mi, Mj + 1].Name == "")
                    {
                        if (h1 == '1')
                        {
                            KingJ1 = Mj + 1;
                            if (!IfChess())
                                game[Mi, Mj + 1].BackColor = Color.Green;
                            KingJ1 -= 1;
                        }
                        else
                        {
                            KingJ2 = Mj + 1;
                            if (!IfChess())
                                game[Mi, Mj + 1].BackColor = Color.Green;
                            KingJ2 -= 1;
                        }
                    }
                    else if (game[Mi, Mj + 1].Name.ToString()[game[Mi, Mj + 1].Name.Length - 1] != h1)
                    {
                        if (h1 == '1')
                        {
                            KingJ1 = Mj + 1;
                            if (!IfChess())
                                game[Mi, Mj + 1].BackColor = Color.Red;
                            KingJ1 -= 1;
                        }
                        else
                        {
                            KingJ2 = Mj + 1;
                            if (!IfChess())
                                game[Mi, Mj + 1].BackColor = Color.Red;
                            KingJ2 -= 1;
                        }
                    }
                    if (game[Mi + 1, Mj + 1].Name == "")
                    {
                        if (h1 == '1')
                        {
                            KingI1 = Mi + 1; KingJ1 = Mj + 1;
                            if (!IfChess())
                                game[Mi + 1, Mj + 1].BackColor = Color.Green;
                            KingI1 -= 1; KingJ1 -= 1;
                        }
                        else
                        {
                            KingI2 = Mi + 1; KingJ2 = Mj + 1;
                            if (!IfChess())
                                game[Mi + 1, Mj + 1].BackColor = Color.Green;
                            KingI2 -= 1; KingJ2 -= 1;
                        }
                    }
                    else if (game[Mi + 1, Mj + 1].Name.ToString()[game[Mi + 1, Mj + 1].Name.Length - 1] != h1)
                    {
                        if (h1 == '1')
                        {
                            KingI1 = Mi + 1; KingJ1 = Mj + 1;
                            if (!IfChess())
                                game[Mi + 1, Mj + 1].BackColor = Color.Red;
                            KingI1 -= 1; KingJ1 -= 1;
                        }
                        else
                        {
                            KingI2 = Mi + 1; KingJ2 = Mj + 1;
                            if (!IfChess())
                                game[Mi + 1, Mj + 1].BackColor = Color.Red;
                            KingI2 -= 1; KingJ2 -= 1;
                        }
                    }
                }
                //לביצוע הצרחה-Castling
                if (h1 == '2' && !isCastling2 && Mi == 4)
                {
                    for (int g = 1; g < 5 && Mi - g > -1 && !isRook21; g++)
                    {
                        if (game[Mi - g, Mj].Name != "")
                            if (game[Mi - g, Mj].Name == img1[0].ToString() && Mi - g == 0)
                            {
                                if (h1 == '1')
                                {
                                    KingI1 = Mi - g + 1;
                                    if (!IfChess())
                                        game[Mi - g + 1, Mj].BackColor = Color.Aqua;
                                    KingI1 += g - 1;
                                }
                                else
                                {
                                    KingI2 = Mi - g + 1;
                                    if (!IfChess())
                                        game[Mi - g + 1, Mj].BackColor = Color.Aqua;
                                    KingI2 += g - 1;
                                }
                            }
                            else break;
                    }
                    for (int g = 1; g < 4 && Mi + g < 8 && !isRook22 && game[Mi - g, Mj].Name == ""; g++)
                    {
                        if (game[Mi + g, Mj].Name != "")
                            if (game[Mi + g, Mj].Name == img1[0].ToString() && Mi + g == 7)
                            {
                                if (h1 == '1')
                                {
                                    KingI1 = Mi + g - 1;
                                    if (!IfChess())
                                        game[Mi + g - 1, Mj].BackColor = Color.Aqua;
                                    KingI1 -= g + 1;
                                }
                                else
                                {
                                    KingI2 = Mi + g - 1;
                                    if (!IfChess())
                                        game[Mi + g - 1, Mj].BackColor = Color.Aqua;
                                    KingI2 -= g + 1;
                                }
                            }
                            else break;
                    }
                }
                else if (!isCastling1 && Mi == 4)
                {
                    for (int g = 1; g < 5 && Mi - g > -1 && !isRook11; g++)
                    {
                        if (game[Mi - g, Mj].Name != "")
                            if (game[Mi - g, Mj].Name == img2[0].ToString() && Mi - g == 0)
                            {
                                if (h1 == '1')
                                {
                                    KingI1 = Mi - g + 1;
                                    if (!IfChess())
                                        game[Mi - g + 1, Mj].BackColor = Color.Aqua;
                                    KingI1 += g - 1;
                                }
                                else
                                {
                                    KingI2 = Mi - g + 1;
                                    if (!IfChess())
                                        game[Mi - g + 1, Mj].BackColor = Color.Aqua;
                                    KingI2 += g - 1;
                                }
                            }
                            else break;
                    }
                    for (int g = 1; g < 4 && Mi + g < 8 && !isRook12; g++)
                    {
                        if (game[Mi + g, Mj].Name != "")
                            if (game[Mi + g, Mj].Name == img2[0].ToString() && Mi + g == 7)
                            {
                                if (h1 == '1')
                                {
                                    KingI1 = Mi + g - 1;
                                    if (!IfChess())
                                        game[Mi + g - 1, Mj].BackColor = Color.Aqua;
                                    KingI1 -= g + 1;
                                }
                                else
                                {
                                    KingI2 = Mi + g - 1;
                                    if (!IfChess())
                                        game[Mi + g - 1, Mj].BackColor = Color.Aqua;
                                    KingI2 -= g + 1;
                                }
                            }
                            else break;
                    }
                }
            }
            else
            {
                if (game[Mi, Mj].Name.Contains("מלך"))//לא כול כך הגיוני-- Pat נראה
                { IsEnabled(); return; }
                if (Mi > 0 && Mi < 7 && Mj > 0 && Mj < 7)
                {
                    if (game[Mi - 1, Mj].Name == C.Name)
                    {
                        if (IfChess())
                        {
                            s = game[Mi, Mj].Name;
                            game[Mi, Mj].Name = C.Name;
                            if (!IfChess())
                            {
                                game[Mi, Mj].Name = s;
                                if (h1 == '1')
                                {
                                    KingI1 = Mi;
                                    KingJ1 = Mj;
                                }
                                else
                                {
                                    KingI2 = Mi;
                                    KingJ2 = Mj;
                                }
                                if (game[Mi, Mj].Name == "")
                                {
                                    game[Mi, Mj].Name = C.Name;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;


                                    if (h1 == '1')
                                    {
                                        isCastling1 = true;
                                        h1 = '2';
                                    }
                                    else
                                    {
                                        h1 = '1';
                                        isCastling2 = true;
                                    }
                                }
                                else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                                {
                                    game[Mi, Mj].BackColor = Color.Black;
                                    game[Mi, Mj].Name = C.Name;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;



                                    if (h1 == '1')
                                    {
                                        isCastling1 = true;
                                        h1 = '2';
                                    }
                                    else
                                    {
                                        h1 = '1';
                                        isCastling2 = true;
                                    }
                                }
                                isFind = true;
                            }
                            else game[Mi, Mj].Name = s;
                        }
                        else
                        {
                            if (h1 == '1')
                            {
                                KingI1 = Mi;
                                KingJ1 = Mj;
                            }
                            else
                            {
                                KingI2 = Mi;
                                KingJ2 = Mj;
                            }
                            if (game[Mi, Mj].Name == "")
                            {
                                game[Mi, Mj].Name = C.Name;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                C.Text = "";
                                if (h1 == '1')
                                {
                                    isCastling1 = true;
                                    h1 = '2';
                                }
                                else
                                {
                                    h1 = '1';
                                    isCastling2 = true;
                                }
                            }
                            else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                            {
                                game[Mi, Mj].BackColor = Color.Black;
                                game[Mi, Mj].Name = C.Name;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                C.Text = "";

                                if (h1 == '1')
                                {
                                    isCastling1 = true;
                                    h1 = '2';
                                }
                                else
                                {
                                    h1 = '1';
                                    isCastling2 = true;
                                }
                            }
                            isFind = true;
                        }
                    }
                    else if (game[Mi, Mj - 1].Name == C.Name)
                    {
                        if (IfChess())
                        {
                            s = game[Mi, Mj].Name;
                            game[Mi, Mj].Name = C.Name;
                            if (!IfChess())
                            {
                                game[Mi, Mj].Name = s;
                                if (h1 == '1')
                                {
                                    KingI1 = Mi;
                                    KingJ1 = Mj;
                                }
                                else
                                {
                                    KingI2 = Mi;
                                    KingJ2 = Mj;
                                }
                                if (game[Mi, Mj].Name == "")
                                {
                                    game[Mi, Mj].Name = C.Name;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;


                                    if (h1 == '1')
                                    {
                                        isCastling1 = true;
                                        h1 = '2';
                                    }
                                    else
                                    {
                                        h1 = '1';
                                        isCastling2 = true;
                                    }
                                }
                                else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                                {
                                    game[Mi, Mj].BackColor = Color.Black;
                                    game[Mi, Mj].Name = C.Name;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;



                                    if (h1 == '1')
                                    {
                                        isCastling1 = true;
                                        h1 = '2';
                                    }
                                    else
                                    {
                                        h1 = '1';
                                        isCastling2 = true;
                                    }
                                }
                                isFind = true;
                            }
                            else game[Mi, Mj].Name = s;
                        }
                        else
                        {
                            if (h1 == '1')
                            {
                                KingI1 = Mi;
                                KingJ1 = Mj;
                            }
                            else
                            {
                                KingI2 = Mi;
                                KingJ2 = Mj;
                            }
                            if (game[Mi, Mj].Name == "")
                            {
                                game[Mi, Mj].Name = C.Name;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                C.Text = "";
                                if (h1 == '1')
                                {
                                    isCastling1 = true;
                                    h1 = '2';
                                }
                                else
                                {
                                    h1 = '1';
                                    isCastling2 = true;
                                }
                            }
                            else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                            {
                                game[Mi, Mj].BackColor = Color.Black;
                                game[Mi, Mj].Name = C.Name;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                C.Text = "";

                                if (h1 == '1')
                                {
                                    isCastling1 = true;
                                    h1 = '2';
                                }
                                else
                                {
                                    h1 = '1';
                                    isCastling2 = true;
                                }
                            }
                            isFind = true;
                        }
                    }
                    else if (game[Mi - 1, Mj - 1].Name == C.Name)
                    {
                        if (IfChess())
                        {
                            s = game[Mi, Mj].Name;
                            game[Mi, Mj].Name = C.Name;
                            if (!IfChess())
                            {
                                game[Mi, Mj].Name = s;
                                if (h1 == '1')
                                {
                                    KingI1 = Mi;
                                    KingJ1 = Mj;
                                }
                                else
                                {
                                    KingI2 = Mi;
                                    KingJ2 = Mj;
                                }
                                if (game[Mi, Mj].Name == "")
                                {
                                    game[Mi, Mj].Name = C.Name;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;


                                    if (h1 == '1')
                                    {
                                        isCastling1 = true;
                                        h1 = '2';
                                    }
                                    else
                                    {
                                        h1 = '1';
                                        isCastling2 = true;
                                    }
                                }
                                else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                                {
                                    game[Mi, Mj].BackColor = Color.Black;
                                    game[Mi, Mj].Name = C.Name;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;



                                    if (h1 == '1')
                                    {
                                        isCastling1 = true;
                                        h1 = '2';
                                    }
                                    else
                                    {
                                        h1 = '1';
                                        isCastling2 = true;
                                    }
                                }
                                isFind = true;
                            }
                            else game[Mi, Mj].Name = s;
                        }
                        else
                        {
                            if (h1 == '1')
                            {
                                KingI1 = Mi;
                                KingJ1 = Mj;
                            }
                            else
                            {
                                KingI2 = Mi;
                                KingJ2 = Mj;
                            }
                            if (game[Mi, Mj].Name == "")
                            {
                                game[Mi, Mj].Name = C.Name;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                C.Text = "";
                                if (h1 == '1')
                                {
                                    isCastling1 = true;
                                    h1 = '2';
                                }
                                else
                                {
                                    h1 = '1';
                                    isCastling2 = true;
                                }
                            }
                            else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                            {
                                game[Mi, Mj].BackColor = Color.Black;
                                game[Mi, Mj].Name = C.Name;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                C.Text = "";

                                if (h1 == '1')
                                {
                                    isCastling1 = true;
                                    h1 = '2';
                                }
                                else
                                {
                                    h1 = '1';
                                    isCastling2 = true;
                                }
                            }
                            isFind = true;
                        }
                    }
                    else if (game[Mi + 1, Mj].Name == C.Name)
                    {
                        if (IfChess())
                        {
                            s = game[Mi, Mj].Name;
                            game[Mi, Mj].Name = C.Name;
                            if (!IfChess())
                            {
                                game[Mi, Mj].Name = s;
                                if (h1 == '1')
                                {
                                    KingI1 = Mi;
                                    KingJ1 = Mj;
                                }
                                else
                                {
                                    KingI2 = Mi;
                                    KingJ2 = Mj;
                                }
                                if (game[Mi, Mj].Name == "")
                                {
                                    game[Mi, Mj].Name = C.Name;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;


                                    if (h1 == '1')
                                    {
                                        isCastling1 = true;
                                        h1 = '2';
                                    }
                                    else
                                    {
                                        h1 = '1';
                                        isCastling2 = true;
                                    }
                                }
                                else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                                {
                                    game[Mi, Mj].BackColor = Color.Black;
                                    game[Mi, Mj].Name = C.Name;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;
                                    if (h1 == '1')
                                    {
                                        isCastling1 = true;
                                        h1 = '2';
                                    }
                                    else
                                    {
                                        h1 = '1';
                                        isCastling2 = true;
                                    }
                                }
                                isFind = true;
                            }
                            else game[Mi, Mj].Name = s;
                        }
                        else
                        {
                            if (h1 == '1')
                            {
                                KingI1 = Mi;
                                KingJ1 = Mj;
                            }
                            else
                            {
                                KingI2 = Mi;
                                KingJ2 = Mj;
                            }
                            if (game[Mi, Mj].Name == "")
                            {
                                game[Mi, Mj].Name = C.Name;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                C.Text = "";
                                if (h1 == '1')
                                {
                                    isCastling1 = true;
                                    h1 = '2';
                                }
                                else
                                {
                                    h1 = '1';
                                    isCastling2 = true;
                                }
                            }
                            else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                            {
                                game[Mi, Mj].BackColor = Color.Black;
                                game[Mi, Mj].Name = C.Name;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                C.Text = "";

                                if (h1 == '1')
                                {
                                    isCastling1 = true;
                                    h1 = '2';
                                }
                                else
                                {
                                    h1 = '1';
                                    isCastling2 = true;
                                }
                            }
                            isFind = true;
                        }
                    }
                    else if (game[Mi, Mj + 1].Name == C.Name)
                    {
                        if (IfChess())
                        {
                            s = game[Mi, Mj].Name;
                            game[Mi, Mj].Name = C.Name;
                            if (!IfChess())
                            {
                                game[Mi, Mj].Name = s;
                                if (h1 == '1')
                                {
                                    KingI1 = Mi;
                                    KingJ1 = Mj;
                                }
                                else
                                {
                                    KingI2 = Mi;
                                    KingJ2 = Mj;
                                }
                                if (game[Mi, Mj].Name == "")
                                {
                                    game[Mi, Mj].Name = C.Name;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;


                                    if (h1 == '1')
                                    {
                                        isCastling1 = true;
                                        h1 = '2';
                                    }
                                    else
                                    {
                                        h1 = '1';
                                        isCastling2 = true;
                                    }
                                }
                                else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                                {
                                    game[Mi, Mj].BackColor = Color.Black;
                                    game[Mi, Mj].Name = C.Name;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;



                                    if (h1 == '1')
                                    {
                                        isCastling1 = true;
                                        h1 = '2';
                                    }
                                    else
                                    {
                                        h1 = '1';
                                        isCastling2 = true;
                                    }
                                }
                                isFind = true;
                            }
                            else game[Mi, Mj].Name = s;
                        }
                        else
                        {
                            if (h1 == '1')
                            {
                                KingI1 = Mi;
                                KingJ1 = Mj;
                            }
                            else
                            {
                                KingI2 = Mi;
                                KingJ2 = Mj;
                            }
                            if (game[Mi, Mj].Name == "")
                            {
                                game[Mi, Mj].Name = C.Name;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                C.Text = "";
                                if (h1 == '1')
                                {
                                    isCastling1 = true;
                                    h1 = '2';
                                }
                                else
                                {
                                    h1 = '1';
                                    isCastling2 = true;
                                }
                            }
                            else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                            {
                                game[Mi, Mj].BackColor = Color.Black;
                                game[Mi, Mj].Name = C.Name;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                C.Text = "";

                                if (h1 == '1')
                                {
                                    isCastling1 = true;
                                    h1 = '2';
                                }
                                else
                                {
                                    h1 = '1';
                                    isCastling2 = true;
                                }
                            }
                            isFind = true;
                        }
                    }
                    else if (game[Mi - 1, Mj + 1].Name == C.Name)
                    {
                        if (IfChess())
                        {
                            s = game[Mi, Mj].Name;
                            game[Mi, Mj].Name = C.Name;
                            if (!IfChess())
                            {
                                game[Mi, Mj].Name = s;
                                if (h1 == '1')
                                {
                                    KingI1 = Mi;
                                    KingJ1 = Mj;
                                }
                                else
                                {
                                    KingI2 = Mi;
                                    KingJ2 = Mj;
                                }
                                if (game[Mi, Mj].Name == "")
                                {
                                    game[Mi, Mj].Name = C.Name;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;


                                    if (h1 == '1')
                                    {
                                        isCastling1 = true;
                                        h1 = '2';
                                    }
                                    else
                                    {
                                        h1 = '1';
                                        isCastling2 = true;
                                    }
                                }
                                else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                                {
                                    game[Mi, Mj].BackColor = Color.Black;
                                    game[Mi, Mj].Name = C.Name;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;



                                    if (h1 == '1')
                                    {
                                        isCastling1 = true;
                                        h1 = '2';
                                    }
                                    else
                                    {
                                        h1 = '1';
                                        isCastling2 = true;
                                    }
                                }
                                isFind = true;
                            }
                            else game[Mi, Mj].Name = s;
                        }
                        else
                        {
                            if (h1 == '1')
                            {
                                KingI1 = Mi;
                                KingJ1 = Mj;
                            }
                            else
                            {
                                KingI2 = Mi;
                                KingJ2 = Mj;
                            }
                            if (game[Mi, Mj].Name == "")
                            {
                                game[Mi, Mj].Name = C.Name;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                C.Text = "";
                                if (h1 == '1')
                                {
                                    isCastling1 = true;
                                    h1 = '2';
                                }
                                else
                                {
                                    h1 = '1';
                                    isCastling2 = true;
                                }
                            }
                            else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                            {
                                game[Mi, Mj].BackColor = Color.Black;
                                game[Mi, Mj].Name = C.Name;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                C.Text = "";

                                if (h1 == '1')
                                {
                                    isCastling1 = true;
                                    h1 = '2';
                                }
                                else
                                {
                                    h1 = '1';
                                    isCastling2 = true;
                                }
                            }
                            isFind = true;
                        }
                    }
                    else if (game[Mi + 1, Mj - 1].Name == C.Name)
                    {
                        if (IfChess())
                        {
                            s = game[Mi, Mj].Name;
                            game[Mi, Mj].Name = C.Name;
                            if (!IfChess())
                            {
                                game[Mi, Mj].Name = s;
                                if (h1 == '1')
                                {
                                    KingI1 = Mi;
                                    KingJ1 = Mj;
                                }
                                else
                                {
                                    KingI2 = Mi;
                                    KingJ2 = Mj;
                                }
                                if (game[Mi, Mj].Name == "")
                                {
                                    game[Mi, Mj].Name = C.Name;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;


                                    if (h1 == '1')
                                    {
                                        isCastling1 = true;
                                        h1 = '2';
                                    }
                                    else
                                    {
                                        h1 = '1';
                                        isCastling2 = true;
                                    }
                                }
                                else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                                {
                                    game[Mi, Mj].BackColor = Color.Black;
                                    game[Mi, Mj].Name = C.Name;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;



                                    if (h1 == '1')
                                    {
                                        isCastling1 = true;
                                        h1 = '2';
                                    }
                                    else
                                    {
                                        h1 = '1';
                                        isCastling2 = true;
                                    }
                                }
                                isFind = true;
                            }
                            else game[Mi, Mj].Name = s;
                        }
                        else
                        {
                            if (h1 == '1')
                            {
                                KingI1 = Mi;
                                KingJ1 = Mj;
                            }
                            else
                            {
                                KingI2 = Mi;
                                KingJ2 = Mj;
                            }
                            if (game[Mi, Mj].Name == "")
                            {
                                game[Mi, Mj].Name = C.Name;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                C.Text = "";
                                if (h1 == '1')
                                {
                                    isCastling1 = true;
                                    h1 = '2';
                                }
                                else
                                {
                                    h1 = '1';
                                    isCastling2 = true;
                                }
                            }
                            else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                            {
                                game[Mi, Mj].BackColor = Color.Black;
                                game[Mi, Mj].Name = C.Name;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                C.Text = "";

                                if (h1 == '1')
                                {
                                    isCastling1 = true;
                                    h1 = '2';
                                }
                                else
                                {
                                    h1 = '1';
                                    isCastling2 = true;
                                }
                            }
                            isFind = true;
                        }
                    }
                }
                else if (Mi == 7 && Mj > 0 && Mj < 7)
                {
                    if (game[Mi, Mj - 1].Name == C.Name)
                    {
                        if (!IfChess())
                        {
                            s = game[Mi, Mj].Name;
                            game[Mi, Mj].Name = C.Name;
                            if (!IfChess())
                            {
                                game[Mi, Mj].Name = s;
                                if (h1 == '1')
                                {
                                    KingI1 = Mi;
                                    KingJ1 = Mj;
                                }
                                else
                                {
                                    KingI2 = Mi;
                                    KingJ2 = Mj;
                                }
                                if (game[Mi, Mj].Name == "")
                                {
                                    game[Mi, Mj].Name = C.Name;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;


                                    if (h1 == '1')
                                    {
                                        isCastling1 = true;
                                        h1 = '2';
                                    }
                                    else
                                    {
                                        h1 = '1';
                                        isCastling2 = true;
                                    }
                                }
                                else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                                {
                                    game[Mi, Mj].BackColor = Color.Black;
                                    game[Mi, Mj].Name = C.Name;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;



                                    if (h1 == '1')
                                    {
                                        isCastling1 = true;
                                        h1 = '2';
                                    }
                                    else
                                    {
                                        h1 = '1';
                                        isCastling2 = true;
                                    }
                                }
                                isFind = true;
                            }
                            else game[Mi, Mj].Name = s;
                        }
                        else
                        {
                            if (h1 == '1')
                            {
                                KingI1 = Mi;
                                KingJ1 = Mj;
                            }
                            else
                            {
                                KingI2 = Mi;
                                KingJ2 = Mj;
                            }
                            if (game[Mi, Mj].Name == "")
                            {
                                game[Mi, Mj].Name = C.Name;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                C.Text = "";
                                if (h1 == '1')
                                {
                                    isCastling1 = true;
                                    h1 = '2';
                                }
                                else
                                {
                                    h1 = '1';
                                    isCastling2 = true;
                                }
                            }
                            else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                            {
                                game[Mi, Mj].BackColor = Color.Black;
                                game[Mi, Mj].Name = C.Name;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                C.Text = "";

                                if (h1 == '1')
                                {
                                    isCastling1 = true;
                                    h1 = '2';
                                }
                                else
                                {
                                    h1 = '1';
                                    isCastling2 = true;
                                }
                            }
                            isFind = true;
                        }
                    }
                    else if (game[Mi, Mj + 1].Name == C.Name)
                    {
                        if (IfChess())
                        {
                            s = game[Mi, Mj].Name;
                            game[Mi, Mj].Name = C.Name;
                            if (!IfChess())
                            {
                                game[Mi, Mj].Name = s;
                                if (h1 == '1')
                                {
                                    KingI1 = Mi;
                                    KingJ1 = Mj;
                                }
                                else
                                {
                                    KingI2 = Mi;
                                    KingJ2 = Mj;
                                }
                                if (game[Mi, Mj].Name == "")
                                {
                                    game[Mi, Mj].Name = C.Name;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;


                                    if (h1 == '1')
                                    {
                                        isCastling1 = true;
                                        h1 = '2';
                                    }
                                    else
                                    {
                                        h1 = '1';
                                        isCastling2 = true;
                                    }
                                }
                                else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                                {
                                    game[Mi, Mj].BackColor = Color.Black;
                                    game[Mi, Mj].Name = C.Name;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;



                                    if (h1 == '1')
                                    {
                                        isCastling1 = true;
                                        h1 = '2';
                                    }
                                    else
                                    {
                                        h1 = '1';
                                        isCastling2 = true;
                                    }
                                }
                                isFind = true;
                            }
                            else game[Mi, Mj].Name = s;
                        }
                        else
                        {
                            if (h1 == '1')
                            {
                                KingI1 = Mi;
                                KingJ1 = Mj;
                            }
                            else
                            {
                                KingI2 = Mi;
                                KingJ2 = Mj;
                            }
                            if (game[Mi, Mj].Name == "")
                            {
                                game[Mi, Mj].Name = C.Name;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                C.Text = "";
                                if (h1 == '1')
                                {
                                    isCastling1 = true;
                                    h1 = '2';
                                }
                                else
                                {
                                    h1 = '1';
                                    isCastling2 = true;
                                }
                            }
                            else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                            {
                                game[Mi, Mj].BackColor = Color.Black;
                                game[Mi, Mj].Name = C.Name;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                C.Text = "";

                                if (h1 == '1')
                                {
                                    isCastling1 = true;
                                    h1 = '2';
                                }
                                else
                                {
                                    h1 = '1';
                                    isCastling2 = true;
                                }
                            }
                            isFind = true;
                        }
                    }
                    else if (game[Mi - 1, Mj].Name == C.Name)
                    {
                        if (IfChess())
                        {
                            s = game[Mi, Mj].Name;
                            game[Mi, Mj].Name = C.Name;
                            if (!IfChess())
                            {
                                game[Mi, Mj].Name = s;
                                if (h1 == '1')
                                {
                                    KingI1 = Mi;
                                    KingJ1 = Mj;
                                }
                                else
                                {
                                    KingI2 = Mi;
                                    KingJ2 = Mj;
                                }
                                if (game[Mi, Mj].Name == "")
                                {
                                    game[Mi, Mj].Name = C.Name;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;


                                    if (h1 == '1')
                                    {
                                        isCastling1 = true;
                                        h1 = '2';
                                    }
                                    else
                                    {
                                        h1 = '1';
                                        isCastling2 = true;
                                    }
                                }
                                else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                                {
                                    game[Mi, Mj].BackColor = Color.Black;
                                    game[Mi, Mj].Name = C.Name;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;



                                    if (h1 == '1')
                                    {
                                        isCastling1 = true;
                                        h1 = '2';
                                    }
                                    else
                                    {
                                        h1 = '1';
                                        isCastling2 = true;
                                    }
                                }
                                isFind = true;
                            }
                            else game[Mi, Mj].Name = s;
                        }
                        else
                        {
                            if (h1 == '1')
                            {
                                KingI1 = Mi;
                                KingJ1 = Mj;
                            }
                            else
                            {
                                KingI2 = Mi;
                                KingJ2 = Mj;
                            }
                            if (game[Mi, Mj].Name == "")
                            {
                                game[Mi, Mj].Name = C.Name;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                C.Text = "";
                                if (h1 == '1')
                                {
                                    isCastling1 = true;
                                    h1 = '2';
                                }
                                else
                                {
                                    h1 = '1';
                                    isCastling2 = true;
                                }
                            }
                            else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                            {
                                game[Mi, Mj].BackColor = Color.Black;
                                game[Mi, Mj].Name = C.Name;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                C.Text = "";

                                if (h1 == '1')
                                {
                                    isCastling1 = true;
                                    h1 = '2';
                                }
                                else
                                {
                                    h1 = '1';
                                    isCastling2 = true;
                                }
                            }
                            isFind = true;
                        }
                    }
                    else if (game[Mi - 1, Mj - 1].Name == C.Name)
                    {
                        if (IfChess())
                        {
                            s = game[Mi, Mj].Name;
                            game[Mi, Mj].Name = C.Name;
                            if (!IfChess())
                            {
                                game[Mi, Mj].Name = s;
                                if (h1 == '1')
                                {
                                    KingI1 = Mi;
                                    KingJ1 = Mj;
                                }
                                else
                                {
                                    KingI2 = Mi;
                                    KingJ2 = Mj;
                                }
                                if (game[Mi, Mj].Name == "")
                                {
                                    game[Mi, Mj].Name = C.Name;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;


                                    if (h1 == '1')
                                    {
                                        isCastling1 = true;
                                        h1 = '2';
                                    }
                                    else
                                    {
                                        h1 = '1';
                                        isCastling2 = true;
                                    }
                                }
                                else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                                {
                                    game[Mi, Mj].BackColor = Color.Black;
                                    game[Mi, Mj].Name = C.Name;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;



                                    if (h1 == '1')
                                    {
                                        isCastling1 = true;
                                        h1 = '2';
                                    }
                                    else
                                    {
                                        h1 = '1';
                                        isCastling2 = true;
                                    }
                                }
                                isFind = true;
                            }
                            else game[Mi, Mj].Name = s;
                        }
                        else
                        {
                            if (h1 == '1')
                            {
                                KingI1 = Mi;
                                KingJ1 = Mj;
                            }
                            else
                            {
                                KingI2 = Mi;
                                KingJ2 = Mj;
                            }
                            if (game[Mi, Mj].Name == "")
                            {
                                game[Mi, Mj].Name = C.Name;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                C.Text = "";
                                if (h1 == '1')
                                {
                                    isCastling1 = true;
                                    h1 = '2';
                                }
                                else
                                {
                                    h1 = '1';
                                    isCastling2 = true;
                                }
                            }
                            else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                            {
                                game[Mi, Mj].BackColor = Color.Black;
                                game[Mi, Mj].Name = C.Name;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                C.Text = "";

                                if (h1 == '1')
                                {
                                    isCastling1 = true;
                                    h1 = '2';
                                }
                                else
                                {
                                    h1 = '1';
                                    isCastling2 = true;
                                }
                            }
                            isFind = true;
                        }
                    }
                    else if (game[Mi - 1, Mj + 1].Name == C.Name)
                    {
                        if (IfChess())
                        {
                            s = game[Mi, Mj].Name;
                            game[Mi, Mj].Name = C.Name;
                            if (!IfChess())
                            {
                                game[Mi, Mj].Name = s;
                                if (h1 == '1')
                                {
                                    KingI1 = Mi;
                                    KingJ1 = Mj;
                                }
                                else
                                {
                                    KingI2 = Mi;
                                    KingJ2 = Mj;
                                }
                                if (game[Mi, Mj].Name == "")
                                {
                                    game[Mi, Mj].Name = C.Name;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;


                                    if (h1 == '1')
                                    {
                                        isCastling1 = true;
                                        h1 = '2';
                                    }
                                    else
                                    {
                                        h1 = '1';
                                        isCastling2 = true;
                                    }
                                }
                                else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                                {
                                    game[Mi, Mj].BackColor = Color.Black;
                                    game[Mi, Mj].Name = C.Name;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;



                                    if (h1 == '1')
                                    {
                                        isCastling1 = true;
                                        h1 = '2';
                                    }
                                    else
                                    {
                                        h1 = '1';
                                        isCastling2 = true;
                                    }
                                }
                                isFind = true;
                            }
                            else game[Mi, Mj].Name = s;
                        }
                        else
                        {
                            if (h1 == '1')
                            {
                                KingI1 = Mi;
                                KingJ1 = Mj;
                            }
                            else
                            {
                                KingI2 = Mi;
                                KingJ2 = Mj;
                            }
                            if (game[Mi, Mj].Name == "")
                            {
                                game[Mi, Mj].Name = C.Name;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                C.Text = "";
                                if (h1 == '1')
                                {
                                    isCastling1 = true;
                                    h1 = '2';
                                }
                                else
                                {
                                    h1 = '1';
                                    isCastling2 = true;
                                }
                            }
                            else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                            {
                                game[Mi, Mj].BackColor = Color.Black;
                                game[Mi, Mj].Name = C.Name;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                C.Text = "";

                                if (h1 == '1')
                                {
                                    isCastling1 = true;
                                    h1 = '2';
                                }
                                else
                                {
                                    h1 = '1';
                                    isCastling2 = true;
                                }
                            }
                            isFind = true;
                        }
                    }
                }
                else if (Mi == 7 && Mj == 0)
                {
                    if (game[Mi, Mj + 1].Name == C.Name)
                    {
                        if (IfChess())
                        {
                            s = game[Mi, Mj].Name;
                            game[Mi, Mj].Name = C.Name;
                            if (!IfChess())
                            {
                                game[Mi, Mj].Name = s;
                                if (h1 == '1')
                                {
                                    KingI1 = Mi;
                                    KingJ1 = Mj;
                                }
                                else
                                {
                                    KingI2 = Mi;
                                    KingJ2 = Mj;
                                }
                                if (game[Mi, Mj].Name == "")
                                {
                                    game[Mi, Mj].Name = C.Name;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;


                                    if (h1 == '1')
                                    {
                                        isCastling1 = true;
                                        h1 = '2';
                                    }
                                    else
                                    {
                                        h1 = '1';
                                        isCastling2 = true;
                                    }
                                }
                                else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                                {
                                    game[Mi, Mj].BackColor = Color.Black;
                                    game[Mi, Mj].Name = C.Name;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;



                                    if (h1 == '1')
                                    {
                                        isCastling1 = true;
                                        h1 = '2';
                                    }
                                    else
                                    {
                                        h1 = '1';
                                        isCastling2 = true;
                                    }
                                }
                                isFind = true;
                            }
                            else game[Mi, Mj].Name = s;
                        }
                        else
                        {
                            if (h1 == '1')
                            {
                                KingI1 = Mi;
                                KingJ1 = Mj;
                            }
                            else
                            {
                                KingI2 = Mi;
                                KingJ2 = Mj;
                            }
                            if (game[Mi, Mj].Name == "")
                            {
                                game[Mi, Mj].Name = C.Name;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                C.Text = "";
                                if (h1 == '1')
                                {
                                    isCastling1 = true;
                                    h1 = '2';
                                }
                                else
                                {
                                    h1 = '1';
                                    isCastling2 = true;
                                }
                            }
                            else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                            {
                                game[Mi, Mj].BackColor = Color.Black;
                                game[Mi, Mj].Name = C.Name;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                C.Text = "";

                                if (h1 == '1')
                                {
                                    isCastling1 = true;
                                    h1 = '2';
                                }
                                else
                                {
                                    h1 = '1';
                                    isCastling2 = true;
                                }
                            }
                            isFind = true;
                        }
                    }
                    else if (game[Mi - 1, Mj + 1].Name == C.Name)
                    {
                        if (IfChess())
                        {
                            s = game[Mi, Mj].Name;
                            game[Mi, Mj].Name = C.Name;
                            if (!IfChess())
                            {
                                game[Mi, Mj].Name = s;
                                if (h1 == '1')
                                {
                                    KingI1 = Mi;
                                    KingJ1 = Mj;
                                }
                                else
                                {
                                    KingI2 = Mi;
                                    KingJ2 = Mj;
                                }
                                if (game[Mi, Mj].Name == "")
                                {
                                    game[Mi, Mj].Name = C.Name;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;


                                    if (h1 == '1')
                                    {
                                        isCastling1 = true;
                                        h1 = '2';
                                    }
                                    else
                                    {
                                        h1 = '1';
                                        isCastling2 = true;
                                    }
                                }
                                else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                                {
                                    game[Mi, Mj].BackColor = Color.Black;
                                    game[Mi, Mj].Name = C.Name;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;



                                    if (h1 == '1')
                                    {
                                        isCastling1 = true;
                                        h1 = '2';
                                    }
                                    else
                                    {
                                        h1 = '1';
                                        isCastling2 = true;
                                    }
                                }
                                isFind = true;
                            }
                            else game[Mi, Mj].Name = s;
                        }
                        else
                        {
                            if (h1 == '1')
                            {
                                KingI1 = Mi;
                                KingJ1 = Mj;
                            }
                            else
                            {
                                KingI2 = Mi;
                                KingJ2 = Mj;
                            }
                            if (game[Mi, Mj].Name == "")
                            {
                                game[Mi, Mj].Name = C.Name;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                C.Text = "";
                                if (h1 == '1')
                                {
                                    isCastling1 = true;
                                    h1 = '2';
                                }
                                else
                                {
                                    h1 = '1';
                                    isCastling2 = true;
                                }
                            }
                            else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                            {
                                game[Mi, Mj].BackColor = Color.Black;
                                game[Mi, Mj].Name = C.Name;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                C.Text = "";

                                if (h1 == '1')
                                {
                                    isCastling1 = true;
                                    h1 = '2';
                                }
                                else
                                {
                                    h1 = '1';
                                    isCastling2 = true;
                                }
                            }
                            isFind = true;
                        }
                    }
                    else if (game[Mi - 1, Mj].Name == C.Name)
                    {
                        if (IfChess())
                        {
                            s = game[Mi, Mj].Name;
                            game[Mi, Mj].Name = C.Name;
                            if (!IfChess())
                            {
                                game[Mi, Mj].Name = s;
                                if (h1 == '1')
                                {
                                    KingI1 = Mi;
                                    KingJ1 = Mj;
                                }
                                else
                                {
                                    KingI2 = Mi;
                                    KingJ2 = Mj;
                                }
                                if (game[Mi, Mj].Name == "")
                                {
                                    game[Mi, Mj].Name = C.Name;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;


                                    if (h1 == '1')
                                    {
                                        isCastling1 = true;
                                        h1 = '2';
                                    }
                                    else
                                    {
                                        h1 = '1';
                                        isCastling2 = true;
                                    }
                                }
                                else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                                {
                                    game[Mi, Mj].BackColor = Color.Black;
                                    game[Mi, Mj].Name = C.Name;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;



                                    if (h1 == '1')
                                    {
                                        isCastling1 = true;
                                        h1 = '2';
                                    }
                                    else
                                    {
                                        h1 = '1';
                                        isCastling2 = true;
                                    }
                                }
                                isFind = true;
                            }
                            else game[Mi, Mj].Name = s;
                        }
                        else
                        {
                            if (h1 == '1')
                            {
                                KingI1 = Mi;
                                KingJ1 = Mj;
                            }
                            else
                            {
                                KingI2 = Mi;
                                KingJ2 = Mj;
                            }
                            if (game[Mi, Mj].Name == "")
                            {
                                game[Mi, Mj].Name = C.Name;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                C.Text = "";
                                if (h1 == '1')
                                {
                                    isCastling1 = true;
                                    h1 = '2';
                                }
                                else
                                {
                                    h1 = '1';
                                    isCastling2 = true;
                                }
                            }
                            else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                            {
                                game[Mi, Mj].BackColor = Color.Black;
                                game[Mi, Mj].Name = C.Name;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                C.Text = "";

                                if (h1 == '1')
                                {
                                    isCastling1 = true;
                                    h1 = '2';
                                }
                                else
                                {
                                    h1 = '1';
                                    isCastling2 = true;
                                }
                            }
                            isFind = true;
                        }
                    }
                }
                else if (Mi == 7 && Mj == 7)
                {
                    if (game[Mi - 1, Mj].Name == C.Name)
                    {
                        if (IfChess())
                        {
                            s = game[Mi, Mj].Name;
                            game[Mi, Mj].Name = C.Name;
                            if (!IfChess())
                            {
                                game[Mi, Mj].Name = s;
                                if (h1 == '1')
                                {
                                    KingI1 = Mi;
                                    KingJ1 = Mj;
                                }
                                else
                                {
                                    KingI2 = Mi;
                                    KingJ2 = Mj;
                                }
                                if (game[Mi, Mj].Name == "")
                                {
                                    game[Mi, Mj].Name = C.Name;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;


                                    if (h1 == '1')
                                    {
                                        isCastling1 = true;
                                        h1 = '2';
                                    }
                                    else
                                    {
                                        h1 = '1';
                                        isCastling2 = true;
                                    }
                                }
                                else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                                {
                                    game[Mi, Mj].BackColor = Color.Black;
                                    game[Mi, Mj].Name = C.Name;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;



                                    if (h1 == '1')
                                    {
                                        isCastling1 = true;
                                        h1 = '2';
                                    }
                                    else
                                    {
                                        h1 = '1';
                                        isCastling2 = true;
                                    }
                                }
                                isFind = true;
                            }
                            else game[Mi, Mj].Name = s;
                        }
                        else
                        {
                            if (h1 == '1')
                            {
                                KingI1 = Mi;
                                KingJ1 = Mj;
                            }
                            else
                            {
                                KingI2 = Mi;
                                KingJ2 = Mj;
                            }
                            if (game[Mi, Mj].Name == "")
                            {
                                game[Mi, Mj].Name = C.Name;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                C.Text = "";
                                if (h1 == '1')
                                {
                                    isCastling1 = true;
                                    h1 = '2';
                                }
                                else
                                {
                                    h1 = '1';
                                    isCastling2 = true;
                                }
                            }
                            else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                            {
                                game[Mi, Mj].BackColor = Color.Black;
                                game[Mi, Mj].Name = C.Name;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                C.Text = "";

                                if (h1 == '1')
                                {
                                    isCastling1 = true;
                                    h1 = '2';
                                }
                                else
                                {
                                    h1 = '1';
                                    isCastling2 = true;
                                }
                            }
                            isFind = true;
                        }
                    }
                    else if (game[Mi - 1, Mj - 1].Name == C.Name)
                    {
                        if (IfChess())
                        {
                            s = game[Mi, Mj].Name;
                            game[Mi, Mj].Name = C.Name;
                            if (!IfChess())
                            {
                                game[Mi, Mj].Name = s;
                                if (h1 == '1')
                                {
                                    KingI1 = Mi;
                                    KingJ1 = Mj;
                                }
                                else
                                {
                                    KingI2 = Mi;
                                    KingJ2 = Mj;
                                }
                                if (game[Mi, Mj].Name == "")
                                {
                                    game[Mi, Mj].Name = C.Name;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;


                                    if (h1 == '1')
                                    {
                                        isCastling1 = true;
                                        h1 = '2';
                                    }
                                    else
                                    {
                                        h1 = '1';
                                        isCastling2 = true;
                                    }
                                }
                                else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                                {
                                    game[Mi, Mj].BackColor = Color.Black;
                                    game[Mi, Mj].Name = C.Name;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;



                                    if (h1 == '1')
                                    {
                                        isCastling1 = true;
                                        h1 = '2';
                                    }
                                    else
                                    {
                                        h1 = '1';
                                        isCastling2 = true;
                                    }
                                }
                                isFind = true;
                            }
                            else game[Mi, Mj].Name = s;
                        }
                        else
                        {
                            if (h1 == '1')
                            {
                                KingI1 = Mi;
                                KingJ1 = Mj;
                            }
                            else
                            {
                                KingI2 = Mi;
                                KingJ2 = Mj;
                            }
                            if (game[Mi, Mj].Name == "")
                            {
                                game[Mi, Mj].Name = C.Name;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                C.Text = "";
                                if (h1 == '1')
                                {
                                    isCastling1 = true;
                                    h1 = '2';
                                }
                                else
                                {
                                    h1 = '1';
                                    isCastling2 = true;
                                }
                            }
                            else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                            {
                                game[Mi, Mj].BackColor = Color.Black;
                                game[Mi, Mj].Name = C.Name;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                C.Text = "";

                                if (h1 == '1')
                                {
                                    isCastling1 = true;
                                    h1 = '2';
                                }
                                else
                                {
                                    h1 = '1';
                                    isCastling2 = true;
                                }
                            }
                            isFind = true;
                        }
                    }
                    else if (game[Mi, Mj - 1].Name == C.Name)
                    {
                        if (IfChess())
                        {
                            s = game[Mi, Mj].Name;
                            game[Mi, Mj].Name = C.Name;
                            if (!IfChess())
                            {
                                game[Mi, Mj].Name = s;
                                if (h1 == '1')
                                {
                                    KingI1 = Mi;
                                    KingJ1 = Mj;
                                }
                                else
                                {
                                    KingI2 = Mi;
                                    KingJ2 = Mj;
                                }
                                if (game[Mi, Mj].Name == "")
                                {
                                    game[Mi, Mj].Name = C.Name;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;


                                    if (h1 == '1')
                                    {
                                        isCastling1 = true;
                                        h1 = '2';
                                    }
                                    else
                                    {
                                        h1 = '1';
                                        isCastling2 = true;
                                    }
                                }
                                else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                                {
                                    game[Mi, Mj].BackColor = Color.Black;
                                    game[Mi, Mj].Name = C.Name;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;



                                    if (h1 == '1')
                                    {
                                        isCastling1 = true;
                                        h1 = '2';
                                    }
                                    else
                                    {
                                        h1 = '1';
                                        isCastling2 = true;
                                    }
                                }
                                isFind = true;
                            }
                            else game[Mi, Mj].Name = s;
                        }
                        else
                        {
                            if (h1 == '1')
                            {
                                KingI1 = Mi;
                                KingJ1 = Mj;
                            }
                            else
                            {
                                KingI2 = Mi;
                                KingJ2 = Mj;
                            }
                            if (game[Mi, Mj].Name == "")
                            {
                                game[Mi, Mj].Name = C.Name;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                C.Text = "";
                                if (h1 == '1')
                                {
                                    isCastling1 = true;
                                    h1 = '2';
                                }
                                else
                                {
                                    h1 = '1';
                                    isCastling2 = true;
                                }
                            }
                            else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                            {
                                game[Mi, Mj].BackColor = Color.Black;
                                game[Mi, Mj].Name = C.Name;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                C.Text = "";

                                if (h1 == '1')
                                {
                                    isCastling1 = true;
                                    h1 = '2';
                                }
                                else
                                {
                                    h1 = '1';
                                    isCastling2 = true;
                                }
                            }
                            isFind = true;
                        }
                    }
                }
                else if (Mj == 7 && Mi > 0 && Mi < 7)
                {
                    if (game[Mi - 1, Mj].Name == C.Name)
                    {
                        if (IfChess())
                        {
                            s = game[Mi, Mj].Name;
                            game[Mi, Mj].Name = C.Name;
                            if (!IfChess())
                            {
                                game[Mi, Mj].Name = s;
                                if (h1 == '1')
                                {
                                    KingI1 = Mi;
                                    KingJ1 = Mj;
                                }
                                else
                                {
                                    KingI2 = Mi;
                                    KingJ2 = Mj;
                                }
                                if (game[Mi, Mj].Name == "")
                                {
                                    game[Mi, Mj].Name = C.Name;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;


                                    if (h1 == '1')
                                    {
                                        isCastling1 = true;
                                        h1 = '2';
                                    }
                                    else
                                    {
                                        h1 = '1';
                                        isCastling2 = true;
                                    }
                                }
                                else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                                {
                                    game[Mi, Mj].BackColor = Color.Black;
                                    game[Mi, Mj].Name = C.Name;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;



                                    if (h1 == '1')
                                    {
                                        isCastling1 = true;
                                        h1 = '2';
                                    }
                                    else
                                    {
                                        h1 = '1';
                                        isCastling2 = true;
                                    }
                                }
                                isFind = true;
                            }
                            else game[Mi, Mj].Name = s;
                        }
                        else
                        {
                            if (h1 == '1')
                            {
                                KingI1 = Mi;
                                KingJ1 = Mj;
                            }
                            else
                            {
                                KingI2 = Mi;
                                KingJ2 = Mj;
                            }
                            if (game[Mi, Mj].Name == "")
                            {
                                game[Mi, Mj].Name = C.Name;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                C.Text = "";
                                if (h1 == '1')
                                {
                                    isCastling1 = true;
                                    h1 = '2';
                                }
                                else
                                {
                                    h1 = '1';
                                    isCastling2 = true;
                                }
                            }
                            else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                            {
                                game[Mi, Mj].BackColor = Color.Black;
                                game[Mi, Mj].Name = C.Name;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                C.Text = "";

                                if (h1 == '1')
                                {
                                    isCastling1 = true;
                                    h1 = '2';
                                }
                                else
                                {
                                    h1 = '1';
                                    isCastling2 = true;
                                }
                            }
                            isFind = true;
                        }
                    }
                    else if (game[Mi - 1, Mj - 1].Name == C.Name)
                    {
                        if (IfChess())
                        {
                            s = game[Mi, Mj].Name;
                            game[Mi, Mj].Name = C.Name;
                            if (!IfChess())
                            {
                                game[Mi, Mj].Name = s;
                                if (h1 == '1')
                                {
                                    KingI1 = Mi;
                                    KingJ1 = Mj;
                                }
                                else
                                {
                                    KingI2 = Mi;
                                    KingJ2 = Mj;
                                }
                                if (game[Mi, Mj].Name == "")
                                {
                                    game[Mi, Mj].Name = C.Name;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;


                                    if (h1 == '1')
                                    {
                                        isCastling1 = true;
                                        h1 = '2';
                                    }
                                    else
                                    {
                                        h1 = '1';
                                        isCastling2 = true;
                                    }
                                }
                                else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                                {
                                    game[Mi, Mj].BackColor = Color.Black;
                                    game[Mi, Mj].Name = C.Name;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;



                                    if (h1 == '1')
                                    {
                                        isCastling1 = true;
                                        h1 = '2';
                                    }
                                    else
                                    {
                                        h1 = '1';
                                        isCastling2 = true;
                                    }
                                }
                                isFind = true;
                            }
                            else game[Mi, Mj].Name = s;
                        }
                        else
                        {
                            if (h1 == '1')
                            {
                                KingI1 = Mi;
                                KingJ1 = Mj;
                            }
                            else
                            {
                                KingI2 = Mi;
                                KingJ2 = Mj;
                            }
                            if (game[Mi, Mj].Name == "")
                            {
                                game[Mi, Mj].Name = C.Name;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                C.Text = "";
                                if (h1 == '1')
                                {
                                    isCastling1 = true;
                                    h1 = '2';
                                }
                                else
                                {
                                    h1 = '1';
                                    isCastling2 = true;
                                }
                            }
                            else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                            {
                                game[Mi, Mj].BackColor = Color.Black;
                                game[Mi, Mj].Name = C.Name;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                C.Text = "";

                                if (h1 == '1')
                                {
                                    isCastling1 = true;
                                    h1 = '2';
                                }
                                else
                                {
                                    h1 = '1';
                                    isCastling2 = true;
                                }
                            }
                            isFind = true;
                        }
                    }
                    else if (game[Mi, Mj - 1].Name == C.Name)
                    {
                        if (IfChess())
                        {
                            s = game[Mi, Mj].Name;
                            game[Mi, Mj].Name = C.Name;
                            if (!IfChess())
                            {
                                game[Mi, Mj].Name = s;
                                if (h1 == '1')
                                {
                                    KingI1 = Mi;
                                    KingJ1 = Mj;
                                }
                                else
                                {
                                    KingI2 = Mi;
                                    KingJ2 = Mj;
                                }
                                if (game[Mi, Mj].Name == "")
                                {
                                    game[Mi, Mj].Name = C.Name;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;


                                    if (h1 == '1')
                                    {
                                        isCastling1 = true;
                                        h1 = '2';
                                    }
                                    else
                                    {
                                        h1 = '1';
                                        isCastling2 = true;
                                    }
                                }
                                else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                                {
                                    game[Mi, Mj].BackColor = Color.Black;
                                    game[Mi, Mj].Name = C.Name;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;



                                    if (h1 == '1')
                                    {
                                        isCastling1 = true;
                                        h1 = '2';
                                    }
                                    else
                                    {
                                        h1 = '1';
                                        isCastling2 = true;
                                    }
                                }
                                isFind = true;
                            }
                            else game[Mi, Mj].Name = s;
                        }
                        else
                        {
                            if (h1 == '1')
                            {
                                KingI1 = Mi;
                                KingJ1 = Mj;
                            }
                            else
                            {
                                KingI2 = Mi;
                                KingJ2 = Mj;
                            }
                            if (game[Mi, Mj].Name == "")
                            {
                                game[Mi, Mj].Name = C.Name;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                C.Text = "";
                                if (h1 == '1')
                                {
                                    isCastling1 = true;
                                    h1 = '2';
                                }
                                else
                                {
                                    h1 = '1';
                                    isCastling2 = true;
                                }
                            }
                            else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                            {
                                game[Mi, Mj].BackColor = Color.Black;
                                game[Mi, Mj].Name = C.Name;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                C.Text = "";

                                if (h1 == '1')
                                {
                                    isCastling1 = true;
                                    h1 = '2';
                                }
                                else
                                {
                                    h1 = '1';
                                    isCastling2 = true;
                                }
                            }
                            isFind = true;
                        }
                    }
                    else if (game[Mi + 1, Mj].Name == C.Name)
                    {
                        if (IfChess())
                        {
                            s = game[Mi, Mj].Name;
                            game[Mi, Mj].Name = C.Name;
                            if (!IfChess())
                            {
                                game[Mi, Mj].Name = s;
                                if (h1 == '1')
                                {
                                    KingI1 = Mi;
                                    KingJ1 = Mj;
                                }
                                else
                                {
                                    KingI2 = Mi;
                                    KingJ2 = Mj;
                                }
                                if (game[Mi, Mj].Name == "")
                                {
                                    game[Mi, Mj].Name = C.Name;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;


                                    if (h1 == '1')
                                    {
                                        isCastling1 = true;
                                        h1 = '2';
                                    }
                                    else
                                    {
                                        h1 = '1';
                                        isCastling2 = true;
                                    }
                                }
                                else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                                {
                                    game[Mi, Mj].BackColor = Color.Black;
                                    game[Mi, Mj].Name = C.Name;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;



                                    if (h1 == '1')
                                    {
                                        isCastling1 = true;
                                        h1 = '2';
                                    }
                                    else
                                    {
                                        h1 = '1';
                                        isCastling2 = true;
                                    }
                                }
                                isFind = true;
                            }
                            else game[Mi, Mj].Name = s;
                        }
                        else
                        {
                            if (h1 == '1')
                            {
                                KingI1 = Mi;
                                KingJ1 = Mj;
                            }
                            else
                            {
                                KingI2 = Mi;
                                KingJ2 = Mj;
                            }
                            if (game[Mi, Mj].Name == "")
                            {
                                game[Mi, Mj].Name = C.Name;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                C.Text = "";
                                if (h1 == '1')
                                {
                                    isCastling1 = true;
                                    h1 = '2';
                                }
                                else
                                {
                                    h1 = '1';
                                    isCastling2 = true;
                                }
                            }
                            else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                            {
                                game[Mi, Mj].BackColor = Color.Black;
                                game[Mi, Mj].Name = C.Name;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                C.Text = "";

                                if (h1 == '1')
                                {
                                    isCastling1 = true;
                                    h1 = '2';
                                }
                                else
                                {
                                    h1 = '1';
                                    isCastling2 = true;
                                }
                            }
                            isFind = true;
                        }
                    }
                    else if (game[Mi + 1, Mj - 1].Name == C.Name)
                    {
                        if (IfChess())
                        {
                            s = game[Mi, Mj].Name;
                            game[Mi, Mj].Name = C.Name;
                            if (!IfChess())
                            {
                                game[Mi, Mj].Name = s;
                                if (h1 == '1')
                                {
                                    KingI1 = Mi;
                                    KingJ1 = Mj;
                                }
                                else
                                {
                                    KingI2 = Mi;
                                    KingJ2 = Mj;
                                }
                                if (game[Mi, Mj].Name == "")
                                {
                                    game[Mi, Mj].Name = C.Name;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;


                                    if (h1 == '1')
                                    {
                                        isCastling1 = true;
                                        h1 = '2';
                                    }
                                    else
                                    {
                                        h1 = '1';
                                        isCastling2 = true;
                                    }
                                }
                                else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                                {
                                    game[Mi, Mj].BackColor = Color.Black;
                                    game[Mi, Mj].Name = C.Name;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;



                                    if (h1 == '1')
                                    {
                                        isCastling1 = true;
                                        h1 = '2';
                                    }
                                    else
                                    {
                                        h1 = '1';
                                        isCastling2 = true;
                                    }
                                }
                                isFind = true;
                            }
                            else game[Mi, Mj].Name = s;
                        }
                        else
                        {
                            if (h1 == '1')
                            {
                                KingI1 = Mi;
                                KingJ1 = Mj;
                            }
                            else
                            {
                                KingI2 = Mi;
                                KingJ2 = Mj;
                            }
                            if (game[Mi, Mj].Name == "")
                            {
                                game[Mi, Mj].Name = C.Name;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                C.Text = "";
                                if (h1 == '1')
                                {
                                    isCastling1 = true;
                                    h1 = '2';
                                }
                                else
                                {
                                    h1 = '1';
                                    isCastling2 = true;
                                }
                            }
                            else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                            {
                                game[Mi, Mj].BackColor = Color.Black;
                                game[Mi, Mj].Name = C.Name;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                C.Text = "";

                                if (h1 == '1')
                                {
                                    isCastling1 = true;
                                    h1 = '2';
                                }
                                else
                                {
                                    h1 = '1';
                                    isCastling2 = true;
                                }
                            }
                            isFind = true;
                        }
                    }
                }
                else if (Mj == 7 && Mi == 0)
                {
                    if (game[Mi, Mj - 1].Name == C.Name)
                    {
                        if (IfChess())
                        {
                            s = game[Mi, Mj].Name;
                            game[Mi, Mj].Name = C.Name;
                            if (!IfChess())
                            {
                                game[Mi, Mj].Name = s;
                                if (h1 == '1')
                                {
                                    KingI1 = Mi;
                                    KingJ1 = Mj;
                                }
                                else
                                {
                                    KingI2 = Mi;
                                    KingJ2 = Mj;
                                }
                                if (game[Mi, Mj].Name == "")
                                {
                                    game[Mi, Mj].Name = C.Name;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;


                                    if (h1 == '1')
                                    {
                                        isCastling1 = true;
                                        h1 = '2';
                                    }
                                    else
                                    {
                                        h1 = '1';
                                        isCastling2 = true;
                                    }
                                }
                                else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                                {
                                    game[Mi, Mj].BackColor = Color.Black;
                                    game[Mi, Mj].Name = C.Name;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;



                                    if (h1 == '1')
                                    {
                                        isCastling1 = true;
                                        h1 = '2';
                                    }
                                    else
                                    {
                                        h1 = '1';
                                        isCastling2 = true;
                                    }
                                }
                                isFind = true;
                            }
                            else game[Mi, Mj].Name = s;
                        }
                        else
                        {
                            if (h1 == '1')
                            {
                                KingI1 = Mi;
                                KingJ1 = Mj;
                            }
                            else
                            {
                                KingI2 = Mi;
                                KingJ2 = Mj;
                            }
                            if (game[Mi, Mj].Name == "")
                            {
                                game[Mi, Mj].Name = C.Name;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                C.Text = "";
                                if (h1 == '1')
                                {
                                    isCastling1 = true;
                                    h1 = '2';
                                }
                                else
                                {
                                    h1 = '1';
                                    isCastling2 = true;
                                }
                            }
                            else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                            {
                                game[Mi, Mj].BackColor = Color.Black;
                                game[Mi, Mj].Name = C.Name;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                C.Text = "";

                                if (h1 == '1')
                                {
                                    isCastling1 = true;
                                    h1 = '2';
                                }
                                else
                                {
                                    h1 = '1';
                                    isCastling2 = true;
                                }
                            }
                            isFind = true;
                        }
                    }
                    else if (game[Mi + 1, Mj - 1].Name == C.Name)
                    {
                        if (IfChess())
                        {
                            s = game[Mi, Mj].Name;
                            game[Mi, Mj].Name = C.Name;
                            if (!IfChess())
                            {
                                game[Mi, Mj].Name = s;
                                if (h1 == '1')
                                {
                                    KingI1 = Mi;
                                    KingJ1 = Mj;
                                }
                                else
                                {
                                    KingI2 = Mi;
                                    KingJ2 = Mj;
                                }
                                if (game[Mi, Mj].Name == "")
                                {
                                    game[Mi, Mj].Name = C.Name;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;


                                    if (h1 == '1')
                                    {
                                        isCastling1 = true;
                                        h1 = '2';
                                    }
                                    else
                                    {
                                        h1 = '1';
                                        isCastling2 = true;
                                    }
                                }
                                else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                                {
                                    game[Mi, Mj].BackColor = Color.Black;
                                    game[Mi, Mj].Name = C.Name;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;



                                    if (h1 == '1')
                                    {
                                        isCastling1 = true;
                                        h1 = '2';
                                    }
                                    else
                                    {
                                        h1 = '1';
                                        isCastling2 = true;
                                    }
                                }
                                isFind = true;
                            }
                            else game[Mi, Mj].Name = s;
                        }
                        else
                        {
                            if (h1 == '1')
                            {
                                KingI1 = Mi;
                                KingJ1 = Mj;
                            }
                            else
                            {
                                KingI2 = Mi;
                                KingJ2 = Mj;
                            }
                            if (game[Mi, Mj].Name == "")
                            {
                                game[Mi, Mj].Name = C.Name;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                C.Text = "";
                                if (h1 == '1')
                                {
                                    isCastling1 = true;
                                    h1 = '2';
                                }
                                else
                                {
                                    h1 = '1';
                                    isCastling2 = true;
                                }
                            }
                            else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                            {
                                game[Mi, Mj].BackColor = Color.Black;
                                game[Mi, Mj].Name = C.Name;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                C.Text = "";

                                if (h1 == '1')
                                {
                                    isCastling1 = true;
                                    h1 = '2';
                                }
                                else
                                {
                                    h1 = '1';
                                    isCastling2 = true;
                                }
                            }
                            isFind = true;
                        }
                    }
                    else if (game[Mi + 1, Mj].Name == C.Name)
                    {
                        if (IfChess())
                        {
                            s = game[Mi, Mj].Name;
                            game[Mi, Mj].Name = C.Name;
                            if (!IfChess())
                            {
                                game[Mi, Mj].Name = s;
                                if (h1 == '1')
                                {
                                    KingI1 = Mi;
                                    KingJ1 = Mj;
                                }
                                else
                                {
                                    KingI2 = Mi;
                                    KingJ2 = Mj;
                                }
                                if (game[Mi, Mj].Name == "")
                                {
                                    game[Mi, Mj].Name = C.Name;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;


                                    if (h1 == '1')
                                    {
                                        isCastling1 = true;
                                        h1 = '2';
                                    }
                                    else
                                    {
                                        h1 = '1';
                                        isCastling2 = true;
                                    }
                                }
                                else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                                {
                                    game[Mi, Mj].BackColor = Color.Black;
                                    game[Mi, Mj].Name = C.Name;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;



                                    if (h1 == '1')
                                    {
                                        isCastling1 = true;
                                        h1 = '2';
                                    }
                                    else
                                    {
                                        h1 = '1';
                                        isCastling2 = true;
                                    }
                                }
                                isFind = true;
                            }
                            else game[Mi, Mj].Name = s;
                        }
                        else
                        {
                            if (h1 == '1')
                            {
                                KingI1 = Mi;
                                KingJ1 = Mj;
                            }
                            else
                            {
                                KingI2 = Mi;
                                KingJ2 = Mj;
                            }
                            if (game[Mi, Mj].Name == "")
                            {
                                game[Mi, Mj].Name = C.Name;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                C.Text = "";
                                if (h1 == '1')
                                {
                                    isCastling1 = true;
                                    h1 = '2';
                                }
                                else
                                {
                                    h1 = '1';
                                    isCastling2 = true;
                                }
                            }
                            else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                            {
                                game[Mi, Mj].BackColor = Color.Black;
                                game[Mi, Mj].Name = C.Name;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                C.Text = "";

                                if (h1 == '1')
                                {
                                    isCastling1 = true;
                                    h1 = '2';
                                }
                                else
                                {
                                    h1 = '1';
                                    isCastling2 = true;
                                }
                            }
                            isFind = true;
                        }
                    }
                }
                else if (Mi == 0 && Mj < 7 && Mj > 0)
                {
                    if (game[Mi, Mj - 1].Name == C.Name)
                    {
                        if (IfChess())
                        {
                            s = game[Mi, Mj].Name;
                            game[Mi, Mj].Name = C.Name;
                            if (!IfChess())
                            {
                                game[Mi, Mj].Name = s;
                                if (h1 == '1')
                                {
                                    KingI1 = Mi;
                                    KingJ1 = Mj;
                                }
                                else
                                {
                                    KingI2 = Mi;
                                    KingJ2 = Mj;
                                }
                                if (game[Mi, Mj].Name == "")
                                {
                                    game[Mi, Mj].Name = C.Name;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;


                                    if (h1 == '1')
                                    {
                                        isCastling1 = true;
                                        h1 = '2';
                                    }
                                    else
                                    {
                                        h1 = '1';
                                        isCastling2 = true;
                                    }
                                }
                                else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                                {
                                    game[Mi, Mj].BackColor = Color.Black;
                                    game[Mi, Mj].Name = C.Name;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;



                                    if (h1 == '1')
                                    {
                                        isCastling1 = true;
                                        h1 = '2';
                                    }
                                    else
                                    {
                                        h1 = '1';
                                        isCastling2 = true;
                                    }
                                }
                                isFind = true;
                            }
                            else game[Mi, Mj].Name = s;
                        }
                        else
                        {
                            if (h1 == '1')
                            {
                                KingI1 = Mi;
                                KingJ1 = Mj;
                            }
                            else
                            {
                                KingI2 = Mi;
                                KingJ2 = Mj;
                            }
                            if (game[Mi, Mj].Name == "")
                            {
                                game[Mi, Mj].Name = C.Name;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                C.Text = "";
                                if (h1 == '1')
                                {
                                    isCastling1 = true;
                                    h1 = '2';
                                }
                                else
                                {
                                    h1 = '1';
                                    isCastling2 = true;
                                }
                            }
                            else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                            {
                                game[Mi, Mj].BackColor = Color.Black;
                                game[Mi, Mj].Name = C.Name;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                C.Text = "";

                                if (h1 == '1')
                                {
                                    isCastling1 = true;
                                    h1 = '2';
                                }
                                else
                                {
                                    h1 = '1';
                                    isCastling2 = true;
                                }
                            }
                            isFind = true;
                        }
                    }
                    else if (game[Mi + 1, Mj - 1].Name == C.Name)
                    {
                        if (IfChess())
                        {
                            s = game[Mi, Mj].Name;
                            game[Mi, Mj].Name = C.Name;
                            if (!IfChess())
                            {
                                game[Mi, Mj].Name = s;
                                if (h1 == '1')
                                {
                                    KingI1 = Mi;
                                    KingJ1 = Mj;
                                }
                                else
                                {
                                    KingI2 = Mi;
                                    KingJ2 = Mj;
                                }
                                if (game[Mi, Mj].Name == "")
                                {
                                    game[Mi, Mj].Name = C.Name;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;


                                    if (h1 == '1')
                                    {
                                        isCastling1 = true;
                                        h1 = '2';
                                    }
                                    else
                                    {
                                        h1 = '1';
                                        isCastling2 = true;
                                    }
                                }
                                else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                                {
                                    game[Mi, Mj].BackColor = Color.Black;
                                    game[Mi, Mj].Name = C.Name;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;



                                    if (h1 == '1')
                                    {
                                        isCastling1 = true;
                                        h1 = '2';
                                    }
                                    else
                                    {
                                        h1 = '1';
                                        isCastling2 = true;
                                    }
                                }
                                isFind = true;
                            }
                            else game[Mi, Mj].Name = s;
                        }
                        else
                        {
                            if (h1 == '1')
                            {
                                KingI1 = Mi;
                                KingJ1 = Mj;
                            }
                            else
                            {
                                KingI2 = Mi;
                                KingJ2 = Mj;
                            }
                            if (game[Mi, Mj].Name == "")
                            {
                                game[Mi, Mj].Name = C.Name;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                C.Text = "";
                                if (h1 == '1')
                                {
                                    isCastling1 = true;
                                    h1 = '2';
                                }
                                else
                                {
                                    h1 = '1';
                                    isCastling2 = true;
                                }
                            }
                            else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                            {
                                game[Mi, Mj].BackColor = Color.Black;
                                game[Mi, Mj].Name = C.Name;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                C.Text = "";

                                if (h1 == '1')
                                {
                                    isCastling1 = true;
                                    h1 = '2';
                                }
                                else
                                {
                                    h1 = '1';
                                    isCastling2 = true;
                                }
                            }
                            isFind = true;
                        }
                    }
                    else if (game[Mi + 1, Mj].Name == C.Name)
                    {
                        if (IfChess())
                        {
                            s = game[Mi, Mj].Name;
                            game[Mi, Mj].Name = C.Name;
                            if (!IfChess())
                            {
                                game[Mi, Mj].Name = s;
                                if (h1 == '1')
                                {
                                    KingI1 = Mi;
                                    KingJ1 = Mj;
                                }
                                else
                                {
                                    KingI2 = Mi;
                                    KingJ2 = Mj;
                                }
                                if (game[Mi, Mj].Name == "")
                                {
                                    game[Mi, Mj].Name = C.Name;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;


                                    if (h1 == '1')
                                    {
                                        isCastling1 = true;
                                        h1 = '2';
                                    }
                                    else
                                    {
                                        h1 = '1';
                                        isCastling2 = true;
                                    }
                                }
                                else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                                {
                                    game[Mi, Mj].BackColor = Color.Black;
                                    game[Mi, Mj].Name = C.Name;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;



                                    if (h1 == '1')
                                    {
                                        isCastling1 = true;
                                        h1 = '2';
                                    }
                                    else
                                    {
                                        h1 = '1';
                                        isCastling2 = true;
                                    }
                                }
                                isFind = true;
                            }
                            else game[Mi, Mj].Name = s;
                        }
                        else
                        {
                            if (h1 == '1')
                            {
                                KingI1 = Mi;
                                KingJ1 = Mj;
                            }
                            else
                            {
                                KingI2 = Mi;
                                KingJ2 = Mj;
                            }
                            if (game[Mi, Mj].Name == "")
                            {
                                game[Mi, Mj].Name = C.Name;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                C.Text = "";
                                if (h1 == '1')
                                {
                                    isCastling1 = true;
                                    h1 = '2';
                                }
                                else
                                {
                                    h1 = '1';
                                    isCastling2 = true;
                                }
                            }
                            else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                            {
                                game[Mi, Mj].BackColor = Color.Black;
                                game[Mi, Mj].Name = C.Name;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                C.Text = "";

                                if (h1 == '1')
                                {
                                    isCastling1 = true;
                                    h1 = '2';
                                }
                                else
                                {
                                    h1 = '1';
                                    isCastling2 = true;
                                }
                            }
                            isFind = true;
                        }
                    }
                    else if (game[Mi, Mj + 1].Name == C.Name)
                    {
                        if (IfChess())
                        {
                            s = game[Mi, Mj].Name;
                            game[Mi, Mj].Name = C.Name;
                            if (!IfChess())
                            {
                                game[Mi, Mj].Name = s;
                                if (h1 == '1')
                                {
                                    KingI1 = Mi;
                                    KingJ1 = Mj;
                                }
                                else
                                {
                                    KingI2 = Mi;
                                    KingJ2 = Mj;
                                }
                                if (game[Mi, Mj].Name == "")
                                {
                                    game[Mi, Mj].Name = C.Name;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;


                                    if (h1 == '1')
                                    {
                                        isCastling1 = true;
                                        h1 = '2';
                                    }
                                    else
                                    {
                                        h1 = '1';
                                        isCastling2 = true;
                                    }
                                }
                                else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                                {
                                    game[Mi, Mj].BackColor = Color.Black;
                                    game[Mi, Mj].Name = C.Name;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;



                                    if (h1 == '1')
                                    {
                                        isCastling1 = true;
                                        h1 = '2';
                                    }
                                    else
                                    {
                                        h1 = '1';
                                        isCastling2 = true;
                                    }
                                }
                                isFind = true;
                            }
                            else game[Mi, Mj].Name = s;
                        }
                        else
                        {
                            if (h1 == '1')
                            {
                                KingI1 = Mi;
                                KingJ1 = Mj;
                            }
                            else
                            {
                                KingI2 = Mi;
                                KingJ2 = Mj;
                            }
                            if (game[Mi, Mj].Name == "")
                            {
                                game[Mi, Mj].Name = C.Name;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                C.Text = "";
                                if (h1 == '1')
                                {
                                    isCastling1 = true;
                                    h1 = '2';
                                }
                                else
                                {
                                    h1 = '1';
                                    isCastling2 = true;
                                }
                            }
                            else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                            {
                                game[Mi, Mj].BackColor = Color.Black;
                                game[Mi, Mj].Name = C.Name;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                C.Text = "";

                                if (h1 == '1')
                                {
                                    isCastling1 = true;
                                    h1 = '2';
                                }
                                else
                                {
                                    h1 = '1';
                                    isCastling2 = true;
                                }
                            }
                            isFind = true;
                        }
                    }
                    else if (game[Mi + 1, Mj + 1].Name == C.Name)
                    {
                        if (IfChess())
                        {
                            s = game[Mi, Mj].Name;
                            game[Mi, Mj].Name = C.Name;
                            if (!IfChess())
                            {
                                game[Mi, Mj].Name = s;
                                if (h1 == '1')
                                {
                                    KingI1 = Mi;
                                    KingJ1 = Mj;
                                }
                                else
                                {
                                    KingI2 = Mi;
                                    KingJ2 = Mj;
                                }
                                if (game[Mi, Mj].Name == "")
                                {
                                    game[Mi, Mj].Name = C.Name;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;


                                    if (h1 == '1')
                                    {
                                        isCastling1 = true;
                                        h1 = '2';
                                    }
                                    else
                                    {
                                        h1 = '1';
                                        isCastling2 = true;
                                    }
                                }
                                else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                                {
                                    game[Mi, Mj].BackColor = Color.Black;
                                    game[Mi, Mj].Name = C.Name;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;



                                    if (h1 == '1')
                                    {
                                        isCastling1 = true;
                                        h1 = '2';
                                    }
                                    else
                                    {
                                        h1 = '1';
                                        isCastling2 = true;
                                    }
                                }
                                isFind = true;
                            }
                            else game[Mi, Mj].Name = s;
                        }
                        else
                        {
                            if (h1 == '1')
                            {
                                KingI1 = Mi;
                                KingJ1 = Mj;
                            }
                            else
                            {
                                KingI2 = Mi;
                                KingJ2 = Mj;
                            }
                            if (game[Mi, Mj].Name == "")
                            {
                                game[Mi, Mj].Name = C.Name;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                C.Text = "";
                                if (h1 == '1')
                                {
                                    isCastling1 = true;
                                    h1 = '2';
                                }
                                else
                                {
                                    h1 = '1';
                                    isCastling2 = true;
                                }
                            }
                            else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                            {
                                game[Mi, Mj].BackColor = Color.Black;
                                game[Mi, Mj].Name = C.Name;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                C.Text = "";

                                if (h1 == '1')
                                {
                                    isCastling1 = true;
                                    h1 = '2';
                                }
                                else
                                {
                                    h1 = '1';
                                    isCastling2 = true;
                                }
                            }
                            isFind = true;
                        }
                    }
                }
                else if (Mj == 0 && Mi > 0 && Mi < 7)
                {
                    if (game[Mi - 1, Mj + 1].Name == C.Name)
                    {
                        if (IfChess())
                        {
                            s = game[Mi, Mj].Name;
                            game[Mi, Mj].Name = C.Name;
                            if (!IfChess())
                            {
                                game[Mi, Mj].Name = s;
                                if (h1 == '1')
                                {
                                    KingI1 = Mi;
                                    KingJ1 = Mj;
                                }
                                else
                                {
                                    KingI2 = Mi;
                                    KingJ2 = Mj;
                                }
                                if (game[Mi, Mj].Name == "")
                                {
                                    game[Mi, Mj].Name = C.Name;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;


                                    if (h1 == '1')
                                    {
                                        isCastling1 = true;
                                        h1 = '2';
                                    }
                                    else
                                    {
                                        h1 = '1';
                                        isCastling2 = true;
                                    }
                                }
                                else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                                {
                                    game[Mi, Mj].BackColor = Color.Black;
                                    game[Mi, Mj].Name = C.Name;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;



                                    if (h1 == '1')
                                    {
                                        isCastling1 = true;
                                        h1 = '2';
                                    }
                                    else
                                    {
                                        h1 = '1';
                                        isCastling2 = true;
                                    }
                                }
                                isFind = true;
                            }
                            else game[Mi, Mj].Name = s;
                        }
                        else
                        {
                            if (h1 == '1')
                            {
                                KingI1 = Mi;
                                KingJ1 = Mj;
                            }
                            else
                            {
                                KingI2 = Mi;
                                KingJ2 = Mj;
                            }
                            if (game[Mi, Mj].Name == "")
                            {
                                game[Mi, Mj].Name = C.Name;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                C.Text = "";
                                if (h1 == '1')
                                {
                                    isCastling1 = true;
                                    h1 = '2';
                                }
                                else
                                {
                                    h1 = '1';
                                    isCastling2 = true;
                                }
                            }
                            else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                            {
                                game[Mi, Mj].BackColor = Color.Black;
                                game[Mi, Mj].Name = C.Name;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                C.Text = "";

                                if (h1 == '1')
                                {
                                    isCastling1 = true;
                                    h1 = '2';
                                }
                                else
                                {
                                    h1 = '1';
                                    isCastling2 = true;
                                }
                            }
                            isFind = true;
                        }
                    }
                    else if (game[Mi - 1, Mj].Name == C.Name)
                    {
                        if (IfChess())
                        {
                            s = game[Mi, Mj].Name;
                            game[Mi, Mj].Name = C.Name;
                            if (!IfChess())
                            {
                                game[Mi, Mj].Name = s;
                                if (h1 == '1')
                                {
                                    KingI1 = Mi;
                                    KingJ1 = Mj;
                                }
                                else
                                {
                                    KingI2 = Mi;
                                    KingJ2 = Mj;
                                }
                                if (game[Mi, Mj].Name == "")
                                {
                                    game[Mi, Mj].Name = C.Name;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;


                                    if (h1 == '1')
                                    {
                                        isCastling1 = true;
                                        h1 = '2';
                                    }
                                    else
                                    {
                                        h1 = '1';
                                        isCastling2 = true;
                                    }
                                }
                                else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                                {
                                    game[Mi, Mj].BackColor = Color.Black;
                                    game[Mi, Mj].Name = C.Name;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;



                                    if (h1 == '1')
                                    {
                                        isCastling1 = true;
                                        h1 = '2';
                                    }
                                    else
                                    {
                                        h1 = '1';
                                        isCastling2 = true;
                                    }
                                }
                                isFind = true;
                            }
                            else game[Mi, Mj].Name = s;
                        }
                        else
                        {
                            if (h1 == '1')
                            {
                                KingI1 = Mi;
                                KingJ1 = Mj;
                            }
                            else
                            {
                                KingI2 = Mi;
                                KingJ2 = Mj;
                            }
                            if (game[Mi, Mj].Name == "")
                            {
                                game[Mi, Mj].Name = C.Name;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                C.Text = "";
                                if (h1 == '1')
                                {
                                    isCastling1 = true;
                                    h1 = '2';
                                }
                                else
                                {
                                    h1 = '1';
                                    isCastling2 = true;
                                }
                            }
                            else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                            {
                                game[Mi, Mj].BackColor = Color.Black;
                                game[Mi, Mj].Name = C.Name;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                C.Text = "";

                                if (h1 == '1')
                                {
                                    isCastling1 = true;
                                    h1 = '2';
                                }
                                else
                                {
                                    h1 = '1';
                                    isCastling2 = true;
                                }
                            }
                            isFind = true;
                        }
                    }
                    else if (game[Mi + 1, Mj + 1].Name == C.Name)
                    {
                        if (IfChess())
                        {
                            s = game[Mi, Mj].Name;
                            game[Mi, Mj].Name = C.Name;
                            if (!IfChess())
                            {
                                game[Mi, Mj].Name = s;
                                if (h1 == '1')
                                {
                                    KingI1 = Mi;
                                    KingJ1 = Mj;
                                }
                                else
                                {
                                    KingI2 = Mi;
                                    KingJ2 = Mj;
                                }
                                if (game[Mi, Mj].Name == "")
                                {
                                    game[Mi, Mj].Name = C.Name;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;


                                    if (h1 == '1')
                                    {
                                        isCastling1 = true;
                                        h1 = '2';
                                    }
                                    else
                                    {
                                        h1 = '1';
                                        isCastling2 = true;
                                    }
                                }
                                else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                                {
                                    game[Mi, Mj].BackColor = Color.Black;
                                    game[Mi, Mj].Name = C.Name;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;



                                    if (h1 == '1')
                                    {
                                        isCastling1 = true;
                                        h1 = '2';
                                    }
                                    else
                                    {
                                        h1 = '1';
                                        isCastling2 = true;
                                    }
                                }
                                isFind = true;
                            }
                            else game[Mi, Mj].Name = s;
                        }
                        else
                        {
                            if (h1 == '1')
                            {
                                KingI1 = Mi;
                                KingJ1 = Mj;
                            }
                            else
                            {
                                KingI2 = Mi;
                                KingJ2 = Mj;
                            }
                            if (game[Mi, Mj].Name == "")
                            {
                                game[Mi, Mj].Name = C.Name;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                C.Text = "";
                                if (h1 == '1')
                                {
                                    isCastling1 = true;
                                    h1 = '2';
                                }
                                else
                                {
                                    h1 = '1';
                                    isCastling2 = true;
                                }
                            }
                            else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                            {
                                game[Mi, Mj].BackColor = Color.Black;
                                game[Mi, Mj].Name = C.Name;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                C.Text = "";

                                if (h1 == '1')
                                {
                                    isCastling1 = true;
                                    h1 = '2';
                                }
                                else
                                {
                                    h1 = '1';
                                    isCastling2 = true;
                                }
                            }
                            isFind = true;
                        }
                    }
                    else if (game[Mi + 1, Mj].Name == C.Name)
                    {
                        if (game[Mi, Mj].Name == "")
                            if (IfChess())
                            {
                                s = game[Mi, Mj].Name;
                                game[Mi, Mj].Name = C.Name;
                                if (!IfChess())
                                {
                                    game[Mi, Mj].Name = s;
                                    if (h1 == '1')
                                    {
                                        KingI1 = Mi;
                                        KingJ1 = Mj;
                                    }
                                    else
                                    {
                                        KingI2 = Mi;
                                        KingJ2 = Mj;
                                    }
                                    if (game[Mi, Mj].Name == "")
                                    {
                                        game[Mi, Mj].Name = C.Name;
                                        game[i, j].Image = C.Image;  game[i, j].Image = C.Image; C.Name = null;C.Image = null;


                                        if (h1 == '1')
                                        {
                                            isCastling1 = true;
                                            h1 = '2';
                                        }
                                        else
                                        {
                                            h1 = '1';
                                            isCastling2 = true;
                                        }
                                    }
                                    else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                                    {
                                        game[Mi, Mj].BackColor = Color.Black;
                                        game[Mi, Mj].Name = C.Name;
                                        game[i, j].Image = C.Image;  game[i, j].Image = C.Image; C.Name = null;C.Image = null;



                                        if (h1 == '1')
                                        {
                                            isCastling1 = true;
                                            h1 = '2';
                                        }
                                        else
                                        {
                                            h1 = '1';
                                            isCastling2 = true;
                                        }
                                    }
                                    isFind = true;
                                }
                                else game[Mi, Mj].Name = s;
                            }
                            else
                            {
                                if (h1 == '1')
                                {
                                    KingI1 = Mi;
                                    KingJ1 = Mj;
                                }
                                else
                                {
                                    KingI2 = Mi;
                                    KingJ2 = Mj;
                                }
                                if (game[Mi, Mj].Name == "")
                                {
                                    game[Mi, Mj].Name = C.Name;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;


                                    if (h1 == '1')
                                    {
                                        isCastling1 = true;
                                        h1 = '2';
                                    }
                                    else
                                    {
                                        h1 = '1';
                                        isCastling2 = true;
                                    }
                                }
                                else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                                {
                                    game[Mi, Mj].BackColor = Color.Black;
                                    game[Mi, Mj].Name = C.Name;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;



                                    if (h1 == '1')
                                    {
                                        isCastling1 = true;
                                        h1 = '2';
                                    }
                                    else
                                    {
                                        h1 = '1';
                                        isCastling2 = true;
                                    }
                                }
                                isFind = true;
                            }
                    }
                    else if (game[Mi, Mj + 1].Name == C.Name)
                    {
                        if (IfChess())
                        {
                            s = game[Mi, Mj].Name;
                            game[Mi, Mj].Name = C.Name;
                            if (!IfChess())
                            {
                                game[Mi, Mj].Name = s;
                                if (h1 == '1')
                                {
                                    KingI1 = Mi;
                                    KingJ1 = Mj;
                                }
                                else
                                {
                                    KingI2 = Mi;
                                    KingJ2 = Mj;
                                }
                                if (game[Mi, Mj].Name == "")
                                {
                                    game[Mi, Mj].Name = C.Name;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;


                                    if (h1 == '1')
                                    {
                                        isCastling1 = true;
                                        h1 = '2';
                                    }
                                    else
                                    {
                                        h1 = '1';
                                        isCastling2 = true;
                                    }
                                }
                                else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                                {
                                    game[Mi, Mj].BackColor = Color.Black;
                                    game[Mi, Mj].Name = C.Name;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;



                                    if (h1 == '1')
                                    {
                                        isCastling1 = true;
                                        h1 = '2';
                                    }
                                    else
                                    {
                                        h1 = '1';
                                        isCastling2 = true;
                                    }
                                }
                                isFind = true;
                            }
                            else game[Mi, Mj].Name = s;
                        }
                        else
                        {
                            if (h1 == '1')
                            {
                                KingI1 = Mi;
                                KingJ1 = Mj;
                            }
                            else
                            {
                                KingI2 = Mi;
                                KingJ2 = Mj;
                            }
                            if (game[Mi, Mj].Name == "")
                            {
                                game[Mi, Mj].Name = C.Name;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                C.Text = "";
                                if (h1 == '1')
                                {
                                    isCastling1 = true;
                                    h1 = '2';
                                }
                                else
                                {
                                    h1 = '1';
                                    isCastling2 = true;
                                }
                            }
                            else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                            {
                                game[Mi, Mj].BackColor = Color.Black;
                                game[Mi, Mj].Name = C.Name;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                C.Text = "";

                                if (h1 == '1')
                                {
                                    isCastling1 = true;
                                    h1 = '2';
                                }
                                else
                                {
                                    h1 = '1';
                                    isCastling2 = true;
                                }
                            }
                            isFind = true;
                        }
                    }
                }
                else if (Mj == 0 && Mi == 0)
                {
                    if (game[Mi + 1, Mj].Name == C.Name)
                    {
                        if (IfChess())
                        {
                            s = game[Mi, Mj].Name;
                            game[Mi, Mj].Name = C.Name;
                            if (!IfChess())
                            {
                                game[Mi, Mj].Name = s;
                                if (h1 == '1')
                                {
                                    KingI1 = Mi;
                                    KingJ1 = Mj;
                                }
                                else
                                {
                                    KingI2 = Mi;
                                    KingJ2 = Mj;
                                }
                                if (game[Mi, Mj].Name == "")
                                {
                                    game[Mi, Mj].Name = C.Name;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;


                                    if (h1 == '1')
                                    {
                                        isCastling1 = true;
                                        h1 = '2';
                                    }
                                    else
                                    {
                                        h1 = '1';
                                        isCastling2 = true;
                                    }
                                }
                                else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                                {
                                    game[Mi, Mj].BackColor = Color.Black;
                                    game[Mi, Mj].Name = C.Name;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;



                                    if (h1 == '1')
                                    {
                                        isCastling1 = true;
                                        h1 = '2';
                                    }
                                    else
                                    {
                                        h1 = '1';
                                        isCastling2 = true;
                                    }
                                }
                                isFind = true;
                            }
                            else game[Mi, Mj].Name = s;
                        }
                        else
                        {
                            if (h1 == '1')
                            {
                                KingI1 = Mi;
                                KingJ1 = Mj;
                            }
                            else
                            {
                                KingI2 = Mi;
                                KingJ2 = Mj;
                            }
                            if (game[Mi, Mj].Name == "")
                            {
                                game[Mi, Mj].Name = C.Name;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                C.Text = "";
                                if (h1 == '1')
                                {
                                    isCastling1 = true;
                                    h1 = '2';
                                }
                                else
                                {
                                    h1 = '1';
                                    isCastling2 = true;
                                }
                            }
                            else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                            {
                                game[Mi, Mj].BackColor = Color.Black;
                                game[Mi, Mj].Name = C.Name;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                C.Text = "";

                                if (h1 == '1')
                                {
                                    isCastling1 = true;
                                    h1 = '2';
                                }
                                else
                                {
                                    h1 = '1';
                                    isCastling2 = true;
                                }
                            }
                            isFind = true;
                        }
                    }
                    else if (game[Mi, Mj + 1].Name == C.Name)
                    {
                        if (game[Mi, Mj].Name == "")
                            if (IfChess())
                            {
                                s = game[Mi, Mj].Name;
                                game[Mi, Mj].Name = C.Name;
                                if (!IfChess())
                                {
                                    game[Mi, Mj].Name = s;
                                    if (h1 == '1')
                                    {
                                        KingI1 = Mi;
                                        KingJ1 = Mj;
                                    }
                                    else
                                    {
                                        KingI2 = Mi;
                                        KingJ2 = Mj;
                                    }
                                    if (game[Mi, Mj].Name == "")
                                    {
                                        game[Mi, Mj].Name = C.Name;
                                        game[i, j].Image = C.Image;  game[i, j].Image = C.Image; C.Name = null;C.Image = null;


                                        if (h1 == '1')
                                        {
                                            isCastling1 = true;
                                            h1 = '2';
                                        }
                                        else
                                        {
                                            h1 = '1';
                                            isCastling2 = true;
                                        }
                                    }
                                    else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                                    {
                                        game[Mi, Mj].BackColor = Color.Black;
                                        game[Mi, Mj].Name = C.Name;
                                        game[i, j].Image = C.Image;  game[i, j].Image = C.Image; C.Name = null;C.Image = null;



                                        if (h1 == '1')
                                        {
                                            isCastling1 = true;
                                            h1 = '2';
                                        }
                                        else
                                        {
                                            h1 = '1';
                                            isCastling2 = true;
                                        }
                                    }
                                    isFind = true;
                                }
                                else game[Mi, Mj].Name = s;
                            }
                            else
                            {
                                if (h1 == '1')
                                {
                                    KingI1 = Mi;
                                    KingJ1 = Mj;
                                }
                                else
                                {
                                    KingI2 = Mi;
                                    KingJ2 = Mj;
                                }
                                if (game[Mi, Mj].Name == "")
                                {
                                    game[Mi, Mj].Name = C.Name;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;


                                    if (h1 == '1')
                                    {
                                        isCastling1 = true;
                                        h1 = '2';
                                    }
                                    else
                                    {
                                        h1 = '1';
                                        isCastling2 = true;
                                    }
                                }
                                else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                                {
                                    game[Mi, Mj].BackColor = Color.Black;
                                    game[Mi, Mj].Name = C.Name;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;



                                    if (h1 == '1')
                                    {
                                        isCastling1 = true;
                                        h1 = '2';
                                    }
                                    else
                                    {
                                        h1 = '1';
                                        isCastling2 = true;
                                    }
                                }
                                isFind = true;
                            }
                    }
                    else if (game[Mi + 1, Mj + 1].Name == C.Name)
                    {
                        if (IfChess())
                        {
                            s = game[Mi, Mj].Name;
                            game[Mi, Mj].Name = C.Name;
                            if (!IfChess())
                            {
                                game[Mi, Mj].Name = s;
                                if (h1 == '1')
                                {
                                    KingI1 = Mi;
                                    KingJ1 = Mj;
                                }
                                else
                                {
                                    KingI2 = Mi;
                                    KingJ2 = Mj;
                                }
                                if (game[Mi, Mj].Name == "")
                                {
                                    game[Mi, Mj].Name = C.Name;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;


                                    if (h1 == '1')
                                    {
                                        isCastling1 = true;
                                        h1 = '2';
                                    }
                                    else
                                    {
                                        h1 = '1';
                                        isCastling2 = true;
                                    }
                                }
                                else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                                {
                                    game[Mi, Mj].BackColor = Color.Black;
                                    game[Mi, Mj].Name = C.Name;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;



                                    if (h1 == '1')
                                    {
                                        isCastling1 = true;
                                        h1 = '2';
                                    }
                                    else
                                    {
                                        h1 = '1';
                                        isCastling2 = true;
                                    }
                                }
                                isFind = true;
                            }
                            else game[Mi, Mj].Name = s;
                        }
                        else
                        {
                            if (h1 == '1')
                            {
                                KingI1 = Mi;
                                KingJ1 = Mj;
                            }
                            else
                            {
                                KingI2 = Mi;
                                KingJ2 = Mj;
                            }
                            if (game[Mi, Mj].Name == "")
                            {
                                game[Mi, Mj].Name = C.Name;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                C.Text = "";
                                if (h1 == '1')
                                {
                                    isCastling1 = true;
                                    h1 = '2';
                                }
                                else
                                {
                                    h1 = '1';
                                    isCastling2 = true;
                                }
                            }
                            else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                            {
                                game[Mi, Mj].BackColor = Color.Black;
                                game[Mi, Mj].Name = C.Name;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                C.Text = "";

                                if (h1 == '1')
                                {
                                    isCastling1 = true;
                                    h1 = '2';
                                }
                                else
                                {
                                    h1 = '1';
                                    isCastling2 = true;
                                }
                            }
                            isFind = true;
                        }
                    }
                }

                if (h1 == '2' && !isCastling2 && Mj == 0 && (Mi == 1 && game[Mi - 1, Mj].Name == img1[0].ToString() || Mi == 6 && game[Mi + 1, Mj].Name == img1[0].ToString()))
                {
                    for (int g = 1; g < 3 && Mi - g > 3 && !isRook21; g++)
                    {
                        if (game[Mi - g, Mj].Name != "")
                            if (game[Mi - g, Mj].Name == C.Name)
                            {
                                if (IfChess())
                                {
                                    s = game[6, 0].Name;
                                    game[6, 0].Name = C.Name;
                                    if (!IfChess())
                                    {
                                        game[6, 0].Name = s;
                                        if (h1 == '1')
                                        {
                                            KingI1 = Mi;
                                            KingJ1 = Mj;
                                        }
                                        else
                                        {
                                            KingI2 = Mi;
                                            KingJ2 = Mj;
                                        }
                                        game[5, 0].BackColor = Color.Olive;
                                        game[6, 0].BackColor = Color.Olive;
                                        game[5, 0].Name = img1[0].ToString();
                                        game[5, 0].Text = img1[0].ToString();
                                        game[6, 0].Name = C.Name;
                                        game[6, 0].Text = C.Name;
                                        game[7, 0].Name = null;
                                        game[7, 0].Text = "";
                                        game[i, j].Image = C.Image;  game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                        h1 = '1';
                                        isRook21 = true;
                                        isRook22 = true;
                                        isCastling2 = true;
                                        isFind = true;
                                    }
                                    else game[6, 0].Name = s;
                                }
                                else
                                {
                                    if (h1 == '1')
                                    {
                                        KingI1 = Mi;
                                        KingJ1 = Mj;
                                    }
                                    else
                                    {
                                        KingI2 = Mi;
                                        KingJ2 = Mj;
                                    }
                                    game[5, 0].BackColor = Color.Olive;
                                    game[6, 0].BackColor = Color.Olive;
                                    game[5, 0].Name = img1[0].ToString();
                                    game[5, 0].Text = img1[0].ToString();
                                    game[6, 0].Name = C.Name;
                                    game[6, 0].Text = C.Name;
                                    game[7, 0].Name = null;
                                    game[7, 0].Text = "";
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                    h1 = '1';
                                    isRook21 = true;
                                    isRook22 = true;
                                    isCastling2 = true;
                                    isFind = true;
                                }
                            }
                            else break;
                    }
                    for (int g = 1; g < 4 && Mi + g < 5 && !isRook22; g++)
                    {
                        if (game[Mi + g, Mj].Name != "")
                            if (game[Mi + g, Mj].Name == C.Name)
                            {
                                if (!IfChess())
                                {
                                    s = game[1, 0].Name;
                                    game[1, 0].Name = C.Name;
                                    if (!IfChess())
                                    {
                                        game[1, 0].Name = s;
                                        if (h1 == '1')
                                        {
                                            KingI1 = Mi;
                                            KingJ1 = Mj;
                                        }
                                        else
                                        {
                                            KingI2 = Mi;
                                            KingJ2 = Mj;
                                        }
                                        game[2, 0].BackColor = Color.Olive;
                                        game[1, 0].BackColor = Color.Olive;
                                        game[2, 0].Name = img1[0].ToString();
                                        game[2, 0].Text = img1[0].ToString();
                                        game[1, 0].Name = C.Name;
                                        game[1, 0].Text = C.Name;
                                        game[0, 0].Name = null;
                                        game[0, 0].Text = "";
                                        game[i, j].Image = C.Image;  game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                        h1 = '1';
                                        isRook21 = true;
                                        isRook22 = true;
                                        isCastling2 = true;
                                        isFind = true;
                                    }
                                    else game[1, 0].Name = s;
                                }
                                else
                                {
                                    if (h1 == '1')
                                    {
                                        KingI1 = Mi;
                                        KingJ1 = Mj;
                                    }
                                    else
                                    {
                                        KingI2 = Mi;
                                        KingJ2 = Mj;
                                    }
                                    game[2, 0].BackColor = Color.Olive;
                                    game[1, 0].BackColor = Color.Olive;
                                    game[2, 0].Name = img1[0].ToString();
                                    game[2, 0].Text = img1[0].ToString();
                                    game[1, 0].Name = C.Name;
                                    game[1, 0].Text = C.Name;
                                    game[0, 0].Name = null;
                                    game[0, 0].Text = "";
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                    h1 = '1';
                                    isRook21 = true;
                                    isRook22 = true;
                                    isCastling2 = true;
                                    isFind = true;
                                }
                            }
                            else break;
                    }
                }
                else if (!isCastling1 && Mj == 7 && (Mi == 1 && game[Mi - 1, Mj].Name == img2[0].ToString() || Mi == 6 && game[Mi + 1, Mj].Name == img2[0].ToString()))
                {
                    for (int g = 1; g < 3 && Mi - g > -1 && !isRook11; g++)
                    {
                        if (game[Mi - g, Mj].Name != "")
                            if (game[Mi - g, Mj].Name == C.Name)
                            {
                                if (IfChess())
                                {
                                    s = game[6, 7].Name;
                                    game[6, 7].Name = C.Name;
                                    if (!IfChess())
                                    {
                                        game[6, 7].Name = s;
                                        if (h1 == '1')
                                        {
                                            KingI1 = Mi;
                                            KingJ1 = Mj;
                                        }
                                        else
                                        {
                                            KingI2 = Mi;
                                            KingJ2 = Mj;
                                        }
                                        game[5, 7].BackColor = Color.Olive;
                                        game[6, 7].BackColor = Color.Olive;
                                        game[5, 7].Name = img2[0].ToString();
                                        game[5, 7].Text = img2[0].ToString();
                                        game[6, 7].Name = C.Name;
                                        game[6, 7].Text = C.Name;
                                        game[7, 7].Name = null;
                                        game[7, 7].Text = "";
                                        game[i, j].Image = C.Image;  game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                        h1 = '2';
                                        isRook11 = true;
                                        isRook12 = true;
                                        isCastling1 = true;
                                        isFind = true;
                                    }
                                    else game[6, 7].Name = s;
                                }
                                else
                                {
                                    if (h1 == '1')
                                    {
                                        KingI1 = Mi;
                                        KingJ1 = Mj;
                                    }
                                    else
                                    {
                                        KingI2 = Mi;
                                        KingJ2 = Mj;
                                    }
                                    game[5, 7].BackColor = Color.Olive;
                                    game[6, 7].BackColor = Color.Olive;
                                    game[5, 7].Name = img2[0].ToString();
                                    game[5, 7].Text = img2[0].ToString();
                                    game[6, 7].Name = C.Name;
                                    game[6, 7].Text = C.Name;
                                    game[7, 7].Name = null;
                                    game[7, 7].Text = "";
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                    h1 = '2';
                                    isRook11 = true;
                                    isRook12 = true;
                                    isCastling1 = true;
                                    isFind = true;
                                }
                            }
                            else break;
                    }
                    for (int g = 1; g < 4 && Mi + g < 8 && !isRook12; g++)
                    {
                        if (game[Mi + g, Mj].Name != "")
                            if (game[Mi + g, Mj].Name == C.Name)
                            {
                                if (IfChess())
                                {
                                    s = game[1, 7].Name;
                                    game[1, 7].Name = C.Name;
                                    if (!IfChess())
                                    {
                                        game[1, 7].Name = s;
                                        if (h1 == '1')
                                        {
                                            KingI1 = Mi;
                                            KingJ1 = Mj;
                                        }
                                        else
                                        {
                                            KingI2 = Mi;
                                            KingJ2 = Mj;
                                        }
                                        game[2, 7].BackColor = Color.Olive;
                                        game[1, 7].BackColor = Color.Olive;
                                        game[2, 7].Name = img2[0].ToString();
                                        game[2, 7].Text = img2[0].ToString();
                                        game[1, 7].Name = C.Name;
                                        game[1, 7].Text = C.Name;
                                        game[0, 7].Name = null;
                                        game[0, 7].Text = "";
                                        game[i, j].Image = C.Image;  game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                        h1 = '2';
                                        isRook11 = true;
                                        isRook12 = true;
                                        isCastling1 = true;
                                        isFind = true;
                                    }
                                    else game[1, 7].Name = s;
                                }
                                else
                                {
                                    if (h1 == '1')
                                    {
                                        KingI1 = Mi;
                                        KingJ1 = Mj;
                                    }
                                    else
                                    {
                                        KingI2 = Mi;
                                        KingJ2 = Mj;
                                    }
                                    game[2, 7].BackColor = Color.Olive;
                                    game[1, 7].BackColor = Color.Olive;
                                    game[2, 7].Name = img2[0].ToString();
                                    game[2, 7].Text = img2[0].ToString();
                                    game[1, 7].Name = C.Name;
                                    game[1, 7].Text = C.Name;
                                    game[0, 7].Name = null;
                                    game[0, 7].Text = "";
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                    h1 = '2';
                                    isRook11 = true;
                                    isRook12 = true;
                                    isCastling1 = true;
                                    isFind = true;
                                }
                            }
                            else break;
                    }
                }
            }

            KingI1 = MinKingI1;
            KingJ1 = MinKingJ1;
            KingI2 = MinKingI2;
            KingJ2 = MinKingJ2;
        }
        #endregion

        #region//שורות
        private void Row(int Mi, int Mj, bool correct)
        {
            for (int g = Mi + 1; g < 8 && !isFind; g++)
            {
                if (!correct)
                {
                    if (IfChess())
                    {
                        s = game[g, Mj].Name;
                        game[g, Mj].Name = h1.ToString();
                        if (!IfChess())
                        {
                            if (game[g, Mj].Name == "")
                            {
                                game[g, Mj].BackColor = Color.Green;
                            }
                            else if (game[g, Mj].Name.ToString()[game[g, Mj].Name.Length - 1] != h1)
                            {
                                game[g, Mj].BackColor = Color.Red;
                                break;
                            }
                            else
                                break;
                        }
                        game[g, Mj].Name = s;
                    }
                    else
                    {
                        if (game[g, Mj].Name == "")
                        {
                            game[g, Mj].BackColor = Color.Green;
                        }
                        else if (game[g, Mj].Name.ToString()[game[g, Mj].Name.Length - 1] != h1)
                        {
                            game[g, Mj].BackColor = Color.Red;
                            break;
                        }
                        else
                            break;
                    }
                }
                else if (game[g, Mj].Name == C.Name)
                {
                    if (IfChess())
                    {
                        s = game[Mi, Mj].Name;
                        game[Mi, Mj].Name = C.Name;
                        if (!IfChess())
                        {
                            game[Mi, Mj].Name = s;
                            if (game[Mi, Mj].Name == "")
                            {
                                game[Mi, Mj].Name = C.Name;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                C.Text = "";
                                if (h1 == '1')
                                    h1 = '2';
                                else
                                    h1 = '1';
                            }
                            else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                            {
                                if (game[Mi, Mj].Name.Contains("מלך"))
                                {
                                    Mate = true;
                                    MessageBox.Show("plyer" + h1 + "winner");
                                }
                                else
                                {
                                    game[Mi, Mj].BackColor = Color.Black;
                                    game[Mi, Mj].Name = C.Name;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;


                                    if (h1 == '1')
                                        h1 = '2';
                                    else
                                        h1 = '1';
                                }
                            }
                        }
                        else
                            game[Mi, Mj].Name = s;
                    }
                    else
                    {
                        if (game[Mi, Mj].Name == "")
                        {
                            game[Mi, Mj].Name = C.Name;
                             game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                            C.Text = "";
                            if (h1 == '1')
                                h1 = '2';
                            else
                                h1 = '1';
                        }
                        else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                        {
                            if (game[Mi, Mj].Name.Contains("מלך"))
                            {
                                Mate = true;
                                MessageBox.Show("plyer" + h1 + "winner");
                            }
                            else
                            {
                                game[Mi, Mj].BackColor = Color.Black;
                                game[Mi, Mj].Name = C.Name;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                C.Text = "";
                                if (h1 == '1')
                                    h1 = '2';
                                else
                                    h1 = '1';
                            }
                        }
                    }
                    isFind = true;
                    break;
                }
                else if (game[g, Mj].Name != "")
                    break;
            }
            for (int g = Mi - 1; g >= 0 && !isFind; g--)
            {
                if (!correct)
                {
                    if (IfChess())
                    {
                        s = game[g, Mj].Name;
                        game[g, Mj].Name = h1.ToString();
                        if (!IfChess())
                        {
                            if (game[g, Mj].Name == "")
                            {
                                game[g, Mj].BackColor = Color.Green;
                            }
                            else if (game[g, Mj].Name.ToString()[game[g, Mj].Name.Length - 1] != h1)
                            {
                                game[g, Mj].BackColor = Color.Red;
                                break;
                            }
                            else
                                break;
                        }
                        game[g, Mj].Name = s;
                    }
                    else
                    {
                        if (game[g, Mj].Name == "")
                        {
                            game[g, Mj].BackColor = Color.Green;
                        }
                        else if (game[g, Mj].Name.ToString()[game[g, Mj].Name.Length - 1] != h1)
                        {
                            game[g, Mj].BackColor = Color.Red;
                            break;
                        }
                        else
                            break;
                    }
                }
                else if (game[g, Mj].Name == C.Name)
                {

                    if (IfChess())
                    {
                        s = game[Mi, Mj].Name;
                        game[Mi, Mj].Name = C.Name;
                        if (!IfChess())
                        {
                            if (game[Mi, Mj].Name == "")
                            {
                                game[Mi, Mj].Name = C.Name;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                C.Text = "";
                                if (h1 == '1')
                                    h1 = '2';
                                else
                                    h1 = '1';
                            }
                            else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                            {
                                if (game[Mi, Mj].Name.Contains("מלך"))
                                {
                                    Mate = true;
                                    MessageBox.Show("plyer" + h1 + "winner");
                                }
                                else
                                {
                                    game[Mi, Mj].BackColor = Color.Black;
                                    game[Mi, Mj].Name = C.Name;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;


                                    if (h1 == '1')
                                        h1 = '2';
                                    else
                                        h1 = '1';
                                }
                            }

                        }
                        else
                            game[Mi, Mj].Name = s;
                    }
                    else
                    {
                        if (game[Mi, Mj].Name == "")
                        {
                            game[Mi, Mj].Name = C.Name;
                             game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                            C.Text = "";
                            if (h1 == '1')
                                h1 = '2';
                            else
                                h1 = '1';
                        }
                        else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                        {
                            if (game[Mi, Mj].Name.Contains("מלך"))
                            {
                                Mate = true;
                                MessageBox.Show("plyer" + h1 + "winner");
                            }
                            else
                            {
                                game[Mi, Mj].BackColor = Color.Black;
                                game[Mi, Mj].Name = C.Name;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                C.Text = "";
                                if (h1 == '1')
                                    h1 = '2';
                                else
                                    h1 = '1';
                            }
                        }
                    }
                    isFind = true;
                    break;
                }
                else if (game[g, Mj].Name != "")
                    break;
            }
        }
        #endregion

        #region //עמודות
        private void Col(int Mi, int Mj, bool correct)//this is original colomb
        {
            for (int g = Mj + 1; g < 8 && !isFind; g++)
            {
                if (!correct)
                {
                    if (IfChess())
                    {
                        s = game[Mi, g].Name;
                        game[Mi, g].Name = h1.ToString();
                        if (!IfChess())
                        {
                            if (game[Mi, g].Name == "")
                            {
                                game[Mi, g].BackColor = Color.Green;
                            }
                            else if (game[Mi, g].Name.ToString()[game[Mi, g].Name.Length - 1] != h1)
                            {
                                game[Mi, g].BackColor = Color.Red;
                                break;
                            }
                            else
                                break;
                        }
                        game[Mi, g].Name = s;
                    }
                    else
                    {
                        if (game[Mi, g].Name == "")
                        {
                            game[Mi, g].BackColor = Color.Green;
                        }
                        else if (game[Mi, g].Name.ToString()[game[Mi, g].Name.Length - 1] != h1)
                        {
                            game[Mi, g].BackColor = Color.Red;
                            break;
                        }
                        else
                            break;
                    }
                }
                else if (game[Mi, g].Name == C.Name)
                {
                    if (IfChess())
                    {
                        s = game[Mi, Mj].Name;
                        game[Mi, Mj].Name = C.Name;
                        if (!IfChess())
                        {
                            if (game[Mi, Mj].Name == "")
                            {
                                game[Mi, Mj].Name = C.Name;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                C.Text = "";
                                if (h1 == '1')
                                    h1 = '2';
                                else
                                    h1 = '1';
                            }
                            else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                            {
                                if (game[Mi, Mj].Name.Contains("מלך"))
                                {
                                    Mate = true;
                                    MessageBox.Show("plyer" + h1 + "winner");
                                }
                                else
                                {
                                    game[Mi, Mj].BackColor = Color.Black;
                                    game[Mi, Mj].Name = C.Name;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;


                                    if (h1 == '1')
                                        h1 = '2';
                                    else
                                        h1 = '1';
                                }
                            }

                        }
                        else
                            game[Mi, Mj].Name = s;
                    }
                    else
                    {
                        if (game[Mi, Mj].Name == "")
                        {
                            game[Mi, Mj].Name = C.Name;
                             game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                            C.Text = "";
                            if (h1 == '1')
                                h1 = '2';
                            else
                                h1 = '1';
                        }
                        else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                        {
                            if (game[Mi, Mj].Name.Contains("מלך"))
                            {
                                Mate = true;
                                MessageBox.Show("plyer" + h1 + "winner");
                            }
                            else
                            {
                                game[Mi, Mj].BackColor = Color.Black;
                                game[Mi, Mj].Name = C.Name;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                C.Text = "";

                                if (h1 == '1')
                                    h1 = '2';
                                else
                                    h1 = '1';


                            }
                        }
                    }
                    isFind = true;
                    break;
                }
                else if (game[Mi, g].Name != "")
                    break;
            }
            for (int g = Mj - 1; g >= 0 && !isFind; g--)
            {
                if (!correct)
                {
                    if (IfChess())
                    {
                        s = game[Mi, g].Name;
                        game[Mi, g].Name = h1.ToString();
                        if (!IfChess())
                        {
                            if (game[Mi, g].Name == "")
                            {
                                game[Mi, g].BackColor = Color.Green;
                            }
                            else if (game[Mi, g].Name.ToString()[game[Mi, g].Name.Length - 1] != h1)
                            {
                                game[Mi, g].BackColor = Color.Red;
                                break;
                            }
                            else
                                break;
                        }
                        game[Mi, g].Name = s;
                    }
                    else
                    {
                        if (game[Mi, g].Name == "")
                        {
                            game[Mi, g].BackColor = Color.Green;
                        }
                        else if (game[Mi, g].Name.ToString()[game[Mi, g].Name.Length - 1] != h1)
                        {
                            game[Mi, g].BackColor = Color.Red;
                            break;
                        }
                        else
                            break;
                    }
                }
                else if (game[Mi, g].Name == C.Name)
                {
                    if (IfChess())
                    {
                        s = game[Mi, Mj].Name;
                        game[Mi, Mj].Name = C.Name;
                        if (!IfChess())
                        {
                            if (game[Mi, Mj].Name == "")
                            {
                                game[Mi, Mj].Name = C.Name;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                C.Text = "";
                                if (h1 == '1')
                                    h1 = '2';
                                else
                                    h1 = '1';
                            }
                            else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                            {
                                if (game[Mi, Mj].Name.Contains("מלך"))
                                {
                                    Mate = true;
                                    MessageBox.Show("plyer" + h1 + "winner");
                                }
                                else
                                {
                                    game[Mi, Mj].BackColor = Color.Black;
                                    game[Mi, Mj].Name = C.Name;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;


                                    if (h1 == '1')
                                        h1 = '2';
                                    else
                                        h1 = '1';
                                }
                            }

                        }
                        else
                            game[Mi, Mj].Name = s;
                    }
                    else
                    {
                        if (game[Mi, Mj].Name == "")
                        {
                            game[Mi, Mj].Name = C.Name;
                             game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                            C.Text = "";
                            if (h1 == '1')
                                h1 = '2';
                            else
                                h1 = '1';
                        }
                        else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                        {
                            if (game[Mi, Mj].Name.Contains("מלך"))
                            {
                                Mate = true;
                                MessageBox.Show("plyer" + h1 + "winner");
                            }
                            else
                            {
                                game[Mi, Mj].BackColor = Color.Black;
                                game[Mi, Mj].Name = C.Name;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                C.Text = "";

                                if (h1 == '1')
                                    h1 = '2';
                                else
                                    h1 = '1';
                            }
                        }
                    }
                    isFind = true;
                    break;

                }
                else if (game[Mi, g].Name != "")
                    break;
            }
        }
        #endregion

        #region//אלכסונים
        private void Dag(int Mi, int Mj, bool correct)
        {
            for (int g = 1; g < 8 && Mi - g >= 0 && Mj - g >= 0 && !isFind; g++)
            {
                if (!correct)
                {
                    if (IfChess())
                    {
                        s = game[Mi - g, Mj - g].Name;
                        game[Mi - g, Mj - g].Name = h1.ToString();
                        if (!IfChess())
                        {
                            game[Mi - g, Mj - g].Name = s;
                            if (game[Mi - g, Mj - g].Name == "")
                            {
                                game[Mi - g, Mj - g].BackColor = Color.Green;
                            }
                            else if (game[Mi - g, Mj - g].Name.ToString()[game[Mi - g, Mj - g].Name.Length - 1] != h1)
                            {
                                game[Mi - g, Mj - g].BackColor = Color.Red;
                                break;
                            }
                        }
                        else game[Mi - g, Mj - g].Name = s;
                    }
                    else
                    {
                        if (game[Mi - g, Mj - g].Name == "")
                        {
                            game[Mi - g, Mj - g].BackColor = Color.Green;
                        }
                        else if (game[Mi - g, Mj - g].Name.ToString()[game[Mi - g, Mj - g].Name.Length - 1] != h1)
                        {
                            game[Mi - g, Mj - g].BackColor = Color.Red;
                            break;
                        }
                        else
                            break;
                    }
                }
                else if (game[Mi - g, Mj - g].Name == C.Name)
                {
                    if (IfChess())
                    {
                        s = game[Mi, Mj].Name;
                        game[Mi, Mj].Name = C.Name;
                        if (!IfChess())
                        {
                            game[Mi, Mj].Name = s;
                            if (game[Mi, Mj].Name == "")
                            {
                                game[Mi, Mj].Name = C.Name;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                C.Text = "";
                                if (h1 == '1')
                                    h1 = '2';
                                else
                                    h1 = '1';
                            }
                            else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                            {
                                if (game[Mi, Mj].Name.Contains("מלך"))
                                {
                                    Mate = true;
                                    MessageBox.Show("plyer" + h1 + "winner");
                                }
                                else
                                {
                                    game[Mi, Mj].BackColor = Color.Black;
                                    game[Mi, Mj].Name = C.Name;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;


                                    if (h1 == '1')
                                        h1 = '2';
                                    else
                                        h1 = '1';
                                }
                            }

                        }
                        else
                            game[Mi, Mj].Name = s;
                    }
                    else
                    {
                        if (game[Mi, Mj].Name == "")
                        {
                            game[Mi, Mj].Name = C.Name;
                             game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                            C.Text = "";
                            if (h1 == '1')
                                h1 = '2';
                            else
                                h1 = '1';
                        }
                        else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                        {
                            if (game[Mi, Mj].Name.Contains("מלך"))
                            {
                                Mate = true;
                                MessageBox.Show("plyer" + h1 + "winner");
                            }
                            else
                            {
                                game[Mi, Mj].BackColor = Color.Black;
                                game[Mi, Mj].Name = C.Name;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                C.Text = "";

                                if (h1 == '1')
                                    h1 = '2';
                                else
                                    h1 = '1';


                            }
                        }
                    }
                    isFind = true;
                    break;
                }
                else if (game[Mi - g, Mj - g].Name != "")
                    break;
            }
            for (int g = 1; g < 8 && Mi + g < 8 && Mj - g >= 0 && !isFind; g++)
            {
                if (!correct)
                {
                    if (IfChess())
                    {
                        s = game[Mi + g, Mj - g].Name;
                        game[Mi + g, Mj - g].Name = h1.ToString();
                        if (!IfChess())
                        {
                            game[Mi + g, Mj - g].Name = s;
                            if (game[Mi + g, Mj - g].Name == "")
                            {
                                game[Mi + g, Mj - g].BackColor = Color.Green;
                            }
                            else if (game[Mi + g, Mj - g].Name.ToString()[game[Mi + g, Mj - g].Name.Length - 1] != h1)
                            {
                                game[Mi + g, Mj - g].BackColor = Color.Red;
                                break;
                            }
                        }
                        else game[Mi + g, Mj - g].Name = s;
                    }
                    else
                    {
                        if (game[Mi + g, Mj - g].Name == "")
                        {
                            game[Mi + g, Mj - g].BackColor = Color.Green;
                        }
                        else if (game[Mi + g, Mj - g].Name.ToString()[game[Mi + g, Mj - g].Name.Length - 1] != h1)
                        {
                            game[Mi + g, Mj - g].BackColor = Color.Red;
                            break;
                        }
                        else
                            break;
                    }
                }
                else if (game[Mi + g, Mj - g].Name == C.Name)
                {
                    if (IfChess())
                    {
                        s = game[Mi, Mj].Name;
                        game[Mi, Mj].Name = C.Name;
                        if (!IfChess())
                        {
                            if (game[Mi, Mj].Name == "")
                            {
                                game[Mi, Mj].Name = C.Name;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                C.Text = "";
                                if (h1 == '1')
                                    h1 = '2';
                                else
                                    h1 = '1';
                            }
                            else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                            {
                                if (game[Mi, Mj].Name.Contains("מלך"))
                                {
                                    Mate = true;
                                    MessageBox.Show("plyer" + h1 + "winner");
                                }
                                else
                                {
                                    game[Mi, Mj].BackColor = Color.Black;
                                    game[Mi, Mj].Name = C.Name;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;


                                    if (h1 == '1')
                                        h1 = '2';
                                    else
                                        h1 = '1';
                                }
                            }

                        }
                        else
                            game[Mi, Mj].Name = s;
                    }
                    else
                    {
                        if (game[Mi, Mj].Name == "")
                        {
                            game[Mi, Mj].Name = C.Name;
                             game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                            C.Text = "";
                            if (h1 == '1')
                                h1 = '2';
                            else
                                h1 = '1';
                        }
                        else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                        {
                            if (game[Mi, Mj].Name.Contains("מלך"))
                            {
                                Mate = true;
                                MessageBox.Show("plyer" + h1 + "winner");
                            }
                            else
                            {
                                game[Mi, Mj].BackColor = Color.Black;
                                game[Mi, Mj].Name = C.Name;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                C.Text = "";

                                if (h1 == '1')
                                    h1 = '2';
                                else
                                    h1 = '1';


                            }
                        }
                    }
                    isFind = true;
                    break;
                }
                else if (game[Mi + g, Mj - g].Name != "")
                    break;
            }
            for (int g = 1; g < 8 && Mi - g >= 0 && Mj + g < 8 && !isFind; g++)
            {
                if (!correct)
                {
                    if (IfChess())
                    {
                        s = game[Mi - g, Mj + g].Name;
                        game[Mi - g, Mj + g].Name = h1.ToString();
                        if (!IfChess())
                        {
                            game[Mi - g, Mj + g].Name = s;
                            if (game[Mi - g, Mj + g].Name == "")
                            {
                                game[Mi - g, Mj + g].BackColor = Color.Green;
                            }
                            else if (game[Mi - g, Mj + g].Name.ToString()[game[Mi - g, Mj + g].Name.Length - 1] != h1)
                            {
                                game[Mi - g, Mj + g].BackColor = Color.Red;
                                break;
                            }
                        }
                        else game[Mi - g, Mj + g].Name = s;
                    }
                    else
                    {
                        if (game[Mi - g, Mj + g].Name == "")
                            game[Mi - g, Mj + g].BackColor = Color.Green;
                        else if (game[Mi - g, Mj + g].Name.ToString()[game[Mi - g, Mj + g].Name.Length - 1] != h1)
                        {
                            game[Mi - g, Mj + g].BackColor = Color.Red;
                            break;
                        }
                        else
                            break;
                    }
                }
                else if (game[Mi - g, Mj + g].Name == C.Name)
                {
                    if (IfChess())
                    {
                        s = game[Mi, Mj].Name;
                        game[Mi, Mj].Name = C.Name;
                        if (!IfChess())
                        {
                            game[Mi, Mj].Name = s;
                            if (game[Mi, Mj].Name == "")
                            {
                                game[Mi, Mj].Name = C.Name;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                C.Text = "";
                                if (h1 == '1')
                                    h1 = '2';
                                else
                                    h1 = '1';
                            }
                            else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                            {
                                if (game[Mi, Mj].Name.Contains("מלך"))
                                {
                                    Mate = true;
                                    MessageBox.Show("plyer" + h1 + "winner");
                                }
                                else
                                {
                                    game[Mi, Mj].BackColor = Color.Black;
                                    game[Mi, Mj].Name = C.Name;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;


                                    if (h1 == '1')
                                        h1 = '2';
                                    else
                                        h1 = '1';
                                }
                            }

                        }
                        else
                            game[Mi, Mj].Name = s;
                    }
                    else
                    {
                        if (game[Mi, Mj].Name == "")
                        {
                            game[Mi, Mj].Name = C.Name;
                             game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                            C.Text = "";
                            if (h1 == '1')
                                h1 = '2';
                            else
                                h1 = '1';
                        }
                        else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                        {
                            if (game[Mi, Mj].Name.Contains("מלך"))
                            {
                                Mate = true;
                                MessageBox.Show("plyer" + h1 + "winner");
                            }
                            else
                            {
                                game[Mi, Mj].BackColor = Color.Black;
                                game[Mi, Mj].Name = C.Name;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                C.Text = "";

                                if (h1 == '1')
                                    h1 = '2';
                                else
                                    h1 = '1';


                            }
                        }
                    }
                    isFind = true;
                    break;
                }
                else if (game[Mi - g, Mj + g].Name != "")
                    break;
            }
            for (int g = 1; g < 8 && Mi + g < 8 && Mj + g < 8 && !isFind; g++)
            {
                if (!correct)
                {
                    if (IfChess())
                    {
                        s = game[Mi + g, Mj + g].Name;
                        game[Mi + g, Mj + g].Name = h1.ToString();
                        if (!IfChess())
                        {
                            game[Mi + g, Mj + g].Name = s;
                            if (game[Mi + g, Mj + g].Name == "")
                            {
                                game[Mi + g, Mj + g].BackColor = Color.Green;
                            }
                            else if (game[Mi + g, Mj + g].Name.ToString()[game[Mi + g, Mj + g].Name.Length - 1] != h1)
                            {
                                game[Mi + g, Mj + g].BackColor = Color.Red;
                                break;
                            }
                        }
                        else game[Mi + g, Mj + g].Name = s;
                    }
                    else
                    {
                        if (game[Mi + g, Mj + g].Name == "")
                        {
                            game[Mi + g, Mj + g].BackColor = Color.Green;
                        }
                        else if (game[Mi + g, Mj + g].Name.ToString()[game[Mi + g, Mj + g].Name.Length - 1] != h1)
                        {
                            game[Mi + g, Mj + g].BackColor = Color.Red;
                            break;
                        }
                        else
                            break;
                    }
                }
                else if (game[Mi + g, Mj + g].Name == C.Name)
                {
                    if (IfChess())
                    {
                        s = game[Mi, Mj].Name;
                        game[Mi, Mj].Name = C.Name;
                        if (!IfChess())
                        {
                            game[Mi, Mj].Name = s;
                            if (game[Mi, Mj].Name == "")
                            {
                                game[Mi, Mj].Name = C.Name;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                C.Text = "";
                                if (h1 == '1')
                                    h1 = '2';
                                else
                                    h1 = '1';
                            }
                            else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                            {
                                if (game[Mi, Mj].Name.Contains("מלך"))
                                {
                                    Mate = true;
                                    MessageBox.Show("plyer" + h1 + "winner");
                                }
                                else
                                {
                                    game[Mi, Mj].BackColor = Color.Black;
                                    game[Mi, Mj].Name = C.Name;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;


                                    if (h1 == '1')
                                        h1 = '2';
                                    else
                                        h1 = '1';
                                }
                            }
                        }
                        else
                            game[Mi, Mj].Name = s;
                    }
                    else
                    {
                        if (game[Mi, Mj].Name == "")
                        {
                            game[Mi, Mj].Name = C.Name;
                             game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                            C.Text = "";
                            if (h1 == '1')
                                h1 = '2';
                            else
                                h1 = '1';
                        }
                        else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                        {
                            if (game[Mi, Mj].Name.Contains("מלך"))
                            {
                                Mate = true;
                                MessageBox.Show("plyer" + h1 + "winner");
                            }
                            else
                            {
                                game[Mi, Mj].BackColor = Color.Black;
                                game[Mi, Mj].Name = C.Name;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                C.Text = "";

                                if (h1 == '1')
                                    h1 = '2';
                                else
                                    h1 = '1';


                            }
                        }
                    }
                    isFind = true;
                    break;
                }
                else if (game[Mi + g, Mj + g].Name != "")
                    break;
            }
        }
        #endregion

        #region //סוס
        private void Knight(int Mi, int Mj, bool correct)//...סוס יכול ללכת רק 
        {
            i = Mi;
            j = Mj;

            if (!correct)
            {
                if (IfChess())
                {
                    if (i + 2 < 8 && j + 1 < 8)
                    {
                        s = game[i + 2, j + 1].Name;
                        game[i + 2, j + 1].Name = h1.ToString();
                        if (!IfChess())
                        {
                            game[i + 2, j + 1].Name = s;
                            if (game[i + 2, j + 1].Name == "")
                            {
                                game[i + 2, j + 1].BackColor = Color.Green;
                            }
                            else if (game[i + 2, j + 1].Name.ToString()[game[i + 2, j + 1].Name.Length - 1] != h1)
                                game[i + 2, j + 1].BackColor = Color.Red;
                        }
                        else
                            game[i + 2, j + 1].Name = s;
                    }
                    if (i + 1 < 8 && j + 2 < 8)
                    {
                        s = game[i + 1, j + 2].Name;
                        game[i + 1, j + 2].Name = h1.ToString();
                        if (!IfChess())
                        {
                            game[i + 1, j + 2].Name = s;
                            if (game[i + 1, j + 2].Name == "")
                                game[i + 1, j + 2].BackColor = Color.Green;
                            else if (game[i + 1, j + 2].Name.ToString()[game[i + 1, j + 2].Name.Length - 1] != h1)
                                game[i + 1, j + 2].BackColor = Color.Red;
                        }
                        else
                            game[i + 1, j + 2].Name = s;
                    }
                    if (i + 2 < 8 && j - 1 >= 0)
                    {
                        s = game[i + 2, j - 1].Name;
                        game[i + 2, j - 1].Name = h1.ToString();
                        if (!IfChess())
                        {
                            game[i + 2, j - 1].Name = s;
                            if (game[i + 2, j - 1].Name == "")
                                game[i + 2, j - 1].BackColor = Color.Green;
                            else if (game[i + 2, j - 1].Name.ToString()[game[i + 2, j - 1].Name.Length - 1] != h1)
                                game[i + 2, j - 1].BackColor = Color.Red;
                        }
                        else
                            game[i + 2, j - 1].Name = s;
                    }
                    if (i + 1 < 8 && j - 2 >= 0)
                    {
                        s = game[i + 1, j - 2].Name;
                        game[i + 1, j - 2].Name = h1.ToString();
                        if (!IfChess())
                        {
                            game[i + 1, j - 2].Name = s;
                            if (game[i + 1, j - 2].Name == "")
                                game[i + 1, j - 2].BackColor = Color.Green;
                            else if (game[i + 1, j - 2].Name.ToString()[game[i + 1, j - 2].Name.Length - 1] != h1)
                                game[i + 1, j - 2].BackColor = Color.Red;
                        }
                        else
                            game[i + 1, j - 2].Name = s;

                    }
                    if (i - 1 >= 0 && j + 2 < 8)
                    {
                        s = game[i - 1, j + 2].Name;
                        game[i - 1, j + 2].Name = h1.ToString();
                        if (!IfChess())
                        {
                            game[i - 1, j + 2].Name = s;
                            if (game[i - 1, j + 2].Name == "")
                                game[i - 1, j + 2].BackColor = Color.Green;
                            else if (game[i - 1, j + 2].Name.ToString()[game[i - 1, j + 2].Name.Length - 1] != h1)
                                game[i - 1, j + 2].BackColor = Color.Red;
                        }
                        else game[i - 1, j + 2].Name = s;
                    }
                    if (i - 2 >= 0 && j - 1 >= 0)
                    {
                        s = game[i - 2, j - 1].Name;
                        game[i - 2, j - 1].Name = h1.ToString();
                        if (!IfChess())
                        {
                            game[i - 2, j - 1].Name = s;
                            if (game[i - 2, j - 1].Name == "")
                                game[i - 2, j - 1].BackColor = Color.Green;
                            else if (game[i - 2, j - 1].Name.ToString()[game[i - 2, j - 1].Name.Length - 1] != h1)
                                game[i - 2, j - 1].BackColor = Color.Red;
                        }
                        else
                            game[i - 2, j - 1].Name = s;
                    }
                    if (i - 1 >= 0 && j - 2 >= 0)
                    {
                        s = game[i - 1, j - 2].Name;
                        game[i - 1, j - 2].Name = h1.ToString();
                        if (!IfChess())
                        {
                            game[i - 1, j - 2].Name = s;
                            if (game[i - 1, j - 2].Name == "")
                                game[i - 1, j - 2].BackColor = Color.Green;
                            else if (game[i - 1, j - 2].Name.ToString()[game[i - 1, j - 2].Name.Length - 1] != h1)
                                game[i - 1, j - 2].BackColor = Color.Red;
                        }
                        else game[i - 1, j - 2].Name = s;
                    }
                    if (i - 2 >= 0 && j + 1 < 8)
                    {
                        s = game[i - 2, j + 1].Name;
                        game[i - 2, j + 1].Name = h1.ToString();
                        if (!IfChess())
                        {
                            game[i - 2, j + 1].Name = s;
                            if (game[i - 2, j + 1].Name == "")
                                game[i - 2, j + 1].BackColor = Color.Green;
                            else if (game[i - 2, j + 1].Name.ToString()[game[i - 2, j + 1].Name.Length - 1] != h1)
                                game[i - 2, j + 1].BackColor = Color.Red;
                        }
                        else game[i - 2, j + 1].Name = s;
                    }
                }
                else
                {
                    if (i + 2 < 8 && j + 1 < 8)
                    {
                        if (game[i + 2, j + 1].Name == "")
                        {
                            game[i + 2, j + 1].BackColor = Color.Green;
                        }
                        else if (game[i + 2, j + 1].Name.ToString()[game[i + 2, j + 1].Name.Length - 1] != h1)
                            game[i + 2, j + 1].BackColor = Color.Red;

                    }
                    if (i + 1 < 8 && j + 2 < 8)
                    {
                        if (game[i + 1, j + 2].Name == "")
                            game[i + 1, j + 2].BackColor = Color.Green;
                        else if (game[i + 1, j + 2].Name.ToString()[game[i + 1, j + 2].Name.Length - 1] != h1)
                            game[i + 1, j + 2].BackColor = Color.Red;
                    }
                    if (i + 2 < 8 && j - 1 >= 0)
                    {
                        if (game[i + 2, j - 1].Name == "")
                            game[i + 2, j - 1].BackColor = Color.Green;
                        else if (game[i + 2, j - 1].Name.ToString()[game[i + 2, j - 1].Name.Length - 1] != h1)
                            game[i + 2, j - 1].BackColor = Color.Red;
                    }
                    if (i + 1 < 8 && j - 2 >= 0)
                    {
                        if (game[i + 1, j - 2].Name == "")
                            game[i + 1, j - 2].BackColor = Color.Green;
                        else if (game[i + 1, j - 2].Name.ToString()[game[i + 1, j - 2].Name.Length - 1] != h1)
                            game[i + 1, j - 2].BackColor = Color.Red;
                    }
                    if (i - 1 >= 0 && j + 2 < 8)
                    {
                        if (game[i - 1, j + 2].Name == "")
                            game[i - 1, j + 2].BackColor = Color.Green;
                        else if (game[i - 1, j + 2].Name.ToString()[game[i - 1, j + 2].Name.Length - 1] != h1)
                            game[i - 1, j + 2].BackColor = Color.Red;
                    }
                    if (i - 2 >= 0 && j - 1 >= 0)
                    {
                        if (game[i - 2, j - 1].Name == "")
                            game[i - 2, j - 1].BackColor = Color.Green;
                        else if (game[i - 2, j - 1].Name.ToString()[game[i - 2, j - 1].Name.Length - 1] != h1)
                            game[i - 2, j - 1].BackColor = Color.Red;
                    }
                    if (i - 1 >= 0 && j - 2 >= 0)
                    {
                        if (game[i - 1, j - 2].Name == "")
                            game[i - 1, j - 2].BackColor = Color.Green;
                        else if (game[i - 1, j - 2].Name.ToString()[game[i - 1, j - 2].Name.Length - 1] != h1)
                            game[i - 1, j - 2].BackColor = Color.Red;
                    }
                    if (i - 2 >= 0 && j + 1 < 8)
                    {
                        if (game[i - 2, j + 1].Name == "")
                            game[i - 2, j + 1].BackColor = Color.Green;
                        else if (game[i - 2, j + 1].Name.ToString()[game[i - 2, j + 1].Name.Length - 1] != h1)
                            game[i - 2, j + 1].BackColor = Color.Red;

                    }
                }
            }
            else
            {
                bool okFound = false;
                if (IfChess())
                {
                    if (i + 1 < 8 && j + 2 < 8 && !okFound)
                    {
                        if (game[i + 1, j + 2].Name == C.Name)
                        {
                            s = game[Mi, Mj].Name;
                            game[Mi, Mj].Name = C.Name;
                            if (!IfChess())
                            {
                                game[Mi, Mj].Name = s;
                                if (game[i, j].Name == "")
                                {
                                    game[i, j].Name = C.Name;
                                    game[i, j].Text = C.Text;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                    if (h1 == '1')
                                        h1 = '2';
                                    else
                                        h1 = '1';
                                }
                                else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                                {
                                    if (game[Mi, Mj].Name.Contains("מלך"))
                                    {
                                        Mate = true;
                                        MessageBox.Show("plyer" + h1 + "winner");
                                    }
                                    else
                                    {
                                        game[Mi, Mj].BackColor = Color.Black;
                                        game[Mi, Mj].Name = C.Name;
                                        game[i, j].Image = C.Image;  game[i, j].Image = C.Image; C.Name = null;C.Image = null;


                                        if (h1 == '1')
                                            h1 = '2';
                                        else
                                            h1 = '1';
                                    }
                                }
                                isFind = true;
                                okFound = true;
                            }
                            else
                                game[Mi, Mj].Name = s;
                        }
                    }
                    if (i + 1 < 8 && j - 2 >= 0 && !okFound)
                    {
                        if (game[i + 1, j - 2].Name == C.Name)
                        {
                            s = game[Mi, Mj].Name;
                            game[Mi, Mj].Name = C.Name;
                            if (!IfChess())
                            {
                                game[Mi, Mj].Name = s;
                                if (game[i, j].Name == "")
                                {
                                    game[i, j].Name = C.Name;
                                    game[i, j].Text = C.Text;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                    if (h1 == '1')
                                        h1 = '2';
                                    else
                                        h1 = '1';
                                }
                                else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                                {
                                    if (game[Mi, Mj].Name.Contains("מלך"))
                                    {
                                        Mate = true;
                                        MessageBox.Show("plyer" + h1 + "winner");
                                    }
                                    else
                                    {
                                        game[Mi, Mj].BackColor = Color.Black;
                                        game[Mi, Mj].Name = C.Name;
                                        game[i, j].Image = C.Image;  game[i, j].Image = C.Image; C.Name = null;C.Image = null;


                                        if (h1 == '1')
                                            h1 = '2';
                                        else
                                            h1 = '1';
                                    }
                                }
                                isFind = true;
                                okFound = true;
                            }
                            else
                                game[Mi, Mj].Name = s;
                        }
                    }
                    if (i + 2 < 8 && j + 1 < 8 && !okFound)
                    {
                        if (game[i + 2, j + 1].Name == C.Name)
                        {
                            s = game[Mi, Mj].Name;
                            game[Mi, Mj].Name = C.Name;
                            if (!IfChess())
                            {
                                game[Mi, Mj].Name = s;
                                if (game[i, j].Name == "")
                                {
                                    game[i, j].Name = C.Name;
                                    game[i, j].Text = C.Text;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                    if (h1 == '1')
                                        h1 = '2';
                                    else
                                        h1 = '1';
                                }
                                else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                                {
                                    if (game[Mi, Mj].Name.Contains("מלך"))
                                    {
                                        Mate = true;
                                        MessageBox.Show("plyer" + h1 + "winner");
                                    }
                                    else
                                    {
                                        game[Mi, Mj].BackColor = Color.Black;
                                        game[Mi, Mj].Name = C.Name;
                                        game[i, j].Image = C.Image;  game[i, j].Image = C.Image; C.Name = null;C.Image = null;


                                        if (h1 == '1')
                                            h1 = '2';
                                        else
                                            h1 = '1';
                                    }
                                }
                                isFind = true;
                                okFound = true;
                            }
                            else
                                game[Mi, Mj].Name = s;
                        }
                    }
                    if (i + 2 < 8 && j - 1 >= 0 && !okFound)
                    {
                        if (game[i + 2, j - 1].Name == C.Name)
                        {
                            s = game[Mi, Mj].Name;
                            game[Mi, Mj].Name = C.Name;
                            if (!IfChess())
                            {
                                game[Mi, Mj].Name = s;
                                if (game[i, j].Name == "")
                                {
                                    game[i, j].Name = C.Name;
                                    game[i, j].Text = C.Text;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                    if (h1 == '1')
                                        h1 = '2';
                                    else
                                        h1 = '1';
                                }
                                else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                                {
                                    if (game[Mi, Mj].Name.Contains("מלך"))
                                    {
                                        Mate = true;
                                        MessageBox.Show("plyer" + h1 + "winner");
                                    }
                                    else
                                    {
                                        game[Mi, Mj].BackColor = Color.Black;
                                        game[Mi, Mj].Name = C.Name;
                                        game[i, j].Image = C.Image;  game[i, j].Image = C.Image; C.Name = null;C.Image = null;


                                        if (h1 == '1')
                                            h1 = '2';
                                        else
                                            h1 = '1';
                                    }
                                }
                                isFind = true;
                                okFound = true;
                            }
                            else
                                game[Mi, Mj].Name = s;
                        }
                    }
                    if (i - 1 >= 0 && j + 2 < 8 && !okFound)
                    {
                        if (game[i - 1, j + 2].Name == C.Name)
                        {
                            s = game[Mi, Mj].Name;
                            game[Mi, Mj].Name = C.Name;
                            if (!IfChess())
                            {
                                game[Mi, Mj].Name = s;
                                if (game[i, j].Name == "")
                                {
                                    game[i, j].Name = C.Name;
                                    game[i, j].Text = C.Text;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                    if (h1 == '1')
                                        h1 = '2';
                                    else
                                        h1 = '1';
                                }
                                else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                                {
                                    if (game[Mi, Mj].Name.Contains("מלך"))
                                    {
                                        Mate = true;
                                        MessageBox.Show("plyer" + h1 + "winner");
                                    }
                                    else
                                    {
                                        game[Mi, Mj].BackColor = Color.Black;
                                        game[Mi, Mj].Name = C.Name;
                                        game[i, j].Image = C.Image;  game[i, j].Image = C.Image; C.Name = null;C.Image = null;


                                        if (h1 == '1')
                                            h1 = '2';
                                        else
                                            h1 = '1';
                                    }
                                }
                                isFind = true;
                                okFound = true;
                            }
                            else
                                game[Mi, Mj].Name = s;
                        }
                    }
                    if (i - 2 >= 0 && j - 1 >= 0 && !okFound)
                    {
                        if (game[i - 2, j - 1].Name == C.Name)
                        {
                            s = game[Mi, Mj].Name;
                            game[Mi, Mj].Name = C.Name;
                            if (!IfChess())
                            {
                                game[Mi, Mj].Name = s;
                                if (game[i, j].Name == "")
                                {
                                    game[i, j].Name = C.Name;
                                    game[i, j].Text = C.Text;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                    if (h1 == '1')
                                        h1 = '2';
                                    else
                                        h1 = '1';
                                }
                                else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                                {
                                    if (game[Mi, Mj].Name.Contains("מלך"))
                                    {
                                        Mate = true;
                                        MessageBox.Show("plyer" + h1 + "winner");
                                    }
                                    else
                                    {
                                        game[Mi, Mj].BackColor = Color.Black;
                                        game[Mi, Mj].Name = C.Name;
                                        game[i, j].Image = C.Image;  game[i, j].Image = C.Image; C.Name = null;C.Image = null;


                                        if (h1 == '1')
                                            h1 = '2';
                                        else
                                            h1 = '1';
                                    }
                                }
                                isFind = true;
                                okFound = true;
                            }
                            else
                                game[Mi, Mj].Name = s;
                        }
                    }
                    if (i - 2 >= 0 && j + 1 < 8 && !okFound)
                    {
                        if (game[i - 2, j + 1].Name == C.Name)
                        {
                            s = game[Mi, Mj].Name;
                            game[Mi, Mj].Name = C.Name;
                            if (!IfChess())
                            {
                                game[Mi, Mj].Name = s;
                                if (game[i, j].Name == "")
                                {
                                    game[i, j].Name = C.Name;
                                    game[i, j].Text = C.Text;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                    if (h1 == '1')
                                        h1 = '2';
                                    else
                                        h1 = '1';
                                }
                                else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                                {
                                    if (game[Mi, Mj].Name.Contains("מלך"))
                                    {
                                        Mate = true;
                                        MessageBox.Show("plyer" + h1 + "winner");
                                    }
                                    else
                                    {
                                        game[Mi, Mj].BackColor = Color.Black;
                                        game[Mi, Mj].Name = C.Name;
                                        game[i, j].Image = C.Image;  game[i, j].Image = C.Image; C.Name = null;C.Image = null;


                                        if (h1 == '1')
                                            h1 = '2';
                                        else
                                            h1 = '1';
                                    }
                                }
                                isFind = true;
                                okFound = true;
                            }
                            else
                                game[Mi, Mj].Name = s;
                        }
                    }
                    if (i - 1 >= 0 && j - 2 >= 0 && !okFound)
                    {
                        if (game[i - 1, j - 2].Name == C.Name)
                        {
                            s = game[Mi, Mj].Name;
                            game[Mi, Mj].Name = C.Name;
                            if (!IfChess())
                            {
                                game[Mi, Mj].Name = s;
                                if (game[i, j].Name == "")
                                {
                                    game[i, j].Name = C.Name;
                                    game[i, j].Text = C.Text;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;

                                    if (h1 == '1')
                                        h1 = '2';
                                    else
                                        h1 = '1';
                                }
                                else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                                {
                                    if (game[Mi, Mj].Name.Contains("מלך"))
                                    {
                                        Mate = true;
                                        MessageBox.Show("plyer" + h1 + "winner");
                                    }
                                    else
                                    {
                                        game[Mi, Mj].BackColor = Color.Black;
                                        game[Mi, Mj].Name = C.Name;
                                        game[i, j].Image = C.Image;  game[i, j].Image = C.Image; C.Name = null;C.Image = null;


                                        if (h1 == '1')
                                            h1 = '2';
                                        else
                                            h1 = '1';
                                    }
                                }
                                isFind = true;
                                okFound = true;
                            }
                            else
                                game[Mi, Mj].Name = s;
                        }
                    }
                }
                else
                {
                    if (i + 1 < 8 && j + 2 < 8 && !okFound)
                    {
                        if (game[i + 1, j + 2].Name == C.Name)
                        {
                            if (game[i, j].Name == "")
                            {
                                game[i, j].Name = C.Name;
                                game[i, j].Text = C.Text;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;
                                C.Text = "";
                                if (h1 == '1')
                                    h1 = '2';
                                else
                                    h1 = '1';
                            }
                            else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                            {
                                if (game[Mi, Mj].Name.Contains("מלך"))
                                {
                                    Mate = true;
                                    MessageBox.Show("plyer" + h1 + "winner");
                                }
                                else
                                {
                                    game[Mi, Mj].BackColor = Color.Black;
                                    game[Mi, Mj].Name = C.Name;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;


                                    if (h1 == '1')
                                        h1 = '2';
                                    else
                                        h1 = '1';
                                }
                            }
                            isFind = true;
                            okFound = true;
                        }
                    }
                    if (i + 1 < 8 && j - 2 >= 0 && !okFound)
                    {
                        if (game[i + 1, j - 2].Name == C.Name)
                        {
                            if (game[i, j].Name == "")
                            {
                                game[i, j].Name = C.Name;
                                game[i, j].Text = C.Text;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;
                                C.Text = "";
                                if (h1 == '1')
                                    h1 = '2';
                                else
                                    h1 = '1';
                            }
                            else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                            {
                                if (game[Mi, Mj].Name.Contains("מלך"))
                                {
                                    Mate = true;
                                    MessageBox.Show("plyer" + h1 + "winner");
                                }
                                else
                                {
                                    game[Mi, Mj].BackColor = Color.Black;
                                    game[Mi, Mj].Name = C.Name;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;


                                    if (h1 == '1')
                                        h1 = '2';
                                    else
                                        h1 = '1';
                                }
                            }
                            isFind = true;
                            okFound = true;
                        }
                    }
                    if (i + 2 < 8 && j + 1 < 8 && !okFound)
                    {
                        if (game[i + 2, j + 1].Name == C.Name)
                        {
                            if (game[i, j].Name == "")
                            {
                                game[i, j].Name = C.Name;
                                game[i, j].Text = C.Text;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;
                                C.Text = "";
                                if (h1 == '1')
                                    h1 = '2';
                                else
                                    h1 = '1';
                            }
                            else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                            {
                                if (game[Mi, Mj].Name.Contains("מלך"))
                                {
                                    Mate = true;
                                    MessageBox.Show("plyer" + h1 + "winner");
                                }
                                else
                                {
                                    game[Mi, Mj].BackColor = Color.Black;
                                    game[Mi, Mj].Name = C.Name;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;


                                    if (h1 == '1')
                                        h1 = '2';
                                    else
                                        h1 = '1';
                                }
                            }
                            isFind = true;
                            okFound = true;
                        }
                    }
                    if (i + 2 < 8 && j - 1 >= 0 && !okFound)
                    {
                        if (game[i + 2, j - 1].Name == C.Name)
                        {
                            if (game[i, j].Name == "")
                            {
                                game[i, j].Name = C.Name;
                                game[i, j].Text = C.Text;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;
                                C.Text = "";
                                if (h1 == '1')
                                    h1 = '2';
                                else
                                    h1 = '1';
                            }
                            else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                            {
                                if (game[Mi, Mj].Name.Contains("מלך"))
                                {
                                    Mate = true;
                                    MessageBox.Show("plyer" + h1 + "winner");
                                }
                                else
                                {
                                    game[Mi, Mj].BackColor = Color.Black;
                                    game[Mi, Mj].Name = C.Name;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;


                                    if (h1 == '1')
                                        h1 = '2';
                                    else
                                        h1 = '1';
                                }
                            }
                            isFind = true;
                            okFound = true;
                        }
                    }
                    if (i - 1 >= 0 && j + 2 < 8 && !okFound)
                    {
                        if (game[i - 1, j + 2].Name == C.Name)
                        {
                            if (game[i, j].Name == "")
                            {
                                game[i, j].Name = C.Name;
                                game[i, j].Text = C.Text;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;
                                C.Text = "";
                                if (h1 == '1')
                                    h1 = '2';
                                else
                                    h1 = '1';
                            }
                            else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                            {
                                if (game[Mi, Mj].Name.Contains("מלך"))
                                {
                                    Mate = true;
                                    MessageBox.Show("plyer" + h1 + "winner");
                                }
                                else
                                {
                                    game[Mi, Mj].BackColor = Color.Black;
                                    game[Mi, Mj].Name = C.Name;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;


                                    if (h1 == '1')
                                        h1 = '2';
                                    else
                                        h1 = '1';
                                }
                            }
                            isFind = true;
                            okFound = true;
                        }
                    }
                    if (i - 2 >= 0 && j - 1 >= 0 && !okFound)
                    {
                        if (game[i - 2, j - 1].Name == C.Name)
                        {
                            if (game[i, j].Name == "")
                            {
                                game[i, j].Name = C.Name;
                                game[i, j].Text = C.Text;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;
                                C.Text = "";
                                if (h1 == '1')
                                    h1 = '2';
                                else
                                    h1 = '1';
                            }
                            else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                            {
                                if (game[Mi, Mj].Name.Contains("מלך"))
                                {
                                    Mate = true;
                                    MessageBox.Show("plyer" + h1 + "winner");
                                }
                                else
                                {
                                    game[Mi, Mj].BackColor = Color.Black;
                                    game[Mi, Mj].Name = C.Name;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;


                                    if (h1 == '1')
                                        h1 = '2';
                                    else
                                        h1 = '1';
                                }
                            }
                            isFind = true;
                            okFound = true;
                        }
                    }
                    if (i - 2 >= 0 && j + 1 < 8 && !okFound)
                    {
                        if (game[i - 2, j + 1].Name == C.Name)
                        {
                            if (game[i, j].Name == "")
                            {
                                game[i, j].Name = C.Name;
                                game[i, j].Text = C.Text;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;
                                C.Text = "";
                                if (h1 == '1')
                                    h1 = '2';
                                else
                                    h1 = '1';
                            }
                            else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                            {
                                if (game[Mi, Mj].Name.Contains("מלך"))
                                {
                                    Mate = true;
                                    MessageBox.Show("plyer" + h1 + "winner");
                                }
                                else
                                {
                                    game[Mi, Mj].BackColor = Color.Black;
                                    game[Mi, Mj].Name = C.Name;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;


                                    if (h1 == '1')
                                        h1 = '2';
                                    else
                                        h1 = '1';
                                }
                            }
                            isFind = true;
                            okFound = true;
                        }
                    }
                    if (i - 1 >= 0 && j - 2 >= 0 && !okFound)
                    {
                        if (game[i - 1, j - 2].Name == C.Name)
                        {
                            if (game[i, j].Name == "")
                            {
                                game[i, j].Name = C.Name;
                                game[i, j].Text = C.Text;
                                 game[i, j].Image = C.Image; C.Name = null;C.Image = null;
                                C.Text = "";
                                if (h1 == '1')
                                    h1 = '2';
                                else
                                    h1 = '1';
                            }
                            else if (game[Mi, Mj].Name.ToString()[game[Mi, Mj].Name.Length - 1] != h1)
                            {
                                if (game[Mi, Mj].Name.Contains("מלך"))
                                {
                                    Mate = true;
                                    MessageBox.Show("plyer" + h1 + "winner");
                                }
                                else
                                {
                                    game[Mi, Mj].BackColor = Color.Black;
                                    game[Mi, Mj].Name = C.Name;
                                     game[i, j].Image = C.Image; C.Name = null;C.Image = null;


                                    if (h1 == '1')
                                        h1 = '2';
                                    else
                                        h1 = '1';
                                }
                            }
                            isFind = true;
                            okFound = true;
                        }
                    }
                }
            }
        }
        #endregion

        #region//אם יש איום על המלך 
        private bool IfChess()
        {
            int g = 0;
            int Mi;
            int Mj;
            if (h1 == '1')
            {
                Mi = KingI1;
                Mj = KingJ1;
            }
            else
            {
                Mi = KingI2;
                Mj = KingJ2;
            }
            //חייל
            if (h1 == '1')
            {
                if (Mi > 0 && Mj > 1)
                    if (game[Mi - 1, Mj - 1].Name.Contains("חייל") && game[Mi - 1, Mj - 1].Name.ToString()[game[Mi - 1, Mj - 1].Name.Length - 1] != h1)
                        return true;
                if (Mi < 7 && Mj > 1)
                    if (game[Mi + 1, Mj - 1].Name.Contains("חייל") && game[Mi + 1, Mj - 1].Name.ToString()[game[Mi + 1, Mj - 1].Name.Length - 1] != h1)
                        return true;
            }
            else
            {
                if (Mi > 0 && Mj < 6)
                    if (game[Mi - 1, Mj + 1].Name.Contains("חייל") && game[Mi - 1, Mj + 1].Name.ToString()[game[Mi - 1, Mj + 1].Name.Length - 1] != h1)
                        return true;
                if (Mi < 7 && Mj < 6)
                    if (game[Mi + 1, Mj + 1].Name.Contains("חייל") && game[Mi + 1, Mj + 1].Name.ToString()[game[Mi + 1, Mj + 1].Name.Length - 1] != h1)
                        return true;
            }
            //שורה
            for (g = Mi + 1; g < 8; g++)
            {
                if (game[g, Mj].Name != "")
                {
                    if ((game[g, Mj].Name.Contains("מלכה") || game[g, Mj].Name.Contains("טורה") || game[g, Mj].Name.Contains("מלך")) && game[g, Mj].Name.ToString()[game[g, Mj].Name.Length - 1] != h1)
                        return true;
                    else
                        break;
                }

            }
            for (g = Mi - 1; g >= 0; g--)
            {
                if (game[g, Mj].Name != "")
                {
                    if ((game[g, Mj].Name.Contains("מלכה") || game[g, Mj].Name.Contains("טורה") || game[g, Mj].Name.Contains("מלך")) && game[g, Mj].Name.ToString()[game[g, Mj].Name.Length - 1] != h1)
                        return true;
                    else
                        break;
                }
            }
            //עמודה
            for (g = Mj + 1; g < 8; g++)
            {
                if (game[Mi, g].Name != "")
                {
                    if ((game[Mi, g].Name.Contains("מלכה") || game[Mi, g].Name.Contains("טורה") || game[Mi, g].Name.Contains("מלך")) && game[Mi, g].Name.ToString()[game[Mi, g].Name.Length - 1] != h1)
                        return true;
                    else
                        break;
                }
            }
            for (g = Mj - 1; g >= 0; g--)
            {
                if (game[Mi, g].Name != "")
                {
                    if ((game[Mi, g].Name.Contains("מלכה") || game[Mi, g].Name.Contains("טורה") || game[Mi, g].Name.Contains("מלך")) && game[Mi, g].Name.ToString()[game[Mi, g].Name.Length - 1] != h1)
                        return true;
                    else
                        break;
                }
            }
            //אלכסונים
            for (g = 1; g < 8 && Mi - g >= 0 && Mj - g >= 0; g++)
            {
                if (game[Mi - g, Mj - g].Name != "")
                {
                    if ((game[Mi - g, Mj - g].Name.Contains("מלכה") || game[Mi - g, Mj - g].Name.Contains("רץ")) && game[Mi - g, Mj - g].Name.ToString()[game[Mi - g, Mj - g].Name.Length - 1] != h1)
                        return true;
                    else
                        break;
                }
            }
            for (g = 1; g < 8 && Mi + g < 8 && Mj - g >= 0; g++)
            {
                if (game[Mi + g, Mj - g].Name != "")
                {
                    if ((game[Mi + g, Mj - g].Name.Contains("מלכה") || game[Mi + g, Mj - g].Name.Contains("רץ")) && game[Mi + g, Mj - g].Name.ToString()[game[Mi + g, Mj - g].Name.Length - 1] != h1)
                        return true;
                    else
                        break;
                }
            }
            for (g = 1; g < 8 && Mi - g >= 0 && Mj + g < 8; g++)
            {
                if (game[Mi - g, Mj + g].Name != "")
                {
                    if ((game[Mi - g, Mj + g].Name.Contains("מלכה") || game[Mi - g, Mj + g].Name.Contains("רץ")) && game[Mi - g, Mj + g].Name.ToString()[game[Mi - g, Mj + g].Name.Length - 1] != h1)
                        return true;
                    else
                        break;
                }
            }
            for (g = 1; g < 8 && Mi + g < 8 && Mj + g < 8; g++)
            {
                if (game[Mi + g, Mj + g].Name != "")
                {
                    if ((game[Mi + g, Mj + g].Name.Contains("מלכה") || game[Mi + g, Mj + g].Name.Contains("רץ")) && game[Mi + g, Mj + g].Name.ToString()[game[Mi + g, Mj + g].Name.Length - 1] != h1)
                        return true;
                    else
                        break;
                }
            }
            //סוס
            if (Mi + 2 < 8 && Mj + 1 < 8)
            {
                if (game[Mi + 2, Mj + 1].Name != "")
                    if (game[Mi + 2, Mj + 1].Name.Contains("סוס") && game[Mi + 2, Mj + 1].Name.ToString()[game[Mi + 2, Mj + 1].Name.Length - 1] != h1)
                        return true;
            }
            if (Mi + 1 < 8 && Mj + 2 < 8)
            {
                if (game[Mi + 1, Mj + 2].Name != "")
                    if (game[Mi + 1, Mj + 2].Name.Contains("סוס") && game[Mi + 1, Mj + 2].Name.ToString()[game[Mi + 1, Mj + 2].Name.Length - 1] != h1)
                        return true;
            }
            if (Mi + 2 < 8 && Mj - 1 >= 0)
            {
                if (game[Mi + 2, Mj - 1].Name != "")
                    if (game[Mi + 2, Mj - 1].Name.Contains("סוס") && game[Mi + 2, Mj - 1].Name.ToString()[game[Mi + 2, Mj - 1].Name.Length - 1] != h1)
                        return true;
            }
            if (Mi + 1 < 8 && Mj - 2 >= 0)
            {
                if (game[Mi + 1, Mj - 2].Name != "")
                    if (game[Mi + 1, Mj - 2].Name.Contains("סוס") && game[Mi + 1, Mj - 2].Name.ToString()[game[Mi + 1, Mj - 2].Name.Length - 1] != h1)
                        return true;
            }
            if (Mi - 1 >= 0 && Mj + 2 < 8)
            {
                if (game[Mi - 1, Mj + 2].Name != "")
                    if (game[Mi - 1, Mj + 2].Name.Contains("סוס") && game[Mi - 1, Mj + 2].Name.ToString()[game[Mi - 1, Mj + 2].Name.Length - 1] != h1)
                        return true;
            }
            if (Mi - 2 >= 0 && Mj - 1 >= 0)
            {
                if (game[Mi - 2, Mj - 1].Name != "")
                    if (game[Mi - 2, Mj - 1].Name.Contains("סוס") && game[Mi - 2, Mj - 1].Name.ToString()[game[Mi - 2, Mj - 1].Name.Length - 1] != h1)
                        return true;
            }
            if (Mi - 1 >= 0 && Mj - 2 >= 0)
            {
                if (game[Mi - 1, Mj - 2].Name != "")
                    if (game[Mi - 1, Mj - 2].Name.Contains("סוס") && game[Mi - 1, Mj - 2].Name.ToString()[game[Mi - 1, Mj - 2].Name.Length - 1] != h1)
                        return true;
            }
            if (Mi - 2 >= 0 && Mj + 1 < 8)
            {
                if (game[Mi - 2, Mj + 1].Name != "")
                    if (game[Mi - 2, Mj + 1].Name.Contains("סוס") && game[Mi - 2, Mj + 1].Name.ToString()[game[Mi - 2, Mj + 1].Name.Length - 1] != h1)
                        return true;

            }

            return false;
        }
        #endregion

        #region//if Pat
        private bool Pat()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    /*   if (game[i, j].Name != "")
                           if (game[i, j].Name.ToString()[game[i, j].Name.Length - 1] == h1)
                           {
                           switch (game[i, j].Name)
                           {
                               case ("טורה1"):
                                   {
                                       Row(i, j, true);
                                       Col(i, j, true);
                                   }
                                   break;
                               case ("מלכה1"):
                                   {
                                       Col(i, j, true);
                                       Row(i, j, true);
                                       Dag(i, j, true);
                                   }
                                   break;
                               case ("רץ1"):
                                   {
                                       Dag(i, j, true);
                                   }
                                   break;
                               case ("סוס1"):
                                   {
                                       Knight(i, j, true);
                                   }
                                   break;
                               case ("מלך1"):
                                   {
                                       King(i, j, true);
                                   }
                                   break;
                               case ("מלך2"):
                                   {
                                       King(i, j, true);
                                   }
                                   break;
                               case ("טורה2"):
                                   {
                                       Col(i, j, true);
                                       Row(i, j, true);
                                   }
                                   break;
                               case ("מלכה2"):
                                   {
                                       Col(i, j, true);
                                       Row(i, j, true);
                                       Dag(i, j, true);
                                   }
                                   break;
                               case ("רץ2"):
                                   {
                                       Dag(i, j, true);
                                   }
                                   break;
                               case ("סוס2"):
                                   {
                                       Knight(i, j, true);
                                   }
                                   break;
                               case ("חייל1"):
                                   {

                                       if (j + 1 == jj)
                                       {
                                           if ((i == ii && game[i, j].Name == "") ||
                                               (i > 0 && ii - 1 == i && game[i, j].Name.ToString()[game[i, j].Name.Length - 1] != h1)
                                               || (i < 8 && ii + 1 == i && game[i, j].Name != "" && game[i, j].Name.ToString()[game[i, j].Name.Length - 1] != h1)
                                               || (i < 8 && i > 0 && ((ii + 1 == i) || (ii - 1 == i)) && (game[i, j].Name != "" && game[i, j].Name.ToString()[game[i, j].Name.Length - 1] != h1)))
                                           {
                                               if (IfChess())
                                               {
                                                   s = game[i, j].Name;
                                                   game[i, j].Name = C.Name;
                                                   if (!IfChess())
                                                   {
                                                       game[i, j].Name = s;
                                                       if (game[i, j].Name != "")
                                                           game[i, j].BackColor = Color.Black;
                                                       game[i, j].Name = C.Name;
                       game[i, j].Image = C.Image;              game[i, j].Image = C.Image; C.Name = null;C.Image = null;
                                                       game[i, j].Text = C.Text;

                                                       if (h1 == '1')
                                                           h1 = '2';
                                                       else
                                                           h1 = '1';
                                                   }
                                                   else game[i, j].Name = s;
                                               }
                                               else
                                               {
                                                   if (game[i, j].Name != "")
                                                       game[i, j].BackColor = Color.Black;
                                                   game[i, j].Name = C.Name;
                   game[i, j].Image = C.Image;              game[i, j].Image = C.Image; C.Name = null;C.Image = null;
                                                   game[i, j].Text = C.Text;

                                                   if (h1 == '1')
                                                       h1 = '2';
                                                   else
                                                       h1 = '1';
                                               }
                                           }
                                       }
                                       else if (j + 2 == jj && jj == 6)
                                           if (i == ii && game[i, j].Name == "" && game[i, j + 1].Name == "")
                                           {
                                               if (IfChess())
                                               {
                                                   s = game[i, j].Name;
                                                   game[i, j].Name = C.Name;
                                                   if (!IfChess())
                                                   {
                                                       game[i, j].Name = s;
                                                       if (game[i, j].Name != "")
                                                           game[i, j].BackColor = Color.Black;
                                                       game[i, j].Name = C.Name;
                       game[i, j].Image = C.Image;              game[i, j].Image = C.Image; C.Name = null;C.Image = null;
                                                       game[i, j].Text = C.Text;

                                                       if (h1 == '1')
                                                           h1 = '2';
                                                       else
                                                           h1 = '1';
                                                   }
                                                   else game[i, j].Name = s;
                                               }
                                               else
                                               {
                                                   if (game[i, j].Name != "")
                                                       game[i, j].BackColor = Color.Black;
                                                   game[i, j].Name = C.Name;
                   game[i, j].Image = C.Image;              game[i, j].Image = C.Image; C.Name = null;C.Image = null;
                                                   game[i, j].Text = C.Text;

                                                   if (h1 == '1')
                                                       h1 = '2';
                                                   else
                                                       h1 = '1';
                                               }
                                           }
                                   }
                                   break;
                               case ("חייל2"):
                                   {
                                       if (j - 1 == jj)
                                       {
                                           if ((i == ii && game[i, j].Name == "") ||
                                               (i > 0 && ii - 1 == i && game[i, j].Name.ToString()[game[i, j].Name.Length - 1] != h1)
                                               || (i < 8 && ii + 1 == i && game[i, j].Name != "" && game[i, j].Name.ToString()[game[i, j].Name.Length - 1] != h1)
                                               || (i < 8 && i > 0 && ((ii - 1 == i) || (ii + 1 == i)) && (game[i, j].Name != "" && game[i, j].Name.ToString()[game[i, j].Name.Length - 1] != h1)))
                                           {
                                               if (IfChess())
                                               {
                                                   s = game[i, j].Name;
                                                   game[i, j].Name = C.Name;
                                                   if (!IfChess())
                                                   {
                                                       game[i, j].Name = s;
                                                       if (game[i, j].Name != "")
                                                           game[i, j].BackColor = Color.Black;
                                                       game[i, j].Name = C.Name;
                       game[i, j].Image = C.Image;              game[i, j].Image = C.Image; C.Name = null;C.Image = null;
                                                       game[i, j].Text = C.Text;

                                                       if (h1 == '1')
                                                           h1 = '2';
                                                       else
                                                           h1 = '1';
                                                   }
                                                   else game[i, j].Name = s;
                                               }
                                               else
                                               {
                                                   if (game[i, j].Name != "")
                                                       game[i, j].BackColor = Color.Black;
                                                   game[i, j].Name = C.Name;
                   game[i, j].Image = C.Image;              game[i, j].Image = C.Image; C.Name = null;C.Image = null;
                                                   game[i, j].Text = C.Text;

                                                   if (h1 == '1')
                                                       h1 = '2';
                                                   else
                                                       h1 = '1';
                                               }
                                           }
                                       }
                                       else if (jj == j - 2 && jj == 1)
                                       {
                                           if (i == ii && game[i, j].Name == "" && game[i, j - 1].Name == "")
                                           {
                                               if (IfChess())
                                               {
                                                   s = game[i, j].Name;
                                                   game[i, j].Name = C.Name;
                                                   if (!IfChess())
                                                   {
                                                       game[i, j].Name = s;
                                                       game[i, j].Name = C.Name;
                       game[i, j].Image = C.Image;              game[i, j].Image = C.Image; C.Name = null;C.Image = null;
                                                       game[i, j].Text = C.Text;

                                                       if (h1 == '1')
                                                           h1 = '2';
                                                       else
                                                           h1 = '1';
                                                   }
                                                   else game[i, j].Name = s;
                                               }
                                               else
                                               {
                                                   game[i, j].Name = C.Name;
                   game[i, j].Image = C.Image;              game[i, j].Image = C.Image; C.Name = null;C.Image = null;
                                                   game[i, j].Text = C.Text;

                                                   if (h1 == '1')
                                                       h1 = '2';
                                                   else
                                                       h1 = '1';
                                               }
                                           }
                                       }
                                   }
                                   break;
                           }
                           if (isFind == true)
                               return false;
                       }*/
                }
            }
            return true;
        }
        #endregion
    }
}