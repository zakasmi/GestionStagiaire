using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Stagiaire
{
    public partial class Notes : Form
    {
        SqlConnection cnx = new SqlConnection("Data Source=DELL ;Initial Catalog=Stagiaire ;Integrated Security=True;Pooling=False");
        SqlDataReader dr;



        public Notes()
        {
            InitializeComponent();
            dataGridView1.Columns.Add("notes", "Notes");
            dataGridView1.Columns.Add("matieres", "Matières");
            label6.Text = LesFonctions.IDstagiaire;
            label3.Text = LesFonctions.nom;
            label4.Text = LesFonctions.prenom;
     

        }

        private void Notes_Load(object sender, EventArgs e)
        {
            label9.Visible = false;
            
          
            
            
            SqlCommand cmd = new SqlCommand("select *from Note1 where idstg='"+LesFonctions.IDstagiaire+"'",cnx);
            cnx.Open();
            dr=cmd.ExecuteReader();


            while(dr.Read()){

            dataGridView1.Rows.Add(dr[1],dr[2]);
            comboBox1.Items.Add(dr[1]);
            }
            comboBox1.SelectedIndex = -1;
           // textBox1.Text = dataGridView1.Rows[0].Cells[1].Value.ToString();

            cnx.Close();

            if (comboBox1.Items.Count == 0)
            {
                comboBox1.Enabled = false;
                textBox1.Enabled = false;
                label9.Visible = true;
            }

        }

    

        private void Notes_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            Stagiaire stg = new Stagiaire();
            stg.Show();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i ;
            i = comboBox1.SelectedIndex;

            textBox1.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();


        }

        private void label1_Click(object sender, EventArgs e)
        {
            
            Stagiaire stg = new Stagiaire();
            this.Hide();
            stg.Show();

        }

        private void button1_Click(object sender, EventArgs e)
        {   
try{
            if((!string.IsNullOrEmpty(textBox1.Text) )&& (!string.IsNullOrEmpty(textBox1.Text)))
             {
            SqlCommand cmd = new SqlCommand("insert into Note1 values ("+LesFonctions.IDstagiaire+","+comboBox1.Text+","+textBox1.Text+")",cnx);
            cnx.Open();

            cmd.ExecuteNonQuery();

            cnx.Close();

            dataGridView1.Rows.Clear();
            comboBox1.Items.Clear();
            textBox1.Text = null;

            Notes_Load(sender, e);

            } else { MessageBox.Show("s'il vous plaît remplir les champs !!", "Ajouter"); }
                
    
} 
            catch (Exception e4)
            {

                MessageBox.Show(""+ e4.Message);
               
            }
            finally {

                cnx.Close();
            
            }
        












        }

    }
}
