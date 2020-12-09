using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DuckSimulator
{
	public partial class Form1 : Form
	{
		Random rnd = new Random();
		int x, y, z, k1, k2 = 0;
		double balance = 10;
		int maxduck = 10;
		int ID = 0;
		string[] names = {"Бернард","Франклин","Дункан","Фрезер","Монти","Шарлемань","Цезарь","Гордо"};
		List<Duck> duckList = new List<Duck>();
		List<int> duckangelx = new List<int>();
		List<int> duckangely = new List<int>();
		List<PictureBox> duckImg = new List<PictureBox>();
		public Form1()
		{
			InitializeComponent();
			tableLayoutPanel1.Visible = false;

		}
		public PictureBox GenPct()
		{
			PictureBox pct = new PictureBox();
			pct.Click += GetInfo;
			pct.Tag = ID;
			ID++;
			pct.Size = new Size(10, 10);
			pct.BackColor = Color.FromArgb(247, 255, 1);
			pct.BorderStyle = BorderStyle.FixedSingle;
			pct.Location = new Point(rnd.Next(0, pictureBox1.Width), rnd.Next(0, pictureBox1.Height));
			return pct;
		}
		public PictureBox GenUniqPct()
		{
			PictureBox pct = new PictureBox();
			pct.Click += GetInfo;
			pct.Tag = ID;
			ID++;
			pct.Size = new Size(10, 10);
			pct.BackColor = Color.FromArgb(rnd.Next(0,256), rnd.Next(0, 256), rnd.Next(0, 256));
			pct.BorderStyle = BorderStyle.FixedSingle;
			pct.Location = new Point(rnd.Next(0, pictureBox1.Width), rnd.Next(0, pictureBox1.Height));
			return pct;
		}
		public PictureBox GenSparPct(int x, int y, int z, int k1, int k2)
		{
			PictureBox pct = new PictureBox();
			pct.Click += GetInfo;
			pct.Tag = ID;
			ID++;
			pct.Size = new Size(10, 10);
			pct.BackColor = Color.FromArgb(x,y,z);
			pct.BorderStyle = BorderStyle.FixedSingle;
			pct.Location = new Point(k1, k2);
			return pct;
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			label2.Text = $"Количество уточек: {duckList.Count}/{maxduck}";
			label3.Text = $"Бюджет озера: {(int)balance}$";
			label4.Text = $"Доход озера: { duckList.Count}$ / день";
			if (duckList.Count > 1)
			{
				for (int i = 0; i < duckList.Count-1; i++)
				{
					for (int j = i+1; j < duckList.Count; j++)
					{
						if (duckList.Count > 1 && duckImg[i].Bounds.IntersectsWith(duckImg[j].Bounds) && rnd.Next(0, 100) == 99 && duckList.Count < maxduck)
						{
							Sparivanie(duckList[Convert.ToInt32(duckImg[i].Tag)], duckList[Convert.ToInt32(duckImg[j].Tag)]);
							Console.WriteLine("Спаривание удалось!");
						}
					}
				}
			}
			for (int i = 0; i < duckList.Count; i++)
			{
				if (duckangelx[i] == 0 && duckangely[i] == 0)
				{duckangelx[i] = rnd.Next(-3, 3);
					duckangelx[i] = rnd.Next(-3, 3);
				}
				if (duckImg[i].Location.X >= 0 && duckImg[i].Location.X <= pictureBox1.Width && duckImg[i].Location.Y >= 0 && duckImg[i].Location.Y <= pictureBox1.Height)
				{
					duckImg[i].Location = new Point(duckImg[i].Location.X + duckangelx[i], duckImg[i].Location.Y + duckangely[i]);
				}
				else if (duckImg[i].Location.X <= 0)
				{
					duckImg[i].Location = new Point(duckImg[i].Location.X + 5, duckImg[i].Location.Y);
					duckangelx[i] = rnd.Next(-3, 3);
					duckangely[i] = rnd.Next(-3, 3);
				}
				else if (duckImg[i].Location.X >= pictureBox1.Width - duckImg[i].Width*2)
				{
					duckImg[i].Location = new Point(duckImg[i].Location.X - 5, duckImg[i].Location.Y);
					duckangelx[i] = rnd.Next(-3, 3);
					duckangely[i] = rnd.Next(-3, 3);
				}
				else if (duckImg[i].Location.Y <= 0)
				{
					duckImg[i].Location = new Point(duckImg[i].Location.X, duckImg[i].Location.Y + 5);
					duckangelx[i] = rnd.Next(-3, 3);
					duckangely[i] = rnd.Next(-3, 3);
				}
				else if (duckImg[i].Location.Y >= pictureBox1.Height - duckImg[i].Height*2)
				{
					duckImg[i].Location = new Point(duckImg[i].Location.X, duckImg[i].Location.Y - 5);
					duckangelx[i] = rnd.Next(-3, 3);
					duckangely[i] = rnd.Next(-3, 3);
				}
			}
		}
		public void GetInfo(object sender, EventArgs e)
		{
			Button btn = new Button();
			tableLayoutPanel1.Controls.Clear();
			var pct = (PictureBox)sender;
			Label lbl = new Label();
			Label lbl1 = new Label();
			Label lbl2 = new Label();
			Label lbl3 = new Label();
			Label lbl4 = new Label();
			if (Convert.ToInt32(pct.Tag) == duckList[Convert.ToInt32(pct.Tag)].getid())
			{
				lbl.Text = $"Имя: {duckList[Convert.ToInt32(pct.Tag)].getname()}";
				if (duckList[Convert.ToInt32(pct.Tag)].getage() < 7)
				{
					lbl1.Text = $"Возраст: {duckList[Convert.ToInt32(pct.Tag)].getage()} дн.";
				}
				if (duckList[Convert.ToInt32(pct.Tag)].getage() > 7 && duckList[Convert.ToInt32(pct.Tag)].getage() < 31)
				{
					lbl1.Text = $"Возраст: {duckList[Convert.ToInt32(pct.Tag)].getage() / 7} нед.";
				}
				if (duckList[Convert.ToInt32(pct.Tag)].getage() > 31)
				{
					lbl1.Text = $"Возраст: {duckList[Convert.ToInt32(pct.Tag)].getage() / 31} мес.";
				}
				if (duckList[Convert.ToInt32(pct.Tag)].getage() > 365)
				{
					lbl1.Text = $"Возраст: {duckList[Convert.ToInt32(pct.Tag)].getage() / 365} г.";
				}
				lbl.AutoSize = true;
				lbl1.AutoSize = true;
				lbl3.AutoSize = true;
				lbl4.AutoSize = true;
				lbl2.Text = "Информация о утке:";
				lbl2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
				lbl2.ForeColor = Color.DarkBlue;
				lbl2.AutoSize = true;
				lbl3.Text = $"Порода: {duckList[Convert.ToInt32(pct.Tag)].getporoda()}";
				lbl4.Text = $"Родители: {duckList[Convert.ToInt32(pct.Tag)].getparent()}";
				tableLayoutPanel1.Visible = true;
				tableLayoutPanel1.Controls.Add(lbl2);
				tableLayoutPanel1.Controls.Add(lbl);
				tableLayoutPanel1.Controls.Add(lbl3);
				tableLayoutPanel1.Controls.Add(lbl4);
				tableLayoutPanel1.Controls.Add(lbl1);
				btn.Text = $"Продать утку за {(float)duckList[Convert.ToInt32(pct.Tag)].getage()}$";
				btn.AutoSize = true;
				btn.Tag = Convert.ToInt32(pct.Tag);
				btn.Click += Sell;
				tableLayoutPanel1.Controls.Add(btn);
			}
		}
		private void Sell(object sender, EventArgs e)
		{
			var btn = (Button)sender;
			if (duckList.Count > 1)
			{
				for (int i = 0; i < duckList.Count; i++)
				{
					if (Convert.ToInt32(btn.Tag) == duckList[i].getid())
					{
						tableLayoutPanel1.Visible = false;
						balance += duckList[Convert.ToInt32(btn.Tag)].getage();
						duckList.Remove(duckList[Convert.ToInt32(btn.Tag)]);
						duckImg[Convert.ToInt32(btn.Tag)].Dispose();
						duckImg.Remove(duckImg[Convert.ToInt32(btn.Tag)]);
						duckangelx.Remove(duckangelx[Convert.ToInt32(btn.Tag)]);
						duckangely.Remove(duckangely[Convert.ToInt32(btn.Tag)]);
					}
				}
			}
			else
			{
				MessageBox.Show("Вы не можете продать утку, когда она у вас всего одна!", "Ошибка!");
			}
		}
		private void Sparivanie(Duck duck1, Duck duck2)
		{
			Duck duck = new Duck();
			if (duck1.getporoda() == "Уникальная" || duck2.getporoda() == "Уникальная")
			{
				duck.setporoda("Уникальная");
			}
			else
			{
				duck.setporoda("Обычная");
			}
			x = (int)((double)(duckImg[duck1.getid()].BackColor.R *0.6 + duckImg[duck2.getid()].BackColor.R * 0.4));
			y = (int)((double)(duckImg[duck1.getid()].BackColor.G * 0.6 + duckImg[duck2.getid()].BackColor.G * 0.4));
			z = (int)((double)(duckImg[duck1.getid()].BackColor.B * 0.6 + duckImg[duck2.getid()].BackColor.B * 0.4));
			k1 = duckImg[duck1.getid()].Location.X;
			k2 = duckImg[duck1.getid()].Location.Y;
			duck.setid(ID);
			duck.setparent($"{duck1.getname()}:{duck2.getname()}");
			duck.setname(names[rnd.Next(0, names.Length)]);
			duckImg.Add(GenSparPct(x,y,z, k1, k2));
			pictureBox1.Controls.Add(duckImg[duckImg.Count - 1]);
			duckImg[duckImg.Count - 1].Parent = pictureBox1;
			duckImg[duckImg.Count - 1].BringToFront();
			duckList.Add(duck);
			duckangelx.Add(rnd.Next(-3, 3));
			duckangely.Add(rnd.Next(-3, 3));
		}
		private void button1_Click(object sender, EventArgs e)
		{
			if (balance >= 5 && duckList.Count < maxduck)
			{
				if (rnd.Next(0, 10) >= 7)
				{
					Duck duck = new Duck();
					duck.setporoda("Уникальная");
					duck.setid(ID);
					duck.setname(names[rnd.Next(0, names.Length)]);
					duckImg.Add(GenUniqPct());
					pictureBox1.Controls.Add(duckImg[duckImg.Count - 1]);
					duckImg[duckImg.Count - 1].Parent = pictureBox1;
					duckImg[duckImg.Count - 1].BringToFront();
					duckList.Add(duck);
					duckangelx.Add(rnd.Next(-3, 3));
					duckangely.Add(rnd.Next(-3, 3));
				}
				else
				{
					Duck duck = new Duck();
					duck.setid(ID);
					duck.setname(names[rnd.Next(0, names.Length)]);
					duckImg.Add(GenPct());
					pictureBox1.Controls.Add(duckImg[duckImg.Count - 1]);
					duckImg[duckImg.Count - 1].Parent = pictureBox1;
					duckImg[duckImg.Count - 1].BringToFront();
					duckList.Add(duck);
					duckangelx.Add(rnd.Next(-3, 3));
					duckangely.Add(rnd.Next(-3, 3));
				}
				balance -= 5;
			}
			else
			{
				if (balance < 5)
				{
					MessageBox.Show("Недостаточно средств!", "Ошибка!");
				}
				else
				{
					MessageBox.Show("В озере недостаточно места!", "Ошибка!");
				}
			}
		}

		private void timer2_Tick(object sender, EventArgs e)
		{
			balance += duckList.Count;
			for (int i = 0; i < duckList.Count; i++)
			{
				duckList[i].setage(duckList[i].getage() + 1);
				if (duckList[i].getage() < 20)
				{
					duckImg[i].Size = new Size(duckImg[i].Width + 1, duckImg[i].Height + 1);
				}
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			if (balance > 50)
			{
				maxduck++;
				balance -= 50;
			}
			else
			{
				MessageBox.Show("Недостаточно средств!", "Ошибка!");
			}
		}
	}

	class Duck
	{
		int age = 0;
		int id = 0;
		string name = "";
		string poroda = "Обычная";
		string parents = "Неизвестно";
		public int getage() { return age; }
		public int getid() { return id; }
		public string getname() { return name; }
		public string getporoda() { return poroda; }
		public string getparent() { return parents; }

		public void setage(int setage) { age = setage; }
		public void setid(int setid) { id = setid; }
		public void setname(string setname) { name = setname; }
		public void setporoda(string setporoda) { poroda = setporoda; }
		public void setparent(string setparent) { parents = setparent; }

	}
}
