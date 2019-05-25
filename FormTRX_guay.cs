﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RaposoFact
{
    public partial class FormTRX_guay : Form
    {
        public FormTRX_guay()
        {
            InitializeComponent();
        }
        double res;

        private void loadGRilla()
        {
            Clases.ClsCaja a1 = new Clases.ClsCaja();
            string fecha;
            fecha = dateTimePicker1.Value.ToString("dd/MM/yyyy");

            DataSet dex = a1.kxMiguay(Convert.ToDateTime(fecha));

            //Borrar grilla
            dataGridView1.Rows.Clear();
            //PErsonalizar columnas grilla
            dataGridView1.Columns[0].Width = 72;
            dataGridView1.Columns[1].Width = 150;
            dataGridView1.Columns["Column3"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["Column4"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["Column5"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns[2].Width = 55;
            dataGridView1.Columns[3].Width = 55;
            dataGridView1.Columns[4].Width = 55;

            //Llenar grilla desde dataset
            for (int i = 0; i <= dex.Tables[0].Rows.Count - 1; i++)
            {
                dataGridView1.Rows.Add();
                // MessageBox.Show(dex.Tables[0].Rows[i].ItemArray[1].ToString());
                dataGridView1[0, i].Value = dex.Tables[0].Rows[i].ItemArray[2].ToString().Substring(0, 10);
                dataGridView1[1, i].Value = dex.Tables[0].Rows[i].ItemArray[3].ToString();
                if (dex.Tables[0].Rows[i].ItemArray[1].ToString() == "1")
                {
                    dataGridView1[2, i].Value = String.Format("{0:f2}", dex.Tables[0].Rows[i].ItemArray[4]);
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(191, 222, 185);
                }
                else
                {
                    dataGridView1[3, i].Value = String.Format("{0:f2}", dex.Tables[0].Rows[i].ItemArray[4]);
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(246, 255, 218);
                }
                dataGridView1[4, i].Value = String.Format("{0:f2}", dex.Tables[0].Rows[i].ItemArray[5]);

            }
        }
        private void limpiar()
        {
            textBox1.Text = "0";
            textBox2.Text = "";
            textBox1.Focus();
        }
        private void FormTRX_guay_Load(object sender, EventArgs e)
        {
            loadGRilla();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //convertir a double para que reconozca la coma como punto deciml

            // MessageBox.Show(""+valor);
            if (radioButton2.Checked)
            {
                Clases.ClsCaja a = new Clases.ClsCaja();
                a.trx_guay(Convert.ToDateTime(DateTime.Now.ToString()), textBox2.Text, res, -1);
                limpiar();
            }

            if (radioButton1.Checked)
            {
                Clases.ClsCaja a1 = new Clases.ClsCaja();
                a1.trx_guay(Convert.ToDateTime(DateTime.Now.ToString()), textBox2.Text, res,1);
                limpiar();
            }

            //Recargar la grilla
            loadGRilla();
            
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                //MessageBox.Show("Debe completar la informacion");
                textBox1.Focus();
                return;

            }

            res = Convert.ToDouble(textBox1.Text, System.Globalization.CultureInfo.InvariantCulture);

            //textBox1.Text= string.Format("{0:f2}", res);
          

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar) || (e.KeyChar == (char)Keys.Back) || (e.KeyChar == '.')))
                e.Handled = true;
            if (e.KeyChar == 13) // Si no es numerico y si no es espacio
            {
                this.button1.Focus();
            
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //Form1no newMDIChildForm = new Form1no();
            //newMDIChildForm.MdiParent = this.MdiParent;

            //newMDIChildForm.Show();
            //this.Close();
            loadGRilla();

        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) // Si no es numerico y si no es espacio
            {
                this.button1.Focus();

            }
        }
    }
}
